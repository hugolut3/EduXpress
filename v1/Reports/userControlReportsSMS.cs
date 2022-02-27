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
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Resources;
using DevExpress.XtraPrinting;
using System.Globalization;

namespace EduXpress.Reports
{
    public partial class userControlReportsSMS : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlReportsLogs).Assembly);

        public userControlReportsSMS()
        {
            InitializeComponent();
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}
        //populate Logs controls
        private void populateLogs()
        {
            gridControlLogs.DataSource = GetData();
            fillCombo();
            //get total number of sent SMS
           // txtNoSentSMS.Text = gridView1.RowCount.ToString();
            CalculateCount();
        }
        //sql connection
        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(databaseConnectionString);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        //Fill dataGridViewLogs
        public DataView GetData()
        {
           
            string DateTimeColumn = LocRM.GetString("strDateTime").ToUpper();
            string SMSMessageColumn = LocRM.GetString("strSMSMessage").ToUpper();
            string SurnameColumn = LocRM.GetString("strSurname").ToUpper();            
            string PhoneNumberColumn = LocRM.GetString("strPhoneNumber").ToUpper();
            

            dynamic SelectQry = "SELECT RTRIM(UserID) as [" + SurnameColumn + "], RTRIM(Operation) as [" + SMSMessageColumn + "], " +
                "RTRIM(PhoneNumber) as [" + PhoneNumberColumn + "],RTRIM(Date) as [" + DateTimeColumn + "]" +
                " from SMSLogs order by Date";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                SqlCommand SampleCommand = new SqlCommand();
                dynamic SampleDataAdapter = new SqlDataAdapter();
                SampleCommand.CommandText = SelectQry;
                SampleCommand.Connection = Connection;
                SampleDataAdapter.SelectCommand = SampleCommand;
                SampleDataAdapter.Fill(SampleSource);
                TableView = SampleSource.Tables[0].DefaultView;
                if ((Connection.State == ConnectionState.Open))
                {
                    Connection.Close();
                    Connection.Dispose();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
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
            return TableView;
        }

        void fillCombo()
        {
            cmbUserType.Properties.Items.Clear();
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string field;
                    cmd = new SqlCommand("SELECT Surname FROM Registration", con);
                    rdr = cmd.ExecuteReader();
                    cmbUserType.Properties.Items.Clear();
                    while (rdr.Read())
                    {
                        field = rdr[0].ToString();
                        cmbUserType.Properties.Items.Add(field);
                    }
                    rdr.Close();
                    con.Close();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
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
       
        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            populateLogs();
            cmbUserType.Enabled = true;
            cmbUserType.SelectedIndex = -1;
            groupDate.Enabled = true;            
        }
        private void CalculateCount()
        {
            try
            {
                gridView1.Columns[LocRM.GetString("strPhoneNumber").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                gridView1.Columns[LocRM.GetString("strPhoneNumber").ToUpper()].SummaryItem.FieldName = "id";
                gridView1.Columns[LocRM.GetString("strPhoneNumber").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strNumberSentSMS").ToUpper()+ ": {0:n0}";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string DateTimeColumn = LocRM.GetString("strDateTime").ToUpper();
                string SMSMessageColumn = LocRM.GetString("strSMSMessage").ToUpper();
                string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
                string PhoneNumberColumn = LocRM.GetString("strPhoneNumber").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT RTRIM(UserID) as [" + SurnameColumn + "], RTRIM(Operation) as [" + SMSMessageColumn + "], " +
                "RTRIM(PhoneNumber) as [" + PhoneNumberColumn + "],RTRIM(Date) as [" + DateTimeColumn + "] from" +
                    " SMSLogs where Date >= @date1 and Date <= @date2 order by Date", con);
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " Date").Value = dtFrom.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " Date").Value = dtTo.DateTime;

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "SMSLogs");
                    gridControlLogs.DataSource = myDataSet.Tables["SMSLogs"].DefaultView;

                    con.Close();
                }
                //get total number of sent SMS
                CalculateCount();
                cmbUserType.SelectedIndex = -1;

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
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

        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string DateTimeColumn = LocRM.GetString("strDateTime").ToUpper();
                string SMSMessageColumn = LocRM.GetString("strSMSMessage").ToUpper();
                string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
                string PhoneNumberColumn = LocRM.GetString("strPhoneNumber").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT RTRIM(UserID) as [" + SurnameColumn + "], RTRIM(Operation) as [" + SMSMessageColumn + "], " +
                "RTRIM(PhoneNumber) as [" + PhoneNumberColumn + "],RTRIM(Date) as [" + DateTimeColumn + "] from" +
                " SMSLogs where UserID=@d1 order by Date", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 30, "UserID").Value = cmbUserType.Text;

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "SMSLogs");
                    gridControlLogs.DataSource = myDataSet.Tables["SMSLogs"].DefaultView;

                    con.Close();
                }
                //get total number of sent SMS
                CalculateCount();

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
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

        private void btnReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            populateLogs();
            cmbUserType.SelectedIndex = -1;
        }

        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;                
                gridView1.OptionsPrint.AutoWidth = true;
                gridView1.OptionsView.ColumnAutoWidth = false;               

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".pdf";
                    //Export to pdf
                    gridControlLogs.ExportToPdf(fileName);
                    gridView1.OptionsView.ColumnAutoWidth = true;
                    // barStaticItemProcess.Caption = "Opening the PDF file...";
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strReportSentSMS"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                        }
                        Process.Start(fileName);

                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
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

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowGridPreview(gridControlLogs);
        }
        //Print Preview datagridview
        private void ShowGridPreview(GridControl grid)
        {
            // Check whether the GridControl can be previewed.
            if (!grid.IsPrintingAvailable)
            {
                XtraMessageBox.Show(LocRM.GetString("strPrintErrorLibraryNotFound"), LocRM.GetString("strPrintError"));
                return;
            }

            try
            {
                // Open the Preview window.
                grid.UseWaitCursor = true;
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                grid.ShowRibbonPrintPreview();
                // grid.ShowPrintPreview();
                grid.UseWaitCursor = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                }
                PrintingSystem printingSystem = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink();
                link.Component = gridControlLogs;
                printingSystem.Links.Add(link);
                link.Print(Properties.Settings.Default.ReportPrinter);
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
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
        //format date time with culture
        List<int> list = new List<int>();
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName != LocRM.GetString("strDateTime").ToUpper()) return;
            int dataSourceRowIndex = view.GetDataSourceRowIndex(1);
            if (list.Contains(dataSourceRowIndex)) return;
            DateTime newDatetime = DateTime.Parse(e.DisplayText, CultureInfo.CurrentCulture);
            e.DisplayText = newDatetime.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
        }

        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            (e.PrintingSystem as PrintingSystemBase).PageSettings.Landscape = true;           
        }

       

        private void userControlReportsSMS_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                gridControlLogs.DataSource = null;
            }
        }
    }
}
