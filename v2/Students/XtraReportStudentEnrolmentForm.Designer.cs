namespace EduXpress.Students
{
    partial class XtraReportStudentEnrolmentForm
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
            this.xrLblEnrolmentForm = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblSchoolMoto = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblTelephone = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLblSchoolName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPictureSchoolLogo = new DevExpress.XtraReports.UI.XRPictureBox();
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
            this.printableComponentContainer1.SizeF = new System.Drawing.SizeF(2799.29F, 190.5F);
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLblEnrolmentForm,
            this.xrLblDate,
            this.xrLblSchoolMoto,
            this.xrLblTelephone,
            this.xrLblSchoolName,
            this.xrPictureSchoolLogo});
            this.ReportHeader.Dpi = 254F;
            this.ReportHeader.HeightF = 390.4648F;
            this.ReportHeader.Name = "ReportHeader";
            this.ReportHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.ReportHeader_BeforePrint);
            // 
            // xrLblEnrolmentForm
            // 
            this.xrLblEnrolmentForm.CanShrink = true;
            this.xrLblEnrolmentForm.Dpi = 254F;
            this.xrLblEnrolmentForm.Font = new System.Drawing.Font("Arial", 14.25F);
            this.xrLblEnrolmentForm.LocationFloat = new DevExpress.Utils.PointFloat(995.9901F, 327.4207F);
            this.xrLblEnrolmentForm.Multiline = true;
            this.xrLblEnrolmentForm.Name = "xrLblEnrolmentForm";
            this.xrLblEnrolmentForm.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblEnrolmentForm.SizeF = new System.Drawing.SizeF(1828.3F, 59.05F);
            this.xrLblEnrolmentForm.StylePriority.UseFont = false;
            this.xrLblEnrolmentForm.StylePriority.UseTextAlignment = false;
            this.xrLblEnrolmentForm.Text = "Student Enrolment Form";
            this.xrLblEnrolmentForm.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblDate
            // 
            this.xrLblDate.CanShrink = true;
            this.xrLblDate.Dpi = 254F;
            this.xrLblDate.Font = new System.Drawing.Font("Arial", 14.25F);
            this.xrLblDate.LocationFloat = new DevExpress.Utils.PointFloat(995.9899F, 208.27F);
            this.xrLblDate.Multiline = true;
            this.xrLblDate.Name = "xrLblDate";
            this.xrLblDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblDate.SizeF = new System.Drawing.SizeF(1828.3F, 59.04995F);
            this.xrLblDate.StylePriority.UseFont = false;
            this.xrLblDate.StylePriority.UseTextAlignment = false;
            this.xrLblDate.Text = "Date:";
            this.xrLblDate.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblSchoolMoto
            // 
            this.xrLblSchoolMoto.CanShrink = true;
            this.xrLblSchoolMoto.Dpi = 254F;
            this.xrLblSchoolMoto.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblSchoolMoto.LocationFloat = new DevExpress.Utils.PointFloat(995.9899F, 90.16998F);
            this.xrLblSchoolMoto.Multiline = true;
            this.xrLblSchoolMoto.Name = "xrLblSchoolMoto";
            this.xrLblSchoolMoto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblSchoolMoto.SizeF = new System.Drawing.SizeF(1828.3F, 59.05F);
            this.xrLblSchoolMoto.StylePriority.UseFont = false;
            this.xrLblSchoolMoto.StylePriority.UseTextAlignment = false;
            this.xrLblSchoolMoto.Text = "School Moto";
            this.xrLblSchoolMoto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblTelephone
            // 
            this.xrLblTelephone.CanShrink = true;
            this.xrLblTelephone.Dpi = 254F;
            this.xrLblTelephone.Font = new System.Drawing.Font("Arial", 14.25F);
            this.xrLblTelephone.LocationFloat = new DevExpress.Utils.PointFloat(995.9899F, 149.22F);
            this.xrLblTelephone.Multiline = true;
            this.xrLblTelephone.Name = "xrLblTelephone";
            this.xrLblTelephone.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblTelephone.SizeF = new System.Drawing.SizeF(1828.3F, 59.05F);
            this.xrLblTelephone.StylePriority.UseFont = false;
            this.xrLblTelephone.StylePriority.UseTextAlignment = false;
            this.xrLblTelephone.Text = "Telephone:";
            this.xrLblTelephone.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrLblSchoolName
            // 
            this.xrLblSchoolName.CanShrink = true;
            this.xrLblSchoolName.Dpi = 254F;
            this.xrLblSchoolName.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLblSchoolName.LocationFloat = new DevExpress.Utils.PointFloat(995.99F, 0F);
            this.xrLblSchoolName.Multiline = true;
            this.xrLblSchoolName.Name = "xrLblSchoolName";
            this.xrLblSchoolName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLblSchoolName.SizeF = new System.Drawing.SizeF(1828.3F, 90.16998F);
            this.xrLblSchoolName.StylePriority.UseFont = false;
            this.xrLblSchoolName.StylePriority.UseTextAlignment = false;
            this.xrLblSchoolName.Text = "School Name:";
            this.xrLblSchoolName.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrPictureSchoolLogo
            // 
            this.xrPictureSchoolLogo.Dpi = 254F;
            this.xrPictureSchoolLogo.LocationFloat = new DevExpress.Utils.PointFloat(56.38F, 46.23F);
            this.xrPictureSchoolLogo.Name = "xrPictureSchoolLogo";
            this.xrPictureSchoolLogo.SizeF = new System.Drawing.SizeF(648.2292F, 340.2407F);
            this.xrPictureSchoolLogo.Sizing = DevExpress.XtraPrinting.ImageSizeMode.AutoSize;
            // 
            // XtraReportStudentEnrolmentForm
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.ReportHeader});
            this.Dpi = 254F;
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Landscape = true;
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.PageHeight = 2100;
            this.PageWidth = 2970;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 25F;
            this.Version = "20.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureSchoolLogo;
        private DevExpress.XtraReports.UI.XRLabel xrLblEnrolmentForm;
        private DevExpress.XtraReports.UI.XRLabel xrLblDate;
        private DevExpress.XtraReports.UI.XRLabel xrLblSchoolMoto;
        private DevExpress.XtraReports.UI.XRLabel xrLblTelephone;
        private DevExpress.XtraReports.UI.XRLabel xrLblSchoolName;
        public DevExpress.XtraReports.UI.PrintableComponentContainer printableComponentContainer1;
    }
}
