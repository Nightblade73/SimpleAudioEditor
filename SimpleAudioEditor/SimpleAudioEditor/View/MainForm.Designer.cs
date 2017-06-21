namespace SimpleAudioEditor {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waveformPainter2 = new NAudio.Gui.WaveformPainter();
            this.volumeMeter2 = new NAudio.Gui.VolumeMeter();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.trackBarPosition = new System.Windows.Forms.TrackBar();
            this.labelTotalTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.waveformPainter1 = new NAudio.Gui.WaveformPainter();
            this.volumeMeter1 = new NAudio.Gui.VolumeMeter();
            this.labelCurrentTime = new System.Windows.Forms.ToolStripLabel();
            this.buttonPause = new System.Windows.Forms.ToolStripButton();
            this.buttonPlay = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpenFile = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.buttonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.panelOutputDeviceSettings = new System.Windows.Forms.Panel();
            this.comboBoxOutputDevice = new System.Windows.Forms.ComboBox();
            this.groupBoxDriverModel = new System.Windows.Forms.GroupBox();
            this.comboBoxLatency = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBoxDriverModel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "fileDialogOpenSound";
            this.openFileDialog.Multiselect = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(669, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // waveformPainter2
            // 
            this.waveformPainter2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.waveformPainter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.waveformPainter2.ForeColor = System.Drawing.Color.SaddleBrown;
            this.waveformPainter2.Location = new System.Drawing.Point(275, 229);
            this.waveformPainter2.Name = "waveformPainter2";
            this.waveformPainter2.Size = new System.Drawing.Size(300, 60);
            this.waveformPainter2.TabIndex = 30;
            this.waveformPainter2.Text = "waveformPainter1";
            // 
            // volumeMeter2
            // 
            this.volumeMeter2.Amplitude = 0F;
            this.volumeMeter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeMeter2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.volumeMeter2.Location = new System.Drawing.Point(561, 54);
            this.volumeMeter2.MaxDb = 3F;
            this.volumeMeter2.MinDb = -60F;
            this.volumeMeter2.Name = "volumeMeter2";
            this.volumeMeter2.Size = new System.Drawing.Size(14, 102);
            this.volumeMeter2.TabIndex = 28;
            this.volumeMeter2.Text = "volumeMeter1";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(327, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Volume:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(327, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 27;
            this.label2.Text = "Requested Latency:";
            // 
            // volumeSlider1
            // 
            this.volumeSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeSlider1.Location = new System.Drawing.Point(436, 78);
            this.volumeSlider1.Name = "volumeSlider1";
            this.volumeSlider1.Size = new System.Drawing.Size(96, 16);
            this.volumeSlider1.TabIndex = 21;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            // 
            // trackBarPosition
            // 
            this.trackBarPosition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarPosition.LargeChange = 10;
            this.trackBarPosition.Location = new System.Drawing.Point(12, 348);
            this.trackBarPosition.Maximum = 100;
            this.trackBarPosition.Name = "trackBarPosition";
            this.trackBarPosition.Size = new System.Drawing.Size(569, 45);
            this.trackBarPosition.TabIndex = 25;
            this.trackBarPosition.TickFrequency = 5;
            // 
            // labelTotalTime
            // 
            this.labelTotalTime.Name = "labelTotalTime";
            this.labelTotalTime.Size = new System.Drawing.Size(34, 22);
            this.labelTotalTime.Text = "00:00";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(66, 22);
            this.toolStripLabel3.Text = "Total Time:";
            // 
            // waveformPainter1
            // 
            this.waveformPainter1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.waveformPainter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.waveformPainter1.ForeColor = System.Drawing.Color.SaddleBrown;
            this.waveformPainter1.Location = new System.Drawing.Point(275, 165);
            this.waveformPainter1.Name = "waveformPainter1";
            this.waveformPainter1.Size = new System.Drawing.Size(300, 60);
            this.waveformPainter1.TabIndex = 31;
            this.waveformPainter1.Text = "waveformPainter1";
            // 
            // volumeMeter1
            // 
            this.volumeMeter1.Amplitude = 0F;
            this.volumeMeter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.volumeMeter1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.volumeMeter1.Location = new System.Drawing.Point(543, 54);
            this.volumeMeter1.MaxDb = 3F;
            this.volumeMeter1.MinDb = -60F;
            this.volumeMeter1.Name = "volumeMeter1";
            this.volumeMeter1.Size = new System.Drawing.Size(14, 102);
            this.volumeMeter1.TabIndex = 29;
            this.volumeMeter1.Text = "volumeMeter1";
            // 
            // labelCurrentTime
            // 
            this.labelCurrentTime.Name = "labelCurrentTime";
            this.labelCurrentTime.Size = new System.Drawing.Size(34, 22);
            this.labelCurrentTime.Text = "00:00";
            // 
            // buttonPause
            // 
            this.buttonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(23, 22);
            this.buttonPause.Text = "Pause";
            // 
            // buttonPlay
            // 
            this.buttonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(23, 22);
            this.buttonPlay.Text = "Play";
            // 
            // toolStripButtonOpenFile
            // 
            this.toolStripButtonOpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenFile.Name = "toolStripButtonOpenFile";
            this.toolStripButtonOpenFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpenFile.Text = "Open File";
            this.toolStripButtonOpenFile.Click += new System.EventHandler(this.toolStripButtonOpenFile_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(517, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "ms";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenFile,
            this.buttonPlay,
            this.buttonPause,
            this.buttonStop,
            this.toolStripLabel1,
            this.labelCurrentTime,
            this.toolStripLabel3,
            this.labelTotalTime});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(669, 25);
            this.toolStrip1.TabIndex = 24;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // buttonStop
            // 
            this.buttonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(23, 22);
            this.buttonStop.Text = "Stop";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(80, 22);
            this.toolStripLabel1.Text = "Current Time:";
            // 
            // panelOutputDeviceSettings
            // 
            this.panelOutputDeviceSettings.Location = new System.Drawing.Point(7, 52);
            this.panelOutputDeviceSettings.Name = "panelOutputDeviceSettings";
            this.panelOutputDeviceSettings.Size = new System.Drawing.Size(250, 236);
            this.panelOutputDeviceSettings.TabIndex = 1;
            // 
            // comboBoxOutputDevice
            // 
            this.comboBoxOutputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOutputDevice.FormattingEnabled = true;
            this.comboBoxOutputDevice.Location = new System.Drawing.Point(6, 24);
            this.comboBoxOutputDevice.Name = "comboBoxOutputDevice";
            this.comboBoxOutputDevice.Size = new System.Drawing.Size(251, 21);
            this.comboBoxOutputDevice.TabIndex = 0;
            // 
            // groupBoxDriverModel
            // 
            this.groupBoxDriverModel.Controls.Add(this.panelOutputDeviceSettings);
            this.groupBoxDriverModel.Controls.Add(this.comboBoxOutputDevice);
            this.groupBoxDriverModel.Location = new System.Drawing.Point(6, 54);
            this.groupBoxDriverModel.Name = "groupBoxDriverModel";
            this.groupBoxDriverModel.Size = new System.Drawing.Size(263, 288);
            this.groupBoxDriverModel.TabIndex = 23;
            this.groupBoxDriverModel.TabStop = false;
            this.groupBoxDriverModel.Text = "Output Driver";
            // 
            // comboBoxLatency
            // 
            this.comboBoxLatency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxLatency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLatency.FormattingEnabled = true;
            this.comboBoxLatency.Location = new System.Drawing.Point(436, 51);
            this.comboBoxLatency.Name = "comboBoxLatency";
            this.comboBoxLatency.Size = new System.Drawing.Size(75, 21);
            this.comboBoxLatency.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(493, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 26);
            this.button1.TabIndex = 32;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 435);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.waveformPainter2);
            this.Controls.Add(this.volumeMeter2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.volumeSlider1);
            this.Controls.Add(this.trackBarPosition);
            this.Controls.Add(this.waveformPainter1);
            this.Controls.Add(this.volumeMeter1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBoxDriverModel);
            this.Controls.Add(this.comboBoxLatency);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPosition)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxDriverModel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private NAudio.Gui.WaveformPainter waveformPainter2;
        private NAudio.Gui.VolumeMeter volumeMeter2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private NAudio.Gui.VolumeSlider volumeSlider1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar trackBarPosition;
        private System.Windows.Forms.ToolStripLabel labelTotalTime;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private NAudio.Gui.WaveformPainter waveformPainter1;
        private NAudio.Gui.VolumeMeter volumeMeter1;
        private System.Windows.Forms.ToolStripLabel labelCurrentTime;
        private System.Windows.Forms.ToolStripButton buttonPause;
        private System.Windows.Forms.ToolStripButton buttonPlay;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton buttonStop;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.Panel panelOutputDeviceSettings;
        private System.Windows.Forms.ComboBox comboBoxOutputDevice;
        private System.Windows.Forms.GroupBox groupBoxDriverModel;
        private System.Windows.Forms.ComboBox comboBoxLatency;
        private System.Windows.Forms.Button button1;
    }
}

