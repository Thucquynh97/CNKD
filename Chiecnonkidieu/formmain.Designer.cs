namespace Chiecnonkidieu
{
    partial class Formmain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Formmain));
            this.playgane = new System.Windows.Forms.PictureBox();
            this.xemdiem = new System.Windows.Forms.PictureBox();
            this.thoat = new System.Windows.Forms.PictureBox();
            this.tuychinh = new System.Windows.Forms.PictureBox();
            this.bthuongdan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.playgane)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xemdiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.thoat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tuychinh)).BeginInit();
            this.SuspendLayout();
            // 
            // playgane
            // 
            this.playgane.BackColor = System.Drawing.Color.Transparent;
            this.playgane.BackgroundImage = global::Chiecnonkidieu.Properties.Resources.colorful_triangles_wallpaper_800x480_47_12;
            this.playgane.Cursor = System.Windows.Forms.Cursors.Hand;
            this.playgane.Location = new System.Drawing.Point(348, 81);
            this.playgane.Name = "playgane";
            this.playgane.Size = new System.Drawing.Size(186, 75);
            this.playgane.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.playgane.TabIndex = 1;
            this.playgane.TabStop = false;
            this.playgane.Click += new System.EventHandler(this.playgane_Click);
            // 
            // xemdiem
            // 
            this.xemdiem.BackColor = System.Drawing.Color.Transparent;
            this.xemdiem.BackgroundImage = global::Chiecnonkidieu.Properties.Resources.diem;
            this.xemdiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.xemdiem.Location = new System.Drawing.Point(344, 174);
            this.xemdiem.Name = "xemdiem";
            this.xemdiem.Size = new System.Drawing.Size(210, 63);
            this.xemdiem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xemdiem.TabIndex = 1;
            this.xemdiem.TabStop = false;
            this.xemdiem.Click += new System.EventHandler(this.xemdiem_Click);
            // 
            // thoat
            // 
            this.thoat.BackColor = System.Drawing.Color.Transparent;
            this.thoat.BackgroundImage = global::Chiecnonkidieu.Properties.Resources.Thoat;
            this.thoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.thoat.Location = new System.Drawing.Point(366, 332);
            this.thoat.Name = "thoat";
            this.thoat.Size = new System.Drawing.Size(168, 71);
            this.thoat.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.thoat.TabIndex = 1;
            this.thoat.TabStop = false;
            this.thoat.Click += new System.EventHandler(this.thoat_Click);
            // 
            // tuychinh
            // 
            this.tuychinh.BackColor = System.Drawing.Color.Transparent;
            this.tuychinh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tuychinh.Image = ((System.Drawing.Image)(resources.GetObject("tuychinh.Image")));
            this.tuychinh.Location = new System.Drawing.Point(366, 259);
            this.tuychinh.Name = "tuychinh";
            this.tuychinh.Size = new System.Drawing.Size(165, 55);
            this.tuychinh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.tuychinh.TabIndex = 2;
            this.tuychinh.TabStop = false;
            this.tuychinh.Click += new System.EventHandler(this.tuychinh_Click);
            // 
            // bthuongdan
            // 
            this.bthuongdan.Location = new System.Drawing.Point(528, 432);
            this.bthuongdan.Name = "bthuongdan";
            this.bthuongdan.Size = new System.Drawing.Size(75, 23);
            this.bthuongdan.TabIndex = 3;
            this.bthuongdan.Text = "Hướng Dẫn";
            this.bthuongdan.UseVisualStyleBackColor = true;
            this.bthuongdan.Click += new System.EventHandler(this.bthuongdan_Click);
            // 
            // Formmain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImage = global::Chiecnonkidieu.Properties.Resources.background;
            this.ClientSize = new System.Drawing.Size(665, 480);
            this.Controls.Add(this.bthuongdan);
            this.Controls.Add(this.tuychinh);
            this.Controls.Add(this.thoat);
            this.Controls.Add(this.xemdiem);
            this.Controls.Add(this.playgane);
            this.Name = "Formmain";
            this.Text = "Chiếc nón kỳ diệu";
            this.Load += new System.EventHandler(this.Formmain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.playgane)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xemdiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.thoat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tuychinh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox playgane;
        private System.Windows.Forms.PictureBox xemdiem;
        private System.Windows.Forms.PictureBox thoat;
        private System.Windows.Forms.PictureBox tuychinh;
        private System.Windows.Forms.Button bthuongdan;
    }
}