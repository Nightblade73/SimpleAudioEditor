﻿namespace SimpleAudioEditor {
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
                components.Dispose();
                mEditor.CloseWaveFile();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonLoadFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.trackBarVolume = new System.Windows.Forms.TrackBar();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.samplesPanel = new System.Windows.Forms.Panel();
            this.groupBoxSemples = new System.Windows.Forms.GroupBox();
            this.mEditor = new SimpleAudioEditor.Controller.WaveController.WaveEditor();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).BeginInit();
            this.groupBoxSemples.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(276, 94);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "старт";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(195, 94);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 23);
            this.buttonPause.TabIndex = 2;
            this.buttonPause.Text = "пауза";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.Location = new System.Drawing.Point(357, 94);
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Size = new System.Drawing.Size(174, 23);
            this.buttonLoadFile.TabIndex = 3;
            this.buttonLoadFile.Text = "Выбрать файлы";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "fileDialogOpenSound";
            this.openFileDialog.Multiselect = true;
            // 
            // trackBarVolume
            // 
            this.trackBarVolume.Location = new System.Drawing.Point(537, 94);
            this.trackBarVolume.Maximum = 100;
            this.trackBarVolume.Name = "trackBarVolume";
            this.trackBarVolume.Size = new System.Drawing.Size(174, 45);
            this.trackBarVolume.TabIndex = 4;
            this.trackBarVolume.TickFrequency = 10;
            this.trackBarVolume.Scroll += new System.EventHandler(this.trackBarVolume_Scroll);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(15, 96);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(174, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // samplesPanel
            // 
            this.samplesPanel.AutoScroll = true;
            this.samplesPanel.Location = new System.Drawing.Point(6, 22);
            this.samplesPanel.Name = "samplesPanel";
            this.samplesPanel.Size = new System.Drawing.Size(700, 360);
            this.samplesPanel.TabIndex = 8;
            // 
            // groupBoxSemples
            // 
            this.groupBoxSemples.Controls.Add(this.samplesPanel);
            this.groupBoxSemples.Location = new System.Drawing.Point(12, 123);
            this.groupBoxSemples.Name = "groupBoxSemples";
            this.groupBoxSemples.Size = new System.Drawing.Size(713, 392);
            this.groupBoxSemples.TabIndex = 9;
            this.groupBoxSemples.TabStop = false;
            this.groupBoxSemples.Text = "Список сэмплов";
            // 
            // mEditor
            // 
            this.mEditor.AddAndDeleteButtonsVisibility = false;
            this.mEditor.BackColor = System.Drawing.Color.White;
            this.mEditor.Location = new System.Drawing.Point(12, 12);
            this.mEditor.MinimumSize = new System.Drawing.Size(675, 0);
            this.mEditor.Name = "mEditor";
            this.mEditor.Size = new System.Drawing.Size(713, 76);
            this.mEditor.TabIndex = 5;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 518);
            this.Controls.Add(this.groupBoxSemples);
            this.Controls.Add(this.mEditor);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.trackBarVolume);
            this.Controls.Add(this.buttonLoadFile);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStart);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarVolume)).EndInit();
            this.groupBoxSemples.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonLoadFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TrackBar trackBarVolume;
        private Controller.WaveController.WaveEditor mEditor;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel samplesPanel;
        private System.Windows.Forms.GroupBox groupBoxSemples;
    }
}

