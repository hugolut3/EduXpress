namespace EduXpress.Reports
{
    partial class reportFeeDue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reportFeeDue));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.printableComponentContainer1 = new DevExpress.XtraReports.UI.PrintableComponentContainer();
            this.xrLblSchoolName = new DevExpress.XtraReports.UI.XRLabel();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.xrLblFeesDue = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblFeePeriod = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblSchoolClass = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblSchoolYear = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureSchoolLogo = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.xrLblFooterText = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            resources.ApplyResources(this.TopMargin, "TopMargin");
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            resources.ApplyResources(this.BottomMargin, "BottomMargin");
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.printableComponentContainer1});
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.HierarchyPrintOptions.Indent = 50.8F;
            this.Detail.Name = "Detail";
            // 
            // printableComponentContainer1
            // 
            resources.ApplyResources(this.printableComponentContainer1, "printableComponentContainer1");
            this.printableComponentContainer1.Name = "printableComponentContainer1";
            // 
            // xrLblSchoolName
            // 
            this.xrLblSchoolName.CanShrink = true;
            resources.ApplyResources(this.xrLblSchoolName, "xrLblSchoolName");
            this.xrLblSchoolName.Multiline = true;
            this.xrLblSchoolName.Name = "xrLblSchoolName";
            this.xrLblSchoolName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblSchoolName.StylePriority.UseFont = false;
            this.xrLblSchoolName.StylePriority.UseTextAlignment = false;
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLblFeesDue,
            this.xrLblFeePeriod,
            this.xrLblSchoolClass,
            this.xrLblSchoolYear,
            this.xrPictureSchoolLogo,
            this.xrLblSchoolName});
            resources.ApplyResources(this.ReportHeader, "ReportHeader");
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
            // 
            // xrLblFeesDue
            // 
            this.xrLblFeesDue.CanShrink = true;
            resources.ApplyResources(this.xrLblFeesDue, "xrLblFeesDue");
            this.xrLblFeesDue.Multiline = true;
            this.xrLblFeesDue.Name = "xrLblFeesDue";
            this.xrLblFeesDue.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblFeesDue.StylePriority.UseFont = false;
            this.xrLblFeesDue.StylePriority.UseTextAlignment = false;
            // 
            // xrLblFeePeriod
            // 
            this.xrLblFeePeriod.CanShrink = true;
            resources.ApplyResources(this.xrLblFeePeriod, "xrLblFeePeriod");
            this.xrLblFeePeriod.Multiline = true;
            this.xrLblFeePeriod.Name = "xrLblFeePeriod";
            this.xrLblFeePeriod.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblFeePeriod.StylePriority.UseFont = false;
            this.xrLblFeePeriod.StylePriority.UseTextAlignment = false;
            // 
            // xrLblSchoolClass
            // 
            this.xrLblSchoolClass.CanShrink = true;
            resources.ApplyResources(this.xrLblSchoolClass, "xrLblSchoolClass");
            this.xrLblSchoolClass.Multiline = true;
            this.xrLblSchoolClass.Name = "xrLblSchoolClass";
            this.xrLblSchoolClass.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblSchoolClass.StylePriority.UseFont = false;
            this.xrLblSchoolClass.StylePriority.UseTextAlignment = false;
            // 
            // xrLblSchoolYear
            // 
            this.xrLblSchoolYear.CanShrink = true;
            resources.ApplyResources(this.xrLblSchoolYear, "xrLblSchoolYear");
            this.xrLblSchoolYear.Multiline = true;
            this.xrLblSchoolYear.Name = "xrLblSchoolYear";
            this.xrLblSchoolYear.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblSchoolYear.StylePriority.UseFont = false;
            this.xrLblSchoolYear.StylePriority.UseTextAlignment = false;
            // 
            // xrPictureSchoolLogo
            // 
            resources.ApplyResources(this.xrPictureSchoolLogo, "xrPictureSchoolLogo");
            this.xrPictureSchoolLogo.Name = "xrPictureSchoolLogo";
            this.xrPictureSchoolLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLblFooterText});
            resources.ApplyResources(this.ReportFooter, "ReportFooter");
            this.ReportFooter.Name = "ReportFooter";
            // 
            // xrLblFooterText
            // 
            this.xrLblFooterText.CanShrink = true;
            resources.ApplyResources(this.xrLblFooterText, "xrLblFooterText");
            this.xrLblFooterText.Multiline = true;
            this.xrLblFooterText.Name = "xrLblFooterText";
            this.xrLblFooterText.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblFooterText.StylePriority.UseFont = false;
            this.xrLblFooterText.StylePriority.UseTextAlignment = false;
            // 
            // reportFeeDue
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader,
            this.ReportFooter});
            resources.ApplyResources(this, "$this");
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLblSchoolName;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        public DevExpress.XtraReports.UI.PrintableComponentContainer printableComponentContainer1;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLblFeePeriod;
        private DevExpress.XtraReports.UI.XRLabel xrLblSchoolClass;
        private DevExpress.XtraReports.UI.XRLabel xrLblSchoolYear;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureSchoolLogo;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel xrLblFooterText;
        private DevExpress.XtraReports.UI.XRLabel xrLblFeesDue;
    }
}
