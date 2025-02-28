namespace Marstek
{
    partial class MarstekForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarstekForm));
            textBoxP1Watt = new TextBox();
            labelP1Watt = new Label();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            tableLayoutPanel1 = new TableLayoutPanel();
            labelInternalTemp = new Label();
            labelAcPower = new Label();
            textBoxLog = new TextBox();
            textBoxSOC = new TextBox();
            textBoxInternalM2Temp = new TextBox();
            textBoxInternalM1Temp = new TextBox();
            textBoxInternalTemp = new TextBox();
            textBoxVenusCharge = new TextBox();
            labelVenusCharge = new Label();
            textBoxVenusPower = new TextBox();
            labelVenusPower = new Label();
            textBoxAcPower = new TextBox();
            buttonRestart = new Button();
            numericUpDownChargeDiff = new NumericUpDown();
            numericUpDownDischargeDiff = new NumericUpDown();
            labelChargeDiff = new Label();
            labelDischargeDiff = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownChargeDiff).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDischargeDiff).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxP1Watt
            // 
            textBoxP1Watt.BackColor = Color.Black;
            textBoxP1Watt.ForeColor = Color.White;
            textBoxP1Watt.Location = new Point(3, 30);
            textBoxP1Watt.Name = "textBoxP1Watt";
            textBoxP1Watt.ReadOnly = true;
            textBoxP1Watt.Size = new Size(74, 23);
            textBoxP1Watt.TabIndex = 0;
            textBoxP1Watt.TextAlign = HorizontalAlignment.Right;
            // 
            // labelP1Watt
            // 
            labelP1Watt.AutoSize = true;
            labelP1Watt.Location = new Point(3, 0);
            labelP1Watt.Name = "labelP1Watt";
            labelP1Watt.Size = new Size(48, 15);
            labelP1Watt.TabIndex = 1;
            labelP1Watt.Text = "P1 Watt";
            // 
            // chart1
            // 
            chart1.BackColor = Color.Black;
            chart1.BorderSkin.BorderColor = Color.White;
            chart1.BorderSkin.PageColor = Color.Black;
            chartArea1.AxisX.LabelStyle.ForeColor = Color.White;
            chartArea1.AxisX.LabelStyle.Format = "mm:ss";
            chartArea1.AxisX.LineColor = Color.White;
            chartArea1.AxisX.MajorGrid.LineColor = Color.White;
            chartArea1.AxisX.MinorGrid.LineColor = Color.White;
            chartArea1.AxisX.TitleForeColor = Color.White;
            chartArea1.AxisX2.TitleForeColor = Color.White;
            chartArea1.AxisY.LabelStyle.ForeColor = Color.White;
            chartArea1.AxisY.LineColor = Color.White;
            chartArea1.AxisY.MajorGrid.LineColor = Color.White;
            chartArea1.AxisY.MinorGrid.LineColor = Color.White;
            chartArea1.AxisY.TitleForeColor = Color.White;
            chartArea1.AxisY2.TitleForeColor = Color.White;
            chartArea1.BackColor = Color.Black;
            chartArea1.BackSecondaryColor = Color.Black;
            chartArea1.BorderColor = Color.White;
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            tableLayoutPanel2.SetColumnSpan(chart1, 3);
            chart1.Dock = DockStyle.Fill;
            legend1.BackColor = Color.Black;
            legend1.ForeColor = Color.White;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(3, 63);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.LabelBackColor = Color.Black;
            series1.LabelForeColor = Color.White;
            series1.Legend = "Legend1";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "P1";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Name = "Venus";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            chart1.Series.Add(series1);
            chart1.Series.Add(series2);
            chart1.Size = new Size(1290, 527);
            chart1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 12;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(labelInternalTemp, 4, 0);
            tableLayoutPanel1.Controls.Add(labelAcPower, 1, 0);
            tableLayoutPanel1.Controls.Add(labelP1Watt, 0, 0);
            tableLayoutPanel1.Controls.Add(textBoxP1Watt, 0, 1);
            tableLayoutPanel1.Controls.Add(textBoxLog, 11, 0);
            tableLayoutPanel1.Controls.Add(textBoxSOC, 7, 1);
            tableLayoutPanel1.Controls.Add(textBoxInternalM2Temp, 6, 1);
            tableLayoutPanel1.Controls.Add(textBoxInternalM1Temp, 5, 1);
            tableLayoutPanel1.Controls.Add(textBoxInternalTemp, 4, 1);
            tableLayoutPanel1.Controls.Add(textBoxVenusCharge, 3, 1);
            tableLayoutPanel1.Controls.Add(labelVenusCharge, 3, 0);
            tableLayoutPanel1.Controls.Add(textBoxVenusPower, 2, 1);
            tableLayoutPanel1.Controls.Add(labelVenusPower, 2, 0);
            tableLayoutPanel1.Controls.Add(textBoxAcPower, 1, 1);
            tableLayoutPanel1.Controls.Add(buttonRestart, 10, 0);
            tableLayoutPanel1.Controls.Add(numericUpDownChargeDiff, 8, 1);
            tableLayoutPanel1.Controls.Add(numericUpDownDischargeDiff, 9, 1);
            tableLayoutPanel1.Controls.Add(labelChargeDiff, 8, 0);
            tableLayoutPanel1.Controls.Add(labelDischargeDiff, 9, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1290, 54);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // labelInternalTemp
            // 
            labelInternalTemp.AutoSize = true;
            labelInternalTemp.Location = new Point(403, 0);
            labelInternalTemp.Name = "labelInternalTemp";
            labelInternalTemp.Size = new Size(74, 27);
            labelInternalTemp.TabIndex = 15;
            labelInternalTemp.Text = "Internal Temperature";
            // 
            // labelAcPower
            // 
            labelAcPower.AutoSize = true;
            labelAcPower.Location = new Point(103, 0);
            labelAcPower.Name = "labelAcPower";
            labelAcPower.Size = new Size(59, 15);
            labelAcPower.TabIndex = 13;
            labelAcPower.Text = "AC Power";
            // 
            // textBoxLog
            // 
            textBoxLog.BackColor = Color.Black;
            textBoxLog.Dock = DockStyle.Fill;
            textBoxLog.ForeColor = Color.White;
            textBoxLog.Location = new Point(1103, 3);
            textBoxLog.Multiline = true;
            textBoxLog.Name = "textBoxLog";
            textBoxLog.ReadOnly = true;
            tableLayoutPanel1.SetRowSpan(textBoxLog, 2);
            textBoxLog.Size = new Size(184, 48);
            textBoxLog.TabIndex = 11;
            // 
            // textBoxSOC
            // 
            textBoxSOC.BackColor = Color.Black;
            textBoxSOC.ForeColor = Color.White;
            textBoxSOC.Location = new Point(703, 30);
            textBoxSOC.Name = "textBoxSOC";
            textBoxSOC.ReadOnly = true;
            textBoxSOC.Size = new Size(72, 23);
            textBoxSOC.TabIndex = 10;
            textBoxSOC.TextAlign = HorizontalAlignment.Right;
            // 
            // textBoxInternalM2Temp
            // 
            textBoxInternalM2Temp.BackColor = Color.Black;
            textBoxInternalM2Temp.ForeColor = Color.White;
            textBoxInternalM2Temp.Location = new Point(603, 30);
            textBoxInternalM2Temp.Name = "textBoxInternalM2Temp";
            textBoxInternalM2Temp.ReadOnly = true;
            textBoxInternalM2Temp.Size = new Size(72, 23);
            textBoxInternalM2Temp.TabIndex = 9;
            textBoxInternalM2Temp.TextAlign = HorizontalAlignment.Right;
            // 
            // textBoxInternalM1Temp
            // 
            textBoxInternalM1Temp.BackColor = Color.Black;
            textBoxInternalM1Temp.ForeColor = Color.White;
            textBoxInternalM1Temp.Location = new Point(503, 30);
            textBoxInternalM1Temp.Name = "textBoxInternalM1Temp";
            textBoxInternalM1Temp.ReadOnly = true;
            textBoxInternalM1Temp.Size = new Size(72, 23);
            textBoxInternalM1Temp.TabIndex = 8;
            textBoxInternalM1Temp.TextAlign = HorizontalAlignment.Right;
            // 
            // textBoxInternalTemp
            // 
            textBoxInternalTemp.BackColor = Color.Black;
            textBoxInternalTemp.ForeColor = Color.White;
            textBoxInternalTemp.Location = new Point(403, 30);
            textBoxInternalTemp.Name = "textBoxInternalTemp";
            textBoxInternalTemp.ReadOnly = true;
            textBoxInternalTemp.Size = new Size(72, 23);
            textBoxInternalTemp.TabIndex = 7;
            textBoxInternalTemp.TextAlign = HorizontalAlignment.Right;
            // 
            // textBoxVenusCharge
            // 
            textBoxVenusCharge.BackColor = Color.Black;
            textBoxVenusCharge.ForeColor = Color.White;
            textBoxVenusCharge.Location = new Point(303, 30);
            textBoxVenusCharge.Name = "textBoxVenusCharge";
            textBoxVenusCharge.ReadOnly = true;
            textBoxVenusCharge.Size = new Size(74, 23);
            textBoxVenusCharge.TabIndex = 3;
            textBoxVenusCharge.TextAlign = HorizontalAlignment.Right;
            // 
            // labelVenusCharge
            // 
            labelVenusCharge.AutoSize = true;
            labelVenusCharge.Location = new Point(303, 0);
            labelVenusCharge.Name = "labelVenusCharge";
            labelVenusCharge.Size = new Size(65, 27);
            labelVenusCharge.TabIndex = 6;
            labelVenusCharge.Text = "Requested Charge";
            // 
            // textBoxVenusPower
            // 
            textBoxVenusPower.BackColor = Color.Black;
            textBoxVenusPower.ForeColor = Color.White;
            textBoxVenusPower.Location = new Point(203, 30);
            textBoxVenusPower.Name = "textBoxVenusPower";
            textBoxVenusPower.ReadOnly = true;
            textBoxVenusPower.Size = new Size(74, 23);
            textBoxVenusPower.TabIndex = 4;
            textBoxVenusPower.TextAlign = HorizontalAlignment.Right;
            // 
            // labelVenusPower
            // 
            labelVenusPower.AutoSize = true;
            labelVenusPower.Location = new Point(203, 0);
            labelVenusPower.Name = "labelVenusPower";
            labelVenusPower.Size = new Size(80, 15);
            labelVenusPower.TabIndex = 5;
            labelVenusPower.Text = "Battery Power";
            // 
            // textBoxAcPower
            // 
            textBoxAcPower.BackColor = Color.Black;
            textBoxAcPower.ForeColor = Color.White;
            textBoxAcPower.Location = new Point(103, 30);
            textBoxAcPower.Name = "textBoxAcPower";
            textBoxAcPower.ReadOnly = true;
            textBoxAcPower.Size = new Size(74, 23);
            textBoxAcPower.TabIndex = 14;
            textBoxAcPower.TextAlign = HorizontalAlignment.Right;
            // 
            // buttonRestart
            // 
            buttonRestart.BackColor = Color.Black;
            buttonRestart.ForeColor = Color.White;
            buttonRestart.Location = new Point(1003, 3);
            buttonRestart.Name = "buttonRestart";
            buttonRestart.Size = new Size(72, 21);
            buttonRestart.TabIndex = 12;
            buttonRestart.Text = "Restart";
            buttonRestart.UseVisualStyleBackColor = false;
            buttonRestart.Click += buttonRestart_Click;
            // 
            // numericUpDownChargeDiff
            // 
            numericUpDownChargeDiff.Location = new Point(803, 30);
            numericUpDownChargeDiff.Name = "numericUpDownChargeDiff";
            numericUpDownChargeDiff.Size = new Size(94, 23);
            numericUpDownChargeDiff.TabIndex = 16;
            numericUpDownChargeDiff.Value = new decimal(new int[] { 25, 0, 0, 0 });
            numericUpDownChargeDiff.ValueChanged += numericUpDownChargeDiff_ValueChanged;
            // 
            // numericUpDownDischargeDiff
            // 
            numericUpDownDischargeDiff.Location = new Point(903, 30);
            numericUpDownDischargeDiff.Name = "numericUpDownDischargeDiff";
            numericUpDownDischargeDiff.Size = new Size(94, 23);
            numericUpDownDischargeDiff.TabIndex = 17;
            numericUpDownDischargeDiff.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDownDischargeDiff.ValueChanged += numericUpDownDischargeDiff_ValueChanged;
            // 
            // labelChargeDiff
            // 
            labelChargeDiff.AutoSize = true;
            labelChargeDiff.Location = new Point(803, 0);
            labelChargeDiff.Name = "labelChargeDiff";
            labelChargeDiff.Size = new Size(67, 15);
            labelChargeDiff.TabIndex = 18;
            labelChargeDiff.Text = "Charge Diff";
            // 
            // labelDischargeDiff
            // 
            labelDischargeDiff.AutoSize = true;
            labelDischargeDiff.Location = new Point(903, 0);
            labelDischargeDiff.Name = "labelDischargeDiff";
            labelDischargeDiff.Size = new Size(81, 15);
            labelDischargeDiff.TabIndex = 19;
            labelDischargeDiff.Text = "Discharge Diff";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.Controls.Add(chart1, 0, 1);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel1, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(1296, 593);
            tableLayoutPanel2.TabIndex = 7;
            // 
            // MarstekForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1296, 593);
            Controls.Add(tableLayoutPanel2);
            ForeColor = Color.White;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MarstekForm";
            Text = "Marstek Venus";
            FormClosing += Form1_FormClosing;
            FormClosed += Form1_FormClosed;
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownChargeDiff).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownDischargeDiff).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox textBoxP1Watt;
        private Label labelP1Watt;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox textBoxVenusPower;
        private TextBox textBoxVenusCharge;
        private Label labelVenusPower;
        private Label labelVenusCharge;
        private TableLayoutPanel tableLayoutPanel2;
        private TextBox textBoxInternalTemp;
        private TextBox textBoxInternalM1Temp;
        private TextBox textBoxInternalM2Temp;
        private TextBox textBoxSOC;
        private TextBox textBoxLog;
        private Button buttonRestart;
        private Label labelAcPower;
        private TextBox textBoxAcPower;
        private Label labelInternalTemp;
        private NumericUpDown numericUpDownChargeDiff;
        private NumericUpDown numericUpDownDischargeDiff;
        private Label labelChargeDiff;
        private Label labelDischargeDiff;
    }
}
