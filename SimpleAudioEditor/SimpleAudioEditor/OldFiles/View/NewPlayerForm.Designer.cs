namespace SimpleAudioEditor.View
{
    partial class NewPlayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPlayerForm));
            this.buttonAddSample = new System.Windows.Forms.Button();
            this.panelSamples = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // buttonAddSample
            // 
            this.buttonAddSample.BackColor = System.Drawing.Color.OrangeRed;
            this.buttonAddSample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSample.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAddSample.Location = new System.Drawing.Point(12, 110);
            this.buttonAddSample.Name = "buttonAddSample";
            this.buttonAddSample.Size = new System.Drawing.Size(126, 23);
            this.buttonAddSample.TabIndex = 8;
            this.buttonAddSample.Text = "Добавить файл";
            this.buttonAddSample.UseVisualStyleBackColor = false;
            this.buttonAddSample.Click += new System.EventHandler(this.buttonAddSample_Click);
            // 
            // panelSamples
            // 
            this.panelSamples.Location = new System.Drawing.Point(12, 140);
            this.panelSamples.Name = "panelSamples";
            this.panelSamples.Size = new System.Drawing.Size(700, 289);
            this.panelSamples.TabIndex = 11;
            this.panelSamples.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.panelSamples_ControlAdded);
            // 
            // NewPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(724, 441);
            this.Controls.Add(this.panelSamples);
            this.Controls.Add(this.buttonAddSample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewPlayerForm";
            this.Text = "Peach Editor";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonAddSample;
        private System.Windows.Forms.Panel panelSamples;
    }
}