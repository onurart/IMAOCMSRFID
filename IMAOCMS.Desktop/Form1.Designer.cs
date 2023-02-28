namespace IMAOCMS.Desktop
{
    partial class Form1
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
            btn_conn = new Button();
            cb_com = new ComboBox();
            cb_bau = new ComboBox();
            cb_data = new ComboBox();
            cb_stop = new ComboBox();
            cb_checked = new ComboBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            pictureBox1 = new PictureBox();
            out1 = new Button();
            btn_read = new Button();
            btn_laohua = new Button();
            txt_outcount = new TextBox();
            txt_laohuaterval = new TextBox();
            txt_xunhuanterval = new TextBox();
            txt_readinterval = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            groupBox3 = new GroupBox();
            groupBox4 = new GroupBox();
            txt_addr = new TextBox();
            groupBox5 = new GroupBox();
            btn_setpara = new Button();
            groupBox6 = new GroupBox();
            btn_outopen = new Button();
            btn_outclose = new Button();
            btn_xunhuan = new Button();
            txt_incount = new TextBox();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            groupBox7 = new GroupBox();
            btn_control = new Button();
            cb_sha = new ComboBox();
            txt_controltimeout = new TextBox();
            txt_controlcount = new TextBox();
            txt_startchanel = new TextBox();
            label16 = new Label();
            label15 = new Label();
            label14 = new Label();
            label13 = new Label();
            groupBox8 = new GroupBox();
            chk_log = new CheckBox();
            groupBox9 = new GroupBox();
            txt_log = new TextBox();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            SuspendLayout();
            // 
            // btn_conn
            // 
            btn_conn.Location = new Point(521, 38);
            btn_conn.Name = "btn_conn";
            btn_conn.Size = new Size(79, 28);
            btn_conn.TabIndex = 0;
            btn_conn.Text = "Bağlan ";
            btn_conn.UseVisualStyleBackColor = true;
            btn_conn.Click += btn_conn_Click;
            // 
            // cb_com
            // 
            cb_com.FormattingEnabled = true;
            cb_com.Location = new Point(6, 42);
            cb_com.Name = "cb_com";
            cb_com.Size = new Size(85, 23);
            cb_com.TabIndex = 1;
            // 
            // cb_bau
            // 
            cb_bau.FormattingEnabled = true;
            cb_bau.Items.AddRange(new object[] { "4800", "9600", "14400", "19200", "38400", "57600", "115200" });
            cb_bau.Location = new Point(108, 42);
            cb_bau.Name = "cb_bau";
            cb_bau.Size = new Size(82, 23);
            cb_bau.TabIndex = 2;
            // 
            // cb_data
            // 
            cb_data.FormattingEnabled = true;
            cb_data.Items.AddRange(new object[] { "7", "8" });
            cb_data.Location = new Point(316, 42);
            cb_data.Name = "cb_data";
            cb_data.Size = new Size(81, 23);
            cb_data.TabIndex = 3;
            // 
            // cb_stop
            // 
            cb_stop.FormattingEnabled = true;
            cb_stop.Items.AddRange(new object[] { "1", "1,5", "2" });
            cb_stop.Location = new Point(413, 42);
            cb_stop.Name = "cb_stop";
            cb_stop.Size = new Size(79, 23);
            cb_stop.TabIndex = 4;
            // 
            // cb_checked
            // 
            cb_checked.FormattingEnabled = true;
            cb_checked.Items.AddRange(new object[] { "None", "Odd", "Even" });
            cb_checked.Location = new Point(208, 42);
            cb_checked.Name = "cb_checked";
            cb_checked.Size = new Size(89, 23);
            cb_checked.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Location = new Point(2, 278);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1458, 174);
            groupBox1.TabIndex = 6;
            groupBox1.TabStop = false;
            groupBox1.Text = "Flashover kontrolü";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(pictureBox1);
            groupBox2.Controls.Add(out1);
            groupBox2.Location = new Point(2, 458);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(1459, 179);
            groupBox2.TabIndex = 7;
            groupBox2.TabStop = false;
            groupBox2.Text = "Röle çıkışı";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.close;
            pictureBox1.Location = new Point(192, 52);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(131, 94);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // out1
            // 
            out1.Location = new Point(78, 79);
            out1.Name = "out1";
            out1.Size = new Size(75, 23);
            out1.TabIndex = 0;
            out1.Text = "button1";
            out1.UseVisualStyleBackColor = true;
            // 
            // btn_read
            // 
            btn_read.Location = new Point(557, 26);
            btn_read.Name = "btn_read";
            btn_read.Size = new Size(108, 31);
            btn_read.TabIndex = 9;
            btn_read.Text = "Start Read Status";
            btn_read.UseVisualStyleBackColor = true;
            // 
            // btn_laohua
            // 
            btn_laohua.Location = new Point(862, 26);
            btn_laohua.Name = "btn_laohua";
            btn_laohua.Size = new Size(108, 31);
            btn_laohua.TabIndex = 9;
            btn_laohua.Text = "Aging Test";
            btn_laohua.UseVisualStyleBackColor = true;
            btn_laohua.Click += btn_laohua_Click;
            // 
            // txt_outcount
            // 
            txt_outcount.Location = new Point(305, 31);
            txt_outcount.Name = "txt_outcount";
            txt_outcount.Size = new Size(50, 23);
            txt_outcount.TabIndex = 10;
            txt_outcount.Text = "32";
            // 
            // txt_laohuaterval
            // 
            txt_laohuaterval.Location = new Point(789, 31);
            txt_laohuaterval.Name = "txt_laohuaterval";
            txt_laohuaterval.Size = new Size(25, 23);
            txt_laohuaterval.TabIndex = 10;
            txt_laohuaterval.Text = "1";
            // 
            // txt_xunhuanterval
            // 
            txt_xunhuanterval.Location = new Point(1071, 34);
            txt_xunhuanterval.Name = "txt_xunhuanterval";
            txt_xunhuanterval.Size = new Size(35, 23);
            txt_xunhuanterval.TabIndex = 10;
            txt_xunhuanterval.Text = "0.5";
            txt_xunhuanterval.TextChanged += txt_xunhuanterval_TextChanged;
            // 
            // txt_readinterval
            // 
            txt_readinterval.Location = new Point(471, 31);
            txt_readinterval.Name = "txt_readinterval";
            txt_readinterval.Size = new Size(38, 23);
            txt_readinterval.TabIndex = 10;
            txt_readinterval.Text = "1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 19);
            label1.Name = "label1";
            label1.Size = new Size(35, 15);
            label1.TabIndex = 11;
            label1.Text = "COM";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(108, 19);
            label2.Name = "label2";
            label2.Size = new Size(26, 15);
            label2.TabIndex = 11;
            label2.Text = "HIZ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(208, 19);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 11;
            label3.Text = "KONTROL";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(316, 19);
            label4.Name = "label4";
            label4.Size = new Size(34, 15);
            label4.TabIndex = 11;
            label4.Text = "DATE";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(413, 19);
            label5.Name = "label5";
            label5.Size = new Size(53, 15);
            label5.TabIndex = 11;
            label5.Text = "STOP BİT";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(cb_com);
            groupBox3.Controls.Add(label5);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(label4);
            groupBox3.Controls.Add(cb_bau);
            groupBox3.Controls.Add(label3);
            groupBox3.Controls.Add(label2);
            groupBox3.Controls.Add(cb_checked);
            groupBox3.Controls.Add(cb_data);
            groupBox3.Controls.Add(btn_conn);
            groupBox3.Controls.Add(cb_stop);
            groupBox3.Location = new Point(2, 28);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(606, 83);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            groupBox3.Text = "Serial";
            // 
            // groupBox4
            // 
            groupBox4.BackgroundImageLayout = ImageLayout.Center;
            groupBox4.Controls.Add(txt_addr);
            groupBox4.Location = new Point(626, 28);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(119, 83);
            groupBox4.TabIndex = 13;
            groupBox4.TabStop = false;
            groupBox4.Text = "Cİhaz Addres";
            groupBox4.Enter += groupBox4_Enter;
            // 
            // txt_addr
            // 
            txt_addr.Location = new Point(6, 42);
            txt_addr.Name = "txt_addr";
            txt_addr.Size = new Size(40, 23);
            txt_addr.TabIndex = 0;
            txt_addr.Text = "1";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btn_setpara);
            groupBox5.Location = new Point(770, 28);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(168, 83);
            groupBox5.TabIndex = 14;
            groupBox5.TabStop = false;
            groupBox5.Text = "Parametre ayarlarını aç";
            // 
            // btn_setpara
            // 
            btn_setpara.Location = new Point(6, 42);
            btn_setpara.Name = "btn_setpara";
            btn_setpara.Size = new Size(147, 23);
            btn_setpara.TabIndex = 0;
            btn_setpara.Text = "Parametre ayarlarını aç";
            btn_setpara.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(btn_outopen);
            groupBox6.Controls.Add(btn_outclose);
            groupBox6.Controls.Add(btn_xunhuan);
            groupBox6.Controls.Add(txt_laohuaterval);
            groupBox6.Controls.Add(txt_incount);
            groupBox6.Controls.Add(label12);
            groupBox6.Controls.Add(label11);
            groupBox6.Controls.Add(txt_xunhuanterval);
            groupBox6.Controls.Add(label10);
            groupBox6.Controls.Add(label9);
            groupBox6.Controls.Add(btn_laohua);
            groupBox6.Controls.Add(label8);
            groupBox6.Controls.Add(label7);
            groupBox6.Controls.Add(label6);
            groupBox6.Controls.Add(txt_readinterval);
            groupBox6.Controls.Add(btn_read);
            groupBox6.Controls.Add(txt_outcount);
            groupBox6.Location = new Point(8, 117);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(1463, 69);
            groupBox6.TabIndex = 15;
            groupBox6.TabStop = false;
            groupBox6.Text = "Ekipman Testi";
            // 
            // btn_outopen
            // 
            btn_outopen.Location = new Point(1330, 33);
            btn_outopen.Name = "btn_outopen";
            btn_outopen.Size = new Size(123, 23);
            btn_outopen.TabIndex = 14;
            btn_outopen.Text = "Çıkış Tamamen Açık";
            btn_outopen.UseVisualStyleBackColor = true;
            // 
            // btn_outclose
            // 
            btn_outclose.Location = new Point(1208, 34);
            btn_outclose.Name = "btn_outclose";
            btn_outclose.Size = new Size(116, 23);
            btn_outclose.TabIndex = 13;
            btn_outclose.Text = "Tüm Çıktılar Kapalı";
            btn_outclose.UseVisualStyleBackColor = true;
            // 
            // btn_xunhuan
            // 
            btn_xunhuan.Location = new Point(1112, 34);
            btn_xunhuan.Name = "btn_xunhuan";
            btn_xunhuan.Size = new Size(90, 23);
            btn_xunhuan.TabIndex = 12;
            btn_xunhuan.Text = "Döngü testi";
            btn_xunhuan.UseVisualStyleBackColor = true;
            // 
            // txt_incount
            // 
            txt_incount.Location = new Point(126, 31);
            txt_incount.Name = "txt_incount";
            txt_incount.Size = new Size(58, 23);
            txt_incount.TabIndex = 11;
            txt_incount.Text = "32";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(976, 34);
            label12.Name = "label12";
            label12.Size = new Size(89, 15);
            label12.TabIndex = 0;
            label12.Text = "Cycle interval：";
            label12.Click += label10_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(820, 34);
            label11.Name = "label11";
            label11.Size = new Size(36, 15);
            label11.TabIndex = 0;
            label11.Text = "ikinci";
            label11.Click += label10_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(683, 34);
            label10.Name = "label10";
            label10.Size = new Size(100, 15);
            label10.TabIndex = 0;
            label10.Text = "Yaşlanma aralığı：";
            label10.Click += label10_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(515, 34);
            label9.Name = "label9";
            label9.Size = new Size(36, 15);
            label9.TabIndex = 0;
            label9.Text = "ikinci";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(376, 34);
            label8.Name = "label8";
            label8.Size = new Size(86, 15);
            label8.TabIndex = 0;
            label8.Text = "Okuma aralığı:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(196, 34);
            label7.Name = "label7";
            label7.Size = new Size(90, 15);
            label7.TabIndex = 0;
            label7.Text = "Röle çıkış sayısı";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(6, 34);
            label6.Name = "label6";
            label6.Size = new Size(114, 15);
            label6.TabIndex = 0;
            label6.Text = "Anahtar girişi sayısı:";
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(btn_control);
            groupBox7.Controls.Add(cb_sha);
            groupBox7.Controls.Add(txt_controltimeout);
            groupBox7.Controls.Add(txt_controlcount);
            groupBox7.Controls.Add(txt_startchanel);
            groupBox7.Controls.Add(label16);
            groupBox7.Controls.Add(label15);
            groupBox7.Controls.Add(label14);
            groupBox7.Controls.Add(label13);
            groupBox7.Location = new Point(12, 192);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(660, 69);
            groupBox7.TabIndex = 16;
            groupBox7.TabStop = false;
            groupBox7.Text = "Flashover kontrolü";
            // 
            // btn_control
            // 
            btn_control.Location = new Point(536, 36);
            btn_control.Name = "btn_control";
            btn_control.Size = new Size(75, 23);
            btn_control.TabIndex = 5;
            btn_control.Text = "Kontrol";
            btn_control.UseVisualStyleBackColor = true;
            // 
            // cb_sha
            // 
            cb_sha.FormattingEnabled = true;
            cb_sha.Location = new Point(389, 37);
            cb_sha.Name = "cb_sha";
            cb_sha.Size = new Size(121, 23);
            cb_sha.TabIndex = 4;
            // 
            // txt_controltimeout
            // 
            txt_controltimeout.Location = new Point(240, 40);
            txt_controltimeout.Name = "txt_controltimeout";
            txt_controltimeout.Size = new Size(100, 23);
            txt_controltimeout.TabIndex = 3;
            // 
            // txt_controlcount
            // 
            txt_controlcount.Location = new Point(122, 40);
            txt_controlcount.Name = "txt_controlcount";
            txt_controlcount.Size = new Size(100, 23);
            txt_controlcount.TabIndex = 2;
            // 
            // txt_startchanel
            // 
            txt_startchanel.Location = new Point(6, 40);
            txt_startchanel.Name = "txt_startchanel";
            txt_startchanel.Size = new Size(100, 23);
            txt_startchanel.TabIndex = 1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(389, 19);
            label16.Name = "label16";
            label16.Size = new Size(93, 15);
            label16.TabIndex = 0;
            label16.Text = "Flaş açık / kapalı";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(240, 19);
            label15.Name = "label15";
            label15.Size = new Size(127, 15);
            label15.TabIndex = 0;
            label15.Text = "Kontrol gecikme süresi";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(122, 19);
            label14.Name = "label14";
            label14.Size = new Size(86, 15);
            label14.TabIndex = 0;
            label14.Text = "Kontrol miktarı";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(6, 19);
            label13.Name = "label13";
            label13.Size = new Size(72, 15);
            label13.TabIndex = 0;
            label13.Text = "kanalı başlat";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(chk_log);
            groupBox8.Location = new Point(3, 643);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(72, 54);
            groupBox8.TabIndex = 17;
            groupBox8.TabStop = false;
            groupBox8.Text = "Zaman";
            // 
            // chk_log
            // 
            chk_log.AutoSize = true;
            chk_log.Checked = true;
            chk_log.CheckState = CheckState.Checked;
            chk_log.Location = new Point(6, 22);
            chk_log.Name = "chk_log";
            chk_log.Size = new Size(15, 14);
            chk_log.TabIndex = 18;
            chk_log.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(txt_log);
            groupBox9.Location = new Point(3, 703);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(1457, 88);
            groupBox9.TabIndex = 18;
            groupBox9.TabStop = false;
            groupBox9.Text = "groupBox9";
            // 
            // txt_log
            // 
            txt_log.Location = new Point(0, 22);
            txt_log.Name = "txt_log";
            txt_log.Size = new Size(1451, 23);
            txt_log.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1473, 792);
            Controls.Add(groupBox9);
            Controls.Add(groupBox8);
            Controls.Add(groupBox7);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btn_conn;
        private ComboBox cb_com;
        private ComboBox cb_bau;
        private ComboBox cb_data;
        private ComboBox cb_stop;
        private ComboBox cb_checked;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btn_read;
        private Button btn_laohua;
        private TextBox txt_outcount;
        private TextBox txt_laohuaterval;
        private TextBox txt_xunhuanterval;
        private TextBox txt_readinterval;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private GroupBox groupBox3;
        private GroupBox groupBox4;
        private TextBox txt_addr;
        private GroupBox groupBox5;
        private Button btn_setpara;
        private GroupBox groupBox6;
        private Label label6;
        private TextBox txt_incount;
        private Label label10;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label12;
        private Label label11;
        private Button btn_outopen;
        private Button btn_outclose;
        private Button btn_xunhuan;
        private GroupBox groupBox7;
        private Button btn_control;
        private ComboBox cb_sha;
        private TextBox txt_controltimeout;
        private TextBox txt_controlcount;
        private TextBox txt_startchanel;
        private Label label16;
        private Label label15;
        private Label label14;
        private Label label13;
        private GroupBox groupBox8;
        private CheckBox chk_log;
        private GroupBox groupBox9;
        private TextBox txt_log;
        private Button out1;
        private PictureBox pictureBox1;
    }
}