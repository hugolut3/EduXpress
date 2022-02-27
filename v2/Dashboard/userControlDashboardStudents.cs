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

namespace EduXpress.Dashboard
{
    public partial class userControlDashboardStudents : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlDashboardStudents).Assembly);

        public userControlDashboardStudents()
        {
            InitializeComponent();
        }
        private void loadDashboard()
        {
            lblDisplay.Visible = false;
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
                      //  dashBoardLoaded = true; //dashboard loaded, next time use the usercontrol visible properties

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
                                     dashboardViewer1.LoadDashboard(ms); //normal loading the dashboard
                                    //dashboardViewer1.AsyncDataLoading(ms);//.LoadDashboard(ms);
                                }

                                if (splashScreenManager1.IsSplashFormVisible == true)
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                            }
                            else
                            {
                                if (splashScreenManager1.IsSplashFormVisible == true)
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                                lblDisplay.Visible = true;
                            }
                        }
                        else
                        {
                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                            lblDisplay.Visible = true;
                        }
                    }

                    else
                    {
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        lblDisplay.Visible = true;
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
        
        private void userControlDashboardStudents_Load(object sender, EventArgs e)
        {
            loadDashboard();            
        }       

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void dashboardViewer1_ConfigureDataConnection(object sender, DevExpress.DashboardCommon.DashboardConfigureDataConnectionEventArgs e)
        {
            string connectionString = "XpoProvider=MSSqlServer;" + databaseConnectionString;

            //assign modified connection parameters to the e.ConnectionParameters property 
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(connectionString);
            e.ConnectionParameters = connectionParameters;
        }

        private void userControlDashboardStudents_VisibleChanged(object sender, EventArgs e)
        {
            //if (dashBoardLoaded == true)
            //{
            //    loadDashboard();
            //}
        }

        private void btnRefreshDashboard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            loadDashboard();
        }

        private void btnOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            lblDisplay.Visible = false;
            using (XtraOpenFileDialog openFileDialog = new XtraOpenFileDialog())
            { 
                openFileDialog.Filter = LocRM.GetString("strDashboardFile") + " (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    dashboardViewer1.LoadDashboard(filePath);
                }
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                con = new SqlConnection(databaseConnectionString);

                con.Open();
                string ct = "select DashboardStudent from Dashboards ";

                cmd = new SqlCommand(ct);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();

                MemoryStream ms = new MemoryStream();
                if (rdr.HasRows)
                {
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                    }

                    string ct1 = "update  Dashboards set DashboardStudent=@d1";
                    cmd = new SqlCommand(ct1);
                    cmd.Connection = con;

                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarBinary);
                    dashboardViewer1.Dashboard.SaveToXml(ms);
                    byte[] dashboardFile = ms.ToArray();
                    cmd.Parameters["@d1"].Value = dashboardFile;

                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    XtraMessageBox.Show(LocRM.GetString("strDashboardUpdated"), LocRM.GetString("strStudentDashboard"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                    }

                    string ct2 = "insert into Dashboards(DashboardStudent) VALUES (@d1)";
                    cmd = new SqlCommand(ct2);
                    cmd.Connection = con;

                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarBinary);
                    // Saves the dashboard to the specified XML file. 
                    // dashboardDesigner1.Dashboard.SaveToXml(filePath);
                    // Saves the dashboard to memory stream. 
                    dashboardViewer1.Dashboard.SaveToXml(ms);
                    byte[] dashboardFile = ms.ToArray();
                    cmd.Parameters["@d1"].Value = dashboardFile;

                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    XtraMessageBox.Show(LocRM.GetString("strDashboardSaved"), LocRM.GetString("strStudentDashboard"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                    ms.Close();
                    ms.Dispose();
                }

            }
            catch (Exception ex)
            {
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    con.Dispose();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
