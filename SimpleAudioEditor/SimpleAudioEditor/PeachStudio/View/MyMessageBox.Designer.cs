namespace SimpleAudioEditor.PeachStudio.View
{
    partial class MyMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyMessageBox));
            this.labelMes = new System.Windows.Forms.Label();
            this.butCancle = new System.Windows.Forms.Button();
            this.butOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelMes
            // 
            this.labelMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMes.ForeColor = System.Drawing.Color.Orange;
            this.labelMes.Location = new System.Drawing.Point(12, 0);
            this.labelMes.Name = "labelMes";
            this.labelMes.Size = new System.Drawing.Size(210, 89);
            this.labelMes.TabIndex = 0;
            this.labelMes.Text = "Message";
            this.labelMes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // butCancle
            // 
            this.butCancle.BackColor = System.Drawing.Color.Gray;
            this.butCancle.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.icons8_Peach_48_Delete;
            this.butCancle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butCancle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butCancle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butCancle.ForeColor = System.Drawing.Color.Gray;
            this.butCancle.Location = new System.Drawing.Point(144, 83);
            this.butCancle.Name = "butCancle";
            this.butCancle.Size = new System.Drawing.Size(52, 51);
            this.butCancle.TabIndex = 5;
            this.butCancle.UseVisualStyleBackColor = false;
            this.butCancle.Click += new System.EventHandler(this.butCancle_Click);
            // 
            // butOK
            // 
            this.butOK.BackColor = System.Drawing.Color.Gray;
            this.butOK.BackgroundImage = global::SimpleAudioEditor.Properties.Resources.icons8_Peach_48_OK;
            this.butOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.butOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butOK.ForeColor = System.Drawing.Color.Gray;
            this.butOK.Location = new System.Drawing.Point(38, 83);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(52, 51);
            this.butOK.TabIndex = 4;
            this.butOK.UseVisualStyleBackColor = false;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // MyMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(234, 146);
            this.Controls.Add(this.butCancle);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.labelMes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MyMessageBox";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelMes;
        private System.Windows.Forms.Button butCancle;
        private System.Windows.Forms.Button butOK;
    }
}