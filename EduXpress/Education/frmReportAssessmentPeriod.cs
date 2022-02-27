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
using EduXpress.Functions;
using System.Resources;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using static EduXpress.Functions.PublicVariables;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;

namespace EduXpress.Education
{
    public partial class frmReportAssessmentPeriod : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmReportAssessmentPeriod).Assembly);

        public frmReportAssessmentPeriod()
        {
            InitializeComponent();
        }
        bool formLoaded = false;
        private void frmReportAssessmentPeriod_Load(object sender, EventArgs e)
        {
            reset();
            AutocompleteAcademicYear();
            getTeacherID();
            formLoaded = true;
        }
        //Get Teacher ID
        private void getTeacherID()
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
                    cmd = con.CreateCommand();

                    cmd.CommandText = "SELECT UserName, UserNameID, UserType  FROM Registration where UserName = '" + Functions.PublicVariables.userLogged + "'";
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        txtTeacherID.Text = rdr.GetValue(1).ToString();
                        txtTeacherUserType.Text = rdr.GetString(2);
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
                    cmbSchoolYear.Properties.Items.Clear();
                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSchoolYear.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSchoolYear.SelectedIndex = -1;
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
        private void clearControls()
        {
            gridLookUpSubject.Properties.DataSource = null; //clear all contents of gridLookUpSubject
            //clear gridcontrol
            gridControlReportAssessmentPeriod.DataSource = null;

            txtMaximaPeriode.Text = "0";
            txtMaximaExam.Text = "0";
            txtTot.Text = "0";
            txtTotGen.Text = "0";
            txtClass.Text = "";
            txtSubjectCode.Text = "";
            //txtTeacherID.Text = "";

            cmbAssessmentPeriod.SelectedIndex = -1;
            cmbAssessmentPeriod.Properties.Items.Clear();
        }
        private void cmbSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCycle.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            clearControls();

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            fillSection();
            cmbCycle.Enabled = true;
        }
        //Fill cmbSection
        private void fillSection()
        {
            cmbCycle.Properties.Items.Clear();
            cmbCycle.Properties.Items.AddRange(new object[]
            {
                LocRM.GetString("strMaternelle"),
                LocRM.GetString("strPrimaire") ,
                LocRM.GetString("strSecondOrientation"),
                LocRM.GetString("strSecondHuma"),
                LocRM.GetString("strTVETCollege")
            });
        }
        //Autocomplete Option
        private void AutocompleteOption()
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

                    if (cmbSection.SelectedIndex != -1)
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1 and SectionName=@d2", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = cmbCycle.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 60, " SectionName").Value = cmbSection.Text.Trim();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = cmbCycle.Text.Trim();
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

        private void cmbCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;

            clearControls();

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();


            if (cmbCycle.SelectedIndex >= 3)
            {
                cmbClass.Properties.Items.Clear();
                cmbSection.Enabled = true;
                AutocompleteOption();
            }
            else
            {
                cmbClass.Enabled = true;
                AutocompleteClass();
                cmbSection.Enabled = false;
                cmbSection.Properties.Items.Clear();
            }
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
            AutocompleteClass();
            cmbClass.Enabled = true;
        }
        private DataTable CreateDataBranchesPrimary()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(int));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str1eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str2eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strExam1").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotal1").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("str3eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str4eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strExam2").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotal2").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("str5eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str6eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strExam3").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotal3").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("strGrandTotal").ToUpper(), typeof(string));
            return dt;
        }
        private DataTable CreateDataBranchesSecondary()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(int));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str1eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str2eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strExam1").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotal1").ToUpper(), typeof(string));

            dt.Columns.Add(LocRM.GetString("str3eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str4eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strExam2").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTotal2").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strGrandTotal").ToUpper(), typeof(string));
            return dt;
        }


       
       
        private void hideShowColumns()
        {
            if (cmbCycle.SelectedIndex >= 2)
            {
                //display culumns with 2 semesters
                if (cmbAssessmentPeriod.SelectedIndex == 0)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = true;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;

                }
                else if (cmbAssessmentPeriod.SelectedIndex == 1)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = true;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;

                }
                else if (cmbAssessmentPeriod.SelectedIndex == 2)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = true;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;

                }
                else if (cmbAssessmentPeriod.SelectedIndex == 3)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = true;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;

                }
                else if (cmbAssessmentPeriod.SelectedIndex == 4)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = true;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;

                }
                else if (cmbAssessmentPeriod.SelectedIndex == 5)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = true;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;

                }
            }
            else
            {
                //display culumns with 3 trimesters
                if (cmbAssessmentPeriod.SelectedIndex == 0)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = true;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;
                    gridView1.Columns[12].Visible = false;
                    gridView1.Columns[13].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }
                else if (cmbAssessmentPeriod.SelectedIndex == 1)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = true;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;
                    gridView1.Columns[12].Visible = false;
                    gridView1.Columns[13].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }
                else if (cmbAssessmentPeriod.SelectedIndex == 2)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = true;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;
                    gridView1.Columns[12].Visible = false;
                    gridView1.Columns[13].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }
                else if (cmbAssessmentPeriod.SelectedIndex == 3)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = true;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;
                    gridView1.Columns[12].Visible = false;
                    gridView1.Columns[13].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }
                else if (cmbAssessmentPeriod.SelectedIndex == 4)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = true;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;
                    gridView1.Columns[12].Visible = false;
                    gridView1.Columns[13].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }
                else if (cmbAssessmentPeriod.SelectedIndex == 5)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = true;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;
                    gridView1.Columns[12].Visible = false;
                    gridView1.Columns[13].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }
                else if (cmbAssessmentPeriod.SelectedIndex == 6)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = true;
                    gridView1.Columns[12].Visible = false;
                    gridView1.Columns[13].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }
                else if (cmbAssessmentPeriod.SelectedIndex == 7)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;
                    gridView1.Columns[12].Visible = true;
                    gridView1.Columns[13].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }
                else if (cmbAssessmentPeriod.SelectedIndex == 8)
                {
                    //Show only the current assessment period
                    gridView1.Columns[3].Visible = false;
                    gridView1.Columns[4].Visible = false;
                    gridView1.Columns[5].Visible = false;
                    gridView1.Columns[6].Visible = false;
                    gridView1.Columns[7].Visible = false;
                    gridView1.Columns[8].Visible = false;
                    gridView1.Columns[9].Visible = false;
                    gridView1.Columns[10].Visible = false;
                    gridView1.Columns[11].Visible = false;
                    gridView1.Columns[12].Visible = false;
                    gridView1.Columns[13].Visible = true;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                }

            }
        }
        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
            gridLookUpSubject.Enabled = true;
            cmbAssessmentPeriod.Enabled = true;
            loadSubjects();
            gridLookUpSubject.ForceInitialize();
            gridLookUpEdit1View.BestFitColumns();
            loadAssessmentPeriod();
        }
        private void loadAssessmentPeriod()
        {
            if (cmbCycle.SelectedIndex >= 2)
            {
                //display culumns with 2 semesters
                cmbAssessmentPeriod.Properties.Items.Clear();
                cmbAssessmentPeriod.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("str1eP"),
                LocRM.GetString("str2eP") ,
                LocRM.GetString("strExam1"),
                LocRM.GetString("str3eP"),
                LocRM.GetString("str4eP") ,
                LocRM.GetString("strExam2")
                });
            }
            else
            {
                //display culumns with 3 trimesters
                cmbAssessmentPeriod.Properties.Items.Clear();
                cmbAssessmentPeriod.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("str1eP"),
                LocRM.GetString("str2eP") ,
                LocRM.GetString("strExam1"),
                LocRM.GetString("str3eP"),
                LocRM.GetString("str4eP") ,
                LocRM.GetString("strExam2"),
                LocRM.GetString("str5eP"),
                LocRM.GetString("str6eP") ,
                LocRM.GetString("strExam3")
                });
            }
        }
        private void loadSubjects()
        {
            //initialize gridlookupeditSubject
            gridLookUpSubject.Properties.DataSource = GetDataTableSubject();
            gridLookUpSubject.Properties.PopulateViewColumns();
            gridLookUpSubject.Properties.ValueMember = LocRM.GetString("strSubjectCode").ToUpper();
            gridLookUpSubject.Properties.DisplayMember = LocRM.GetString("strSubject").ToUpper();
            gridLookUpSubject.Properties.SearchMode = DevExpress.XtraEditors.Repository.GridLookUpSearchMode.AutoSuggest;
            gridLookUpSubject.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
        }
        public DataTable GetDataTableSubject()  //public  static DataTable GetDataTableSubject() 
        {
            //ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmAssignSubjectsClasses).Assembly);

            DataTable dtable = new DataTable();
            dtable.Columns.Add(LocRM.GetString("strSubjectCode").ToUpper(), typeof(string));
            dtable.Columns.Add(LocRM.GetString("strSubject").ToUpper(), typeof(string));
            dtable.Columns.Add(LocRM.GetString("strClassName").ToUpper(), typeof(string));
            dtable.Columns.Add(LocRM.GetString("strMaximaPeriod").ToUpper(), typeof(int));
            dtable.Columns.Add(LocRM.GetString("strMaximaExam").ToUpper(), typeof(int));

            SqlConnection con = null;
            SqlDataAdapter adp;
            DataSet ds;
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                    }

                    con.Open();

                    if (txtTeacherUserType.Text.Trim() == "Administrator") //Show all subjects for this class as admin
                    {
                        cmd = new SqlCommand("SELECT Subject.SubjectCode, Subject.SubjectName,SubjectAssignment.Class," +
                                                "MaximaPeriode,MaximaExam FROM Subject,SubjectAssignment where " +
                                                "Subject.SubjectCode= SubjectAssignment.SubjectCode and SubjectAssignment.Class=@d1 order by Subject.SubjectName", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Class").Value = cmbClass.Text.Trim();
                    }
                    else  //Show only subjects assigned to this teacher
                    {
                        cmd = new SqlCommand("SELECT Subject.SubjectCode, Subject.SubjectName,SubjectAssignment.Class," +
                                                "MaximaPeriode,MaximaExam,InstructorID FROM Subject,SubjectAssignment where " +
                                                "Subject.SubjectCode= SubjectAssignment.SubjectCode and SubjectAssignment.Class=@d1 and InstructorID=@d2 order by Subject.SubjectName", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Class").Value = cmbClass.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 60, " InstructorID").Value = txtTeacherID.Text.Trim();
                    }

                    adp = new SqlDataAdapter(cmd);
                    ds = new DataSet("ds");
                    adp.Fill(ds);

                    foreach (DataRow drow in ds.Tables[0].Rows)
                    {
                        DataRow row = dtable.NewRow();
                        row[LocRM.GetString("strSubjectCode").ToUpper()] = drow[0].ToString();
                        row[LocRM.GetString("strSubject").ToUpper()] = drow[1].ToString();
                        row[LocRM.GetString("strClassName").ToUpper()] = drow[2].ToString();
                        row[LocRM.GetString("strMaximaPeriod").ToUpper()] = drow[3].ToString();
                        row[LocRM.GetString("strMaximaExam").ToUpper()] = drow[4].ToString();
                        dtable.Rows.Add(row);
                    }
                    con.Close();
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
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

            dtable.AcceptChanges();
            return dtable;
        }

        private void gridLookUpSubject_EditValueChanged(object sender, EventArgs e)
        {
            GridLookUpEdit lookup = sender as GridLookUpEdit;
            DataRowView dataRowView = lookup.GetSelectedDataRow() as DataRowView;
            if (dataRowView != null)
            {
                DataRow row = dataRowView.Row;

                txtMaximaExam.Text = row[LocRM.GetString("strMaximaExam").ToUpper()].ToString();
                txtClass.Text = row[LocRM.GetString("strClassName").ToUpper()].ToString();
                txtSubjectCode.Text = row[LocRM.GetString("strSubjectCode").ToUpper()].ToString();
                txtMaximaPeriode.Text = row[LocRM.GetString("strMaximaPeriod").ToUpper()].ToString();
            }
        }

       

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            //Change Cell and Row Appearances Dynamically. Change font to Red when marks below 50%  
            if (e.Column.FieldName == LocRM.GetString("str1eP").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str1eP").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaPeriode.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else if (e.Column.FieldName == LocRM.GetString("str2eP").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str2eP").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaPeriode.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else if (e.Column.FieldName == LocRM.GetString("str3eP").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str3eP").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaPeriode.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else if (e.Column.FieldName == LocRM.GetString("str4eP").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str4eP").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaPeriode.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else if (e.Column.FieldName == LocRM.GetString("str5eP").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str5eP").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaPeriode.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else if (e.Column.FieldName == LocRM.GetString("str6eP").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str6eP").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaPeriode.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else if (e.Column.FieldName == LocRM.GetString("strExam1").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strExam1").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaExam.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else if (e.Column.FieldName == LocRM.GetString("strExam2").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strExam2").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaExam.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
            else if (e.Column.FieldName == LocRM.GetString("strExam3").ToUpper())
            {
                string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strExam3").ToUpper()]);
                int intMark = 0;
                bool canConvert = int.TryParse(stringMark, out intMark);
                if (canConvert == true)
                {
                    intMark = (intMark * 100) / Convert.ToInt16(txtMaximaExam.Text);

                    if (intMark >= 50)
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                }
            }
        }
        
        private void reset()
        {
            cmbSchoolYear.SelectedIndex = -1;
            //cmbSchoolYear.Enabled = false;

            cmbCycle.SelectedIndex = -1;
            cmbCycle.Enabled = false;
            cmbSection.SelectedIndex = -1;
            cmbSection.Enabled = false;
            cmbClass.SelectedIndex = -1;
            cmbClass.Enabled = false;
            gridLookUpSubject.Text = "";           

            clearControls(); 
        }
        private void frmReportAssessmentPeriod_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                if (formLoaded == true)
                {
                    formLoaded = false;
                }
                else
                {
                    reset();
                }
            }
            
        }
        int maximaSubject, numberStudents, totalPoints, passed = 0;
        private void btnLoadReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbSchoolYear.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSchoolYear.Focus();
                return;
            }

            if (cmbCycle.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectCycle"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCycle.Focus();
                return;
            }

            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            if (gridLookUpSubject.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSubject"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                gridLookUpSubject.Focus();
                return;
            }

            if (cmbAssessmentPeriod.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectAssessmentPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAssessmentPeriod.Focus();
                return;
            }
            maximaSubject = 0;
            numberStudents = 0;
            totalPoints = 0;
            passed = 0;

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

                    string ct = "SELECT Students.StudentNumber, Students.StudentSurname,Students.StudentFirstNames," +
                        "Students.SchoolYear,Students.Class, Cycle,Section,MarksEntry.SubjectCode,MarksEntry.AssessmentPeriod,MarksEntry.SubjectMaxima," +
                        "MarksObtained, Subject.SubjectName from Students,MarksEntry,Subject where " +
                        "Students.StudentNumber= MarksEntry.StudentNumber and MarksEntry.SubjectCode= Subject.SubjectCode " +
                        "and Students.Class= MarksEntry.Class and MarksEntry.SchoolYear= @d1 and MarksEntry.Class= @d2 " +
                        "and MarksEntry.SubjectCode= @d3 and MarksEntry.AssessmentPeriod= @d4 order by Students.StudentSurname";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbClass.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", txtSubjectCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@d4", cmbAssessmentPeriod.Text.Trim());

                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //CommandBehavior.CloseConnection usefull when you use a connection over and over again in a loop, the connection will remain "open" until the Garbage collector picks it up, and only then will it be released back to the ADO.net Connection pool to be re-used

                    //clear gridcontrol
                    gridControlReportAssessmentPeriod.DataSource = null;

                    //refresh gridview to rebuildnew columns
                    gridView1.Columns.Clear();
                    gridView1.PopulateColumns();

                    if (cmbCycle.SelectedIndex >= 2)
                    {
                        //display culumns with 2 semesters
                        gridControlReportAssessmentPeriod.DataSource = CreateDataBranchesSecondary();
                        gridView1.BestFitColumns();
                        gridView1.OptionsView.ColumnAutoWidth = true;
                        isSemesterGlobalVariable = true;
                    }
                    else
                    {
                        //display culumns with 3 trimesters
                        gridControlReportAssessmentPeriod.DataSource = CreateDataBranchesPrimary();
                        gridView1.BestFitColumns();
                        gridView1.OptionsView.ColumnAutoWidth = true;
                        isSemesterGlobalVariable = false;
                    }


                    while ((rdr.Read() == true))
                    {
                        //add new  row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                        if (rowHandle == 0)
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle).ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], "**********");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], LocRM.GetString("strMaxima").ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], rdr.GetValue(9).ToString());
                            maximaSubject = Convert.ToInt16(rdr.GetValue(9));
                            //add new  row
                            gridView1.AddNewRow();
                            gridView1.UpdateCurrentRow();
                            rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle).ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString().ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper()); //merge surname and name
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], rdr.GetValue(10).ToString());
                        }
                        else
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle).ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString().ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper()); //merge surname and name
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], rdr.GetValue(10).ToString());
                        }
                        numberStudents++;
                        totalPoints = totalPoints + Convert.ToInt16(rdr.GetValue(10));
                        if (((Convert.ToInt16(rdr.GetValue(10)) * 100) / maximaSubject) >= 50)
                        {
                            passed++;
                        }

                    }
                    maximaSubjectGlobalVariable = maximaSubject;
                    numberStudentsGlobalVariable = numberStudents;
                    totalPointsGlobalVariable = totalPoints;
                    SchoolYear = cmbSchoolYear.Text;
                    assessmentPeriodGlobalVariable = cmbAssessmentPeriod.Text;
                    subjectGlobalVariable = gridLookUpSubject.Text;
                    className = cmbClass.Text;
                    passedGlobalVariable = passed;

                    con.Close();
                }

                hideShowColumns();
                // CalculateCount();

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
            
            reset();
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {

                try
                {
                    // gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    // Create a new report instance.
                    reportAssessmentPeriod report = new reportAssessmentPeriod();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlReportAssessmentPeriod;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Rename worksheet
                        report.CreateDocument(false);

                        string fileName = saveFileDialog.FileName + ".xlsx";
                        //Export to excel
                        report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = LocRM.GetString("strAssessmentPeriodReport") });

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strAssessmentPeriodReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                reportAssessmentPeriod report = new reportAssessmentPeriod();

                // Link the required control with the PrintableComponentContainers of a report.
                report.printableComponentContainer1.PrintableComponent = gridControlReportAssessmentPeriod;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                report.Landscape = false;

                // Invoke a Print Preview for the created report document. 
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreview();
            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPreview"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    reportAssessmentPeriod report = new reportAssessmentPeriod();
                    PrinterSettings printerSettings = new PrinterSettings();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlReportAssessmentPeriod;

                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    report.Landscape = false;

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
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }

        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                
                try
                {
                    // gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    // Create a new report instance.
                    reportAssessmentPeriod  report = new reportAssessmentPeriod();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent =gridControlReportAssessmentPeriod;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        string fileName = saveFileDialog.FileName + ".pdf";
                        //Export to pdf
                        report.ExportToPdf(fileName);

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strAssessmentPeriodReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

        //row formating
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string studentNo = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strStudentNo").ToUpper()]);
                if (studentNo == "**********")
                {
                    //e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
            }
        }

        private void cmbAssessmentPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
                       
        }
    }
}