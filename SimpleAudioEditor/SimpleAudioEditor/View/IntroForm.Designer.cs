namespace SimpleAudioEditor
{
    partial class IntroForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntroForm));
            this.panelPath = new System.Windows.Forms.Panel();
            this.btnChoosePath = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panelSamples = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.layoutProjects = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox_Divider = new System.Windows.Forms.GroupBox();
            this.pictureboxWarning = new System.Windows.Forms.PictureBox();
            this.btnNewProject = new System.Windows.Forms.Button();
            this.btnPlaySample = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.panelPath.SuspendLayout();
            this.panelSamples.SuspendLayout();
            this.layoutProjects.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // panelPath
            // 
            this.panelPath.Controls.Add(this.pictureboxWarning);
            this.panelPath.Controls.Add(this.btnChoosePath);
            this.panelPath.Controls.Add(this.label8);
            this.panelPath.Location = new System.Drawing.Point(515, 130);
            this.panelPath.Name = "panelPath";
            this.panelPath.Size = new System.Drawing.Size(181, 194);
            this.panelPath.TabIndex = 16;
            // 
            // btnChoosePath
            // 
            this.btnChoosePath.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnChoosePath.FlatAppearance.BorderSize = 0;
            this.btnChoosePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChoosePath.Location = new System.Drawing.Point(30, 146);
            this.btnChoosePath.Name = "btnChoosePath";
            this.btnChoosePath.Size = new System.Drawing.Size(105, 23);
            this.btnChoosePath.TabIndex = 9;
            this.btnChoosePath.Text = "Выбрать путь";
            this.btnChoosePath.UseVisualStyleBackColor = false;
            this.btnChoosePath.Click += new System.EventHandler(this.btnChoosePath_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label8.Location = new System.Drawing.Point(5, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(163, 30);
            this.label8.TabIndex = 10;
            this.label8.Text = "Внимание: Выберите путь \r\nсохранения проектов";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelSamples
            // 
            this.panelSamples.Controls.Add(this.btnPlaySample);
            this.panelSamples.Controls.Add(this.label1);
            this.panelSamples.Controls.Add(this.button13);
            this.panelSamples.Controls.Add(this.label2);
            this.panelSamples.Enabled = false;
            this.panelSamples.Location = new System.Drawing.Point(498, 38);
            this.panelSamples.Name = "panelSamples";
            this.panelSamples.Size = new System.Drawing.Size(214, 75);
            this.panelSamples.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Onyx", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(27, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Прошлые сэмплы:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.label2.Location = new System.Drawing.Point(37, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Название сэмпла | Вечера";
            // 
            // layoutProjects
            // 
            this.layoutProjects.AutoScroll = true;
            this.layoutProjects.Controls.Add(this.btnNewProject);
            this.layoutProjects.Enabled = false;
            this.layoutProjects.Location = new System.Drawing.Point(19, 38);
            this.layoutProjects.Margin = new System.Windows.Forms.Padding(6);
            this.layoutProjects.Name = "layoutProjects";
            this.layoutProjects.Size = new System.Drawing.Size(458, 359);
            this.layoutProjects.TabIndex = 14;
            // 
            // groupBox_Divider
            // 
            this.groupBox_Divider.BackColor = System.Drawing.Color.Chocolate;
            this.groupBox_Divider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox_Divider.Location = new System.Drawing.Point(477, 38);
            this.groupBox_Divider.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox_Divider.Name = "groupBox_Divider";
            this.groupBox_Divider.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox_Divider.Size = new System.Drawing.Size(2, 359);
            this.groupBox_Divider.TabIndex = 13;
            this.groupBox_Divider.TabStop = false;
            // 
            // pictureboxWarning
            // 
            this.pictureboxWarning.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.icons8_Error_104;
            this.pictureboxWarning.Location = new System.Drawing.Point(30, 5);
            this.pictureboxWarning.Name = "pictureboxWarning";
            this.pictureboxWarning.Size = new System.Drawing.Size(105, 99);
            this.pictureboxWarning.TabIndex = 8;
            this.pictureboxWarning.TabStop = false;
            // 
            // btnNewProject
            // 
            this.btnNewProject.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnNewProject.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.icons8_Add_Folder_104;
            this.btnNewProject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNewProject.FlatAppearance.BorderSize = 0;
            this.btnNewProject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewProject.Font = new System.Drawing.Font("Microsoft New Tai Lue", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewProject.ForeColor = System.Drawing.Color.Linen;
            this.btnNewProject.Location = new System.Drawing.Point(10, 15);
            this.btnNewProject.Margin = new System.Windows.Forms.Padding(10, 15, 25, 10);
            this.btnNewProject.Name = "btnNewProject";
            this.btnNewProject.Size = new System.Drawing.Size(100, 100);
            this.btnNewProject.TabIndex = 1;
            this.btnNewProject.Text = "Новый\r\nпроект\r\n\r\n";
            this.btnNewProject.UseVisualStyleBackColor = false;
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // btnPlaySample
            // 
            this.btnPlaySample.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.btnPlaySample.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.icons8_Pause_48;
            this.btnPlaySample.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlaySample.FlatAppearance.BorderSize = 0;
            this.btnPlaySample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlaySample.Location = new System.Drawing.Point(185, 38);
            this.btnPlaySample.Name = "btnPlaySample";
            this.btnPlaySample.Size = new System.Drawing.Size(24, 24);
            this.btnPlaySample.TabIndex = 7;
            this.btnPlaySample.UseVisualStyleBackColor = false;
            this.btnPlaySample.Click += new System.EventHandler(this.btnPlaySample_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.button13.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.icons8_Audio_Wave_26;
            this.button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button13.FlatAppearance.BorderSize = 0;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button13.Location = new System.Drawing.Point(8, 38);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(24, 22);
            this.button13.TabIndex = 0;
            this.button13.UseVisualStyleBackColor = false;
            // 
            // IntroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(724, 441);
            this.Controls.Add(this.panelPath);
            this.Controls.Add(this.layoutProjects);
            this.Controls.Add(this.groupBox_Divider);
            this.Controls.Add(this.panelSamples);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "IntroForm";
            this.Text = "Peach editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IntroForm_FormClosing);
            this.Load += new System.EventHandler(this.IntroForm_Load);
            this.panelPath.ResumeLayout(false);
            this.panelPath.PerformLayout();
            this.panelSamples.ResumeLayout(false);
            this.panelSamples.PerformLayout();
            this.layoutProjects.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxWarning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPath;
        private System.Windows.Forms.PictureBox pictureboxWarning;
        private System.Windows.Forms.Button btnChoosePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelSamples;
        private System.Windows.Forms.Button btnPlaySample;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel layoutProjects;
        private System.Windows.Forms.Button btnNewProject;
        private System.Windows.Forms.GroupBox groupBox_Divider;
    }
}