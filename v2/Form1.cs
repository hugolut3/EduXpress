using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraGauges.Core.Model;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Navigation;
using System.IO;
using System.Data.SqlClient;
using System.Resources;
using static EduXpress.Functions.PublicFunctions;
using static EduXpress.Functions.PublicVariables;
using System.Reflection;
using System.Globalization;
using DevExpress.XtraSpellChecker;
using System.Diagnostics;
using System.ServiceProcess;

namespace EduXpress
{
    
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(Form1).Assembly);

        Functions.PublicFunctions pf = new Functions.PublicFunctions();

        //Clock
        ArcScaleComponent scaleMinutes, scaleSeconds;
        int lockTimerCounter = 0;

        NavigationFrame navFrameStudents = new NavigationFrame();
        NavigationFrame navFrameEducation = new NavigationFrame();
        public Form1()
        {
            InitializeComponent();
            //clock
            scaleMinutes = Clock.AddScale();
            scaleSeconds = Clock.AddScale();

            scaleMinutes.Assign(scaleHours);
            scaleSeconds.Assign(scaleHours);

            arcScaleNeedleComponent2.ArcScale = scaleMinutes;
            arcScaleNeedleComponent3.ArcScale = scaleSeconds;
            timerClock.Start();
            timerClock_Tick(null, null);



            //populate navigation frames
            //------------------populate student navigation frame

            //Student Enrolment Form
            var ucStudentEnrolmentForm = new Students.userControlStudentEnrolmentForm();
            ucStudentEnrolmentForm.MainRibbon = this.ribbonControlMain;       //Set main ribbon and statusBar as main for userControls
            ucStudentEnrolmentForm.MainStatusBar = this.ribbonStatusBarMain;  
            ucStudentEnrolmentForm.Dock = DockStyle.Fill;
            navFrameStudents.AddPage(ucStudentEnrolmentForm);
            #region OldNavigationFrame
            //this.ribbonControlMain.MergeRibbon(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentForm).ribbonControl1);
            //this.ribbonStatusBarMain.MergeStatusBar(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentForm).ribbonStatusBar1);
            //ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
            //ribbonPageHelp.MergeOrder = 3; 
            #endregion

            //Student Enrolment Form SA
            var ucStudentEnrolmentFormSA = new Students.userControlStudentEnrolmentFormSA();
            ucStudentEnrolmentFormSA.MainRibbon = this.ribbonControlMain;  //Set main ribbon and statusBar as main for userControls
            ucStudentEnrolmentFormSA.MainStatusBar = this.ribbonStatusBarMain;
            ucStudentEnrolmentFormSA.Dock = DockStyle.Fill;
            navFrameStudents.AddPage(ucStudentEnrolmentFormSA);

            //Class Fee Payment
            var ucClassFeePayment = new Students.userControlClassFeePayment();
            ucClassFeePayment.MainRibbon = this.ribbonControlMain;    //Set main ribbon and statusBar as main for userControls
            ucClassFeePayment.MainStatusBar = this.ribbonStatusBarMain;
            ucClassFeePayment.Dock = DockStyle.Fill;
            navFrameStudents.AddPage(ucClassFeePayment);

            //Class Promotion
            var ucClassPromotion = new Students.userControlClassPromotion();
            ucClassPromotion.MainRibbon = this.ribbonControlMain;    //Set main ribbon and statusBar as main for userControls
            ucClassPromotion.MainStatusBar = this.ribbonStatusBarMain;
            ucClassPromotion.Dock = DockStyle.Fill;
            navFrameStudents.AddPage(ucClassPromotion);

            //Student Fees Reports
            var ucStudentFeesReports = new Students.userControlStudentFeesReports();
            ucStudentFeesReports.MainRibbon = this.ribbonControlMain;    //Set main ribbon and statusBar as main for userControls
            ucStudentFeesReports.MainStatusBar = this.ribbonStatusBarMain;
            ucStudentFeesReports.Dock = DockStyle.Fill;
            navFrameStudents.AddPage(ucStudentFeesReports);


            // When Student menus buttons clicked, change the navigation frame and merge ribbons and statusBars
            navBarStudentsEnrolment.LinkClicked += (s, ee) => { navFrameStudents.SelectedPageIndex = 0; ucStudentEnrolmentForm.MergeRibbon(); ucStudentEnrolmentForm.MergeStatusBar(); ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0]; ribbonPageHelp.MergeOrder = 3; };
            navBarStudentsEnrolmentSA.LinkClicked += (s,ee) => { navFrameStudents.SelectedPageIndex = 1; ucStudentEnrolmentFormSA.MergeRibbon(); ucStudentEnrolmentFormSA.MergeStatusBar(); ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0]; ribbonPageHelp.MergeOrder = 3;  };
            navBarStudentsFees.LinkClicked += (s, ee) => { navFrameStudents.SelectedPageIndex = 2; ucClassFeePayment.MergeRibbon(); ucClassFeePayment.MergeStatusBar(); ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0]; ribbonPageHelp.MergeOrder = 3; };
            navBarItemClassPromotion.LinkClicked += (s,ee) => { navFrameStudents.SelectedPageIndex = 3; ucClassPromotion.MergeRibbon(); ucClassPromotion.MergeStatusBar(); ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0]; ribbonPageHelp.MergeOrder = 3; };
            navBarItemStudentReports.LinkClicked += (s, ee) => { navFrameStudents.SelectedPageIndex = 4; ucStudentFeesReports.MergeRibbon(); ucStudentFeesReports.MergeStatusBar(); ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0]; ribbonPageHelp.MergeOrder = 3; };

            navFrameStudents.Parent = navPageMainStudents;
            navFrameStudents.Dock = DockStyle.Fill;

            //------------------populate Education navigation frame
            //Subjects
            var ucControlSubjects = new Education.userControlSubjectsDRC();
            ucControlSubjects.MainRibbon = this.ribbonControlMain;       //Set main ribbon and statusBar as main for userControls
            ucControlSubjects.MainStatusBar = this.ribbonStatusBarMain;
            ucControlSubjects.Dock = DockStyle.Fill;
            navFrameEducation.AddPage(ucControlSubjects);

            //Mark Sheet
            var ucControlMarkSheet = new Education.userControlMarkSheet();
            ucControlMarkSheet.MainRibbon = this.ribbonControlMain;       //Set main ribbon and statusBar as main for userControls
            ucControlMarkSheet.MainStatusBar = this.ribbonStatusBarMain;
            ucControlMarkSheet.Dock = DockStyle.Fill;
            navFrameEducation.AddPage(ucControlMarkSheet);

            //School Reports
            var ucControlBulletins = new Education.userControlBulletins();
            ucControlBulletins.MainRibbon = this.ribbonControlMain;       //Set main ribbon and statusBar as main for userControls
            ucControlBulletins.MainStatusBar = this.ribbonStatusBarMain;
            ucControlBulletins.Dock = DockStyle.Fill;
            navFrameEducation.AddPage(ucControlBulletins);

            // When Education menus buttons clicked, change the navigation frame and merge ribbons and statusBars
            navBarItemSubjects.LinkClicked += (s, ee) => { navFrameEducation.SelectedPageIndex = 0; ucControlSubjects.MergeRibbon(); ucControlSubjects.MergeStatusBar(); ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0]; ribbonPageHelp.MergeOrder = 3; };
            navBarItemMarkSheet.LinkClicked += (s, ee) => { navFrameEducation.SelectedPageIndex = 1; ucControlMarkSheet.MergeRibbon(); ucControlMarkSheet.MergeStatusBar(); ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0]; ribbonPageHelp.MergeOrder = 3; };
            navBarItemReportCards.LinkClicked += (s, ee) => { navFrameEducation.SelectedPageIndex = 2; ucControlBulletins.MergeRibbon(); ucControlBulletins.MergeStatusBar(); ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0]; ribbonPageHelp.MergeOrder = 3; };

            navFrameEducation.Parent = navPageMainEducation;
            navFrameEducation.Dock = DockStyle.Fill;
        }
        //If there is another instance of this application running
        //maximize it
        protected override void WndProc(ref Message m)
        {
            // ----- Watch for messages from other copies of the application.
            if (m.Msg ==Functions.NativeMethods.WM_EduXpressNotify)
            {
                // ----- Focus on this form.
                if (this.WindowState == FormWindowState.Minimized)
                    this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
            base.WndProc(ref m);
        }
        void UpdateClock(DateTime dt, IArcScale h, IArcScale m, IArcScale s)
        {
            int hour = dt.Hour < 12 ? dt.Hour : dt.Hour - 12;
            int min = dt.Minute;
            int sec = dt.Second;
            h.Value = (float)hour + (float)(min) / 60.0f;
            m.Value = ((float)min + (float)(sec) / 60.0f) / 5f;
            s.Value = sec / 5.0f;
        }

        //check if sql service is running if not start it
        //  public static bool sqlService()
        public bool sqlService()
        {
            ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(Form1).Assembly);

            string status; //service status (For example, Running or Stopped)
            if ((Properties.Settings.Default.SQLServiceName + "").Trim().Length == 0)
            {
                return false;
            }
            else
            {
                string myServiceName = Properties.Settings.Default.SQLServiceName; //service name of SQL Server (SQL server Instance name) 
                try
                {
                    ServiceController mySC = new ServiceController(myServiceName); ////Add Reference: System.ServiceProcess add on Using
                    status = mySC.Status.ToString();
                    //if service is Stopped or StopPending, you can run it with the following code.
                    if (mySC.Status.Equals(ServiceControllerStatus.Stopped) | mySC.Status.Equals(ServiceControllerStatus.StopPending))
                    {
                        try
                        {
                            if (splashScreenManagerWait.IsSplashFormVisible == false)
                            {
                                splashScreenManagerWait.ShowWaitForm();
                                splashScreenManagerWait.SetWaitFormDescription(LocRM.GetString("strStartingDatabaseService"));
                            }
                            
                            barStaticServer.Caption = LocRM.GetString("strStartingDatabaseService");
                            //To have the priveleges to start the SQL service, we need admin privileges.
                            //Add new app.manifest file into your project
                            //Replace this line: <requestedExecutionLevel level="asInvoker" uiAccess="false" />
                            //With this: <requestedExecutionLevel level="requireAdministrator" uiAccess="false" />
                            mySC.Start();
                            mySC.WaitForStatus(ServiceControllerStatus.Running);
                            sqlServerStatus = LocRM.GetString("strSQLServiceStatusRunning");

                            if (splashScreenManagerWait.IsSplashFormVisible == true)
                            {
                                splashScreenManagerWait.CloseWaitForm();
                            }
                            return true;
                        }
                        catch (Exception ex)
                        {
                            if (splashScreenManagerWait.IsSplashFormVisible == true)
                            {
                                splashScreenManagerWait.CloseWaitForm();
                            }
                            sqlServerStatus = LocRM.GetString("strErrorStartingSQLService") + ": " + ex.Message;
                            return false;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    sqlServerStatus = LocRM.GetString("strSQLServiceNotRunning") + ". " + "Exception: " + ex.Message;
                    return false;
                }
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection testConnect;
            bool serverStatus;

            barStaticServer.Caption = "";
            barStaticServer.Caption = LocRM.GetString("strCheckingServerStatus");
            // -----  Configure Screen by hiding/showing pannels.
            ConfigureScreen();

            //---- check if SQL server instance is running again and update status. Maybe SQL server was restarted.
            serverStatus = sqlService();
            barStaticServer.Caption = "";
            barStaticServer.Caption = sqlServerStatus;

            //---- check if we can connect to database
            testConnect = ConnectDatabase();
            if (testConnect == null)
            {                
                Application.Exit();
                return;
            }
            testConnect.Dispose();           
            

            //Display business name in main form title bar
            if (Properties.Settings.Default.BusinessName != "")
            {
                //Business Name in Title Bar
                this.Text = this.Text + " - " + Properties.Settings.Default.BusinessName;
            }
            else
            {
                try
                {
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ct = "select RTRIM(SchoolName) from CompanyProfile where CompanyDetailsID= @d1 ";
                        cmd = new SqlCommand(ct);
                        cmd.Parameters.AddWithValue("@d1", 1);                        
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {

                            if (rdr.Read())
                            {
                                //Business Name in Title Bar
                                this.Text = this.Text + " - " + pf.Decrypt(rdr.GetString(0));
                                //save company name in settings
                                Properties.Settings.Default.BusinessName = pf.Decrypt(rdr.GetString(0));
                                // ----- Save any updated settings.
                                Properties.Settings.Default.Save();
                            }
                        }
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                            // con.Dispose();
                        }
                    }


                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            

            //-------Show main application
            //   this.Show();
            //show login form
            ShowLoginForm();            
            
        }
       
        private void ConfigureScreen()
        {
            // ----- Update the display.

            //start displaying dashboard
            hideMenu();
            navBarGroupDashboard.Expanded = true;
            navigationFrameMain.SelectedPageIndex = 0;
            dashboardTime();
            
            barStaticUser.Caption = "";
            barStaticUser.Caption = LocRM.GetString("strNoLoggedIn");            
            //show close button in dashboard only
            btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }


        private void navBarItemTime_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
           dashboardTime();
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            if (lockTimerCounter == 0)
            {
                lockTimerCounter++;
                UpdateClock(DateTime.Now, scaleHours, scaleMinutes, scaleSeconds);
                lockTimerCounter--;
            }

        }
        //Main Form student dashboard
        private void navBarItemStudents_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // dashboardStudents();
            try
            {
                
                navFrameDashboard.SelectedPageIndex = 1;
                //if (splashScreenManagerWait.IsSplashFormVisible == false)
                //{
                //    splashScreenManagerWait.ShowWaitForm();
                //}
               // navFrameDashboard.Visible = false;
                this.ribbonControlMain.MergeRibbon(((navFrameDashboard.SelectedPage as NavigationPage).Controls[0] as Dashboard.userControlDashboardStudents).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameDashboard.SelectedPage as NavigationPage).Controls[0] as Dashboard.userControlDashboardStudents).ribbonStatusBar1);
                // ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                //show close button in dashboard Time only
                //btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                //navFrameDashboard.Visible = false;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //Display dashboard Time when  navBarItemTime clicked
        private void dashboardTime()
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
               // navFrameDashboard.Visible = false;
                navFrameDashboard.SelectedPageIndex = 0;
                  this.ribbonControlMain.UnMergeRibbon();
                  this.ribbonStatusBarMain.UnMergeStatusBar();
                dateNavigatorDashboard.DateTime = DateTime.Today;
                //show close button in dashboard Time only
                btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                ribbonPageGroupExit.Visible = true;
               // navFrameDashboard.Visible = false;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       


        private void navBarControlMain_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            try
            {
                if (navBarGroupDashboard.Expanded)
                {
                    navigationFrameMain.SelectedPageIndex = 0;
                    dashboardTime();
                }
                if (navBarGroupOffice.Expanded)
                {
                    // ----- Don't allow to use this feature if the program License has expired or invalid.
                    while ((ExamineLicense().Status == LicenseStatus.LicenseExpired) || (ExamineLicense().Status == LicenseStatus.InvalidSignature))
                    {
                        // ----- Ask the user what to do.
                        if (XtraMessageBox.Show(LocRM.GetString("strNoValidLicenseDetected"), LocRM.GetString("strProgramTitle"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            navigationFrameMain.SelectedPageIndex = 0;
                            dashboardTime();
                            navBarGroupDashboard.Expanded = true;
                            return;
                        }

                        // ----- Prompt for an updated license.
                        (new UI.ConfigurationWizard()).ChangeLicense();
                    }
                    // -----------------------------
                    navigationFrameMain.SelectedPageIndex = 1;
                    OfficeWordDocument();
                    //show close button in dashboard Time only
                    //  btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //  ribbonPageGroupExit.Visible = false;

                  // ribbonControlMain.UnMergeRibbon();   ;
                }
                
                if (navBarGroupStudents.Expanded)
                {
                    // ----- Don't allow to use this feature if the program is unlicensed.
                    while (ExamineLicense().Status != LicenseStatus.ValidLicense)
                    {
                        // ----- Ask the user what to do.
                        if (XtraMessageBox.Show(LocRM.GetString("strNoValidLicenseDetected"), LocRM.GetString("strProgramTitle"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            navigationFrameMain.SelectedPageIndex = 0;
                            dashboardTime();
                            navBarGroupDashboard.Expanded = true;
                            return;
                        }

                        // ----- Prompt for an updated license.
                        (new UI.ConfigurationWizard()).ChangeLicense();
                    }
                    // -----------------------------
                    //ribbonControlMain.UnMergeRibbon();                   

                    try
                    {

                        //    //navPageMainStudents.Controls.Add(new NavigationFrame(){ Dock = DockStyle.Fill});

                        if ((Role == 1) || (Role == 2) || (Role == 4) || (Role == 7)) //Administrator, Administrator Assistant, Account & Admission, Admission Officer
                        {
                            if (splashScreenManagerWait.IsSplashFormVisible == false)
                            {
                                splashScreenManagerWait.ShowWaitForm();
                            }

                            if (Properties.Settings.Default.Country != "")
                            {
                                navigationFrameMain.SelectedPage = navPageMainStudents; //same as using index: navigationFrameMain.SelectedPageIndex = 2;
                                                                                        // Display Student enrolmnet form based on country info when in DRC only
                                if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                                {
                                    navFrameStudents.SelectedPageIndex = 0; //DRC enrolment form
                                    navBarStudentsEnrolment.Visible = true;
                                    navBarStudentsEnrolmentSA.Visible = false;
                                }
                                else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                                {
                                    navFrameStudents.SelectedPageIndex = 1;
                                    navBarStudentsEnrolment.Visible = false;
                                    navBarStudentsEnrolmentSA.Visible = true;
                                }
                                else
                                {
                                    navFrameStudents.SelectedPageIndex = 1;
                                    navBarStudentsEnrolment.Visible = false;
                                    navBarStudentsEnrolmentSA.Visible = true;
                                }
                            }
                            else
                            {
                                // get the country from database
                                try
                                {
                                    using (con = new SqlConnection(databaseConnectionString))
                                    {
                                        con.Open();
                                        string ct = "select Country from CompanyProfile ";

                                        cmd = new SqlCommand(ct);
                                        cmd.Connection = con;
                                        rdr = cmd.ExecuteReader();

                                        if (rdr.HasRows)
                                        {
                                            //load data from database to controls
                                            if (rdr.Read())
                                            {
                                            string CountryName = rdr.GetString(0).ToString().Trim();
                                            if (CountryName =="")
                                            {
                                                if (splashScreenManagerWait.IsSplashFormVisible == true)
                                                {
                                                    splashScreenManagerWait.CloseWaitForm();
                                                }
                                                XtraMessageBox.Show(LocRM.GetString("strSetupSchoolProfile"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                    navigationFrameMain.SelectedPageIndex = 0;
                                                    dashboardTime();
                                                    navBarGroupDashboard.Expanded = true;
                                                    return;
                                            }
                                            else
                                            {
                                                //save country name in application settings                                        
                                                Properties.Settings.Default.Country = rdr.GetString(0).ToString().Trim();
                                                // ----- Save any updated settings.
                                                Properties.Settings.Default.Save();

                                                navigationFrameMain.SelectedPage = navPageMainStudents; //same as using index: navigationFrameMain.SelectedPageIndex = 2;
                                                // Display Student enrolmnet form based on country info when in DRC only
                                                if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                                                {
                                                    navFrameStudents.SelectedPageIndex = 0; //DRC enrolment form
                                                    navBarStudentsEnrolment.Visible = true;
                                                    navBarStudentsEnrolmentSA.Visible = false;
                                                }
                                                // Display Student enrolmnet form based on country info when in SA only
                                                else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                                                {
                                                    navFrameStudents.SelectedPageIndex = 1;
                                                    navBarStudentsEnrolment.Visible = false;
                                                    navBarStudentsEnrolmentSA.Visible = true;
                                                }
                                                // Display Student enrolmnet form based on other countries beside DRC and SA
                                                else
                                                {
                                                    navFrameStudents.SelectedPageIndex = 1;
                                                    navBarStudentsEnrolment.Visible = false;
                                                    navBarStudentsEnrolmentSA.Visible = true;
                                                }
                                            }
                                            
                                            }
                                        }
                                        else
                                        {
                                        if (splashScreenManagerWait.IsSplashFormVisible == true)
                                        {
                                            splashScreenManagerWait.CloseWaitForm();
                                        }
                                        XtraMessageBox.Show(LocRM.GetString("strSetupSchoolProfile"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        navigationFrameMain.SelectedPageIndex = 0;
                                        dashboardTime();
                                            navBarGroupDashboard.Expanded = true;
                                            return;
                                        }
                                        if ((rdr != null))
                                        {
                                            rdr.Close();
                                        }
                                        if (con.State == ConnectionState.Open)
                                        {
                                            con.Close();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                                    {
                                        splashScreenManagerWait.CloseWaitForm();
                                    }
                                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                
                            }
                                
                            //        //navFrameStudents.QueryControl += NavFrameStudents_QueryControl; // raised when navigation frame page changes to display empty content
                            //        //used to add usercontrols to navigation pages

                            //this.ribbonControlMain.MergeRibbon(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentForm).ribbonControl1);
                            //this.ribbonStatusBarMain.MergeStatusBar(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentForm).ribbonStatusBar1);



                            //ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                            //ribbonPageHelp.MergeOrder = 3;
                            if (splashScreenManagerWait.IsSplashFormVisible == true)
                            {
                                splashScreenManagerWait.CloseWaitForm();
                            }
                        }
                        else  // doesn't have access to enrolment form, display fees form.
                        {
                            if (splashScreenManagerWait.IsSplashFormVisible == false)
                            {
                                splashScreenManagerWait.ShowWaitForm();
                            }
                            navigationFrameMain.SelectedPage = navPageMainStudents;
                            navFrameStudents.SelectedPageIndex = 2;

                            navBarStudentsEnrolment.Visible = false;
                            navBarStudentsEnrolmentSA.Visible = false;
                            navBarStudentsFees.Visible = true;
                            //        this.ribbonControlMain.MergeRibbon(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlClassFeePayment).ribbonControl1);
                            //        this.ribbonStatusBarMain.MergeStatusBar(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlClassFeePayment).ribbonStatusBar1);
                            //        ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                            //        ribbonPageHelp.MergeOrder = 3;
                            if (splashScreenManagerWait.IsSplashFormVisible == true)
                            {
                                splashScreenManagerWait.CloseWaitForm();
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        if (splashScreenManagerWait.IsSplashFormVisible == true)
                        {
                            splashScreenManagerWait.CloseWaitForm();
                        }
                        XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    //merge usercontrol ribbons, use interface
                    (((navPageMainStudents.Controls.OfType<NavigationFrame>().FirstOrDefault().SelectedPage as NavigationPage).Controls.OfType<UserControl>().FirstOrDefault() as Functions.IMergeRibbons)).MergeRibbon();
                    (((navPageMainStudents.Controls.OfType<NavigationFrame>().FirstOrDefault().SelectedPage as NavigationPage).Controls.OfType<UserControl>().FirstOrDefault() as Functions.IMergeRibbons)).MergeStatusBar();

                    ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                    ribbonPageHelp.MergeOrder = 3;
                  //  studentenrolment(); 
                    //show close button in dashboard Time only
                     //btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                     //ribbonPageGroupExit.Visible = false;

                }
                if (navBarGroupReports.Expanded)
                {
                    // ----- Don't allow to use this feature if the program is unlicensed.
                    while (ExamineLicense().Status != LicenseStatus.ValidLicense)
                    {
                        // ----- Ask the user what to do.
                        if (XtraMessageBox.Show(LocRM.GetString("strNoValidLicenseDetected"), LocRM.GetString("strProgramTitle"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            navigationFrameMain.SelectedPageIndex = 0;
                            dashboardTime();
                            navBarGroupDashboard.Expanded = true;
                            return;
                        }

                        // ----- Prompt for an updated license.
                        (new UI.ConfigurationWizard()).ChangeLicense();
                    }
                    // -----------------------------
                    navigationFrameMain.SelectedPageIndex = 3;
                    Reports();
                    //show close button in dashboard Time only
                    //btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //ribbonPageGroupExit.Visible = false;
                }

                if (navBarGroupSettings.Expanded)
                {
                    navigationFrameMain.SelectedPageIndex = 4;
                    //if ((Role == 1) || (Role == 2)) //Administrator, Administrator Assistant, Account & Admission, Admission Officer
                    //{
                    //    navBarItemDashboardDesigner.Visible = true;  //View Dashboard Designer
                    //}
                    //else
                    //{
                    //    navBarItemDashboardDesigner.Visible = false; //Hide Dashboard Designer
                    //}
                    Preferences();

                    //show close button in dashboard only
                    //btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //ribbonPageGroupExit.Visible = false;
                }
                if (navBarGroupAdministration.Expanded)
                {
                    // ----- Don't allow to use this feature if the program is unlicensed.
                    while (ExamineLicense().Status != LicenseStatus.ValidLicense)
                    {
                        // ----- Ask the user what to do.
                        if (XtraMessageBox.Show(LocRM.GetString("strNoValidLicenseDetected"), LocRM.GetString("strProgramTitle"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            navigationFrameMain.SelectedPageIndex = 0;
                            dashboardTime();
                            navBarGroupDashboard.Expanded = true;
                            return;
                        }

                        // ----- Prompt for an updated license.
                        (new UI.ConfigurationWizard()).ChangeLicense();
                    }
                    // -----------------------------
                    navigationFrameMain.SelectedPageIndex = 5;
                    AdminEmployeeReg();
                    //show close button in dashboard only
                    //   btnExitMain.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    //  ribbonPageGroupExit.Visible = false;
                }
                //Education group menu Clicked
                if (navBarGroupEducation.Expanded)
                {
                    // ----- Don't allow to use this feature if the program is unlicensed.
                    while (ExamineLicense().Status != LicenseStatus.ValidLicense)
                    {
                        // ----- Ask the user what to do.
                        if (XtraMessageBox.Show(LocRM.GetString("strNoValidLicenseDetected"), LocRM.GetString("strProgramTitle"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            navigationFrameMain.SelectedPageIndex = 0;
                            dashboardTime();
                            navBarGroupDashboard.Expanded = true;
                            return;
                        }

                        // ----- Prompt for an updated license.
                        (new UI.ConfigurationWizard()).ChangeLicense();
                    }

                   
                    // -----------------------------
                    navigationFrameMain.SelectedPage = navPageMainEducation; //navigationFrameMain.SelectedPageIndex = 6;
                    navFrameEducation.SelectedPageIndex = 0;
                    //merge usercontrol ribbons, use interface
                    (((navPageMainEducation.Controls.OfType<NavigationFrame>().FirstOrDefault().SelectedPage as NavigationPage).Controls.OfType<UserControl>().FirstOrDefault() as Functions.IMergeRibbons)).MergeRibbon();
                    (((navPageMainEducation.Controls.OfType<NavigationFrame>().FirstOrDefault().SelectedPage as NavigationPage).Controls.OfType<UserControl>().FirstOrDefault() as Functions.IMergeRibbons)).MergeStatusBar();

                    ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                    ribbonPageHelp.MergeOrder = 3;
                    //SubjectsEntry();                    
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Display word document when  navBarItemWordDocument clicked
        private void OfficeWordDocument()
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameOffice.SelectedPageIndex = 0;
                this.ribbonControlMain.MergeRibbon(((navFrameOffice.SelectedPage as NavigationPage).Controls[0] as Office.userControlOfficeWord).ribbonControl1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 16;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void navBarItemWordDocument_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OfficeWordDocument();
        }
        //Spreadsheet
        private void navBarItemSpreadsheet_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameOffice.SelectedPageIndex = 1;
                this.ribbonControlMain.MergeRibbon(((navFrameOffice.SelectedPage as NavigationPage).Controls[0] as Office.userControlOfficeSpreadsheet).ribbonControl1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 22;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //PDF document Viewer
        private void navBarItemPdfViewer_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameOffice.SelectedPageIndex = 2;
                this.ribbonControlMain.MergeRibbon(((navFrameOffice.SelectedPage as NavigationPage).Controls[0] as Office.userControlOfficePDF).ribbonControl1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 5;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarItemScheduling_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameOffice.SelectedPageIndex = 3;
                this.ribbonControlMain.MergeRibbon(((navFrameOffice.SelectedPage as NavigationPage).Controls[0] as Office.userControlOfficeScheduling).ribbonControl1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 6;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void barBtnMenuLogin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            //show login form            
            ShowLoginForm();
            
        }
        private void ShowLoginForm()
        {
            
            // ----- login form.
            DialogResult userChoice;

            // ----- Prompt the user.
            UI.frmLogin frm = new UI.frmLogin();
            //userChoice = (new UI.frmLogin()).ShowDialog();
            //Suspend layout: Basically it's if you want to adjust multiple layout-related properties - 
            //or add multiple children - but avoid the layout system repeatedly reacting to your changes. 
            //You want it to only perform the layout at the very end, when everything's "ready".
            //this.be
            this.SuspendLayout();
            //minimise form
           // frm.WindowState = FormWindowState.Minimized;
            userChoice = frm.ShowDialog();
            //frm.Activate();


            //frm.BringToFront();
            //frm.TopMost = true;
            //frm.Activate();
           // frm.ActiveControl = txtUserName;
            //maximiz it
            // frm.WindowState = FormWindowState.Normal;
            //Resume layout
            this.ResumeLayout();
            

            //   frm.txtUserName.
            if (userChoice == DialogResult.OK)
            {
                //check the activated license
                checkLicense();

                //Show user logged in in status bar and respective menus
                if (Role == 0) // No one logged in
                {
                    hideMenu();
                }
                if (Role == 1) //Administrator
                {
                    showMenuAdmin();
                    barStaticUser.Caption = "";
                    barStaticUser.Caption = UserLoggedSurname.ToUpper() + " " + UserLoggedName.ToUpper() + " " +LocRM.GetString("strLoggedIn");
                }

                if (Role == 2) //Administrator Assistant
                {
                    showMenuAdminAssistant();
                    barStaticUser.Caption = "";
                    barStaticUser.Caption = UserLoggedSurname.ToUpper() + " " + UserLoggedName.ToUpper() + " " + LocRM.GetString("strLoggedIn");
                }
                if (Role == 3) // Accountant
                {
                    showMenuAccountant();
                    barStaticUser.Caption = "";
                    barStaticUser.Caption = UserLoggedSurname.ToUpper() + " " + UserLoggedName.ToUpper() + " " + LocRM.GetString("strLoggedIn");
                }
                if (Role == 4) //Account & Admission
                {
                    showMenuAccountAdmission();
                    barStaticUser.Caption = "";
                    barStaticUser.Caption = UserLoggedSurname.ToUpper() + " " + UserLoggedName.ToUpper() + " " + LocRM.GetString("strLoggedIn");
                }
                if (Role == 5) //Account & HR
                {
                    hideMenu();
                }
                if (Role == 6) //Account, Admission & HR
                {
                    hideMenu();
                }
                if (Role == 7) //Admission Officer
                {
                    showMenuAdmissionOfficer();
                    barStaticUser.Caption = "";
                    barStaticUser.Caption = UserLoggedSurname.ToUpper() + " " + UserLoggedName.ToUpper() + " " + LocRM.GetString("strLoggedIn");
                }
                if (Role == 8) //Human Resources
                {
                    hideMenu();
                }
                if (Role == 9) //Librarian
                {
                    hideMenu();
                }
                if (Role == 10) //Librarian & Admission
                {
                    hideMenu();
                }
                if (Role == 11) //Stock Clerk
                {
                    hideMenu();
                }
                if (Role == 12) //Teacher
                {
                    hideMenu();
                }

            }
            else
            {
                // -----  Configure Screen by hiding/showing pannels.
                ConfigureScreen();
                //frm.ShowDialog();
            }
        }
        private void hideMenu()
        {
            officeNavigationBarMain.Visible = false;
            barBtnMenuLogout.Enabled = false;
            barBtnMenuLogin.Enabled = true;
            navBarItemStudents.Visible = false;//Student Home dashboard
            navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
        }
        //string LicenseFeature = "";
        private void checkLicense()
        {
            // ----- Prepare the form.
            LicenseFileDetail licenseDetails;
            
            licenseDetails = ExamineLicense();
            if (licenseDetails.Status == LicenseStatus.ValidLicense)
            {
                barStaticItemLicenseStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barStaticItemLicenseStatus.Caption = "";
                LicenseFeature = licenseDetails.Feature;                
            }
            else if (licenseDetails.Status == LicenseStatus.LicenseExpired)
            {
                barStaticItemLicenseStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barStaticItemLicenseStatus.Caption= LocRM.GetString("strEduXpressLicenseExpired");
                LicenseFeature = "";
            }
            else
            {
                barStaticItemLicenseStatus.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barStaticItemLicenseStatus.Caption = LocRM.GetString("strEduXpressUnlicensed");
                LicenseFeature = "";
            }
        }
        private void showMenuAdmin()
        {
            //Show Admin menu based on license
            if (LicenseFeature == "0".Trim()) //3 months Trial version with all current modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = true;//Student Home dashboard
                navBarItemFeesCollected.Visible = true;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu
                                                    // Display Student enrolmnet form based on country info when in DRC only
                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }               
                    
                
                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = true; // Logs
                navBarItemListStudents.Visible = true; // Export list of students
                navBarItemPaymentReports.Visible = true; // summarized payment reports
                navBarItemSMSReports.Visible = true; // SMS Reports
                navBarItemFeeDue.Visible = true; // Fee Due Reports

                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = true; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = true; // Administration School Details
                navBarItemAcademicYear.Visible = true; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = true; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = true; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = true; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = true; // Settings Dashboard Designer
            }
            if (LicenseFeature == "1".Trim())
            {
                //; Express Version
            }
            if (LicenseFeature == "2".Trim()) //Standard Version: Module de base only
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = true;//Student Home dashboard
                navBarItemFeesCollected.Visible = true;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                
                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }
                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = true; // Logs
                navBarItemListStudents.Visible = true; // Export list of students
                navBarItemPaymentReports.Visible = true; // summarized payment reports
                navBarItemSMSReports.Visible = true; // SMS Reports
                navBarItemFeeDue.Visible = true; // Fee Due Reports

                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = true; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = true; // Administration School Details
                navBarItemAcademicYear.Visible = true; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = true; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = true; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = true; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = true; // Settings Dashboard Designer
            }
            if (LicenseFeature == "3".Trim()) //Professional Version: module de base plus suplimentaire
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                //--------------------Module de base
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = true;//Student Home dashboard
                navBarItemFeesCollected.Visible = true;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = true; // Logs
                navBarItemListStudents.Visible = true; // Export list of students
                navBarItemPaymentReports.Visible = true; // summarized payment reports
                navBarItemSMSReports.Visible = true; // SMS Reports
                navBarItemFeeDue.Visible = true; // Fee Due Reports

                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = true; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = true; // Administration School Details
                navBarItemAcademicYear.Visible = true; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = true; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = true; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = true; // Administration RestoreDatabase
                //---

                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                
                //
                navBarItemDashboardDesigner.Visible = true; // Settings Dashboard Designer
                //-----------------------------------------------------
            }
            if (LicenseFeature == "4".Trim()) //Lite Version: only enrolment, office and communication (email and sms)
            {
                eduxpressLiteStudent = true;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion
                navBarItemStudentReports.Visible = false; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = false; // Export list of students
                navBarItemPaymentReports.Visible = false; // summarized payment reports
                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = true; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = true; // Administration School Details
                navBarItemAcademicYear.Visible = true; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = false; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = true; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = true; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "5".Trim()) //Ultimate Version: All available modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = true;//Student Home dashboard
                navBarItemFeesCollected.Visible = true;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = true; // Logs
                navBarItemListStudents.Visible = true; // Export list of students
                navBarItemPaymentReports.Visible = true; // summarized payment reports
                navBarItemSMSReports.Visible = true; // SMS Reports
                navBarItemFeeDue.Visible = true; // Fee Due Reports

                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = true; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = true; // Administration School Details
                navBarItemAcademicYear.Visible = true; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = true; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = true; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = true; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = true; // Settings Dashboard Designer
            }
            if (LicenseFeature == "") //unlicensed or expired: Basic module
            {
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = false; // Export list of students
                navBarItemPaymentReports.Visible = false; // summarized payment reports
                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = false; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = false; // Administration School Details
                navBarItemAcademicYear.Visible = false; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = false; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = false; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = false; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = false; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = false; // office Spread sheet
                navBarItemScheduling.Visible = false; // office Scheduling
                navBarItemEmail.Visible = false; // office Email
                navBarItemSendSMS.Visible = false; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer                
            }
        }
        private void showMenuAdminAssistant()
        {
            //Show Admin menu based on license
            if (LicenseFeature == "0".Trim()) //3 months Trial version with all current modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = true;//Student Home dashboard
                navBarItemFeesCollected.Visible = true;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion     
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = true; // Export list of students
                navBarItemPaymentReports.Visible = true; // summarized payment reports
                navBarItemSMSReports.Visible = true; // SMS Reports
                navBarItemFeeDue.Visible = true; // Fee Due Reports

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = true; // Settings Dashboard Designer
            }
            if (LicenseFeature == "1".Trim())
            {
                //; Express Version
            }
            if (LicenseFeature == "2".Trim()) //Standard Version: Module de base only
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = true;//Student Home dashboard
                navBarItemFeesCollected.Visible = true;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion   
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = true; // Export list of students
                navBarItemPaymentReports.Visible = true; // summarized payment reports
                navBarItemSMSReports.Visible = true; // SMS Reports
                navBarItemFeeDue.Visible = true; // Fee Due Reports

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = true; // Settings Dashboard Designer
            }
            if (LicenseFeature == "3".Trim()) //Professional Version: module de base plus suplimentaire
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                //--------------------Module de base
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = true;//Student Home dashboard
                navBarItemFeesCollected.Visible = true;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion   
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = true; // Export list of students
                navBarItemPaymentReports.Visible = true; // summarized payment reports
                navBarItemSMSReports.Visible = true; // SMS Reports
                navBarItemFeeDue.Visible = true; // Fee Due Reports

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = true; // Settings Dashboard Designer
                //-----------------------------------------------------
            }
            if (LicenseFeature == "4".Trim()) //Lite Version: only enrolment, office and communication (email and sms)
            {
                eduxpressLiteStudent = true;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion
                navBarItemStudentReports.Visible = false;// Student Reports


                navBarGroupReports.Visible = false; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = false; // Export list of students
                navBarItemPaymentReports.Visible = false; // summarized payment reports
                navBarGroupAdministration.Visible = false; // Administration Menu
                
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "5".Trim()) //Ultimate Version: All available modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = true;//Student Home dashboard
                navBarItemFeesCollected.Visible = true;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion 
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = true; // Export list of students
                navBarItemPaymentReports.Visible = true; // summarized payment reports
                navBarItemSMSReports.Visible = true; // SMS Reports
                navBarItemFeeDue.Visible = true; // Fee Due Reports

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = true; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = true; // Settings System Information
                navBarItemSMS.Visible = true; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = true; // Settings Dashboard Designer
            }
            if (LicenseFeature == "") //unlicensed or expired: Basic module
            {
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = false; // Export list of students
                navBarItemPaymentReports.Visible = false; // summarized payment reports
                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = false; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = false; // Administration School Details
                navBarItemAcademicYear.Visible = false; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = false; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = false; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = false; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = false; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //
                navBarItemSpreadsheet.Visible = false; // office Spread sheet
                navBarItemScheduling.Visible = false; // office Scheduling
                navBarItemEmail.Visible = false; // office Email
                navBarItemSendSMS.Visible = false; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer                
            }
        }
        private void showMenuAccountant()
        {
            //Show Admin menu based on license
            if (LicenseFeature == "0".Trim()) //3 months Trial version with all current modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion    
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu                

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "1".Trim())
            {
                //; Express Version
            }
            if (LicenseFeature == "2".Trim()) //Standard Version: Module de base only
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion  
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu                

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "3".Trim()) //Professional Version: module de base plus suplimentaire
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                //--------------------Module de base
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion  
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu                

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
                //-----------------------------------------------------
            }

            if (LicenseFeature == "4".Trim()) //Lite Version: only enrolment, office and communication (email and sms)
            {
                XtraMessageBox.Show(LocRM.GetString("strFinanceNotAllowedEXpressLite"), LocRM.GetString("strProgramTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (LicenseFeature == "5".Trim()) //Ultimate Version: All available modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion    
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu
                
                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "") //unlicensed or expired: Basic module
            {
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = false; // Export list of students
                navBarItemPaymentReports.Visible = false; // summarized payment reports
                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = false; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = false; // Administration School Details
                navBarItemAcademicYear.Visible = false; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = false; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = false; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = false; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = false; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //
                navBarItemSpreadsheet.Visible = false; // office Spread sheet
                navBarItemScheduling.Visible = false; // office Scheduling
                navBarItemEmail.Visible = false; // office Email
                navBarItemSendSMS.Visible = false; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer                
            }

        }
        private void showMenuAccountAdmission()
        {
            //Show Admin menu based on license
            if (LicenseFeature == "0".Trim()) //3 months Trial version with all current modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion  
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu                

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "1".Trim())
            {
                //; Express Version
            }
            if (LicenseFeature == "2".Trim()) //Standard Version: Module de base only
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion  
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu                

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "3".Trim()) //Professional Version: module de base plus suplimentaire
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                //--------------------Module de base
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion 
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu                

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
                //-----------------------------------------------------
            }

            if (LicenseFeature == "4".Trim()) //Lite Version: only enrolment, office and communication (email and sms)
            {
                XtraMessageBox.Show(LocRM.GetString("strFinanceNotAllowedEXpressLite"), LocRM.GetString("strProgramTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (LicenseFeature == "5".Trim()) //Ultimate Version: All available modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = true; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion
                navBarItemStudentReports.Visible = true; // Student Reports

                navBarGroupReports.Visible = false; // Reports Menu

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "") //unlicensed or expired: Basic module
            {
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = false; // Export list of students
                navBarItemPaymentReports.Visible = false; // summarized payment reports
                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = false; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = false; // Administration School Details
                navBarItemAcademicYear.Visible = false; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = false; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = false; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = false; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = false; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //
                navBarItemSpreadsheet.Visible = false; // office Spread sheet
                navBarItemScheduling.Visible = false; // office Scheduling
                navBarItemEmail.Visible = false; // office Email
                navBarItemSendSMS.Visible = false; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer                
            }
        }
        private void showMenuAdmissionOfficer()
        {
            //Show Admin menu based on license
            if (LicenseFeature == "0".Trim()) //3 months Trial version with all current modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion    
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = false; // Reports Menu
                
                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "1".Trim())
            {
                //; Express Version
            }
            if (LicenseFeature == "2".Trim()) //Standard Version: Module de base only
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion  
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = false; // Reports Menu

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "3".Trim()) //Professional Version: module de base plus suplimentaire
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion  
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = false; // Reports Menu

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "4".Trim()) //Lite Version: only enrolment, office and communication (email and sms)
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion 
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = false; // Reports Menu

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "5".Trim()) //Ultimate Version: All available modules
            {
                eduxpressLiteStudent = false;  //Variable to check when Student menu loads to hide/show based on lite version
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = true; // student Promotion  
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = false; // Reports Menu

                navBarGroupAdministration.Visible = false; // Administration Menu
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = true; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //                
                navBarItemSpreadsheet.Visible = true; // office Spread sheet
                navBarItemScheduling.Visible = true; // office Scheduling
                navBarItemEmail.Visible = true; // office Email
                navBarItemSendSMS.Visible = true; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer
            }
            if (LicenseFeature == "") //unlicensed or expired: Basic module
            {
                barBtnMenuLogout.Enabled = true;
                barBtnMenuLogin.Enabled = false;
                navBarItemStudents.Visible = false;//Student Home dashboard
                navBarItemFeesCollected.Visible = false;//Fees Collected Home dashboard
                navBarGroupStudents.Visible = true; // student Menu

                if (Properties.Settings.Default.Country != "")
                {
                    if (Properties.Settings.Default.Country == LocRM.GetString("strDRC") || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strDRC", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = true; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = false; // student Enrolment
                    }
                    else if (Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica") || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("fr-FR")) || Properties.Settings.Default.Country == LocRM.GetString("strSouthAfrica", CultureInfo.CreateSpecificCulture("en-US")))
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                    else
                    {
                        navBarStudentsEnrolment.Visible = false; // student Enrolment DRC form
                        navBarStudentsEnrolmentSA.Visible = true; // student Enrolment
                    }
                }

                navBarStudentsFees.Visible = false; // student Fees
                navBarItemClassPromotion.Visible = false; // student Promotion
                navBarItemStudentReports.Visible = false;// Student Reports

                navBarGroupReports.Visible = true; // Reports Menu
                navBarItemLogs.Visible = false; // Logs
                navBarItemListStudents.Visible = false; // Export list of students
                navBarItemPaymentReports.Visible = false; // summarized payment reports
                navBarGroupAdministration.Visible = true; // Administration Menu
                //---Admin submenu
                navBarItemEmployeeRegistration.Visible = false; // Administration Employee Registration
                navBarItemSchoolDetails.Visible = false; // Administration School Details
                navBarItemAcademicYear.Visible = false; // Administration Academic Year
                navBarItemClassFeesEntry.Visible = false; // Administration Class Fees Entry
                navBarItemClassSetting.Visible = false; // Administration Class Setting
                navBarBackupRestoreDatabase.Visible = false; // Administration RestoreDatabase
                //---
                navBarGroupSettings.Visible = true; // Settings Menu
                navBarItemEmailServer.Visible = false; // Settings Email Server
                navBarItemPreferences.Visible = true; // Settings Preferences
                navBarItemSystemInf.Visible = false; // Settings System Information
                navBarItemSMS.Visible = false; // Settings SMS
                navBarItemPrinters.Visible = false; // Settings Printers
                navBarItemConfigurationWizard.Visible = true; // Configuration Wizard
                officeNavigationBarMain.Visible = true;
                //
                navBarItemSpreadsheet.Visible = false; // office Spread sheet
                navBarItemScheduling.Visible = false; // office Scheduling
                navBarItemEmail.Visible = false; // office Email
                navBarItemSendSMS.Visible = false; // office Send SMS
                //
                navBarItemDashboardDesigner.Visible = false; // Settings Dashboard Designer                
            }
        }
        private void barBtnMenuLogout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            logOff();
        }
        //Logoff workstation
        private void logOff()
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            authenticated = false;
            ConfigureScreen();

            //Log logoff transaction in logs
            string st = UserLoggedSurname + " "+ UserLoggedName + " " + LocRM.GetString("strLoggedOff");
            pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
            UI.frmLogin frm = new UI.frmLogin();
           // frm.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ----- Save any updated settings.
            Properties.Settings.Default.Save();
            //In case windows is trying to shut down, don't hold the process up
            // Autosave and clear up ressources
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                //Log logoff transaction in logs
                string st = LocRM.GetString("strShutDown");
                pf.LogFunc(UserLoggedSurname, st);
                return;
            }
            // Assume that X has been clicked and act accordingly.
            // Confirm user wants to close
            // Prompt user to save his data
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (XtraMessageBox.Show(LocRM.GetString("strExiting"), LocRM.GetString("strProgramTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //Log logoff transaction in logs
                      string st = LocRM.GetString("strClosed"); 
                       pf.LogFunc(UserLoggedSurname, st);
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        //Display Student Enrolment 
        
        
        private void studentenrolment()
        {
            try
            {

                //    //navPageMainStudents.Controls.Add(new NavigationFrame(){ Dock = DockStyle.Fill});

                if ((Role == 1) || (Role == 2) || (Role == 4) || (Role == 7)) //Administrator, Administrator Assistant, Account & Admission, Admission Officer
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                    }
                    // Display Student enrolmnet form based on country info when in DRC only
                    //if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
                    //{
                        
                    //    navFrameStudents.SelectedPageIndex = 0; //DRC enrolment form
                    //    navBarStudentsEnrolment.Visible = true;
                    //    navBarStudentsEnrolmentSA.Visible = false;

                    //}
                    //else
                    //{
                    //    navFrameStudents.SelectedPageIndex = 1;
                    //    navBarStudentsEnrolment.Visible = false ;
                    //    navBarStudentsEnrolmentSA.Visible = true;
                    //}
                        //        //navFrameStudents.QueryControl += NavFrameStudents_QueryControl; // raised when navigation frame page changes to display empty content
                        //        //used to add usercontrols to navigation pages

                        //this.ribbonControlMain.MergeRibbon(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentForm).ribbonControl1);
                        //this.ribbonStatusBarMain.MergeStatusBar(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentForm).ribbonStatusBar1);



                        //ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                        //ribbonPageHelp.MergeOrder = 3;
                        if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                }
                else  // doesn't have access to enrolment form, display fees form.
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                    }
                            navFrameStudents.SelectedPageIndex = 2;
                    //        this.ribbonControlMain.MergeRibbon(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlClassFeePayment).ribbonControl1);
                    //        this.ribbonStatusBarMain.MergeStatusBar(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlClassFeePayment).ribbonStatusBar1);
                    //        ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                    //        ribbonPageHelp.MergeOrder = 3;
                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                }

            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void NavFrameStudents_QueryControl(object sender, QueryControlEventArgs e)
        //{
        //   switch (e.Page.Caption)
        //    {
        //        case ("Student Enrolment"):
        //            string test = Properties.Settings.Default.Country;
        //            // Display Student enrolmnet form based on country info when in DRC only
        //            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
        //            {

        //                e.Control = new Students.userControlStudentEnrolmentForm() { Dock = DockStyle.Fill };

        //                //this.ribbonControlMain.MergeRibbon(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentForm).ribbonControl1);
        //                //this.ribbonStatusBarMain.MergeStatusBar(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentForm).ribbonStatusBar1);
        //            }
        //            else
        //            {
        //                e.Control = new Students.userControlStudentEnrolmentFormSA() { Dock = DockStyle.Fill };

        //                //this.ribbonControlMain.MergeRibbon(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentFormSA).ribbonControl1);
        //                //this.ribbonStatusBarMain.MergeStatusBar(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlStudentEnrolmentFormSA).ribbonStatusBar1);
        //            }
        //            //ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
        //            //ribbonPageHelp.MergeOrder = 3;
        //            break;
        //        case ("Student School Fees"):
        //            e.Control = new Students.userControlClassFeePayment() { Dock = DockStyle.Fill };
        //            // e.Control = new MyUserControl2() { Dock = DockStyle.Fill };
        //            break;

        //        case ("Student Class Promotion"):
        //            e.Control = new Students.userControlClassPromotion() { Dock = DockStyle.Fill };
        //            // e.Control = new MyUserControl2() { Dock = DockStyle.Fill };
        //            break;

        //    }
        //}

        

        private void navBarItemEmail_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameOffice.SelectedPageIndex = 4;
                this.ribbonControlMain.MergeRibbon(((navFrameOffice.SelectedPage as NavigationPage).Controls[0] as Office.userControlOfficeEmail).ribbonControl1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 10;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reports()
        {
            try
            {
                if (Role == 1) //Administrator
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                    }
                    navFrameReports.SelectedPageIndex = 0;
                    this.ribbonControlMain.MergeRibbon(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlReportsLogs).ribbonControl1);
                    this.ribbonStatusBarMain.MergeStatusBar(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlReportsLogs).ribbonStatusBar1);
                    ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                    ribbonPageHelp.MergeOrder = 3;
                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                }
                else
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                    }
                    navFrameReports.SelectedPageIndex = 1;
                    this.ribbonControlMain.MergeRibbon(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlListStudents).ribbonControl1);
                    this.ribbonStatusBarMain.MergeStatusBar(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlListStudents).ribbonStatusBar1);
                    ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                    ribbonPageHelp.MergeOrder = 22;
                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                }
                
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void navBarItemLogs_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Reports();            
        }

        
        private void SMSsettings()
        {
            try
            {                
                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                    }
                    navFrameSettings.SelectedPageIndex = 0;
                    this.ribbonControlMain.MergeRibbon(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlSMSsettings).ribbonControl1);
                    this.ribbonStatusBarMain.MergeStatusBar(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlSMSsettings).ribbonStatusBar1);
                    ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                    ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }                
               
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void navBarItemSMS_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            SMSsettings();
        }

        private void navBarItemEmailServer_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameSettings.SelectedPageIndex = 1;                
                this.ribbonControlMain.MergeRibbon(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlEmailSettings).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlEmailSettings).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarItemPreferences_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Preferences();
        }
        private void Preferences()
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameSettings.SelectedPageIndex = 2;
                this.ribbonControlMain.MergeRibbon(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlPreferences).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlPreferences).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExitMain_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
              
             

        private void navBarItemAcademicYear_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navigationFrameAdmin.SelectedPageIndex = 2;
                this.ribbonControlMain.MergeRibbon(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlAcademicYear).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlAcademicYear).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarItemClassFeesEntry_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navigationFrameAdmin.SelectedPageIndex = 3;
                this.ribbonControlMain.MergeRibbon(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlClassFeeEntry).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlClassFeeEntry).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarItemClassSetting_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navigationFrameAdmin.SelectedPageIndex = 4;
                this.ribbonControlMain.MergeRibbon(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlClassSetting).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlClassSetting).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarItemSchoolDetails_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navigationFrameAdmin.SelectedPageIndex = 1;
                this.ribbonControlMain.MergeRibbon(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlSchoolDetails).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlSchoolDetails).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //load list of students
        private void navBarItemListStudents_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameReports.SelectedPageIndex = 1;
                this.ribbonControlMain.MergeRibbon(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlListStudents).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlListStudents).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 22;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void navBarItemPaymentReports_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameReports.SelectedPageIndex = 2;
                this.ribbonControlMain.MergeRibbon(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlFeePaymentRecord).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlFeePaymentRecord).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Select System Info
        private void navBarItemSystemInf_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameSettings.SelectedPageIndex = 3;
                
                this.ribbonControlMain.MergeRibbon(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlSystemInfo).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlSystemInfo).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarItemEmployeeRegistration_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            AdminEmployeeReg();
        }
         //Select Printer settings
        private void navBarItemPrinters_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameSettings.SelectedPageIndex = 4;

                this.ribbonControlMain.MergeRibbon(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlPrinterSettings).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameSettings.SelectedPage as NavigationPage).Controls[0] as Settings.userControlPrinterSettings).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarItemClassPromotion_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //try
            //{
            //    if (splashScreenManagerWait.IsSplashFormVisible == false)
            //    {
            //        splashScreenManagerWait.ShowWaitForm();
            //    }
            //    navFrameStudents.SelectedPageIndex = 3;
            //    // navFrameStudents.QueryControl += NavFrameStudents_QueryControl; // raised when navigation frame page changes to display empty content
            //    //used to add usercontrols to navigation pages
            //    this.ribbonControlMain.MergeRibbon(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlClassPromotion).ribbonControl1);
            //    this.ribbonStatusBarMain.MergeStatusBar(((navFrameStudents.SelectedPage as NavigationPage).Controls[0] as Students.userControlClassPromotion).ribbonStatusBar1);
            //    ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
            //    ribbonPageHelp.MergeOrder = 3;
            //    if (splashScreenManagerWait.IsSplashFormVisible == true)
            //    {
            //        splashScreenManagerWait.CloseWaitForm();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (splashScreenManagerWait.IsSplashFormVisible == true)
            //    {
            //        splashScreenManagerWait.CloseWaitForm();
            //    }
            //    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        //configuration wizards
        private void navBarItemConfigurationWizard_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            // if (splashScreenManagerWait.IsSplashFormVisible == false)
            //  {
            //     splashScreenManagerWait.ShowWaitForm();
            //  }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            UI.ConfigurationWizard configWizard = new UI.ConfigurationWizard();
            configWizard.ShowDialog();
           // if (splashScreenManagerWait.IsSplashFormVisible == true)
           // {
            //    splashScreenManagerWait.CloseWaitForm();
           // }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void navBarItemSendSMS_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameOffice.SelectedPageIndex = 5;
                this.ribbonControlMain.MergeRibbon(((navFrameOffice.SelectedPage as NavigationPage).Controls[0] as Office.userControlOfficeSMS).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameOffice.SelectedPage as NavigationPage).Controls[0] as Office.userControlOfficeSMS).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Sent SMS Reports
        private void navBarItemSMSReports_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navFrameReports.SelectedPageIndex = 3;
                this.ribbonControlMain.MergeRibbon(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlReportsSMS).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlReportsSMS).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Fee Due Reports
        private void navBarItemFeeDue_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
                try
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                    }
                    navFrameReports.SelectedPageIndex = 4;
                    this.ribbonControlMain.MergeRibbon(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlReportsFeeDue).ribbonControl1);
                    this.ribbonStatusBarMain.MergeStatusBar(((navFrameReports.SelectedPage as NavigationPage).Controls[0] as Reports.userControlReportsFeeDue).ribbonStatusBar1);
                    ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                    ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                }
                catch (Exception ex)
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }            
        }

        private void btnBinduWebsite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                Process.Start("http://www.bindu.co.za/");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAbout_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            UI.frmAbout frm = new UI.frmAbout();
            frm.ShowDialog();
        }

        private void btnContactUs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)  
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                Process.Start(String.Format("mailto:{0}?subject={1}", "info@bindu.co.za", LocRM.GetString("strFeedbackCustomer")));
            }
            catch (Exception ex)
            {                
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarBackupRestoreDatabase_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            Admin.frmBackupRestore frm = new Admin.frmBackupRestore();
            frm.ShowDialog();
        }

        //Fess Collected Dashboard
        private void navBarItemFeesCollected_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {

                navFrameDashboard.SelectedPageIndex = 2;
                //if (splashScreenManagerWait.IsSplashFormVisible == false)
                //{
                //    splashScreenManagerWait.ShowWaitForm();
                //}                
                navPageDashboardFeesCollected.Controls.Add(new Dashboard.userControlDashboardFeesCollected() { Dock = DockStyle.Fill });
              
                this.ribbonControlMain.MergeRibbon(((navFrameDashboard.SelectedPage as NavigationPage).Controls[0] as Dashboard.userControlDashboardFeesCollected).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navFrameDashboard.SelectedPage as NavigationPage).Controls[0] as Dashboard.userControlDashboardFeesCollected).ribbonStatusBar1);

                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void navBarItemDashboardDesigner_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            Students.frmStudentDashboardDesigner frmDesigner = new Students.frmStudentDashboardDesigner();
            frmDesigner.Show();
        }

        

        private void navBarStudentsFees_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
            //ribbonPageHelp.MergeOrder = 3;
            //.,mn
        }

        private void navBarStudentsEnrolment_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            //ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
            //ribbonPageHelp.MergeOrder = 3;
        }


        // employee registration
        private void AdminEmployeeReg()
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                navigationFrameAdmin.SelectedPageIndex = 0;
                this.ribbonControlMain.MergeRibbon(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlEmployeeRegistration).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlEmployeeRegistration).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Subjects entry
        private void SubjectsEntry()
        {
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }

                navFrameEducation.SelectedPageIndex = 0;
                this.ribbonControlMain.MergeRibbon(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlEmployeeRegistration).ribbonControl1);
                this.ribbonStatusBarMain.MergeStatusBar(((navigationFrameAdmin.SelectedPage as NavigationPage).Controls[0] as Admin.userControlEmployeeRegistration).ribbonStatusBar1);
                ribbonControlMain.SelectedPage = ribbonControlMain.MergedPages[0];
                ribbonPageHelp.MergeOrder = 3;
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
