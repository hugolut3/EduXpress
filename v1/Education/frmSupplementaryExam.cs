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
using DevExpress.XtraGrid.Views.Grid;
using static EduXpress.Functions.PublicVariables;
using System.Diagnostics;
using EduXpress.Functions;
using System.Resources;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using DevExpress.XtraPrinting;

namespace EduXpress.Education
{
    public partial class frmSupplementaryExam : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmSupplementaryExam).Assembly);


        public frmSupplementaryExam()
        {
            InitializeComponent();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }
        bool formLoaded = false;
        private void frmSupplementaryExam_Load(object sender, EventArgs e)
        {
            reset();
            AutocompleteAcademicYear();
            getTeacherID();
            formLoaded = true;

            //load from settings the supplementary exam
            rangeControlPercentage.SelectedRange.Maximum = Properties.Settings.Default.SupplementaryExamMax;
            rangeControlPercentage.SelectedRange.Minimum = Properties.Settings.Default.SupplementaryExamMin;            
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
            gridControlSupplementaryExam.DataSource = null;

            txtMaximaPeriode.Text = "0";
            txtMaximaExam.Text = "0";
            txtTot.Text = "0";
            txtTotGen.Text = "0";
            txtClass.Text = "";
            txtSubjectCode.Text = "";
           

            //cmbAssessmentPeriod.SelectedIndex = -1;
            //cmbAssessmentPeriod.Properties.Items.Clear();
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
        private void frmSupplementaryExam_VisibleChanged(object sender, EventArgs e)
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
        bool isSemester = false;
        private void cmbCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;

            clearControls();

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            if (cmbCycle.SelectedIndex >= 2)
            {
                //2 semesters                
                isSemester = true;
            }
            else
            {
                //3 trimesters                
                isSemester = false;
            }


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
        
        private DataTable CreateDataBranches()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(int));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));           
            dt.Columns.Add(LocRM.GetString("strMaximaGrandTotal").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strGrandTotal").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strPercentage").ToUpper(), typeof(string));            
            return dt;
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
            gridLookUpSubject.Enabled = true;
           // cmbAssessmentPeriod.Enabled = true;
            loadSubjects();
            gridLookUpSubject.ForceInitialize();
            gridLookUpEdit1View.BestFitColumns();
        
            gridLookUpSubject.EditValue = null; //Clear gridLookUpEdit selection and avoid auto select previous item.
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

            if (Convert.ToInt16(rangeControlPercentage.SelectedRange.Maximum) == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strMaxPercentageRangeCannotBeZero"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                rangeControlPercentage.Focus();
                return;
            }          
            
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
                    string ct = "";
                    if (isSemester == true)
                    {
                    ct = "SELECT * FROM " +
                            "(" +
                       "SELECT Students.StudentNumber, Students.StudentSurname,Students.StudentFirstNames," +
                       "SUM (MarksEntry.SubjectMaxima) as Maxima, SUM (MarksObtained) as Marks," +
                       "SUM (MarksObtained *100.0)/SUM (MarksEntry.SubjectMaxima) as MarksPecentage " +
                       "from Students,MarksEntry " +
                       "where (" +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class= MarksEntry.Class and MarksEntry.SchoolYear= @Year1 and MarksEntry.Class= @Class1 " +
                       "and MarksEntry.SubjectCode= @SubjectCode1 and MarksEntry.AssessmentPeriod= @P1) " +
                       "OR " +
                       "(Students.StudentNumber= MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @Year2 and MarksEntry.Class = @Class2 " +
                       "and MarksEntry.SubjectCode = @SubjectCode2  and MarksEntry.AssessmentPeriod =  @P2) " +
                       "OR " +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @YearExam1 and MarksEntry.Class = @ClassExam1 " +
                       "and MarksEntry.SubjectCode = @SubjectCodeExam1 and MarksEntry.AssessmentPeriod = @Exam1) " +
                       "OR " +
                       "(Students.StudentNumber= MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @Year3 and MarksEntry.Class= @Class3 " +
                       "and MarksEntry.SubjectCode = @SubjectCode3  and MarksEntry.AssessmentPeriod =  @P3) " +
                       "OR " +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @Year4 and MarksEntry.Class= @Class4 " +
                       "and MarksEntry.SubjectCode = @SubjectCode4  and MarksEntry.AssessmentPeriod =  @P4) " +
                       "OR " +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @YearExam2 and MarksEntry.Class = @ClassExam2 " +
                       "and MarksEntry.SubjectCode = @SubjectCodeExam2 and MarksEntry.AssessmentPeriod = @Exam2) " +
                       ") " +
                       "group by Students.StudentNumber, Students.StudentSurname,Students.StudentFirstNames ) AS query1 " +
                       "Where " +
                       "query1.MarksPecentage BETWEEN @Percentage1 AND @Percentage2 order by query1.StudentSurname asc";

                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Year1", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class1", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode1", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P1", "1è P");

                        cmd.Parameters.AddWithValue("@Year2", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class2", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode2", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P2", "2è P");

                        cmd.Parameters.AddWithValue("@YearExam1", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@ClassExam1", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCodeExam1", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Exam1", "Examen 1");

                        cmd.Parameters.AddWithValue("@Year3", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class3", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode3", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P3", "3è P");

                        cmd.Parameters.AddWithValue("@Year4", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class4", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode4", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P4", "4è P");

                        cmd.Parameters.AddWithValue("@YearExam2", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@ClassExam2", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCodeExam2", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Exam2", "Examen 2");

                        cmd.Parameters.AddWithValue("@Percentage1", Convert.ToInt16(rangeControlPercentage.SelectedRange.Minimum));
                        cmd.Parameters.AddWithValue("@Percentage2", Convert.ToInt16(rangeControlPercentage.SelectedRange.Maximum));

                        rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //CommandBehavior.CloseConnection usefull when you use a connection over and over again in a loop, the connection will remain "open" until the Garbage collector picks it up, and only then will it be released back to the ADO.net Connection pool to be re-used

                    }
                    else
                    {
                        ct = "SELECT * FROM " +
                            "(" +
                       "SELECT Students.StudentNumber, Students.StudentSurname,Students.StudentFirstNames," +
                       "SUM (MarksEntry.SubjectMaxima) as Maxima, SUM (MarksObtained) as Marks," +
                       "SUM (MarksObtained *100.0)/SUM (MarksEntry.SubjectMaxima) as MarksPecentage " +
                       "from Students,MarksEntry " +
                       "where (" +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class= MarksEntry.Class and MarksEntry.SchoolYear= @Year1 and MarksEntry.Class= @Class1 " +
                       "and MarksEntry.SubjectCode= @SubjectCode1 and MarksEntry.AssessmentPeriod= @P1) " +
                       "OR " +
                       "(Students.StudentNumber= MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @Year2 and MarksEntry.Class = @Class2 " +
                       "and MarksEntry.SubjectCode = @SubjectCode2  and MarksEntry.AssessmentPeriod =  @P2) " +
                       "OR " +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @YearExam1 and MarksEntry.Class = @ClassExam1 " +
                       "and MarksEntry.SubjectCode = @SubjectCodeExam1 and MarksEntry.AssessmentPeriod = @Exam1) " +
                       "OR " +
                       "(Students.StudentNumber= MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @Year3 and MarksEntry.Class= @Class3 " +
                       "and MarksEntry.SubjectCode = @SubjectCode3  and MarksEntry.AssessmentPeriod =  @P3) " +
                       "OR " +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @Year4 and MarksEntry.Class= @Class4 " +
                       "and MarksEntry.SubjectCode = @SubjectCode4  and MarksEntry.AssessmentPeriod =  @P4) " +
                       "OR " +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @YearExam2 and MarksEntry.Class = @ClassExam2 " +
                       "and MarksEntry.SubjectCode = @SubjectCodeExam2 and MarksEntry.AssessmentPeriod = @Exam2) " +
                       "OR " +
                       "(Students.StudentNumber= MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @Year5 and MarksEntry.Class= @Class5 " +
                       "and MarksEntry.SubjectCode = @SubjectCode5  and MarksEntry.AssessmentPeriod =  @P5) " +
                       "OR " +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @Year6 and MarksEntry.Class= @Class6 " +
                       "and MarksEntry.SubjectCode = @SubjectCode6  and MarksEntry.AssessmentPeriod =  @P6) " +
                       "OR " +
                       "(Students.StudentNumber = MarksEntry.StudentNumber " +
                       "and Students.Class = MarksEntry.Class and MarksEntry.SchoolYear = @YearExam3 and MarksEntry.Class = @ClassExam3 " +
                       "and MarksEntry.SubjectCode = @SubjectCodeExam3 and MarksEntry.AssessmentPeriod = @Exam3) " +
                       ") " +
                       "group by Students.StudentNumber, Students.StudentSurname,Students.StudentFirstNames ) AS query1 " +
                       "Where " +
                       "query1.MarksPecentage BETWEEN @Percentage1 AND @Percentage2 order by query1.StudentSurname asc";

                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@Year1", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class1", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode1", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P1", "1è P");

                        cmd.Parameters.AddWithValue("@Year2", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class2", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode2", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P2", "2è P");

                        cmd.Parameters.AddWithValue("@YearExam1", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@ClassExam1", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCodeExam1", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Exam1", "Examen 1");

                        cmd.Parameters.AddWithValue("@Year3", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class3", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode3", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P3", "3è P");

                        cmd.Parameters.AddWithValue("@Year4", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class4", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode4", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P4", "4è P");

                        cmd.Parameters.AddWithValue("@YearExam2", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@ClassExam2", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCodeExam2", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Exam2", "Examen 2");

                        cmd.Parameters.AddWithValue("@Year5", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class5", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode5", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P5", "5è P");

                        cmd.Parameters.AddWithValue("@Year6", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@Class6", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCode6", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@P6", "6è P");

                        cmd.Parameters.AddWithValue("@YearExam3", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@ClassExam3", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@SubjectCodeExam3", txtSubjectCode.Text.Trim());
                        cmd.Parameters.AddWithValue("@Exam3", "Examen 3");

                        cmd.Parameters.AddWithValue("@Percentage1", Convert.ToInt16(rangeControlPercentage.SelectedRange.Minimum));
                        cmd.Parameters.AddWithValue("@Percentage2", Convert.ToInt16(rangeControlPercentage.SelectedRange.Maximum));

                        rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //CommandBehavior.CloseConnection usefull when you use a connection over and over again in a loop, the connection will remain "open" until the Garbage collector picks it up, and only then will it be released back to the ADO.net Connection pool to be re-used

                    }

                    //clear gridcontrol
                    gridControlSupplementaryExam.DataSource = null;

                    //refresh gridview to rebuildnew columns
                    gridView1.Columns.Clear();
                    gridView1.PopulateColumns();

                    if (cmbCycle.SelectedIndex >= 2)
                    {
                        //display culumns with 2 semesters
                        gridControlSupplementaryExam.DataSource = CreateDataBranches();
                        gridView1.BestFitColumns();
                        gridView1.OptionsView.ColumnAutoWidth = true;
                        isSemesterGlobalVariable = true;
                    }
                    else
                    {
                        //display culumns with 3 trimesters
                        gridControlSupplementaryExam.DataSource = CreateDataBranches();
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
                        
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle +1).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper()); //merge surname and name
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strMaximaGrandTotal").ToUpper()], rdr.GetValue(3).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], rdr.GetValue(4).ToString());
                       
                        decimal percetangeValue =(decimal) rdr.GetValue(5);
                        //decimal.Round(percetangeValue, 1);
                        
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPercentage").ToUpper()], percetangeValue.ToString("0.#") + "%");

                                         
                    }                    
                                     
                    SchoolYear = cmbSchoolYear.Text;                    
                    subjectGlobalVariable = gridLookUpSubject.Text;
                    className = cmbClass.Text;                   

                    con.Close();
                }

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
        private void CalculateCount()
        {
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                  
                    //tot number of students 
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;                    
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.FieldName = gridView1.Columns[2].ToString();
                    gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strNumberStudents").ToUpper() + ": {0}";

                    //refresh summary
                    gridView1.UpdateSummary();                    
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void frmSupplementaryExam_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save application settings
            Properties.Settings.Default.SupplementaryExamMax = (int)rangeControlPercentage.SelectedRange.Maximum;
            Properties.Settings.Default.SupplementaryExamMin = (int)rangeControlPercentage.SelectedRange.Minimum;
            Properties.Settings.Default.Save();
        }

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                // Create a new report instance.
                reportSupplementaryExam report = new reportSupplementaryExam();

                // Link the required control with the PrintableComponentContainers of a report.
                report.printableComponentContainer1.PrintableComponent = gridControlSupplementaryExam;
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

                    // Create a new report instance.
                    reportSupplementaryExam report = new reportSupplementaryExam();
                    PrinterSettings printerSettings = new PrinterSettings();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlSupplementaryExam;

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
                    reportSupplementaryExam report = new reportSupplementaryExam();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlSupplementaryExam;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        string fileName = saveFileDialog.FileName + ".pdf";
                        //Export to pdf
                        report.ExportToPdf(fileName);

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strSupplementaryExam"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                    reportSupplementaryExam report = new reportSupplementaryExam();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlSupplementaryExam;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Rename worksheet
                        report.CreateDocument(false);

                        string fileName = saveFileDialog.FileName + ".xlsx";
                        //Export to excel
                        report.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = LocRM.GetString("strSupplementaryExam") });

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strSupplementaryExam"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
    }
}