namespace SimpleAudioEditor.PeachStudio {
    partial class ProjectControl {
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
            this.pbWaveViewer = new System.Windows.Forms.PictureBox();
            this.bPlayPause = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.bDelete = new System.Windows.Forms.Button();
            this.maskedTextBoxCurrentTime = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxSplitEndTime = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxSplitStartTime = new System.Windows.Forms.MaskedTextBox();
            this.maskedTextBoxResultTime = new System.Windows.Forms.MaskedTextBox();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // pbWaveViewer
            // 
            this.pbWaveViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbWaveViewer.BackColor = System.Drawing.Color.Transparent;
            this.pbWaveViewer.Location = new System.Drawing.Point(50, 0);
            this.pbWaveViewer.Name = "pbWaveViewer";
            this.pbWaveViewer.Size = new System.Drawing.Size(300, 100);
            this.pbWaveViewer.TabIndex = 0;
            this.pbWaveViewer.TabStop = false;
            // 
            // bPlayPause
            // 
            this.bPlayPause.BackColor = System.Drawing.Color.DarkOrange;
            this.bPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPlayPause.Location = new System.Drawing.Point(0, 0);
            this.bPlayPause.Name = "bPlayPause";
            this.bPlayPause.Size = new System.Drawing.Size(50, 50);
            this.bPlayPause.TabIndex = 1;
            this.bPlayPause.Text = ">";
            this.bPlayPause.UseVisualStyleBackColor = false;
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStop.BackColor = System.Drawing.Color.DarkOrange;
            this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStop.Location = new System.Drawing.Point(0, 50);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(50, 50);
            this.bStop.TabIndex = 2;
            this.bStop.Text = "■";
            this.bStop.UseVisualStyleBackColor = false;
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.BackColor = System.Drawing.Color.DarkOrange;
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.Location = new System.Drawing.Point(350, 0);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(50, 50);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "button3";
            this.bSave.UseVisualStyleBackColor = false;
            // 
            // bDelete
            // 
            this.bDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDelete.BackColor = System.Drawing.Color.DarkOrange;
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDelete.Location = new System.Drawing.Point(350, 50);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(50, 50);
            this.bDelete.TabIndex = 4;
            this.bDelete.Text = "button4";
            this.bDelete.UseVisualStyleBackColor = false;
            // 
            // maskedTextBoxCurrentTime
            // 
            this.maskedTextBoxCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxCurrentTime.BackColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxCurrentTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxCurrentTime.Location = new System.Drawing.Point(0, 100);
            this.maskedTextBoxCurrentTime.Mask = "00:00:00.00";
            this.maskedTextBoxCurrentTime.Name = "maskedTextBoxCurrentTime";
            this.maskedTextBoxCurrentTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxCurrentTime.TabIndex = 14;
            // 
            // maskedTextBoxSplitEndTime
            // 
            this.maskedTextBoxSplitEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxSplitEndTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxSplitEndTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxSplitEndTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxSplitEndTime.Location = new System.Drawing.Point(70, 100);
            this.maskedTextBoxSplitEndTime.Mask = "00:00:00.00";
            this.maskedTextBoxSplitEndTime.Name = "maskedTextBoxSplitEndTime";
            this.maskedTextBoxSplitEndTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxSplitEndTime.TabIndex = 17;
            // 
            // maskedTextBoxSplitStartTime
            // 
            this.maskedTextBoxSplitStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxSplitStartTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxSplitStartTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxSplitStartTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxSplitStartTime.Location = new System.Drawing.Point(140, 100);
            this.maskedTextBoxSplitStartTime.Mask = "00:00:00.00";
            this.maskedTextBoxSplitStartTime.Name = "maskedTextBoxSplitStartTime";
            this.maskedTextBoxSplitStartTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxSplitStartTime.TabIndex = 18;
            // 
            // maskedTextBoxResultTime
            // 
            this.maskedTextBoxResultTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.maskedTextBoxResultTime.BackColor = System.Drawing.Color.Black;
            this.maskedTextBoxResultTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxResultTime.ForeColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxResultTime.Location = new System.Drawing.Point(210, 100);
            this.maskedTextBoxResultTime.Mask = "00:00:00.00";
            this.maskedTextBoxResultTime.Name = "maskedTextBoxResultTime";
            this.maskedTextBoxResultTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxResultTime.TabIndex = 19;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hScrollBar.LargeChange = 2;
            this.hScrollBar.Location = new System.Drawing.Point(280, 100);
            this.hScrollBar.Maximum = 1;
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(120, 13);
            this.hScrollBar.TabIndex = 20;
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.maskedTextBoxResultTime);
            this.Controls.Add(this.maskedTextBoxSplitStartTime);
            this.Controls.Add(this.maskedTextBoxSplitEndTime);
            this.Controls.Add(this.maskedTextBoxCurrentTime);
            this.Controls.Add(this.pbWaveViewer);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bPlayPause);
            this.Controls.Add(this.bStop);
            this.MinimumSize = new System.Drawing.Size(400, 113);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(400, 113);
            this.Load += new System.EventHandler(this.ProjectControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWaveViewer;
        private System.Windows.Forms.Button bPlayPause;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bDelete;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxCurrentTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSplitEndTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxSplitStartTime;
        private System.Windows.Forms.MaskedTextBox maskedTextBoxResultTime;
        private System.Windows.Forms.HScrollBar hScrollBar;
    }
}
