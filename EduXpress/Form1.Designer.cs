namespace EduXpress
{
    partial class Form1
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
            DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.SplashScreen1), true, true, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ribbonControlMain = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barBtnMenuLogin = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnMenuLogout = new DevExpress.XtraBars.BarButtonItem();
            this.barBtnMenuExit = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticUser = new DevExpress.XtraBars.BarStaticItem();
            this.btnExitMain = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticServer = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            this.btnBinduWebsite = new DevExpress.XtraBars.BarButtonItem();
            this.btnAbout = new DevExpress.XtraBars.BarButtonItem();
            this.btnContactUs = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItemLicenseStatus = new DevExpress.XtraBars.BarStaticItem();
            this.ribbonPageFile = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupSecurity = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupExit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageHelp = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupAbout = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBarMain = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.ribbonLogoHelper1 = new DevExpress.XtraBars.Ribbon.RibbonLogoHelper();
            this.navigationFrameMain = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navPageMainDashboard = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navFrameDashboard = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navPageDashboardTime = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.sidePanelTime = new DevExpress.XtraEditors.SidePanel();
            this.gaugeControl1 = new DevExpress.XtraGauges.Win.GaugeControl();
            this.Clock = new DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge();
            this.arcScaleBackgroundLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent();
            this.scaleHours = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleEffectLayerComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleEffectLayerComponent();
            this.arcScaleNeedleComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.arcScaleNeedleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.arcScaleComponent2 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleNeedleComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent();
            this.arcScaleComponent3 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent();
            this.arcScaleSpindleCapComponent1 = new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent();
            this.sidePanelCalendar = new DevExpress.XtraEditors.SidePanel();
            this.dateNavigatorDashboard = new DevExpress.XtraScheduler.DateNavigator();
            this.navPageDashboardStudents = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlDashboardStudents1 = new EduXpress.Dashboard.userControlDashboardStudents();
            this.navPageDashboardFeesCollected = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navPageMainStudents = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navPageMainReports = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navFrameReports = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navPageReportsLogs = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlReportsLogs1 = new EduXpress.Reports.userControlReportsLogs();
            this.navPageReportsListStudents = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlListStudents1 = new EduXpress.Reports.userControlListStudents();
            this.navPageReportsFeePayment = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlFeePaymentRecord1 = new EduXpress.Reports.userControlFeePaymentRecord();
            this.navPageReportsSMS = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlReportsSMS1 = new EduXpress.Reports.userControlReportsSMS();
            this.navPageReportsFeeDue = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlReportsFeeDue1 = new EduXpress.Reports.userControlReportsFeeDue();
            this.navPageMainSettings = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navFrameSettings = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navPageSettingsSMS = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlSMSsettings1 = new EduXpress.Settings.userControlSMSsettings();
            this.navPageSettingsEmail = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlEmailSettings1 = new EduXpress.Settings.userControlEmailSettings();
            this.navPageSettingsPreferences = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlPreferences1 = new EduXpress.Settings.userControlPreferences();
            this.navPageSettingsSystemInfo = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlSystemInfo1 = new EduXpress.Settings.userControlSystemInfo();
            this.navPageSettingsPrinters = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlPrinterSettings1 = new EduXpress.Settings.userControlPrinterSettings();
            this.navPageMainOffice = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navFrameOffice = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navPageOfficeSpreadsheet = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlOfficeSpreadsheet1 = new EduXpress.Office.userControlOfficeSpreadsheet();
            this.navPageOfficeWord = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlOfficeWord1 = new EduXpress.Office.userControlOfficeWord();
            this.navPageOfficePDF = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlOfficePDF1 = new EduXpress.Office.userControlOfficePDF();
            this.navPageOfficeScheduling = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlOfficeScheduling1 = new EduXpress.Office.userControlOfficeScheduling();
            this.navPageOfficeEmail = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlOfficeEmail1 = new EduXpress.Office.userControlOfficeEmail();
            this.navPageOfficeSMS = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlOfficeSMS1 = new EduXpress.Office.userControlOfficeSMS();
            this.navPageMainAdmin = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.navigationFrameAdmin = new DevExpress.XtraBars.Navigation.NavigationFrame();
            this.navPageAdminSchoolDetails = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlSchoolDetails1 = new EduXpress.Admin.userControlSchoolDetails();
            this.navPageAdminEmployeeReg = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlEmployeeRegistration1 = new EduXpress.Admin.userControlEmployeeRegistration();
            this.navPageAdminAcademicYear = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlAcademicYear1 = new EduXpress.Admin.userControlAcademicYear();
            this.navPageAdminFeeEntry = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlClassFeeEntry1 = new EduXpress.Admin.userControlClassFeeEntry();
            this.navPageAdminClassSettings = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlClassSetting1 = new EduXpress.Admin.userControlClassSetting();
            this.navPageMainEducation = new DevExpress.XtraBars.Navigation.NavigationPage();
            this.userControlStudentEnrolmentForm1 = new EduXpress.Students.userControlStudentEnrolmentForm();
            this.navBarControlMain = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroupStudents = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarStudentsEnrolment = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarStudentsEnrolmentSA = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarStudentsFees = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemClassPromotion = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemStudentReports = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupDashboard = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemTime = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemStudents = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemFeesCollected = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupOffice = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemWordDocument = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemSpreadsheet = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemPdfViewer = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemScheduling = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemEmail = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemSendSMS = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupEducation = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemMarkSheet = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemSubjects = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemReportCards = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupTransport = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemBusInformation = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemStudentBusCardHolder = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemBusFeePayment = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupReports = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemLogs = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemListStudents = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemPaymentReports = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemSMSReports = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemFeeDue = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupAdministration = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemEmployeeRegistration = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemSchoolDetails = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemAcademicYear = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemClassFeesEntry = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemClassSetting = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarBackupRestoreDatabase = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroupSettings = new DevExpress.XtraNavBar.NavBarGroup();
            this.navBarItemPreferences = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemSMS = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemEmailServer = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemPrinters = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemConfigurationWizard = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemSystemInf = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarItemDashboardDesigner = new DevExpress.XtraNavBar.NavBarItem();
            this.officeNavigationBarMain = new DevExpress.XtraBars.Navigation.OfficeNavigationBar();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.splashScreenManagerWait = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::EduXpress.Dialogs.WaitForm1), true, true, true);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrameMain)).BeginInit();
            this.navigationFrameMain.SuspendLayout();
            this.navPageMainDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navFrameDashboard)).BeginInit();
            this.navFrameDashboard.SuspendLayout();
            this.navPageDashboardTime.SuspendLayout();
            this.sidePanelTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Clock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleHours)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleEffectLayerComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleSpindleCapComponent1)).BeginInit();
            this.sidePanelCalendar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorDashboard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorDashboard.CalendarTimeProperties)).BeginInit();
            this.navPageDashboardStudents.SuspendLayout();
            this.navPageMainReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navFrameReports)).BeginInit();
            this.navFrameReports.SuspendLayout();
            this.navPageReportsLogs.SuspendLayout();
            this.navPageReportsListStudents.SuspendLayout();
            this.navPageReportsFeePayment.SuspendLayout();
            this.navPageReportsSMS.SuspendLayout();
            this.navPageReportsFeeDue.SuspendLayout();
            this.navPageMainSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navFrameSettings)).BeginInit();
            this.navFrameSettings.SuspendLayout();
            this.navPageSettingsSMS.SuspendLayout();
            this.navPageSettingsEmail.SuspendLayout();
            this.navPageSettingsPreferences.SuspendLayout();
            this.navPageSettingsSystemInfo.SuspendLayout();
            this.navPageSettingsPrinters.SuspendLayout();
            this.navPageMainOffice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navFrameOffice)).BeginInit();
            this.navFrameOffice.SuspendLayout();
            this.navPageOfficeSpreadsheet.SuspendLayout();
            this.navPageOfficeWord.SuspendLayout();
            this.navPageOfficePDF.SuspendLayout();
            this.navPageOfficeScheduling.SuspendLayout();
            this.navPageOfficeEmail.SuspendLayout();
            this.navPageOfficeSMS.SuspendLayout();
            this.navPageMainAdmin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrameAdmin)).BeginInit();
            this.navigationFrameAdmin.SuspendLayout();
            this.navPageAdminSchoolDetails.SuspendLayout();
            this.navPageAdminEmployeeReg.SuspendLayout();
            this.navPageAdminAcademicYear.SuspendLayout();
            this.navPageAdminFeeEntry.SuspendLayout();
            this.navPageAdminClassSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeNavigationBarMain)).BeginInit();
            this.SuspendLayout();
            // 
            // splashScreenManager1
            // 
            splashScreenManager1.ClosingDelay = 500;
            // 
            // ribbonControlMain
            // 
            this.ribbonControlMain.ExpandCollapseItem.Id = 0;
            this.ribbonControlMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControlMain.ExpandCollapseItem,
            this.ribbonControlMain.SearchEditItem,
            this.barBtnMenuLogin,
            this.barBtnMenuLogout,
            this.barBtnMenuExit,
            this.barStaticUser,
            this.btnExitMain,
            this.barStaticServer,
            this.barStaticItem1,
            this.barButtonGroup1,
            this.btnBinduWebsite,
            this.btnAbout,
            this.btnContactUs,
            this.barStaticItemLicenseStatus});
            resources.ApplyResources(this.ribbonControlMain, "ribbonControlMain");
            this.ribbonControlMain.MaxItemId = 13;
            this.ribbonControlMain.Name = "ribbonControlMain";
            this.ribbonControlMain.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageFile,
            this.ribbonPageHelp});
            this.ribbonControlMain.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2019;
            this.ribbonControlMain.StatusBar = this.ribbonStatusBarMain;
            // 
            // barBtnMenuLogin
            // 
            resources.ApplyResources(this.barBtnMenuLogin, "barBtnMenuLogin");
            this.barBtnMenuLogin.Id = 1;
            this.barBtnMenuLogin.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_user;
            this.barBtnMenuLogin.Name = "barBtnMenuLogin";
            this.barBtnMenuLogin.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            this.barBtnMenuLogin.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnMenuLogin_ItemClick);
            // 
            // barBtnMenuLogout
            // 
            resources.ApplyResources(this.barBtnMenuLogout, "barBtnMenuLogout");
            this.barBtnMenuLogout.Id = 2;
            this.barBtnMenuLogout.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_security_permission;
            this.barBtnMenuLogout.Name = "barBtnMenuLogout";
            this.barBtnMenuLogout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barBtnMenuLogout_ItemClick);
            // 
            // barBtnMenuExit
            // 
            resources.ApplyResources(this.barBtnMenuExit, "barBtnMenuExit");
            this.barBtnMenuExit.Id = 3;
            this.barBtnMenuExit.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.barBtnMenuExit.Name = "barBtnMenuExit";
            // 
            // barStaticUser
            // 
            resources.ApplyResources(this.barStaticUser, "barStaticUser");
            this.barStaticUser.Id = 4;
            this.barStaticUser.Name = "barStaticUser";
            // 
            // btnExitMain
            // 
            resources.ApplyResources(this.btnExitMain, "btnExitMain");
            this.btnExitMain.Id = 5;
            this.btnExitMain.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.close;
            this.btnExitMain.Name = "btnExitMain";
            this.btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.btnExitMain.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnExitMain_ItemClick);
            // 
            // barStaticServer
            // 
            this.barStaticServer.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            resources.ApplyResources(this.barStaticServer, "barStaticServer");
            this.barStaticServer.Id = 6;
            this.barStaticServer.Name = "barStaticServer";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            resources.ApplyResources(this.barStaticItem1, "barStaticItem1");
            this.barStaticItem1.Id = 7;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barButtonGroup1
            // 
            this.barButtonGroup1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            resources.ApplyResources(this.barButtonGroup1, "barButtonGroup1");
            this.barButtonGroup1.Id = 8;
            this.barButtonGroup1.Name = "barButtonGroup1";
            // 
            // btnBinduWebsite
            // 
            resources.ApplyResources(this.btnBinduWebsite, "btnBinduWebsite");
            this.btnBinduWebsite.Id = 9;
            this.btnBinduWebsite.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_localization;
            this.btnBinduWebsite.Name = "btnBinduWebsite";
            this.btnBinduWebsite.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnBinduWebsite_ItemClick);
            // 
            // btnAbout
            // 
            resources.ApplyResources(this.btnAbout, "btnAbout");
            this.btnAbout.Id = 10;
            this.btnAbout.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.about;
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnAbout_ItemClick);
            // 
            // btnContactUs
            // 
            resources.ApplyResources(this.btnContactUs, "btnContactUs");
            this.btnContactUs.Id = 11;
            this.btnContactUs.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.glyph_mail;
            this.btnContactUs.Name = "btnContactUs";
            this.btnContactUs.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnContactUs_ItemClick);
            // 
            // barStaticItemLicenseStatus
            // 
            resources.ApplyResources(this.barStaticItemLicenseStatus, "barStaticItemLicenseStatus");
            this.barStaticItemLicenseStatus.Id = 12;
            this.barStaticItemLicenseStatus.Name = "barStaticItemLicenseStatus";
            this.barStaticItemLicenseStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // ribbonPageFile
            // 
            this.ribbonPageFile.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupSecurity,
            this.ribbonPageGroupExit});
            this.ribbonPageFile.MergeOrder = 1;
            this.ribbonPageFile.Name = "ribbonPageFile";
            resources.ApplyResources(this.ribbonPageFile, "ribbonPageFile");
            // 
            // ribbonPageGroupSecurity
            // 
            this.ribbonPageGroupSecurity.AllowTextClipping = false;
            this.ribbonPageGroupSecurity.ItemLinks.Add(this.barBtnMenuLogin);
            this.ribbonPageGroupSecurity.ItemLinks.Add(this.barBtnMenuLogout);
            this.ribbonPageGroupSecurity.Name = "ribbonPageGroupSecurity";
            resources.ApplyResources(this.ribbonPageGroupSecurity, "ribbonPageGroupSecurity");
            // 
            // ribbonPageGroupExit
            // 
            this.ribbonPageGroupExit.AllowTextClipping = false;
            this.ribbonPageGroupExit.ItemLinks.Add(this.btnExitMain);
            this.ribbonPageGroupExit.Name = "ribbonPageGroupExit";
            resources.ApplyResources(this.ribbonPageGroupExit, "ribbonPageGroupExit");
            // 
            // ribbonPageHelp
            // 
            this.ribbonPageHelp.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupAbout});
            this.ribbonPageHelp.Name = "ribbonPageHelp";
            resources.ApplyResources(this.ribbonPageHelp, "ribbonPageHelp");
            // 
            // ribbonPageGroupAbout
            // 
            this.ribbonPageGroupAbout.ItemLinks.Add(this.btnContactUs);
            this.ribbonPageGroupAbout.ItemLinks.Add(this.btnBinduWebsite);
            this.ribbonPageGroupAbout.ItemLinks.Add(this.btnAbout);
            this.ribbonPageGroupAbout.Name = "ribbonPageGroupAbout";
            resources.ApplyResources(this.ribbonPageGroupAbout, "ribbonPageGroupAbout");
            // 
            // ribbonStatusBarMain
            // 
            this.ribbonStatusBarMain.ItemLinks.Add(this.barStaticUser);
            this.ribbonStatusBarMain.ItemLinks.Add(this.barStaticItemLicenseStatus);
            resources.ApplyResources(this.ribbonStatusBarMain, "ribbonStatusBarMain");
            this.ribbonStatusBarMain.Name = "ribbonStatusBarMain";
            this.ribbonStatusBarMain.Ribbon = this.ribbonControlMain;
            // 
            // ribbonLogoHelper1
            // 
            this.ribbonLogoHelper1.Image = global::EduXpress.Properties.Resources.Bindu_Tech_Solutions_high_resolution_157x67;
            this.ribbonLogoHelper1.RibbonControl = this.ribbonControlMain;
            // 
            // navigationFrameMain
            // 
            this.navigationFrameMain.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.True;
            this.navigationFrameMain.Controls.Add(this.navPageMainDashboard);
            this.navigationFrameMain.Controls.Add(this.navPageMainStudents);
            this.navigationFrameMain.Controls.Add(this.navPageMainReports);
            this.navigationFrameMain.Controls.Add(this.navPageMainSettings);
            this.navigationFrameMain.Controls.Add(this.navPageMainOffice);
            this.navigationFrameMain.Controls.Add(this.navPageMainAdmin);
            this.navigationFrameMain.Controls.Add(this.navPageMainEducation);
            resources.ApplyResources(this.navigationFrameMain, "navigationFrameMain");
            this.navigationFrameMain.Name = "navigationFrameMain";
            this.navigationFrameMain.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navPageMainDashboard,
            this.navPageMainOffice,
            this.navPageMainStudents,
            this.navPageMainReports,
            this.navPageMainSettings,
            this.navPageMainAdmin,
            this.navPageMainEducation});
            this.navigationFrameMain.SelectedPage = this.navPageMainDashboard;
            this.navigationFrameMain.TransitionAnimationProperties.FrameInterval = 1500;
            // 
            // navPageMainDashboard
            // 
            this.navPageMainDashboard.Controls.Add(this.navFrameDashboard);
            this.navPageMainDashboard.Name = "navPageMainDashboard";
            resources.ApplyResources(this.navPageMainDashboard, "navPageMainDashboard");
            // 
            // navFrameDashboard
            // 
            this.navFrameDashboard.Controls.Add(this.navPageDashboardTime);
            this.navFrameDashboard.Controls.Add(this.navPageDashboardStudents);
            this.navFrameDashboard.Controls.Add(this.navPageDashboardFeesCollected);
            resources.ApplyResources(this.navFrameDashboard, "navFrameDashboard");
            this.navFrameDashboard.Name = "navFrameDashboard";
            this.navFrameDashboard.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navPageDashboardTime,
            this.navPageDashboardStudents,
            this.navPageDashboardFeesCollected});
            this.navFrameDashboard.SelectedPage = this.navPageDashboardTime;
            this.navFrameDashboard.TransitionAnimationProperties.FrameInterval = 3000;
            // 
            // navPageDashboardTime
            // 
            this.navPageDashboardTime.Controls.Add(this.sidePanelTime);
            this.navPageDashboardTime.Controls.Add(this.sidePanelCalendar);
            this.navPageDashboardTime.Name = "navPageDashboardTime";
            resources.ApplyResources(this.navPageDashboardTime, "navPageDashboardTime");
            // 
            // sidePanelTime
            // 
            this.sidePanelTime.Controls.Add(this.gaugeControl1);
            resources.ApplyResources(this.sidePanelTime, "sidePanelTime");
            this.sidePanelTime.Name = "sidePanelTime";
            // 
            // gaugeControl1
            // 
            this.gaugeControl1.AutoLayout = false;
            this.gaugeControl1.BackColor = System.Drawing.Color.Transparent;
            this.gaugeControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.gaugeControl1.Gauges.AddRange(new DevExpress.XtraGauges.Base.IGauge[] {
            this.Clock});
            resources.ApplyResources(this.gaugeControl1, "gaugeControl1");
            this.gaugeControl1.Name = "gaugeControl1";
            // 
            // Clock
            // 
            this.Clock.BackgroundLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent[] {
            this.arcScaleBackgroundLayerComponent1});
            this.Clock.Bounds = new System.Drawing.Rectangle(6, 6, 339, 316);
            this.Clock.EffectLayers.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleEffectLayerComponent[] {
            this.arcScaleEffectLayerComponent1});
            this.Clock.Name = "Clock";
            this.Clock.Needles.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent[] {
            this.arcScaleNeedleComponent1,
            this.arcScaleNeedleComponent2,
            this.arcScaleNeedleComponent3});
            this.Clock.Scales.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent[] {
            this.scaleHours,
            this.arcScaleComponent2,
            this.arcScaleComponent3});
            this.Clock.SpindleCaps.AddRange(new DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent[] {
            this.arcScaleSpindleCapComponent1});
            // 
            // arcScaleBackgroundLayerComponent1
            // 
            this.arcScaleBackgroundLayerComponent1.ArcScale = this.scaleHours;
            this.arcScaleBackgroundLayerComponent1.Name = "bg";
            this.arcScaleBackgroundLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.BackgroundLayerShapeType.CircularFull_Clock;
            this.arcScaleBackgroundLayerComponent1.ZOrder = 1000;
            // 
            // scaleHours
            // 
            this.scaleHours.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.scaleHours.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.scaleHours.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.scaleHours.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.scaleHours.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.scaleHours.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 125F);
            this.scaleHours.EndAngle = 270F;
            this.scaleHours.MajorTickCount = 13;
            this.scaleHours.MajorTickmark.FormatString = resources.GetString("scaleHours.MajorTickmark.FormatString");
            this.scaleHours.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Line;
            this.scaleHours.MajorTickmark.ShowFirst = false;
            this.scaleHours.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.scaleHours.MaxValue = 12F;
            this.scaleHours.MinorTickCount = 0;
            this.scaleHours.Name = "hours";
            this.scaleHours.RadiusX = 98F;
            this.scaleHours.RadiusY = 98F;
            this.scaleHours.Shader = new DevExpress.XtraGauges.Core.Drawing.GrayShader("");
            this.scaleHours.StartAngle = -90F;
            // 
            // arcScaleEffectLayerComponent1
            // 
            this.arcScaleEffectLayerComponent1.ArcScale = this.scaleHours;
            this.arcScaleEffectLayerComponent1.Name = "glass";
            this.arcScaleEffectLayerComponent1.ScaleCenterPos = new DevExpress.XtraGauges.Core.Base.PointF2D(0.5F, 1F);
            this.arcScaleEffectLayerComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.EffectLayerShapeType.CircularFull_Clock;
            this.arcScaleEffectLayerComponent1.Size = new System.Drawing.SizeF(196F, 98F);
            this.arcScaleEffectLayerComponent1.ZOrder = -1000;
            // 
            // arcScaleNeedleComponent1
            // 
            this.arcScaleNeedleComponent1.ArcScale = this.scaleHours;
            this.arcScaleNeedleComponent1.EndOffset = 10F;
            this.arcScaleNeedleComponent1.Name = "hourNeedle";
            this.arcScaleNeedleComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_ClockHour;
            this.arcScaleNeedleComponent1.ZOrder = -50;
            // 
            // arcScaleNeedleComponent2
            // 
            this.arcScaleNeedleComponent2.ArcScale = this.arcScaleComponent2;
            this.arcScaleNeedleComponent2.EndOffset = 10F;
            this.arcScaleNeedleComponent2.Name = "minuteNeedle";
            this.arcScaleNeedleComponent2.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_ClockMinute;
            this.arcScaleNeedleComponent2.ZOrder = -50;
            // 
            // arcScaleComponent2
            // 
            this.arcScaleComponent2.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent2.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 125F);
            this.arcScaleComponent2.EndAngle = 270F;
            this.arcScaleComponent2.MajorTickCount = 13;
            this.arcScaleComponent2.MajorTickmark.FormatString = resources.GetString("arcScaleComponent2.MajorTickmark.FormatString");
            this.arcScaleComponent2.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Diamond;
            this.arcScaleComponent2.MajorTickmark.ShowFirst = false;
            this.arcScaleComponent2.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent2.MaxValue = 12F;
            this.arcScaleComponent2.MinorTickCount = 0;
            this.arcScaleComponent2.Name = "minutes";
            this.arcScaleComponent2.RadiusX = 98F;
            this.arcScaleComponent2.RadiusY = 98F;
            this.arcScaleComponent2.StartAngle = -90F;
            this.arcScaleComponent2.Value = 2F;
            this.arcScaleComponent2.ZOrder = 1001;
            // 
            // arcScaleNeedleComponent3
            // 
            this.arcScaleNeedleComponent3.ArcScale = this.arcScaleComponent3;
            this.arcScaleNeedleComponent3.Name = "secondNeedle";
            this.arcScaleNeedleComponent3.ShapeType = DevExpress.XtraGauges.Core.Model.NeedleShapeType.CircularFull_ClockSecond;
            this.arcScaleNeedleComponent3.ZOrder = -50;
            // 
            // arcScaleComponent3
            // 
            this.arcScaleComponent3.AppearanceMajorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.AppearanceMajorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.AppearanceMinorTickmark.BorderBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.AppearanceMinorTickmark.ContentBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.AppearanceTickmarkText.TextBrush = new DevExpress.XtraGauges.Core.Drawing.SolidBrushObject("Color:White");
            this.arcScaleComponent3.Center = new DevExpress.XtraGauges.Core.Base.PointF2D(125F, 125F);
            this.arcScaleComponent3.EndAngle = 270F;
            this.arcScaleComponent3.MajorTickCount = 13;
            this.arcScaleComponent3.MajorTickmark.FormatString = resources.GetString("arcScaleComponent3.MajorTickmark.FormatString");
            this.arcScaleComponent3.MajorTickmark.ShapeType = DevExpress.XtraGauges.Core.Model.TickmarkShapeType.Diamond;
            this.arcScaleComponent3.MajorTickmark.ShowFirst = false;
            this.arcScaleComponent3.MajorTickmark.TextOrientation = DevExpress.XtraGauges.Core.Model.LabelOrientation.LeftToRight;
            this.arcScaleComponent3.MaxValue = 12F;
            this.arcScaleComponent3.MinorTickCount = 0;
            this.arcScaleComponent3.Name = "seconds";
            this.arcScaleComponent3.RadiusX = 98F;
            this.arcScaleComponent3.RadiusY = 98F;
            this.arcScaleComponent3.StartAngle = -90F;
            this.arcScaleComponent3.Value = 4F;
            this.arcScaleComponent3.ZOrder = 1001;
            // 
            // arcScaleSpindleCapComponent1
            // 
            this.arcScaleSpindleCapComponent1.ArcScale = this.scaleHours;
            this.arcScaleSpindleCapComponent1.Name = "cap";
            this.arcScaleSpindleCapComponent1.ShapeType = DevExpress.XtraGauges.Core.Model.SpindleCapShapeType.CircularFull_Clock;
            this.arcScaleSpindleCapComponent1.Size = new System.Drawing.SizeF(20F, 20F);
            this.arcScaleSpindleCapComponent1.ZOrder = -100;
            // 
            // sidePanelCalendar
            // 
            this.sidePanelCalendar.Controls.Add(this.dateNavigatorDashboard);
            resources.ApplyResources(this.sidePanelCalendar, "sidePanelCalendar");
            this.sidePanelCalendar.Name = "sidePanelCalendar";
            // 
            // dateNavigatorDashboard
            // 
            this.dateNavigatorDashboard.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dateNavigatorDashboard.Appearance.Options.UseBackColor = true;
            this.dateNavigatorDashboard.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.dateNavigatorDashboard.CalendarAppearance.DayCellSpecial.FontStyleDelta = ((System.Drawing.FontStyle)(resources.GetObject("dateNavigatorDashboard.CalendarAppearance.DayCellSpecial.FontStyleDelta")));
            this.dateNavigatorDashboard.CalendarAppearance.DayCellSpecial.Options.UseFont = true;
            this.dateNavigatorDashboard.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dateNavigatorDashboard.CalendarTimeProperties.Buttons"))))});
            this.dateNavigatorDashboard.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Fluent;
            this.dateNavigatorDashboard.CaseMonthNames = DevExpress.XtraEditors.Controls.TextCaseMode.SentenceCase;
            this.dateNavigatorDashboard.DateTime = new System.DateTime(((long)(0)));
            resources.ApplyResources(this.dateNavigatorDashboard, "dateNavigatorDashboard");
            this.dateNavigatorDashboard.FirstDayOfWeek = System.DayOfWeek.Sunday;
            this.dateNavigatorDashboard.Name = "dateNavigatorDashboard";
            // 
            // navPageDashboardStudents
            // 
            this.navPageDashboardStudents.Controls.Add(this.userControlDashboardStudents1);
            this.navPageDashboardStudents.Name = "navPageDashboardStudents";
            resources.ApplyResources(this.navPageDashboardStudents, "navPageDashboardStudents");
            // 
            // userControlDashboardStudents1
            // 
            resources.ApplyResources(this.userControlDashboardStudents1, "userControlDashboardStudents1");
            this.userControlDashboardStudents1.Name = "userControlDashboardStudents1";
            // 
            // navPageDashboardFeesCollected
            // 
            this.navPageDashboardFeesCollected.Name = "navPageDashboardFeesCollected";
            resources.ApplyResources(this.navPageDashboardFeesCollected, "navPageDashboardFeesCollected");
            // 
            // navPageMainStudents
            // 
            this.navPageMainStudents.Name = "navPageMainStudents";
            resources.ApplyResources(this.navPageMainStudents, "navPageMainStudents");
            // 
            // navPageMainReports
            // 
            this.navPageMainReports.Controls.Add(this.navFrameReports);
            this.navPageMainReports.Name = "navPageMainReports";
            resources.ApplyResources(this.navPageMainReports, "navPageMainReports");
            // 
            // navFrameReports
            // 
            this.navFrameReports.Controls.Add(this.navPageReportsLogs);
            this.navFrameReports.Controls.Add(this.navPageReportsListStudents);
            this.navFrameReports.Controls.Add(this.navPageReportsFeePayment);
            this.navFrameReports.Controls.Add(this.navPageReportsSMS);
            this.navFrameReports.Controls.Add(this.navPageReportsFeeDue);
            resources.ApplyResources(this.navFrameReports, "navFrameReports");
            this.navFrameReports.Name = "navFrameReports";
            this.navFrameReports.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navPageReportsLogs,
            this.navPageReportsListStudents,
            this.navPageReportsFeePayment,
            this.navPageReportsSMS,
            this.navPageReportsFeeDue});
            this.navFrameReports.SelectedPage = this.navPageReportsLogs;
            // 
            // navPageReportsLogs
            // 
            this.navPageReportsLogs.Controls.Add(this.userControlReportsLogs1);
            this.navPageReportsLogs.Name = "navPageReportsLogs";
            resources.ApplyResources(this.navPageReportsLogs, "navPageReportsLogs");
            // 
            // userControlReportsLogs1
            // 
            resources.ApplyResources(this.userControlReportsLogs1, "userControlReportsLogs1");
            this.userControlReportsLogs1.Name = "userControlReportsLogs1";
            // 
            // navPageReportsListStudents
            // 
            this.navPageReportsListStudents.Controls.Add(this.userControlListStudents1);
            this.navPageReportsListStudents.Name = "navPageReportsListStudents";
            resources.ApplyResources(this.navPageReportsListStudents, "navPageReportsListStudents");
            // 
            // userControlListStudents1
            // 
            resources.ApplyResources(this.userControlListStudents1, "userControlListStudents1");
            this.userControlListStudents1.Name = "userControlListStudents1";
            // 
            // navPageReportsFeePayment
            // 
            this.navPageReportsFeePayment.Controls.Add(this.userControlFeePaymentRecord1);
            this.navPageReportsFeePayment.Name = "navPageReportsFeePayment";
            resources.ApplyResources(this.navPageReportsFeePayment, "navPageReportsFeePayment");
            // 
            // userControlFeePaymentRecord1
            // 
            resources.ApplyResources(this.userControlFeePaymentRecord1, "userControlFeePaymentRecord1");
            this.userControlFeePaymentRecord1.Name = "userControlFeePaymentRecord1";
            // 
            // navPageReportsSMS
            // 
            this.navPageReportsSMS.Controls.Add(this.userControlReportsSMS1);
            this.navPageReportsSMS.Name = "navPageReportsSMS";
            resources.ApplyResources(this.navPageReportsSMS, "navPageReportsSMS");
            // 
            // userControlReportsSMS1
            // 
            resources.ApplyResources(this.userControlReportsSMS1, "userControlReportsSMS1");
            this.userControlReportsSMS1.Name = "userControlReportsSMS1";
            // 
            // navPageReportsFeeDue
            // 
            this.navPageReportsFeeDue.Controls.Add(this.userControlReportsFeeDue1);
            this.navPageReportsFeeDue.Name = "navPageReportsFeeDue";
            resources.ApplyResources(this.navPageReportsFeeDue, "navPageReportsFeeDue");
            // 
            // userControlReportsFeeDue1
            // 
            resources.ApplyResources(this.userControlReportsFeeDue1, "userControlReportsFeeDue1");
            this.userControlReportsFeeDue1.Name = "userControlReportsFeeDue1";
            // 
            // navPageMainSettings
            // 
            this.navPageMainSettings.Controls.Add(this.navFrameSettings);
            this.navPageMainSettings.Name = "navPageMainSettings";
            resources.ApplyResources(this.navPageMainSettings, "navPageMainSettings");
            // 
            // navFrameSettings
            // 
            this.navFrameSettings.Controls.Add(this.navPageSettingsSMS);
            this.navFrameSettings.Controls.Add(this.navPageSettingsEmail);
            this.navFrameSettings.Controls.Add(this.navPageSettingsPreferences);
            this.navFrameSettings.Controls.Add(this.navPageSettingsSystemInfo);
            this.navFrameSettings.Controls.Add(this.navPageSettingsPrinters);
            resources.ApplyResources(this.navFrameSettings, "navFrameSettings");
            this.navFrameSettings.Name = "navFrameSettings";
            this.navFrameSettings.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navPageSettingsSMS,
            this.navPageSettingsEmail,
            this.navPageSettingsPreferences,
            this.navPageSettingsSystemInfo,
            this.navPageSettingsPrinters});
            this.navFrameSettings.SelectedPage = this.navPageSettingsPreferences;
            // 
            // navPageSettingsSMS
            // 
            this.navPageSettingsSMS.Controls.Add(this.userControlSMSsettings1);
            this.navPageSettingsSMS.Name = "navPageSettingsSMS";
            resources.ApplyResources(this.navPageSettingsSMS, "navPageSettingsSMS");
            // 
            // userControlSMSsettings1
            // 
            resources.ApplyResources(this.userControlSMSsettings1, "userControlSMSsettings1");
            this.userControlSMSsettings1.Name = "userControlSMSsettings1";
            // 
            // navPageSettingsEmail
            // 
            this.navPageSettingsEmail.Controls.Add(this.userControlEmailSettings1);
            this.navPageSettingsEmail.Name = "navPageSettingsEmail";
            resources.ApplyResources(this.navPageSettingsEmail, "navPageSettingsEmail");
            // 
            // userControlEmailSettings1
            // 
            resources.ApplyResources(this.userControlEmailSettings1, "userControlEmailSettings1");
            this.userControlEmailSettings1.Name = "userControlEmailSettings1";
            // 
            // navPageSettingsPreferences
            // 
            this.navPageSettingsPreferences.Controls.Add(this.userControlPreferences1);
            this.navPageSettingsPreferences.Name = "navPageSettingsPreferences";
            resources.ApplyResources(this.navPageSettingsPreferences, "navPageSettingsPreferences");
            // 
            // userControlPreferences1
            // 
            resources.ApplyResources(this.userControlPreferences1, "userControlPreferences1");
            this.userControlPreferences1.Name = "userControlPreferences1";
            // 
            // navPageSettingsSystemInfo
            // 
            this.navPageSettingsSystemInfo.Controls.Add(this.userControlSystemInfo1);
            this.navPageSettingsSystemInfo.Name = "navPageSettingsSystemInfo";
            resources.ApplyResources(this.navPageSettingsSystemInfo, "navPageSettingsSystemInfo");
            // 
            // userControlSystemInfo1
            // 
            this.userControlSystemInfo1.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.userControlSystemInfo1, "userControlSystemInfo1");
            this.userControlSystemInfo1.Name = "userControlSystemInfo1";
            // 
            // navPageSettingsPrinters
            // 
            this.navPageSettingsPrinters.Controls.Add(this.userControlPrinterSettings1);
            this.navPageSettingsPrinters.Name = "navPageSettingsPrinters";
            resources.ApplyResources(this.navPageSettingsPrinters, "navPageSettingsPrinters");
            // 
            // userControlPrinterSettings1
            // 
            resources.ApplyResources(this.userControlPrinterSettings1, "userControlPrinterSettings1");
            this.userControlPrinterSettings1.Name = "userControlPrinterSettings1";
            // 
            // navPageMainOffice
            // 
            this.navPageMainOffice.Controls.Add(this.navFrameOffice);
            this.navPageMainOffice.Name = "navPageMainOffice";
            resources.ApplyResources(this.navPageMainOffice, "navPageMainOffice");
            // 
            // navFrameOffice
            // 
            this.navFrameOffice.Controls.Add(this.navPageOfficeSpreadsheet);
            this.navFrameOffice.Controls.Add(this.navPageOfficeWord);
            this.navFrameOffice.Controls.Add(this.navPageOfficePDF);
            this.navFrameOffice.Controls.Add(this.navPageOfficeScheduling);
            this.navFrameOffice.Controls.Add(this.navPageOfficeEmail);
            this.navFrameOffice.Controls.Add(this.navPageOfficeSMS);
            resources.ApplyResources(this.navFrameOffice, "navFrameOffice");
            this.navFrameOffice.Name = "navFrameOffice";
            this.navFrameOffice.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navPageOfficeWord,
            this.navPageOfficeSpreadsheet,
            this.navPageOfficePDF,
            this.navPageOfficeScheduling,
            this.navPageOfficeEmail,
            this.navPageOfficeSMS});
            this.navFrameOffice.SelectedPage = this.navPageOfficeWord;
            // 
            // navPageOfficeSpreadsheet
            // 
            this.navPageOfficeSpreadsheet.Controls.Add(this.userControlOfficeSpreadsheet1);
            this.navPageOfficeSpreadsheet.Name = "navPageOfficeSpreadsheet";
            resources.ApplyResources(this.navPageOfficeSpreadsheet, "navPageOfficeSpreadsheet");
            // 
            // userControlOfficeSpreadsheet1
            // 
            resources.ApplyResources(this.userControlOfficeSpreadsheet1, "userControlOfficeSpreadsheet1");
            this.userControlOfficeSpreadsheet1.Name = "userControlOfficeSpreadsheet1";
            // 
            // navPageOfficeWord
            // 
            this.navPageOfficeWord.Controls.Add(this.userControlOfficeWord1);
            this.navPageOfficeWord.Name = "navPageOfficeWord";
            resources.ApplyResources(this.navPageOfficeWord, "navPageOfficeWord");
            // 
            // userControlOfficeWord1
            // 
            resources.ApplyResources(this.userControlOfficeWord1, "userControlOfficeWord1");
            this.userControlOfficeWord1.Name = "userControlOfficeWord1";
            // 
            // navPageOfficePDF
            // 
            this.navPageOfficePDF.Controls.Add(this.userControlOfficePDF1);
            this.navPageOfficePDF.Name = "navPageOfficePDF";
            resources.ApplyResources(this.navPageOfficePDF, "navPageOfficePDF");
            // 
            // userControlOfficePDF1
            // 
            resources.ApplyResources(this.userControlOfficePDF1, "userControlOfficePDF1");
            this.userControlOfficePDF1.Name = "userControlOfficePDF1";
            // 
            // navPageOfficeScheduling
            // 
            this.navPageOfficeScheduling.Controls.Add(this.userControlOfficeScheduling1);
            this.navPageOfficeScheduling.Name = "navPageOfficeScheduling";
            resources.ApplyResources(this.navPageOfficeScheduling, "navPageOfficeScheduling");
            // 
            // userControlOfficeScheduling1
            // 
            resources.ApplyResources(this.userControlOfficeScheduling1, "userControlOfficeScheduling1");
            this.userControlOfficeScheduling1.Name = "userControlOfficeScheduling1";
            // 
            // navPageOfficeEmail
            // 
            this.navPageOfficeEmail.Controls.Add(this.userControlOfficeEmail1);
            this.navPageOfficeEmail.Name = "navPageOfficeEmail";
            resources.ApplyResources(this.navPageOfficeEmail, "navPageOfficeEmail");
            // 
            // userControlOfficeEmail1
            // 
            resources.ApplyResources(this.userControlOfficeEmail1, "userControlOfficeEmail1");
            this.userControlOfficeEmail1.Name = "userControlOfficeEmail1";
            // 
            // navPageOfficeSMS
            // 
            this.navPageOfficeSMS.Controls.Add(this.userControlOfficeSMS1);
            this.navPageOfficeSMS.Name = "navPageOfficeSMS";
            resources.ApplyResources(this.navPageOfficeSMS, "navPageOfficeSMS");
            // 
            // userControlOfficeSMS1
            // 
            resources.ApplyResources(this.userControlOfficeSMS1, "userControlOfficeSMS1");
            this.userControlOfficeSMS1.Name = "userControlOfficeSMS1";
            // 
            // navPageMainAdmin
            // 
            this.navPageMainAdmin.Controls.Add(this.navigationFrameAdmin);
            this.navPageMainAdmin.Name = "navPageMainAdmin";
            resources.ApplyResources(this.navPageMainAdmin, "navPageMainAdmin");
            // 
            // navigationFrameAdmin
            // 
            this.navigationFrameAdmin.Controls.Add(this.navPageAdminSchoolDetails);
            this.navigationFrameAdmin.Controls.Add(this.navPageAdminEmployeeReg);
            this.navigationFrameAdmin.Controls.Add(this.navPageAdminAcademicYear);
            this.navigationFrameAdmin.Controls.Add(this.navPageAdminFeeEntry);
            this.navigationFrameAdmin.Controls.Add(this.navPageAdminClassSettings);
            resources.ApplyResources(this.navigationFrameAdmin, "navigationFrameAdmin");
            this.navigationFrameAdmin.Name = "navigationFrameAdmin";
            this.navigationFrameAdmin.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] {
            this.navPageAdminEmployeeReg,
            this.navPageAdminSchoolDetails,
            this.navPageAdminAcademicYear,
            this.navPageAdminFeeEntry,
            this.navPageAdminClassSettings});
            this.navigationFrameAdmin.SelectedPage = this.navPageAdminEmployeeReg;
            // 
            // navPageAdminSchoolDetails
            // 
            this.navPageAdminSchoolDetails.Controls.Add(this.userControlSchoolDetails1);
            this.navPageAdminSchoolDetails.Name = "navPageAdminSchoolDetails";
            resources.ApplyResources(this.navPageAdminSchoolDetails, "navPageAdminSchoolDetails");
            // 
            // userControlSchoolDetails1
            // 
            resources.ApplyResources(this.userControlSchoolDetails1, "userControlSchoolDetails1");
            this.userControlSchoolDetails1.Name = "userControlSchoolDetails1";
            // 
            // navPageAdminEmployeeReg
            // 
            this.navPageAdminEmployeeReg.Controls.Add(this.userControlEmployeeRegistration1);
            this.navPageAdminEmployeeReg.Name = "navPageAdminEmployeeReg";
            resources.ApplyResources(this.navPageAdminEmployeeReg, "navPageAdminEmployeeReg");
            // 
            // userControlEmployeeRegistration1
            // 
            resources.ApplyResources(this.userControlEmployeeRegistration1, "userControlEmployeeRegistration1");
            this.userControlEmployeeRegistration1.Name = "userControlEmployeeRegistration1";
            // 
            // navPageAdminAcademicYear
            // 
            this.navPageAdminAcademicYear.Controls.Add(this.userControlAcademicYear1);
            this.navPageAdminAcademicYear.Name = "navPageAdminAcademicYear";
            resources.ApplyResources(this.navPageAdminAcademicYear, "navPageAdminAcademicYear");
            // 
            // userControlAcademicYear1
            // 
            resources.ApplyResources(this.userControlAcademicYear1, "userControlAcademicYear1");
            this.userControlAcademicYear1.Name = "userControlAcademicYear1";
            // 
            // navPageAdminFeeEntry
            // 
            this.navPageAdminFeeEntry.Controls.Add(this.userControlClassFeeEntry1);
            this.navPageAdminFeeEntry.Name = "navPageAdminFeeEntry";
            resources.ApplyResources(this.navPageAdminFeeEntry, "navPageAdminFeeEntry");
            // 
            // userControlClassFeeEntry1
            // 
            resources.ApplyResources(this.userControlClassFeeEntry1, "userControlClassFeeEntry1");
            this.userControlClassFeeEntry1.Name = "userControlClassFeeEntry1";
            // 
            // navPageAdminClassSettings
            // 
            this.navPageAdminClassSettings.Controls.Add(this.userControlClassSetting1);
            this.navPageAdminClassSettings.Name = "navPageAdminClassSettings";
            resources.ApplyResources(this.navPageAdminClassSettings, "navPageAdminClassSettings");
            // 
            // userControlClassSetting1
            // 
            resources.ApplyResources(this.userControlClassSetting1, "userControlClassSetting1");
            this.userControlClassSetting1.Name = "userControlClassSetting1";
            // 
            // navPageMainEducation
            // 
            this.navPageMainEducation.Name = "navPageMainEducation";
            resources.ApplyResources(this.navPageMainEducation, "navPageMainEducation");
            // 
            // userControlStudentEnrolmentForm1
            // 
            resources.ApplyResources(this.userControlStudentEnrolmentForm1, "userControlStudentEnrolmentForm1");
            this.userControlStudentEnrolmentForm1.MainRibbon = null;
            this.userControlStudentEnrolmentForm1.MainStatusBar = null;
            this.userControlStudentEnrolmentForm1.Name = "userControlStudentEnrolmentForm1";
            // 
            // navBarControlMain
            // 
            this.navBarControlMain.ActiveGroup = this.navBarGroupDashboard;
            resources.ApplyResources(this.navBarControlMain, "navBarControlMain");
            this.navBarControlMain.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroupDashboard,
            this.navBarGroupOffice,
            this.navBarGroupStudents,
            this.navBarGroupEducation,
            this.navBarGroupTransport,
            this.navBarGroupReports,
            this.navBarGroupAdministration,
            this.navBarGroupSettings});
            this.navBarControlMain.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.navBarItemTime,
            this.navBarItemStudents,
            this.navBarItemWordDocument,
            this.navBarItemSpreadsheet,
            this.navBarItemPdfViewer,
            this.navBarItemScheduling,
            this.navBarStudentsEnrolment,
            this.navBarItemEmail,
            this.navBarItemLogs,
            this.navBarStudentsFees,
            this.navBarItemSMS,
            this.navBarItemEmailServer,
            this.navBarItemPreferences,
            this.navBarItemEmployeeRegistration,
            this.navBarItemSchoolDetails,
            this.navBarItemAcademicYear,
            this.navBarItemClassFeesEntry,
            this.navBarItemClassSetting,
            this.navBarItemListStudents,
            this.navBarItemPaymentReports,
            this.navBarItemSystemInf,
            this.navBarItemPrinters,
            this.navBarItemClassPromotion,
            this.navBarItemConfigurationWizard,
            this.navBarItemSendSMS,
            this.navBarItemSMSReports,
            this.navBarItemFeeDue,
            this.navBarBackupRestoreDatabase,
            this.navBarItemBusInformation,
            this.navBarItemStudentBusCardHolder,
            this.navBarItemBusFeePayment,
            this.navBarItemFeesCollected,
            this.navBarItemDashboardDesigner,
            this.navBarStudentsEnrolmentSA,
            this.navBarItemStudentReports,
            this.navBarItemSubjects,
            this.navBarItemMarkSheet,
            this.navBarItemReportCards});
            this.navBarControlMain.Name = "navBarControlMain";
            this.navBarControlMain.OptionsNavPane.ExpandedWidth = ((int)(resources.GetObject("resource.ExpandedWidth")));
            this.navBarControlMain.PaintStyleKind = DevExpress.XtraNavBar.NavBarViewKind.NavigationPane;
            this.navBarControlMain.ActiveGroupChanged += new DevExpress.XtraNavBar.NavBarGroupEventHandler(this.navBarControlMain_ActiveGroupChanged);
            // 
            // navBarGroupStudents
            // 
            resources.ApplyResources(this.navBarGroupStudents, "navBarGroupStudents");
            this.navBarGroupStudents.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupStudents.ImageOptions.SmallImageSize = new System.Drawing.Size(16, 16);
            this.navBarGroupStudents.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.reviewers;
            this.navBarGroupStudents.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarStudentsEnrolment),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarStudentsEnrolmentSA),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarStudentsFees),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemClassPromotion),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemStudentReports)});
            this.navBarGroupStudents.Name = "navBarGroupStudents";
            this.navBarGroupStudents.TopVisibleLinkIndex = 2;
            this.navBarGroupStudents.Visible = false;
            // 
            // navBarStudentsEnrolment
            // 
            resources.ApplyResources(this.navBarStudentsEnrolment, "navBarStudentsEnrolment");
            this.navBarStudentsEnrolment.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.assigntask;
            this.navBarStudentsEnrolment.Name = "navBarStudentsEnrolment";
            this.navBarStudentsEnrolment.Visible = false;
            this.navBarStudentsEnrolment.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarStudentsEnrolment_LinkClicked);
            // 
            // navBarStudentsEnrolmentSA
            // 
            resources.ApplyResources(this.navBarStudentsEnrolmentSA, "navBarStudentsEnrolmentSA");
            this.navBarStudentsEnrolmentSA.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.assigntask;
            this.navBarStudentsEnrolmentSA.Name = "navBarStudentsEnrolmentSA";
            this.navBarStudentsEnrolmentSA.Visible = false;
            // 
            // navBarStudentsFees
            // 
            resources.ApplyResources(this.navBarStudentsFees, "navBarStudentsFees");
            this.navBarStudentsFees.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.business_dollar;
            this.navBarStudentsFees.Name = "navBarStudentsFees";
            this.navBarStudentsFees.Visible = false;
            this.navBarStudentsFees.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarStudentsFees_LinkClicked);
            // 
            // navBarItemClassPromotion
            // 
            resources.ApplyResources(this.navBarItemClassPromotion, "navBarItemClassPromotion");
            this.navBarItemClassPromotion.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.productquickcomparisons;
            this.navBarItemClassPromotion.Name = "navBarItemClassPromotion";
            this.navBarItemClassPromotion.Visible = false;
            this.navBarItemClassPromotion.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemClassPromotion_LinkClicked);
            // 
            // navBarItemStudentReports
            // 
            resources.ApplyResources(this.navBarItemStudentReports, "navBarItemStudentReports");
            this.navBarItemStudentReports.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.functionsstatistical;
            this.navBarItemStudentReports.Name = "navBarItemStudentReports";
            this.navBarItemStudentReports.Visible = false;
            // 
            // navBarGroupDashboard
            // 
            resources.ApplyResources(this.navBarGroupDashboard, "navBarGroupDashboard");
            this.navBarGroupDashboard.Expanded = true;
            this.navBarGroupDashboard.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupDashboard.ImageOptions.SmallImageSize = new System.Drawing.Size(16, 16);
            this.navBarGroupDashboard.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.createpie3dchart;
            this.navBarGroupDashboard.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemTime),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemStudents),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemFeesCollected)});
            this.navBarGroupDashboard.Name = "navBarGroupDashboard";
            // 
            // navBarItemTime
            // 
            resources.ApplyResources(this.navBarItemTime, "navBarItemTime");
            this.navBarItemTime.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.switchtimescalesto;
            this.navBarItemTime.Name = "navBarItemTime";
            this.navBarItemTime.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemTime_LinkClicked);
            // 
            // navBarItemStudents
            // 
            resources.ApplyResources(this.navBarItemStudents, "navBarItemStudents");
            this.navBarItemStudents.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.doctor;
            this.navBarItemStudents.Name = "navBarItemStudents";
            this.navBarItemStudents.Visible = false;
            this.navBarItemStudents.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemStudents_LinkClicked);
            // 
            // navBarItemFeesCollected
            // 
            resources.ApplyResources(this.navBarItemFeesCollected, "navBarItemFeesCollected");
            this.navBarItemFeesCollected.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.financial;
            this.navBarItemFeesCollected.Name = "navBarItemFeesCollected";
            this.navBarItemFeesCollected.Visible = false;
            this.navBarItemFeesCollected.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemFeesCollected_LinkClicked);
            // 
            // navBarGroupOffice
            // 
            resources.ApplyResources(this.navBarGroupOffice, "navBarGroupOffice");
            this.navBarGroupOffice.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupOffice.ImageOptions.SmallImageSize = new System.Drawing.Size(16, 16);
            this.navBarGroupOffice.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.textalignment0;
            this.navBarGroupOffice.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemWordDocument),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSpreadsheet),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemPdfViewer),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemScheduling),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemEmail),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSendSMS)});
            this.navBarGroupOffice.Name = "navBarGroupOffice";
            // 
            // navBarItemWordDocument
            // 
            resources.ApplyResources(this.navBarItemWordDocument, "navBarItemWordDocument");
            this.navBarItemWordDocument.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.inserttextbox;
            this.navBarItemWordDocument.Name = "navBarItemWordDocument";
            this.navBarItemWordDocument.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemWordDocument_LinkClicked);
            // 
            // navBarItemSpreadsheet
            // 
            resources.ApplyResources(this.navBarItemSpreadsheet, "navBarItemSpreadsheet");
            this.navBarItemSpreadsheet.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.above_average;
            this.navBarItemSpreadsheet.Name = "navBarItemSpreadsheet";
            this.navBarItemSpreadsheet.Visible = false;
            this.navBarItemSpreadsheet.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemSpreadsheet_LinkClicked);
            // 
            // navBarItemPdfViewer
            // 
            resources.ApplyResources(this.navBarItemPdfViewer, "navBarItemPdfViewer");
            this.navBarItemPdfViewer.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.documentpdf;
            this.navBarItemPdfViewer.Name = "navBarItemPdfViewer";
            this.navBarItemPdfViewer.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemPdfViewer_LinkClicked);
            // 
            // navBarItemScheduling
            // 
            resources.ApplyResources(this.navBarItemScheduling, "navBarItemScheduling");
            this.navBarItemScheduling.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.longdate;
            this.navBarItemScheduling.Name = "navBarItemScheduling";
            this.navBarItemScheduling.Visible = false;
            this.navBarItemScheduling.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemScheduling_LinkClicked);
            // 
            // navBarItemEmail
            // 
            resources.ApplyResources(this.navBarItemEmail, "navBarItemEmail");
            this.navBarItemEmail.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.forward;
            this.navBarItemEmail.Name = "navBarItemEmail";
            this.navBarItemEmail.Visible = false;
            this.navBarItemEmail.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemEmail_LinkClicked);
            // 
            // navBarItemSendSMS
            // 
            resources.ApplyResources(this.navBarItemSendSMS, "navBarItemSendSMS");
            this.navBarItemSendSMS.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.electronics_phoneandroid;
            this.navBarItemSendSMS.Name = "navBarItemSendSMS";
            this.navBarItemSendSMS.Visible = false;
            this.navBarItemSendSMS.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemSendSMS_LinkClicked);
            // 
            // navBarGroupEducation
            // 
            resources.ApplyResources(this.navBarGroupEducation, "navBarGroupEducation");
            this.navBarGroupEducation.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupEducation.ImageOptions.SmallImageSize = new System.Drawing.Size(16, 16);
            this.navBarGroupEducation.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.doctor;
            this.navBarGroupEducation.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemMarkSheet),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSubjects),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemReportCards)});
            this.navBarGroupEducation.Name = "navBarGroupEducation";
            this.navBarGroupEducation.Visible = false;
            // 
            // navBarItemMarkSheet
            // 
            resources.ApplyResources(this.navBarItemMarkSheet, "navBarItemMarkSheet");
            this.navBarItemMarkSheet.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_document;
            this.navBarItemMarkSheet.Name = "navBarItemMarkSheet";
            // 
            // navBarItemSubjects
            // 
            resources.ApplyResources(this.navBarItemSubjects, "navBarItemSubjects");
            this.navBarItemSubjects.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.multipledocuments;
            this.navBarItemSubjects.Name = "navBarItemSubjects";
            // 
            // navBarItemReportCards
            // 
            resources.ApplyResources(this.navBarItemReportCards, "navBarItemReportCards");
            this.navBarItemReportCards.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_list;
            this.navBarItemReportCards.Name = "navBarItemReportCards";
            // 
            // navBarGroupTransport
            // 
            resources.ApplyResources(this.navBarGroupTransport, "navBarGroupTransport");
            this.navBarGroupTransport.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupTransport.ImageOptions.SmallImageSize = new System.Drawing.Size(16, 16);
            this.navBarGroupTransport.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.travel_bus;
            this.navBarGroupTransport.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemBusInformation),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemStudentBusCardHolder),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemBusFeePayment)});
            this.navBarGroupTransport.Name = "navBarGroupTransport";
            this.navBarGroupTransport.Visible = false;
            // 
            // navBarItemBusInformation
            // 
            resources.ApplyResources(this.navBarItemBusInformation, "navBarItemBusInformation");
            this.navBarItemBusInformation.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.travel_bus;
            this.navBarItemBusInformation.Name = "navBarItemBusInformation";
            // 
            // navBarItemStudentBusCardHolder
            // 
            resources.ApplyResources(this.navBarItemStudentBusCardHolder, "navBarItemStudentBusCardHolder");
            this.navBarItemStudentBusCardHolder.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.card;
            this.navBarItemStudentBusCardHolder.Name = "navBarItemStudentBusCardHolder";
            // 
            // navBarItemBusFeePayment
            // 
            resources.ApplyResources(this.navBarItemBusFeePayment, "navBarItemBusFeePayment");
            this.navBarItemBusFeePayment.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.business_creditcard;
            this.navBarItemBusFeePayment.Name = "navBarItemBusFeePayment";
            // 
            // navBarGroupReports
            // 
            resources.ApplyResources(this.navBarGroupReports, "navBarGroupReports");
            this.navBarGroupReports.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupReports.ImageOptions.SmallImageSize = new System.Drawing.Size(16, 16);
            this.navBarGroupReports.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.functionsstatistical;
            this.navBarGroupReports.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemLogs),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemListStudents),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemPaymentReports),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSMSReports),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemFeeDue)});
            this.navBarGroupReports.Name = "navBarGroupReports";
            this.navBarGroupReports.TopVisibleLinkIndex = 2;
            this.navBarGroupReports.Visible = false;
            // 
            // navBarItemLogs
            // 
            resources.ApplyResources(this.navBarItemLogs, "navBarItemLogs");
            this.navBarItemLogs.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("navBarItemLogs.ImageOptions.SvgImage")));
            this.navBarItemLogs.Name = "navBarItemLogs";
            this.navBarItemLogs.Visible = false;
            this.navBarItemLogs.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemLogs_LinkClicked);
            // 
            // navBarItemListStudents
            // 
            resources.ApplyResources(this.navBarItemListStudents, "navBarItemListStudents");
            this.navBarItemListStudents.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.doctor;
            this.navBarItemListStudents.Name = "navBarItemListStudents";
            this.navBarItemListStudents.Visible = false;
            this.navBarItemListStudents.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemListStudents_LinkClicked);
            // 
            // navBarItemPaymentReports
            // 
            resources.ApplyResources(this.navBarItemPaymentReports, "navBarItemPaymentReports");
            this.navBarItemPaymentReports.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.financial;
            this.navBarItemPaymentReports.Name = "navBarItemPaymentReports";
            this.navBarItemPaymentReports.Visible = false;
            this.navBarItemPaymentReports.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemPaymentReports_LinkClicked);
            // 
            // navBarItemSMSReports
            // 
            resources.ApplyResources(this.navBarItemSMSReports, "navBarItemSMSReports");
            this.navBarItemSMSReports.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.electronics_phoneandroid;
            this.navBarItemSMSReports.Name = "navBarItemSMSReports";
            this.navBarItemSMSReports.Visible = false;
            this.navBarItemSMSReports.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemSMSReports_LinkClicked);
            // 
            // navBarItemFeeDue
            // 
            resources.ApplyResources(this.navBarItemFeeDue, "navBarItemFeeDue");
            this.navBarItemFeeDue.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.business_dollar;
            this.navBarItemFeeDue.Name = "navBarItemFeeDue";
            this.navBarItemFeeDue.Visible = false;
            this.navBarItemFeeDue.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemFeeDue_LinkClicked);
            // 
            // navBarGroupAdministration
            // 
            resources.ApplyResources(this.navBarGroupAdministration, "navBarGroupAdministration");
            this.navBarGroupAdministration.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupAdministration.ImageOptions.SmallImageSize = new System.Drawing.Size(16, 16);
            this.navBarGroupAdministration.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_lead;
            this.navBarGroupAdministration.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemEmployeeRegistration),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSchoolDetails),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemAcademicYear),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemClassFeesEntry),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemClassSetting),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarBackupRestoreDatabase)});
            this.navBarGroupAdministration.Name = "navBarGroupAdministration";
            this.navBarGroupAdministration.TopVisibleLinkIndex = 1;
            this.navBarGroupAdministration.Visible = false;
            // 
            // navBarItemEmployeeRegistration
            // 
            resources.ApplyResources(this.navBarItemEmployeeRegistration, "navBarItemEmployeeRegistration");
            this.navBarItemEmployeeRegistration.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.employeeprofile;
            this.navBarItemEmployeeRegistration.Name = "navBarItemEmployeeRegistration";
            this.navBarItemEmployeeRegistration.Visible = false;
            this.navBarItemEmployeeRegistration.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemEmployeeRegistration_LinkClicked);
            // 
            // navBarItemSchoolDetails
            // 
            resources.ApplyResources(this.navBarItemSchoolDetails, "navBarItemSchoolDetails");
            this.navBarItemSchoolDetails.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.travel_hotel;
            this.navBarItemSchoolDetails.Name = "navBarItemSchoolDetails";
            this.navBarItemSchoolDetails.Visible = false;
            this.navBarItemSchoolDetails.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemSchoolDetails_LinkClicked);
            // 
            // navBarItemAcademicYear
            // 
            resources.ApplyResources(this.navBarItemAcademicYear, "navBarItemAcademicYear");
            this.navBarItemAcademicYear.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.longdate;
            this.navBarItemAcademicYear.Name = "navBarItemAcademicYear";
            this.navBarItemAcademicYear.Visible = false;
            this.navBarItemAcademicYear.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemAcademicYear_LinkClicked);
            // 
            // navBarItemClassFeesEntry
            // 
            resources.ApplyResources(this.navBarItemClassFeesEntry, "navBarItemClassFeesEntry");
            this.navBarItemClassFeesEntry.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.business_dollarcircled;
            this.navBarItemClassFeesEntry.Name = "navBarItemClassFeesEntry";
            this.navBarItemClassFeesEntry.Visible = false;
            this.navBarItemClassFeesEntry.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemClassFeesEntry_LinkClicked);
            // 
            // navBarItemClassSetting
            // 
            resources.ApplyResources(this.navBarItemClassSetting, "navBarItemClassSetting");
            this.navBarItemClassSetting.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.bo_position;
            this.navBarItemClassSetting.Name = "navBarItemClassSetting";
            this.navBarItemClassSetting.Visible = false;
            this.navBarItemClassSetting.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemClassSetting_LinkClicked);
            // 
            // navBarBackupRestoreDatabase
            // 
            resources.ApplyResources(this.navBarBackupRestoreDatabase, "navBarBackupRestoreDatabase");
            this.navBarBackupRestoreDatabase.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.editconnection;
            this.navBarBackupRestoreDatabase.Name = "navBarBackupRestoreDatabase";
            this.navBarBackupRestoreDatabase.Visible = false;
            this.navBarBackupRestoreDatabase.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarBackupRestoreDatabase_LinkClicked);
            // 
            // navBarGroupSettings
            // 
            resources.ApplyResources(this.navBarGroupSettings, "navBarGroupSettings");
            this.navBarGroupSettings.GroupStyle = DevExpress.XtraNavBar.NavBarGroupStyle.LargeIconsList;
            this.navBarGroupSettings.ImageOptions.SmallImageSize = new System.Drawing.Size(16, 16);
            this.navBarGroupSettings.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.viewsettings;
            this.navBarGroupSettings.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemPreferences),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSMS),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemEmailServer),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemPrinters),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemConfigurationWizard),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemSystemInf),
            new DevExpress.XtraNavBar.NavBarItemLink(this.navBarItemDashboardDesigner)});
            this.navBarGroupSettings.Name = "navBarGroupSettings";
            // 
            // navBarItemPreferences
            // 
            resources.ApplyResources(this.navBarItemPreferences, "navBarItemPreferences");
            this.navBarItemPreferences.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.localcolors;
            this.navBarItemPreferences.Name = "navBarItemPreferences";
            this.navBarItemPreferences.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemPreferences_LinkClicked);
            // 
            // navBarItemSMS
            // 
            resources.ApplyResources(this.navBarItemSMS, "navBarItemSMS");
            this.navBarItemSMS.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.electronics_phoneandroid;
            this.navBarItemSMS.Name = "navBarItemSMS";
            this.navBarItemSMS.Visible = false;
            this.navBarItemSMS.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemSMS_LinkClicked);
            // 
            // navBarItemEmailServer
            // 
            resources.ApplyResources(this.navBarItemEmailServer, "navBarItemEmailServer");
            this.navBarItemEmailServer.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.mailmerge;
            this.navBarItemEmailServer.Name = "navBarItemEmailServer";
            this.navBarItemEmailServer.Visible = false;
            this.navBarItemEmailServer.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemEmailServer_LinkClicked);
            // 
            // navBarItemPrinters
            // 
            resources.ApplyResources(this.navBarItemPrinters, "navBarItemPrinters");
            this.navBarItemPrinters.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.print;
            this.navBarItemPrinters.Name = "navBarItemPrinters";
            this.navBarItemPrinters.Visible = false;
            this.navBarItemPrinters.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemPrinters_LinkClicked);
            // 
            // navBarItemConfigurationWizard
            // 
            resources.ApplyResources(this.navBarItemConfigurationWizard, "navBarItemConfigurationWizard");
            this.navBarItemConfigurationWizard.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.charttype_stepline3d;
            this.navBarItemConfigurationWizard.Name = "navBarItemConfigurationWizard";
            this.navBarItemConfigurationWizard.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemConfigurationWizard_LinkClicked);
            // 
            // navBarItemSystemInf
            // 
            resources.ApplyResources(this.navBarItemSystemInf, "navBarItemSystemInf");
            this.navBarItemSystemInf.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.electronics_desktopmac;
            this.navBarItemSystemInf.Name = "navBarItemSystemInf";
            this.navBarItemSystemInf.Visible = false;
            this.navBarItemSystemInf.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemSystemInf_LinkClicked);
            // 
            // navBarItemDashboardDesigner
            // 
            resources.ApplyResources(this.navBarItemDashboardDesigner, "navBarItemDashboardDesigner");
            this.navBarItemDashboardDesigner.ImageOptions.SvgImage = global::EduXpress.Properties.Resources.createpie3dchart;
            this.navBarItemDashboardDesigner.Name = "navBarItemDashboardDesigner";
            this.navBarItemDashboardDesigner.Visible = false;
            this.navBarItemDashboardDesigner.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.navBarItemDashboardDesigner_LinkClicked);
            // 
            // officeNavigationBarMain
            // 
            resources.ApplyResources(this.officeNavigationBarMain, "officeNavigationBarMain");
            this.officeNavigationBarMain.Name = "officeNavigationBarMain";
            this.officeNavigationBarMain.NavigationClient = this.navBarControlMain;
            // 
            // timerClock
            // 
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // splashScreenManagerWait
            // 
            this.splashScreenManagerWait.ClosingDelay = 500;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.navigationFrameMain);
            this.Controls.Add(this.officeNavigationBarMain);
            this.Controls.Add(this.navBarControlMain);
            this.Controls.Add(this.ribbonStatusBarMain);
            this.Controls.Add(this.ribbonControlMain);
            this.IconOptions.Icon = ((System.Drawing.Icon)(resources.GetObject("Form1.IconOptions.Icon")));
            this.Name = "Form1";
            this.Ribbon = this.ribbonControlMain;
            this.StatusBar = this.ribbonStatusBarMain;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrameMain)).EndInit();
            this.navigationFrameMain.ResumeLayout(false);
            this.navPageMainDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navFrameDashboard)).EndInit();
            this.navFrameDashboard.ResumeLayout(false);
            this.navPageDashboardTime.ResumeLayout(false);
            this.sidePanelTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Clock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleBackgroundLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scaleHours)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleEffectLayerComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleNeedleComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleComponent3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arcScaleSpindleCapComponent1)).EndInit();
            this.sidePanelCalendar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorDashboard.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateNavigatorDashboard)).EndInit();
            this.navPageDashboardStudents.ResumeLayout(false);
            this.navPageMainReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navFrameReports)).EndInit();
            this.navFrameReports.ResumeLayout(false);
            this.navPageReportsLogs.ResumeLayout(false);
            this.navPageReportsListStudents.ResumeLayout(false);
            this.navPageReportsFeePayment.ResumeLayout(false);
            this.navPageReportsSMS.ResumeLayout(false);
            this.navPageReportsFeeDue.ResumeLayout(false);
            this.navPageMainSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navFrameSettings)).EndInit();
            this.navFrameSettings.ResumeLayout(false);
            this.navPageSettingsSMS.ResumeLayout(false);
            this.navPageSettingsEmail.ResumeLayout(false);
            this.navPageSettingsPreferences.ResumeLayout(false);
            this.navPageSettingsSystemInfo.ResumeLayout(false);
            this.navPageSettingsPrinters.ResumeLayout(false);
            this.navPageMainOffice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navFrameOffice)).EndInit();
            this.navFrameOffice.ResumeLayout(false);
            this.navPageOfficeSpreadsheet.ResumeLayout(false);
            this.navPageOfficeWord.ResumeLayout(false);
            this.navPageOfficePDF.ResumeLayout(false);
            this.navPageOfficeScheduling.ResumeLayout(false);
            this.navPageOfficeEmail.ResumeLayout(false);
            this.navPageOfficeSMS.ResumeLayout(false);
            this.navPageMainAdmin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navigationFrameAdmin)).EndInit();
            this.navigationFrameAdmin.ResumeLayout(false);
            this.navPageAdminSchoolDetails.ResumeLayout(false);
            this.navPageAdminEmployeeReg.ResumeLayout(false);
            this.navPageAdminAcademicYear.ResumeLayout(false);
            this.navPageAdminFeeEntry.ResumeLayout(false);
            this.navPageAdminClassSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControlMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.officeNavigationBarMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControlMain;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageFile;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSecurity;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBarMain;
        private DevExpress.XtraBars.BarButtonItem barBtnMenuLogin;
        private DevExpress.XtraBars.BarButtonItem barBtnMenuLogout;
        private DevExpress.XtraBars.BarButtonItem barBtnMenuExit;
        private DevExpress.XtraBars.Ribbon.RibbonLogoHelper ribbonLogoHelper1;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrameMain;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageMainDashboard;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageMainOffice;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageMainStudents;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageMainReports;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageMainSettings;
        private DevExpress.XtraNavBar.NavBarControl navBarControlMain;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupDashboard;
        private DevExpress.XtraBars.Navigation.OfficeNavigationBar officeNavigationBarMain;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupOffice;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupStudents;
        private DevExpress.XtraNavBar.NavBarItem navBarItemTime;
        private DevExpress.XtraNavBar.NavBarItem navBarItemStudents;
        private DevExpress.XtraBars.Navigation.NavigationFrame navFrameDashboard;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageDashboardTime;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageDashboardStudents;
        private DevExpress.XtraEditors.SidePanel sidePanelCalendar;
        private DevExpress.XtraEditors.SidePanel sidePanelTime;
        private DevExpress.XtraGauges.Win.GaugeControl gaugeControl1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.CircularGauge Clock;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleBackgroundLayerComponent arcScaleBackgroundLayerComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent scaleHours;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleEffectLayerComponent arcScaleEffectLayerComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent1;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent2;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleNeedleComponent arcScaleNeedleComponent3;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleComponent arcScaleComponent3;
        private DevExpress.XtraGauges.Win.Gauges.Circular.ArcScaleSpindleCapComponent arcScaleSpindleCapComponent1;
        private System.Windows.Forms.Timer timerClock;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagerWaitForm;
        private DevExpress.XtraScheduler.DateNavigator dateNavigatorDashboard;
        private DevExpress.XtraNavBar.NavBarItem navBarItemWordDocument;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSpreadsheet;
        private DevExpress.XtraNavBar.NavBarItem navBarItemPdfViewer;
        private DevExpress.XtraNavBar.NavBarItem navBarItemScheduling;
        private DevExpress.XtraBars.Navigation.NavigationFrame navFrameOffice;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageOfficeWord;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageOfficeSpreadsheet;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageOfficePDF;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageOfficeScheduling;
        private Office.userControlOfficePDF userControlOfficePDF1;
        private Office.userControlOfficeWord userControlOfficeWord1;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagerWait;
        private Office.userControlOfficeSpreadsheet userControlOfficeSpreadsheet1;
        private Office.userControlOfficeScheduling userControlOfficeScheduling1;
        private DevExpress.XtraBars.BarStaticItem barStaticUser;
        private DevExpress.XtraNavBar.NavBarItem navBarStudentsEnrolment;
        private DevExpress.XtraNavBar.NavBarItem navBarItemEmail;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageOfficeEmail;
        private Office.userControlOfficeEmail userControlOfficeEmail1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupReports;
        private DevExpress.XtraNavBar.NavBarItem navBarItemLogs;
        private DevExpress.XtraBars.Navigation.NavigationFrame navFrameReports;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageReportsLogs;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageReportsListStudents;
        private DevExpress.XtraNavBar.NavBarItem navBarStudentsFees;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupSettings;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSMS;
        private DevExpress.XtraNavBar.NavBarItem navBarItemEmailServer;
        private DevExpress.XtraNavBar.NavBarItem navBarItemPreferences;
        private DevExpress.XtraBars.Navigation.NavigationFrame navFrameSettings;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageSettingsSMS;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageSettingsEmail;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageSettingsPreferences;
        private Settings.userControlSMSsettings userControlSMSsettings1;
        private Reports.userControlReportsLogs userControlReportsLogs1;
        private Settings.userControlEmailSettings userControlEmailSettings1;
        private DevExpress.XtraBars.BarButtonItem btnExitMain;
        private Settings.userControlPreferences userControlPreferences1;
        private Dashboard.userControlDashboardStudents userControlDashboardStudents1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupAdministration;
        private DevExpress.XtraNavBar.NavBarItem navBarItemEmployeeRegistration;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSchoolDetails;
        private DevExpress.XtraNavBar.NavBarItem navBarItemAcademicYear;
        private DevExpress.XtraNavBar.NavBarItem navBarItemClassFeesEntry;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageMainAdmin;
        private DevExpress.XtraBars.Navigation.NavigationFrame navigationFrameAdmin;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageAdminEmployeeReg;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageAdminSchoolDetails;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageAdminAcademicYear;
        private Admin.userControlAcademicYear userControlAcademicYear1;
        
        private Students.userControlStudentEnrolmentForm userControlStudentEnrolmentForm1;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageAdminFeeEntry;
        private Admin.userControlClassFeeEntry userControlClassFeeEntry1;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageAdminClassSettings;
        private DevExpress.XtraNavBar.NavBarItem navBarItemClassSetting;
        
        private Admin.userControlClassSetting userControlClassSetting1;
        private Admin.userControlSchoolDetails userControlSchoolDetails1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemListStudents;
        private Reports.userControlListStudents userControlListStudents1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemPaymentReports;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageReportsFeePayment;
        private Reports.userControlFeePaymentRecord userControlFeePaymentRecord1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSystemInf;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageSettingsSystemInfo;
        private Settings.userControlSystemInfo userControlSystemInfo1;
        private Admin.userControlEmployeeRegistration userControlEmployeeRegistration1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemPrinters;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageSettingsPrinters;
        private Settings.userControlPrinterSettings userControlPrinterSettings1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemClassPromotion;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupExit;
        private DevExpress.XtraNavBar.NavBarItem navBarItemConfigurationWizard;
        public DevExpress.XtraBars.BarStaticItem barStaticServer;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
        private System.Windows.Forms.Timer timer1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSendSMS;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageOfficeSMS;
        private Office.userControlOfficeSMS userControlOfficeSMS1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSMSReports;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageReportsSMS;
        private Reports.userControlReportsSMS userControlReportsSMS1;
        private DevExpress.XtraNavBar.NavBarItem navBarItemFeeDue;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageReportsFeeDue;
        private Reports.userControlReportsFeeDue userControlReportsFeeDue1;
        private DevExpress.XtraBars.BarButtonItem btnBinduWebsite;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageHelp;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupAbout;
        private DevExpress.XtraBars.BarButtonItem btnAbout;
        private DevExpress.XtraBars.BarButtonItem btnContactUs;
        private DevExpress.XtraNavBar.NavBarItem navBarBackupRestoreDatabase;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupTransport;
        private DevExpress.XtraNavBar.NavBarItem navBarItemBusInformation;
        private DevExpress.XtraNavBar.NavBarItem navBarItemStudentBusCardHolder;
        private DevExpress.XtraNavBar.NavBarItem navBarItemBusFeePayment;
        private DevExpress.XtraNavBar.NavBarItem navBarItemFeesCollected;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageDashboardFeesCollected;
        private DevExpress.XtraNavBar.NavBarItem navBarItemDashboardDesigner;
        private DevExpress.XtraBars.BarStaticItem barStaticItemLicenseStatus;
        private DevExpress.XtraNavBar.NavBarItem navBarStudentsEnrolmentSA;
        private DevExpress.XtraNavBar.NavBarItem navBarItemStudentReports;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroupEducation;
        private DevExpress.XtraNavBar.NavBarItem navBarItemSubjects;
        private DevExpress.XtraNavBar.NavBarItem navBarItemMarkSheet;
        private DevExpress.XtraNavBar.NavBarItem navBarItemReportCards;
        private DevExpress.XtraBars.Navigation.NavigationPage navPageMainEducation;
    }
}

