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
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonAddSample = new System.Windows.Forms.Button();
            this.panelSamples = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonAddPause = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.Location = new System.Drawing.Point(12, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(700, 92);
            this.panelMain.TabIndex = 10;
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
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(359, 110);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(221, 21);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonAddPause
            // 
            this.buttonAddPause.BackColor = System.Drawing.Color.OrangeRed;
            this.buttonAddPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddPause.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.buttonAddPause.Location = new System.Drawing.Point(586, 109);
            this.buttonAddPause.Name = "buttonAddPause";
            this.buttonAddPause.Size = new System.Drawing.Size(126, 23);
            this.buttonAddPause.TabIndex = 12;
            this.buttonAddPause.Text = "Добавить паузу";
            this.buttonAddPause.UseVisualStyleBackColor = false;
            this.buttonAddPause.Click += new System.EventHandler(this.buttonAddPause_Click);
            // 
            // NewPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(724, 441);
            this.Controls.Add(this.buttonAddPause);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.panelSamples);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.buttonAddSample);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewPlayerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Peach Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NewPlayerForm_FormClosed);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Button buttonAddSample;

        private System.Windows.Forms.Panel panelSamples;

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button buttonAddPause;

    }
}