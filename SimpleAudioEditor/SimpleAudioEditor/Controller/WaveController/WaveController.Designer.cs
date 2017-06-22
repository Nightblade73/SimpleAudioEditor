namespace SimpleAudioEditor.Controller.WaveController {
    partial class WaveEditor {
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
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveEditor));
            this.hrScroll = new System.Windows.Forms.HScrollBar();
            this.cursorTimer = new System.Windows.Forms.Timer(this.components);
            this.playTimer = new System.Windows.Forms.Timer(this.components);
            this.status = new System.Windows.Forms.StatusStrip();
            this.btnPlay = new System.Windows.Forms.ToolStripSplitButton();
            this.btnStop = new System.Windows.Forms.ToolStripSplitButton();
            this.lblSamplesPerPixel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCursorPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectStartPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSep1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectEndPos = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSep2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSelectLength = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.bCut = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // hrScroll
            // 
            this.hrScroll.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hrScroll.Enabled = false;
            this.hrScroll.Location = new System.Drawing.Point(0, 500);
            this.hrScroll.Maximum = 10;
            this.hrScroll.Name = "hrScroll";
            this.hrScroll.Size = new System.Drawing.Size(675, 17);
            this.hrScroll.TabIndex = 1;
            // 
            // cursorTimer
            // 
            this.cursorTimer.Interval = 700;
            this.cursorTimer.Tick += new System.EventHandler(this.cursorTimer_Tick);
            // 
            // playTimer
            // 
            this.playTimer.Interval = 50;
            this.playTimer.Tick += new System.EventHandler(this.playTimer_Tick);
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPlay,
            this.btnStop,
            this.lblSamplesPerPixel,
            this.lblCursorPos,
            this.lblSelectStartPos,
            this.lblSep1,
            this.lblSelectEndPos,
            this.lblSep2,
            this.lblSelectLength,
            this.progress});
            this.status.Location = new System.Drawing.Point(0, 476);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(675, 24);
            this.status.SizingGrip = false;
            this.status.TabIndex = 2;
            this.status.Text = "StatusStrip1";
            // 
            // btnPlay
            // 
            this.btnPlay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlay.DropDownButtonWidth = 0;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(21, 22);
            this.btnPlay.Text = "toolStripSplitButton1";
            this.btnPlay.ToolTipText = "Play";
            this.btnPlay.ButtonClick += new System.EventHandler(this.btnPlay_ButtonClick);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.DropDownButtonWidth = 0;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(21, 22);
            this.btnStop.Text = "toolStripSplitButton1";
            this.btnStop.ToolTipText = "Stop";
            this.btnStop.ButtonClick += new System.EventHandler(this.btnStop_ButtonClick);
            // 
            // lblSamplesPerPixel
            // 
            this.lblSamplesPerPixel.BackColor = System.Drawing.SystemColors.Control;
            this.lblSamplesPerPixel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblSamplesPerPixel.Name = "lblSamplesPerPixel";
            this.lblSamplesPerPixel.Size = new System.Drawing.Size(26, 19);
            this.lblSamplesPerPixel.Text = "1:1";
            // 
            // lblCursorPos
            // 
            this.lblCursorPos.BackColor = System.Drawing.SystemColors.Control;
            this.lblCursorPos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblCursorPos.Name = "lblCursorPos";
            this.lblCursorPos.Size = new System.Drawing.Size(68, 19);
            this.lblCursorPos.Text = "00:00:00.00";
            // 
            // lblSelectStartPos
            // 
            this.lblSelectStartPos.BackColor = System.Drawing.SystemColors.Control;
            this.lblSelectStartPos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblSelectStartPos.Name = "lblSelectStartPos";
            this.lblSelectStartPos.Size = new System.Drawing.Size(68, 19);
            this.lblSelectStartPos.Text = "00:00:00.00";
            // 
            // lblSep1
            // 
            this.lblSep1.BackColor = System.Drawing.SystemColors.Control;
            this.lblSep1.Name = "lblSep1";
            this.lblSep1.Size = new System.Drawing.Size(12, 19);
            this.lblSep1.Text = "-";
            // 
            // lblSelectEndPos
            // 
            this.lblSelectEndPos.BackColor = System.Drawing.SystemColors.Control;
            this.lblSelectEndPos.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblSelectEndPos.Name = "lblSelectEndPos";
            this.lblSelectEndPos.Size = new System.Drawing.Size(68, 19);
            this.lblSelectEndPos.Text = "00:00:00.00";
            // 
            // lblSep2
            // 
            this.lblSep2.BackColor = System.Drawing.SystemColors.Control;
            this.lblSep2.Name = "lblSep2";
            this.lblSep2.Size = new System.Drawing.Size(15, 19);
            this.lblSep2.Text = "=";
            // 
            // lblSelectLength
            // 
            this.lblSelectLength.BackColor = System.Drawing.SystemColors.Control;
            this.lblSelectLength.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblSelectLength.Name = "lblSelectLength";
            this.lblSelectLength.Size = new System.Drawing.Size(68, 19);
            this.lblSelectLength.Text = "00:00:00.00";
            // 
            // progress
            // 
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(100, 18);
            this.progress.Visible = false;
            // 
            // bCut
            // 
            this.bCut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCut.Location = new System.Drawing.Point(516, 481);
            this.bCut.Name = "bCut";
            this.bCut.Size = new System.Drawing.Size(75, 20);
            this.bCut.TabIndex = 3;
            this.bCut.Text = "Добавить";
            this.bCut.UseVisualStyleBackColor = true;
            this.bCut.Click += new System.EventHandler(this.bCut_Click);
            // 
            // bDelete
            // 
            this.bDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDelete.Location = new System.Drawing.Point(597, 481);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(75, 20);
            this.bDelete.TabIndex = 4;
            this.bDelete.Text = "Удалить";
            this.bDelete.UseVisualStyleBackColor = true;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Coral;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(3, 454);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 5;
            // 
            // WaveEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bCut);
            this.Controls.Add(this.status);
            this.Controls.Add(this.hrScroll);
            this.MinimumSize = new System.Drawing.Size(675, 0);
            this.Name = "WaveEditor";
            this.Size = new System.Drawing.Size(675, 517);
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.HScrollBar hrScroll;
        internal System.Windows.Forms.Timer cursorTimer;
        internal System.Windows.Forms.Timer playTimer;
        internal System.Windows.Forms.StatusStrip status;
        internal System.Windows.Forms.ToolStripStatusLabel lblSamplesPerPixel;
        internal System.Windows.Forms.ToolStripStatusLabel lblCursorPos;
        internal System.Windows.Forms.ToolStripStatusLabel lblSelectStartPos;
        internal System.Windows.Forms.ToolStripStatusLabel lblSep1;
        internal System.Windows.Forms.ToolStripStatusLabel lblSelectEndPos;
        internal System.Windows.Forms.ToolStripStatusLabel lblSep2;
        internal System.Windows.Forms.ToolStripStatusLabel lblSelectLength;
        internal System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.ToolStripSplitButton btnPlay;
        private System.Windows.Forms.ToolStripSplitButton btnStop;
        private System.Windows.Forms.Button bCut;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.Label label1;
    }
}
