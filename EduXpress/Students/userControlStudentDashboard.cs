using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.IO;
using System.Resources;
using DevExpress.DataAccess.ConnectionParameters;


namespace EduXpress.Students
{
    public partial class userControlStudentDashboard : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlStudentDashboard).Assembly);
        public userControlStudentDashboard()
        {
            InitializeComponent();          
        }
               

        private void loadDashboard()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                //Check if Dashboard XML file exists in database then load it.
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select DashboardStudent from Dashboards ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        //load Dashboard XML from database
                        if (rdr.Read())
                        {
                            if (!Convert.IsDBNull(rdr["DashboardStudent"]))
                            {
                                //if (splashScreenManager1.IsSplashFormVisible == false)
                                //{
                                //    splashScreenManager1.ShowWaitForm();
                                //}

                                byte[] x = (byte[])rdr["DashboardStudent"];
                                using (MemoryStream ms = new MemoryStream(x))
                                {
                                    // Loads a dashboard from an XML file. 
                                    dashboardViewer1.LoadDashboard(ms);                                    
                                }

                                if (splashScreenManager1.IsSplashFormVisible == true)
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                            }
                        }
                    }

                    else
                    {
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        XtraMessageBox.Show(LocRM.GetString("strNoSavedDashboard"), LocRM.GetString("strStudentDashboard"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        
        private void userControlStudentDashboard_Load(object sender, EventArgs e)
        {
            loadDashboard();
          //  dashBoardLoaded = true;
        }
               
        private void btnDashboardDesigner_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            frmStudentDashboardDesigner frm = new frmStudentDashboardDesigner();
            frm.Show();
            Cursor = Cursors.Default;
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void btnRefreshDashboard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadDashboard();
        }

        private void dashboardViewer1_ConfigureDataConnection(object sender, DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs e)
        {   
            string connectionString = "XpoProvider=MSSqlServer;" + databaseConnectionString;

            //assign modified connection parameters to the e.ConnectionParameters property 
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(connectionString);
            e.ConnectionParameters = connectionParameters;

            //  CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(connectionString);
           // DashboardSqlDataSource ds = new DashboardSqlDataSource(connectionParameters);
        }

        private void userControlStudentDashboard_VisibleChanged(object sender, EventArgs e)
        {
            //if (dashBoardLoaded == true)
            //{
            //    loadDashboard();
            //}
        }
    }
}
