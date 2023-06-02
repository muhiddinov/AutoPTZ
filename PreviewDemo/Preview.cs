using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.IO;
using System.Globalization;

using System.Runtime.InteropServices;

using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.Structure;

namespace PreviewDemo
{
	/// <summary>
	/// Form1 µÄÕªÒªËµÃ÷¡£
	/// </summary>
	public class Preview : System.Windows.Forms.Form
	{
        private uint iLastErr = 0;
		private Int32 m_lUserID = -1;
		private bool m_bInitSDK = false;
        private bool m_bRecord = false;
        private bool m_bTalk = false;
		private Int32 m_lRealHandle = -1;
        private int lVoiceComHandle = -1;
        private string str;

        CHCNetSDK.REALDATACALLBACK RealData = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg;

        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnPreview;
		private System.Windows.Forms.PictureBox RealPlayWnd;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.TextBox textBoxPassword;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private System.Windows.Forms.Button btnBMP;
        private System.Windows.Forms.Button btnJPEG;
        private Label label11;
        private Label label12;
        private Label label13;
        private System.Windows.Forms.TextBox textBoxChannel;
        private System.Windows.Forms.Button btnRecord;
        private Label label14;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btnVioceTalk;
        private Label label16;
        private Label label17;
        private System.Windows.Forms.TextBox textBoxID;
        /*private Button PtzGet;
        private Button PtzSet;*/
        private Label label19;
        /*private ComboBox comboBox1;
        private TextBox textBoxPanPos;
        private TextBox textBoxTiltPos;
        private TextBox textBoxZoomPos;*/
        private Label label20;
        private Label label21;
        private Label label22;
        private System.Windows.Forms.Button PreSet;
        private Label label23;
        private System.Windows.Forms.GroupBox groupBox1;
        private Label label15;
        private System.Windows.Forms.Button ctrl_left;
        private Button ctrl_right;
        private Button ctrl_down;
        private Button ctrl_home;
        private Button ctrl_up;
        private Label label18;
        private Button zoom_max;
        private Button zoom_min;
        private Button tracking_btn;
        private Label label24;
        private Label lbl_speed;
        private TrackBar trackBar1;
        private PictureBox pictureBox_process;
        private Panel panel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private Button detect_button;
        private PictureBox copy_pictureBox;

        //private GroupBox groupBox1;

        /// <summary>
        /// ±ØÐèµÄÉè¼ÆÆ÷±äÁ¿¡£
        /// </summary>
        private System.ComponentModel.Container components = null;

		public Preview()
		{
			//
			// Windows ´°ÌåÉè¼ÆÆ÷Ö§³ÖËù±ØÐèµÄ
			//
			InitializeComponent();
			m_bInitSDK = CHCNetSDK.NET_DVR_Init();
			if (m_bInitSDK == false)
			{
				MessageBox.Show("NET_DVR_Init error!");
				return;
			}
			else
			{
                //±£´æSDKÈÕÖ¾ To save the SDK log
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
			}
			//
			// TODO: ÔÚ InitializeComponent µ÷ÓÃºóÌí¼ÓÈÎºÎ¹¹Ôìº¯Êý´úÂë
			//
		}

		/// <summary>
		/// ÇåÀíËùÓÐÕýÔÚÊ¹ÓÃµÄ×ÊÔ´¡£
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if (m_lRealHandle >= 0)
			{
				CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
			}
			if (m_lUserID >= 0)
			{
				CHCNetSDK.NET_DVR_Logout(m_lUserID);
			}
			if (m_bInitSDK == true)
			{
				CHCNetSDK.NET_DVR_Cleanup();
			}
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.RealPlayWnd = new System.Windows.Forms.PictureBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnBMP = new System.Windows.Forms.Button();
            this.btnJPEG = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxChannel = new System.Windows.Forms.TextBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btnVioceTalk = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.PreSet = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_speed = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label18 = new System.Windows.Forms.Label();
            this.zoom_max = new System.Windows.Forms.Button();
            this.zoom_min = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.ctrl_left = new System.Windows.Forms.Button();
            this.ctrl_right = new System.Windows.Forms.Button();
            this.ctrl_down = new System.Windows.Forms.Button();
            this.ctrl_home = new System.Windows.Forms.Button();
            this.ctrl_up = new System.Windows.Forms.Button();
            this.tracking_btn = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.pictureBox_process = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.detect_button = new System.Windows.Forms.Button();
            this.copy_pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_process)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.copy_pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 21);
            this.label1.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 21);
            this.label2.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 21);
            this.label3.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 21);
            this.label4.TabIndex = 37;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(372, 35);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(66, 47);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(14, 530);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(64, 32);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "Live View";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // RealPlayWnd
            // 
            this.RealPlayWnd.BackColor = System.Drawing.SystemColors.InfoText;
            this.RealPlayWnd.Location = new System.Drawing.Point(15, 97);
            this.RealPlayWnd.Name = "RealPlayWnd";
            this.RealPlayWnd.Size = new System.Drawing.Size(575, 376);
            this.RealPlayWnd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.RealPlayWnd.TabIndex = 4;
            this.RealPlayWnd.TabStop = false;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(76, 28);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(95, 20);
            this.textBoxIP.TabIndex = 2;
            this.textBoxIP.Text = "192.168.0.64";
            this.textBoxIP.TextChanged += new System.EventHandler(this.textBoxIP_TextChanged);
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(266, 28);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(93, 20);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "8000";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(76, 65);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(95, 20);
            this.textBoxUserName.TabIndex = 4;
            this.textBoxUserName.Text = "admin";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPassword.Location = new System.Drawing.Point(266, 66);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(93, 20);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.Text = "hk123456789";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Device IP";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Device Port";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "User Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(198, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 511);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "preview";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 21);
            this.label10.TabIndex = 33;
            // 
            // btnBMP
            // 
            this.btnBMP.Location = new System.Drawing.Point(92, 531);
            this.btnBMP.Name = "btnBMP";
            this.btnBMP.Size = new System.Drawing.Size(85, 32);
            this.btnBMP.TabIndex = 8;
            this.btnBMP.Text = "Capture BMP ";
            this.btnBMP.UseVisualStyleBackColor = true;
            this.btnBMP.Click += new System.EventHandler(this.btnBMP_Click);
            // 
            // btnJPEG
            // 
            this.btnJPEG.Location = new System.Drawing.Point(191, 530);
            this.btnJPEG.Name = "btnJPEG";
            this.btnJPEG.Size = new System.Drawing.Size(100, 32);
            this.btnJPEG.TabIndex = 9;
            this.btnJPEG.Text = "Capture JPEG";
            this.btnJPEG.UseVisualStyleBackColor = true;
            this.btnJPEG.Click += new System.EventHandler(this.btnJPEG_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(94, 511);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "BMP capture";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(203, 510);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "JPEG capture";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(14, 484);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "preview/capture channel";
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Location = new System.Drawing.Point(142, 480);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.Size = new System.Drawing.Size(46, 20);
            this.textBoxChannel.TabIndex = 6;
            this.textBoxChannel.Text = "1";
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(305, 530);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(83, 32);
            this.btnRecord.TabIndex = 10;
            this.btnRecord.Text = "Start Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(309, 511);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "client recording";
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(191, 595);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(63, 30);
            this.btn_Exit.TabIndex = 11;
            this.btn_Exit.Tag = "";
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btnVioceTalk
            // 
            this.btnVioceTalk.Location = new System.Drawing.Point(15, 595);
            this.btnVioceTalk.Name = "btnVioceTalk";
            this.btnVioceTalk.Size = new System.Drawing.Size(63, 32);
            this.btnVioceTalk.TabIndex = 25;
            this.btnVioceTalk.Text = "Start Talk";
            this.btnVioceTalk.UseVisualStyleBackColor = true;
            this.btnVioceTalk.Click += new System.EventHandler(this.btnVioceTalk_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 577);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 26;
            this.label16.Text = "TwoWayAudio";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(198, 483);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 27;
            this.label17.Text = "stream ID";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(250, 479);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(188, 20);
            this.textBoxID.TabIndex = 28;
            // 
            // PreSet
            // 
            this.PreSet.Location = new System.Drawing.Point(96, 595);
            this.PreSet.Name = "PreSet";
            this.PreSet.Size = new System.Drawing.Size(81, 31);
            this.PreSet.TabIndex = 31;
            this.PreSet.Text = "PTZ Control";
            this.PreSet.UseVisualStyleBackColor = true;
            this.PreSet.Click += new System.EventHandler(this.PreSet_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(99, 577);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 13);
            this.label23.TabIndex = 32;
            this.label23.Text = "PTZ control";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_speed);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.zoom_max);
            this.groupBox1.Controls.Add(this.zoom_min);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.ctrl_left);
            this.groupBox1.Controls.Add(this.ctrl_right);
            this.groupBox1.Controls.Add(this.ctrl_down);
            this.groupBox1.Controls.Add(this.ctrl_home);
            this.groupBox1.Controls.Add(this.ctrl_up);
            this.groupBox1.Location = new System.Drawing.Point(514, 522);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(339, 153);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "PTZ";
            // 
            // lbl_speed
            // 
            this.lbl_speed.AutoSize = true;
            this.lbl_speed.Location = new System.Drawing.Point(227, 38);
            this.lbl_speed.Name = "lbl_speed";
            this.lbl_speed.Size = new System.Drawing.Size(13, 13);
            this.lbl_speed.TabIndex = 56;
            this.lbl_speed.Text = "4";
            this.lbl_speed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(178, 54);
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(157, 45);
            this.trackBar1.TabIndex = 55;
            this.trackBar1.Value = 4;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(179, 119);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 13);
            this.label18.TabIndex = 54;
            this.label18.Text = "ZOOM:";
            // 
            // zoom_max
            // 
            this.zoom_max.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoom_max.Location = new System.Drawing.Point(231, 105);
            this.zoom_max.Name = "zoom_max";
            this.zoom_max.Size = new System.Drawing.Size(42, 36);
            this.zoom_max.TabIndex = 53;
            this.zoom_max.Text = "+";
            this.zoom_max.UseVisualStyleBackColor = true;
            this.zoom_max.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zoom_max_MouseDown);
            this.zoom_max.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoom_max_MouseUp);
            // 
            // zoom_min
            // 
            this.zoom_min.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoom_min.Location = new System.Drawing.Point(279, 105);
            this.zoom_min.Name = "zoom_min";
            this.zoom_min.Size = new System.Drawing.Size(42, 36);
            this.zoom_min.TabIndex = 52;
            this.zoom_min.Text = "-";
            this.zoom_min.UseVisualStyleBackColor = true;
            this.zoom_min.MouseDown += new System.Windows.Forms.MouseEventHandler(this.zoom_min_MouseDown);
            this.zoom_min.MouseUp += new System.Windows.Forms.MouseEventHandler(this.zoom_min_MouseUp);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(175, 38);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 13);
            this.label15.TabIndex = 51;
            this.label15.Text = "SPEED:";
            // 
            // ctrl_left
            // 
            this.ctrl_left.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl_left.Location = new System.Drawing.Point(16, 63);
            this.ctrl_left.Name = "ctrl_left";
            this.ctrl_left.Size = new System.Drawing.Size(42, 36);
            this.ctrl_left.TabIndex = 49;
            this.ctrl_left.Text = "←";
            this.ctrl_left.UseVisualStyleBackColor = true;
            this.ctrl_left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrl_left_MouseDown);
            this.ctrl_left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrl_left_MouseUp);
            // 
            // ctrl_right
            // 
            this.ctrl_right.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl_right.Location = new System.Drawing.Point(112, 63);
            this.ctrl_right.Name = "ctrl_right";
            this.ctrl_right.Size = new System.Drawing.Size(42, 36);
            this.ctrl_right.TabIndex = 48;
            this.ctrl_right.Text = "→";
            this.ctrl_right.UseVisualStyleBackColor = true;
            this.ctrl_right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrl_right_MouseDown);
            this.ctrl_right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrl_right_MouseUp);
            // 
            // ctrl_down
            // 
            this.ctrl_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl_down.Location = new System.Drawing.Point(64, 105);
            this.ctrl_down.Name = "ctrl_down";
            this.ctrl_down.Size = new System.Drawing.Size(42, 36);
            this.ctrl_down.TabIndex = 47;
            this.ctrl_down.Text = "↓";
            this.ctrl_down.UseVisualStyleBackColor = true;
            this.ctrl_down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrl_down_MouseDown);
            this.ctrl_down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrl_down_MouseUp);
            // 
            // ctrl_home
            // 
            this.ctrl_home.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl_home.Location = new System.Drawing.Point(64, 63);
            this.ctrl_home.Name = "ctrl_home";
            this.ctrl_home.Size = new System.Drawing.Size(42, 36);
            this.ctrl_home.TabIndex = 46;
            this.ctrl_home.Text = "H";
            this.ctrl_home.UseVisualStyleBackColor = true;
            // 
            // ctrl_up
            // 
            this.ctrl_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrl_up.Location = new System.Drawing.Point(64, 21);
            this.ctrl_up.Name = "ctrl_up";
            this.ctrl_up.Size = new System.Drawing.Size(42, 36);
            this.ctrl_up.TabIndex = 45;
            this.ctrl_up.Text = "↑";
            this.ctrl_up.UseVisualStyleBackColor = true;
            this.ctrl_up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrl_up_MouseDown);
            this.ctrl_up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrl_up_MouseUp);
            // 
            // tracking_btn
            // 
            this.tracking_btn.Location = new System.Drawing.Point(400, 530);
            this.tracking_btn.Name = "tracking_btn";
            this.tracking_btn.Size = new System.Drawing.Size(83, 32);
            this.tracking_btn.TabIndex = 46;
            this.tracking_btn.Text = "Start tracking";
            this.tracking_btn.UseVisualStyleBackColor = true;
            this.tracking_btn.Click += new System.EventHandler(this.tracking_btn_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(404, 510);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(79, 13);
            this.label24.TabIndex = 47;
            this.label24.Text = "Object tracking";
            // 
            // pictureBox_process
            // 
            this.pictureBox_process.BackColor = System.Drawing.SystemColors.InfoText;
            this.pictureBox_process.Location = new System.Drawing.Point(596, 97);
            this.pictureBox_process.Name = "pictureBox_process";
            this.pictureBox_process.Size = new System.Drawing.Size(575, 376);
            this.pictureBox_process.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_process.TabIndex = 48;
            this.pictureBox_process.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(28, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(47, 39);
            this.panel1.TabIndex = 49;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.detect_button);
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Location = new System.Drawing.Point(872, 522);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(296, 153);
            this.groupBox2.TabIndex = 50;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Detecting process";
            // 
            // detect_button
            // 
            this.detect_button.Location = new System.Drawing.Point(15, 86);
            this.detect_button.Name = "detect_button";
            this.detect_button.Size = new System.Drawing.Size(83, 32);
            this.detect_button.TabIndex = 51;
            this.detect_button.Text = "Detect";
            this.detect_button.UseVisualStyleBackColor = true;
            this.detect_button.Click += new System.EventHandler(this.detect_button_Click);
            // 
            // copy_pictureBox
            // 
            this.copy_pictureBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.copy_pictureBox.Location = new System.Drawing.Point(312, 576);
            this.copy_pictureBox.Name = "copy_pictureBox";
            this.copy_pictureBox.Size = new System.Drawing.Size(171, 104);
            this.copy_pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.copy_pictureBox.TabIndex = 51;
            this.copy_pictureBox.TabStop = false;
            // 
            // Preview
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1180, 687);
            this.Controls.Add(this.copy_pictureBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox_process);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.tracking_btn);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.PreSet);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnVioceTalk);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.textBoxChannel);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnJPEG);
            this.Controls.Add(this.btnBMP);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxUserName);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.RealPlayWnd);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Name = "Preview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Preview";
            this.Load += new System.EventHandler(this.Preview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_process)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.copy_pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		[STAThread]
		static void Main() 
		{
			Application.Run(new Preview());
		}

		private void textBox1_TextChanged(object sender, System.EventArgs e)
		{
		
		}

		private void btnLogin_Click(object sender, System.EventArgs e)
		{
			if (textBoxIP.Text == "" || textBoxPort.Text == "" ||
				textBoxUserName.Text == "" || textBoxPassword.Text == "")
			{
				MessageBox.Show("Please input IP, Port, User name and Password!");
				return;
			}
            if (m_lUserID < 0)
            {
                string DVRIPAddress = textBoxIP.Text;
                Int16 DVRPortNumber = Int16.Parse(textBoxPort.Text);
                string DVRUserName = textBoxUserName.Text;
                string DVRPassword = textBoxPassword.Text;

                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();

                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Login_V30 failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    MessageBox.Show("Login Success!");
                    btnLogin.Text = "Logout";
                }

            }
            else
            {
                if (m_lRealHandle >= 0)
                {
                    MessageBox.Show("Please stop live view firstly");
                    return;
                }

                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Logout failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;           
                }
                m_lUserID = -1;
                btnLogin.Text = "Login";
            }
            return;
		}

		private void btnPreview_Click(object sender, System.EventArgs e)
		{
            if(m_lUserID < 0)
            {
                MessageBox.Show("Please login the device firstly");
                return;
            }

            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle;
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);
                lpPreviewInfo.dwStreamType = 0;
                lpPreviewInfo.dwLinkMode = 0;
                lpPreviewInfo.bBlocked = true;
                lpPreviewInfo.dwDisplayBufNum = 1;
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;


                if (textBoxID.Text != "")
                {
                    lpPreviewInfo.lChannel = -1;
                    byte[] byStreamID = System.Text.Encoding.Default.GetBytes(textBoxID.Text);
                    lpPreviewInfo.byStreamID = new byte[32];
                    byStreamID.CopyTo(lpPreviewInfo.byStreamID, 0);
                }


                if (RealData == null)
                {
                    RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);
                }
                
                IntPtr pUser = new IntPtr();

                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    btnPreview.Text = "Stop Live View";
                }
            }
            else
            {
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopRealPlay failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                m_lRealHandle = -1;
                btnPreview.Text = "Live View";

            }
            return;
		}

        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
		{
            if (dwBufSize > 0)
            {
                byte[] sData = new byte[dwBufSize];
                Marshal.Copy(pBuffer, sData, 0, (Int32)dwBufSize);

                string str = "ÊµÊ±Á÷Êý¾Ý.ps";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sData, 0, iLen);
                fs.Close();            
            }
		}

        private void btnBMP_Click(object sender, EventArgs e)
        {
            string sBmpPicFileName;
            //Í¼Æ¬±£´æÂ·¾¶ºÍÎÄ¼þÃû the path and file name to save
            sBmpPicFileName = "BMP_test.bmp"; 

            //BMP×¥Í¼ Capture a BMP picture
            if (!CHCNetSDK.NET_DVR_CapturePicture(m_lRealHandle, sBmpPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_CapturePicture failed, error code= " + iLastErr;
                MessageBox.Show(str);
                return;
            }
            else
            {
                str = "Successful to capture the BMP file and the saved file is " + sBmpPicFileName;
                MessageBox.Show(str); 
            }
            return;
        }

        private void btnJPEG_Click(object sender, EventArgs e)
        {
            string sJpegPicFileName;
            sJpegPicFileName = "JPEG_test.jpg";

            int lChannel = Int16.Parse(textBoxChannel.Text);
            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0;
            lpJpegPara.wPicSize = 0xff;

            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_CaptureJPEGPicture failed, error code= " + iLastErr;
                MessageBox.Show(str);
                return;
            }
            else
            {
                str = "Successful to capture the JPEG file and the saved file is " + sJpegPicFileName;
                MessageBox.Show(str);
            }
            return;
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            //Â¼Ïñ±£´æÂ·¾¶ºÍÎÄ¼þÃû the path and file name to save
            string sVideoFileName;
            sVideoFileName = "Record_test.mp4";

            if (m_bRecord == false)
            {
                //Ç¿ÖÆIÖ¡ Make a I frame
                int lChannel = Int16.Parse(textBoxChannel.Text); //Í¨µÀºÅ Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, lChannel);

                //¿ªÊ¼Â¼Ïñ Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_SaveRealData failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {                  
                    btnRecord.Text = "Stop Record";
                    m_bRecord = true;
                }
            }
            else
            {
                //Í£Ö¹Â¼Ïñ Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopSaveRealData failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    str = "Successful to stop recording and the saved file is " + sVideoFileName;
                    MessageBox.Show(str);
                    btnRecord.Text = "Start Record";
                    m_bRecord = false;
                }            
            }

            return;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            //Í£Ö¹Ô¤ÀÀ Stop live view 
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
                m_lRealHandle = -1;
            }

            //×¢ÏúµÇÂ¼ Logout the device
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
                m_lUserID = -1;
            }

            CHCNetSDK.NET_DVR_Cleanup();

            Application.Exit();
        }

        private void btnPTZ_Click(object sender, EventArgs e)
        {

        }

        public void VoiceDataCallBack(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser)
        {
            byte[] sString = new byte[dwBufSize];
            Marshal.Copy(pRecvDataBuffer, sString, 0, (Int32)dwBufSize);

            if (byAudioFlag ==0)
            {
                //½«»º³åÇøÀïµÄÒôÆµÊý¾ÝÐ´ÈëÎÄ¼þ save the data into a file
                string str = "PC²É¼¯ÒôÆµÎÄ¼þ.pcm";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sString, 0, iLen);
                fs.Close();
            }
            if (byAudioFlag == 1)
            {
                //½«»º³åÇøÀïµÄÒôÆµÊý¾ÝÐ´ÈëÎÄ¼þ save the data into a file
                string str = "Éè±¸ÒôÆµÎÄ¼þ.pcm";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sString, 0, iLen);
                fs.Close();
            }

        }

        private void btnVioceTalk_Click(object sender, EventArgs e)
        {
            if (m_bTalk == false)
            {
                //¿ªÊ¼ÓïÒô¶Ô½² Start two-way talk
                CHCNetSDK.VOICEDATACALLBACKV30 VoiceData = new CHCNetSDK.VOICEDATACALLBACKV30(VoiceDataCallBack);//Ô¤ÀÀÊµÊ±Á÷»Øµ÷º¯Êý

                lVoiceComHandle = CHCNetSDK.NET_DVR_StartVoiceCom_V30(m_lUserID, 1, true, VoiceData, IntPtr.Zero);
                //bNeedCBNoEncData [in]ÐèÒª»Øµ÷µÄÓïÒôÊý¾ÝÀàÐÍ£º0- ±àÂëºóµÄÓïÒôÊý¾Ý£¬1- ±àÂëÇ°µÄPCMÔ­Ê¼Êý¾Ý

                if (lVoiceComHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StartVoiceCom_V30 failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    btnVioceTalk.Text = "Stop Talk";
                    m_bTalk = true;
                }
            }
            else
            {
                //Í£Ö¹ÓïÒô¶Ô½² Stop two-way talk
                if (!CHCNetSDK.NET_DVR_StopVoiceCom(lVoiceComHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopVoiceCom failed, error code= " + iLastErr;
                    MessageBox.Show(str);
                    return;
                }
                else
                {
                    btnVioceTalk.Text = "Start Talk";
                    m_bTalk = false;
                }
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void Preview_Load(object sender, EventArgs e)
        {
            cam_center_x = pictureBox_process.Width / 2;
            cam_center_y = pictureBox_process.Height / 2;
            timer = new Timer();
            timer.Tick += Timer_Tick;
            timer.Interval = 30;
        }

        private void Ptz_Set_Click(object sender, EventArgs e)
        {

        }

        private void PreSet_Click(object sender, EventArgs e)
        {
            PreSet dlg = new PreSet();
            dlg.m_lUserID = m_lUserID;
            dlg.m_lChannel = 1;
            dlg.m_lRealHandle = m_lRealHandle;
            dlg.ShowDialog();
            
        }

        private void textBoxIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void ctrl_up_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 0, (uint)trackBar1.Value + 1);
        }

        private void ctrl_up_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 1, (uint)trackBar1.Value + 1);
        }

        private void ctrl_down_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 0, (uint)trackBar1.Value + 1);
        }

        private void ctrl_down_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 1, (uint)trackBar1.Value + 1);
        }

        private void ctrl_right_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 0, (uint)trackBar1.Value + 1);
        }

        private void ctrl_right_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 1, (uint)trackBar1.Value + 1);
        }

        private void ctrl_left_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 0, (uint)trackBar1.Value + 1);
        }

        private void ctrl_left_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 1, (uint)trackBar1.Value + 1);
        }

        private void zoom_min_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.ZOOM_OUT, 0, (uint)trackBar1.Value + 1);
        }

        private void zoom_min_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.ZOOM_OUT, 1, (uint)trackBar1.Value + 1);
        }

        private void zoom_max_MouseDown(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.ZOOM_IN, 0, (uint)trackBar1.Value + 1);
        }

        private void zoom_max_MouseUp(object sender, MouseEventArgs e)
        {
            CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.ZOOM_IN, 1, (uint)trackBar1.Value + 1);
        }

        private bool tracking_btn_state = false;
        private Color rgb;
        private Timer timer;
        private void tracking_btn_Click(object sender, EventArgs e)
        {
            tracking_btn_state = !tracking_btn_state;
            if (tracking_btn_state)
            {
                tracking_btn.Text = "Stop tracking";
                timer.Start();
            } else
            {
                tracking_btn.Text = "Start tracking";
                timer.Stop();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            trackSetColor();
        }

        void trackSetColor()
        {
            string rgb_hex = "#0000FF";
            rgb = HexToRgb(rgb_hex);
            panel1.BackColor = rgb;
            Bitmap bitmap = CaptureControl(RealPlayWnd.Handle, RealPlayWnd.Width, RealPlayWnd.Height);
            copy_pictureBox.Image = bitmap;
            pictureBox_process.Image = RgbFilter(new Image<Bgr, byte>(bitmap), rgb, 100, 12 * 2 + 1, true).ToBitmap();
            drawRect();
            objectTracking();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lbl_speed.Text = trackBar1.Value.ToString();
        }
        private Color HexToRgb(string hextext)
        {
            if (hextext.IndexOf("#") == 0)
            {
                hextext = hextext.Replace("#", "");
            }
            int r = 0, g = 0, b = 0;
            r = int.Parse(hextext.Substring(0, 2), NumberStyles.HexNumber);
            g = int.Parse(hextext.Substring(2, 2), NumberStyles.HexNumber);
            b = int.Parse(hextext.Substring(4, 2), NumberStyles.HexNumber);
            return Color.FromArgb(r, g, b);
        }

        public int x_max = 0, x_min = 10000, y_max = 0, y_min = 10000, center_object_x = -1, center_object_y = -1;

        private void detect_button_Click(object sender, EventArgs e)
        {
            trackSetColor();
        }
        private int cam_center_x, cam_center_y;
        public void drawRect()
        {
            x_max = 0;
            y_max = 0;
            x_min = 10000;
            y_min = 10000;
            Image<Bgr, byte> img = new Image<Bgr, byte>(new Bitmap(pictureBox_process.Image));
            Image<Gray, byte> grayImg = img.Convert<Gray, byte>();
            Image<Gray, byte> treshImg = grayImg.ThresholdBinary(new Gray(50), new Gray(255));
            Mat heir = new Mat();
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(treshImg, contours, heir, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            Point[][] points = contours.ToArrayOfArray();
            foreach (Point[] p in points)
            {
                foreach (Point pnt in p)
                {
                    x_max = Math.Max(x_max, pnt.X);
                    x_min = Math.Min(x_min, pnt.X);
                    y_max = Math.Max(y_max, pnt.Y);
                    y_min = Math.Min(y_min, pnt.Y);
                }
            }
            center_object_x = (x_max + x_min) / 2;
            center_object_y = (y_max + y_min) / 2;
            if (center_object_x < 0) center_object_x = -1;
            if (center_object_y < 0) center_object_y = -1;
            if (center_object_x < pictureBox_process.Width && center_object_y < pictureBox_process.Height)
            {
                object_detected = true;
                //MessageBox.Show(x_max.ToString() + "\n" + x_min.ToString() + "\n" + y_max.ToString() + "\n" + y_min.ToString());
                //MessageBox.Show(center_object_x.ToString() + "\n" + center_object_y.ToString());
                Rectangle rct = new Rectangle(new Point(x_min, y_min), new Size(x_max - x_min, y_max - y_min));
                Rectangle rct1 = new Rectangle(new Point(center_object_x - 20, center_object_y - 20), new Size(40, 40));
                using (Pen pin = new Pen(Color.Red, 2))
                {
                    img = new Image<Bgr, byte>(new Bitmap(pictureBox_process.Image));
                    img.Draw(rct1, new Bgr(0, 0, 255), 2);
                    using (var grapics = Graphics.FromImage(img.Bitmap))
                    {
                        grapics.DrawLine(new Pen(Color.Green, 1), new Point(0, pictureBox_process.Height / 2), new Point(pictureBox_process.Width, pictureBox_process.Height / 2));
                        grapics.DrawLine(new Pen(Color.Green, 1), new Point(pictureBox_process.Width / 2, 0), new Point(pictureBox_process.Width / 2, pictureBox_process.Height));
                    }
                    pictureBox_process.Image = img.Bitmap;

                }
            } else
            {
                object_detected = false;
            }
            
        }

        public Image<Bgr, byte> RgbFilter(Image<Bgr, byte> img, Color rgb_color, int err, int krnl_size, bool not)
        {
            try
            {
                img._SmoothGaussian(krnl_size);
                int r = rgb_color.R;
                int g = rgb_color.G;
                int b = rgb_color.B;
                Bgr lower = new Bgr(b - err, g - err, r - err);
                Bgr higher = new Bgr(b + err, g + err, r + err);
                var mask = img.InRange(lower, higher);
                if (not)
                {
                    mask = mask.Not();
                }
                img.SetValue(new Bgr(0, 0, 0), mask);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
            return img;
        }

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);
        public Bitmap CaptureControl(IntPtr handle, int width, int height)
        {
            Bitmap controlBmp;
            using (Graphics g1 = Graphics.FromHwnd(handle))
            {
                controlBmp = new Bitmap(width, height, g1);
                using (Graphics g2 = Graphics.FromImage(controlBmp))
                {
                    g2.CopyFromScreen(this.Location.X + RealPlayWnd.Left, this.Location.Y + RealPlayWnd.Top + 30, 0, 0, RealPlayWnd.Size);

                    IntPtr dc1 = g1.GetHdc();
                    IntPtr dc2 = g2.GetHdc();

                    BitBlt(dc2, 0, 0, width, height, handle, 0, 0, 13369376);
                    g1.ReleaseHdc(dc1);
                    g2.ReleaseHdc(dc2);
                }
            }

            return controlBmp;
        }
        private bool object_detected = false;
        private void objectTracking ()
        {
            if (object_detected == true)
            {
                if (Math.Abs(cam_center_x - center_object_x) < 20)
                {
                    if (cam_center_y > center_object_y)
                    {
                        CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 0, 255);
                    }
                    else if (cam_center_y < center_object_y)
                    {
                        CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 0, 255);
                    }
                    else
                    {
                        CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 1, 0);
                        CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 1, 0);
                    }
                } else
                {
                    if (cam_center_x > center_object_x)
                    {
                        CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 0, 255);
                    }
                    else if (cam_center_x < center_object_x)
                    {
                        CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 0, 255);
                    }
                    else
                    {
                        CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 1, 0);
                        CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 1, 0);
                    }
                }
            } else
            {
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_RIGHT, 1, 0);
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.PAN_LEFT, 1, 0);
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_UP, 1, 0);
                CHCNetSDK.NET_DVR_PTZControlWithSpeed(m_lRealHandle, CHCNetSDK.TILT_DOWN, 1, 0);
            }
        }
    }
}
