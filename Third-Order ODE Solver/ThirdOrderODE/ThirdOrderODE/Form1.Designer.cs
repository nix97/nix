namespace ThirdOrderODE
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.tbFunction = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_a = new System.Windows.Forms.TextBox();
            this.tb_b = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_yt0 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_yt01 = new System.Windows.Forms.TextBox();
            this.tb_yt02 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_M = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.butRun = new System.Windows.Forms.Button();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.butExample = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.rbExample1 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbExactY3 = new System.Windows.Forms.TextBox();
            this.tbExactY2 = new System.Windows.Forms.TextBox();
            this.tbExactY = new System.Windows.Forms.TextBox();
            this.rbY3 = new System.Windows.Forms.RadioButton();
            this.rbY2 = new System.Windows.Forms.RadioButton();
            this.rbY = new System.Windows.Forms.RadioButton();
            this.cbExact = new System.Windows.Forms.CheckBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.butAbout = new System.Windows.Forms.Button();
            this.tbHelp = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.butClear = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbRK4All = new System.Windows.Forms.RadioButton();
            this.rbRK4Y3 = new System.Windows.Forms.RadioButton();
            this.rbRK4Y2 = new System.Windows.Forms.RadioButton();
            this.rbRK4Y = new System.Windows.Forms.RadioButton();
            this.butFormula = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Y \' \' \' = H ( t, y, x, z ) = ";
            // 
            // tbFunction
            // 
            this.tbFunction.Location = new System.Drawing.Point(122, 12);
            this.tbFunction.Name = "tbFunction";
            this.tbFunction.Size = new System.Drawing.Size(281, 20);
            this.tbFunction.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "x = y \' ;";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "z = y \' \' ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(90, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "a = ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 388);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Result :";
            // 
            // tb_a
            // 
            this.tb_a.Location = new System.Drawing.Point(121, 68);
            this.tb_a.Name = "tb_a";
            this.tb_a.Size = new System.Drawing.Size(98, 20);
            this.tb_a.TabIndex = 2;
            // 
            // tb_b
            // 
            this.tb_b.Location = new System.Drawing.Point(121, 94);
            this.tb_b.Name = "tb_b";
            this.tb_b.Size = new System.Drawing.Size(98, 20);
            this.tb_b.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(90, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "b = ";
            // 
            // tb_yt0
            // 
            this.tb_yt0.Location = new System.Drawing.Point(121, 120);
            this.tb_yt0.Name = "tb_yt0";
            this.tb_yt0.Size = new System.Drawing.Size(98, 20);
            this.tb_yt0.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(64, 123);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 11;
            this.label9.Text = "y ( t 0 ) = ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(50, 149);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "y \' ( t 0 ) = ";
            // 
            // tb_yt01
            // 
            this.tb_yt01.Location = new System.Drawing.Point(121, 146);
            this.tb_yt01.Name = "tb_yt01";
            this.tb_yt01.Size = new System.Drawing.Size(98, 20);
            this.tb_yt01.TabIndex = 5;
            // 
            // tb_yt02
            // 
            this.tb_yt02.Location = new System.Drawing.Point(121, 172);
            this.tb_yt02.Name = "tb_yt02";
            this.tb_yt02.Size = new System.Drawing.Size(98, 20);
            this.tb_yt02.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(54, 175);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(61, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "y \' \' ( t 0 ) = ";
            // 
            // tb_M
            // 
            this.tb_M.Location = new System.Drawing.Point(121, 198);
            this.tb_M.Name = "tb_M";
            this.tb_M.Size = new System.Drawing.Size(98, 20);
            this.tb_M.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(78, 201);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(28, 13);
            this.label12.TabIndex = 17;
            this.label12.Text = "M = ";
            // 
            // butRun
            // 
            this.butRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butRun.Location = new System.Drawing.Point(272, 337);
            this.butRun.Name = "butRun";
            this.butRun.Size = new System.Drawing.Size(80, 31);
            this.butRun.TabIndex = 8;
            this.butRun.Text = "Run";
            this.butRun.UseVisualStyleBackColor = true;
            this.butRun.Click += new System.EventHandler(this.butRun_Click);
            // 
            // tbResult
            // 
            this.tbResult.Location = new System.Drawing.Point(15, 414);
            this.tbResult.Multiline = true;
            this.tbResult.Name = "tbResult";
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbResult.Size = new System.Drawing.Size(614, 281);
            this.tbResult.TabIndex = 20;
            // 
            // butExample
            // 
            this.butExample.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butExample.Location = new System.Drawing.Point(308, 12);
            this.butExample.Name = "butExample";
            this.butExample.Size = new System.Drawing.Size(65, 24);
            this.butExample.TabIndex = 16;
            this.butExample.Text = "Input";
            this.butExample.UseVisualStyleBackColor = true;
            this.butExample.Click += new System.EventHandler(this.butExample_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.radioButton2);
            this.groupBox1.Controls.Add(this.butExample);
            this.groupBox1.Controls.Add(this.rbExample1);
            this.groupBox1.Location = new System.Drawing.Point(272, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 98);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Example";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(33, 85);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(243, 13);
            this.label13.TabIndex = 29;
            this.label13.Text = "or input Y\'\'\'=(t^2*z-3*t*x+4*y+5*t^2*ln(t)+9*t^3)/t^3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "or input Y\'\'\'=exp(t)+y-x+z";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(15, 59);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(243, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "2. t^3* y\'\'\'-t^2* y\'\'+3*t*y\'-4y = 5*t^2*ln(t) + 9*t^3";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // rbExample1
            // 
            this.rbExample1.AutoSize = true;
            this.rbExample1.Checked = true;
            this.rbExample1.Location = new System.Drawing.Point(15, 19);
            this.rbExample1.Name = "rbExample1";
            this.rbExample1.Size = new System.Drawing.Size(113, 17);
            this.rbExample1.TabIndex = 0;
            this.rbExample1.TabStop = true;
            this.rbExample1.Text = "1. y\'\'\'-y\'\'+y\'-y=exp(t)";
            this.rbExample1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbExactY3);
            this.groupBox2.Controls.Add(this.tbExactY2);
            this.groupBox2.Controls.Add(this.tbExactY);
            this.groupBox2.Controls.Add(this.rbY3);
            this.groupBox2.Controls.Add(this.rbY2);
            this.groupBox2.Controls.Add(this.rbY);
            this.groupBox2.Location = new System.Drawing.Point(272, 204);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(401, 108);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Exact";
            // 
            // tbExactY3
            // 
            this.tbExactY3.Location = new System.Drawing.Point(98, 70);
            this.tbExactY3.Name = "tbExactY3";
            this.tbExactY3.Size = new System.Drawing.Size(281, 20);
            this.tbExactY3.TabIndex = 15;
            // 
            // tbExactY2
            // 
            this.tbExactY2.Location = new System.Drawing.Point(98, 44);
            this.tbExactY2.Name = "tbExactY2";
            this.tbExactY2.Size = new System.Drawing.Size(281, 20);
            this.tbExactY2.TabIndex = 13;
            // 
            // tbExactY
            // 
            this.tbExactY.Location = new System.Drawing.Point(98, 18);
            this.tbExactY.Name = "tbExactY";
            this.tbExactY.Size = new System.Drawing.Size(281, 20);
            this.tbExactY.TabIndex = 11;
            // 
            // rbY3
            // 
            this.rbY3.AutoSize = true;
            this.rbY3.Location = new System.Drawing.Point(15, 65);
            this.rbY3.Name = "rbY3";
            this.rbY3.Size = new System.Drawing.Size(90, 17);
            this.rbY3.TabIndex = 14;
            this.rbY3.Text = "3.  Y  \' \' ( t ) = ";
            this.rbY3.UseVisualStyleBackColor = true;
            // 
            // rbY2
            // 
            this.rbY2.AutoSize = true;
            this.rbY2.Location = new System.Drawing.Point(15, 42);
            this.rbY2.Name = "rbY2";
            this.rbY2.Size = new System.Drawing.Size(82, 17);
            this.rbY2.TabIndex = 12;
            this.rbY2.Text = "2.  Y \' ( t ) = ";
            this.rbY2.UseVisualStyleBackColor = true;
            // 
            // rbY
            // 
            this.rbY.AutoSize = true;
            this.rbY.Checked = true;
            this.rbY.Location = new System.Drawing.Point(15, 19);
            this.rbY.Name = "rbY";
            this.rbY.Size = new System.Drawing.Size(77, 17);
            this.rbY.TabIndex = 0;
            this.rbY.TabStop = true;
            this.rbY.Text = "1. Y  ( t ) = ";
            this.rbY.UseVisualStyleBackColor = true;
            // 
            // cbExact
            // 
            this.cbExact.AutoSize = true;
            this.cbExact.Location = new System.Drawing.Point(272, 177);
            this.cbExact.Name = "cbExact";
            this.cbExact.Size = new System.Drawing.Size(214, 17);
            this.cbExact.TabIndex = 10;
            this.cbExact.Text = "Have an Exact solution (for comparison)";
            this.cbExact.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(697, 204);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(649, 491);
            this.chart1.TabIndex = 27;
            this.chart1.Text = "chart1";
            // 
            // butAbout
            // 
            this.butAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAbout.Location = new System.Drawing.Point(608, 358);
            this.butAbout.Name = "butAbout";
            this.butAbout.Size = new System.Drawing.Size(65, 24);
            this.butAbout.TabIndex = 22;
            this.butAbout.Text = "About";
            this.butAbout.UseVisualStyleBackColor = true;
            this.butAbout.Click += new System.EventHandler(this.butAbout_Click);
            // 
            // tbHelp
            // 
            this.tbHelp.Location = new System.Drawing.Point(697, 41);
            this.tbHelp.Multiline = true;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbHelp.Size = new System.Drawing.Size(309, 144);
            this.tbHelp.TabIndex = 28;
            this.tbHelp.Text = resources.GetString("tbHelp.Text");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(694, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Help :";
            // 
            // butClear
            // 
            this.butClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butClear.Location = new System.Drawing.Point(406, 337);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(80, 31);
            this.butClear.TabIndex = 9;
            this.butClear.Text = "Clear";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbRK4All);
            this.groupBox3.Controls.Add(this.rbRK4Y3);
            this.groupBox3.Controls.Add(this.rbRK4Y2);
            this.groupBox3.Controls.Add(this.rbRK4Y);
            this.groupBox3.Location = new System.Drawing.Point(21, 246);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(151, 71);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "View RK4";
            // 
            // rbRK4All
            // 
            this.rbRK4All.AutoSize = true;
            this.rbRK4All.Location = new System.Drawing.Point(81, 42);
            this.rbRK4All.Name = "rbRK4All";
            this.rbRK4All.Size = new System.Drawing.Size(48, 17);
            this.rbRK4All.TabIndex = 3;
            this.rbRK4All.Text = "4. All";
            this.rbRK4All.UseVisualStyleBackColor = true;
            // 
            // rbRK4Y3
            // 
            this.rbRK4Y3.AutoSize = true;
            this.rbRK4Y3.Location = new System.Drawing.Point(81, 19);
            this.rbRK4Y3.Name = "rbRK4Y3";
            this.rbRK4Y3.Size = new System.Drawing.Size(63, 17);
            this.rbRK4Y3.TabIndex = 2;
            this.rbRK4Y3.Text = "3. Y \' \' k";
            this.rbRK4Y3.UseVisualStyleBackColor = true;
            // 
            // rbRK4Y2
            // 
            this.rbRK4Y2.AutoSize = true;
            this.rbRK4Y2.Location = new System.Drawing.Point(16, 42);
            this.rbRK4Y2.Name = "rbRK4Y2";
            this.rbRK4Y2.Size = new System.Drawing.Size(58, 17);
            this.rbRK4Y2.TabIndex = 1;
            this.rbRK4Y2.Text = "2. Y \' k";
            this.rbRK4Y2.UseVisualStyleBackColor = true;
            // 
            // rbRK4Y
            // 
            this.rbRK4Y.AutoSize = true;
            this.rbRK4Y.Checked = true;
            this.rbRK4Y.Location = new System.Drawing.Point(15, 19);
            this.rbRK4Y.Name = "rbRK4Y";
            this.rbRK4Y.Size = new System.Drawing.Size(53, 17);
            this.rbRK4Y.TabIndex = 0;
            this.rbRK4Y.TabStop = true;
            this.rbRK4Y.Text = "1. Y k";
            this.rbRK4Y.UseVisualStyleBackColor = true;
            // 
            // butFormula
            // 
            this.butFormula.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butFormula.Location = new System.Drawing.Point(608, 318);
            this.butFormula.Name = "butFormula";
            this.butFormula.Size = new System.Drawing.Size(65, 24);
            this.butFormula.TabIndex = 30;
            this.butFormula.Text = "Formula";
            this.butFormula.UseVisualStyleBackColor = true;
            this.butFormula.Click += new System.EventHandler(this.butFormula_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 729);
            this.Controls.Add(this.butFormula);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.butClear);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbHelp);
            this.Controls.Add(this.butAbout);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.cbExact);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.butRun);
            this.Controls.Add(this.tb_M);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tb_yt02);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tb_yt01);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tb_yt0);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.tb_b);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_a);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbFunction);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Third-Order ODE Solver by Lukas Setiawan";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbFunction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_a;
        private System.Windows.Forms.TextBox tb_b;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_yt0;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_yt01;
        private System.Windows.Forms.TextBox tb_yt02;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_M;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button butRun;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.Button butExample;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton rbExample1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbY2;
        private System.Windows.Forms.RadioButton rbY;
        private System.Windows.Forms.CheckBox cbExact;
        private System.Windows.Forms.RadioButton rbY3;
        private System.Windows.Forms.TextBox tbExactY;
        private System.Windows.Forms.TextBox tbExactY2;
        private System.Windows.Forms.TextBox tbExactY3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button butAbout;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbHelp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbRK4Y2;
        private System.Windows.Forms.RadioButton rbRK4Y;
        private System.Windows.Forms.RadioButton rbRK4All;
        private System.Windows.Forms.RadioButton rbRK4Y3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button butFormula;
    }
}

