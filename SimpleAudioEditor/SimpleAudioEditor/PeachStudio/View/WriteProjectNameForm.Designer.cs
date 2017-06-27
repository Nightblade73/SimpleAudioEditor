namespace SimpleAudioEditor.View
{
    partial class WriteProjectNameForm
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
            this.tBName = new System.Windows.Forms.TextBox();
            this.LabText = new System.Windows.Forms.Label();
            this.butOK = new System.Windows.Forms.Button();
            this.butCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tBName
            // 
            this.tBName.BackColor = System.Drawing.Color.DimGray;
            this.tBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tBName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tBName.ForeColor = System.Drawing.Color.Orange;
            this.tBName.Location = new System.Drawing.Point(45, 48);
            this.tBName.Name = "tBName";
            this.tBName.Size = new System.Drawing.Size(185, 23);
            this.tBName.TabIndex = 0;
            // 
            // LabText
            // 
            this.LabText.AutoSize = true;
            this.LabText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LabText.ForeColor = System.Drawing.Color.Orange;
            this.LabText.Location = new System.Drawing.Point(58, 19);
            this.LabText.Name = "LabText";
            this.LabText.Size = new System.Drawing.Size(161, 18);
            this.LabText.TabIndex = 1;
            this.LabText.Text = "Введите имя проекта:";
            // 
            // butOK
            // 
            this.butOK.BackColor = System.Drawing.Color.Gray;
            this.butOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butOK.ForeColor = System.Drawing.Color.Orange;
            this.butOK.Location = new System.Drawing.Point(12, 80);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(109, 27);
            this.butOK.TabIndex = 2;
            this.butOK.Text = "Принять";
            this.butOK.UseVisualStyleBackColor = false;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancle
            // 
            this.butCancle.BackColor = System.Drawing.Color.Gray;
            this.butCancle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butCancle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.butCancle.ForeColor = System.Drawing.Color.Orange;
            this.butCancle.Location = new System.Drawing.Point(153, 80);
            this.butCancle.Name = "butCancle";
            this.butCancle.Size = new System.Drawing.Size(109, 27);
            this.butCancle.TabIndex = 3;
            this.butCancle.Text = "Отменить";
            this.butCancle.UseVisualStyleBackColor = false;
            this.butCancle.Click += new System.EventHandler(this.butCancle_Click);
            // 
            // WriteProjectNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(274, 119);
            this.Controls.Add(this.butCancle);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.LabText);
            this.Controls.Add(this.tBName);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.Color.OrangeRed;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "WriteProjectNameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tBName;
        private System.Windows.Forms.Label LabText;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.Button butCancle;
    }
}