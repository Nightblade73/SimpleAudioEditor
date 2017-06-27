namespace SimpleAudioEditor.PeachStudio.View
{
    partial class PeachEditor
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
            SimpleAudioEditor.PeachStudio.Project project1 = new SimpleAudioEditor.PeachStudio.Project();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PeachEditor));
            this.projectControl = new SimpleAudioEditor.PeachStudio.ProjectControl();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.panelSample = new System.Windows.Forms.Panel();
            this.panelSupport = new System.Windows.Forms.Panel();
            this.buttonAddSample = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.panelSample.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectControl
            // 
            this.projectControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.projectControl.CurrentProject = project1;
            this.projectControl.Location = new System.Drawing.Point(12, 12);
            this.projectControl.MinimumSize = new System.Drawing.Size(475, 100);
            this.projectControl.Name = "projectControl";
            this.projectControl.Size = new System.Drawing.Size(700, 100);
            this.projectControl.TabIndex = 0;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.Location = new System.Drawing.Point(590, 117);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(122, 42);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // panelSample
            // 
            this.panelSample.AllowDrop = true;
            this.panelSample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSample.AutoScroll = true;
            this.panelSample.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panelSample.Controls.Add(this.panelSupport);
            this.panelSample.Location = new System.Drawing.Point(12, 146);
            this.panelSample.MinimumSize = new System.Drawing.Size(700, 302);
            this.panelSample.Name = "panelSample";
            this.panelSample.Size = new System.Drawing.Size(700, 378);
            this.panelSample.TabIndex = 3;
            this.panelSample.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.panelSample_ControlAdded);
            this.panelSample.Layout += new System.Windows.Forms.LayoutEventHandler(this.panelSample_Layout);
            // 
            // panelSupport
            // 
            this.panelSupport.BackColor = System.Drawing.Color.Transparent;
            this.panelSupport.Location = new System.Drawing.Point(0, 0);
            this.panelSupport.Name = "panelSupport";
            this.panelSupport.Size = new System.Drawing.Size(0, 304);
            this.panelSupport.TabIndex = 0;
            // 
            // buttonAddSample
            // 
            this.buttonAddSample.BackColor = System.Drawing.Color.DarkOrange;
            this.buttonAddSample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSample.Location = new System.Drawing.Point(12, 117);
            this.buttonAddSample.Name = "buttonAddSample";
            this.buttonAddSample.Size = new System.Drawing.Size(75, 23);
            this.buttonAddSample.TabIndex = 4;
            this.buttonAddSample.Text = "Add";
            this.buttonAddSample.UseVisualStyleBackColor = false;
            this.buttonAddSample.Click += new System.EventHandler(this.buttonAddSample_Click);
            // 
            // PeachEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(732, 548);
            this.Controls.Add(this.buttonAddSample);
            this.Controls.Add(this.panelSample);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.projectControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(740, 480);
            this.Name = "PeachEditor";
            this.Text = "PeachEditor";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.panelSample.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ProjectControl projectControl;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Panel panelSample;
        private System.Windows.Forms.Button buttonAddSample;
        private System.Windows.Forms.Panel panelSupport;
    }
}