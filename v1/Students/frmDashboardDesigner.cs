using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using DevExpress.DashboardCommon;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Sql;
using System.IO;
using System.Resources;


namespace EduXpress.Students
{
    public partial class frmStudentDashboardDesigner : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmStudentDashboardDesigner).Assembly);


        public frmStudentDashboardDesigner()
        {
            InitializeComponent();            
        }

        private void frmStudentDashboardDesigner_Load(object sender, EventArgs e)
        {
            //Cursor = Cursors.WaitCursor;
            //timer1.Enabled = true;
            //try
            //{
            //   // splashScreenManager1.ShowWaitForm();

            //    //Bind Dashboard manually to SQL database
            //    CustomStringConnectionParameters customstringParams = new CustomStringConnectionParameters();
            //    customstringParams.ConnectionString = databaseConnectionString;
            //    DashboardSqlDataSource sqlDataSource = new DashboardSqlDataSource("Data Source 1", customstringParams);
            //    SelectQuery selectQuery = SelectQueryFluentBuilder
            //       .AddTable("Students")
            //       .SelectColumns("Section", "StudentNumber", "StudentSurname", "EnrolmentDate", "Class", "Gender", "Age", "Nationality", "HomeLanguage", "Religion", "MedicalAllergies")
            //       .Build("Query 1");
            //    sqlDataSource.Queries.Add(selectQuery);
            //    sqlDataSource.Fill();
            //    // dashboard.DataSources.Add(sqlDataSource);
            //    dashboardDesigner1.Dashboard.DataSources.Add(sqlDataSource);

            //    //splashScreenManager1.CloseWaitForm();

            //    //Check if Dashboard XML file exists in database then load it.
            //    using (con = new SqlConnection(databaseConnectionString))
            //    {
            //        con.Open();
            //        string ct = "select DashboardStudent from Dashboards ";

            //        cmd = new SqlCommand(ct);
            //        cmd.Connection = con;
            //        rdr = cmd.ExecuteReader();

            //        if (rdr.HasRows)
            //        {
            //            //load Dashboard XML from database
            //            if (rdr.Read())
            //            {
            //                if (!Convert.IsDBNull(rdr["DashboardStudent"]))
            //                {
            //                    if (splashScreenManager1.IsSplashFormVisible == false)
            //                    {
            //                        splashScreenManager1.ShowWaitForm();
            //                    }

            //                    byte[] x = (byte[])rdr["DashboardStudent"];
            //                    using (MemoryStream ms = new MemoryStream(x))
            //                    {
            //                        // Loads a dashboard from an XML file. 
            //                        dashboardDesigner1.LoadDashboard(ms);
            //                    }                                    

            //                    if (splashScreenManager1.IsSplashFormVisible == true)
            //                    {
            //                        splashScreenManager1.CloseWaitForm();
            //                    }
            //                }
            //            }
            //        }

            //        else
            //        {
            //            if (splashScreenManager1.IsSplashFormVisible == true)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //            }
            //            Cursor = Cursors.Default;
            //            XtraMessageBox.Show(LocRM.GetString("strNoSavedDashboard"), LocRM.GetString("strStudentDashboard"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //        if ((rdr != null))
            //        {
            //            rdr.Close();
            //        }
            //        if (con.State == ConnectionState.Open)
            //        {
            //            con.Close();
            //        }
            //    }
                    
            //}
            //catch (Exception ex)
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //    Cursor = Cursors.Default;
            //    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void dashboardDesigner1_DashboardSaving(object sender, DevExpress.DashboardWin.DashboardSavingEventArgs e)
        {
          //  Cursor = Cursors.WaitCursor;
          //  timer1.Enabled = true;
          //  try { 
          //  // Determines whether the user has called the Save command. 
          //  if (e.Command == DevExpress.DashboardWin.DashboardSaveCommand.Save)
          //  {
          //          con = new SqlConnection(databaseConnectionString);
                    
          //              con.Open();
          //      string ct = "select DashboardStudent from Dashboards ";

          //      cmd = new SqlCommand(ct);
          //      cmd.Connection = con;
          //      rdr = cmd.ExecuteReader();

          //      MemoryStream ms = new MemoryStream();
          //      if (rdr.HasRows)
          //      {
          //          if ((rdr != null))
          //          {
          //              rdr.Close();
          //          }
          //              if (splashScreenManager1.IsSplashFormVisible == false)
          //              {
          //                  splashScreenManager1.ShowWaitForm();
          //              }

          //          string ct1 = "update  Dashboards set DashboardStudent=@d1";
          //          cmd = new SqlCommand(ct1);
          //          cmd.Connection = con;

          //          cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarBinary);
          //          dashboardDesigner1.Dashboard.SaveToXml(ms);
          //          byte[] dashboardFile = ms.ToArray();
          //          cmd.Parameters["@d1"].Value = dashboardFile;
                    
          //          cmd.ExecuteNonQuery();
          //          con.Close();

          //          // Specifies that the dashboard has been saved and no default actions are required. 
          //          e.Handled = true;
          //          // Specifies that the dashboard has been saved. 
          //          e.Saved = true;
          //              if (splashScreenManager1.IsSplashFormVisible == true)
          //              {
          //                  splashScreenManager1.CloseWaitForm();
          //              }
          //              XtraMessageBox.Show( LocRM.GetString("strDashboardUpdated"), LocRM.GetString("strStudentDashboard"), MessageBoxButtons.OK, MessageBoxIcon.Information);
          //      }

          //      else
          //      {
          //          if ((rdr != null))
          //          {
          //              rdr.Close();
          //          }
          //              if (splashScreenManager1.IsSplashFormVisible == false)
          //              {
          //                  splashScreenManager1.ShowWaitForm();
          //              }

          //              string ct2 = "insert into Dashboards(DashboardStudent) VALUES (@d1)";
          //          cmd = new SqlCommand(ct2);
          //          cmd.Connection = con;

          //          cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarBinary);
          //          // Saves the dashboard to the specified XML file. 
          //          // dashboardDesigner1.Dashboard.SaveToXml(filePath);
          //          // Saves the dashboard to memory stream. 
          //          dashboardDesigner1.Dashboard.SaveToXml(ms);
          //          byte[] dashboardFile = ms.ToArray();
          //          cmd.Parameters["@d1"].Value = dashboardFile;
                   
          //          cmd.ExecuteNonQuery();
          //          con.Close();

          //          // Specifies that the dashboard has been saved and no default actions are required. 
          //          e.Handled = true;
          //          // Specifies that the dashboard has been saved. 
          //           e.Saved = true;

          //              if (splashScreenManager1.IsSplashFormVisible == true)
          //              {
          //                  splashScreenManager1.CloseWaitForm();
          //              }
          //              XtraMessageBox.Show( LocRM.GetString("strDashboardSaved"), LocRM.GetString("strStudentDashboard"), MessageBoxButtons.OK, MessageBoxIcon.Information);
          //      }
          //      if ((rdr != null))
          //      {
          //          rdr.Close();
          //      }
          //      if (con.State == ConnectionState.Open)
          //      {
          //              con.Close();
          //              con.Dispose();
          //              ms.Close();
          //              ms.Dispose();
          //      }               
          //  }
          //  // Determines whether the user has called the Save As command. 
          //  if (e.Command == DevExpress.DashboardWin.DashboardSaveCommand.SaveAs)
          //  {
          ////      DialogResult result = InvokeMessageBox();
          ////      if (result == System.Windows.Forms.DialogResult.OK)
          // //     {
          ////          dashboardDesigner1.Dashboard.SaveToXml(filePath);
          ////          e.Handled = true;

          //          // Specifies that the dashboard has been saved. 
          ////          e.Saved = true;
          ////      }
          ////      if (result == System.Windows.Forms.DialogResult.Cancel)
          ////      {
          ////          e.Handled = true;
          ////          e.Saved = false;
          ////      }
          //  }
          //  }
          //  catch (Exception ex)
          //  {
          //      if ((rdr != null))
          //      {
          //          rdr.Close();
          //      }
          //      if (con.State == ConnectionState.Open)
          //      {
          //          con.Close();
          //          con.Dispose();
          //      }

          //      if (splashScreenManager1.IsSplashFormVisible == true)
          //      {
          //          splashScreenManager1.CloseWaitForm();
          //      }

          //      XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
          //  }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void dashboardDesigner1_ConfigureDataConnection(object sender, DashboardConfigureDataConnectionEventArgs e)
        {
            string connectionString = "XpoProvider=MSSqlServer;" + databaseConnectionString;

            //assign modified connection parameters to the e.ConnectionParameters property 
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(connectionString);
            e.ConnectionParameters = connectionParameters;
        }
    }
}