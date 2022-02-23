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
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using System.Diagnostics;
using static EduXpress.Functions.PublicVariables;
using System.Resources;
using DevExpress.XtraPrinting;

namespace EduXpress.Students
{
    public partial class frmListClassFeePayment : DevExpress.XtraEditors.XtraForm
    {

        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmListClassFeePayment).Assembly);
        

        public frmListClassFeePayment()
        {
            InitializeComponent();
        }        

        private void frmListClassFeePayment_Load(object sender, EventArgs e)
        {
            //Suspend layout: Basically it's if you want to adjust multiple layout-related properties - 
            //or add multiple children - but avoid the layout system repeatedly reacting to your changes. 
            //You want it to only perform the layout at the very end, when everything's "ready".
            this.SuspendLayout();
            gridControlListFeePayments.DataSource = Getdata();
            //Resume layout
            this.ResumeLayout();
            //hide course ID column
            gridView1.Columns[0].Visible = false;
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
        //Fill dataGridViewEmails
        public DataView Getdata()
        {            
            string ReceiptNoColumn = LocRM.GetString("strReceiptNo").ToUpper();
            string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
            string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
            string SectionColumn = LocRM.GetString("strSection").ToUpper();
            string ClassColumn = LocRM.GetString("strClass").ToUpper();
            string ClassFeeColumn = LocRM.GetString("strClassFee").ToUpper();
            string DiscountPercColumn = LocRM.GetString("strDiscountPerc").ToUpper();
            string DiscountColumn = LocRM.GetString("strDiscount").ToUpper();
            string PreviousDueColumn = LocRM.GetString("strPreviousDue").ToUpper();
            string FineColumn = LocRM.GetString("strFine").ToUpper();
            string TotalDueColumn = LocRM.GetString("strTotalDue").ToUpper();
            string TotalPaidColumn = LocRM.GetString("strTotalPaid").ToUpper();
            string ModeOfPaymentColumn = LocRM.GetString("strModeOfPayment").ToUpper();
            string PaymentDateColumn = LocRM.GetString("strPaymentDate").ToUpper();
            string BalanceColumn = LocRM.GetString("strBalance").ToUpper();

            dynamic SelectQry = "SELECT (CourseFeePaymentID) as [ID] , RTRIM(PaymentID) as [" + ReceiptNoColumn + "]," +
                "RTRIM(CourseFeePayment.StudentNumber) as [" + StudentNoColumn + "],RTRIM(StudentSurname) as [" + SurnameColumn + "], " +
                "RTRIM(CourseFeePayment.SchoolYear) as [" + SchoolYearColumn + "],RTRIM(Section) as [" + SectionColumn + "]," +
                "RTRIM(Student_Class) as [" + ClassColumn + "],RTRIM(TotalFee) as [" + ClassFeeColumn + "],RTRIM(DiscountPer) as [" + DiscountPercColumn + "]," +
                "RTRIM(DiscountAmt) as [" + DiscountColumn + "],RTRIM(PreviousDue) as [" + PreviousDueColumn + "],RTRIM(Fine) as [" + FineColumn + "]," +
                "RTRIM(GrandTotal) as [" + TotalDueColumn + "] ,RTRIM(TotalPaid) as [" + TotalPaidColumn + "],RTRIM(ModeOfPayment) as [" + ModeOfPaymentColumn + "]," +
                "RTRIM(PaymentDate) as [" + PaymentDateColumn + "],RTRIM(PaymentDue) as [" + BalanceColumn + "] FROM Students,CourseFeePayment " +
                "where Students.StudentNumber=CourseFeePayment.StudentNumber order by StudentSurname";
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        //Fill cmbcycle
        private void fillCycle()
        {
            comboCycle.Properties.Items.Clear();
            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                comboCycle.Properties.Items.AddRange(new object[]
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
                comboCycle.Properties.Items.AddRange(new object[]
                {
                    LocRM.GetString("strPrePrimary"),
                    LocRM.GetString("strPreparatory") ,
                    LocRM.GetString("strHighSchoolGET"),
                    LocRM.GetString("strHighSchoolFET"),
                    LocRM.GetString("strTVETCollege")
                });
            }
            
        }
        private void btnFilter_Click(object sender, EventArgs e)
        {
            groupSurname.Enabled = true;
            //groupScan.Enabled = true;
            groupSectionClass.Enabled = true;
            groupDates.Enabled = true;
            //Autocomplete
            AutocompleteSurname();
            fillCycle();
        }
        //Autocomplete Surname
        private void AutocompleteSurname()
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

                    SqlCommand cmd = new SqlCommand("SELECT StudentSurname,CourseFeePayment.StudentNumber FROM Students,CourseFeePayment where Students.StudentNumber=CourseFeePayment.StudentNumber", con);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "Students");

                    AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                    int i = 0;
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        col.Add(ds.Tables[0].Rows[i]["StudentSurname"].ToString());

                    }
                    txtSearchSurname.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtSearchSurname.MaskBox.AutoCompleteCustomSource = col;
                    txtSearchSurname.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;

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
       
        //Autocomplete Section
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
                XtraMessageBox.Show(ex.Message);
            }
        }
        //Autocomplete Class
        private void AutocompleteClass()
        {
           // con = new SqlConnection(databaseConnectionString);
           // con.Open();
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    if (cmbSection.SelectedIndex != -1)
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1 and SectionName=@d2", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCycle.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 60, " SectionName").Value = cmbSection.Text.Trim();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCycle.Text.Trim();
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
                XtraMessageBox.Show(ex.Message);
            }
        }

        private void btnSearchSurname_Click(object sender, EventArgs e)
        {
            if (txtSearchSurname.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSurname"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearchSurname.Focus();
                return;
            }
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string ReceiptNoColumn = LocRM.GetString("strReceiptNo").ToUpper();
                string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
                string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
                string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
                string SectionColumn = LocRM.GetString("strSection").ToUpper();
                string ClassColumn = LocRM.GetString("strClass").ToUpper();
                string ClassFeeColumn = LocRM.GetString("strClassFee").ToUpper();
                string DiscountPercColumn = LocRM.GetString("strDiscountPerc").ToUpper();
                string DiscountColumn = LocRM.GetString("strDiscount").ToUpper();
                string PreviousDueColumn = LocRM.GetString("strPreviousDue").ToUpper();
                string FineColumn = LocRM.GetString("strFine").ToUpper();
                string TotalDueColumn = LocRM.GetString("strTotalDue").ToUpper();
                string TotalPaidColumn = LocRM.GetString("strTotalPaid").ToUpper();
                string ModeOfPaymentColumn = LocRM.GetString("strModeOfPayment").ToUpper();
                string PaymentDateColumn = LocRM.GetString("strPaymentDate").ToUpper();
                string BalanceColumn = LocRM.GetString("strBalance").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT (CourseFeePaymentID) as [ID] , RTRIM(PaymentID) as [" + ReceiptNoColumn + "]," +
                "RTRIM(CourseFeePayment.StudentNumber) as [" + StudentNoColumn + "],RTRIM(StudentSurname) as [" + SurnameColumn + "], " +
                "RTRIM(CourseFeePayment.SchoolYear) as [" + SchoolYearColumn + "],RTRIM(Section) as [" + SectionColumn + "]," +
                "RTRIM(Student_Class) as [" + ClassColumn + "],RTRIM(TotalFee) as [" + ClassFeeColumn + "],RTRIM(DiscountPer) as [" + DiscountPercColumn + "]," +
                "RTRIM(DiscountAmt) as [" + DiscountColumn + "],RTRIM(PreviousDue) as [" + PreviousDueColumn + "],RTRIM(Fine) as [" + FineColumn + "]," +
                "RTRIM(GrandTotal) as [" + TotalDueColumn + "] ,RTRIM(TotalPaid) as [" + TotalPaidColumn + "],RTRIM(ModeOfPayment) as [" + ModeOfPaymentColumn + "]," +
                "RTRIM(PaymentDate) as [" + PaymentDateColumn + "],RTRIM(PaymentDue) as [" + BalanceColumn + "] FROM Students,CourseFeePayment " +
                    "where Students.StudentNumber=CourseFeePayment.StudentNumber and StudentSurname=@d1 ", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 30, "StudentSurname").Value = txtSearchSurname.Text;

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "Students");
                    gridControlListFeePayments.DataSource = myDataSet.Tables["Students"].DefaultView;

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

        private void btnResetList_Click(object sender, EventArgs e)
        {
            gridControlListFeePayments.DataSource = Getdata();
            clearControls();
        }
        private void clearControls()
        {
            txtSearchSurname.Text = "";
            txtReceiptBarcode.Text = "";
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            dtSearchbyDateFrom.EditValue = DateTime.Now;
            dtSearchbyDateTo.EditValue = DateTime.Now;
            txtStudentNumber.Text = "";
            txtSection.Text = "";
            txtClass.Text = "";
            txtCycle.Text = "";
            txtStudentSurname.Text = "";
            txtStudentNames.Text = "";
            pictureStudent.EditValue = null;
            barcodeText = "";
            barcode();

        }

        private void btnSearchSectionClass_Click(object sender, EventArgs e)
        {
          //  if (cmbSection.Text == "")
          //  {
          //      XtraMessageBox.Show("Please select the section", LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
          ////      cmbSection.Focus();
            //    return;
           // }
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string ReceiptNoColumn = LocRM.GetString("strReceiptNo").ToUpper();
                string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
                string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
                string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
                string SectionColumn = LocRM.GetString("strSection").ToUpper();
                string ClassColumn = LocRM.GetString("strClass").ToUpper();
                string ClassFeeColumn = LocRM.GetString("strClassFee").ToUpper();
                string DiscountPercColumn = LocRM.GetString("strDiscountPerc").ToUpper();
                string DiscountColumn = LocRM.GetString("strDiscount").ToUpper();
                string PreviousDueColumn = LocRM.GetString("strPreviousDue").ToUpper();
                string FineColumn = LocRM.GetString("strFine").ToUpper();
                string TotalDueColumn = LocRM.GetString("strTotalDue").ToUpper();
                string TotalPaidColumn = LocRM.GetString("strTotalPaid").ToUpper();
                string ModeOfPaymentColumn = LocRM.GetString("strModeOfPayment").ToUpper();
                string PaymentDateColumn = LocRM.GetString("strPaymentDate").ToUpper();
                string BalanceColumn = LocRM.GetString("strBalance").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT (CourseFeePaymentID) as [ID] , RTRIM(PaymentID) as [" + ReceiptNoColumn + "]," +
                "RTRIM(CourseFeePayment.StudentNumber) as [" + StudentNoColumn + "],RTRIM(StudentSurname) as [" + SurnameColumn + "], " +
                "RTRIM(CourseFeePayment.SchoolYear) as [" + SchoolYearColumn + "],RTRIM(Section) as [" + SectionColumn + "]," +
                "RTRIM(Student_Class) as [" + ClassColumn + "],RTRIM(TotalFee) as [" + ClassFeeColumn + "],RTRIM(DiscountPer) as [" + DiscountPercColumn + "]," +
                "RTRIM(DiscountAmt) as [" + DiscountColumn + "],RTRIM(PreviousDue) as [" + PreviousDueColumn + "],RTRIM(Fine) as [" + FineColumn + "]," +
                "RTRIM(GrandTotal) as [" + TotalDueColumn + "] ,RTRIM(TotalPaid) as [" + TotalPaidColumn + "],RTRIM(ModeOfPayment) as [" + ModeOfPaymentColumn + "]," +
                "RTRIM(PaymentDate) as [" + PaymentDateColumn + "],RTRIM(PaymentDue) as [" + BalanceColumn + "] FROM Students,CourseFeePayment " +
                    "where Students.StudentNumber=CourseFeePayment.StudentNumber and Section=@d1 and Class=@d2 order by Class ", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Section").Value = cmbSection.Text;
                    cmd.Parameters.Add("@d2", SqlDbType.NChar, 30, " Class").Value = cmbClass.Text;

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "Students");
                    gridControlListFeePayments.DataSource = myDataSet.Tables["Students"].DefaultView;

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

        private void btnSearchPaymentDate_Click(object sender, EventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string ReceiptNoColumn = LocRM.GetString("strReceiptNo").ToUpper();
                string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
                string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
                string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
                string SectionColumn = LocRM.GetString("strSection").ToUpper();
                string ClassColumn = LocRM.GetString("strClass").ToUpper();
                string ClassFeeColumn = LocRM.GetString("strClassFee").ToUpper();
                string DiscountPercColumn = LocRM.GetString("strDiscountPerc").ToUpper();
                string DiscountColumn = LocRM.GetString("strDiscount").ToUpper();
                string PreviousDueColumn = LocRM.GetString("strPreviousDue").ToUpper();
                string FineColumn = LocRM.GetString("strFine").ToUpper();
                string TotalDueColumn = LocRM.GetString("strTotalDue").ToUpper();
                string TotalPaidColumn = LocRM.GetString("strTotalPaid").ToUpper();
                string ModeOfPaymentColumn = LocRM.GetString("strModeOfPayment").ToUpper();
                string PaymentDateColumn = LocRM.GetString("strPaymentDate").ToUpper();
                string BalanceColumn = LocRM.GetString("strBalance").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT (CourseFeePaymentID) as [ID] , RTRIM(PaymentID) as [" + ReceiptNoColumn + "]," +
                "RTRIM(CourseFeePayment.StudentNumber) as [" + StudentNoColumn + "],RTRIM(StudentSurname) as [" + SurnameColumn + "], " +
                "RTRIM(CourseFeePayment.SchoolYear) as [" + SchoolYearColumn + "],RTRIM(Section) as [" + SectionColumn + "]," +
                "RTRIM(Student_Class) as [" + ClassColumn + "],RTRIM(TotalFee) as [" + ClassFeeColumn + "],RTRIM(DiscountPer) as [" + DiscountPercColumn + "]," +
                "RTRIM(DiscountAmt) as [" + DiscountColumn + "],RTRIM(PreviousDue) as [" + PreviousDueColumn + "],RTRIM(Fine) as [" + FineColumn + "]," +
                "RTRIM(GrandTotal) as [" + TotalDueColumn + "] ,RTRIM(TotalPaid) as [" + TotalPaidColumn + "],RTRIM(ModeOfPayment) as [" + ModeOfPaymentColumn + "]," +
                "RTRIM(PaymentDate) as [" + PaymentDateColumn + "],RTRIM(PaymentDue) as [" + BalanceColumn + "] FROM Students,CourseFeePayment " +
                    "where Students.StudentNumber=CourseFeePayment.StudentNumber and PaymentDate between @date1 and @date2 order " +
                    "by PaymentDate ", con);
                    cmd.Parameters.Add("@date1", SqlDbType.Date, 30, " PaymentDate").Value = dtSearchbyDateFrom.EditValue;
                    cmd.Parameters.Add("@date2", SqlDbType.Date, 30, " PaymentDate").Value = dtSearchbyDateTo.EditValue;

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "Students");
                    gridControlListFeePayments.DataSource = myDataSet.Tables["Students"].DefaultView;

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

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            ShowGridPreview(gridControlListFeePayments);
        }
        //Print Preview datagridview
        private void ShowGridPreview(GridControl grid)
        {
            // Check whether the GridControl can be previewed.
            if (!grid.IsPrintingAvailable)
            {
                MessageBox.Show(LocRM.GetString("strPrintErrorLibraryNotFound"), LocRM.GetString("strError"));
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            // PrintGrid(gridControlListFeePayments);
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                }
                PrintingSystem printingSystem = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink();
                 link.Component = gridControlListFeePayments;
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

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                gridView1.OptionsView.ColumnAutoWidth = false;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".xlsx";
                    //Export to excel
                    gridControlListFeePayments.ExportToXlsx(fileName);
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strClassFeePayment"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                // gridView1.OptionsView.ColumnAutoWidth = false;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".pdf";
                    //Export to pdf
                    gridControlListFeePayments.ExportToPdf(fileName);
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strClassFeePayment"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void btnExportWord_Click(object sender, EventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                // gridView1.OptionsView.ColumnAutoWidth = false;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".docx";
                    //Export to docx
                    gridControlListFeePayments.ExportToDocx(fileName);
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strClassFeePayment"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                XtraMessageBox.Show(ex.ToString());
            }
        }
        string barcodeText;
        private void gridControlListFeePayments_MouseClick(object sender, MouseEventArgs e)
        {
            barcodeText = "";
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (gridView1.DataRowCount > 0)
                {
                    txtStudentNumber.Text = txtStudentNumber.Text.TrimEnd();
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();
                        
                        cmd.CommandText = "SELECT * from Students,CourseFeePayment where Students.StudentNumber=CourseFeePayment.StudentNumber and PaymentID = '" + gridView1.GetFocusedRowCellValue(LocRM.GetString("strReceiptNo").ToUpper()).ToString() + "'";
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            txtCFPId.Text = (rdr.GetValue(44).ToString());
                            barcodeText = (rdr.GetString(45).Trim());
                            txtStudentNumber.Text = (rdr.GetString(1).Trim());
                            txtCycle.Text = (rdr.GetString(38).Trim());
                            txtSection.Text = (rdr.GetString(2).Trim());
                            txtClass.Text = (rdr.GetString(4).Trim());
                            txtStudentSurname.Text = (rdr.GetString(5).Trim());
                            txtStudentNames.Text = (rdr.GetString(6).Trim());
                            txtTotalPaid.Text = (rdr.GetDecimal(54).ToString());
                            datePaymentDate.EditValue = (rdr.GetValue(56));
                            if (!Convert.IsDBNull(rdr["StudentPicture"]))
                            {
                                byte[] x = (byte[])rdr["StudentPicture"];
                                MemoryStream ms = new MemoryStream(x);
                                pictureStudent.Image = Image.FromStream(ms);
                            }
                            else
                            {
                                pictureStudent.EditValue = null;
                            }
                            barcode();
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
        private void barcode()
        {
            barCodeReceipt.Visible = true;
            barCodeReceipt.Text = barcodeText;
            barCodeReceipt.BackColor = Color.White;
            barCodeReceipt.ForeColor = Color.Black;
          
        }

        private void gridControlListFeePayments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    receiptNumber = barcodeText;
                    payemntID = txtCFPId.Text.Trim();
                    Close();
                    Owner.Show();  //Show the previous form                    
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (txtReceiptBarcode.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterReceiptBarcode") , LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtReceiptBarcode.Focus();
                return;
            }
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string ReceiptNoColumn = LocRM.GetString("strReceiptNo").ToUpper();
                string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
                string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
                string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
                string SectionColumn = LocRM.GetString("strSection").ToUpper();
                string ClassColumn = LocRM.GetString("strClass").ToUpper();
                string ClassFeeColumn = LocRM.GetString("strClassFee").ToUpper();
                string DiscountPercColumn = LocRM.GetString("strDiscountPerc").ToUpper();
                string DiscountColumn = LocRM.GetString("strDiscount").ToUpper();
                string PreviousDueColumn = LocRM.GetString("strPreviousDue").ToUpper();
                string FineColumn = LocRM.GetString("strFine").ToUpper();
                string TotalDueColumn = LocRM.GetString("strTotalDue").ToUpper();
                string TotalPaidColumn = LocRM.GetString("strTotalPaid").ToUpper();
                string ModeOfPaymentColumn = LocRM.GetString("strModeOfPayment").ToUpper();
                string PaymentDateColumn = LocRM.GetString("strPaymentDate").ToUpper();
                string BalanceColumn = LocRM.GetString("strBalance").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT (CourseFeePaymentID) as [ID] , RTRIM(PaymentID) as [" + ReceiptNoColumn + "]," +
                "RTRIM(CourseFeePayment.StudentNumber) as [" + StudentNoColumn + "],RTRIM(StudentSurname) as [" + SurnameColumn + "], " +
                "RTRIM(CourseFeePayment.SchoolYear) as [" + SchoolYearColumn + "],RTRIM(Section) as [" + SectionColumn + "]," +
                "RTRIM(Student_Class) as [" + ClassColumn + "],RTRIM(TotalFee) as [" + ClassFeeColumn + "],RTRIM(DiscountPer) as [" + DiscountPercColumn + "]," +
                "RTRIM(DiscountAmt) as [" + DiscountColumn + "],RTRIM(PreviousDue) as [" + PreviousDueColumn + "],RTRIM(Fine) as [" + FineColumn + "]," +
                "RTRIM(GrandTotal) as [" + TotalDueColumn + "] ,RTRIM(TotalPaid) as [" + TotalPaidColumn + "],RTRIM(ModeOfPayment) as [" + ModeOfPaymentColumn + "]," +
                "RTRIM(PaymentDate) as [" + PaymentDateColumn + "],RTRIM(PaymentDue) as [" + BalanceColumn + "] FROM Students,CourseFeePayment " +
                    "where Students.StudentNumber=CourseFeePayment.StudentNumber and PaymentID=@d1 ", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 30, "PaymentID").Value = txtReceiptBarcode.Text.ToUpper().Trim();

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "Students");
                    gridControlListFeePayments.DataSource = myDataSet.Tables["Students"].DefaultView;

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

        private void comboCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (comboCycle.SelectedIndex >= 3)
                {
                    cmbSection.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClass.Enabled = true;
                    AutocompleteClass();
                    cmbSection.Enabled = false;
                    cmbSection.Properties.Items.Clear();
                }
            }
            else
            {
                if (comboCycle.SelectedIndex >= 4)
                {
                    cmbSection.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClass.Enabled = true;
                    AutocompleteClass();
                    cmbSection.Enabled = false;
                    cmbSection.Properties.Items.Clear();
                }

            }
            
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbClass.Enabled = true;
            AutocompleteClass();
        }

        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            (e.PrintingSystem as PrintingSystemBase).PageSettings.Landscape = true;
        }
    }
}