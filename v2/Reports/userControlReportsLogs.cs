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
using DevExpress.XtraEditors.Repository;


namespace EduXpress.Reports
{
    public partial class userControlReportsLogs : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlReportsLogs).Assembly);

        public userControlReportsLogs()
        {
            InitializeComponent();
        }
        
        public void DeleteRecord()
        {
            try
            {
                int RowsAffected = 0;
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "delete from logs";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                        }
                       // con = new SqlConnection(databaseConnectionString);                        
                       // con.Open();

                        //Log delete logs transaction in logs
                        //string st =  LocRM.GetString("strAllLogsDeleted") + ": '" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "'";
                        string st = LocRM.GetString("strAllLogsDeleted") + ": '" + DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture) + "'";
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strRecord"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                        gridControlLogs.DataSource = GetData();
                        cmbUserType.SelectedIndex = -1;
                        // barStaticItemProcess.Caption = "All event logs deleted successfully";
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strRecord"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                        //  barStaticItemProcess.Caption = "No event log deleted";
                    }
                    con.Close();
                }
                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Create a MemoEdit editor that shows event details text wrapped
        private void memoInGrid()
        {
            RepositoryItemMemoEdit riMemoEdit = new RepositoryItemMemoEdit();
            riMemoEdit.WordWrap = true;
            gridControlLogs.RepositoryItems.Add(riMemoEdit);
            gridView1.Columns[1].ColumnEdit = riMemoEdit;
            gridView1.Columns[2].ColumnEdit = riMemoEdit;
            gridView1.Columns[3].ColumnEdit = riMemoEdit;

            gridView1.BestFitColumns();
            //gridView1.Columns[2].Width = Unit.Percentage(50);
            //column.Width= Unit.Percentage(50);
            //Set gridview OptionView RowAutoHeight = True as well
        }
        //populate Logs controls
        private void populateLogs()
        {
            gridControlLogs.DataSource = GetData();
            memoInGrid();
            fillCombo();
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
            // string DateTimeColumn = LocRM.GetString("strDateTime").ToUpper();
            string DateTimeColumn = LocRM.GetString("strDateTime").ToUpper();
            string EventColumn = LocRM.GetString("strEvent").ToUpper();
            string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            string ComputerNameColumn = LocRM.GetString("strComputerName").ToUpper();

            dynamic SelectQry = "SELECT RTRIM(UserID) as [" + SurnameColumn + "],Date as [" + DateTimeColumn + "]," +
                "RTRIM(Operation) as [" + EventColumn + "],RTRIM(Location) as [" + ComputerNameColumn + "] from Logs order by Date";
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
        void Reset()
        {
            cmbUserType.SelectedIndex = -1;
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
            GetData();
            
            fillCombo();
        }

        private void cmbUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear gridcontrol
            gridControlLogs.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            gridControlLogs.DataSource = CreateDataTable();
            loadLogsUser();

            memoInGrid();

            #region MyRegion
            //try
            // {
            //    if (splashScreenManager1.IsSplashFormVisible == false)
            //    {
            //        splashScreenManager1.ShowWaitForm();
            //    }
            //    string DateTimeColumn = LocRM.GetString("strDateTime").ToUpper();
            //    string EventColumn = LocRM.GetString("strEvent").ToUpper();
            //    string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            //    string ComputerNameColumn = LocRM.GetString("strComputerName").ToUpper();

            //    using (con = new SqlConnection(databaseConnectionString))
            //    {
            //        con.Open();
            //        cmd = new SqlCommand("SELECT RTRIM(UserID) as [" + SurnameColumn + "],RTRIM(Date) as [" + DateTimeColumn + "]," +
            //    "RTRIM(Operation) as [" + EventColumn + "],RTRIM(Location) as [" + ComputerNameColumn + "] from" +
            //    " Logs where UserID=@d1 order by Date", con);
            //        cmd.Parameters.Add("@d1", SqlDbType.NChar, 30, "UserID").Value = cmbUserType.Text;

            //        SqlDataAdapter myDA = new SqlDataAdapter(cmd);
            //        DataSet myDataSet = new DataSet();

            //        myDA.Fill(myDataSet, "Logs");
            //        gridControlLogs.DataSource = myDataSet.Tables["Logs"].DefaultView;

            //        con.Close();
            //    }

            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //    }
            //    catch (Exception ex)
            //    {
            //        if (splashScreenManager1.IsSplashFormVisible == true)
            //        {
            //            splashScreenManager1.CloseWaitForm();
            //        }
            //        XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //memoInGrid(); 
            #endregion
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            //clear gridcontrol
            gridControlLogs.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            gridControlLogs.DataSource = CreateDataTable();
            loadLogsDate();

            memoInGrid();           


            #region MyRegion
            //try
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == false)
            //    {
            //        splashScreenManager1.ShowWaitForm();
            //    }
            //    string DateTimeColumn = LocRM.GetString("strDateTime").ToUpper();
            //    string EventColumn = LocRM.GetString("strEvent").ToUpper();
            //    string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            //    string ComputerNameColumn = LocRM.GetString("strComputerName").ToUpper();

            //    using (con = new SqlConnection(databaseConnectionString))
            //    {
            //        con.Open();
            //        cmd = new SqlCommand("SELECT RTRIM(UserID) as [" + SurnameColumn + "],RTRIM(Date) as [" + DateTimeColumn + "]," +
            //        "RTRIM(Operation) as [" + EventColumn + "],RTRIM(Location) as [" + ComputerNameColumn + "] from" +
            //        " Logs where Date >= @date1 and Date <= @date2 order by Date", con);
            //        cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " Date").Value = dtFrom.DateTime;
            //        cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " Date").Value = dtTo.DateTime;

            //        SqlDataAdapter myDA = new SqlDataAdapter(cmd);
            //        DataSet myDataSet = new DataSet();

            //        myDA.Fill(myDataSet, "Logs");
            //        gridControlLogs.DataSource = myDataSet.Tables["Logs"].DefaultView;

            //        con.Close();
            //    }
            //    cmbUserType.SelectedIndex = -1;
            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
            
        }

       

        private void btnReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //clear gridcontrol
            gridControlLogs.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            gridControlLogs.DataSource = CreateDataTable();
            //loadLogs();

            memoInGrid();
            fillCombo();
            // gridView1.OptionsView.ColumnAutoWidth = false;           

            //populateLogs();
            cmbUserType.SelectedIndex = -1;
            //  barStaticItemProcess.Caption = "User type reset";
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = true;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".xlsx";
                    //Export to excel
                    gridControlLogs.ExportToXlsx(fileName);
                   // barStaticItemProcess.Caption = "Opening the Excel file...";
                    if (XtraMessageBox.Show( LocRM.GetString("strWantOpenFile"), LocRM.GetString("strEventLogs") , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                   // barStaticItemProcess.Caption = "event logs exported to Excel file";
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

        private void btnDeleteLogs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show( LocRM.GetString("strDeleteConfirmLogs"), LocRM.GetString("strDeleteConfirm")  , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    DeleteRecord();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
           
        //}

        private void btnLoadLogs_Click(object sender, EventArgs e)
        {
            // populateLogs();
            //clear gridcontrol
            gridControlLogs.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            gridControlLogs.DataSource = CreateDataTable();
            loadLogs();

            memoInGrid();
            fillCombo();
           // gridView1.OptionsView.ColumnAutoWidth = false;

            cmbUserType.Enabled = true;
            cmbUserType.SelectedIndex = -1;
            groupDate.Enabled = true;
            //gridView1.OptionsPrint.AutoWidth = false;
        }

        //GridViewLogs
        private DataTable CreateDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strDateTime").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strEvent").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strComputerName").ToUpper(), typeof(string));
            return dt;
        }

        private void loadLogs()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strLoading"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(UserID),Date, RTRIM(Operation),RTRIM(Location) from Logs order by Date";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;                   

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[0].ToString());
                        DateTime dt = (DateTime)rdr.GetValue(1);
                        //string time1 = dt.ToString("dd/MM/yyyy HH:mm:ss");
                        //string time2 = dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateTime").ToUpper()], dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture));
                        
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strEvent").ToUpper()], rdr[2].ToString());                        
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strComputerName").ToUpper()], rdr[3].ToString());
                        
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void loadLogsDate()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strLoading"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(UserID),Date, RTRIM(Operation),RTRIM(Location) from Logs where Date >= @date1 and Date <= @date2 order by Date";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " Date").Value = dtFrom.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " Date").Value = dtTo.DateTime;

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[0].ToString());
                        DateTime dt = (DateTime)rdr.GetValue(1);
                        //string time1 = dt.ToString("dd/MM/yyyy HH:mm:ss");
                        //string time2 = dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateTime").ToUpper()], dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture));

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strEvent").ToUpper()], rdr[2].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strComputerName").ToUpper()], rdr[3].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void loadLogsUser()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strLoading"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(UserID),Date, RTRIM(Operation),RTRIM(Location) from Logs where UserID=@d1 order by Date";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, "UserID").Value = cmbUserType.Text;

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[0].ToString());
                        DateTime dt = (DateTime)rdr.GetValue(1);
                        //string time1 = dt.ToString("dd/MM/yyyy HH:mm:ss");
                        //string time2 = dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDateTime").ToUpper()], dt.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture));

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strEvent").ToUpper()], rdr[2].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strComputerName").ToUpper()], rdr[3].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
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
        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                //To export to pdf show all row witdh, disable  gridView1.OptionsPrint.AutoWidth, WordWrap option enabled for your RepositoryItemMemoEdit.
                //RepositoryItemMemoEdit.Appearance.TextOptions.WordWrap
                //GridView.AppearancePrint.Row.TextOptions.WordWrap
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = true;
               // gridView1.AppearancePrint.Row.TextOptions.WordWrap= DevExpress.Utils.WordWrap.Wrap;


                //gridView1.au

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".pdf";
                    //Export to excel
                    gridControlLogs.ExportToPdf(fileName);
                   // barStaticItemProcess.Caption = "Opening the PDF file...";
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strEventLogs"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                   // barStaticItemProcess.Caption = "event logs exported to PDF file";
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

        private void btnExportWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = true;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".docx";
                    //Export to excel
                    gridControlLogs.ExportToDocx(fileName);
                   // barStaticItemProcess.Caption = "Opening the Docx file...";
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strEventLogs"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                   // barStaticItemProcess.Caption = "event logs exported to Docx file";
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
                XtraMessageBox.Show( LocRM.GetString("strPrintErrorLibraryNotFound"), LocRM.GetString("strPrintError")  );
                return;
            }

            try
            {
                // Open the Preview window.
                grid.UseWaitCursor = true;
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = true;
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
        //    GridView view = sender as GridView;
        //    if (e.Column.FieldName != LocRM.GetString("strDateTime").ToUpper()) return;
        //    int dataSourceRowIndex = view.GetDataSourceRowIndex(1);
        //    if (list.Contains(dataSourceRowIndex)) return;

        //    //DateTime newDatetime = DateTime.Parse(e.DisplayText, CultureInfo.CurrentCulture);
        //    DateTime newDatetime = DateTime.Parse(e.DisplayText);
        //    // e.DisplayText = newDatetime.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
        //    e.DisplayText = newDatetime.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture);
        }

        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            (e.PrintingSystem as PrintingSystemBase).PageSettings.Landscape = true;
        }
    }
}
