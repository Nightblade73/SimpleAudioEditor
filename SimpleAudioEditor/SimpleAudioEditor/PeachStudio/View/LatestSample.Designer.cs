namespace SimpleAudioEditor.PeachStudio.View
{
    partial class LatestSample
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureWave = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.btnPlayStop = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureWave)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureWave
            // 
            this.pictureWave.BackColor = System.Drawing.Color.Transparent;
            this.pictureWave.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.icons8_Audio_Wave_26;
            this.pictureWave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureWave.Location = new System.Drawing.Point(0, 0);
            this.pictureWave.Name = "pictureWave";
            this.pictureWave.Size = new System.Drawing.Size(30, 25);
            this.pictureWave.TabIndex = 0;
            this.pictureWave.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoEllipsis = true;
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.SystemColors.Control;
            this.labelName.Location = new System.Drawing.Point(36, 4);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(141, 19);
            this.labelName.TabIndex = 1;
            this.labelName.Text = "label1";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPlayStop
            // 
            this.btnPlayStop.AccessibleName = "play";
            this.btnPlayStop.BackColor = System.Drawing.Color.Transparent;
            this.btnPlayStop.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.icons8_Play_26;
            this.btnPlayStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPlayStop.FlatAppearance.BorderSize = 0;
            this.btnPlayStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlayStop.Location = new System.Drawing.Point(183, -1);
            this.btnPlayStop.Name = "btnPlayStop";
            this.btnPlayStop.Size = new System.Drawing.Size(24, 26);
            this.btnPlayStop.TabIndex = 2;
            this.btnPlayStop.UseVisualStyleBackColor = false;
            this.btnPlayStop.Click += new System.EventHandler(this.btnPlayStop_Click);
            // 
            // LatestSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnPlayStop);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.pictureWave);
            this.Name = "LatestSample";
            this.Size = new System.Drawing.Size(210, 25);
            ((System.ComponentModel.ISupportInitialize)(this.pictureWave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureWave;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Button btnPlayStop;
    }
}
