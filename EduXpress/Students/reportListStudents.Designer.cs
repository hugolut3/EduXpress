namespace EduXpress.Students
{
    partial class reportListStudents
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.printableComponentContainer1 = new DevExpress.XtraReports.UI.PrintableComponentContainer();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrlblSchoolYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureSchoolLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrlblClass = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblSchoolName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblSchoolMoto = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblSection = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Dpi = 254F;
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Dpi = 254F;
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.printableComponentContainer1});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 254F;
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.Name = "Detail";
            // 
            // printableComponentContainer1
            // 
            this.printableComponentContainer1.Dpi = 254F;
            this.printableComponentContainer1.LocationFloat = new DevExpress.Utils.PointFloat(25.00009F, 38.49999F);
            this.printableComponentContainer1.Name = "printableComponentContainer1";
            this.printableComponentContainer1.SizeF = new System.Drawing.SizeF(1950F, 190.5F);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrlblSchoolYear,
            this.xrPictureSchoolLogo,
            this.xrlblClass,
            this.xrLblSchoolName,
            this.xrLblSchoolMoto,
            this.xrLblSection});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 278.6935F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
            // 
            // xrlblSchoolYear
            // 
            this.xrlblSchoolYear.CanShrink = true;
            this.xrlblSchoolYear.Dpi = 254F;
            this.xrlblSchoolYear.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblSchoolYear.LocationFloat = new DevExpress.Utils.PointFloat(474.0702F, 123.6686F);
            this.xrlblSchoolYear.Multiline = true;
            this.xrlblSchoolYear.Name = "xrlblSchoolYear";
            this.xrlblSchoolYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlblSchoolYear.SizeF = new System.Drawing.SizeF(1149.034F, 41.91F);
            this.xrlblSchoolYear.StylePriority.UseFont = false;
            this.xrlblSchoolYear.StylePriority.UseTextAlignment = false;
            this.xrlblSchoolYear.Text = "School Year";
            this.xrlblSchoolYear.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureSchoolLogo
            // 
            this.xrPictureSchoolLogo.Dpi = 254F;
            this.xrPictureSchoolLogo.LocationFloat = new DevExpress.Utils.PointFloat(51.02269F, 25F);
            this.xrPictureSchoolLogo.Name = "xrPictureSchoolLogo";
            this.xrPictureSchoolLogo.SizeF = new System.Drawing.SizeF(357.7889F, 226.5901F);
            this.xrPictureSchoolLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
            // 
            // xrlblClass
            // 
            this.xrlblClass.CanShrink = true;
            this.xrlblClass.Dpi = 254F;
            this.xrlblClass.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrlblClass.LocationFloat = new DevExpress.Utils.PointFloat(474.0702F, 236.7835F);
            this.xrlblClass.Multiline = true;
            this.xrlblClass.Name = "xrlblClass";
            this.xrlblClass.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrlblClass.SizeF = new System.Drawing.SizeF(1149.034F, 41.91F);
            this.xrlblClass.StylePriority.UseFont = false;
            this.xrlblClass.StylePriority.UseTextAlignment = false;
            this.xrlblClass.Text = "Grade";
            this.xrlblClass.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblSchoolName
            // 
            this.xrLblSchoolName.CanShrink = true;
            this.xrLblSchoolName.Dpi = 254F;
            this.xrLblSchoolName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblSchoolName.LocationFloat = new DevExpress.Utils.PointFloat(474.0702F, 4.330142F);
            this.xrLblSchoolName.Multiline = true;
            this.xrLblSchoolName.Name = "xrLblSchoolName";
            this.xrLblSchoolName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblSchoolName.SizeF = new System.Drawing.SizeF(1149.034F, 53.97499F);
            this.xrLblSchoolName.StylePriority.UseFont = false;
            this.xrLblSchoolName.StylePriority.UseTextAlignment = false;
            this.xrLblSchoolName.Text = "School Name:";
            this.xrLblSchoolName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblSchoolMoto
            // 
            this.xrLblSchoolMoto.CanShrink = true;
            this.xrLblSchoolMoto.Dpi = 254F;
            this.xrLblSchoolMoto.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblSchoolMoto.LocationFloat = new DevExpress.Utils.PointFloat(474.0702F, 58.30515F);
            this.xrLblSchoolMoto.Multiline = true;
            this.xrLblSchoolMoto.Name = "xrLblSchoolMoto";
            this.xrLblSchoolMoto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblSchoolMoto.SizeF = new System.Drawing.SizeF(1149.034F, 47.61999F);
            this.xrLblSchoolMoto.StylePriority.UseFont = false;
            this.xrLblSchoolMoto.StylePriority.UseTextAlignment = false;
            this.xrLblSchoolMoto.Text = "School Moto";
            this.xrLblSchoolMoto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblSection
            // 
            this.xrLblSection.CanShrink = true;
            this.xrLblSection.Dpi = 254F;
            this.xrLblSection.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblSection.LocationFloat = new DevExpress.Utils.PointFloat(474.0702F, 165.5786F);
            this.xrLblSection.Multiline = true;
            this.xrLblSection.Name = "xrLblSection";
            this.xrLblSection.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblSection.SizeF = new System.Drawing.SizeF(1149.034F, 41.90501F);
            this.xrLblSection.StylePriority.UseFont = false;
            this.xrLblSection.StylePriority.UseTextAlignment = false;
            this.xrLblSection.Text = "Field";
            this.xrLblSection.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // reportListStudents
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.PageHeight = 2970;
            this.PageWidth = 2100;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLblSchoolName;
        private DevExpress.XtraReports.UI.XRLabel xrLblSchoolMoto;
        private DevExpress.XtraReports.UI.XRLabel xrLblSection;
        public DevExpress.XtraReports.UI.PrintableComponentContainer printableComponentContainer1;
        private DevExpress.XtraReports.UI.XRLabel xrlblClass;
        private DevExpress.XtraReports.UI.XRLabel xrlblSchoolYear;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureSchoolLogo;
    }
}
