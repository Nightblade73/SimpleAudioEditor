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
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // pbWaveViewer
            // 
            this.pbWaveViewer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbWaveViewer.Location = new System.Drawing.Point(50, 0);
            this.pbWaveViewer.Name = "pbWaveViewer";
            this.pbWaveViewer.Size = new System.Drawing.Size(425, 144);
            this.pbWaveViewer.TabIndex = 0;
            this.pbWaveViewer.TabStop = false;
            // 
            // bPlayPause
            // 
            this.bPlayPause.Location = new System.Drawing.Point(0, 0);
            this.bPlayPause.Name = "bPlayPause";
            this.bPlayPause.Size = new System.Drawing.Size(50, 50);
            this.bPlayPause.TabIndex = 1;
            this.bPlayPause.Text = "button1";
            this.bPlayPause.UseVisualStyleBackColor = true;
            // 
            // bStop
            // 
            this.bStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bStop.Location = new System.Drawing.Point(0, 94);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(50, 50);
            this.bStop.TabIndex = 2;
            this.bStop.Text = "button2";
            this.bStop.UseVisualStyleBackColor = true;
            // 
            // bSave
            // 
            this.bSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSave.Location = new System.Drawing.Point(475, 0);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(50, 50);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "button3";
            this.bSave.UseVisualStyleBackColor = true;
            // 
            // bDelete
            // 
            this.bDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bDelete.Location = new System.Drawing.Point(475, 94);
            this.bDelete.Name = "bDelete";
            this.bDelete.Size = new System.Drawing.Size(50, 50);
            this.bDelete.TabIndex = 4;
            this.bDelete.Text = "button4";
            this.bDelete.UseVisualStyleBackColor = true;
            // 
            // ProjectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pbWaveViewer);
            this.Controls.Add(this.bDelete);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.bPlayPause);
            this.Controls.Add(this.bStop);
            this.MinimumSize = new System.Drawing.Size(475, 100);
            this.Name = "ProjectControl";
            this.Size = new System.Drawing.Size(525, 144);
            this.Load += new System.EventHandler(this.ProjectControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbWaveViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWaveViewer;
        private System.Windows.Forms.Button bPlayPause;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Button bDelete;
    }
}
