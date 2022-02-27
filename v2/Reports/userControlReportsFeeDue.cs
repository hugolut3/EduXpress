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
using System.Resources;
using System.Data.SqlClient;
using CM.Sms;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using System.Threading;
using System.Collections;
using System.Diagnostics;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.Printing.ExportHelpers;
using System.IO;
using DevExpress.Spreadsheet;
using DevExpress.XtraReports.UI;
using EduXpress.Functions;
using System.Drawing.Printing;

namespace EduXpress.Reports
{
    public partial class userControlReportsFeeDue : DevExpress.XtraEditors.XtraUserControl
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlReportsFeeDue).Assembly);

        public userControlReportsFeeDue()
        {
            InitializeComponent();
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}
        //Autocomplete Academic Year Full/Partial
        private void AutocompleteAcademicYear()
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
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SchoolYear) FROM AcademicYear", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbSchoolYearFull.Properties.Items.Clear();
                    cmbSchoolYearPartial.Properties.Items.Clear();
                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSchoolYearFull.Properties.Items.Add(drow[0].ToString());
                        cmbSchoolYearPartial.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSchoolYearFull.SelectedIndex = -1;
                    cmbSchoolYearPartial.SelectedIndex = -1;
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

        //Fill cmbcycle
        private void fillCycle()
        {
            comboCycleFull.Properties.Items.Clear();
            comboCyclePartial.Properties.Items.Clear();

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                comboCycleFull.Properties.Items.AddRange(new object[]
                {
                    LocRM.GetString("strMaternelle"),
                    LocRM.GetString("strPrimaire") ,
                    LocRM.GetString("strSecondOrientation"),
                    LocRM.GetString("strSecondHuma"),
                    LocRM.GetString("strTVETCollege")
                });

                comboCyclePartial.Properties.Items.AddRange(new object[]
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
                comboCycleFull.Properties.Items.AddRange(new object[]
                {
                    LocRM.GetString("strPrePrimary"),
                    LocRM.GetString("strPreparatory") ,
                    LocRM.GetString("strHighSchoolGET"),
                    LocRM.GetString("strHighSchoolFET"),
                    LocRM.GetString("strTVETCollege")
                });

                comboCyclePartial.Properties.Items.AddRange(new object[]
                {
                    LocRM.GetString("strPrePrimary"),
                    LocRM.GetString("strPreparatory") ,
                    LocRM.GetString("strHighSchoolGET"),
                    LocRM.GetString("strHighSchoolFET"),
                    LocRM.GetString("strTVETCollege")
                });
            }            
        }
        //Autocomplete Section Full/partial
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
                    cmbSectionFull.Properties.Items.Clear();
                    cmbSectionPartial.Properties.Items.Clear();
                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSectionFull.Properties.Items.Add(drow[0].ToString());
                        cmbSectionPartial.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSectionFull.SelectedIndex = -1;
                    cmbSectionPartial.SelectedIndex = -1;
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
        //Autocomplete Class Full
        private void AutocompleteClassFull()
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

                    if (cmbSectionFull.SelectedIndex != -1)
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1 and SectionName=@d2", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCycleFull.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 60, " SectionName").Value = cmbSectionFull.Text.Trim();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCycleFull.Text.Trim();
                    }
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    cmbClassFull.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbClassFull.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbClassFull.SelectedIndex = -1;
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
        //Autocomplete Class Partial
        private void AutocompleteClassPartial()
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

                    if (cmbSectionPartial.SelectedIndex != -1)
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1 and SectionName=@d2", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCyclePartial.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 60, " SectionName").Value = cmbSectionPartial.Text.Trim();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCyclePartial.Text.Trim();
                    }
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    cmbClassPartial.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbClassPartial.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbClassPartial.SelectedIndex = -1;
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
        //Fill Month Full
        private void AutocompleteMonthFull()
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
                    cmd = new SqlCommand("SELECT distinct RTRIM(Month),MonthNo FROM CourseFee where ClassName=@d1 order by MonthNo asc", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, "ClassName").Value = cmbClassFull.Text.Trim();

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    cmbMonthFull.Properties.Items.Clear();
                   
                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbMonthFull.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbMonthFull.SelectedIndex = -1;
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
        //Fill Month Partial
        private void AutocompleteMonthPartial()
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
                    cmd = new SqlCommand("SELECT distinct RTRIM(Month),MonthNo FROM CourseFee where ClassName=@d1 order by MonthNo asc", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, "ClassName").Value = cmbClassPartial.Text.Trim();

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    cmbMonthPartial.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbMonthPartial.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbMonthPartial.SelectedIndex = -1;
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

        private void userControlReportsFeeDue_Load(object sender, EventArgs e)
        {
            AutocompleteAcademicYear();
          //  gridControlFeeDueReport.DataSource = CreateData();
        }

        private void cmbSchoolYearFull_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboCycleFull.SelectedIndex = -1;
            cmbSectionFull.SelectedIndex = -1;
            cmbClassFull.SelectedIndex = -1;
            cmbMonthFull.SelectedIndex = -1;
            fillCycle();
            comboCycleFull.Enabled = true;
            clearlPartial();
        }
        private void clearlPartial()
        {
            cmbSchoolYearPartial.SelectedIndex = -1;
            comboCyclePartial.SelectedIndex = -1;
            comboCyclePartial.Enabled = false;
            cmbClassPartial.SelectedIndex = -1;
            cmbClassPartial.Enabled = false;
            cmbSectionPartial.SelectedIndex = -1;
            cmbSectionPartial.Enabled = false;
            cmbClassPartial.SelectedIndex = -1;
            cmbClassPartial.Enabled = false;
            cmbMonthPartial.SelectedIndex = -1;
            cmbMonthPartial.Enabled = false;
        }
        private void clearlFull()
        {
            cmbSchoolYearFull.SelectedIndex = -1;
            comboCycleFull.SelectedIndex = -1;
            comboCycleFull.Enabled = false;
            cmbClassFull.SelectedIndex = -1;
            cmbClassFull.Enabled = false;
            cmbSectionFull.SelectedIndex = -1;
            cmbSectionFull.Enabled = false;
            cmbClassFull.SelectedIndex = -1;
            cmbClassFull.Enabled = false;
            cmbMonthFull.SelectedIndex = -1;
            cmbMonthFull.Enabled = false;
        }
        private void comboCycleFull_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSectionFull.SelectedIndex = -1;
            cmbClassFull.SelectedIndex = -1;
            cmbMonthFull.SelectedIndex = -1;

            cmbSectionFull.Enabled = false;
            cmbClassFull.Enabled = false;
            cmbMonthFull.Enabled = false;

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (comboCycleFull.SelectedIndex >= 3)
                {
                    cmbSectionFull.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClassFull.Enabled = true;
                    AutocompleteClassFull();
                    cmbSectionFull.Enabled = false;
                    cmbSectionFull.Properties.Items.Clear();
                }
            }
            else
            {
                if (comboCycleFull.SelectedIndex >= 4)
                {
                    cmbSectionFull.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClassFull.Enabled = true;
                    AutocompleteClassFull();
                    cmbSectionFull.Enabled = false;
                    cmbSectionFull.Properties.Items.Clear();
                }
            }
        }

        private void cmbSectionFull_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbClassFull.SelectedIndex = -1;
            cmbMonthFull.SelectedIndex = -1;
            cmbClassFull.Enabled = true;
            AutocompleteClassFull();           
            cmbMonthFull.Enabled = false;
        }

        private void cmbClassFull_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonthFull.SelectedIndex = -1;
            AutocompleteMonthFull();
            cmbMonthFull.Enabled = true;
        }
        //GridViewFeeDueReport columns
        private DataTable CreateData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));           
            dt.Columns.Add(LocRM.GetString("strAmountOwed").ToUpper(), typeof(decimal));
            dt.Columns.Add(LocRM.GetString("strNotificationPhoneNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNotificationEmailAddress").ToUpper(), typeof(string));
            // dt.Columns.Add(LocRM.GetString("strFeeDue").ToUpper(), typeof(string));
            return dt;
        }
        private void btnSearchFullDueList_Click(object sender, EventArgs e)
        {
            if (cmbClassFull.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClassFull.Focus();
                return;
            }
            if (cmbMonthFull.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMonthFull.Focus();
                return;
            }

            try
            {
                //clear gridcontrol
                gridControlFeeDueReport.DataSource = null;
                //load culumns in gridControlFeeInfo
                gridControlFeeDueReport.DataSource = CreateData();


                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    // cmd = New SqlCommand("SELECT RTRIM(Student.AdmissionNo),RTRIM(GRNo),RTRIM(StudentName),RTRIM(SchoolName),Sum(CourseFee.Fee) from Student,SchoolInfo,CourseFee,Class where Student.SchoolID=SchoolInfo.S_ID and Class.Classname=CourseFee.Class and CourseFee.SchoolID=SchoolInfo.S_ID and Student.Session=@d1 and CourseFee.Class=@d2 and CourseFee.Month=@d3 group by Student.AdmissionNo,GRNo,StudentName,SchoolName Except SELECT RTRIM(Student.AdmissionNo),RTRIM(GRNo),RTRIM(StudentName),RTRIM(SchoolName),Sum(CourseFee.Fee) from Student,SchoolInfo,CourseFee,CourseFeePayment,Class,CourseFeePayment_Join where CourseFeePayment.CFP_ID=CourseFeePayment_Join.C_PaymentID and CourseFee.SchoolID=SchoolInfo.S_ID and Class.Classname=CourseFee.Class and Student.SchoolID=SchoolInfo.S_ID and CourseFeePayment.Student_Class=CourseFee.Class and Student.AdmissionNo=CourseFeePayment.AdmissionNo and CourseFee.Month=CourseFeePayment_Join.Month and CourseFeePayment.Session=@d1 and CourseFee.Class=@d2 and CourseFee.Month=@d3 group by Student.AdmissionNo,GRNo,StudentName,SchoolName order by 3", con);

                    //string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(Students.StudentSurname),RTRIM(Students.StudentFirstNames),Sum(distinct CourseFee.Fee) from Students,CourseFee,Classes where Students.Class= Classes.ClassName and Classes.ClassName=CourseFee.ClassName and Students.SchoolYear=@d1 and CourseFee.ClassName=@d2 and CourseFee.Month=@d3 group by Students.StudentNumber,Students.StudentSurname,Students.StudentFirstNames Except SELECT RTRIM(Students.StudentNumber),RTRIM(Students.StudentSurname),RTRIM(Students.StudentFirstNames),Sum(distinct CourseFee.Fee) from Students,CourseFee,CourseFeePayment,Classes,CourseFeePayment_Join where CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and Students.Class= Classes.ClassName and Classes.ClassName=CourseFee.ClassName and CourseFeePayment.Student_Class=CourseFee.ClassName and Students.StudentNumber=CourseFeePayment.StudentNumber and CourseFee.Month=CourseFeePayment_Join.Month and CourseFeePayment.SchoolYear=@d1 and CourseFee.ClassName=@d2 and CourseFee.Month=@d3  group by Students.StudentNumber,Students.StudentSurname,Students.StudentFirstNames order by 2";
                    string ct = "SELECT RTRIM(Students.StudentNumber),RTRIM(Students.StudentSurname),RTRIM(Students.StudentFirstNames), RTRIM(Students.NotificationNo),RTRIM(Students.NotificationEmail), Sum(distinct CourseFee.Fee) from Students,CourseFee,Classes where Students.Class= Classes.ClassName and Classes.ClassName=CourseFee.ClassName and Students.SchoolYear=@d1 and CourseFee.ClassName=@d2 and CourseFee.Month=@d3 group by Students.StudentNumber,Students.StudentSurname,Students.StudentFirstNames, Students.NotificationNo,Students.NotificationEmail Except SELECT RTRIM(Students.StudentNumber),RTRIM(Students.StudentSurname),RTRIM(Students.StudentFirstNames),RTRIM(Students.NotificationNo),RTRIM(Students.NotificationEmail),Sum(distinct CourseFee.Fee) from Students,CourseFee,CourseFeePayment,Classes,CourseFeePayment_Join where CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID and Students.Class= Classes.ClassName and Classes.ClassName=CourseFee.ClassName and CourseFeePayment.Student_Class=CourseFee.ClassName and Students.StudentNumber=CourseFeePayment.StudentNumber and CourseFee.Month=CourseFeePayment_Join.Month and CourseFeePayment.SchoolYear=@d1 and CourseFee.ClassName=@d2 and CourseFee.Month=@d3  group by Students.StudentNumber,Students.StudentSurname,Students.StudentFirstNames, Students.NotificationNo,Students.NotificationEmail order by 2"; //Add notification no and email


                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYearFull.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbClassFull.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", cmbMonthFull.Text.Trim()); 
                    
                    rdr = cmd.ExecuteReader();                    

                    decimal feeDue;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);                       

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        // gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[1].ToString());
                        // gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strName").ToUpper()], rdr[2].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper()); //merge surname and name
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNotificationPhoneNo").ToUpper()], rdr[3].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNotificationEmailAddress").ToUpper()], rdr[4].ToString());

                        feeDue = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                        //string sFeeDue = feeDue.ToString("0.00");
                        string sFeeDue = feeDue.ToString();
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()], sFeeDue);                                               

                        // gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()],(Convert.ToDecimal( rdr[3])));                        
                        //gridViewFee.Columns[LocRM.GetString("strFee").ToUpper()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    }
                    con.Close();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                CalculateCount();
                //Set School details
                PublicVariables.SchoolYear = cmbSchoolYearFull.Text;
                PublicVariables.SchoolClass = cmbClassFull.Text;
                PublicVariables.FeePeriod = cmbMonthFull.Text;
                PublicVariables.FeesDueFull = true;
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
       // SchoolFeeReport schoolFeeReport = new SchoolFeeReport();
        //check if it was full school fees or partial       
        // static string schoolYear = "";
        //static string schoolClass = "";
        // static string schoolPeriod = "";
        private void cmbSchoolYearPartial_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboCyclePartial.SelectedIndex = -1;
            cmbSectionPartial.SelectedIndex = -1;
            cmbClassPartial.SelectedIndex = -1;
            cmbMonthPartial.SelectedIndex = -1;
            fillCycle();
            comboCyclePartial.Enabled = true;
            clearlFull();
        }
        private void CalculateCount()
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    //Display sum of total due
                    gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                    // gridView1.Columns[LocRM.GetString("strFeeDue").ToUpper()].SummaryItem.FieldName = "id";
                    //gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()].SummaryItem.FieldName = gridView1.Columns[3].ToString();
                    gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()].SummaryItem.FieldName = gridView1.Columns[2].ToString();
                    gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strTotalAmountOwed").ToUpper() + ": {0:n2}";

                    //display number of students owing
                    gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;
                   // gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].SummaryItem.FieldName = gridView1.Columns[3].ToString();
                    gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].SummaryItem.FieldName = gridView1.Columns[2].ToString();
                    gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strNumberStudents").ToUpper() + ": {0}";

                    //refresh summary
                    gridView1.UpdateSummary();
                    //textedit1.text = gridView1.Columns["Salary"].SummaryItem.SummaryValue.ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Reset()
        {
            clearlFull();
            clearlPartial();
            //clear gridcontrol
            gridControlFeeDueReport.DataSource = null;
        }
        private void userControlReportsFeeDue_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                Reset();
            }
        }

        private void comboCyclePartial_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSectionPartial.SelectedIndex = -1;
            cmbClassPartial.SelectedIndex = -1;
            cmbMonthPartial.SelectedIndex = -1;

            cmbSectionPartial.Enabled = false;
            cmbClassPartial.Enabled = false;
            cmbMonthPartial.Enabled = false;

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (comboCyclePartial.SelectedIndex >= 3)
                {
                    cmbSectionPartial.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClassPartial.Enabled = true;
                    AutocompleteClassPartial();
                    cmbSectionPartial.Enabled = false;
                    cmbSectionPartial.Properties.Items.Clear();
                }
            }
            else
            {
                if (comboCyclePartial.SelectedIndex == 4)
                {
                    cmbSectionPartial.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClassPartial.Enabled = true;
                    AutocompleteClassPartial();
                    cmbSectionPartial.Enabled = false;
                    cmbSectionPartial.Properties.Items.Clear();
                }

            }
            
        }

        private void cmbSectionPartial_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbClassPartial.SelectedIndex = -1;
            cmbMonthPartial.SelectedIndex = -1;
            cmbClassPartial.Enabled = true;
            AutocompleteClassPartial();            
            cmbMonthPartial.Enabled = false;
        }

        private void cmbClassPartial_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonthPartial.SelectedIndex = -1;
            AutocompleteMonthPartial();
            cmbMonthPartial.Enabled = true;
        }

        private void btnSearchPartialDueList_Click(object sender, EventArgs e)
        {
            if (cmbClassPartial.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClassPartial.Focus();
                return;
            }
            if (cmbMonthPartial.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMonthPartial.Focus();
                return;
            }

            try
            {
                ////*****************this code to be optimized*********** check totalFeePaid first***************************//

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }
                //declare array to hold total paid fees per this month per student
                string[] totalPaidFees;
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    cmd = new SqlCommand("SELECT SUM( distinct CourseFeePayment.TotalPaid),StudentNumber from CourseFeePayment INNER JOIN CourseFeePayment_Join on CourseFeePayment.CourseFeePaymentID=CourseFeePayment_Join.C_PaymentID where Month=@d3 and CourseFeePayment.SchoolYear=@d1 and CourseFeePayment.Student_Class=@d2 group by StudentNumber,Month", con);
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYearPartial.Text);
                    cmd.Parameters.AddWithValue("@d2", cmbClassPartial.Text);
                    cmd.Parameters.AddWithValue("@d3", cmbMonthPartial.Text);

                    DataSet ds = new DataSet();
                    DataTable dtable;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "CourseFeePayment");
                    dtable = ds.Tables[0];

                    ArrayList sTotalPaidFees = new ArrayList(ds.Tables[0].Rows.Count);
                    foreach (DataRow dRow in dtable.Rows)
                    {
                        sTotalPaidFees.Add(dRow[0].ToString());

                    }
                    totalPaidFees = (string[])sTotalPaidFees.ToArray(typeof(string));
                    con.Close();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                ////**********************************************************************************************************

                //clear gridcontrol
                gridControlFeeDueReport.DataSource = null;
                //load culumns in gridControlFeeInfo
                gridControlFeeDueReport.DataSource = CreateData();

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSearching"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    //string ct = "SELECT distinct RTRIM(Students.StudentNumber),RTRIM(Students.StudentSurname),RTRIM(Students.StudentFirstNames),RTRIM(TotalFee) from Students, CourseFeePayment, CourseFeePayment_Join where Students.StudentNumber = CourseFeePayment.StudentNumber and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and CourseFeePayment.SchoolYear = @d1 and CourseFeePayment.Student_Class = @d2  and Month = @d3 and PaymentDue > 0  order by 2";
                    string ct = "SELECT distinct RTRIM(Students.StudentNumber),RTRIM(Students.StudentSurname),RTRIM(Students.StudentFirstNames),RTRIM(Students.NotificationNo),RTRIM(Students.NotificationEmail),RTRIM(TotalFee) from Students, CourseFeePayment, CourseFeePayment_Join where Students.StudentNumber = CourseFeePayment.StudentNumber and CourseFeePayment.CourseFeePaymentID = CourseFeePayment_Join.C_PaymentID and CourseFeePayment.SchoolYear = @d1 and CourseFeePayment.Student_Class = @d2  and Month = @d3 and PaymentDue > 0  order by 2"; // add notification no and email
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYearPartial.Text);
                    cmd.Parameters.AddWithValue("@d2", cmbClassPartial.Text);
                    cmd.Parameters.AddWithValue("@d3", cmbMonthPartial.Text);
                    rdr = cmd.ExecuteReader();

                    int rowNumber = 0;
                    decimal totalFee, feeDue;
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        //gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strSurname").ToUpper()], rdr[1].ToString());
                        //gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strName").ToUpper()], rdr[2].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNotificationPhoneNo").ToUpper()], rdr[3].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNotificationEmailAddress").ToUpper()], rdr[4].ToString());

                        totalFee = decimal.Parse(rdr[5].ToString(), CultureInfo.InvariantCulture);
                        //feeDue = decimal.Parse(totalPaidFees[rowNumber], CultureInfo.InvariantCulture);
                        feeDue = totalFee - decimal.Parse(totalPaidFees[rowNumber]);
                        string sFeeDue = feeDue.ToString("0.00");
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strAmountOwed").ToUpper()], sFeeDue);
                        //gridViewFee.Columns[LocRM.GetString("strFee").ToUpper()].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        rowNumber++;
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
                gridControlFeeDueReport.RefreshDataSource();
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                CalculateCount();
                //Set School details
                PublicVariables.SchoolYear = cmbSchoolYearPartial.Text;
                PublicVariables.SchoolClass = cmbClassPartial.Text;
                PublicVariables.FeePeriod = cmbMonthPartial.Text;
                PublicVariables.FeesDueFull = false;
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
            clearlPartial();
            clearlFull();
            //clear gridcontrol
            gridControlFeeDueReport.DataSource = null;
        }

        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                    gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;//true
                   // gridView1.OptionsView.ColumnAutoWidth = false;//false
                    
                    // Create a new report instance.
                    reportFeeDue report = new reportFeeDue();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlFeeDueReport;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    
                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = saveFileDialog.FileName + ".pdf";
                        //Export to pdf
                        report.ExportToPdf(fileName);
                       // gridView1.OptionsView.ColumnAutoWidth = true;
                        if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strReportUnpaidFees"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
        }

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                // Create a new report instance.
              reportFeeDue  report = new reportFeeDue();

                // Link the required control with the PrintableComponentContainers of a report.
                report.printableComponentContainer1.PrintableComponent = gridControlFeeDueReport;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                report.Landscape = true;

                // Invoke a Print Preview for the created report document. 
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();
                //printTool.ShowPreview();
                #region PrintPreview
                //// Invoke a Print Preview for the created report document. 
                //ReportPrintTool preview = new ReportPrintTool(report);
                //preview.ShowRibbonPreview();
                ////   ShowGridPreview(gridControlFeeDueReport); 
                #endregion


            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPreview"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

           
        }
        //Print Preview datagridview
        //private void ShowGridPreview(GridControl grid)
        //{
        //    // Check whether the GridControl can be previewed.
        //    if (!grid.IsPrintingAvailable)
        //    {
        //        XtraMessageBox.Show(LocRM.GetString("strPrintErrorLibraryNotFound"), LocRM.GetString("strPrintError"));
        //        return;
        //    }

        //    try
        //    {
        //        // Open the Preview window.
        //        grid.UseWaitCursor = true;
        //        gridView1.BestFitColumns();
        //        gridView1.OptionsPrint.AllowMultilineHeaders = true;
        //        gridView1.OptionsPrint.AutoWidth = false;
        //        grid.ShowRibbonPrintPreview();
        //        // grid.ShowPrintPreview();
        //        grid.UseWaitCursor = false;
        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.ToString());
        //    }
        //}

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
                    reportFeeDue report = new reportFeeDue();
                    PrinterSettings printerSettings = new PrinterSettings();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlFeeDueReport;
                   
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    report.Landscape = true;

                    // Specify the PrinterName if the target printer is not the default one.
                    printerSettings.PrinterName = Properties.Settings.Default.ReportPrinter;
                    // Assign minimum margins supported for the selected printer to the report document.
                    //report.Margins = PageSettingsHelper.GetMinMargins(printerSettings.DefaultPageSettings);

                    // Invoke a Print Preview for the created report document. 
                    ReportPrintTool printTool = new ReportPrintTool(report);
                    // preview.ShowRibbonPreview();
                    printTool.Print(Properties.Settings.Default.ReportPrinter);                    

                    #region Print
                    //PrintingSystem printingSystem = new PrintingSystem();
                    //PrintableComponentLink link = new PrintableComponentLink();
                    //link.Component = gridControlFeeDueReport;
                    //printingSystem.Links.Add(link);
                    //link.Print(Properties.Settings.Default.ReportPrinter);
                    #endregion


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
        }

        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            (e.PrintingSystem as PrintingSystemBase).PageSettings.Landscape = true;

            //insert header image and text to pdf
            #region InsertHeaderImageTextPDF
            //CreateAreaEventHandler handler = null;
            //handler = (s, ea) => {
            //    Image image = logoFile;
            //    PageImageBrick brick = ea.Graph.DrawPageImage(image, new RectangleF(Point.Empty, image.Size), BorderSide.None, Color.Empty);
            //    brick.LineAlignment = BrickAlignment.Center;
            //    brick.Alignment = BrickAlignment.Center;
            //    //brick.AutoWidth = true;

            //    //add text
            //    PageInfoBrick brick2 = ea.Graph.DrawPageInfo(PageInfo.DateTime, "", Color.DarkBlue, new RectangleF(0, 0, 100, 20), BorderSide.None);
            //    brick2.LineAlignment = BrickAlignment.Center;
            //    brick2.Alignment = BrickAlignment.Center;
            //    brick2.AutoWidth = true;

            //    e.Link.CreateMarginalHeaderArea -= handler;
            //};

            //e.Link.CreateMarginalHeaderArea += handler; 
            #endregion
        }


        private void btnExportToExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                    gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;

                    #region ExportExcelWithCustomHeader
                    //Export options Header/footer***********************
                    // Ensure that the data-aware export mode is enabled.
                    // ExportSettings.DefaultExportType = ExportType.DataAware;
                    // Create a new object defining how a document is exported to the XLSX format.
                    // var options = new XlsxExportOptionsEx();
                    // Specify a name of the sheet in the created XLSX file.
                    // options.SheetName = LocRM.GetString("strReportUnpaidFees");

                    // Subscribe to export customization events.
                    // options.CustomizeSheetSettings += Options_CustomizeSheetSettings;
                    // options.CustomizeSheetHeader += Options_CustomizeSheetHeader;
                    // options.CustomizeCell += Options_CustomizeCell;
                    // options.CustomizeSheetFooter += Options_CustomizeSheetFooter;
                    // options.AfterAddRow += Options_AfterAddRow;
                    //************************************************ 
                    #endregion

                    // Create a new report instance.
                    reportFeeDue report = new reportFeeDue();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlFeeDueReport;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    
                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Rename worksheet
                        report.CreateDocument(false);
                       // report.PrintingSystem.XlSheetCreated += PrintingSystem_XlSheetCreated;

                        string fileName = saveFileDialog.FileName + ".xlsx";
                        //Export to excel
                        report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = LocRM.GetString("strReportUnpaidFees") });
                        
                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportUnpaidFees"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                
        }

        //private void PrintingSystem_XlSheetCreated(object sender, XlSheetCreatedEventArgs e)
        //{
        //    e.SheetName = LocRM.GetString("strReportUnpaidFees");
        //}

        //Customize Header/Footer Export Excel
        #region CustomizeHeaderFooterExportExcel
        private void Options_AfterAddRow(AfterAddRowEventArgs e)
        {
            // Merge cells in rows that correspond to the grid's group rows.
            if (e.DataSourceRowIndex < 0)
            {
                e.ExportContext.MergeCells(new XlCellRange(new XlCellPosition(0, e.DocumentRow - 1), new XlCellPosition(3, e.DocumentRow - 1)));
            }
        }


        //Customize Sheet Footer Event
        private void Options_CustomizeSheetFooter(ContextEventArgs e)
        {
            //// Add an empty row to the document's footer.
            //e.ExportContext.AddRow();

            //// Create a new row.
            //var firstRow = new CellObject();
            //// Specify row values.
            //// firstRow.Value = Properties.Settings.Default.BusinessName;// @"The report is generated from the NorthWind database.";
            //firstRow.Value = schoolName;
            // // Specify the cell content alignment and font settings.
            // var rowFormatting = CreateXlFormattingObject(true, 18);
            //rowFormatting.Alignment.HorizontalAlignment = XlHorizontalAlignment.Left;
            //firstRow.Formatting = rowFormatting;
            //// Add the created row to the output document. 
            //e.ExportContext.AddRow(new[] { firstRow });


            //// Create one more row.
            //var secondRow = new CellObject();
            //// Specify the row value. 
            //// DateTime currentdateTime = Convert.ToDateTime(DateTime.Now.ToString(CultureInfo.CurrentCulture));
            //DateTime currentdateTime = Convert.ToDateTime(DateTime.Now.ToString(CultureInfo.CurrentCulture.DateTimeFormat.FullDateTimePattern, CultureInfo.CurrentCulture));
            //secondRow.Value = LocRM.GetString("strReportGeneratedDateTime")  +": "+ DateTime.Now;
            //// Change the row's font settings.
            //rowFormatting.Font.Size = 14;
            //rowFormatting.Font.Bold = false;
            //rowFormatting.Font.Italic = true;
            //secondRow.Formatting = rowFormatting;
            //// Add this row to the output document.
            //e.ExportContext.AddRow(new[] { secondRow });
        }
        // delegate void AddCells(ContextEventArgs e, XlFormattingObject formatFirstCell, XlFormattingObject formatSecondCell);
        //Dictionary<int, AddCells> methods = CreateMethodSet();
        //static Dictionary<int, AddCells> CreateMethodSet()
        //{
        //  var dictionary = new Dictionary<int, AddCells>();
        //  dictionary.Add(1, AddSchoolYearRow);
        //  dictionary.Add(2, AddSchoolClassRow);
        //  dictionary.Add(3, AddSchoolPeriodRow);         
        //  return dictionary;
        // }
        static void AddSchoolYearRow(ContextEventArgs e, XlFormattingObject formatFirstCell,
            XlFormattingObject formatSecondCell)
        {
            // ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlReportsFeeDue).Assembly);

            // var SchoolYearCellName = CreateCell(LocRM.GetString("strSchoolYear") + ": ", formatFirstCell);
            //  var SchoolYearCellLocation = CreateCell(schoolYear, formatSecondCell);
            //  e.ExportContext.AddRow(new[] { SchoolYearCellName, null, SchoolYearCellLocation });
        }
        static void AddSchoolClassRow(ContextEventArgs e, XlFormattingObject formatFirstCell,
            XlFormattingObject formatSecondCell)
        {
            // ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlReportsFeeDue).Assembly);

            // var SchoolClassCellName = CreateCell(LocRM.GetString("strClass")  + ": ", formatFirstCell);
            //  var SchoolClassCellLocation = CreateCell(schoolClass, formatSecondCell);
            //  e.ExportContext.AddRow(new[] { SchoolClassCellName, null, SchoolClassCellLocation });
        }
        static void AddSchoolPeriodRow(ContextEventArgs e, XlFormattingObject formatFirstCell,
            XlFormattingObject formatSecondCell)
        {
            //  ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlReportsFeeDue).Assembly);

            //  var SchoolPeriodName = CreateCell(LocRM.GetString("strPeriod") + ": ", formatFirstCell);
            //  var SchoolPeriodLocation = CreateCell(schoolPeriod, formatSecondCell);
            //  e.ExportContext.AddRow(new[] { SchoolPeriodName, null, SchoolPeriodLocation });
        }
        // Create a new cell with a specified value and format settings.
        static CellObject CreateCell(object value, XlFormattingObject formatCell)
        {
            return new CellObject { Value = value, Formatting = formatCell };
        }

        private void Options_CustomizeSheetHeader(ContextEventArgs e)
        {
            //Display School info
            //Image logoFile=null;         

            //if (logoFile != null)
            //{
            //    var imageToHeader = logoFile;;
            //    e.ExportContext.InsertImage(imageToHeader, imageToHeader.Size);
            //}

            //// Specify cell formatting. 
            //var formatFirstCell = CreateXlFormattingObject(true, 18);
            //var formatSecondCell = CreateXlFormattingObject(true, 18);
            // Add new rows displaying custom information. 
            // for (var i = 0; i < 5; i++)
            // {
            //     AddCells addCellMethod;
            //     if (methods.TryGetValue(i, out addCellMethod))
            //         addCellMethod(e, formatFirstCell, formatSecondCell);
            //     else e.ExportContext.AddRow();
            // }
            // Merge specific cells.
            // MergeCells(e);            
        }

        //Customize Sheet Settings Event
        private void Options_CustomizeSheetSettings(CustomizeSheetEventArgs e)
        {
            //// Anchor the output document's header to the top and set its fixed height
            //const int lastHeaderRowIndex = 6;
            //e.ExportContext.SetFixedHeader(lastHeaderRowIndex);
            //// Add the AutoFilter button to the document's cells corresponding to the grid column headers.
            //e.ExportContext.AddAutoFilter(new XlCellRange(new XlCellPosition(0, lastHeaderRowIndex), new XlCellPosition(3, 100))); 
        }
        // Merge specific cells.
        /*
        * left	Int32﻿: An integer that is the zero-based index of the left column.
        * top	Int32: An integer that is the zero-based index of the top row.
        * right	Int32﻿: An integer that is the zero-based index of the right column.
        * bottom	Int32﻿: An integer that is the zero-based index of the bottom row
        */
        static void MergeCells(ContextEventArgs e)
        {//               C  R  C  R
            //MergeCells(e, 0, 0, 3, 0); //logo
            //MergeCells(e, 0, 1, 3, 1);  // First blank row
            //MergeCells(e, 0, 2, 1, 2);  // SchoolYear Name
            //MergeCells(e, 2, 2, 3, 2);  // SchoolYear value
            //MergeCells(e, 2, 3, 3, 3);   //Period Value  Class Value
            //MergeCells(e, 0, 3, 1, 3);   //Period Name  Class Name
            //MergeCells(e, 2, 4, 3, 4);   //Period Value  
            //MergeCells(e, 0, 4, 1, 4);   //Period Name 
            //MergeCells(e, 0, 5, 3, 5);   //Last blank header row
        }
        static void MergeCells(ContextEventArgs e, int left, int top, int right, int bottom)
        {
            // e.ExportContext.MergeCells(new XlCellRange(new XlCellPosition(left, top), new XlCellPosition(right, bottom)));
        }
        // Specify a cell's alignment and font settings. 
        static XlFormattingObject CreateXlFormattingObject(bool bold, double size)
        {
            var cellFormat = new XlFormattingObject
            {
                Font = new XlCellFont
                {
                    Bold = bold,
                    Size = size
                },
                Alignment = new XlCellAlignment
                {
                    RelativeIndent = 10,
                    HorizontalAlignment = XlHorizontalAlignment.Center,
                    VerticalAlignment = XlVerticalAlignment.Center
                }
            };
            return cellFormat;
        } 
        #endregion
        // Check whether the current row contains "0,00" in the FeeDue Column so we can hide it
        private void gridView1_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            string FeeDueColumn = LocRM.GetString("strAmountOwed").ToUpper();
            string FeeDue = view.GetListSourceRowCellValue(e.ListSourceRow, FeeDueColumn).ToString();
            // Check whether the current row contains "0,00" in the FeeDue Column. 
            string zeroFeeDue = "";
            if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
            {
                zeroFeeDue = "0,00";
            }
            else
            {
                zeroFeeDue = "0.00";
            }

            if (FeeDue == zeroFeeDue)
            {
                // Make the current row invisible. 
                e.Visible = false;
                // Prevent default processing, so the row will be invisible  
                // regardless of the view's filter. 
                e.Handled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            getPhoneNumbers();
        }
        private void getPhoneNumbers()
        {
            if (gridView1.DataRowCount > 0)
            {
                string phoneNumbers = "";
                string phoneNo = "";
                try
                {
                    for (int i = 0; i < gridView1.DataRowCount; ++i)
                    {
                        DataRow row = gridView1.GetDataRow(i);
                        if (gridView1.DataRowCount > 0)
                        {
                            phoneNo = row[LocRM.GetString("strNotificationPhoneNo").ToUpper()].ToString();
                            if (phoneNo != "")
                            {
                                phoneNumbers = phoneNumbers + phoneNo + ",";
                            }                               
                        }
                    }

                    if (phoneNumbers != "")
                    {
                        PublicVariables.NotificationNo = phoneNumbers.Remove(phoneNumbers.Length - 1, 1); //Remove the last ","
                                                                                                          //open send SMS form
                        frmReportSMS frm = new frmReportSMS();
                        frm.ShowDialog();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoNotificationNo"), LocRM.GetString("strReportUnpaidFees"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbSchoolYearFull.Focus();
                        return;
                    }

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strGenerateReportFirst"), LocRM.GetString("strReportUnpaidFees"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSearchFullDueList.Focus();
                return;
            }
                
        }
    }
}
