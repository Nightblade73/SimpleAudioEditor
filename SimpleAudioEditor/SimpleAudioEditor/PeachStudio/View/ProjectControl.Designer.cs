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
            this.maskedTextBoxCurrentTime = new System.Windows.Forms.MaskedTextBox();
            this.pbWaveViewer = new System.Windows.Forms.PictureBox();
            this.bDelete = new System.Windows.Forms.Button();
            this.bSave = new System.Windows.Forms.Button();
            this.bPlayPause = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // maskedTextBoxCurrentTime
            // 
            this.maskedTextBoxCurrentTime.BackColor = System.Drawing.Color.DarkOrange;
            this.maskedTextBoxCurrentTime.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.maskedTextBoxCurrentTime.Location = new System.Drawing.Point(40, 67);
            this.maskedTextBoxCurrentTime.Mask = "00:00:00.00";
            this.maskedTextBoxCurrentTime.Name = "maskedTextBoxCurrentTime";
            this.maskedTextBoxCurrentTime.Size = new System.Drawing.Size(70, 13);
            this.maskedTextBoxCurrentTime.TabIndex = 14;
            // 
            // pbWaveViewer
            // 
            this.pbWaveViewer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbWaveViewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.pbWaveViewer.Location = new System.Drawing.Point(40, 0);
            this.pbWaveViewer.Name = "pbWaveViewer";
            this.pbWaveViewer.Size = new System.Drawing.Size(618, 80);
            this.pbWaveViewer.TabIndex = 0;
            this.pbWaveViewer.TabStop = false;
            this.pbWaveViewer.Paint += new System.Windows.Forms.PaintEventHandler(this.pbWaveViewer_Paint);
            this.pbWaveViewer.Layout += new System.Windows.Forms.LayoutEventHandler(this.pbWaveViewer_Layout);
            this.pbWaveViewer.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pbWaveViewer_MouseDoubleClick);
            // 
            // bDelete
            // 
            this.bDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bDelete.BackColor = System.Drawing.Color.DarkRed;
            this.bDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bDelete.CausesValidation = false;
            this.bDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bDelete.Image = global::SimpleAudioEditor.Properties.Resources.icons8_Peach_48;
            this.bDelete.Location = new System.Drawing.Point(658, 40);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(40, 40);
            this.bDelete.TabIndex = 4;
            this.bDelete.UseVisualStyleBackColor = false;
            this.bDelete.Click += new System.EventHandler(this.bDelete_Click);
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.BackColor = System.Drawing.Color.LimeGreen;
            this.bSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bSave.Image = global::SimpleAudioEditor.Properties.Resources.icons8_Peach_48;
            this.bSave.Location = new System.Drawing.Point(658, 0);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(40, 40);
            this.bSave.TabIndex = 3;
            this.bSave.UseVisualStyleBackColor = false;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // bPlayPause
            // 
            this.bPlayPause.AccessibleName = "started";
            this.bPlayPause.BackColor = System.Drawing.Color.DarkOrange;
            this.bPlayPause.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.play_icon;
            this.bPlayPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bPlayPause.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bPlayPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPlayPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bPlayPause.Location = new System.Drawing.Point(0, 0);
            this.bPlayPause.Name = "bPlayPause";
            this.bPlayPause.Size = new System.Drawing.Size(40, 40);
            this.bPlayPause.TabIndex = 1;
            this.bPlayPause.UseVisualStyleBackColor = false;
            this.bPlayPause.Click += new System.EventHandler(this.bPlayPause_Click);
            // 
            // bStop
            // 
            this.bStop.BackColor = System.Drawing.Color.DarkOrange;
            this.bStop.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.stop_icon;
            this.bStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.bStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStop.Location = new System.Drawing.Point(0, 40);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(40, 40);
            this.bStop.TabIndex = 2;
            this.bStop.UseVisualStyleBackColor = false;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.maskedTextBoxCurrentTime);
            this.Controls.Add(this.pbWaveViewer);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bPlayPause);
            this.Controls.Add(this.bStop);
            this.MinimumSize = new System.Drawing.Size(700, 80);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(698, 78);
            this.Load += new System.EventHandler(this.ProjectControl_Load);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.ProjectControl_Layout);
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
    }
}
