namespace EduXpress.Students
{
    partial class reportInvoice80mm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(reportInvoice80mm));
            DevExpress.DataAccess.Sql.CustomSqlQuery customSqlQuery1 = new DevExpress.DataAccess.Sql.CustomSqlQuery();
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.sqlDataSource1 = new DevExpress.DataAccess.Sql.SqlDataSource(this.components);
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.SubBand1 = new DevExpress.XtraReports.UI.SubBand();
            this.lblTel = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCompanyName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCompanyMotto = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelCompanyAddress = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine3 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabelStudentNo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelSurname = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelName = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelClass = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelPaymentDate = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabelMonth = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelDescription = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine4 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabelDiscount = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelFine = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelPreviousDue = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelTotalDue = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelTotalPaid = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabelBalance = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabelCashier = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel5 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrBarCode1 = new DevExpress.XtraReports.UI.XRBarCode();
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
            resources.ApplyResources(this.Detail, "Detail");
            this.Detail.KeepTogether = true;
            this.Detail.Name = "Detail";
            // 
            // sqlDataSource1
            // 
            this.sqlDataSource1.ConnectionName = "EduXpress.Properties.Settings.Default.DatabaseConnection";
            this.sqlDataSource1.Name = "sqlDataSource1";
            customSqlQuery1.Name = "CompanyProfile";
            customSqlQuery1.Sql = resources.GetString("customSqlQuery1.Sql");
            this.sqlDataSource1.Queries.AddRange(new DevExpress.DataAccess.Sql.SqlQuery[] {
            customSqlQuery1});
            this.sqlDataSource1.ResultSchemaSerializable = resources.GetString("sqlDataSource1.ResultSchemaSerializable");
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1});
            resources.ApplyResources(this.PageHeader, "PageHeader");
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.StylePriority.UseTextAlignment = false;
            this.PageHeader.SubBands.AddRange(new DevExpress.XtraReports.UI.SubBand[] {
            this.SubBand1});
            this.PageHeader.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.PageHeader_BeforePrint);
            // 
            // xrPictureBox1
            // 
            resources.ApplyResources(this.xrPictureBox1, "xrPictureBox1");
            this.xrPictureBox1.ImageAlignment = DevExpress.XtraPrinting.ImageAlignment.TopCenter;
            this.xrPictureBox1.Name = "xrPictureBox1";
            // 
            // SubBand1
            // 
            this.SubBand1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTel,
            this.xrLabelCompanyName,
            this.xrLabelCompanyMotto,
            this.xrLabelCompanyAddress,
            this.xrLine3,
            this.xrLabelStudentNo,
            this.xrLabelSurname,
            this.xrLabelName,
            this.xrLabelClass,
            this.xrLabelPaymentDate,
            this.xrLine2,
            this.xrLabelMonth,
            this.xrLabelDescription,
            this.xrLine4,
            this.xrLabelDiscount,
            this.xrLabelFine,
            this.xrLabelPreviousDue,
            this.xrLabelTotalDue,
            this.xrLabelTotalPaid,
            this.xrLabelBalance,
            this.xrLine1,
            this.xrLabelCashier,
            this.xrLabel5,
            this.xrBarCode1});
            resources.ApplyResources(this.SubBand1, "SubBand1");
            this.SubBand1.Name = "SubBand1";
            // 
            // lblTel
            // 
            resources.ApplyResources(this.lblTel, "lblTel");
            this.lblTel.Multiline = true;
            this.lblTel.Name = "lblTel";
            this.lblTel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblTel.StylePriority.UseFont = false;
            // 
            // xrLabelCompanyName
            // 
            this.xrLabelCompanyName.AutoWidth = true;
            this.xrLabelCompanyName.CanShrink = true;
            resources.ApplyResources(this.xrLabelCompanyName, "xrLabelCompanyName");
            this.xrLabelCompanyName.Multiline = true;
            this.xrLabelCompanyName.Name = "xrLabelCompanyName";
            this.xrLabelCompanyName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelCompanyName.SnapLineMargin = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 2, 2, 254F);
            this.xrLabelCompanyName.StylePriority.UseFont = false;
            this.xrLabelCompanyName.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelCompanyMotto
            // 
            this.xrLabelCompanyMotto.AutoWidth = true;
            this.xrLabelCompanyMotto.CanShrink = true;
            resources.ApplyResources(this.xrLabelCompanyMotto, "xrLabelCompanyMotto");
            this.xrLabelCompanyMotto.Multiline = true;
            this.xrLabelCompanyMotto.Name = "xrLabelCompanyMotto";
            this.xrLabelCompanyMotto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelCompanyMotto.StylePriority.UseFont = false;
            this.xrLabelCompanyMotto.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelCompanyAddress
            // 
            this.xrLabelCompanyAddress.CanShrink = true;
            resources.ApplyResources(this.xrLabelCompanyAddress, "xrLabelCompanyAddress");
            this.xrLabelCompanyAddress.Multiline = true;
            this.xrLabelCompanyAddress.Name = "xrLabelCompanyAddress";
            this.xrLabelCompanyAddress.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelCompanyAddress.StylePriority.UseFont = false;
            this.xrLabelCompanyAddress.StylePriority.UseTextAlignment = false;
            // 
            // xrLine3
            // 
            resources.ApplyResources(this.xrLine3, "xrLine3");
            this.xrLine3.Name = "xrLine3";
            // 
            // xrLabelStudentNo
            // 
            this.xrLabelStudentNo.CanShrink = true;
            resources.ApplyResources(this.xrLabelStudentNo, "xrLabelStudentNo");
            this.xrLabelStudentNo.Multiline = true;
            this.xrLabelStudentNo.Name = "xrLabelStudentNo";
            this.xrLabelStudentNo.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelStudentNo.StylePriority.UseFont = false;
            this.xrLabelStudentNo.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelSurname
            // 
            this.xrLabelSurname.CanShrink = true;
            resources.ApplyResources(this.xrLabelSurname, "xrLabelSurname");
            this.xrLabelSurname.Multiline = true;
            this.xrLabelSurname.Name = "xrLabelSurname";
            this.xrLabelSurname.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelSurname.StylePriority.UseFont = false;
            this.xrLabelSurname.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelName
            // 
            this.xrLabelName.CanShrink = true;
            resources.ApplyResources(this.xrLabelName, "xrLabelName");
            this.xrLabelName.Multiline = true;
            this.xrLabelName.Name = "xrLabelName";
            this.xrLabelName.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelName.StylePriority.UseFont = false;
            this.xrLabelName.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelClass
            // 
            this.xrLabelClass.CanShrink = true;
            resources.ApplyResources(this.xrLabelClass, "xrLabelClass");
            this.xrLabelClass.Multiline = true;
            this.xrLabelClass.Name = "xrLabelClass";
            this.xrLabelClass.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelClass.StylePriority.UseFont = false;
            this.xrLabelClass.StylePriority.UsePadding = false;
            this.xrLabelClass.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelPaymentDate
            // 
            this.xrLabelPaymentDate.CanShrink = true;
            resources.ApplyResources(this.xrLabelPaymentDate, "xrLabelPaymentDate");
            this.xrLabelPaymentDate.Multiline = true;
            this.xrLabelPaymentDate.Name = "xrLabelPaymentDate";
            this.xrLabelPaymentDate.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 254F);
            this.xrLabelPaymentDate.StylePriority.UseFont = false;
            this.xrLabelPaymentDate.StylePriority.UsePadding = false;
            this.xrLabelPaymentDate.StylePriority.UseTextAlignment = false;
            // 
            // xrLine2
            // 
            resources.ApplyResources(this.xrLine2, "xrLine2");
            this.xrLine2.Name = "xrLine2";
            // 
            // xrLabelMonth
            // 
            this.xrLabelMonth.CanShrink = true;
            resources.ApplyResources(this.xrLabelMonth, "xrLabelMonth");
            this.xrLabelMonth.Multiline = true;
            this.xrLabelMonth.Name = "xrLabelMonth";
            this.xrLabelMonth.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelMonth.StylePriority.UseFont = false;
            this.xrLabelMonth.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelDescription
            // 
            this.xrLabelDescription.CanShrink = true;
            resources.ApplyResources(this.xrLabelDescription, "xrLabelDescription");
            this.xrLabelDescription.Multiline = true;
            this.xrLabelDescription.Name = "xrLabelDescription";
            this.xrLabelDescription.Padding = new DevExpress.XtraPrinting.PaddingInfo(10, 10, 0, 0, 254F);
            this.xrLabelDescription.StylePriority.UseFont = false;
            this.xrLabelDescription.StylePriority.UsePadding = false;
            this.xrLabelDescription.StylePriority.UseTextAlignment = false;
            // 
            // xrLine4
            // 
            resources.ApplyResources(this.xrLine4, "xrLine4");
            this.xrLine4.Name = "xrLine4";
            // 
            // xrLabelDiscount
            // 
            this.xrLabelDiscount.CanShrink = true;
            resources.ApplyResources(this.xrLabelDiscount, "xrLabelDiscount");
            this.xrLabelDiscount.Multiline = true;
            this.xrLabelDiscount.Name = "xrLabelDiscount";
            this.xrLabelDiscount.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelDiscount.StylePriority.UseFont = false;
            this.xrLabelDiscount.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelFine
            // 
            this.xrLabelFine.CanShrink = true;
            resources.ApplyResources(this.xrLabelFine, "xrLabelFine");
            this.xrLabelFine.Multiline = true;
            this.xrLabelFine.Name = "xrLabelFine";
            this.xrLabelFine.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelFine.StylePriority.UseFont = false;
            this.xrLabelFine.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelPreviousDue
            // 
            this.xrLabelPreviousDue.CanShrink = true;
            resources.ApplyResources(this.xrLabelPreviousDue, "xrLabelPreviousDue");
            this.xrLabelPreviousDue.Multiline = true;
            this.xrLabelPreviousDue.Name = "xrLabelPreviousDue";
            this.xrLabelPreviousDue.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelPreviousDue.StylePriority.UseFont = false;
            this.xrLabelPreviousDue.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelTotalDue
            // 
            this.xrLabelTotalDue.CanShrink = true;
            resources.ApplyResources(this.xrLabelTotalDue, "xrLabelTotalDue");
            this.xrLabelTotalDue.Multiline = true;
            this.xrLabelTotalDue.Name = "xrLabelTotalDue";
            this.xrLabelTotalDue.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelTotalDue.StylePriority.UseFont = false;
            this.xrLabelTotalDue.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelTotalPaid
            // 
            this.xrLabelTotalPaid.CanShrink = true;
            resources.ApplyResources(this.xrLabelTotalPaid, "xrLabelTotalPaid");
            this.xrLabelTotalPaid.Multiline = true;
            this.xrLabelTotalPaid.Name = "xrLabelTotalPaid";
            this.xrLabelTotalPaid.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelTotalPaid.StylePriority.UseFont = false;
            this.xrLabelTotalPaid.StylePriority.UseTextAlignment = false;
            // 
            // xrLabelBalance
            // 
            this.xrLabelBalance.CanShrink = true;
            resources.ApplyResources(this.xrLabelBalance, "xrLabelBalance");
            this.xrLabelBalance.Multiline = true;
            this.xrLabelBalance.Name = "xrLabelBalance";
            this.xrLabelBalance.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelBalance.StylePriority.UseFont = false;
            this.xrLabelBalance.StylePriority.UseTextAlignment = false;
            // 
            // xrLine1
            // 
            resources.ApplyResources(this.xrLine1, "xrLine1");
            this.xrLine1.Name = "xrLine1";
            // 
            // xrLabelCashier
            // 
            this.xrLabelCashier.CanShrink = true;
            resources.ApplyResources(this.xrLabelCashier, "xrLabelCashier");
            this.xrLabelCashier.Multiline = true;
            this.xrLabelCashier.Name = "xrLabelCashier";
            this.xrLabelCashier.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabelCashier.StylePriority.UseFont = false;
            this.xrLabelCashier.StylePriority.UseTextAlignment = false;
            // 
            // xrLabel5
            // 
            this.xrLabel5.CanShrink = true;
            resources.ApplyResources(this.xrLabel5, "xrLabel5");
            this.xrLabel5.Multiline = true;
            this.xrLabel5.Name = "xrLabel5";
            this.xrLabel5.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel5.StylePriority.UseFont = false;
            this.xrLabel5.StylePriority.UseTextAlignment = false;
            // 
            // xrBarCode1
            // 
            this.xrBarCode1.Alignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            resources.ApplyResources(this.xrBarCode1, "xrBarCode1");
            this.xrBarCode1.Module = 3F;
            this.xrBarCode1.Name = "xrBarCode1";
            this.xrBarCode1.Padding = new DevExpress.XtraPrinting.PaddingInfo(26, 26, 0, 0, 254F);
            this.xrBarCode1.StylePriority.UseFont = false;
            this.xrBarCode1.StylePriority.UseTextAlignment = false;
            this.xrBarCode1.Symbology = code128Generator1;
            // 
            // reportInvoice80mm
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail,
            this.PageHeader});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.sqlDataSource1});
            this.DataSource = this.sqlDataSource1;
            this.DefaultPrinterSettingsUsing.UsePaperKind = true;
            resources.ApplyResources(this, "$this");
            this.Version = "20.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.DataAccess.Sql.SqlDataSource sqlDataSource1;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCompanyName;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCompanyMotto;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCompanyAddress;
        private DevExpress.XtraReports.UI.XRLine xrLine3;
        private DevExpress.XtraReports.UI.XRLabel xrLabelStudentNo;
        private DevExpress.XtraReports.UI.XRLabel xrLabelSurname;
        private DevExpress.XtraReports.UI.XRLabel xrLabelName;
        private DevExpress.XtraReports.UI.XRLabel xrLabelClass;
        private DevExpress.XtraReports.UI.XRLabel xrLabelPaymentDate;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLabelMonth;
        private DevExpress.XtraReports.UI.XRLabel xrLabelDescription;
        private DevExpress.XtraReports.UI.XRLine xrLine4;
        private DevExpress.XtraReports.UI.XRLabel xrLabelDiscount;
        private DevExpress.XtraReports.UI.XRLabel xrLabelFine;
        private DevExpress.XtraReports.UI.XRLabel xrLabelPreviousDue;
        private DevExpress.XtraReports.UI.XRLabel xrLabelTotalDue;
        private DevExpress.XtraReports.UI.XRLabel xrLabelTotalPaid;
        private DevExpress.XtraReports.UI.XRLabel xrLabelBalance;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel xrLabelCashier;
        private DevExpress.XtraReports.UI.XRLabel xrLabel5;
        private DevExpress.XtraReports.UI.XRBarCode xrBarCode1;
        private DevExpress.XtraReports.UI.XRLabel lblTel;
        private DevExpress.XtraReports.UI.SubBand SubBand1;
    }
}
