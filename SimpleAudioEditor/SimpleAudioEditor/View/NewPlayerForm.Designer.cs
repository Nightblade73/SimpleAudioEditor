﻿namespace SimpleAudioEditor.View
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelSamples = new System.Windows.Forms.Panel();
            this.buttonAddSample = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.AutoScroll = true;
            this.panelMain.Location = new System.Drawing.Point(12, 12);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(660, 85);
            this.panelMain.TabIndex = 10;
            // 
            // panelSamples
            // 
            this.panelSamples.AutoScroll = true;
            this.panelSamples.Location = new System.Drawing.Point(12, 132);
            this.panelSamples.Name = "panelSamples";
            this.panelSamples.Size = new System.Drawing.Size(660, 208);
            this.panelSamples.TabIndex = 9;
            // 
            // buttonAddSample
            // 
            this.buttonAddSample.BackColor = System.Drawing.Color.OrangeRed;
            this.buttonAddSample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSample.Location = new System.Drawing.Point(12, 103);
            this.buttonAddSample.Name = "buttonAddSample";
            this.buttonAddSample.Size = new System.Drawing.Size(126, 23);
            this.buttonAddSample.TabIndex = 8;
            this.buttonAddSample.Text = "Добавить файл";
            this.buttonAddSample.UseVisualStyleBackColor = false;
            this.buttonAddSample.Click += new System.EventHandler(this.buttonAddSample_Click);
            // 
            // NewPlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ClientSize = new System.Drawing.Size(684, 356);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelSamples);
            this.Controls.Add(this.buttonAddSample);
            this.Name = "NewPlayerForm";
            this.Text = "NewPlayerForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelSamples;
        private System.Windows.Forms.Button buttonAddSample;
    }
}