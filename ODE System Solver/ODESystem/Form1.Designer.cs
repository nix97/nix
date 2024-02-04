namespace WindowsFormsApp1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.label1 = new System.Windows.Forms.Label();
            this.xEqu = new System.Windows.Forms.TextBox();
            this.yEqu = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.atb = new System.Windows.Forms.TextBox();
            this.btb = new System.Windows.Forms.TextBox();
            this.yt0tb = new System.Windows.Forms.TextBox();
            this.xt0tb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.mtb = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.AnX = new System.Windows.Forms.TextBox();
            this.AnY = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m = new System.Windows.Forms.Label();
            this.butRun = new System.Windows.Forms.Button();
            this.result = new System.Windows.Forms.Label();
            this.rtbResult = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label11 = new System.Windows.Forms.Label();
            this.ExactCb = new System.Windows.Forms.CheckBox();
            this.rbx = new System.Windows.Forms.RadioButton();
            this.rby = new System.Windows.Forms.RadioButton();
            this.btClear = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbExp = new System.Windows.Forms.RadioButton();
            this.label18 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.rbDeci = new System.Windows.Forms.RadioButton();
            this.butAbout = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "x \' = f ( t , x , y ) = ";
            // 
            // xEqu
            // 
            this.xEqu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xEqu.Location = new System.Drawing.Point(150, 14);
            this.xEqu.Name = "xEqu";
            this.xEqu.Size = new System.Drawing.Size(286, 23);
            this.xEqu.TabIndex = 8;
            this.xEqu.Text = "x+2*y";
            // 
            // yEqu
            // 
            this.yEqu.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yEqu.Location = new System.Drawing.Point(150, 43);
            this.yEqu.Name = "yEqu";
            this.yEqu.Size = new System.Drawing.Size(286, 23);
            this.yEqu.TabIndex = 9;
            this.yEqu.Text = "3*x+2*y";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(21, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "y \' = g ( t , x , y ) = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "a = ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "b = ";
            // 
            // atb
            // 
            this.atb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.atb.Location = new System.Drawing.Point(150, 72);
            this.atb.Name = "atb";
            this.atb.Size = new System.Drawing.Size(110, 23);
            this.atb.TabIndex = 19;
            this.atb.Text = "0";
            // 
            // btb
            // 
            this.btb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btb.Location = new System.Drawing.Point(150, 101);
            this.btb.Name = "btb";
            this.btb.Size = new System.Drawing.Size(110, 23);
            this.btb.TabIndex = 20;
            this.btb.Text = "0.2";
            // 
            // yt0tb
            // 
            this.yt0tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yt0tb.Location = new System.Drawing.Point(150, 158);
            this.yt0tb.Name = "yt0tb";
            this.yt0tb.Size = new System.Drawing.Size(110, 23);
            this.yt0tb.TabIndex = 24;
            this.yt0tb.Text = "4";
            // 
            // xt0tb
            // 
            this.xt0tb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xt0tb.Location = new System.Drawing.Point(150, 129);
            this.xt0tb.Name = "xt0tb";
            this.xt0tb.Size = new System.Drawing.Size(110, 23);
            this.xt0tb.TabIndex = 23;
            this.xt0tb.Text = "6";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "y(t0) = ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 17);
            this.label5.TabIndex = 21;
            this.label5.Text = "x(t0) = ";
            // 
            // mtb
            // 
            this.mtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtb.Location = new System.Drawing.Point(150, 187);
            this.mtb.Name = "mtb";
            this.mtb.Size = new System.Drawing.Size(110, 23);
            this.mtb.TabIndex = 26;
            this.mtb.Text = "20";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AnX);
            this.groupBox1.Controls.Add(this.AnY);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(27, 257);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 107);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View graph";
            // 
            // AnX
            // 
            this.AnX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnX.Location = new System.Drawing.Point(64, 34);
            this.AnX.Name = "AnX";
            this.AnX.Size = new System.Drawing.Size(286, 23);
            this.AnX.TabIndex = 28;
            this.AnX.Text = "4*exp(4*t)+2*exp(-t)";
            // 
            // AnY
            // 
            this.AnY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnY.Location = new System.Drawing.Point(64, 68);
            this.AnY.Name = "AnY";
            this.AnY.Size = new System.Drawing.Size(286, 23);
            this.AnY.TabIndex = 29;
            this.AnY.Text = "6*exp(4*t)-2*exp(-t)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(61, 17);
            this.label8.TabIndex = 29;
            this.label8.Text = "y ( t ) =  ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 34);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 28;
            this.label7.Text = "x ( t ) =  ";
            // 
            // m
            // 
            this.m.AutoSize = true;
            this.m.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m.Location = new System.Drawing.Point(24, 190);
            this.m.Name = "m";
            this.m.Size = new System.Drawing.Size(35, 17);
            this.m.TabIndex = 25;
            this.m.Text = "m = ";
            // 
            // butRun
            // 
            this.butRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butRun.Location = new System.Drawing.Point(216, 394);
            this.butRun.Name = "butRun";
            this.butRun.Size = new System.Drawing.Size(100, 37);
            this.butRun.TabIndex = 28;
            this.butRun.Text = "Run";
            this.butRun.UseVisualStyleBackColor = true;
            this.butRun.Click += new System.EventHandler(this.butRun_Click);
            // 
            // result
            // 
            this.result.AutoSize = true;
            this.result.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.result.Location = new System.Drawing.Point(24, 444);
            this.result.Name = "result";
            this.result.Size = new System.Drawing.Size(56, 17);
            this.result.TabIndex = 30;
            this.result.Text = "Result :";
            // 
            // rtbResult
            // 
            this.rtbResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbResult.Location = new System.Drawing.Point(24, 475);
            this.rtbResult.Name = "rtbResult";
            this.rtbResult.Size = new System.Drawing.Size(713, 200);
            this.rtbResult.TabIndex = 33;
            this.rtbResult.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(774, 472);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 17);
            this.label6.TabIndex = 34;
            this.label6.Text = "Help :";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(777, 495);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(359, 153);
            this.richTextBox2.TabIndex = 35;
            this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ImeMode = System.Windows.Forms.ImeMode.Off;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(520, 12);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(616, 457);
            this.chart1.TabIndex = 37;
            this.chart1.Text = "chart1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(24, 224);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(309, 17);
            this.label11.TabIndex = 38;
            this.label11.Text = "Have an Analytic/Exact solution(for comparison)";
            // 
            // ExactCb
            // 
            this.ExactCb.AutoSize = true;
            this.ExactCb.Checked = true;
            this.ExactCb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ExactCb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExactCb.Location = new System.Drawing.Point(339, 225);
            this.ExactCb.Name = "ExactCb";
            this.ExactCb.Size = new System.Drawing.Size(99, 21);
            this.ExactCb.TabIndex = 39;
            this.ExactCb.Text = "Check here";
            this.ExactCb.UseVisualStyleBackColor = true;
            // 
            // rbx
            // 
            this.rbx.AutoSize = true;
            this.rbx.Checked = true;
            this.rbx.Location = new System.Drawing.Point(393, 291);
            this.rbx.Name = "rbx";
            this.rbx.Size = new System.Drawing.Size(14, 13);
            this.rbx.TabIndex = 40;
            this.rbx.TabStop = true;
            this.rbx.UseVisualStyleBackColor = true;
            // 
            // rby
            // 
            this.rby.AutoSize = true;
            this.rby.Location = new System.Drawing.Point(393, 325);
            this.rby.Name = "rby";
            this.rby.Size = new System.Drawing.Size(14, 13);
            this.rby.TabIndex = 41;
            this.rby.UseVisualStyleBackColor = true;
            // 
            // btClear
            // 
            this.btClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClear.Location = new System.Drawing.Point(338, 394);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(100, 37);
            this.btClear.TabIndex = 43;
            this.btClear.Text = "Clear";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbExp);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.rbDeci);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(27, 370);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(139, 71);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Display format";
            // 
            // rbExp
            // 
            this.rbExp.AutoSize = true;
            this.rbExp.Location = new System.Drawing.Point(105, 51);
            this.rbExp.Name = "rbExp";
            this.rbExp.Size = new System.Drawing.Size(14, 13);
            this.rbExp.TabIndex = 47;
            this.rbExp.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(14, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(67, 17);
            this.label18.TabIndex = 46;
            this.label18.Text = "Exponent";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(14, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 17);
            this.label10.TabIndex = 45;
            this.label10.Text = "Decimal";
            // 
            // rbDeci
            // 
            this.rbDeci.AutoSize = true;
            this.rbDeci.Checked = true;
            this.rbDeci.Location = new System.Drawing.Point(105, 26);
            this.rbDeci.Name = "rbDeci";
            this.rbDeci.Size = new System.Drawing.Size(14, 13);
            this.rbDeci.TabIndex = 41;
            this.rbDeci.TabStop = true;
            this.rbDeci.UseVisualStyleBackColor = true;
            // 
            // butAbout
            // 
            this.butAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAbout.Location = new System.Drawing.Point(1068, 654);
            this.butAbout.Name = "butAbout";
            this.butAbout.Size = new System.Drawing.Size(68, 27);
            this.butAbout.TabIndex = 45;
            this.butAbout.Text = "About";
            this.butAbout.UseVisualStyleBackColor = true;
            this.butAbout.Click += new System.EventHandler(this.butAbout_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 729);
            this.Controls.Add(this.butAbout);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btClear);
            this.Controls.Add(this.rby);
            this.Controls.Add(this.rbx);
            this.Controls.Add(this.ExactCb);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.rtbResult);
            this.Controls.Add(this.result);
            this.Controls.Add(this.butRun);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mtb);
            this.Controls.Add(this.m);
            this.Controls.Add(this.yt0tb);
            this.Controls.Add(this.xt0tb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btb);
            this.Controls.Add(this.atb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.yEqu);
            this.Controls.Add(this.xEqu);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Systems of Differential Equations Solver by Lukas Setiawan";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xEqu;
        private System.Windows.Forms.TextBox yEqu;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox atb;
        private System.Windows.Forms.TextBox btb;
        private System.Windows.Forms.TextBox yt0tb;
        private System.Windows.Forms.TextBox xt0tb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox mtb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label m;
        private System.Windows.Forms.TextBox AnX;
        private System.Windows.Forms.TextBox AnY;
        private System.Windows.Forms.Button butRun;
        private System.Windows.Forms.Label result;
        private System.Windows.Forms.RichTextBox rtbResult;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox ExactCb;
        private System.Windows.Forms.RadioButton rbx;
        private System.Windows.Forms.RadioButton rby;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbExp;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.RadioButton rbDeci;
        private System.Windows.Forms.Button butAbout;
    }
}

