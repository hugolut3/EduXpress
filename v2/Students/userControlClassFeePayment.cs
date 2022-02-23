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
using System.Threading;
using System.Runtime.InteropServices;
using System.Globalization;
using System.IO;
using CM.Sms;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.Caching;
using static EduXpress.Functions.PublicVariables;
using static EduXpress.Functions.PublicFunctions;
using System.Drawing.Printing;
using System.Resources;
using DevExpress.XtraBars.Ribbon;


namespace EduXpress.Students
{
    public partial class userControlClassFeePayment : DevExpress.XtraEditors.XtraUserControl, Functions.IMergeRibbons
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
       // CultureInfo cultureToUse = CultureInfo.InvariantCulture;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlClassFeePayment).Assembly);

        //create global methods of ribons and status bar to merge when in main.
        public RibbonControl MainRibbon { get; set; }
        public RibbonStatusBar MainStatusBar { get; set; }
        public userControlClassFeePayment()
        {
         //   Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            InitializeComponent();
          
            //load culumns in gridControlFeeInfo
     //       gridControlFeeInfo.DataSource = CreateData();
 //           gridViewFee.OptionsView.ColumnAutoWidth = false;
            gridViewFee.BestFitColumns();
            //  gridViewFee.Columns["MONTH"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //  gridViewFee.Columns["FEE NAME"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
     //       gridViewFee.Columns[LocRM.GetString("strFee").ToUpper()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        }
        //Merge ribbon and statusBar
        public void MergeRibbon()
        {
            if (MainRibbon != null)
            {
                MainRibbon.MergeRibbon(this.ribbonControl1);
            }
        }
        public void MergeStatusBar()
        {
            if (MainStatusBar != null)
            {
                MainStatusBar.MergeStatusBar(this.ribbonStatusBar1);
            }
        }

        private void userControlClassFeePayment_Load(object sender, EventArgs e)
        {
            //  display Exchange Rates Button only in DRC
            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                btnExchangeRates.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                btnExchangeRates.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            AutocompleteSurname();
            AutocompleteFullName();
            txtStudentNumber.Focus();

            //Suspend layout: Basically it's if you want to adjust multiple layout-related properties - 
            //or add multiple children - but avoid the layout system repeatedly reacting to your changes. 
            //You want it to only perform the layout at the very end, when everything's "ready".
            this.SuspendLayout();
            gridControlListStudents.DataSource = Getdata();
            //Resume layout
            this.ResumeLayout();
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
            string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
            string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            string NameColumn = LocRM.GetString("strName").ToUpper();
            string SectionColumn = LocRM.GetString("strSection").ToUpper();
            string ClassColumn = LocRM.GetString("strClass").ToUpper();
            string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();
            string EmailColumn = LocRM.GetString("strEmail").ToUpper();
            string PhoneNumberColumn = LocRM.GetString("strPhoneNumber").ToUpper();
            string FatherSurnameColumn = LocRM.GetString("strFatherSurname").ToUpper();
            string FatherNameColumn = LocRM.GetString("strFatherName").ToUpper();
            string FatherContactNoColumn = LocRM.GetString("strFatherContactNo").ToUpper();
            string FatherEmailColumn = LocRM.GetString("strFatherEmail").ToUpper();
            string NotificationNoColumn = LocRM.GetString("strNotificationNo").ToUpper();
            string NotificationEmailColumn = LocRM.GetString("strNotificationEmail").ToUpper();
            string MotherSurnameColumn = LocRM.GetString("strMotherSurname").ToUpper();
            string MotherNamesColumn = LocRM.GetString("strMotherNames").ToUpper();
            string MotherContactNoColumn = LocRM.GetString("strMotherContactNo").ToUpper();
            string MotherEmailColumn = LocRM.GetString("strMotherEmail").ToUpper();
            
            dynamic SelectQry = "SELECT  RTRIM(StudentNumber) as [" + StudentNoColumn + "],RTRIM(StudentSurname) as [" + SurnameColumn + "]," +
                "RTRIM(StudentFirstNames) as [" + NameColumn + "],RTRIM(Section) as [" + SectionColumn + "]," +
                "RTRIM(Class) as [" + ClassColumn + "],RTRIM(SchoolYear) as [" + SchoolYearColumn + "]," +
                "RTRIM(StudentEmail) as [" + EmailColumn + "],RTRIM(StudentPhoneNumber) as [" + PhoneNumberColumn + "]," +
                "RTRIM(FatherSurname) as [" + FatherSurnameColumn + "],RTRIM(FatherNames) as [" + FatherNameColumn + "] ," +
                "RTRIM(FatherContactNo) as [" + FatherContactNoColumn + "]," +
                "RTRIM(FatherEmail) as [" + FatherEmailColumn + "], RTRIM(MotherSurname) as [" + MotherSurnameColumn + "]," +
                "RTRIM(MotherNames) as [" + MotherNamesColumn + "],RTRIM(MotherContactNo) as [" + MotherContactNoColumn + "]," +
                "RTRIM(MotherEmail) as [" + MotherEmailColumn + "], RTRIM(NotificationNo) as [" + NotificationNoColumn + "]," +
                "RTRIM(NotificationEmail) as [" + NotificationEmailColumn + "] FROM Students order by StudentNumber";
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
        //Autocomplete Surname
        private void AutocompleteSurname()
        {
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT StudentSurname FROM Students", con);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "Students");

                    AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                    int i = 0;
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        col.Add(ds.Tables[0].Rows[i]["StudentSurname"].ToString());

                    }
                    txtStudentSurname.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtStudentSurname.MaskBox.AutoCompleteCustomSource = col;
                    txtStudentSurname.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;

                    con.Close();
                }
                    

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Autocomplete Full Names
        private void AutocompleteFullName()
        {
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    SqlCommand cmd = new SqlCommand("SELECT StudentFirstNames FROM Students", con);
                    DataSet ds = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "Students");

                    AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                    int i = 0;
                    for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        col.Add(ds.Tables[0].Rows[i]["StudentFirstNames"].ToString());

                    }
                    txtStudentFirstNames.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    txtStudentFirstNames.MaskBox.AutoCompleteCustomSource = col;
                    txtStudentFirstNames.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;

                    con.Close();
                }
                    

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool txtStudentNumberChanged = false;
        
        private void txtStudentNumber_TextChanged(object sender, EventArgs e)
        {
           // if (gridControlStudentSelected == false)
           // {
                
                
                textBox1.Text = txtStudentNumber.Text;
                if (txtStudentNumberChanged == false)
                {

                //Ignore if reading data from database
                if (getData == false)
                {
                    if (searchedstudents == false)
                    {
                        txtStudentSurname.Text = "";
                        txtStudentFirstNames.Text = "";
                    }
                    searchedstudents = false;
                }
                    //reset contols
                    ResetFeeInfo();
                    lvMonth.Items.Clear();
                    enableGroup();
                    btnNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnSavePrint.Enabled = true;

                // txtStudentNumberChanged = true;
            }
                
           // }
          // gridControlStudentSelected = false;
        }

        bool searchedstudents = false;
        private void searchStudent()
        {
            
            try
            {

                if (txtStudentNumber.Text != "")
                {
                    txtStudentNumber.Text = txtStudentNumber.Text.TrimEnd();

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "SELECT * FROM Students WHERE StudentNumber = '" + txtStudentNumber.Text + "'";
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            searchedstudents = true;

                            txtStudentID.Text = (rdr.GetValue(0).ToString());
                            txtStudentSurname.Text = (rdr.GetString(5).ToString());
                            txtStudentFirstNames.Text = (rdr.GetString(6).ToString());
                            txtSection.Text = (rdr.GetString(2).ToString());
                            txtClass.Text = (rdr.GetString(4).ToString());
                            txtNotificationEmail.Text = (rdr.GetString(27).ToString());
                            txtNotificationNo.Text = (rdr.GetString(26).ToString());
                            txtAcademicYear.Text = (rdr.GetString(37).ToString());

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
                            fillMonths();
                            btnSave.Enabled = true;
                            btnSavePrint.Enabled = true;
                            btnPrint.Enabled = false;
                        }
                        else
                        {
                            XtraMessageBox.Show(LocRM.GetString("strNoStudent"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            btnNew.Enabled = true;
                            btnSave.Enabled = false;
                            btnSavePrint.Enabled = false;
                            btnPrint.Enabled = false;
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
                ///search with surname
                else
                {
                    if (txtStudentSurname.Text != "")
                    {
                        txtStudentSurname.Text = txtStudentSurname.Text.TrimEnd();
                        using (con = new SqlConnection(databaseConnectionString))
                        {
                            con.Open();
                            cmd = con.CreateCommand();

                            cmd.CommandText = "SELECT * FROM Students WHERE StudentSurname = '" + txtStudentSurname.Text + "'";
                            rdr = cmd.ExecuteReader();

                            if (rdr.Read())
                            {
                                searchedstudents = true;

                                txtStudentID.Text = (rdr.GetValue(0).ToString());
                                txtStudentNumber.Text = (rdr.GetString(1).ToString());
                                txtStudentFirstNames.Text = (rdr.GetString(6).ToString());
                                txtSection.Text = (rdr.GetString(2).ToString());
                                txtClass.Text = (rdr.GetString(4).ToString());
                                txtNotificationEmail.Text = (rdr.GetString(27).ToString());
                                txtNotificationNo.Text = (rdr.GetString(26).ToString());
                                txtAcademicYear.Text = (rdr.GetString(37).ToString());

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
                                fillMonths();
                                btnSave.Enabled = true;
                                btnSavePrint.Enabled = true;
                                btnPrint.Enabled = false;
                            }
                            else
                            {
                                XtraMessageBox.Show(LocRM.GetString("strNoStudent"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                btnNew.Enabled = true;
                                btnSave.Enabled = false;
                                btnSavePrint.Enabled = false;
                                btnPrint.Enabled = false;
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
                }
                  
                ResetFeeInfo();
                lvMonth.Enabled = true;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchStudent();
        }
        public void fillMonths()
        {
            try
            {
                var _with1 = lvMonth;
                _with1.Clear();
                _with1.Columns.Add( LocRM.GetString("strPeriod"), 239, HorizontalAlignment.Left);// size: 340, 212

               // lvMonth.Sorting = System.Windows.Forms.SortOrder.Ascending;
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    //  cmd = new SqlCommand("SELECT distinct RTRIM(Month) FROM CourseFee where  ClassName=@d1 ", con);
                    cmd = new SqlCommand("SELECT distinct RTRIM(Month),MonthNo FROM CourseFee where  ClassName=@d1 order by MonthNo asc", con);                    
                    cmd.Parameters.AddWithValue("@d1", txtClass.Text);

                    rdr = cmd.ExecuteReader();
                    lvMonth.Items.Clear();
                    while (rdr.Read())
                    {
                        var item = new ListViewItem();
                        item.Text = rdr[0].ToString().Trim();
                        lvMonth.Items.Add(item);
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
                MessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void userControlClassFeePayment_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                Reset();                
            }
        }
        private void Clear()
        {
            txtStudentNumber.Text = "";
            txtStudentNumber.Focus();
            txtSection.Text = "";
            txtClass.Text = "";
            txtStudentSurname.Text = "";
            txtStudentFirstNames.Text = "";
            txtNotificationEmail.Text = "";
            txtNotificationNo.Text = "";
            txtAcademicYear.Text = "";
            //dispose of images
            if (pictureStudent != null && pictureStudent.Image != null)
            {
                pictureStudent.Image.Dispose();
            }
            pictureStudent.EditValue = null;
            ResetFeeInfo();
            lvMonth.Items.Clear();
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnSavePrint.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void Reset()
        {
            txtCFPId.Text = "";
            txtStudentID.Text = "";
            txtFeePaymentID.Text = "";

            txtStudentNumber.Text = "";
            txtSection.Text = "";
            txtClass.Text = "";
            txtStudentSurname.Text = "";
            txtStudentFirstNames.Text = "";
            txtNotificationEmail.Text = "";
            txtNotificationNo.Text = "";
            txtAcademicYear.Text = "";
            pictureStudent.EditValue = null;
            gridControlListStudents.DataSource = Getdata();
            ResetFeeInfo();
            lvMonth.Items.Clear();
            lvMonth.Enabled = true;
            datePaymentDate.Enabled = true;
            txtTotalPaid.Enabled = true;
            txtFine.Enabled = true;
            disableGroup();

            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnSavePrint.Enabled = false;
            btnPrint.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            txtStudentNumberChanged = false;
        }

        private void ResetFeeInfo()
        {
            previousDue = 0; 
            txtDiscount.Text = "0";
            txtDiscountPercentage.Text = "0";
            txtPreviousDue.Text = "0";
            txtFine.Text = "0";
            txtGrandTotal.Text = "0";
            txtTotalPaid.Text = "0";
            txtBalance.Text = "0";
            txtPreviousPayment.Text = "0";
            datePaymentDate.DateTime = DateTime.Today;
            comboPaymentMode.SelectedIndex = -1;

            txtClassFee.Text = "0"; 
            gridControlFeeInfo.DataSource = null;
        }

        private void gridControlListStudents_MouseClick(object sender, MouseEventArgs e)
        {
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

                        cmd.CommandText = "SELECT * FROM Students WHERE StudentNumber = '" + gridView1.GetFocusedRowCellValue(LocRM.GetString("strStudentNo").ToUpper()).ToString() + "'";
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            searchedstudents = true;

                            txtStudentID.Text = (rdr.GetValue(0).ToString());
                            txtStudentNumber.Text = (rdr.GetString(1).ToString());
                            txtStudentSurname.Text = (rdr.GetString(5).ToString());
                            txtStudentFirstNames.Text = (rdr.GetString(6).ToString());
                            txtSection.Text = (rdr.GetString(2).ToString());
                            txtClass.Text = (rdr.GetString(4).ToString());
                            txtNotificationEmail.Text = (rdr.GetString(27).ToString());
                            txtNotificationNo.Text = (rdr.GetString(26).ToString());
                            txtAcademicYear.Text = (rdr.GetString(37).ToString());

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
                            fillMonths();
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

        //gridControlFeeInfo columns
        private DataTable CreateData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strPeriod").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strFeeName").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strFee").ToUpper(), typeof(decimal));
            return dt;
        }
        double previousDue, previousPayment, previousPaid;
       // private ListViewItem checkingItem;
        private void lvMonth_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            
            //only select one checkbox at a time
            foreach (ListViewItem lstItem in lvMonth.Items)
            {
                if (lstItem.Text != e.Item.Text)
                {
                    lstItem.Checked = false;
                    ResetFeeInfo();
                }
             }
            //only select one checkbox at a time

            try
            {
                previousDue = 0;
                previousPayment = 0;
                previousPaid = 0;
                if (lvMonth.CheckedItems.Count > 0)
                {
                    string Condition = "";

                    for (int i = 0; i <= lvMonth.CheckedItems.Count - 1; i++)
                    {                      
                        Condition += string.Format("'{0}',", lvMonth.CheckedItems[i].Text);
                    }
                    Condition = Condition.Substring(0, Condition.Length - 1);
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        //save datagridview in public variables to be used in print report
                        paymentMonths = Condition;
                        paymentClassName = txtClass.Text.Trim();
                        string sql = "Select RTRIM(Month),RTRIM(FeeName),Fee from CourseFee where  Month in (" + Condition + ")  and ClassName=@d1 order by Month,FeeName";
                        cmd = new SqlCommand(sql, con);

                        cmd.Parameters.AddWithValue("@d1", txtClass.Text.Trim());
                        rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                        //clear gridcontrol
                        gridControlFeeInfo.DataSource = null;
                        //load culumns in gridControlFeeInfo
                        gridControlFeeInfo.DataSource = CreateData();

                        while ((rdr.Read() == true))
                        {
                            //add new product row
                            gridViewFee.AddNewRow();
                            gridViewFee.UpdateCurrentRow();

                            var row = gridViewFee.GetRow(gridViewFee.GetVisibleRowHandle(gridViewFee.RowCount - 1));
                            int rowHandle = gridViewFee.GetVisibleRowHandle(gridViewFee.RowCount - 1);

                            gridViewFee.SetRowCellValue(rowHandle, gridViewFee.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[0].ToString());
                            gridViewFee.SetRowCellValue(rowHandle, gridViewFee.Columns[LocRM.GetString("strFeeName").ToUpper()], rdr[1].ToString());
                            gridViewFee.SetRowCellValue(rowHandle, gridViewFee.Columns[LocRM.GetString("strFee").ToUpper()], rdr[2].ToString());

                        }
                        con.Close();
                    }
                        

                    double sumFee = 0;
                    for (int i = 0; i < gridViewFee.DataRowCount; ++i)
                    {
                        DataRow row = gridViewFee.GetDataRow(i);
                        sumFee += Convert.ToDouble(row[LocRM.GetString("strFee").ToUpper()].ToString());
                    }
                    
                    // sumFee = Math.Round(sumFee, 2);
                    //txtClassFee.Text = sumFee.ToString("#,##0.00", cultureToUse);
                     txtClassFee.Text = sumFee.ToString(CultureInfo.CurrentCulture);

                    //Select total previous due
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();
                        //Select total previous due for all the months for this student
                         cmd.CommandText = "SELECT Sum(PaymentDue-PreviousDue) from  CourseFeePayment where StudentNumber=@d1  group by StudentNumber";                       
                        //Select total previous due for selected month only for this student
                       // cmd.CommandText = "SELECT Sum(PaymentDue-PreviousDue) from  CourseFeePayment,CourseFeePayment_Join where CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and StudentNumber=@d1 and Month = (" + Condition + ") group by StudentNumber";
                        cmd.Parameters.AddWithValue("@d1", txtStudentNumber.Text);
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            //Previous payment
                            // double previouspayment= rdr.GetDouble(0);
                            //  if ()
                            //  previousDue = Convert.ToDouble( rdr.GetValue(0), cultureToUse);
                            previousDue = Convert.ToDouble(rdr.GetValue(0), CultureInfo.CurrentCulture);
                            txtPreviousDue.Text = previousDue.ToString();
                        }
                        else
                        {
                            previousDue = 0;
                            txtPreviousDue.Text = "0";
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


                    //Select total paid for this month
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();

                        // cmd.CommandText = "SELECT Sum(TotalPaid), Month from  CourseFeePayment,CourseFeePayment_Join where StudentNumber=@d1 and Month in (" + Condition + ")  group by Month";
                        //cmd.CommandText = "SELECT Sum(TotalPaid), Month from  CourseFeePayment JOIN CourseFeePayment_Join ON CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and Month in (" + Condition + ") and StudentNumber=@d1  group by Month";
                       // cmd.CommandText = "SELECT Sum(TotalPaid), Month from  CourseFeePayment JOIN CourseFeePayment_Join ON CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID WHERE Month in (" + Condition + ") and StudentNumber=@d1  group by Month";
                        cmd.CommandText = "SELECT DISTINCT(TotalPaid), CourseFeePaymentID from  CourseFeePayment,CourseFeePayment_Join where CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and StudentNumber=@d1  and Month = (" + Condition + ")";
                        cmd.Parameters.AddWithValue("@d1", txtStudentNumber.Text);
                        // cmd.Parameters.AddWithValue("@d2", txtStudentNumber.Text);
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            previousPaid = previousPaid + Convert.ToDouble(rdr[0], CultureInfo.CurrentCulture);
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
                    //Total previous paid for this month only

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();
                        // cmd.CommandText = "SELECT TotalPaid, Month from CourseFeePayment,CourseFeePayment_Join where CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and Month in (" + Condition + ") and StudentNumber=@d1";
                       cmd.CommandText = "SELECT DISTINCT(TotalPaid) from CourseFeePayment JOIN CourseFeePayment_Join ON CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and Month in (" + Condition + ") and StudentNumber=@d1";
                      //  cmd.CommandText = "SELECT TotalPaid from CourseFeePayment JOIN CourseFeePayment_Join ON CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and Month in (" + Condition + ") and StudentNumber=@d1";
                        cmd.Parameters.AddWithValue("@d1", txtStudentNumber.Text);
                        rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            // previousPayment = previousPayment + Convert.ToDouble( rdr[0], cultureToUse);
                            previousPayment = previousPayment + Convert.ToDouble(rdr[0], CultureInfo.CurrentCulture);
                        }
                        txtPreviousPayment.Text = previousPayment.ToString();

                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        if (con.State == ConnectionState.Open)
                        {
                            con.Close();
                        }
                    }                        

                    Fill();
                }
                else
                {
                      ResetFeeInfo();   
                    //txtClassFee.Text = "0";
                   // gridControlFeeInfo.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Fill()
        {
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT Discount from Discount where StudentID=@d1 and FeeType='Class'";
                    cmd.Parameters.AddWithValue("@d1", txtStudentID.Text);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        txtDiscountPercentage.Text = rdr.GetValue(0).ToString();
                        double numDiscount;
                        // num = Convert.ToDouble((double.Parse(txtClassFee.Text, cultureToUse) * double.Parse(txtDiscountPercentage.Text)) / (double)100);
                        // num = Convert.ToDouble((double.Parse(txtClassFee.Text, CultureInfo.CurrentCulture) * double.Parse(txtDiscountPercentage.Text)) / (double)100);
                        numDiscount = Convert.ToDouble((double.Parse(txtClassFee.Text, CultureInfo.CurrentCulture) * double.Parse(txtDiscountPercentage.Text)) / (double)100);
                        numDiscount = Math.Round(numDiscount, 2);
                        txtDiscount.Text = numDiscount.ToString();
                    }

                    if (rdr != null)
                        rdr.Close();
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                    
                //don't calculate if reading data from database
                if (getData == false)
                {                   
                        Calculate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Calculate()
        {
            double fineAmount, DiscountAmount, ClassFeeAmount, TotalPaidAmount;
           
            if (txtFine.Text != string.Empty)
            {
                fineAmount = Convert.ToDouble(txtFine.Text.Trim(), CultureInfo.CurrentCulture);
            }
            else
            {
                fineAmount = Convert.ToDouble("0", CultureInfo.CurrentCulture);
            }

            if (txtTotalPaid.Text != string.Empty)
            {
                TotalPaidAmount = Convert.ToDouble(txtTotalPaid.Text.Trim(), CultureInfo.CurrentCulture);
            }
            else
            {
                TotalPaidAmount = Convert.ToDouble("0", CultureInfo.CurrentCulture);
            }
            
            DiscountAmount = Convert.ToDouble(txtDiscount.Text.Trim(), CultureInfo.CurrentCulture);
            ClassFeeAmount = double.Parse(txtClassFee.Text.Trim(),  CultureInfo.CurrentCulture);

            double numTotalDue, numBalance;
              if (previousPayment>0)
              {
                 numTotalDue = ClassFeeAmount + fineAmount  - previousPaid - DiscountAmount;
               }
              else
              {
            numTotalDue = ClassFeeAmount + fineAmount + previousDue - previousPaid - DiscountAmount;
             }
            //numTotalDue = ClassFeeAmount + fineAmount + previousDue - previousPaid - DiscountAmount;

            numTotalDue = Math.Round(numTotalDue, 2);
            txtGrandTotal.Text = numTotalDue.ToString();
            numBalance = numTotalDue - TotalPaidAmount;
            numBalance = Math.Round(numBalance, 2);
            txtBalance.Text = numBalance.ToString();
        }

        private void txtFine_TextChanged(object sender, EventArgs e)
        {
            //don't calculate if reading data from database
            if (getData == false)
            {
                Fill();
            }
        }

        private void txtTotalPaid_TextChanged(object sender, EventArgs e)
        {
           
            //don't calculate if reading data from database
            if (getData == false)
            {
                Fill();
            }
        }

        private void txtClassFee_TextChanged(object sender, EventArgs e)
        {
            //don't calculate if reading data from database
            if (getData == false)
            {
                Calculate();
            }
        }

        private void txtFine_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;

            if (char.IsControl(keyChar))
            {
                //Allow all control characters.
            }
            else if (char.IsDigit(keyChar) || keyChar == '.')
            {
                string text = this.txtFine.Text;
                int selectionStart = this.txtFine.SelectionStart;
                int selectionLength = this.txtFine.SelectionLength;

                text = text.Substring(0, selectionStart) + keyChar + text.Substring(selectionStart + selectionLength);

                if (int.TryParse(text, out int result) && text.Length > 16)
                {
                    //Reject an integer that is longer than 16 digits.
                    e.Handled = true;
                }
                else if (double.TryParse(text, out double results) && text.IndexOf('.') < text.Length - 3)
                {
                    //Reject a real number with two many decimal places.
                    e.Handled = false;
                }
            }
            else
            {
                //Reject all other characters.
                e.Handled = true;
            }
        }

        private void txtTotalPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;

            if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
            {
                if (char.IsControl(keyChar))
                {
                    //Allow all control characters.
                }
                else if (char.IsDigit(keyChar) || keyChar == ',')
                {
                    string text = this.txtTotalPaid.Text;
                    int selectionStart = this.txtTotalPaid.SelectionStart;
                    int selectionLength = this.txtTotalPaid.SelectionLength;

                    text = text.Substring(0, selectionStart) + keyChar + text.Substring(selectionStart + selectionLength);

                    if (int.TryParse(text, out int result) && text.Length > 16)
                    {
                        //Reject an integer that is longer than 16 digits.
                        e.Handled = true;
                    }
                    else if (double.TryParse(text, out double results) && text.IndexOf(',') < text.Length - 3)
                    {
                        //Reject a real number with two many decimal places.
                        e.Handled = false;
                    }
                }
                else
                {
                    //Reject all other characters.
                    e.Handled = true;
                }
            }
            else
            {
                if (char.IsControl(keyChar))
                {
                    //Allow all control characters.
                }
                else if (char.IsDigit(keyChar) || keyChar == '.')
                {
                    string text = this.txtTotalPaid.Text;
                    int selectionStart = this.txtTotalPaid.SelectionStart;
                    int selectionLength = this.txtTotalPaid.SelectionLength;

                    text = text.Substring(0, selectionStart) + keyChar + text.Substring(selectionStart + selectionLength);

                    if (int.TryParse(text, out int result) && text.Length > 16)
                    {
                        //Reject an integer that is longer than 16 digits.
                        e.Handled = true;
                    }
                    else if (double.TryParse(text, out double results) && text.IndexOf('.') < text.Length - 3)
                    {
                        //Reject a real number with two many decimal places.
                        e.Handled = false;
                    }
                }
                else
                {
                    //Reject all other characters.
                    e.Handled = true;
                }

            }                
        }

        private void txtTotalPaid_Validating(object sender, CancelEventArgs e)
        {
            if (double.Parse(txtTotalPaid.Text.Trim(), CultureInfo.CurrentCulture) > double.Parse(txtGrandTotal.Text.Trim(), CultureInfo.CurrentCulture))
            {
                XtraMessageBox.Show(LocRM.GetString("strTotalPaiCannotTotalDue"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPaid.Text = "0"; 
                txtTotalPaid.Focus();
            }
        }

        private void txtTotalPaid_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtTotalPaid.Text == "")
            {
                txtTotalPaid.Text = "0";
            }
        }

        private void txtFine_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtFine.Text == "")
            {
                txtFine.Text = "0";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtFine_Validating(object sender, CancelEventArgs e)
        {
            if (double.Parse(txtTotalPaid.Text.Trim(), CultureInfo.CurrentCulture) > double.Parse(txtGrandTotal.Text.Trim(), CultureInfo.CurrentCulture))
            {
                XtraMessageBox.Show(LocRM.GetString("strTotalPaiCannotGrandTotal"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPaid.Text = "0"; 
                txtTotalPaid.Focus();
            }
        }

        private void barButtonClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Clear();
        }

        private void barButtonSearch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            searchStudent();
        }
        //flag. 1= save, 2 = update
        int saveUpdate = 0;
        private void savePayment()
        {
            if (txtStudentNumber.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strRetrieveStudentDetails"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentNumber.Focus(); 
                return;
            }
            if (txtClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strRetrieveStudentDetails"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClass.Focus();
                return;
            }
            if (lvMonth.CheckedItems.Count <= 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPaymentMonth"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                lvMonth.Focus(); 
                return;
            }
            if (comboPaymentMode.SelectedIndex == -1)
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPaymentMode"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboPaymentMode.Focus(); 
                return;
            }
            if ((Convert.ToDouble(txtTotalPaid.Text, CultureInfo.CurrentCulture) < 0) || (Convert.ToDouble(txtTotalPaid.Text, CultureInfo.CurrentCulture) == 0))
            {
                XtraMessageBox.Show(LocRM.GetString("strTotalPaidGreaterZero"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPaid.Focus(); 
                return;
            }

            if (Convert.ToDouble(txtBalance.Text, CultureInfo.CurrentCulture) < 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strBalanceLessZero"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPaid.Focus(); 
                return;
            }
            try
            {
                for (int i = 0; i < gridViewFee.DataRowCount; i++)
                {

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ct = "select SchoolYear,StudentNumber,Month from CourseFeePayment,CourseFeePayment_Join where CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and SchoolYear=@d1 and StudentNumber=@d2 and Month=@d3";
                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@d1", txtAcademicYear.Text);
                        cmd.Parameters.AddWithValue("@d2", txtStudentNumber.Text);
                        cmd.Parameters.AddWithValue("@d3", gridViewFee.GetRowCellValue(i, LocRM.GetString("strPeriod").ToUpper()));
                        rdr = cmd.ExecuteReader();
                        // if ((rdr.Read()) && (double.Parse(txtPreviousDue.Text, CultureInfo.CurrentCulture) == 0))
                        if ((rdr.Read()) && (double.Parse(txtGrandTotal.Text, CultureInfo.CurrentCulture) == 0))
                        {
                            DataRow row = gridViewFee.GetDataRow(i);                            
                            XtraMessageBox.Show(LocRM.GetString("strAlreadyPaid") + " " + LocRM.GetString("strforTheMonth") + " " + row[LocRM.GetString("strMonth").ToUpper()].ToString(), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if (rdr != null)
                            {
                                rdr.Close();
                            }
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                            }
                            return;
                        }
                    }
                        
                }            
            //generate receipt ID
            auto();

                if (IDindex > 2147483600) //SQL int up to: 2,147,483,647
                {
                    XtraMessageBox.Show(LocRM.GetString("strResetReceiptCounter"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm(); 
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving"));
                }
                
                Cursor = Cursors.WaitCursor;
                receiptNumber =  txtFeePaymentID.Text.Trim();
                payemntID = txtCFPId.Text.Trim();
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    //Create a transanction object, only commit to database if insert into CourseFeePayment and insert into CourseFeePayment_Join
                    // are succeful otherwise rollback.
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        string cb = "insert into CourseFeePayment(CourseFeePaymentID, PaymentID, StudentNumber, SchoolYear,TotalFee, DiscountPer, DiscountAmt, PreviousDue, Fine, GrandTotal, TotalPaid, ModeOfPayment, PaymentDate, PaymentDue,Student_Class,Employee) VALUES (@d16,@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15)";// v1.2.0
                        //string cb = "insert into CourseFeePayment(PaymentID, StudentNumber, SchoolYear,TotalFee, DiscountPer, DiscountAmt, PreviousDue, Fine, GrandTotal, TotalPaid, ModeOfPayment, PaymentDate, PaymentDue,Student_Class,Employee) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15)"; //v1.2.1, remove the id culumn: CourseFeePaymentID and set it to auto increment
                        cmd = new SqlCommand(cb, con, transaction);

                        // cmd.Connection = con;
                       // cmd.Parameters.AddWithValue("@d16", Convert.ToInt16(txtCFPId.Text)); //int16 to small up to 32 000+, 
                        cmd.Parameters.AddWithValue("@d16", Convert.ToInt32(txtCFPId.Text)); //use  int (32bit) up to: 0 to 2,147,483,647
                        cmd.Parameters.AddWithValue("@d1", txtFeePaymentID.Text);
                        cmd.Parameters.AddWithValue("@d2", txtStudentNumber.Text);
                        cmd.Parameters.AddWithValue("@d3", txtAcademicYear.Text);
                        cmd.Parameters.AddWithValue("@d4", decimal.Parse(txtClassFee.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d5", decimal.Parse(txtDiscountPercentage.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d6", decimal.Parse(txtDiscount.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d7", decimal.Parse(txtPreviousDue.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d8", decimal.Parse(txtFine.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d9", decimal.Parse(txtGrandTotal.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d10", decimal.Parse(txtTotalPaid.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d11", comboPaymentMode.Text);
                        cmd.Parameters.AddWithValue("@d12", datePaymentDate.EditValue);

                        cmd.Parameters.AddWithValue("@d13", decimal.Parse(txtBalance.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d14", txtClass.Text);
                        cmd.Parameters.AddWithValue("@d15", UserLoggedSurname);
                        cmd.ExecuteNonQuery();
                        
                       //execute 2nd query
                        string cb1 = "insert into CourseFeePayment_Join(C_PaymentID,Month, FeeName, Fee) VALUES (" + txtCFPId.Text + ",@d1,@d2,@d3)";
                        cmd = new SqlCommand(cb1, con, transaction);
                        
                        // Prepare command for repeated execution
                        cmd.Prepare();

                        // Data to be inserted
                        for (int i = 0; i < gridViewFee.DataRowCount; i++)
                        {
                            //   DataRow row = gridViewFee.GetDataRow(i);
                            if (!gridViewFee.IsNewItemRow(i))
                            {
                                cmd.Parameters.AddWithValue("@d1", gridViewFee.GetRowCellValue(i, LocRM.GetString("strPeriod").ToUpper()));  
                                cmd.Parameters.AddWithValue("@d2", gridViewFee.GetRowCellValue(i, LocRM.GetString("strFeeName").ToUpper()));
                                cmd.Parameters.AddWithValue("@d3", gridViewFee.GetRowCellValue(i, LocRM.GetString("strFee").ToUpper()));
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }
                        //commit the transanction
                        transaction.Commit();
                        //close the connection
                        con.Close();

                        Cursor = Cursors.Default;
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyPaid"), LocRM.GetString("strFee"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        //rollback the transanction form the pending state
                        transaction.Rollback();
                    
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        Cursor = Cursors.Default;
                        XtraMessageBox.Show(LocRM.GetString("strSchoolFeesNotSaved")+". " + LocRM.GetString("strError")+": "+ ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            
                if (savePrint==true) 
                {
                    Print();
                    savePrint = false;
                }
                //Log transaction in LedgerBook
                pf.LedgerSave(Convert.ToDateTime(datePaymentDate.EditValue), txtStudentSurname.Text.Trim() + " " + txtStudentFirstNames.Text.Trim(), txtFeePaymentID.Text.Trim(), "Class Fee Payment", Convert.ToDecimal(txtGrandTotal.Text), 0, txtStudentNumber.Text.Trim());
                if (comboPaymentMode.Text == LocRM.GetString("strCash"))
                    pf.LedgerSave(Convert.ToDateTime(datePaymentDate.EditValue), "Cash Account", txtFeePaymentID.Text, "Payment", 0, Convert.ToDecimal(txtTotalPaid.Text, CultureInfo.CurrentCulture), txtStudentNumber.Text);
                if (comboPaymentMode.Text == LocRM.GetString("strBankDeposit") | comboPaymentMode.Text == LocRM.GetString("strBankCard"))
                    pf.LedgerSave(Convert.ToDateTime(datePaymentDate.EditValue), "Bank Account", txtFeePaymentID.Text, "Payment", 0, Convert.ToDecimal(txtTotalPaid.Text, CultureInfo.CurrentCulture), txtStudentNumber.Text);

                //Log record transaction in logs
                string st = LocRM.GetString("strCourseFeePaymentAdded") + " " + txtFeePaymentID.Text;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st); 

                //Student fee payment 0= no notification, = 1 send SMS, 2= send email, 3 send both
                //send notification
                saveUpdate = 1;
                if (Properties.Settings.Default.InvoiceNotification == 1)
                {
                    if (txtNotificationNo.Text.Trim() != "")
                    {
                        sendSMS();
                    }
                }
                if (Properties.Settings.Default.InvoiceNotification == 2)
                {
                    if (txtNotificationEmail.Text.Trim() != "")
                    {
                        sendEmail();
                    }
                }
                if (Properties.Settings.Default.InvoiceNotification == 3)
                {
                    if (txtNotificationNo.Text.Trim() != "")
                    {
                        sendSMS();
                    }
                    if (txtNotificationEmail.Text.Trim() != "")
                    {
                        sendEmail();
                    }
                }

                //Clear all controls
                Reset();
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
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            savePrint = false;
            savePayment();
        }
        private void Print()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting")); 
            }

            if (Properties.Settings.Default.ReceiptPrinter.ToString() !="")
            {
                    //Print on a 58mm paper roll
                    if (Properties.Settings.Default.ReceiptPrinterType.ToString() == "58mm")
                    {
                        var storage58mm = new MemoryDocumentStorage();
                        var report58mm = new reportInvoice();

                        // Disable print ouside margins warning.
                        // report.ShowPrintMarginsWarning = false; doesn't work

                        PrinterSettings printerSettings = new PrinterSettings();

                        // Specify the PrinterName if the target printer is not the default one. 
                        // printerSettings.PrinterName = Properties.Settings.Default.ReceiptPrinter; 

                        var cachedReportSource58mm = new CachedReportSource(report58mm, storage58mm);
                        var printTool = new ReportPrintTool(cachedReportSource58mm);
                        // Disable print ouside margins warning.
                        if (Properties.Settings.Default.Printer58mmMarginWarnings==false)
                        {
                            printTool.PrintingSystem.ShowMarginsWarning = false;
                        }
                        else
                        {
                            printTool.PrintingSystem.ShowMarginsWarning = true;
                        }
                   
                       printTool.Print(Properties.Settings.Default.ReceiptPrinter);
                     }
                    //Print on a 80mm paper roll
                    else if (Properties.Settings.Default.ReceiptPrinterType.ToString() == "80mm")
                    {
                        var storage80mm = new MemoryDocumentStorage();
                        var report80mm = new reportInvoice80mm();

                        // Disable print ouside margins warning.
                        // report.ShowPrintMarginsWarning = false; doesn't work

                        PrinterSettings printerSettings = new PrinterSettings();

                        // Specify the PrinterName if the target printer is not the default one. 
                        // printerSettings.PrinterName = Properties.Settings.Default.ReceiptPrinter; 

                        var cachedReportSource80mm = new CachedReportSource(report80mm, storage80mm);
                        var printTool = new ReportPrintTool(cachedReportSource80mm);
                        // Disable print ouside margins warning.
                        
                        // Disable print ouside margins warning.
                        if (Properties.Settings.Default.Printer80mmMarginWarnings == false)
                        {
                            printTool.PrintingSystem.ShowMarginsWarning = false;
                        }
                        else
                        {
                            printTool.PrintingSystem.ShowMarginsWarning = true;
                        }

                        printTool.Print(Properties.Settings.Default.ReceiptPrinter);
                    }
                    //Print on a 40mm width x 30mm Height sticker paper
                    else if (Properties.Settings.Default.ReceiptPrinterType.ToString() == "40W_30Hmm")
                    {
                        var storage40W_30Hmm = new MemoryDocumentStorage();
                        var report40W_30Hmm = new reportInvoiceSticker40_30mm();

                        // Disable print ouside margins warning.
                        // report.ShowPrintMarginsWarning = false; doesn't work

                        PrinterSettings printerSettings = new PrinterSettings();

                        // Specify the PrinterName if the target printer is not the default one. 
                        // printerSettings.PrinterName = Properties.Settings.Default.ReceiptPrinter; 

                        var cachedReportSource40W_30Hmm = new CachedReportSource(report40W_30Hmm, storage40W_30Hmm);
                        var printTool = new ReportPrintTool(cachedReportSource40W_30Hmm);
                        // Disable print ouside margins warning.

                        printTool.PrintingSystem.ShowMarginsWarning = false;

                        printTool.Print(Properties.Settings.Default.ReceiptPrinter);
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strReceiptNotPrintedWrongConfig"), LocRM.GetString("strPrintError"), MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    }
                }
            else
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(LocRM.GetString("strReceiptNotPrinted"), LocRM.GetString("strPrintError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
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
        // send SMS
        private void sendSMS()
        {
            try
            {
                if (pf.CheckForInternetConnection() == true)
                {
                    /// There is also a Visual Studio NuGet Package available, see
                    /// https://get.cmtelecom.com/microsoft-dotnet-nuget-pack/
                    /// http://www.cmtelecom.com/mobile-messaging/plugins/microsoft-dotnet-nuget-pack
                    try
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSendingSMS"));
                        }
                        

                       Cursor = Cursors.WaitCursor;

                        // validate PhoneNumber;
                        string cellNo = txtNotificationNo.Text;
                        cellNo = cellNo.Substring(1);

                        //Check if SMS settings saved in application settings
                        if (Properties.Settings.Default.InternationalCode != "")
                        {
                            cellNo = Properties.Settings.Default.InternationalCode + cellNo;
                        }
                        else
                        {
                            //read SMS settings from database
                            try
                            {
                                //Check if Company Profile has data in database

                                using (con = new SqlConnection(databaseConnectionString))
                                {
                                    con.Open();
                                    string ct = "select * from SMSSettings ";

                                    cmd = new SqlCommand(ct);
                                    cmd.Connection = con;
                                    rdr = cmd.ExecuteReader();

                                    if (rdr.HasRows)
                                    {
                                        //load data from database to controls
                                        if (rdr.Read())
                                        {
                                            //save SMS gateway profile in application settings to be accessed faster than from database                                       
                                            Properties.Settings.Default.SettingSMSProvider = (rdr.GetString(1).ToString().Trim());
                                            Properties.Settings.Default.SettingSMSCompany = (rdr.GetString(2).ToString().Trim());
                                            Properties.Settings.Default.SettingSMSTocken = (rdr.GetString(3).ToString().Trim());
                                            Properties.Settings.Default.InternationalCode = (rdr.GetString(4).ToString().Trim());

                                            cellNo = (rdr.GetString(4).ToString().Trim()) + cellNo;

                                            // ----- Save any updated settings.
                                            Properties.Settings.Default.Save();
                                        }
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(LocRM.GetString("strNoSMSNotificationSent"), LocRM.GetString("strNotificationError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    if (rdr != null)
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

                        //Check if SMS notification template saved in application settings
                        string PaymentNotificationTemplate = "";
                        if (Properties.Settings.Default.PaymentNotificationSMS != "")
                        {
                            PaymentNotificationTemplate = Properties.Settings.Default.PaymentNotificationSMS;
                        }
                        else
                        {
                            //read SMS notification template  from database
                            try
                            {
                                //Check if SMS notification template has data in database

                                using (con = new SqlConnection(databaseConnectionString))
                                {
                                    con.Open();
                                    string ct = "select TemplateMessage from SMSMessageTemplates where MessageID =@d1"; 
                                    cmd = new SqlCommand(ct);
                                    cmd.Connection = con;
                                    cmd.Parameters.AddWithValue("@d1", 1);
                                    rdr = cmd.ExecuteReader();

                                    if (rdr.HasRows)
                                    {
                                        //load data message template from database
                                        if (rdr.Read())
                                        {
                                            //save SMS message template in application settings to be accessed faster than from database                                       
                                            Properties.Settings.Default.PaymentNotificationSMS = (rdr.GetString(0).Trim());
                                            PaymentNotificationTemplate = (rdr.GetString(0).Trim());

                                            // ----- Save any updated settings.
                                            Properties.Settings.Default.Save();
                                        }
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(LocRM.GetString("strNoSMSNotificationSent"), LocRM.GetString("strNotificationError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                if (splashScreenManager1.IsSplashFormVisible == true)
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }

                        // PaymentNotificationTemplate =   string.Format("Bonjour cher parents, élève {0} {1} vient de payer {2}$ des frais scolaire. Reste à payer: {3}$. Merci CS Les Calinours", txtStudentSurname.Text.ToUpper(), txtStudentFirstNames.Text.ToUpper(), txtTotalPaid.Text, Properties.Settings.Default.CurrencySymbol);
                        PaymentNotificationTemplate = string.Format(PaymentNotificationTemplate, txtStudentSurname.Text.ToUpper(), txtStudentFirstNames.Text.ToUpper(), txtTotalPaid.Text, txtBalance.Text);

                        SmsGatewayClient smsGateway = new SmsGatewayClient(pf.Decrypt(Properties.Settings.Default.SettingSMSTocken));
                        if (saveUpdate == 1)
                        {
                            // smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, cellNo, LocRM.GetString("strHi")+ " " + txtStudentSurname.Text + " " + LocRM.GetString("strSuccessfullyPaidClassFee") + ". " + LocRM.GetString("strReceiptNo") + ": " + txtFeePaymentID.Text + ". " + LocRM.GetString("strTotalPaid")+ ": " + txtTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + ". " + LocRM.GetString("strBalanceToPay") + ": " + txtBalance.Text + Properties.Settings.Default.CurrencySymbol);
                            smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, cellNo, PaymentNotificationTemplate);
                            //Log send SMS transaction in SMS logs
                            string st = LocRM.GetString("strSchoolFeesPaymentNotification") + ": " + LocRM.GetString("strSurname") + ": " + txtStudentSurname.Text + " " + LocRM.GetString("strName") + ": " + txtStudentFirstNames.Text;
                            pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, cellNo);
                        }
                        if (saveUpdate == 2) 
                        {
                            smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, cellNo, LocRM.GetString("strHi") + " " + txtStudentSurname.Text + " " + LocRM.GetString("strclassFeeInvoiceNo") + " " + txtFeePaymentID.Text + " " + LocRM.GetString("strHasBeenUpdated") + ". " + LocRM.GetString("strTotalPaid") + ": " + txtTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + ". " + LocRM.GetString("strBalanceToPay") + ": " + txtBalance.Text + Properties.Settings.Default.CurrencySymbol);
                            //Log send SMS transaction in SMS logs
                            string st = LocRM.GetString("strSchoolFeesPaymentUpdateNotification") + ": " + LocRM.GetString("strSurname") + ": " + txtStudentSurname.Text + " " + LocRM.GetString("strName") + ": " + txtStudentFirstNames.Text;
                            pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, cellNo);
                        }
                        // saveUpdate = 0;
                        Cursor = Cursors.Default;
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
                    XtraMessageBox.Show(LocRM.GetString("strNoInternetToSendSMS"), LocRM.GetString("strSendSMS"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Failed to send SMS due to internet connection transaction in logs  
                    if (saveUpdate == 1)
                    {
                        string st = LocRM.GetString("strNoInternetToSendSMS") + ", "+ LocRM.GetString("strSuccessfullyPaidClassFee")+". " + LocRM.GetString("strStudent")+ ": " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + ", " +  LocRM.GetString("strStudentNo")+ ": " + txtStudentNumber.Text + ". " + LocRM.GetString("strReceiptNo")  +": " + txtFeePaymentID.Text + ". " + LocRM.GetString("strTotalPaid") + ": " + txtTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + ". " + LocRM.GetString("strBalanceToPay") + ": " + txtBalance.Text + Properties.Settings.Default.CurrencySymbol;
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                    }
                    if (saveUpdate == 2)
                    {
                        string st = LocRM.GetString("strNoInternetToSendSMS") + ", " + LocRM.GetString("strClassFeePaymentDetails") + ": " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text +  LocRM.GetString("strHasBeenUpdated") + ". " + LocRM.GetString("strReceiptNo") + ": " + txtFeePaymentID.Text + ". " + LocRM.GetString("strTotalPaid") + ": " + txtTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + ". " + LocRM.GetString("strBalanceToPay") + ": " + txtBalance.Text + Properties.Settings.Default.CurrencySymbol;
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                    }
                    //  saveUpdate = 0;
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
        //send Email
        private void sendEmail()
        {
            try
            {
                if (pf.CheckForInternetConnection() == true)
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSendingEmail")); 
                    }
                    
                    Cursor = Cursors.WaitCursor;

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ctn = "select RTRIM(Username),RTRIM(Password),RTRIM(SMTPAddress),(Port),RTRIM(SenderName) from EmailSetting where IsDefault='Yes' and IsActive='Yes'";
                        cmd = new SqlCommand(ctn);
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            if (saveUpdate == 1)
                            {
                                pf.SendMail(Convert.ToString(rdr.GetValue(0)), txtNotificationEmail.Text, LocRM.GetString("strStudent") + " " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strSuccessfullyPaidClassFee") + ". " + LocRM.GetString("strReceiptNo") + ": " + txtFeePaymentID.Text + ". " + LocRM.GetString("strTotalPaid") + ": " + txtTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + ". " + LocRM.GetString("strBalanceToPay") + ": " + txtBalance.Text + Properties.Settings.Default.CurrencySymbol, LocRM.GetString("strStudentClassFeesPayment"), Convert.ToString(rdr.GetValue(2)), Convert.ToInt16(rdr.GetValue(3)), Convert.ToString(rdr.GetValue(0)), pf.Decrypt(Convert.ToString(rdr.GetValue(1))), Convert.ToString(rdr.GetValue(4)));

                            }
                            if (saveUpdate == 2)
                            {
                                pf.SendMail(Convert.ToString(rdr.GetValue(0)), txtNotificationEmail.Text, LocRM.GetString("strClassFeePaymentDetails") + " " + LocRM.GetString("strStudent") + ": " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text + " " + LocRM.GetString("strHasBeenUpdated") + ". " + LocRM.GetString("strReceiptNo") + ": " + txtFeePaymentID.Text + ". " + LocRM.GetString("strTotalPaid") + ": " + txtTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + ". " + LocRM.GetString("strBalanceToPay") + ": " + txtBalance.Text + Properties.Settings.Default.CurrencySymbol, LocRM.GetString("strStudentClassFeesPaymentUpdated"), Convert.ToString(rdr.GetValue(2)), Convert.ToInt16(rdr.GetValue(3)), Convert.ToString(rdr.GetValue(0)), pf.Decrypt(Convert.ToString(rdr.GetValue(1))), Convert.ToString(rdr.GetValue(4)));

                            }
                            if ((rdr != null))
                            {
                                rdr.Close();
                            }
                            con.Close();
                            // saveUpdate = 0;
                        }
                    }
                        
                    Cursor = Cursors.Default;
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                }
                else
                {
                    XtraMessageBox.Show( LocRM.GetString("strNoInternetToSendEmail"), LocRM.GetString("strStudentClassFeesPayment"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Failed to send email due to internet connection transaction in logs
                    if (saveUpdate == 1)
                    {
                        string st = LocRM.GetString("strNoInternetToSendEmail") +". " + LocRM.GetString("strStudent")+": " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + LocRM.GetString("strSuccessfullyPaidClassFee") + ". " + LocRM.GetString("strReceiptNo") + ": " + txtFeePaymentID.Text + ". " + LocRM.GetString("strTotalPaid") + txtTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + ". " + LocRM.GetString("strBalanceToPay") + ": " + txtBalance.Text + Properties.Settings.Default.CurrencySymbol;
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                    }
                    if (saveUpdate == 2)
                    {
                        string st = LocRM.GetString("strNoInternetToSendEmail") + ". " + LocRM.GetString("strClassFeePaymentDetails") + " " + LocRM.GetString("strStudent") + ": " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text + " " + LocRM.GetString("strHasBeenUpdated") + ". " + LocRM.GetString("strReceiptNo") + ": " + txtFeePaymentID.Text + ". " + LocRM.GetString("strTotalPaid") + txtTotalPaid.Text + Properties.Settings.Default.CurrencySymbol + ". " + LocRM.GetString("strBalanceToPay") + ": " + txtBalance.Text + Properties.Settings.Default.CurrencySymbol;
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                    }
                    //  saveUpdate = 0;
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
        private void auto()
        {
            try
            {
                txtCFPId.Text = GenerateID();
                string studentNo = txtStudentNumber.Text;
                // txtFeePaymentID.Text = "CFE-" + GenerateID() + "-" + studentNo;
                //txtFeePaymentID.Text = studentNo + GenerateID(); version 1.2.0
                txtFeePaymentID.Text = studentNo + txtCFPId.Text; //version 1.2.1
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
        uint IDindex = 0;  //version v1.2.1 increase to ulong: 0 to 4,294,967,295
        private string GenerateID()
        {           
            string value = "0";
            //int IDindex = 0;  limits to 16 bit int up to 32,767
            
            try
            {
                // Fetch the latest ID from the database
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT TOP 1 (CourseFeePaymentID) FROM CourseFeePayment ORDER BY CourseFeePaymentID DESC", con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        IDindex = Convert.ToUInt32(rdr["CourseFeePaymentID"]);                       
                    }
                    rdr.Close();                    
                }
                    
                // Increase the ID by 1
                IDindex++;
                // Because incrementing a string with an integer removes 0's
                // we need to replace them. If necessary.
                if (IDindex <= 9)
                    value = "00" + value + IDindex.ToString();
                else if (IDindex <= 99)
                    value = "0" + value + IDindex.ToString();
                else if (IDindex <= 999)
                    //value = "0" + value + IDindex.ToString();
                    value =  value + IDindex.ToString();
                else
                {
                    value = IDindex.ToString();
                }
            }
            catch (Exception ex)
            {
                // If an error occurs, check the connection state and close it if necessary.
                if (con.State == ConnectionState.Open)
                    con.Close();
                //value = "00";
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            return value;
        }
        //Fill comboPaymentMode
        private void fillPaymentMode()
        {
            comboPaymentMode.Properties.Items.Clear();
            comboPaymentMode.Properties.Items.AddRange(new object[] { LocRM.GetString("strCash"),
            LocRM.GetString("strBankDeposit") , LocRM.GetString("strBankCard") });
        }
        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reset();
            enableGroup();            
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            btnSavePrint.Enabled = true;
            txtStudentNumber.Focus();
            fillPaymentMode();
        }
        private void enableGroup()
        {
            groupStudentDetails.Enabled = true;
            groupPhoto.Enabled = true;
            groupPaymentInformation.Enabled = true;
            groupListMonths.Enabled = true;
            groupFeeInformation.Enabled = true;
            gridControlListStudents.Enabled = true;
        }
        private void disableGroup()
        {
            groupStudentDetails.Enabled = false;
            groupPhoto.Enabled = false;
            groupPaymentInformation.Enabled = false;
            groupListMonths.Enabled = false;
            groupFeeInformation.Enabled = false;
            gridControlListStudents.Enabled = false;
        }

        private void txtTotalPaid_Click(object sender, EventArgs e)
        {
            txtTotalPaid.Focus();
            txtTotalPaid.SelectAll();
        }

        private void txtFine_Click(object sender, EventArgs e)
        {
            txtFine.Focus();
            txtFine.SelectAll();
        }
      
       

        private void btnCalculator_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                System.Diagnostics.Process.Start("Calc.exe");
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

        private void btnNotepad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                System.Diagnostics.Process.Start("Notepad.exe");
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show(LocRM.GetString("strDeleteConfirmQuestion"),  LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        private void delete_records()
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                int RowsAffected = 0;
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cq = "delete from CourseFeePayment  where CourseFeePaymentID = '" + txtCFPId.Text.Trim() + "'";
                    cmd = new SqlCommand(cq);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    if (RowsAffected > 0)
                    {
                        pf.LedgerDelete(txtFeePaymentID.Text, LocRM.GetString("strStudentClassFeesPayment"));
                        pf.LedgerDelete(txtFeePaymentID.Text, LocRM.GetString("strPayment"));
                        //Log send SMS transaction in logs
                        string st = LocRM.GetString("strClassFeePaymentDetails") + ": " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text + ", " + LocRM.GetString("strReceiptNo") + ": " + txtFeePaymentID.Text + " " + LocRM.GetString("strBeenDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strStudentRecords"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strStudentRecords"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
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
                Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool savePrint = false;
        private void btnSavePrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            savePrint = true;
            savePayment();
            
        }
        Students.frmListClassFeePayment listFeePayment;
        private void btnGetData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            if (listFeePayment == null)
            {
                listFeePayment = new frmListClassFeePayment(); //Create form if not created
                //Save list fee data before frmListClassFeePayment close
                listFeePayment.FormClosing += new FormClosingEventHandler(ListFeePaymentFormClosing);
                listFeePayment.FormClosed += ListFeePayment_FormClosed; //Add eventhandler to cleanup after form closes                
            }

            listFeePayment.ShowDialog(this);  //Show Form assigning this form as the forms owner   
        }

        private void ListFeePayment_FormClosed(object sender, FormClosedEventArgs e)
        {
            listFeePayment = null;  //If form is closed make sure reference is set to null 
        }
        private void ListFeePaymentFormClosing(object sender, FormClosingEventArgs e)
        {
            txtFeePaymentID.Text = receiptNumber;
            txtCFPId.Text = payemntID;
            if (txtFeePaymentID.Text.Trim() != "")
            {   
                populateForm();
                getData = false;
                gridControlListStudents.Enabled = false;
            }
        }
        bool getData = false;
        private void populateForm()
        {
            getData = true;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                Reset();
                txtFeePaymentID.Text = receiptNumber;
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = con.CreateCommand();

                    cmd.CommandText = "SELECT * FROM Students, CourseFeePayment  WHERE Students.StudentNumber=CourseFeePayment.StudentNumber and PaymentID = '" + txtFeePaymentID.Text.Trim() + "' ";
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        txtStudentNumber.Text = (rdr.GetString(1).Trim());
                        txtSection.Text = (rdr.GetString(2).Trim());
                        txtClass.Text = (rdr.GetString(4).Trim());
                        txtStudentSurname.Text = (rdr.GetString(5).Trim());
                        txtStudentFirstNames.Text = (rdr.GetString(6).Trim());
                        txtNotificationNo.Text = (rdr.GetString(26).Trim());
                        txtNotificationEmail.Text = (rdr.GetString(27).Trim());
                        txtAcademicYear.Text = (rdr.GetString(37).Trim());

                        if (!Convert.IsDBNull(rdr["StudentPicture"]))
                        {
                            byte[] x = (byte[])rdr["StudentPicture"];
                            MemoryStream ms = new MemoryStream(x);
                            pictureStudent.Image = Image.FromStream(ms);
                            pictureStudent.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                        }
                        else
                        {
                            pictureStudent.EditValue = null;
                        }

                        txtCFPId.Text = (rdr.GetValue(44).ToString());
                        datePaymentDate.EditValue = (rdr.GetValue(56));
                        comboPaymentMode.Text = (rdr.GetString(55).Trim());

                        txtClassFee.Text = (rdr.GetDecimal(48).ToString());
                        txtDiscount.Text = (rdr.GetDecimal(50).ToString());
                        txtDiscountPercentage.Text = (rdr.GetDecimal(49).ToString());
                        txtPreviousDue.Text = (rdr.GetDecimal(51).ToString());
                        txtFine.Text = (rdr.GetDecimal(52).ToString());
                        txtGrandTotal.Text = (rdr.GetDecimal(53).ToString());
                        txtTotalPaid.Text = (rdr.GetDecimal(54).ToString());
                        txtBalance.Text = (rdr.GetDecimal(57).ToString());
                        // txtPreviousPayment.Text = (rdr.GetDecimal(1).ToString());

                        if ((Role == 1) || (Role == 2))//Administrator, Administrator Assistant only
                        {
                            btnUpdate.Enabled = true;
                            btnDelete.Enabled = true;
                        }
                        else
                        {
                            btnUpdate.Enabled = false;
                            btnDelete.Enabled = false;
                        }

                        btnPrint.Enabled = true;
                        btnSave.Enabled = false;
                        btnSavePrint.Enabled = false;
                        btnNew.Enabled = true;
                        lvMonth.Enabled = false;
                        datePaymentDate.Enabled = false;
                        txtTotalPaid.Enabled = false;
                        txtFine.Enabled = false;
                        fillPaymentMode();
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



                //populate month and fee name datagrid
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string sql = "Select RTRIM(Month),RTRIM(FeeName),CourseFeePayment_Join.Fee from CourseFeePayment, CourseFeePayment_Join  where  CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and CourseFeePayment.CourseFeePaymentID= @d1 ";
                    cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@d1", txtCFPId.Text.Trim());
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    //clear gridcontrol
                    gridControlFeeInfo.DataSource = null;
                    //load culumns in gridControlFeeInfo
                    gridControlFeeInfo.DataSource = CreateData();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridViewFee.AddNewRow();
                        gridViewFee.UpdateCurrentRow();

                        var row = gridViewFee.GetRow(gridViewFee.GetVisibleRowHandle(gridViewFee.RowCount - 1));
                        int rowHandle = gridViewFee.GetVisibleRowHandle(gridViewFee.RowCount - 1);

                        gridViewFee.SetRowCellValue(rowHandle, gridViewFee.Columns[LocRM.GetString("strPeriod").ToUpper()], rdr[0].ToString());
                        gridViewFee.SetRowCellValue(rowHandle, gridViewFee.Columns[LocRM.GetString("strFeeName").ToUpper()], rdr[1].ToString());
                        gridViewFee.SetRowCellValue(rowHandle, gridViewFee.Columns[LocRM.GetString("strFee").ToUpper()], rdr[2].ToString());

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

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtStudentNumber.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strRetrieveStudentDetails"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentNumber.Focus();
                return;
            }
            if (txtClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strRetrieveStudentDetails"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClass.Focus();
                return;
            }
           
            if (comboPaymentMode.SelectedIndex == -1)
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectPaymentMode"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboPaymentMode.Focus();
                return;
            }
            if ((Convert.ToDouble(txtTotalPaid.Text, CultureInfo.CurrentCulture) < 0) || (Convert.ToDouble(txtTotalPaid.Text, CultureInfo.CurrentCulture) == 0))
            {
                XtraMessageBox.Show( LocRM.GetString("strTotalPaidGreaterZero"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPaid.Focus();
                return;
            }

            if (Convert.ToDouble(txtBalance.Text, CultureInfo.CurrentCulture) < 0)
            {
                XtraMessageBox.Show( LocRM.GetString("strBalanceLessZero"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTotalPaid.Focus();
                return;
            }
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription( LocRM.GetString("strUpdating"));
                }
                
                Cursor = Cursors.WaitCursor;

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    //Create a transanction object, only commit to database if insert into CourseFeePayment and insert into CourseFeePayment_Join
                    // are succeful otherwise rollback.
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        string cb = "Update CourseFeePayment set CourseFeePaymentID=@d1, PaymentID=@d2, StudentNumber=@d3, SchoolYear=@d4,TotalFee=@d6, DiscountPer=@d7, DiscountAmt=@d8, PreviousDue=@d9, Fine=@d10, GrandTotal=@d11, TotalPaid=@d12, ModeOfPayment=@d13,Student_Class=@d14, PaymentDue=@d15 where CourseFeePaymentID= '" + txtCFPId.Text.Trim() + "'";
                        cmd = new SqlCommand(cb, con, transaction);
                        
                        cmd.Parameters.AddWithValue("@d1", Convert.ToInt16(txtCFPId.Text));
                        cmd.Parameters.AddWithValue("@d2", txtFeePaymentID.Text);
                        cmd.Parameters.AddWithValue("@d3", txtStudentNumber.Text);
                        cmd.Parameters.AddWithValue("@d4", txtAcademicYear.Text);
                        cmd.Parameters.AddWithValue("@d6", decimal.Parse(txtClassFee.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d7", decimal.Parse(txtDiscountPercentage.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d8", decimal.Parse(txtDiscount.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d9", decimal.Parse(txtPreviousDue.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d10", decimal.Parse(txtFine.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d11", decimal.Parse(txtGrandTotal.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d12", decimal.Parse(txtTotalPaid.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d13", comboPaymentMode.Text);
                        cmd.Parameters.AddWithValue("@d14", txtClass.Text);
                        cmd.Parameters.AddWithValue("@d15", decimal.Parse(txtBalance.Text, CultureInfo.CurrentCulture));
                        cmd.ExecuteNonQuery();

                        //execute 2nd query
                        string cb1 = "delete from CourseFeePayment_Join where C_PaymentID= '" + txtCFPId.Text.Trim() + "'";
                        cmd = new SqlCommand(cb1,con,transaction);                        
                        cmd.ExecuteNonQuery();

                        //execute 3rd query
                        string cb2 = "insert into CourseFeePayment_Join(C_PaymentID,Month, FeeName, Fee) VALUES ('" + txtCFPId.Text.Trim() + "',@d1,@d2,@d3)";
                        cmd = new SqlCommand(cb2,con,transaction);                        
                        // Prepare command for repeated execution
                        cmd.Prepare();
                        // Data to be inserted

                        for (int i = 0; i < gridViewFee.DataRowCount; i++)
                        {
                            //   DataRow row = gridViewFee.GetDataRow(i);
                            if (!gridViewFee.IsNewItemRow(i))
                            {
                                cmd.Parameters.AddWithValue("@d1", gridViewFee.GetRowCellValue(i, LocRM.GetString("strPeriod").ToUpper()));
                                cmd.Parameters.AddWithValue("@d2", gridViewFee.GetRowCellValue(i, LocRM.GetString("strFeeName").ToUpper()));
                                cmd.Parameters.AddWithValue("@d3", gridViewFee.GetRowCellValue(i, LocRM.GetString("strFee").ToUpper()));
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();
                            }
                        }

                        //commit the transanction
                        transaction.Commit();
                        //close the connection
                        con.Close();

                        Cursor = Cursors.Default;
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strFee"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        //rollback the transanction form the pending state
                        transaction.Rollback();
                        Cursor = Cursors.Default;
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }

                        XtraMessageBox.Show(LocRM.GetString("strSchoolFeesNotUpdated") + ". " + LocRM.GetString("strError") + ": " + ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                    
                
                //Log transaction in LedgerBook
                pf.LedgerUpdate(Convert.ToDateTime(datePaymentDate.EditValue), txtStudentSurname.Text + " " + txtStudentFirstNames.Text, Convert.ToDecimal(txtGrandTotal.Text, CultureInfo.CurrentCulture),0, txtStudentNumber.Text, txtFeePaymentID.Text, "Class Fee Payment");
                if (comboPaymentMode.Text == LocRM.GetString("strCash"))
                    pf.LedgerUpdate(Convert.ToDateTime(datePaymentDate.EditValue), "Cash Account",0, Convert.ToDecimal(txtTotalPaid.Text, CultureInfo.CurrentCulture), txtStudentNumber.Text, txtFeePaymentID.Text, "Payment");
                if (comboPaymentMode.Text == LocRM.GetString("strBankDeposit") | comboPaymentMode.Text == LocRM.GetString("strBankCard"))
                    pf.LedgerUpdate(Convert.ToDateTime(datePaymentDate.EditValue), "Bank Account",0, Convert.ToDecimal(txtTotalPaid.Text, CultureInfo.CurrentCulture), txtStudentNumber.Text, txtFeePaymentID.Text, "Payment");

                //Log record transaction in logs
                string st =  LocRM.GetString("strCourseFeePaymentUpdated") + ": "+ txtFeePaymentID.Text;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //Student fee payment 0= no notification, = 1 send SMS, 2= send email, 3 send both
                //send notification
                saveUpdate = 2;
                if (Properties.Settings.Default.InvoiceNotification == 1)
                {
                    if (txtNotificationNo.Text.Trim() != "")
                    {
                        sendSMS();
                    }
                }
                if (Properties.Settings.Default.InvoiceNotification == 2)
                {
                    if (txtNotificationEmail.Text.Trim() != "")
                    {
                        sendEmail();
                    }
                }
                if (Properties.Settings.Default.InvoiceNotification == 3)
                {
                    if (txtNotificationNo.Text.Trim() != "")
                    {
                        sendSMS();
                    }
                    if (txtNotificationEmail.Text.Trim() != "")
                    {
                        sendEmail();
                    }
                }

                //Clear all controls
                Reset();
            }
            catch (Exception ex)
            {
                Cursor = Cursors.WaitCursor;
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtStudentSurname_TextChanged(object sender, EventArgs e)
        {
            //Ignore if reading data from database
            if (getData == false)
            {
                if (searchedstudents == false)
                {
                    txtStudentNumber.Text = "";
                    txtStudentFirstNames.Text = "";
                }
                searchedstudents = false;
            }
        }

        //wait for 1 second after scanning student number before searching students
        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        private void TimerEventProcessor(object myObject, EventArgs myEventargs)
        {

            myTimer.Stop();
            myTimer.Enabled = false;
            myTimer.Dispose();            
            myTimer.Tick -= new EventHandler(TimerEventProcessor);
            txtStudentNumberChanged = false;

            searchStudent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //don't start timer if reading data from database
            if (getData == false)
            {
                if (textBox1.Text.Length >= 9)
                {

                    if (txtStudentNumberChanged == false)
                    {
                        myTimer.Enabled = true;
                        myTimer.Tick += new EventHandler(TimerEventProcessor);
                        // Sets the timer interval to 1 second.
                        myTimer.Interval = 1000;
                        myTimer.Start();
                        txtStudentNumberChanged = true;
                    }
                }
            }
        }

        private void txtStudentNumber_Enter(object sender, EventArgs e)
        {
            txtStudentNumber.Focus();
            txtStudentNumber.SelectAll();
        }

        private void btnExchangeRates_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            frmExchangeRates frm = new frmExchangeRates();
            frm.Show();
        }

        

        private void txtStudentSurname_Enter(object sender, EventArgs e)
        {
            txtStudentSurname.Focus();
            txtStudentSurname.SelectAll();
        }

       
        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Print();
        }
    }
}
