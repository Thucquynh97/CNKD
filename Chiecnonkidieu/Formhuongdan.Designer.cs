namespace Chiecnonkidieu
{
    partial class Formhuongdan
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
            this.txthuongdan = new System.Windows.Forms.TextBox();
            this.btthoat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txthuongdan
            // 
            this.txthuongdan.Location = new System.Drawing.Point(0, 0);
            this.txthuongdan.Multiline = true;
            this.txthuongdan.Name = "txthuongdan";
            this.txthuongdan.Size = new System.Drawing.Size(520, 459);
            this.txthuongdan.TabIndex = 0;
            this.txthuongdan.Text = "ABC";
            // 
            // btthoat
            // 
            this.btthoat.Location = new System.Drawing.Point(445, 0);
            this.btthoat.Name = "btthoat";
            this.btthoat.Size = new System.Drawing.Size(75, 23);
            this.btthoat.TabIndex = 1;
            this.btthoat.Text = "Thoát";
            this.btthoat.UseVisualStyleBackColor = true;
            this.btthoat.Click += new System.EventHandler(this.btthoat_Click);
            // 
            // Formhuongdan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 460);
            this.Controls.Add(this.btthoat);
            this.Controls.Add(this.txthuongdan);
            this.Name = "Formhuongdan";
            this.Text = "Hướng dẫn";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txthuongdan;
        private System.Windows.Forms.Button btthoat;
    }
}