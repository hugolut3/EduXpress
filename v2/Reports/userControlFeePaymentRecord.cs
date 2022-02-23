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
using System.Globalization;
using System.Threading;
using System.Resources;
using DevExpress.XtraPrinting;
using EduXpress.Functions;
using static EduXpress.Functions.PublicVariables;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace EduXpress.Reports
{
    public partial class userControlFeePaymentRecord : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
       // CultureInfo cultureToUse = CultureInfo.InvariantCulture;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlFeePaymentRecord).Assembly);


        public userControlFeePaymentRecord()
        {
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            InitializeComponent();
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void resetPaymentFilter()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID " +
                        "order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;                   

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        DateTime dt = (DateTime)rdr.GetValue(9);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPaymentDate").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

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
        //populate Reports
        private void populateReports()
        {
            //clear gridcontrol
            gridControlFeePayments.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            //load culumns in gridControlFeeInfo
            gridControlFeePayments.DataSource = CreateDataFeesPaymentReset();

            resetPaymentFilter();
            CalculateCount();

            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            ribbonPageGroupPrint.Enabled = false;
            ribbonPageGroupExport.Enabled = false;
            // gridControlFeePayments.DataSource = GetData();
           
           
           // calculateTotalPaid();
        }
        ////sql connection
        //private SqlConnection Connection
        //{
        //    get
        //    {
        //        SqlConnection ConnectionToFetch = new SqlConnection(databaseConnectionString);
        //        ConnectionToFetch.Open();
        //        return ConnectionToFetch;
        //    }
        //}
        //Fill dataGridViewLogs
        //public DataView GetData()
        //{
            ////string ReceiptNoColumn = LocRM.GetString("strReceiptNo").ToUpper();
            //string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
            //string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            //string NameColumn = LocRM.GetString("strName").ToUpper();
            ////string SectionColumn = LocRM.GetString("strSection").ToUpper();
            //string ClassColumn = LocRM.GetString("strClass").ToUpper();
            ////string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
            ////string TotalFeeColumn = LocRM.GetString("strTotalFee").ToUpper();
            ////string DiscountPercColumn = LocRM.GetString("strDiscountPerc").ToUpper();
            ////string DiscountColumn = LocRM.GetString("strDiscount").ToUpper();
            //string PreviousDueColumn = LocRM.GetString("strPreviousDue").ToUpper();
            ////string FineColumn = LocRM.GetString("strFine").ToUpper();
            //string TotalDueColumn = LocRM.GetString("strTotalDue").ToUpper();
            //string TotalPaidColumn = LocRM.GetString("strTotalPaid").ToUpper();
            //string ModeOfPaymentColumn = LocRM.GetString("strModeOfPayment").ToUpper();
            //string BalanceColumn = LocRM.GetString("strBalance").ToUpper();
            //string PaymentDateColumn = LocRM.GetString("strPaymentDate").ToUpper();
            //string CashierColumn = LocRM.GetString("strCashier").ToUpper();

            //dynamic SelectQry = "SELECT RTRIM(CourseFeePaymentID) as [ID],RTRIM(Students.StudentNumber) as [" + StudentNoColumn + "]," +
            //    "RTRIM(StudentSurname) as [" + SurnameColumn + "], RTRIM(StudentFirstNames) as [" + NameColumn + "]," +
            //    "RTRIM(Class) as [" + ClassColumn + "], TotalPaid as [" + TotalPaidColumn + "],GrandTotal as [" + TotalDueColumn + "]," +
            //    "PaymentDue as [" + BalanceColumn + "],PreviousDue as [" + PreviousDueColumn + "] ," +
            //    "RTRIM(ModeOfPayment) as [" + ModeOfPaymentColumn + "], PaymentDate as [" + PaymentDateColumn + "]," +
            //    "RTRIM(Employee) as [" + CashierColumn + "]  from Students, " +
            //    "CourseFeePayment where Students.StudentNumber=CourseFeePayment.StudentNumber order by PaymentDate";
            //DataSet SampleSource = new DataSet();
            //DataView TableView = null;            
            //try
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == false)
            //    {
            //        splashScreenManager1.ShowWaitForm();
            //    }
            //    SqlCommand SampleCommand = new SqlCommand();
            //    dynamic SampleDataAdapter = new SqlDataAdapter();
            //    SampleCommand.CommandText = SelectQry;
            //    SampleCommand.Connection = Connection;
            //    SampleDataAdapter.SelectCommand = SampleCommand;
            //    SampleDataAdapter.Fill(SampleSource);
            //    TableView = SampleSource.Tables[0].DefaultView;
            //    if ((Connection.State == ConnectionState.Open))
            //    {
            //        Connection.Close();
            //        Connection.Dispose();
            //    }
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
            //return TableView;
        //}
        
        void fillComboCycle()
        {
            cmbCycleClass.Properties.Items.Clear();
            cmbCycle.Properties.Items.Clear();
            cmbCycleSection.Properties.Items.Clear();
            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                cmbCycleClass.Properties.Items.AddRange(new object[]
            {
                LocRM.GetString("strMaternelle"),
                LocRM.GetString("strPrimaire") ,
                LocRM.GetString("strSecondOrientation"),
                LocRM.GetString("strSecondHuma"),
                LocRM.GetString("strTVETCollege")
            });

                cmbCycle.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("strMaternelle"),
                LocRM.GetString("strPrimaire") ,
                LocRM.GetString("strSecondOrientation"),
                LocRM.GetString("strSecondHuma"),
                LocRM.GetString("strTVETCollege")
                });

                cmbCycleSection.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("strMaternelle"),
                LocRM.GetString("strPrimaire") ,
                LocRM.GetString("strSecondOrientation"),
                LocRM.GetString("strSecondHuma"),
                LocRM.GetString("strTVETCollege")
                });

            }
            else
            {
                cmbCycleClass.Properties.Items.AddRange(new object[]
            {
                LocRM.GetString("strPrePrimary"),
                LocRM.GetString("strPreparatory") ,
                LocRM.GetString("strHighSchoolGET"),
                LocRM.GetString("strHighSchoolFET"),
                LocRM.GetString("strTVETCollege")
            });

                cmbCycle.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("strPrePrimary"),
                LocRM.GetString("strPreparatory") ,
                LocRM.GetString("strHighSchoolGET"),
                LocRM.GetString("strHighSchoolFET"),
                LocRM.GetString("strTVETCollege")
                });

                cmbCycleSection.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("strPrePrimary"),
                LocRM.GetString("strPreparatory") ,
                LocRM.GetString("strHighSchoolGET"),
                LocRM.GetString("strHighSchoolFET"),
                LocRM.GetString("strTVETCollege")
                });
            }
        }
        void fillComboEmployee()
        {
            comboByEmployeeDate.Properties.Items.Clear();            
            comboByEmployeeDefinedDate.Properties.Items.Clear();

            cmbByEmployeeCycle.Properties.Items.Clear();
            cmbByEmployeeSection.Properties.Items.Clear();
            comboByEmployeeClass.Properties.Items.Clear();
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
                    //SqlDataReader dr;
                    rdr = cmd.ExecuteReader();
                    comboByEmployeeDate.Properties.Items.Add(LocRM.GetString("strAll"));
                    comboByEmployeeDefinedDate.Properties.Items.Add(LocRM.GetString("strAll"));                   
                    cmbByEmployeeCycle.Properties.Items.Add(LocRM.GetString("strAll"));
                    cmbByEmployeeSection.Properties.Items.Add(LocRM.GetString("strAll"));
                    comboByEmployeeClass.Properties.Items.Add(LocRM.GetString("strAll"));
                    while (rdr.Read())
                    {
                        field = rdr[0].ToString();
                        comboByEmployeeDate.Properties.Items.Add(field);
                        comboByEmployeeDefinedDate.Properties.Items.Add(field);                        

                        cmbByEmployeeCycle.Properties.Items.Add(field);
                        cmbByEmployeeSection.Properties.Items.Add(field);
                        comboByEmployeeClass.Properties.Items.Add(field);
                    }
                    comboByEmployeeDate.SelectedIndex = 0;
                    comboByEmployeeDefinedDate.SelectedIndex = 0;

                    cmbByEmployeeCycle.SelectedIndex = 0;
                    cmbByEmployeeSection.SelectedIndex = 0;
                    comboByEmployeeClass.SelectedIndex = 0;
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
            comboByEmployeeDate.SelectedIndex = -1;
            comboByEmployeeDefinedDate.SelectedIndex = -1;            
            comboDefinedDate.SelectedIndex = -1;
            
            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;

            cmbCycle.SelectedIndex = -1;
            dtFromCycle.EditValue = DateTime.Today;
            dtToCycle.EditValue = DateTime.Today;
            cmbByEmployeeCycle.SelectedIndex = -1;

            cmbCycleSection.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            dtFromSection.EditValue = DateTime.Today;
            dtToSection.EditValue = DateTime.Today;
            cmbByEmployeeSection.SelectedIndex = -1;

            cmbCycleClass.SelectedIndex = -1;
            cmbSectionClass.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            dtClassFrom.EditValue = DateTime.Today;
            dtClassTo.EditValue = DateTime.Today;
            comboByEmployeeClass.SelectedIndex = -1;
        }

        private void filterPaymentCustomDateAll()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate >= @date1 and PaymentDate <= @date2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " PaymentDate").Value = dtFrom.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " PaymentDate").Value = dtTo.DateTime;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                searchbyDateFrom = dtFrom.DateTime.ToString("dd/MM/yyyy");
                searchbyDateTo = dtTo.DateTime.ToString("dd/MM/yyyy");

                studentSearchBy = 11;
                //searchBy = 11;

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
        private void filterPaymentCustomDateCashier()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate >= @d1 and PaymentDate <= @d2 and Employee=@d3 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, " PaymentDate").Value = dtFrom.DateTime;
                    cmd.Parameters.Add("@d2", SqlDbType.Date, 30, " PaymentDate").Value = dtTo.DateTime;
                    cmd.Parameters.Add("@d3", SqlDbType.NChar, 80, "Employee").Value = comboByEmployeeDate.Text.Trim();

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                searchbyDateFrom = dtFrom.DateTime.ToString("dd/MM/yyyy");
                searchbyDateTo = dtTo.DateTime.ToString("dd/MM/yyyy");
                searchbyCashier = comboByEmployeeDate.Text.Trim();
                studentSearchBy = 12;
                //searchBy = 12;

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
        private void btnGetDataDate_Click(object sender, EventArgs e)
        {
            if (comboByEmployeeDate.Text == "") 
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectReportByEmployee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboByEmployeeDate.Focus();
                return;
            }
            //clear gridcontrol
            gridControlFeePayments.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();


            //load culumns in gridControlFeeInfo
            gridControlFeePayments.DataSource = CreateDataFeesPayment();

            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            if (comboByEmployeeDate.Text == LocRM.GetString("strAll"))
            {
                try
                {
                    filterPaymentCustomDateAll();
                    CalculateCount();

                    comboDefinedDate.SelectedIndex = -1;
                    comboByEmployeeDefinedDate.SelectedIndex = -1;                    

                    cmbCycle.SelectedIndex = -1;
                    dtFromCycle.EditValue = DateTime.Today;
                    dtToCycle.EditValue = DateTime.Today;
                    cmbByEmployeeCycle.SelectedIndex = -1;

                    cmbCycleSection.SelectedIndex = -1;
                    cmbSection.SelectedIndex = -1;
                    dtFromSection.EditValue = DateTime.Today;
                    dtToSection.EditValue = DateTime.Today;
                    cmbByEmployeeSection.SelectedIndex = -1;

                    cmbCycleClass.SelectedIndex = -1;
                    cmbSectionClass.SelectedIndex = -1;
                    cmbClass.SelectedIndex = -1;
                    dtClassFrom.EditValue = DateTime.Today;
                    dtClassTo.EditValue = DateTime.Today;
                    comboByEmployeeClass.SelectedIndex = -1;
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
            else
            {
                try
                {
                    filterPaymentCustomDateCashier();
                    CalculateCount();

                    comboDefinedDate.SelectedIndex = -1;
                    comboByEmployeeDefinedDate.SelectedIndex = -1;

                    cmbCycle.SelectedIndex = -1;
                    dtFromCycle.EditValue = DateTime.Today;
                    dtToCycle.EditValue = DateTime.Today;
                    cmbByEmployeeCycle.SelectedIndex = -1;

                    cmbCycleSection.SelectedIndex = -1;
                    cmbSection.SelectedIndex = -1;
                    dtFromSection.EditValue = DateTime.Today;
                    dtToSection.EditValue = DateTime.Today;
                    cmbByEmployeeSection.SelectedIndex = -1;

                    cmbCycleClass.SelectedIndex = -1;
                    cmbSectionClass.SelectedIndex = -1;
                    cmbClass.SelectedIndex = -1;
                    dtClassFrom.EditValue = DateTime.Today;
                    dtClassTo.EditValue = DateTime.Today;
                    comboByEmployeeClass.SelectedIndex = -1;
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
        }

        private void btnReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            populateReports();
            Reset();
            //ribbonPageGroupPrint.Enabled = false;
            //ribbonPageGroupExport.Enabled = false;
            // barStaticItemProcess.Caption = "User type reset";
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                //Cursor = Cursors.WaitCursor;
                //timer1.Enabled = true;
                try
                {
                    gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;

                    // Create a new report instance.
                    reportFeePayment report = new reportFeePayment();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlFeePayments;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Rename worksheet
                        report.CreateDocument(false);

                        string fileName = saveFileDialog.FileName + ".xlsx";
                        //Export to excel
                        report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = LocRM.GetString("strstrPaymentReport") });

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strstrPaymentReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region oldExport
            //try
            //{
            //    gridView1.BestFitColumns();
            //    gridView1.OptionsPrint.AllowMultilineHeaders = true;
            //    gridView1.OptionsPrint.AutoWidth = false;

            //    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
            //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string fileName = saveFileDialog.FileName + ".xlsx";
            //        //Export to excel
            //        gridControlFeePayments.ExportToXlsx(fileName);
            //       // barStaticItemProcess.Caption = "Opening the Excel file...";
            //        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile")  , LocRM.GetString("strstrPaymentReport") , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            if (splashScreenManager1.IsSplashFormVisible == false)
            //            {
            //                splashScreenManager1.ShowWaitForm();
            //            }
            //            Process.Start(fileName);
            //            if (splashScreenManager1.IsSplashFormVisible == true)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //            }
            //        }
            //       // barStaticItemProcess.Caption = "Payment Report exported to Excel file";
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
       

        private void CalculateCount()
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    //Display sum of total paid
                    gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()].SummaryItem.FieldName = gridView1.Columns[4].ToString();
                    gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strTotalPaid").ToUpper() + ": {0:n2}";

                    //set width of count column to fixed
                    gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()].OptionsColumn.FixedWidth = true;
                    gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()].Width = 130;
                    gridView1.Columns[LocRM.GetString("strNo").ToUpper()].OptionsColumn.FixedWidth = true;
                    gridView1.Columns[LocRM.GetString("strNo").ToUpper()].Width = 30;

                    //Alighn left columns with money
                    gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    gridView1.Columns[LocRM.GetString("strBalance").ToUpper()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    //refresh summary
                    gridView1.UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //Fill Defined Date
        private void fillDefinedDate()
        {
            comboDefinedDate.Properties.Items.Clear();
            comboDefinedDate.Properties.Items.AddRange(new object[] { LocRM.GetString("strToday"),
            LocRM.GetString("strYesterday") , LocRM.GetString("strThisWeek"),
            LocRM.GetString("strLastWeek"),LocRM.GetString("strThisMonth") });
            comboDefinedDate.SelectedIndex = -1;
        }

        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                //Cursor = Cursors.WaitCursor;
                //timer1.Enabled = true;
                try
                {
                    // gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    // Create a new report instance.
                    reportFeePayment report = new reportFeePayment();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlFeePayments;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        string fileName = saveFileDialog.FileName + ".pdf";
                        //Export to pdf
                        report.ExportToPdf(fileName);

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strstrPaymentReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #region OldExportPDF
            //try
            //{
            //    gridView1.BestFitColumns();
            //    gridView1.OptionsPrint.AllowMultilineHeaders = true;
            //    gridView1.OptionsPrint.AutoWidth = false;
            //    // gridView1.OptionsView.ColumnAutoWidth = false;

            //    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
            //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string fileName = saveFileDialog.FileName + ".pdf";
            //        //Export to pdf
            //        gridControlFeePayments.ExportToPdf(fileName);
            //        //barStaticItemProcess.Caption = "Opening the PDF file...";
            //        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strstrPaymentReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            if (splashScreenManager1.IsSplashFormVisible == false)
            //            {
            //                splashScreenManager1.ShowWaitForm();
            //            }
            //            Process.Start(fileName);
            //            if (splashScreenManager1.IsSplashFormVisible == true)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //            }
            //        }
            //       // barStaticItemProcess.Caption = "Payment Report exported to PDF file";
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

        private void btnExportWord_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                //Cursor = Cursors.WaitCursor;
                //timer1.Enabled = true;
                try
                {
                    //gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    // Create a new report instance.
                    reportFeePayment report = new reportFeePayment();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlFeePayments;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        string fileName = saveFileDialog.FileName + ".docx";
                        //Export to pdf
                        report.ExportToDocx(fileName);


                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strstrPaymentReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            #region OldExportWord
            //try
            //{
            //    gridView1.BestFitColumns();
            //    gridView1.OptionsPrint.AllowMultilineHeaders = true;
            //    gridView1.OptionsPrint.AutoWidth = false;
            //    // gridView1.OptionsView.ColumnAutoWidth = false;

            //    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
            //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string fileName = saveFileDialog.FileName + ".docx";
            //        //Export to word
            //        gridControlFeePayments.ExportToDocx(fileName);
            //       // barStaticItemProcess.Caption = "Opening the Docx file...";
            //        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strstrPaymentReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            if (splashScreenManager1.IsSplashFormVisible == false)
            //            {
            //                splashScreenManager1.ShowWaitForm();
            //            }
            //            Process.Start(fileName);
            //            if (splashScreenManager1.IsSplashFormVisible == true)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //            }
            //        }
            //       // barStaticItemProcess.Caption = "Payment Report exported to Docx file";
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

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                // Create a new report instance.
                reportFeePayment report = new reportFeePayment();

                // Link the required control with the PrintableComponentContainers of a report.
                report.printableComponentContainer1.PrintableComponent = gridControlFeePayments;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                report.Landscape = true;

                // Invoke a Print Preview for the created report document. 
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();
            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPreview"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //ShowGridPreview(gridControlFeePayments);
        }
        //Print Preview datagridview
        private void ShowGridPreview(GridControl grid)
        {
            //// Check whether the GridControl can be previewed.
            //if (!grid.IsPrintingAvailable)
            //{
            //    XtraMessageBox.Show(LocRM.GetString("strPrintErrorLibraryNotFound"), LocRM.GetString("strPrintError"));
            //    return; 
            //}

            //// Open the Preview window.
            //grid.UseWaitCursor = true;
            //gridView1.BestFitColumns();
            //gridView1.OptionsPrint.AllowMultilineHeaders = true;
            //gridView1.OptionsPrint.AutoWidth = false;
            //grid.ShowRibbonPrintPreview();
            //grid.UseWaitCursor = false;
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                try
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                    }

                    // XtraReport.PrinterName
                    // Create a new report instance.
                    reportFeePayment report = new reportFeePayment();
                    PrinterSettings printerSettings = new PrinterSettings();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlFeePayments;

                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    report.Landscape = true;

                    // Specify the PrinterName if the target printer is not the default one.
                    printerSettings.PrinterName = Properties.Settings.Default.ReportPrinter;

                    // Invoke a Print Preview for the created report document. 
                    ReportPrintTool printTool = new ReportPrintTool(report);
                   
                    printTool.Print(Properties.Settings.Default.ReportPrinter);

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

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPrint"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #region OldCode
            //PrintGrid(gridControlFeePayments);
            //try
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == false)
            //    {
            //        splashScreenManager1.ShowWaitForm();
            //        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
            //    }
            //    PrintingSystem printingSystem = new PrintingSystem();
            //    PrintableComponentLink link = new PrintableComponentLink();
            //    link.Component = gridControlFeePayments;
            //    printingSystem.Links.Add(link);
            //    link.Print(Properties.Settings.Default.ReportPrinter);
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

        private void filterPaymentClassAll()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and Class=@d1 " +
                        "and PaymentDate >= @date1 and PaymentDate <= @date2  order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, "Class").Value = cmbClass.Text.Trim();
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " PaymentDate").Value = dtClassFrom.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " PaymentDate").Value = dtClassTo.DateTime;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        DateTime dt = (DateTime)rdr.GetValue(9);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPaymentDate").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                className = cmbClass.Text;

                studentSearchBy = 13;
                //searchBy = 13;

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
        private void filterPaymentClassCashier()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and Class=@d1 and Employee=@d2 " +
                        "and PaymentDate >= @date1 and PaymentDate <= @date2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, "Class").Value = cmbClass.Text;
                    cmd.Parameters.Add("@d2", SqlDbType.NChar, 80, "Employee").Value = comboByEmployeeClass.Text.Trim();
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " PaymentDate").Value = dtClassFrom.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " PaymentDate").Value = dtClassTo.DateTime;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        DateTime dt = (DateTime)rdr.GetValue(9);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPaymentDate").ToUpper()], dt.ToString("dd/MM/yyyy"));
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                className = cmbClass.Text;
                searchbyCashier = comboByEmployeeClass.Text.Trim();
                studentSearchBy = 14;
               // searchBy = 14;

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
        private void btnGetDataClass_Click(object sender, EventArgs e)
        {
            if (comboByEmployeeClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectReportByEmployee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboByEmployeeClass.Focus();
                return;
            }
            if (cmbClass.Text == "") 
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }

            //clear gridcontrol
            gridControlFeePayments.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();


            //load culumns in gridControlFeeInfo
            gridControlFeePayments.DataSource = CreateDataFeesPaymentClass();

            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            if (comboByEmployeeClass.Text == LocRM.GetString("strAll"))
            {
                try
                {
                    filterPaymentClassAll();
                    CalculateCount();

                    comboByEmployeeDate.SelectedIndex = -1;
                    dtFrom.EditValue = DateTime.Today;
                    dtTo.EditValue = DateTime.Today;
                    comboDefinedDate.SelectedIndex = -1;
                    comboByEmployeeDefinedDate.SelectedIndex = -1;

                    cmbCycle.SelectedIndex = -1;
                    dtFromCycle.EditValue = DateTime.Today;
                    dtToCycle.EditValue = DateTime.Today;
                    cmbByEmployeeCycle.SelectedIndex = -1;
                    cmbCycleSection.SelectedIndex = -1;
                    cmbSection.SelectedIndex = -1;
                    dtFromSection.EditValue = DateTime.Today;
                    dtToSection.EditValue = DateTime.Today;
                    cmbByEmployeeSection.SelectedIndex = -1;
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
            else
            {
                try
                {
                    filterPaymentClassCashier();
                    CalculateCount();

                    comboByEmployeeDate.SelectedIndex = -1;
                    dtFrom.EditValue = DateTime.Today;
                    dtTo.EditValue = DateTime.Today;
                    comboDefinedDate.SelectedIndex = -1;
                    comboByEmployeeDefinedDate.SelectedIndex = -1;

                    cmbCycle.SelectedIndex = -1;
                    dtFromCycle.EditValue = DateTime.Today;
                    dtToCycle.EditValue = DateTime.Today;
                    cmbByEmployeeCycle.SelectedIndex = -1;
                    cmbCycleSection.SelectedIndex = -1;
                    cmbSection.SelectedIndex = -1;
                    dtFromSection.EditValue = DateTime.Today;
                    dtToSection.EditValue = DateTime.Today;
                    cmbByEmployeeSection.SelectedIndex = -1;

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
        }
        //private void getDataDatePeriod()
        //{
        //    if (comboByEmployeeDefinedDate.Text == "")
        //    {
        //        XtraMessageBox.Show(LocRM.GetString("strSelectReportByEmployee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        comboByEmployeeDefinedDate.Focus();
        //        return;
        //    }
        //    if (comboDefinedDate.Text == "")
        //    {
        //        XtraMessageBox.Show(LocRM.GetString("strSelectDatePeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        comboDefinedDate.Focus();
        //        return;
        //    }
        //    try
        //    {
        //        //clear gridcontrol
        //        gridControlFeeDueReport.DataSource = null;
        //        //load culumns in gridControlFeeInfo
        //        gridControlFeeDueReport.DataSource = CreateData();


        //        if (splashScreenManager1.IsSplashFormVisible == false)
        //        {
        //            splashScreenManager1.ShowWaitForm();
        //            splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
        //        }
        //        using (con = new SqlConnection(databaseConnectionString))
        //        {
        //            con.Open();

        //            // cmd = New SqlCommand("SELECT RTRIM(Student.AdmissionNo),RTRIM(GRNo),RTRIM(StudentName),RTRIM(SchoolName),Sum(CourseFee.Fee) from Student,SchoolInfo,CourseFee,Class where Student.SchoolID=SchoolInfo.S_ID and Class.Classname=CourseFee.Class and CourseFee.SchoolID=SchoolInfo.S_ID and Student.Session=@d1 and CourseFee.Class=@d2 and CourseFee.Month=@d3 group by Student.AdmissionNo,GRNo,StudentName,SchoolName Except SELECT RTRIM(Student.AdmissionNo),RTRIM(GRNo),RTRIM(StudentName),RTRIM(SchoolName),Sum(CourseFee.Fee) from Student,SchoolInfo,CourseFee,CourseFeePayment,Class,CourseFeePayment_Join where CourseFeePayment.CFP_ID=CourseFeePayment_Join.C_PaymentID and CourseFee.SchoolID=SchoolInfo.S_ID and Class.Classname=CourseFee.Class and Student.SchoolID=SchoolInfo.S_ID and CourseFeePayment.Student_Class=CourseFee.Class and Student.AdmissionNo=CourseFeePayment.AdmissionNo and CourseFee.Month=CourseFeePayment_Join.Month and CourseFeePayment.Session=@d1 and CourseFee.Class=@d2 and CourseFee.Month=@d3 group by Student.AdmissionNo,GRNo,StudentName,SchoolName order by 3", con);

        //            string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(Students.StudentSurname),RTRIM(Students.StudentFirstNames),Sum(distinct CourseFee.Fee) from Students,CourseFee,Classes where Students.Class= Classes.ClassName and Classes.ClassName=CourseFee.ClassName and Students.SchoolYear=@d1 and CourseFee.ClassName=@d2 and CourseFee.Month=@d3 group by Students.StudentNumber,Students.StudentSurname,Students.StudentFirstNames Except SELECT RTRIM(Students.StudentNumber),RTRIM(Students.StudentSurname),RTRIM(Students.StudentFirstNames),Sum(distinct CourseFee.Fee) from Students,CourseFee,CourseFeePayment,Classes,CourseFeePayment_Join where CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and Students.Class= Classes.ClassName and Classes.ClassName=CourseFee.ClassName and CourseFeePayment.Student_Class=CourseFee.ClassName and Students.StudentNumber=CourseFeePayment.StudentNumber and CourseFee.Month=CourseFeePayment_Join.Month and CourseFeePayment.SchoolYear=@d1 and CourseFee.ClassName=@d2 and CourseFee.Month=@d3  group by Students.StudentNumber,Students.StudentSurname,Students.StudentFirstNames order by 2";

        //            cmd = new SqlCommand(ct);
        //            cmd.Connection = con;
        //            cmd.Parameters.AddWithValue("@d1", cmbSchoolYearFull.Text.Trim());
        //            cmd.Parameters.AddWithValue("@d2", cmbClassFull.Text.Trim());
        //            cmd.Parameters.AddWithValue("@d3", cmbMonthFull.Text.Trim());

        //            rdr = cmd.ExecuteReader();

        //            decimal feeDue;
        //            while ((rdr.Read() == true))
        //            {
        //                //add new product row
        //                gridView1.AddNewRow();
        //                gridView1.UpdateCurrentRow();

        //                var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
        //                int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

        //                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
        //                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[1].ToString());
        //                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strName").ToUpper()], rdr[2].ToString());

        //                feeDue = decimal.Parse(rdr[3].ToString(), CultureInfo.CurrentCulture);
        //                //string sFeeDue = feeDue.ToString("0.00");
        //                string sFeeDue = feeDue.ToString();
        //                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()], sFeeDue);


        //                // gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()],(Convert.ToDecimal( rdr[3])));                        
        //                //gridViewFee.Columns[LocRM.GetString("strFee").ToUpper()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

        //            }
        //            con.Close();
        //        }

        //        if (splashScreenManager1.IsSplashFormVisible == true)
        //        {
        //            splashScreenManager1.CloseWaitForm();
        //        }
        //        CalculateCount();
        //        //Set School details
        //        PublicVariables.SchoolYear = cmbSchoolYearFull.Text;
        //        PublicVariables.SchoolClass = cmbClassFull.Text;
        //        PublicVariables.FeePeriod = cmbMonthFull.Text;
        //        PublicVariables.FeesDueFull = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (splashScreenManager1.IsSplashFormVisible == true)
        //        {
        //            splashScreenManager1.CloseWaitForm();
        //        }

        //        XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //GridViewpayment Period of time
        private DataTable CreateDataFeesPayment()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strClass").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotalPaid").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotalDue").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strBalance").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strPeriod").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strModeOfPayment").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strCashier").ToUpper(), typeof(string));
            return dt;
        }
        //GridViewpayment by class
        private DataTable CreateDataFeesPaymentClass()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strPaymentDate").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotalPaid").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotalDue").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strBalance").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strPeriod").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strModeOfPayment").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strCashier").ToUpper(), typeof(string));
            return dt;
        }
        //GridViewpayment when reset
        private DataTable CreateDataFeesPaymentReset()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strClass").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotalPaid").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotalDue").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strBalance").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strPeriod").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strPaymentDate").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strModeOfPayment").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strCashier").ToUpper(), typeof(string));
            return dt;
        }
        string schoolYear = "";
        //studentSearchBy, searchBy public variable
        //int searchBy = 0;
        //1: search by All Today
        //2: search by All Yesterday
        //3: search by All this week
        //4:search by All last week
        //5:search by All this month
        //6: search by employee Today
        //7: search by employee Yesterday
        //8: search by employee this week
        //9:search by employee last week
        //10:search by employee this month
        //11:search by All custom date
        //12:search by employee custom date
        //13:search by All class
        //14:search by employee class
        //15:search by All Cycle
        //16:search by employee Cycle
        //17:search by All Section
        //18:search by employee Section
        private void filterPaymentTodayAll()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber and " +
                        "CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID  and " +
                        "PaymentDate = @date1 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DateTime today = DateTime.Now.Date;
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, "PaymentDate").Value = today;
                    
                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue,feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());
                                               
                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                studentSearchBy = 1;
                //searchBy = 1;

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
        private void filterPaymentTodayCashier()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate = @d1 and Employee=@d2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DateTime today = DateTime.Now.Date;
                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, "PaymentDate").Value = today;
                    cmd.Parameters.Add("@d2", SqlDbType.NChar, 80, "Employee").Value = comboByEmployeeDefinedDate.Text.Trim();

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                searchbyCashier = comboByEmployeeDefinedDate.Text.Trim();
                studentSearchBy = 6;
                //searchBy = 6;

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
        private void filterPaymentYesterdayAll()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee) , RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate = @date1 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DateTime today = DateTime.Now.Date;
                    // DateTime yesterday = today.Subtract(TimeSpan.FromHours(24));
                    DateTime yesterday = today.AddDays(-1);
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, "PaymentDate").Value = yesterday;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                studentSearchBy = 2;
                //searchBy = 2;

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
        private void filterPaymentYesterdayCashier()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate = @d1 and Employee=@d2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DateTime today = DateTime.Now.Date;
                    // DateTime yesterday = today.Subtract(TimeSpan.FromHours(24));
                    DateTime yesterday = today.AddDays(-1);
                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, "PaymentDate").Value = yesterday;
                    cmd.Parameters.Add("@d2", SqlDbType.NChar, 80, "Employee").Value = comboByEmployeeDefinedDate.Text.Trim();

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                searchbyCashier = comboByEmployeeDefinedDate.Text.Trim();
                studentSearchBy = 7;
                //searchBy = 7;

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
        private void filterPaymentThisWeekAll()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate >= @d1 and PaymentDate <= @d2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DayOfWeek currentDay = DateTime.Now.DayOfWeek;
                    int daysTillCurrentDay = currentDay - DayOfWeek.Monday;
                    DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);
                    DateTime today = DateTime.Now.Date;
                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, "PaymentDate").Value = currentWeekStartDate;
                    cmd.Parameters.Add("@d2", SqlDbType.Date, 30, "PaymentDate").Value = today;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                studentSearchBy = 3;
                //searchBy = 3;

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
        private void filterPaymentThisWeekCashier()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate >= @d1 and PaymentDate <= @d2 and Employee=@d3 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DayOfWeek currentDay = DateTime.Now.DayOfWeek;
                    int daysTillCurrentDay = currentDay - DayOfWeek.Monday;
                    DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);
                    DateTime today = DateTime.Now.Date;

                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, "PaymentDate").Value = currentWeekStartDate;
                    cmd.Parameters.Add("@d2", SqlDbType.Date, 30, "PaymentDate").Value = today;
                    cmd.Parameters.Add("@d3", SqlDbType.NChar, 80, "Employee").Value = comboByEmployeeDefinedDate.Text.Trim();

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                searchbyCashier = comboByEmployeeDefinedDate.Text.Trim();
                studentSearchBy = 8;
                //searchBy = 8;

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
        private void filterPaymentLastWeekAll()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate >= @d1 and PaymentDate <= @d2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DayOfWeek currentDay = DateTime.Now.DayOfWeek;
                    int daysTillCurrentDay = currentDay - DayOfWeek.Monday;
                    DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);
                    DateTime lastWeek = currentWeekStartDate.AddDays(-7);
                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, "PaymentDate").Value = lastWeek;
                    cmd.Parameters.Add("@d2", SqlDbType.Date, 30, "PaymentDate").Value = currentWeekStartDate;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                studentSearchBy = 4;
                //searchBy = 4;

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
        private void filterPaymentLastWeekCashier()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID  and " +
                        "PaymentDate >= @d1 and PaymentDate < @d2 and Employee=@d3 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DayOfWeek currentDay = DateTime.Now.DayOfWeek;
                    int daysTillCurrentDay = currentDay - DayOfWeek.Monday;
                    DateTime currentWeekStartDate = DateTime.Now.AddDays(-daysTillCurrentDay);
                    DateTime lastWeek = currentWeekStartDate.AddDays(-7);

                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, "PaymentDate").Value = lastWeek;
                    cmd.Parameters.Add("@d2", SqlDbType.Date, 30, "PaymentDate").Value = currentWeekStartDate;
                    cmd.Parameters.Add("@d3", SqlDbType.NChar, 80, "Employee").Value = comboByEmployeeDefinedDate.Text.Trim();

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                searchbyCashier = comboByEmployeeDefinedDate.Text.Trim();
                studentSearchBy = 9;
                //searchBy = 9;

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
        private void filterPaymentThisMonthAll()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate >= @d1 and PaymentDate <= @d2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DateTime today = DateTime.Now;
                    DateTime currentMonth = new DateTime(today.Year, today.Month, 1);
                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, "PaymentDate").Value = currentMonth;
                    cmd.Parameters.Add("@d2", SqlDbType.Date, 30, "PaymentDate").Value = today;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                studentSearchBy = 5;
                //searchBy = 5;

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
        private void filterPaymentThisMonthCashier()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and " +
                        "PaymentDate >= @d1 and PaymentDate <= @d2 and Employee=@d3 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    DateTime today = DateTime.Now;
                    DateTime currentMonth = new DateTime(today.Year, today.Month, 1); ;

                    cmd.Parameters.Add("@d1", SqlDbType.Date, 30, "PaymentDate").Value = currentMonth;
                    cmd.Parameters.Add("@d2", SqlDbType.Date, 30, "PaymentDate").Value = today;
                    cmd.Parameters.Add("@d3", SqlDbType.NChar, 80, "Employee").Value = comboByEmployeeDefinedDate.Text.Trim(); 

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());
                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                searchbyCashier = comboByEmployeeDefinedDate.Text.Trim();
                studentSearchBy = 10;
                //searchBy = 10;

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
        private void btnGetDataDefinedDate_Click(object sender, EventArgs e)
        {
            if (comboByEmployeeDefinedDate.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectReportByEmployee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboByEmployeeDefinedDate.Focus();
                return;
            }
            if (comboDefinedDate.Text == "") 
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectDatePeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboDefinedDate.Focus();
                return;
            }

            //clear gridcontrol
            gridControlFeePayments.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();


            //load culumns in gridControlFeeInfo
            gridControlFeePayments.DataSource = CreateDataFeesPayment();

            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar
                        
            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;


            if (comboByEmployeeDefinedDate.Text == LocRM.GetString("strAll"))
            {
                try
                {
                    if (comboDefinedDate.Text.Trim() == LocRM.GetString("strToday"))
                    {
                        filterPaymentTodayAll();
                        CalculateCount();
                    }
                    else if (comboDefinedDate.Text.Trim() == LocRM.GetString("strYesterday"))
                    {
                        filterPaymentYesterdayAll();
                        CalculateCount();
                    }
                    else if (comboDefinedDate.Text.Trim() == LocRM.GetString("strThisWeek"))
                    {
                        filterPaymentThisWeekAll();
                        CalculateCount();
                    }
                    else if (comboDefinedDate.Text.Trim() == LocRM.GetString("strLastWeek"))
                    {
                        filterPaymentLastWeekAll();
                        CalculateCount();
                    }
                    else if (comboDefinedDate.Text.Trim() == LocRM.GetString("strThisMonth"))
                    {
                        filterPaymentThisMonthAll(); 
                        CalculateCount();
                    }
                    comboByEmployeeDate.SelectedIndex = -1;
                    dtFrom.EditValue = DateTime.Today;
                    dtTo.EditValue = DateTime.Today;
                    cmbCycle.SelectedIndex = -1;
                    dtFromCycle.EditValue = DateTime.Today;
                    dtToCycle.EditValue = DateTime.Today;
                    cmbByEmployeeCycle.SelectedIndex = -1;

                    cmbCycleSection.SelectedIndex = -1;
                    cmbSection.SelectedIndex = -1;
                    dtFromSection.EditValue = DateTime.Today;
                    dtToSection.EditValue = DateTime.Today;
                    cmbByEmployeeSection.SelectedIndex = -1;

                    cmbCycleClass.SelectedIndex = -1;
                    cmbSectionClass.SelectedIndex = -1;
                    cmbClass.SelectedIndex = -1;
                    dtClassFrom.EditValue = DateTime.Today;
                    dtClassTo.EditValue = DateTime.Today;
                    comboByEmployeeClass.SelectedIndex = -1;
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
            else
            {
                try
                {
                    if (comboDefinedDate.Text.Trim() == LocRM.GetString("strToday"))
                    {
                        filterPaymentTodayCashier(); 
                        CalculateCount();
                    }
                    else if (comboDefinedDate.Text.Trim() == LocRM.GetString("strYesterday"))
                    {
                        filterPaymentYesterdayCashier(); 
                        CalculateCount();
                    }
                    else if(comboDefinedDate.Text.Trim() == LocRM.GetString("strThisWeek"))
                    {
                        filterPaymentThisWeekCashier(); 
                        CalculateCount();
                    }
                    if (comboDefinedDate.Text.Trim() == LocRM.GetString("strLastWeek"))
                    {
                        filterPaymentLastWeekCashier();
                        CalculateCount();

                    }
                    if (comboDefinedDate.Text.Trim() == LocRM.GetString("strThisMonth"))
                    {
                        filterPaymentThisMonthCashier();
                        CalculateCount();
                    }
                    comboByEmployeeDate.SelectedIndex = -1;
                    dtFrom.EditValue = DateTime.Today;
                    dtTo.EditValue = DateTime.Today;
                    cmbCycle.SelectedIndex = -1;
                    dtFromCycle.EditValue = DateTime.Today;
                    dtToCycle.EditValue = DateTime.Today;
                    cmbByEmployeeCycle.SelectedIndex = -1;

                    cmbCycleSection.SelectedIndex = -1;
                    cmbSection.SelectedIndex = -1;
                    dtFromSection.EditValue = DateTime.Today;
                    dtToSection.EditValue = DateTime.Today;
                    cmbByEmployeeSection.SelectedIndex = -1;

                    cmbCycleClass.SelectedIndex = -1;
                    cmbSectionClass.SelectedIndex = -1;
                    cmbClass.SelectedIndex = -1;
                    dtClassFrom.EditValue = DateTime.Today;
                    dtClassTo.EditValue = DateTime.Today;
                    comboByEmployeeClass.SelectedIndex = -1;
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
        }
        //format cells
        List<int> list = new List<int>();
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //GridView view = sender as GridView;
            //if (e.Column.FieldName != LocRM.GetString("strTotalPaid").ToUpper()) return;
            //int dataSourceRowIndex = view.GetDataSourceRowIndex(4);
            //if (list.Contains(dataSourceRowIndex)) return;

            //e.DisplayText = e.DisplayText.ToString(new NumberFormatInfo() { NumberDecimalSeparator = "," });
        }

        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            (e.PrintingSystem as PrintingSystemBase).PageSettings.Landscape = true; //page to landscape
        }

        private void userControlFeePaymentRecord_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                gridControlFeePayments.DataSource = null;
            }
        }

        private void btnLoadReports_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            comboByEmployeeDefinedDate.Properties.AdvancedModeOptions.Label = LocRM.GetString("strByCashier")+": ";
            comboDefinedDate.Properties.AdvancedModeOptions.Label = LocRM.GetString("strPeriod") + ": "; 
            dtFrom.Properties.AdvancedModeOptions.Label = LocRM.GetString("strFrom") + ": ";

            TextInfo myTI = CultureInfo.CurrentCulture.TextInfo; 
            dtTo.Properties.AdvancedModeOptions.Label =myTI.ToTitleCase( LocRM.GetString("strTo"))+ ": ";
            comboByEmployeeDate.Properties.AdvancedModeOptions.Label = LocRM.GetString("strByCashier") + ": ";

            cmbCycleClass.Properties.AdvancedModeOptions.Label = LocRM.GetString("strCycle") + ": ";
            cmbSectionClass.Properties.AdvancedModeOptions.Label = LocRM.GetString("strSection") + ": ";
            cmbClass.Properties.AdvancedModeOptions.Label = LocRM.GetString("strClass") + ": ";
            dtClassFrom.Properties.AdvancedModeOptions.Label = LocRM.GetString("strFrom") + ": ";
            dtClassTo.Properties.AdvancedModeOptions.Label = myTI.ToTitleCase(LocRM.GetString("strTo")) + ": ";
            comboByEmployeeClass.Properties.AdvancedModeOptions.Label = LocRM.GetString("strByCashier") + ": "; 

            cmbCycle.Properties.AdvancedModeOptions.Label = LocRM.GetString("strCycle") + ": ";
            dtFromCycle.Properties.AdvancedModeOptions.Label = LocRM.GetString("strFrom") + ": ";
            dtToCycle.Properties.AdvancedModeOptions.Label = myTI.ToTitleCase(LocRM.GetString("strTo")) + ": ";
            cmbByEmployeeCycle.Properties.AdvancedModeOptions.Label = LocRM.GetString("strByCashier") + ": ";

            cmbCycleSection.Properties.AdvancedModeOptions.Label = LocRM.GetString("strCycle") + ": ";
            cmbSection.Properties.AdvancedModeOptions.Label = LocRM.GetString("strSection") + ": ";
            dtFromSection.Properties.AdvancedModeOptions.Label = LocRM.GetString("strFrom") + ": ";
            dtToSection.Properties.AdvancedModeOptions.Label = myTI.ToTitleCase(LocRM.GetString("strTo")) + ": ";
            cmbByEmployeeSection.Properties.AdvancedModeOptions.Label = LocRM.GetString("strByCashier") + ": ";

            dtFrom.EditValue = DateTime.Today;
            dtTo.EditValue = DateTime.Today;
            dtClassTo.EditValue = DateTime.Today;
            dtClassTo.EditValue = DateTime.Today;
            dtFromCycle.EditValue = DateTime.Today;
            dtToCycle.EditValue = DateTime.Today;
            dtFromSection.EditValue = DateTime.Today;
            dtToSection.EditValue = DateTime.Today;

            populateReports();
            groupDatePeriods.Enabled = true;
            groupCustomDates.Enabled = true;
            groupControlFilterBy.Enabled = true;
            fillDefinedDate();

            fillComboEmployee();            
            fillComboCycle();
        }

        private void btnGetDataCycle_Click(object sender, EventArgs e)
        {
            if (cmbCycle.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterCycle"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCycle.Focus();
                return;
            }
            if (cmbByEmployeeCycle.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectReportByEmployee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbByEmployeeCycle.Focus();
                return;
            }

            //clear gridcontrol
            gridControlFeePayments.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();


            //load culumns in gridControlFeeInfo
            gridControlFeePayments.DataSource = CreateDataFeesPayment();

            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            if (cmbByEmployeeCycle.Text == LocRM.GetString("strAll"))
            {
                try
                {
                    filterPaymentCycleAll();
                    CalculateCount();

                    comboByEmployeeDate.SelectedIndex = -1;
                    dtFrom.EditValue = DateTime.Today;
                    dtTo.EditValue = DateTime.Today;
                    comboDefinedDate.SelectedIndex = -1;
                    comboByEmployeeDefinedDate.SelectedIndex = -1;

                    cmbCycleSection.SelectedIndex = -1;
                    cmbSection.SelectedIndex = -1;
                    dtFromSection.EditValue = DateTime.Today;
                    dtToSection.EditValue = DateTime.Today;
                    cmbByEmployeeSection.SelectedIndex = -1;

                    cmbCycleClass.SelectedIndex = -1;
                    cmbSectionClass.SelectedIndex = -1;
                    cmbClass.SelectedIndex = -1;
                    dtClassFrom.EditValue = DateTime.Today;
                    dtClassTo.EditValue = DateTime.Today;
                    comboByEmployeeClass.SelectedIndex = -1;
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
            else
            {
                try
                {
                    filterPaymentCycleCashier();
                    CalculateCount();

                    comboByEmployeeDate.SelectedIndex = -1;
                    dtFrom.EditValue = DateTime.Today;
                    dtTo.EditValue = DateTime.Today;
                    comboDefinedDate.SelectedIndex = -1;
                    comboByEmployeeDefinedDate.SelectedIndex = -1;

                    cmbCycleSection.SelectedIndex = -1;
                    cmbSection.SelectedIndex = -1;
                    dtFromSection.EditValue = DateTime.Today;
                    dtToSection.EditValue = DateTime.Today;
                    cmbByEmployeeSection.SelectedIndex = -1;

                    cmbCycleClass.SelectedIndex = -1;
                    cmbSectionClass.SelectedIndex = -1;
                    cmbClass.SelectedIndex = -1;
                    dtClassFrom.EditValue = DateTime.Today;
                    dtClassTo.EditValue = DateTime.Today;
                    comboByEmployeeClass.SelectedIndex = -1;

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
        }

        private void filterPaymentCycleAll()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month), RTRIM(Cycle) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and Cycle=@d1 " +
                        "and PaymentDate >= @date1 and PaymentDate <= @date2  order by StudentSurname,Students.StudentNumber, Cycle";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, "Cycle").Value = cmbCycle.Text.Trim();
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " PaymentDate").Value = dtFromCycle.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " PaymentDate").Value = dtToCycle.DateTime;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());

                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                educationCycle = cmbCycle.Text;
                searchbyDateFrom = dtFromCycle.DateTime.ToString("dd/MM/yyyy");
                searchbyDateTo = dtToCycle.DateTime.ToString("dd/MM/yyyy");
                studentSearchBy = 15;
                

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
        private void filterPaymentCycleCashier()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month), RTRIM(Cycle) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and Cycle=@d1 and Employee=@d2 " +
                        "and PaymentDate >= @date1 and PaymentDate <= @date2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, "Cycle").Value = cmbCycle.Text;
                    cmd.Parameters.Add("@d2", SqlDbType.NChar, 80, "Employee").Value = cmbByEmployeeCycle.Text.Trim();
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " PaymentDate").Value = dtFromCycle.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " PaymentDate").Value = dtToCycle.DateTime;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());

                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                educationCycle = cmbCycle.Text;
                searchbyCashier = cmbByEmployeeCycle.Text.Trim();
                searchbyDateFrom = dtFromCycle.DateTime.ToString("dd/MM/yyyy");
                searchbyDateTo = dtToCycle.DateTime.ToString("dd/MM/yyyy");
                studentSearchBy = 16;
               

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
        private void filterPaymentSectionAll()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month), RTRIM(Section) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and Section=@d1 " +
                        "and PaymentDate >= @date1 and PaymentDate <= @date2  order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, "Section").Value = cmbSection.Text.Trim();
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " PaymentDate").Value = dtFromSection.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " PaymentDate").Value = dtToSection.DateTime;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());

                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                section = cmbSection.Text;
                searchbyDateFrom = dtFromSection.DateTime.ToString("dd/MM/yyyy");
                searchbyDateTo = dtToSection.DateTime.ToString("dd/MM/yyyy");
                studentSearchBy = 17;


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
        private void filterPaymentSectionCashier()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames),RTRIM(Class)," +
                        "RTRIM(Students.SchoolYear),TotalPaid,GrandTotal,PaymentDue ,RTRIM(ModeOfPayment),PaymentDate, " +
                        "RTRIM(Employee), RTRIM(Month), RTRIM(Section) FROM Students,CourseFeePayment, CourseFeePayment_Join where " +
                        "Students.StudentNumber=CourseFeePayment.StudentNumber " +
                        "and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and Section=@d1 and Employee=@d2 " +
                        "and PaymentDate >= @date1 and PaymentDate <= @date2 order by StudentSurname,Students.StudentNumber";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, "Section").Value = cmbSection.Text;
                    cmd.Parameters.Add("@d2", SqlDbType.NChar, 80, "Employee").Value = cmbByEmployeeSection.Text.Trim();
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " PaymentDate").Value = dtFromSection.DateTime;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " PaymentDate").Value = dtToSection.DateTime;

                    rdr = cmd.ExecuteReader();
                    decimal feePaid, feeDue, feeBalance;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle + 1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClass").ToUpper()], rdr[3].ToString());

                        schoolYear = rdr[4].ToString();
                        feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        feeDue = decimal.Parse(rdr[6].ToString(), CultureInfo.CurrentCulture);
                        feeBalance = decimal.Parse(rdr[7].ToString(), CultureInfo.CurrentCulture);
                        string sFeePaid = feePaid.ToString();
                        string sfeeDue = feeDue.ToString();
                        string sfeeBalance = feeBalance.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalPaid").ToUpper()], sFeePaid);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotalDue").ToUpper()], sfeeDue);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBalance").ToUpper()], sfeeBalance);
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[11].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strModeOfPayment").ToUpper()], rdr[8].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strCashier").ToUpper()], rdr[10].ToString());

                        gridView1.Appearance.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold                        
                    }
                    con.Close();
                }
                //Set Report details                
                SchoolYear = schoolYear;
                section = cmbSection.Text;
                searchbyCashier = cmbByEmployeeSection.Text.Trim();
                searchbyDateFrom = dtFromSection.DateTime.ToString("dd/MM/yyyy");
                searchbyDateTo = dtToSection.DateTime.ToString("dd/MM/yyyy");
                studentSearchBy = 18;

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
        private void cmbCycleSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.SelectedIndex = -1;

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (cmbCycleSection.SelectedIndex >= 3)
                {
                    cmbSection.Enabled = true;
                    AutocompletecmbSection();
                }
                else
                {
                    cmbSection.Enabled = false;
                    cmbSectionClass.Properties.Items.Clear();
                }
            }
            else
            {
                if (cmbCycleSection.SelectedIndex >= 4)
                {
                    cmbSection.Enabled = true;
                    AutocompletecmbSection();
                }
                else
                {
                    cmbSection.Enabled = false;
                    cmbSectionClass.Properties.Items.Clear();
                }
            }
        }

        private void cmbCycleClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSectionClass.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (cmbCycleClass.SelectedIndex >= 3)
                {
                    cmbClass.Properties.Items.Clear();
                    cmbSectionClass.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClass.Enabled = true;
                    AutocompleteClass();
                    cmbSectionClass.Enabled = false;
                    cmbSectionClass.Properties.Items.Clear();
                }
            }
            else
            {
                if (cmbCycleClass.SelectedIndex >= 4)
                {
                    cmbClass.Properties.Items.Clear();
                    cmbSectionClass.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClass.Enabled = true;
                    AutocompleteClass();
                    cmbSectionClass.Enabled = false;
                    cmbSectionClass.Properties.Items.Clear();
                }
            }
        }

        private void cmbSectionClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbClass.Enabled = true;
            AutocompleteClass();
        }
        //Autocomplete Class
        private void AutocompleteClass()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    if (cmbSectionClass.SelectedIndex != -1)
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1 and SectionName=@d2", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, " Cycle").Value = cmbCycleClass.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 80, " SectionName").Value = cmbSectionClass.Text.Trim();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, " Cycle").Value = cmbCycleClass.Text.Trim();
                    }
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    cmbClass.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbClass.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbClass.SelectedIndex = -1;
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
        //Autocomplete Section/class
        private void AutocompleteSection()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SectionName) FROM Sections", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbSectionClass.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSectionClass.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSectionClass.SelectedIndex = -1;
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
        //Autocomplete cmbSection
        private void AutocompletecmbSection()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SectionName) FROM Sections", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbSection.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSection.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSection.SelectedIndex = -1;
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

        private void btnGetDataSection_Click(object sender, EventArgs e)
        {
            if (cmbByEmployeeSection.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectReportByEmployee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbByEmployeeSection.Focus();
                return;
            }
            if (cmbSection.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSection"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSection.Focus();
                return;
            }

            //clear gridcontrol
            gridControlFeePayments.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();


            //load culumns in gridControlFeeInfo
            gridControlFeePayments.DataSource = CreateDataFeesPayment();

            gridView1.BestFitColumns();
            gridView1.OptionsView.ColumnAutoWidth = true; //Make widh fill the available area good with less columns, no horizontal scrollbar

            ribbonPageGroupPrint.Enabled = true;
            ribbonPageGroupExport.Enabled = true;

            if (cmbByEmployeeSection.Text == LocRM.GetString("strAll"))
            {
                try
                {
                    filterPaymentSectionAll();
                    CalculateCount();

                    comboByEmployeeDate.SelectedIndex = -1;
                    dtFrom.EditValue = DateTime.Today;
                    dtTo.EditValue = DateTime.Today;
                    comboDefinedDate.SelectedIndex = -1;
                    comboByEmployeeDefinedDate.SelectedIndex = -1;

                    cmbCycle.SelectedIndex = -1;
                    dtFromCycle.EditValue = DateTime.Today;
                    dtToCycle.EditValue = DateTime.Today;
                    cmbByEmployeeCycle.SelectedIndex = -1;

                    cmbCycleClass.SelectedIndex = -1;
                    cmbSectionClass.SelectedIndex = -1;
                    cmbClass.SelectedIndex = -1;
                    dtClassFrom.EditValue = DateTime.Today;
                    dtClassTo.EditValue = DateTime.Today;
                    comboByEmployeeClass.SelectedIndex = -1;
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
            else
            {
                try
                {
                    filterPaymentSectionCashier();
                    CalculateCount();

                    comboByEmployeeDate.SelectedIndex = -1;
                    dtFrom.EditValue = DateTime.Today;
                    dtTo.EditValue = DateTime.Today;
                    comboDefinedDate.SelectedIndex = -1;
                    comboByEmployeeDefinedDate.SelectedIndex = -1;

                    cmbCycle.SelectedIndex = -1;
                    dtFromCycle.EditValue = DateTime.Today;
                    dtToCycle.EditValue = DateTime.Today;
                    cmbByEmployeeCycle.SelectedIndex = -1;
                    cmbCycleClass.SelectedIndex = -1;
                    cmbSectionClass.SelectedIndex = -1;
                    cmbClass.SelectedIndex = -1;
                    dtClassFrom.EditValue = DateTime.Today;
                    dtClassTo.EditValue = DateTime.Today;
                    comboByEmployeeClass.SelectedIndex = -1;

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
        }
    }
}
