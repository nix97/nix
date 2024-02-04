namespace AES
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.butClear = new System.Windows.Forms.Button();
            this.tbHelp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.butAbout = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tbIV = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rb256 = new System.Windows.Forms.RadioButton();
            this.rb192 = new System.Windows.Forms.RadioButton();
            this.rb128 = new System.Windows.Forms.RadioButton();
            this.tbCipher = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPlain = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.butRun = new System.Windows.Forms.Button();
            this.tbKey = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.butClearDec = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbIVDec = new System.Windows.Forms.TextBox();
            this.tbPlainDec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbCipherDec = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btRunDec = new System.Windows.Forms.Button();
            this.tbKeyDec = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(-1, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 509);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.butClear);
            this.tabPage1.Controls.Add(this.tbHelp);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.butAbout);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.tbIV);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.tbCipher);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.tbPlain);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.butRun);
            this.tabPage1.Controls.Add(this.tbKey);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(764, 483);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Encryption";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // butClear
            // 
            this.butClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butClear.Location = new System.Drawing.Point(493, 377);
            this.butClear.Name = "butClear";
            this.butClear.Size = new System.Drawing.Size(74, 29);
            this.butClear.TabIndex = 6;
            this.butClear.Text = "Clear";
            this.butClear.UseVisualStyleBackColor = true;
            this.butClear.Click += new System.EventHandler(this.butClear_Click);
            // 
            // tbHelp
            // 
            this.tbHelp.Location = new System.Drawing.Point(493, 140);
            this.tbHelp.Multiline = true;
            this.tbHelp.Name = "tbHelp";
            this.tbHelp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbHelp.Size = new System.Drawing.Size(265, 145);
            this.tbHelp.TabIndex = 25;
            this.tbHelp.Text = resources.GetString("tbHelp.Text");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(490, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Help :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(161, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 23;
            this.label7.Text = "Key :";
            // 
            // butAbout
            // 
            this.butAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAbout.Location = new System.Drawing.Point(693, 291);
            this.butAbout.Name = "butAbout";
            this.butAbout.Size = new System.Drawing.Size(65, 22);
            this.butAbout.TabIndex = 22;
            this.butAbout.Text = "About";
            this.butAbout.UseVisualStyleBackColor = true;
            this.butAbout.Click += new System.EventHandler(this.butAbout_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(161, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "IV :";
            // 
            // tbIV
            // 
            this.tbIV.Location = new System.Drawing.Point(203, 46);
            this.tbIV.Name = "tbIV";
            this.tbIV.Size = new System.Drawing.Size(423, 20);
            this.tbIV.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb256);
            this.groupBox1.Controls.Add(this.rb192);
            this.groupBox1.Controls.Add(this.rb128);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(117, 94);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Key size in bit";
            // 
            // rb256
            // 
            this.rb256.AutoSize = true;
            this.rb256.Location = new System.Drawing.Point(6, 64);
            this.rb256.Name = "rb256";
            this.rb256.Size = new System.Drawing.Size(46, 17);
            this.rb256.TabIndex = 3;
            this.rb256.Text = "256";
            this.rb256.UseVisualStyleBackColor = true;
            // 
            // rb192
            // 
            this.rb192.AutoSize = true;
            this.rb192.Location = new System.Drawing.Point(6, 40);
            this.rb192.Name = "rb192";
            this.rb192.Size = new System.Drawing.Size(46, 17);
            this.rb192.TabIndex = 2;
            this.rb192.Text = "192";
            this.rb192.UseVisualStyleBackColor = true;
            // 
            // rb128
            // 
            this.rb128.AutoSize = true;
            this.rb128.Checked = true;
            this.rb128.Location = new System.Drawing.Point(6, 19);
            this.rb128.Name = "rb128";
            this.rb128.Size = new System.Drawing.Size(46, 17);
            this.rb128.TabIndex = 1;
            this.rb128.TabStop = true;
            this.rb128.Text = "128";
            this.rb128.UseVisualStyleBackColor = true;
            // 
            // tbCipher
            // 
            this.tbCipher.Location = new System.Drawing.Point(12, 328);
            this.tbCipher.Multiline = true;
            this.tbCipher.Name = "tbCipher";
            this.tbCipher.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCipher.Size = new System.Drawing.Size(457, 145);
            this.tbCipher.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 312);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Ciphertext :";
            // 
            // tbPlain
            // 
            this.tbPlain.Location = new System.Drawing.Point(12, 140);
            this.tbPlain.Multiline = true;
            this.tbPlain.Name = "tbPlain";
            this.tbPlain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPlain.Size = new System.Drawing.Size(457, 145);
            this.tbPlain.TabIndex = 4;
            this.tbPlain.Text = "This is an example of Cryptography using AES algorithm, presented by Lukas Setiaw" +
    "an.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Plaintext :";
            // 
            // butRun
            // 
            this.butRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butRun.Location = new System.Drawing.Point(493, 328);
            this.butRun.Name = "butRun";
            this.butRun.Size = new System.Drawing.Size(74, 29);
            this.butRun.TabIndex = 5;
            this.butRun.Text = "Run";
            this.butRun.UseVisualStyleBackColor = true;
            this.butRun.Click += new System.EventHandler(this.butRun_Click);
            // 
            // tbKey
            // 
            this.tbKey.Location = new System.Drawing.Point(203, 11);
            this.tbKey.Name = "tbKey";
            this.tbKey.Size = new System.Drawing.Size(423, 20);
            this.tbKey.TabIndex = 7;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.butClearDec);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.tbIVDec);
            this.tabPage2.Controls.Add(this.tbPlainDec);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.tbCipherDec);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.btRunDec);
            this.tabPage2.Controls.Add(this.tbKeyDec);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(764, 483);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Decryption";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // butClearDec
            // 
            this.butClearDec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butClearDec.Location = new System.Drawing.Point(532, 180);
            this.butClearDec.Name = "butClearDec";
            this.butClearDec.Size = new System.Drawing.Size(74, 29);
            this.butClearDec.TabIndex = 6;
            this.butClearDec.Text = "Clear";
            this.butClearDec.UseVisualStyleBackColor = true;
            this.butClearDec.Click += new System.EventHandler(this.butClearDec_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(15, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(36, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Key :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(15, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "IV :";
            // 
            // tbIVDec
            // 
            this.tbIVDec.Location = new System.Drawing.Point(52, 52);
            this.tbIVDec.Name = "tbIVDec";
            this.tbIVDec.Size = new System.Drawing.Size(423, 20);
            this.tbIVDec.TabIndex = 2;
            // 
            // tbPlainDec
            // 
            this.tbPlainDec.Location = new System.Drawing.Point(18, 315);
            this.tbPlainDec.Multiline = true;
            this.tbPlainDec.Name = "tbPlainDec";
            this.tbPlainDec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbPlainDec.Size = new System.Drawing.Size(457, 145);
            this.tbPlainDec.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 299);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Plaintext :";
            // 
            // tbCipherDec
            // 
            this.tbCipherDec.Location = new System.Drawing.Point(18, 127);
            this.tbCipherDec.Multiline = true;
            this.tbCipherDec.Name = "tbCipherDec";
            this.tbCipherDec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCipherDec.Size = new System.Drawing.Size(457, 145);
            this.tbCipherDec.TabIndex = 3;
            this.tbCipherDec.Text = "\r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Ciphertext :";
            // 
            // btRunDec
            // 
            this.btRunDec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btRunDec.Location = new System.Drawing.Point(532, 127);
            this.btRunDec.Name = "btRunDec";
            this.btRunDec.Size = new System.Drawing.Size(74, 29);
            this.btRunDec.TabIndex = 5;
            this.btRunDec.Text = "Run";
            this.btRunDec.UseVisualStyleBackColor = true;
            this.btRunDec.Click += new System.EventHandler(this.btRunDec_Click);
            // 
            // tbKeyDec
            // 
            this.tbKeyDec.Location = new System.Drawing.Point(52, 15);
            this.tbKeyDec.Name = "tbKeyDec";
            this.tbKeyDec.Size = new System.Drawing.Size(423, 20);
            this.tbKeyDec.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 511);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Cryptography using AES algorithm by Lukas Setiawan";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button butRun;
        private System.Windows.Forms.TextBox tbKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPlain;
        private System.Windows.Forms.TextBox tbCipher;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rb192;
        private System.Windows.Forms.RadioButton rb128;
        private System.Windows.Forms.TextBox tbPlainDec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbCipherDec;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btRunDec;
        private System.Windows.Forms.TextBox tbKeyDec;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbIVDec;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbIV;
        private System.Windows.Forms.Button butAbout;
        private System.Windows.Forms.TextBox tbHelp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rb256;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button butClear;
        private System.Windows.Forms.Button butClearDec;
    }
}

