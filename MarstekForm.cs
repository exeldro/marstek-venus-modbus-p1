using System.Globalization;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Text;
using NModbus;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Linq;

namespace Marstek;

public partial class MarstekForm : Form
{
    double received = 0;
    double delivered = 0;
    double prevReceived = 0;
    double prevDelivered = 0;
    DateTime lastP1 = DateTime.MinValue;
    DateTime lastChange = DateTime.MinValue;
    bool running = true;
    ushort prevAlarm = 0;
    ushort prevAlarm2 = 0;
    ushort prevFault = 0;
    ushort prevFault2 = 0;
    bool restartMarstek = false;
    HttpClient httpClient = new HttpClient();


    public MarstekForm()
    {
        InitializeComponent();
        UseImmersiveDarkMode(Handle, true);

        numericUpDownChargeDiff.Value = Properties.Settings.Default.ChargeDiff;
        numericUpDownDischargeDiff.Value = Properties.Settings.Default.DischargeDiff;

        var pt = new Thread(P1Thread);
        pt.Name = "P1Thread";
        pt.Start();
        var mt = new Thread(MarstekThread);
        mt.Name = "MarstekThread";
        mt.Start();
    }

    void Log(string message)
    {
        if (!running)
            return;
        Invoke(() =>
        {
            textBoxLog.Text += "\r\n" + DateTime.Now + " " + message;
        });
    }

    void P1Thread()
    {
        while (running)
        {
            try
            {
                using (var p1Connection = new TcpClient())
                {
                    p1Connection.Connect(Properties.Settings.Default.P1Host, Properties.Settings.Default.P1Port);
                    using (var p1Stream = p1Connection.GetStream())
                    {
                        string incomingTelegram = "";
                        while (running)
                        {
                            var buffer = new byte[2048];
                            var bc = p1Stream.Read(buffer, 0, buffer.Length);
                            var message = Encoding.UTF8.GetString(buffer, 0, bc);
                            if (message[0] == '/')
                            {
                                incomingTelegram = message;
                            }
                            else
                            {
                                incomingTelegram += message;
                            }
                            if (incomingTelegram.StartsWith("/") && incomingTelegram.Contains("!") && incomingTelegram.EndsWith("\r\n"))
                            {
                                Match match = Regex.Match(incomingTelegram, @"^1-0:1\.7\.0\(([0-9.]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                                if (match.Success)
                                {
                                    prevDelivered = delivered;
                                    delivered = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                                }
                                match = Regex.Match(incomingTelegram, @"^1-0:2\.7\.0\(([0-9.]+)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                                if (match.Success)
                                {
                                    prevReceived = received;
                                    received = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                                }
                                lastP1 = DateTime.Now;
                                int watt = Convert.ToInt32((delivered - received) * 1000.0);
                                if (!running)
                                    break;
                                Invoke(
() =>
{
    textBoxP1Watt.Text = watt.ToString() + "W";
    chart1.Series["P1"].Points.AddXY(lastP1, watt);
    while (chart1.Series["P1"].Points.Count() >= 120)
    {
        chart1.Series["P1"].Points.RemoveAt(0);
        chart1.ResetAutoValues();
    }
});

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log("P1Thread error: " + e.ToString());
            }
        }
    }

    void MarstekThread()
    {

        TimeSpan maxP1diff = new TimeSpan(0, 0, 30);
        var factory = new ModbusFactory();
        while (running)
        {
            try
            {
                using (var sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    var serverIP = IPAddress.Parse(Properties.Settings.Default.MarstekHost);
                    var serverFullAddr = new IPEndPoint(serverIP, Properties.Settings.Default.MarstekPort);
                    sock.ReceiveTimeout = 5000;
                    sock.SendTimeout = 5000;
                    sock.Connect(serverFullAddr);

                    IModbusMaster master = factory.CreateMaster(sock);
                    byte slaveId = 1;
                    var step = 0;
                    while (running)
                    {

                        //ushort startAddress = 35000;
                        ushort[] registers;
                        var uiActions = new List<Action>();

                        if (step == 10)
                        {
                            registers = master.ReadHoldingRegisters(slaveId, 32100, 6);
                            var batteryVoltage = registers[0] * 0.01; //volt
                            var batteryCurrent = registers[1] * 0.01; //ampere
                            var batteryPower = int.Parse(registers[2].ToString("x4") + registers[3].ToString("x4"), NumberStyles.HexNumber);
                            var batterySOC = registers[4]; //%
                            var batteryTotalEnergy = registers[5] * 0.001;//kwh
                            uiActions.Add(() =>
                            {
                                textBoxVenusPower.Text = batteryPower.ToString() + "W";
                                textBoxSOC.Text = batterySOC.ToString() + "%";
                            });
                            try
                            {
                                httpClient.GetAsync(Properties.Settings.Default.ReportUrl + "MarstekPower/" + batteryPower);
                                httpClient.GetAsync(Properties.Settings.Default.ReportUrl + "MarstekSOC/" + batterySOC);
                            }
                            catch (Exception ex)
                            {
                                //Log("Error saving production: " + ex.Message);
                            }
                        }

                        registers = master.ReadHoldingRegisters(slaveId, 32200, 5);
                        var acVoltage = registers[0] * 0.1; // volt
                        var acCurrent = registers[1] * 0.01; // ampere
                        var acPower = int.Parse(registers[2].ToString("x4") + registers[3].ToString("x4"), NumberStyles.HexNumber);
                        var acFrequency = registers[4] * 0.01; //Hz

                        if (step == 20)
                        {
                            registers = master.ReadHoldingRegisters(slaveId, 33000, 12);
                            var totalCharge = uint.Parse(registers[0].ToString("x4") + registers[1].ToString("x4"), NumberStyles.HexNumber);
                            var totalDischarge = uint.Parse(registers[2].ToString("x4") + registers[3].ToString("x4"), NumberStyles.HexNumber);
                            var dailyCharge = uint.Parse(registers[4].ToString("x4") + registers[5].ToString("x4"), NumberStyles.HexNumber);
                            var dailyDischarge = uint.Parse(registers[6].ToString("x4") + registers[7].ToString("x4"), NumberStyles.HexNumber);
                            var monthlyCharge = uint.Parse(registers[8].ToString("x4") + registers[9].ToString("x4"), NumberStyles.HexNumber);
                            var monthlyDischarge = uint.Parse(registers[10].ToString("x4") + registers[11].ToString("x4"), NumberStyles.HexNumber);
                        }

                        if (step == 30)
                        {
                            registers = master.ReadHoldingRegisters(slaveId, 35000, 3);
                            var internalTemp = registers[0] * 0.1;
                            var internalM1Temp = registers[1] * 0.1;
                            var internalM2Temp = registers[2] * 0.1;
                            uiActions.Add(() =>
                            {
                                textBoxInternalTemp.Text = internalTemp.ToString("N1") + "°C";
                                textBoxInternalM1Temp.Text = internalM1Temp.ToString("N1") + "°C";
                                textBoxInternalM2Temp.Text = internalM2Temp.ToString("N1") + "°C";
                            });
                            try
                            {
                                httpClient.GetAsync(Properties.Settings.Default.ReportUrl + "MarstekTemp/" + internalTemp);
                            }
                            catch (Exception ex)
                            {
                                //Log("Error saving internal temp: " + ex.Message);
                            }
                        }

                        //registers = master.ReadHoldingRegisters(slaveId, 35010, 2);
                        //var maxCellTemp = registers[0] * 0.1;
                        //var minCellTemp = registers[1] * 0.1;

                        //registers = master.ReadHoldingRegisters(slaveId, 35100, 1);
                        //var inverterState = registers[0];

                        if (step == 40)
                        {
                            registers = master.ReadHoldingRegisters(slaveId, 36000, 2);
                            if (registers[0] != prevAlarm)
                            {
                                Log("Alarm changed from " + prevAlarm + " to " + registers[0]);
                                prevAlarm = registers[0];
                            }
                            if (registers[1] != prevAlarm2)
                            {
                                Log("Alarm2 changed from " + prevAlarm2 + " to " + registers[1]);
                                prevAlarm2 = registers[1];
                            }
                        }
                        if (step == 50)
                        {
                            registers = master.ReadHoldingRegisters(slaveId, 36100, 2);
                            if (registers[0] != prevFault)
                            {
                                Log("Fault changed from " + prevFault + " to " + registers[0]);
                                prevFault = registers[0];
                            }
                            if (registers[1] != prevFault2)
                            {
                                Log("Fault2 changed from " + prevFault2 + " to " + registers[1]);
                                prevFault2 = registers[1];
                            }
                            var fault = registers[0];
                            var fault2 = registers[1];
                            var gridOverVoltage = (fault & (1 << 0)) != 0;
                            var gridUnderVoltage = (fault & (1 << 1)) != 0;
                            var gridOverFrequency = (fault & (1 << 2)) != 0;
                            var gridUnderFrequency = (fault & (1 << 3)) != 0;
                            var gridPeakVoltageAbnormal = (fault & (1 << 4)) != 0;
                            var wifiAbnormal = (fault2 & (1 << 0)) != 0;
                            var bleAbnormal = (fault2 & (1 << 1)) != 0;
                            var networkAbnormal = (fault2 & (1 << 2)) != 0;
                            var ctConnectionAbnormal = (fault2 & (1 << 3)) != 0;
                        }

                        if (restartMarstek)
                        {
                            registers = master.ReadHoldingRegisters(slaveId, 41000, 1);
                            var deviceRestart = registers[0].ToString("X4");
                            if (deviceRestart != "55AA")
                            {
                                ushort restart = ushort.Parse("55AA", NumberStyles.HexNumber);
                                master.WriteSingleRegister(slaveId, 42000, restart);
                                restartMarstek = false;
                            }
                        }

                        //registers = master.ReadHoldingRegisters(slaveId, 42020, 2);
                        //var forceCharge = registers[0];
                        //var forceDischarge = registers[1];

                        //registers = master.ReadHoldingRegisters(slaveId, 43000, 1);
                        //var userWorkMode = registers[0];

                        if (DateTime.Now - lastP1 > maxP1diff) // no p1 data for x seconds
                        {
                            registers = master.ReadHoldingRegisters(slaveId, 42010, 1);
                            var forceChargeDischarge = registers[0];

                            if (forceChargeDischarge == 0)
                            {

                            }
                            else if (forceChargeDischarge == 1) // charge
                            {

                            }
                            else if (forceChargeDischarge == 2) // discharge
                            {
                                master.WriteSingleRegister(slaveId, 42020, 0);
                                master.WriteSingleRegister(slaveId, 42021, 0);
                                master.WriteSingleRegister(slaveId, 42010, 0);
                            }
                            Thread.Sleep(100);
                        }
                        else if (lastChange != lastP1)
                        {
                            lastChange = lastP1;
                            int p1PreviousWatt = Convert.ToInt32((prevDelivered - prevReceived) * 1000.0);
                            int p1watt = Convert.ToInt32((delivered - received) * 1000.0);
                            //int watt = p1watt + acPower;
                            if ((acPower > 0 && p1watt + acPower > 100) || p1watt + acPower > 120)
                            {
                                // start discharging
                                var largeDischarge = p1watt + acPower - numericUpDownDischargeDiff.Value;
                                ushort discharge = largeDischarge > 2500 ? (ushort)2500 : Convert.ToUInt16(largeDischarge);
                                if (!running)
                                    break;
                                //chart1.Series["Venus"].Points
                                master.WriteSingleRegister(slaveId, 42020, 0);
                                master.WriteSingleRegister(slaveId, 42021, discharge);
                                master.WriteSingleRegister(slaveId, 42010, 2);
                                if (!running)
                                    break;
                                Invoke(() =>
                                {
                                    chart1.Series["Venus"].Points.AddXY(lastChange, discharge * -1);
                                    while (chart1.Series["Venus"].Points.Count() >= 120)
                                    {
                                        chart1.Series["Venus"].Points.RemoveAt(0);
                                        chart1.ResetAutoValues();
                                    }
                                    textBoxVenusCharge.Text = (discharge * -1).ToString() + "W";
                                });
                            }
                            else if ((acPower < 0 && p1watt + acPower < -80) || p1watt + acPower < -100)
                            {
                                // start charging
                                var largeCharge = (p1watt + acPower) * -1 - numericUpDownChargeDiff.Value;
                                ushort charge = largeCharge > 2500 ? (ushort)2500 : Convert.ToUInt16(largeCharge);
                                master.WriteSingleRegister(slaveId, 42020, charge);
                                master.WriteSingleRegister(slaveId, 42021, 0);
                                master.WriteSingleRegister(slaveId, 42010, 1);
                                if (!running)
                                    break;
                                Invoke(() =>
                                {
                                    chart1.Series["Venus"].Points.AddXY(lastChange, charge);
                                    while (chart1.Series["Venus"].Points.Count() >= 120)
                                    {
                                        chart1.Series["Venus"].Points.RemoveAt(0);
                                        chart1.ResetAutoValues();
                                    }
                                    textBoxVenusCharge.Text = charge.ToString() + "W";
                                });
                            }
                            else
                            {
                                master.WriteSingleRegister(slaveId, 42020, 0);
                                master.WriteSingleRegister(slaveId, 42021, 0);
                                master.WriteSingleRegister(slaveId, 42010, 0);
                                if (!running)
                                    break;
                                Invoke(() =>
                                {
                                    chart1.Series["Venus"].Points.AddXY(lastChange, 0);
                                    while (chart1.Series["Venus"].Points.Count() >= 120)
                                    {
                                        chart1.Series["Venus"].Points.RemoveAt(0);
                                        chart1.ResetAutoValues();
                                    }
                                    textBoxVenusCharge.Text = "0W";
                                });
                            }
                            if (!running)
                                break;


                            registers = master.ReadHoldingRegisters(slaveId, 42000, 1);
                            var rs485Mode = registers[0].ToString("X4");
                            if (rs485Mode != "55AA")
                            {
                                ushort mode = ushort.Parse("55AA", NumberStyles.HexNumber);
                                master.WriteSingleRegister(slaveId, 42000, mode);
                            }

                            Thread.Sleep(100);
                        }
                        else
                        {
                            Thread.Sleep(50);
                        }

                        Invoke(() =>
                        {
                            foreach (var uiAction in uiActions)
                            {
                                uiAction();
                            }
                            textBoxAcPower.Text = acPower.ToString() + "W";
                        });

                        step++;
                        if (step >= 100)
                            step = 0;
                    }
                    ushort newMode = ushort.Parse("55BB", NumberStyles.HexNumber);
                    master.WriteSingleRegister(slaveId, 42000, newMode);
                }
            }
            catch (SocketException e)
            {
                //Log("Marstek connection error");
            }
            catch (Exception e)
            {
                Log("MarstekThread error: " + e.ToString());
            }
        }
    }

    private void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
        running = false;
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
        running = false;
    }

    private void buttonRestart_Click(object sender, EventArgs e)
    {
        restartMarstek = true;
    }

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr,
ref int attrValue, int attrSize);

    private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
    private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

    internal static bool UseImmersiveDarkMode(IntPtr handle, bool enabled)
    {
        if (IsWindows10OrGreater(17763))
        {
            var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
            if (IsWindows10OrGreater(18985))
            {
                attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
            }

            int useImmersiveDarkMode = enabled ? 1 : 0;
            return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
        }

        return false;
    }

    private static bool IsWindows10OrGreater(int build = -1)
    {
        return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
    }

    private void numericUpDownChargeDiff_ValueChanged(object sender, EventArgs e)
    {
        Properties.Settings.Default.ChargeDiff = Convert.ToInt32(numericUpDownChargeDiff.Value);
    }

    private void numericUpDownDischargeDiff_ValueChanged(object sender, EventArgs e)
    {
        Properties.Settings.Default.DischargeDiff = Convert.ToInt32(numericUpDownDischargeDiff.Value);
    }
}
