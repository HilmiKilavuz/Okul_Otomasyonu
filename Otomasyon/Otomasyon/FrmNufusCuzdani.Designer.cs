namespace Otomasyon
{
    partial class FrmNufusCuzdani
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
            this.lblTC = new DevExpress.XtraEditors.LabelControl();
            this.lblAd = new DevExpress.XtraEditors.LabelControl();
            this.lblDogumTrh = new DevExpress.XtraEditors.LabelControl();
            this.lblCiniyet = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.lblSoyad = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTC
            // 
            this.lblTC.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTC.Appearance.Options.UseFont = true;
            this.lblTC.Location = new System.Drawing.Point(22, 130);
            this.lblTC.Name = "lblTC";
            this.lblTC.Size = new System.Drawing.Size(115, 21);
            this.lblTC.TabIndex = 0;
            this.lblTC.Text = "labelControl1";
            // 
            // lblAd
            // 
            this.lblAd.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblAd.Appearance.Options.UseFont = true;
            this.lblAd.Location = new System.Drawing.Point(234, 199);
            this.lblAd.Name = "lblAd";
            this.lblAd.Size = new System.Drawing.Size(115, 21);
            this.lblAd.TabIndex = 2;
            this.lblAd.Text = "labelControl3";
            // 
            // lblDogumTrh
            // 
            this.lblDogumTrh.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDogumTrh.Appearance.Options.UseFont = true;
            this.lblDogumTrh.Location = new System.Drawing.Point(234, 264);
            this.lblDogumTrh.Name = "lblDogumTrh";
            this.lblDogumTrh.Size = new System.Drawing.Size(115, 21);
            this.lblDogumTrh.TabIndex = 3;
            this.lblDogumTrh.Text = "labelControl4";
            // 
            // lblCiniyet
            // 
            this.lblCiniyet.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblCiniyet.Appearance.Options.UseFont = true;
            this.lblCiniyet.Location = new System.Drawing.Point(422, 264);
            this.lblCiniyet.Name = "lblCiniyet";
            this.lblCiniyet.Size = new System.Drawing.Size(115, 21);
            this.lblCiniyet.TabIndex = 5;
            this.lblCiniyet.Text = "labelControl6";
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.Location = new System.Drawing.Point(52, 190);
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.Size = new System.Drawing.Size(150, 174);
            this.pictureEdit1.TabIndex = 6;
            // 
            // lblSoyad
            // 
            this.lblSoyad.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSoyad.Appearance.Options.UseFont = true;
            this.lblSoyad.Location = new System.Drawing.Point(234, 130);
            this.lblSoyad.Name = "lblSoyad";
            this.lblSoyad.Size = new System.Drawing.Size(115, 21);
            this.lblSoyad.TabIndex = 1;
            this.lblSoyad.Text = "labelControl2";
            // 
            // FrmNufusCuzdani
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = global::Otomasyon.Properties.Resources.images;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(707, 435);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.lblCiniyet);
            this.Controls.Add(this.lblDogumTrh);
            this.Controls.Add(this.lblAd);
            this.Controls.Add(this.lblSoyad);
            this.Controls.Add(this.lblTC);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "FrmNufusCuzdani";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmNufusCuzdani";
            this.Load += new System.EventHandler(this.FrmNufusCuzdani_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTC;
        private DevExpress.XtraEditors.LabelControl lblAd;
        private DevExpress.XtraEditors.LabelControl lblDogumTrh;
        private DevExpress.XtraEditors.LabelControl lblCiniyet;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl lblSoyad;
    }
}