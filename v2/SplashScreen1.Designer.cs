namespace EduXpress
{
    partial class SplashScreen1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen1));
            this.marqueeProgressBarControl1 = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.lblCopyright = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.lblProgramVersion = new DevExpress.XtraEditors.LabelControl();
            this.pictureEdit1 = new DevExpress.XtraEditors.PictureEdit();
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            this.SuspendLayout();
            // 
            // marqueeProgressBarControl1
            // 
            resources.ApplyResources(this.marqueeProgressBarControl1, "marqueeProgressBarControl1");
            this.marqueeProgressBarControl1.Name = "marqueeProgressBarControl1";
            this.marqueeProgressBarControl1.UseWaitCursor = true;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCopyright.Appearance.Font")));
            this.lblCopyright.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblCopyright.Appearance.Options.UseBackColor = true;
            this.lblCopyright.Appearance.Options.UseFont = true;
            this.lblCopyright.Appearance.Options.UseForeColor = true;
            this.lblCopyright.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            resources.ApplyResources(this.lblCopyright, "lblCopyright");
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.UseWaitCursor = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseBackColor = true;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.UseWaitCursor = true;
            // 
            // lblProgramVersion
            // 
            this.lblProgramVersion.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblProgramVersion.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblProgramVersion.Appearance.Font")));
            this.lblProgramVersion.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblProgramVersion.Appearance.Options.UseBackColor = true;
            this.lblProgramVersion.Appearance.Options.UseFont = true;
            this.lblProgramVersion.Appearance.Options.UseForeColor = true;
            this.lblProgramVersion.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            resources.ApplyResources(this.lblProgramVersion, "lblProgramVersion");
            this.lblProgramVersion.Name = "lblProgramVersion";
            this.lblProgramVersion.UseWaitCursor = true;
            // 
            // pictureEdit1
            // 
            this.pictureEdit1.EditValue = global::EduXpress.Properties.Resources.Bindu_Orange_157_56;
            resources.ApplyResources(this.pictureEdit1, "pictureEdit1");
            this.pictureEdit1.Name = "pictureEdit1";
            this.pictureEdit1.Properties.AllowFocused = false;
            this.pictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit1.Properties.ShowMenu = false;
            this.pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.pictureEdit1.UseWaitCursor = true;
            // 
            // imageCollection1
            // 
            resources.ApplyResources(this.imageCollection1, "imageCollection1");
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.InsertImage(global::EduXpress.Properties.Resources.EduXpress_Splash, "EduXpress_Splash", typeof(global::EduXpress.Properties.Resources), 0);
            this.imageCollection1.Images.SetKeyName(0, "EduXpress_Splash");
            this.imageCollection1.InsertImage(global::EduXpress.Properties.Resources.EduXpress_Splash_fr, "EduXpress_Splash_fr", typeof(global::EduXpress.Properties.Resources), 1);
            this.imageCollection1.Images.SetKeyName(1, "EduXpress_Splash_fr");
            // 
            // SplashScreen1
            // 
            this.AllowControlsInImageMode = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblProgramVersion);
            this.Controls.Add(this.pictureEdit1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.marqueeProgressBarControl1);
            this.Name = "SplashScreen1";
            this.ShowMode = DevExpress.XtraSplashScreen.ShowMode.Image;
            this.SplashImageOptions.Image = global::EduXpress.Properties.Resources.EduXpress_Splash_fr;
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.SplashScreen1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.marqueeProgressBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.MarqueeProgressBarControl marqueeProgressBarControl1;
        private DevExpress.XtraEditors.LabelControl lblCopyright;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.PictureEdit pictureEdit1;
        private DevExpress.XtraEditors.LabelControl lblProgramVersion;
        private DevExpress.Utils.ImageCollection imageCollection1;
    }
}
