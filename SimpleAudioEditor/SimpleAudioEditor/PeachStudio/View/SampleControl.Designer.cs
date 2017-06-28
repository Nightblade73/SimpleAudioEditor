namespace SimpleAudioEditor.PeachStudio {
    partial class SampleControl {
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
            this.maskedTextBoxSplitEndTime = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxSplitStartTime = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxCurrentTime = new System.Windows.Forms.MaskedTextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPlay = new System.Windows.Forms.Button();
            this.maskedTextBox5 = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxResultTime = new System.Windows.Forms.MaskedTextBox();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.FromBeginingToPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FromPointToEndingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // maskedTextBoxSplitEndTime
            // 
            this.maskedTextBoxSplitEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxSplitEndTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxSplitEndTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxSplitEndTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxSplitEndTime.Location = new System.Drawing.Point(70, 80);
            this.maskedTextBoxSplitEndTime.Mask = "00:00:00.00";
            this.maskedTextBoxSplitEndTime.Name = "maskedTextBoxSplitEndTime";
            this.maskedTextBoxSplitEndTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxSplitEndTime.TabIndex = 16;
            // 
            // maskedTextBoxSplitStartTime
            // 
            this.maskedTextBoxSplitStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxSplitStartTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxSplitStartTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxSplitStartTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxSplitStartTime.Location = new System.Drawing.Point(145, 80);
            this.maskedTextBoxSplitStartTime.Mask = "00:00:00.00";
            this.maskedTextBoxSplitStartTime.Name = "maskedTextBoxSplitStartTime";
            this.maskedTextBoxSplitStartTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxSplitStartTime.TabIndex = 15;
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBox2.BackColor = System.Drawing.Color.Black;
            this.maskedTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBox2.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBox2.Location = new System.Drawing.Point(215, 80);
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(5, 13);
            this.maskedTextBox2.TabIndex = 14;
            this.maskedTextBox2.Text = "=";
            this.maskedTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // maskedTextBoxCurrentTime
            // 
            this.maskedTextBoxCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxCurrentTime.BackColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxCurrentTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxCurrentTime.Location = new System.Drawing.Point(0, 80);
            this.maskedTextBoxCurrentTime.Mask = "00:00:00.00";
            this.maskedTextBoxCurrentTime.Name = "maskedTextBoxCurrentTime";
            this.maskedTextBoxCurrentTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxCurrentTime.TabIndex = 13;
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStop.BackColor = System.Drawing.Color.DarkOrange;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Location = new System.Drawing.Point(0, 40);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(40, 40);
            this.buttonStop.TabIndex = 11;
            this.buttonStop.Text = "■";
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPlay
            // 
            this.buttonPlay.BackColor = System.Drawing.Color.DarkOrange;
            this.buttonPlay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(204)));
            this.buttonPlay.Location = new System.Drawing.Point(0, 0);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(40, 40);
            this.buttonPlay.TabIndex = 10;
            this.buttonPlay.Text = ">";
            this.buttonPlay.UseVisualStyleBackColor = false;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // maskedTextBox5
            // 
            this.maskedTextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBox5.BackColor = System.Drawing.Color.Black;
            this.maskedTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBox5.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBox5.Location = new System.Drawing.Point(140, 80);
            this.maskedTextBox5.Name = "maskedTextBox5";
            this.maskedTextBox5.Size = new System.Drawing.Size(5, 13);
            this.maskedTextBox5.TabIndex = 17;
            this.maskedTextBox5.Text = "-";
            this.maskedTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // maskedTextBoxResultTime
            // 
            this.maskedTextBoxResultTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxResultTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxResultTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxResultTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxResultTime.Location = new System.Drawing.Point(220, 80);
            this.maskedTextBoxResultTime.Mask = "00:00:00.00";
            this.maskedTextBoxResultTime.Name = "maskedTextBoxResultTime";
            this.maskedTextBoxResultTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxResultTime.TabIndex = 18;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.LargeChange = 100;
            this.hScrollBar.Location = new System.Drawing.Point(290, 80);
            this.hScrollBar.Minimum = 1;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(390, 13);
            this.hScrollBar.SmallChange = 100;
            this.hScrollBar.TabIndex = 19;
            this.hScrollBar.Value = 1;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FromBeginingToPointToolStripMenuItem,
            this.FromPointToEndingToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(231, 48);
            // 
            // FromBeginingToPointToolStripMenuItem
            // 
            this.FromBeginingToPointToolStripMenuItem.Name = "FromBeginingToPointToolStripMenuItem";
            this.FromBeginingToPointToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.FromBeginingToPointToolStripMenuItem.Text = "От начала до точки нажатия";
            this.FromBeginingToPointToolStripMenuItem.Click += new System.EventHandler(this.FromBeginingToPointToolStripMenuItem_Click);
            // 
            // FromPointToEndingToolStripMenuItem
            // 
            this.FromPointToEndingToolStripMenuItem.Name = "FromPointToEndingToolStripMenuItem";
            this.FromPointToEndingToolStripMenuItem.Size = new System.Drawing.Size(230, 22);
            this.FromPointToEndingToolStripMenuItem.Text = "От точки нажатия до конца";
            this.FromPointToEndingToolStripMenuItem.Click += new System.EventHandler(this.FromPointToEndingToolStripMenuItem_Click);
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.Location = new System.Drawing.Point(40, 0);
            this.pictureBox.MinimumSize = new System.Drawing.Size(640, 80);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(640, 80);
            this.pictureBox.TabIndex = 12;
            this.pictureBox.TabStop = false;
            this.pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.pictureBox.Layout += new System.Windows.Forms.LayoutEventHandler(this.pictureBox_Layout);
            this.pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseClick);
            this.pictureBox.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            // 
            // SampleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPlay);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.maskedTextBoxResultTime);
            this.Controls.Add(this.maskedTextBox5);
            this.Controls.Add(this.maskedTextBoxSplitEndTime);
            this.Controls.Add(this.maskedTextBoxSplitStartTime);
            this.Controls.Add(this.maskedTextBox2);
            this.Controls.Add(this.maskedTextBoxCurrentTime);
            this.Controls.Add(this.pictureBox);
            this.MinimumSize = new System.Drawing.Size(680, 93);
            this.Name = "SampleControl";
            this.Size = new System.Drawing.Size(680, 93);
            this.Load += new System.EventHandler(this.SampleControl_Load);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox maskedTextBoxSplitEndTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSplitStartTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxCurrentTime;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.MaskedTextBox maskedTextBox5;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxResultTime;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem FromBeginingToPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FromPointToEndingToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}
