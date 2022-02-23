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
using DevExpress.XtraGrid.Views.Grid;
using static EduXpress.Functions.PublicVariables;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Data.SqlClient;
using EduXpress.Functions;
using System.Resources;
using DevExpress.XtraReports.ReportGeneration;

namespace EduXpress.Education
{
    public partial class frmReportClassMarkSheet : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmReportClassMarkSheet).Assembly);

        public frmReportClassMarkSheet()
        {
            InitializeComponent();
            //gridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.False;
        }
        bool formLoaded = false;
        private void frmReportClassMarkSheet_Load(object sender, EventArgs e)
        {
            reset();
            AutocompleteAcademicYear();           
            formLoaded = true;

            //load from settings the grid height
            trackBarControl1.EditValue =  Properties.Settings.Default.GridReportClassMarkSheetHeight;            
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

            clearControls();
        }
        private void clearControls()
        {
           
            //clear gridcontrol
            gridControlReportClassMarkSheet.DataSource = null;

            //txtMaximaPeriode.Text = "0";
            //txtMaximaExam.Text = "0";
            //txtTot.Text = "0";
            //txtTotGen.Text = "0";
            //txtClass.Text = "";
           // txtSubjectCode.Text = "";
            //txtTeacherID.Text = "";

            cmbAssessmentPeriod.SelectedIndex = -1;
            cmbAssessmentPeriod.Properties.Items.Clear();
        }

        private void frmReportClassMarkSheet_VisibleChanged(object sender, EventArgs e)
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
            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            AutocompleteClass();
            cmbClass.Enabled = true;
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            cmbAssessmentPeriod.Enabled = true;            
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
        

       
        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }

        //get list of subjects in array format
        string listSubjects;
        private void GetSubjectsArray()
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
                    //adp = new SqlDataAdapter();
                    cmd = new SqlCommand("SELECT distinct  Subject.SubjectName AS SchoolSubject, Subject.SubjectCode," +
                        "SubjectAssignment.Class, SubjectPositionBulletin from Subject, SubjectAssignment " +
                        "where Subject.SubjectCode = SubjectAssignment.SubjectCode " +
                        "and SubjectAssignment.Class = @d1 order by SubjectPositionBulletin", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, "SubjectAssignment.Class").Value = cmbClass.Text.Trim();
                    adp = new SqlDataAdapter(cmd);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    // List<string> listSubjects = new List<string>();
                    listSubjects = "";
                    foreach (DataRow drow in dtable.Rows)
                    {
                        // listSubjects.Add(drow[0].ToString());
                        listSubjects = listSubjects + "[" + drow[0].ToString() + "]" + ",";
                    }
                    if (listSubjects != "")
                    {
                        listSubjects = listSubjects.Remove(listSubjects.Length - 1, 1); //Remove the last ","
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
        int maximaSubject, passed;
        
        private void GetSubjectsMaxima()
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
                    //adp = new SqlDataAdapter();
                    //cmd = new SqlCommand("SELECT   Subject.SubjectName AS SchoolSubject, Subject.SubjectCode," +
                    //    "SubjectAssignment.Class, SubjectPositionBulletin from Subject, SubjectAssignment " +
                    //    "where Subject.SubjectCode = SubjectAssignment.SubjectCode " +
                    //    "and SubjectAssignment.Class = @d1 order by SubjectPositionBulletin", con);
                    cmd = new SqlCommand("SELECT   SubjectMaxima,MarksObtained, AssessmentPeriod," +
                        "Class, SchoolYear from MarksEntry " +
                        "where AssessmentPeriod = @d1 and Class = @d2 and SchoolYear = @d3", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, "AssessmentPeriod").Value = cmbAssessmentPeriod.Text.Trim();
                    cmd.Parameters.Add("@d2", SqlDbType.NChar, 80, "Class").Value = cmbClass.Text.Trim();
                    cmd.Parameters.Add("@d3", SqlDbType.NChar, 80, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                    adp = new SqlDataAdapter(cmd);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];

                    maximaSubject = 0;
                    passed = 0;
                    int totalPoints = 0;

                    foreach (DataRow drow in dtable.Rows)
                    {
                        // listSubjects.Add(drow[0].ToString());
                        maximaSubject = maximaSubject + Convert.ToInt16(drow[0]);

                        //totalPoints = Convert.ToInt16(drow[0]);
                        //if (((totalPoints * 100) / maximaSubject) >= 50)
                        //{
                        //    passed++;
                        //}
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
            if (gridView1.DataRowCount > 0)
            {

                try
                {
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = false;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    XtraReport report0 = new XtraReport();
                    report0.Landscape = true;

                    report0.PaperKind = System.Drawing.Printing.PaperKind.A4;  //Set both paperKind for both reports to A4 for the generated report to autofit the paper size.


                    XtraReport report1 = ReportGenerator.GenerateReport(report0, gridView1);
                    //report1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    //report1.Landscape = true;

                    //Display the exported report columns vertically
                    var cellStudentNo = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strStudentNo").ToUpper()).FirstOrDefault();
                    cellStudentNo.Angle = 90f;
                    //cellStudentNo.WidthF = 90f;
                    var cellSurname = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strSurname").ToUpper()).FirstOrDefault();
                    cellSurname.Angle = 90f;
                    var cellName = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strName").ToUpper()).FirstOrDefault();
                    cellName.Angle = 90f;

                    //get subjects culumns
                    string[] entries = listSubjects.Split(',');

                    int count = 0;

                    //declare 40 variables , max so far 29 
                    foreach (string subject in entries)
                    {
                        string extractSubject = subject.Remove(0, 1); //remove [
                        extractSubject = extractSubject.Remove(extractSubject.Length - 1, 1); //remove ]

                        if (count == 0)
                        {
                            var cellName0 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName0.Angle = 90f;
                        }
                        if (count == 1)
                        {
                            var cellName1 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName1.Angle = 90f;
                        }
                        if (count == 2)
                        {
                            var cellName2 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName2.Angle = 90f;
                        }
                        if (count == 3)
                        {
                            var cellName3 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName3.Angle = 90f;
                        }

                        if (count == 4)
                        {
                            var cellName4 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName4.Angle = 90f;
                        }
                        if (count == 5)
                        {
                            var cellName5 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName5.Angle = 90f;
                        }
                        if (count == 6)
                        {
                            var cellName6 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName6.Angle = 90f;
                        }
                        if (count == 7)
                        {
                            var cellName7 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName7.Angle = 90f;
                        }

                        if (count == 8)
                        {
                            var cellName8 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName8.Angle = 90f;
                        }
                        if (count == 9)
                        {
                            var cellName9 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName9.Angle = 90f;
                        }
                        if (count == 10)
                        {
                            var cellName10 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName10.Angle = 90f;
                        }
                        if (count == 11)
                        {
                            var cellName11 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName11.Angle = 90f;
                        }

                        if (count == 12)
                        {
                            var cellName12 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName12.Angle = 90f;
                        }
                        if (count == 13)
                        {
                            var cellName13 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName13.Angle = 90f;
                        }
                        if (count == 14)
                        {
                            var cellName14 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName14.Angle = 90f;
                        }
                        if (count == 15)
                        {
                            var cellName15 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName15.Angle = 90f;
                        }

                        if (count == 16)
                        {
                            var cellName16 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName16.Angle = 90f;
                        }
                        if (count == 17)
                        {
                            var cellName17 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName17.Angle = 90f;
                        }

                        if (count == 18)
                        {
                            var cellName18 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName18.Angle = 90f;
                        }
                        if (count == 19)
                        {
                            var cellName19 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName19.Angle = 90f;
                        }
                        if (count == 20)
                        {
                            var cellName20 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName20.Angle = 90f;
                        }
                        if (count == 21)
                        {
                            var cellName21 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName21.Angle = 90f;
                        }

                        if (count == 22)
                        {
                            var cellName22 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName22.Angle = 90f;
                        }
                        if (count == 23)
                        {
                            var cellName23 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName23.Angle = 90f;
                        }

                        if (count == 24)
                        {
                            var cellName24 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName24.Angle = 90f;
                        }
                        if (count == 25)
                        {
                            var cellName25 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName25.Angle = 90f;
                        }

                        if (count == 26)
                        {
                            var cellName26 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName26.Angle = 90f;
                        }
                        if (count == 27)
                        {
                            var cellName27 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName27.Angle = 90f;
                        }

                        if (count == 28)
                        {
                            var cellName28 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName28.Angle = 90f;
                        }
                        if (count == 29)
                        {
                            var cellName29 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName29.Angle = 90f;
                        }

                        if (count == 30)
                        {
                            var cellName30 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName30.Angle = 90f;
                        }
                        if (count == 31)
                        {
                            var cellName31 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName31.Angle = 90f;
                        }
                        if (count == 32)
                        {
                            var cellName32 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName32.Angle = 90f;
                        }
                        if (count == 33)
                        {
                            var cellName33 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName33.Angle = 90f;
                        }
                        if (count == 34)
                        {
                            var cellName34 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName34.Angle = 90f;
                        }
                        if (count == 35)
                        {
                            var cellName35 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName35.Angle = 90f;
                        }
                        if (count == 36)
                        {
                            var cellName36 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName36.Angle = 90f;
                        }
                        if (count == 37)
                        {
                            var cellName37 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName37.Angle = 90f;
                        }
                        if (count == 38)
                        {
                            var cellName38 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName38.Angle = 90f;
                        }
                        if (count == 39)
                        {
                            var cellName39 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName39.Angle = 90f;
                        }
                        if (count == 40)
                        {
                            var cellName40 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName40.Angle = 90f;
                        }
                        count++;
                    }



                    //Change the exported report columns height
                    var row = report1.AllControls<XRTableRow>().Where(x => x.Controls.Contains(cellStudentNo)).FirstOrDefault();
                    row.HeightF = (int)trackBarControl1.EditValue;

                    report1.CreateDocument();
                    var report2 = new reportClassMarksSheet();
                    var subReport = (XRSubreport)report2.FindControl("xrSubreport1", false);
                    subReport.ReportSource = report1;

                    //reportClassMarksSheet report2 = new reportClassMarksSheet();

                    //report2.xrSubreport1.ReportSource = report1;

                    // report2.CreateDocument();
                    report2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    report2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    report2.Landscape = true;

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {

                        string fileName = saveFileDialog.FileName + ".pdf";
                        //Export to pdf
                        report2.ExportToPdf(fileName);

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strClassMarksheetReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                    gridView1.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
                    // gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = false;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    XtraReport report0 = new XtraReport();
                    report0.Landscape = true;

                    report0.PaperKind = System.Drawing.Printing.PaperKind.A4;  //Set both paperKind for both reports to A4 for the generated report to autofit the paper size.


                    XtraReport report1 = ReportGenerator.GenerateReport(report0, gridView1);
                    //report1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    //report1.Landscape = true;

                    //Display the exported report columns vertically
                    var cellStudentNo = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strStudentNo").ToUpper()).FirstOrDefault();
                    cellStudentNo.Angle = 90f;
                    //cellStudentNo.WidthF = 90f;
                    var cellSurname = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strSurname").ToUpper()).FirstOrDefault();
                    cellSurname.Angle = 90f;
                    var cellName = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strName").ToUpper()).FirstOrDefault();
                    cellName.Angle = 90f;

                    //get subjects culumns
                    string[] entries = listSubjects.Split(',');

                    int count = 0;

                    //declare 40 variables , max so far 29 
                    foreach (string subject in entries)
                    {
                        string extractSubject = subject.Remove(0, 1); //remove [
                        extractSubject = extractSubject.Remove(extractSubject.Length - 1, 1); //remove ]

                        if (count == 0)
                        {
                            var cellName0 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName0.Angle = 90f;
                        }
                        if (count == 1)
                        {
                            var cellName1 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName1.Angle = 90f;
                        }
                        if (count == 2)
                        {
                            var cellName2 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName2.Angle = 90f;
                        }
                        if (count == 3)
                        {
                            var cellName3 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName3.Angle = 90f;
                        }

                        if (count == 4)
                        {
                            var cellName4 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName4.Angle = 90f;
                        }
                        if (count == 5)
                        {
                            var cellName5 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName5.Angle = 90f;
                        }
                        if (count == 6)
                        {
                            var cellName6 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName6.Angle = 90f;
                        }
                        if (count == 7)
                        {
                            var cellName7 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName7.Angle = 90f;
                        }

                        if (count == 8)
                        {
                            var cellName8 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName8.Angle = 90f;
                        }
                        if (count == 9)
                        {
                            var cellName9 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName9.Angle = 90f;
                        }
                        if (count == 10)
                        {
                            var cellName10 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName10.Angle = 90f;
                        }
                        if (count == 11)
                        {
                            var cellName11 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName11.Angle = 90f;
                        }

                        if (count == 12)
                        {
                            var cellName12 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName12.Angle = 90f;
                        }
                        if (count == 13)
                        {
                            var cellName13 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName13.Angle = 90f;
                        }
                        if (count == 14)
                        {
                            var cellName14 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName14.Angle = 90f;
                        }
                        if (count == 15)
                        {
                            var cellName15 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName15.Angle = 90f;
                        }

                        if (count == 16)
                        {
                            var cellName16 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName16.Angle = 90f;
                        }
                        if (count == 17)
                        {
                            var cellName17 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName17.Angle = 90f;
                        }

                        if (count == 18)
                        {
                            var cellName18 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName18.Angle = 90f;
                        }
                        if (count == 19)
                        {
                            var cellName19 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName19.Angle = 90f;
                        }
                        if (count == 20)
                        {
                            var cellName20 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName20.Angle = 90f;
                        }
                        if (count == 21)
                        {
                            var cellName21 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName21.Angle = 90f;
                        }

                        if (count == 22)
                        {
                            var cellName22 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName22.Angle = 90f;
                        }
                        if (count == 23)
                        {
                            var cellName23 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName23.Angle = 90f;
                        }

                        if (count == 24)
                        {
                            var cellName24 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName24.Angle = 90f;
                        }
                        if (count == 25)
                        {
                            var cellName25 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName25.Angle = 90f;
                        }

                        if (count == 26)
                        {
                            var cellName26 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName26.Angle = 90f;
                        }
                        if (count == 27)
                        {
                            var cellName27 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName27.Angle = 90f;
                        }

                        if (count == 28)
                        {
                            var cellName28 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName28.Angle = 90f;
                        }
                        if (count == 29)
                        {
                            var cellName29 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName29.Angle = 90f;
                        }

                        if (count == 30)
                        {
                            var cellName30 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName30.Angle = 90f;
                        }
                        if (count == 31)
                        {
                            var cellName31 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName31.Angle = 90f;
                        }
                        if (count == 32)
                        {
                            var cellName32 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName32.Angle = 90f;
                        }
                        if (count == 33)
                        {
                            var cellName33 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName33.Angle = 90f;
                        }
                        if (count == 34)
                        {
                            var cellName34 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName34.Angle = 90f;
                        }
                        if (count == 35)
                        {
                            var cellName35 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName35.Angle = 90f;
                        }
                        if (count == 36)
                        {
                            var cellName36 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName36.Angle = 90f;
                        }
                        if (count == 37)
                        {
                            var cellName37 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName37.Angle = 90f;
                        }
                        if (count == 38)
                        {
                            var cellName38 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName38.Angle = 90f;
                        }
                        if (count == 39)
                        {
                            var cellName39 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName39.Angle = 90f;
                        }
                        if (count == 40)
                        {
                            var cellName40 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName40.Angle = 90f;
                        }
                        count++;
                    }



                    //Change the exported report columns height
                    var row = report1.AllControls<XRTableRow>().Where(x => x.Controls.Contains(cellStudentNo)).FirstOrDefault();
                    row.HeightF = (int)trackBarControl1.EditValue;

                    report1.CreateDocument();
                    var report2 = new reportClassMarksSheet();
                    var subReport = (XRSubreport)report2.FindControl("xrSubreport1", false);
                    subReport.ReportSource = report1;

                    //reportClassMarksSheet report2 = new reportClassMarksSheet();

                    //report2.xrSubreport1.ReportSource = report1;

                    // report2.CreateDocument();
                    report2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    report2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    report2.Landscape = true;

                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Rename worksheet
                        report2.CreateDocument(false);

                        string fileName = saveFileDialog.FileName + ".xlsx";
                        //Export to excel
                        report2.ExportToXlsx(fileName, new XlsxExportOptions() { SheetName = LocRM.GetString("strClassMarksheetReport") });

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strClassMarksheetReport"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                gridView1.AppearancePrint.Row.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
               // gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                // Create a new report instance.
                //reportClassMarksSheet report3 = new reportClassMarksSheet();
                XtraReport report0 = new XtraReport();
                report0.Landscape = true;

                report0.PaperKind = System.Drawing.Printing.PaperKind.A4;  //Set both paperKind for both reports to A4 for the generated report to autofit the paper size.
                

                XtraReport report1 = ReportGenerator.GenerateReport(report0, gridView1);
                //report1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                //report1.Landscape = true;

                //Display the exported report columns vertically
                var cellStudentNo = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strStudentNo").ToUpper()).FirstOrDefault();
                cellStudentNo.Angle = 90f;
                //cellStudentNo.WidthF = 90f;
                var cellSurname = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strSurname").ToUpper()).FirstOrDefault();
                cellSurname.Angle = 90f;
                var cellName = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strName").ToUpper()).FirstOrDefault();
                cellName.Angle = 90f;

                //get subjects culumns
                string[] entries = listSubjects.Split(',');
               
                int count = 0;

                //declare 40 variables , max so far 29 
                foreach (string subject in entries)
                {
                    string extractSubject = subject.Remove(0, 1); //remove [
                    extractSubject = extractSubject.Remove(extractSubject.Length - 1, 1); //remove ]
                    
                    if (count ==0)
                    {
                        var cellName0 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName0.Angle = 90f;
                    }
                    if (count == 1)
                    {
                        var cellName1 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName1.Angle = 90f;
                    }
                    if (count == 2)
                    {
                        var cellName2 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName2.Angle = 90f;
                    }
                    if (count == 3)
                    {
                        var cellName3 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName3.Angle = 90f;
                    }

                    if (count == 4)
                    {
                        var cellName4 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName4.Angle = 90f;
                    }
                    if (count == 5)
                    {
                        var cellName5 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName5.Angle = 90f;
                    }
                    if (count == 6)
                    {
                        var cellName6 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName6.Angle = 90f;
                    }
                    if (count == 7)
                    {
                        var cellName7 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName7.Angle = 90f;
                    }

                    if (count == 8)
                    {
                        var cellName8 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName8.Angle = 90f;
                    }
                    if (count == 9)
                    {
                        var cellName9 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName9.Angle = 90f;
                    }
                    if (count == 10)
                    {
                        var cellName10 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName10.Angle = 90f;
                    }
                    if (count == 11)
                    {
                        var cellName11 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName11.Angle = 90f;
                    }

                    if (count == 12)
                    {
                        var cellName12 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName12.Angle = 90f;
                    }
                    if (count == 13)
                    {
                        var cellName13 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName13.Angle = 90f;
                    }
                    if (count == 14)
                    {
                        var cellName14 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName14.Angle = 90f;
                    }
                    if (count == 15)
                    {
                        var cellName15 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName15.Angle = 90f;
                    }

                    if (count == 16)
                    {
                        var cellName16 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName16.Angle = 90f;
                    }
                    if (count == 17)
                    {
                        var cellName17 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName17.Angle = 90f;
                    }

                    if (count == 18)
                    {
                        var cellName18 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName18.Angle = 90f;
                    }
                    if (count == 19)
                    {
                        var cellName19 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName19.Angle = 90f;
                    }
                    if (count == 20)
                    {
                        var cellName20 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName20.Angle = 90f;
                    }
                    if (count == 21)
                    {
                        var cellName21 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName21.Angle = 90f;
                    }

                    if (count == 22)
                    {
                        var cellName22 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName22.Angle = 90f;
                    }
                    if (count == 23)
                    {
                        var cellName23 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName23.Angle = 90f;
                    }

                    if (count == 24)
                    {
                        var cellName24 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName24.Angle = 90f;
                    }
                    if (count == 25)
                    {
                        var cellName25 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName25.Angle = 90f;
                    }

                    if (count == 26)
                    {
                        var cellName26 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName26.Angle = 90f;
                    }
                    if (count == 27)
                    {
                        var cellName27 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName27.Angle = 90f;
                    }

                    if (count == 28)
                    {
                        var cellName28 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName28.Angle = 90f;
                    }
                    if (count == 29)
                    {
                        var cellName29 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName29.Angle = 90f;
                    }

                    if (count == 30)
                    {
                        var cellName30 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName30.Angle = 90f;
                    }
                    if (count == 31)
                    {
                        var cellName31 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName31.Angle = 90f;
                    }
                    if (count == 32)
                    {
                        var cellName32 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName32.Angle = 90f;
                    }
                    if (count == 33)
                    {
                        var cellName33 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName33.Angle = 90f;
                    }
                    if (count == 34)
                    {
                        var cellName34 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName34.Angle = 90f;
                    }
                    if (count == 35)
                    {
                        var cellName35 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName35.Angle = 90f;
                    }
                    if (count == 36)
                    {
                        var cellName36 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName36.Angle = 90f;
                    }
                    if (count == 37)
                    {
                        var cellName37 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName37.Angle = 90f;
                    }
                    if (count == 38)
                    {
                        var cellName38 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName38.Angle = 90f;
                    }
                    if (count == 39)
                    {
                        var cellName39 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName39.Angle = 90f;
                    }
                    if (count == 40)
                    {
                        var cellName40 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                        cellName40.Angle = 90f;
                    }
                    count++;
                }            
                
                

                //Change the exported report columns height
                var row = report1.AllControls<XRTableRow>().Where(x => x.Controls.Contains(cellStudentNo)).FirstOrDefault();
                row.HeightF = (int)trackBarControl1.EditValue;

                report1.CreateDocument();
                var report2 = new reportClassMarksSheet();
                var subReport = (XRSubreport)report2.FindControl("xrSubreport1",false);
                subReport.ReportSource = report1;

                //reportClassMarksSheet report2 = new reportClassMarksSheet();

                //report2.xrSubreport1.ReportSource = report1;
                
               // report2.CreateDocument();
                report2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                report2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                report2.Landscape = true;

                // Invoke a Print Preview for the created report document. 
                ReportPrintTool printTool = new ReportPrintTool(report2);
               // printTool.ShowRibbonPreview();
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

                    
                    //reportClassMarksSheet report = new reportClassMarksSheet();
                   


                    XtraReport report0 = new XtraReport();
                    report0.Landscape = true;

                    report0.PaperKind = System.Drawing.Printing.PaperKind.A4;  //Set both paperKind for both reports to A4 for the generated report to autofit the paper size.


                    XtraReport report1 = ReportGenerator.GenerateReport(report0, gridView1);
                    //report1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    //report1.Landscape = true;

                    //Display the exported report columns vertically
                    var cellStudentNo = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strStudentNo").ToUpper()).FirstOrDefault();
                    cellStudentNo.Angle = 90f;
                    //cellStudentNo.WidthF = 90f;
                    var cellSurname = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strSurname").ToUpper()).FirstOrDefault();
                    cellSurname.Angle = 90f;
                    var cellName = report1.AllControls<XRTableCell>().Where(x => x.Text == LocRM.GetString("strName").ToUpper()).FirstOrDefault();
                    cellName.Angle = 90f;

                    //get subjects culumns
                    string[] entries = listSubjects.Split(',');

                    int count = 0;

                    //declare 40 variables , max so far 29 
                    foreach (string subject in entries)
                    {
                        string extractSubject = subject.Remove(0, 1); //remove [
                        extractSubject = extractSubject.Remove(extractSubject.Length - 1, 1); //remove ]

                        if (count == 0)
                        {
                            var cellName0 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName0.Angle = 90f;
                        }
                        if (count == 1)
                        {
                            var cellName1 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName1.Angle = 90f;
                        }
                        if (count == 2)
                        {
                            var cellName2 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName2.Angle = 90f;
                        }
                        if (count == 3)
                        {
                            var cellName3 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName3.Angle = 90f;
                        }

                        if (count == 4)
                        {
                            var cellName4 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName4.Angle = 90f;
                        }
                        if (count == 5)
                        {
                            var cellName5 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName5.Angle = 90f;
                        }
                        if (count == 6)
                        {
                            var cellName6 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName6.Angle = 90f;
                        }
                        if (count == 7)
                        {
                            var cellName7 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName7.Angle = 90f;
                        }

                        if (count == 8)
                        {
                            var cellName8 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName8.Angle = 90f;
                        }
                        if (count == 9)
                        {
                            var cellName9 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName9.Angle = 90f;
                        }
                        if (count == 10)
                        {
                            var cellName10 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName10.Angle = 90f;
                        }
                        if (count == 11)
                        {
                            var cellName11 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName11.Angle = 90f;
                        }

                        if (count == 12)
                        {
                            var cellName12 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName12.Angle = 90f;
                        }
                        if (count == 13)
                        {
                            var cellName13 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName13.Angle = 90f;
                        }
                        if (count == 14)
                        {
                            var cellName14 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName14.Angle = 90f;
                        }
                        if (count == 15)
                        {
                            var cellName15 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName15.Angle = 90f;
                        }

                        if (count == 16)
                        {
                            var cellName16 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName16.Angle = 90f;
                        }
                        if (count == 17)
                        {
                            var cellName17 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName17.Angle = 90f;
                        }

                        if (count == 18)
                        {
                            var cellName18 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName18.Angle = 90f;
                        }
                        if (count == 19)
                        {
                            var cellName19 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName19.Angle = 90f;
                        }
                        if (count == 20)
                        {
                            var cellName20 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName20.Angle = 90f;
                        }
                        if (count == 21)
                        {
                            var cellName21 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName21.Angle = 90f;
                        }

                        if (count == 22)
                        {
                            var cellName22 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName22.Angle = 90f;
                        }
                        if (count == 23)
                        {
                            var cellName23 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName23.Angle = 90f;
                        }

                        if (count == 24)
                        {
                            var cellName24 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName24.Angle = 90f;
                        }
                        if (count == 25)
                        {
                            var cellName25 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName25.Angle = 90f;
                        }

                        if (count == 26)
                        {
                            var cellName26 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName26.Angle = 90f;
                        }
                        if (count == 27)
                        {
                            var cellName27 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName27.Angle = 90f;
                        }

                        if (count == 28)
                        {
                            var cellName28 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName28.Angle = 90f;
                        }
                        if (count == 29)
                        {
                            var cellName29 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName29.Angle = 90f;
                        }

                        if (count == 30)
                        {
                            var cellName30 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName30.Angle = 90f;
                        }
                        if (count == 31)
                        {
                            var cellName31 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName31.Angle = 90f;
                        }
                        if (count == 32)
                        {
                            var cellName32 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName32.Angle = 90f;
                        }
                        if (count == 33)
                        {
                            var cellName33 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName33.Angle = 90f;
                        }
                        if (count == 34)
                        {
                            var cellName34 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName34.Angle = 90f;
                        }
                        if (count == 35)
                        {
                            var cellName35 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName35.Angle = 90f;
                        }
                        if (count == 36)
                        {
                            var cellName36 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName36.Angle = 90f;
                        }
                        if (count == 37)
                        {
                            var cellName37 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName37.Angle = 90f;
                        }
                        if (count == 38)
                        {
                            var cellName38 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName38.Angle = 90f;
                        }
                        if (count == 39)
                        {
                            var cellName39 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName39.Angle = 90f;
                        }
                        if (count == 40)
                        {
                            var cellName40 = report1.AllControls<XRTableCell>().Where(x => x.Text == extractSubject).FirstOrDefault();
                            cellName40.Angle = 90f;
                        }
                        count++;
                    }



                    //Change the exported report columns height
                    var row = report1.AllControls<XRTableRow>().Where(x => x.Controls.Contains(cellStudentNo)).FirstOrDefault();
                    row.HeightF = (int)trackBarControl1.EditValue;

                    report1.CreateDocument();
                    var report2 = new reportClassMarksSheet();
                    var subReport = (XRSubreport)report2.FindControl("xrSubreport1", false);
                    subReport.ReportSource = report1;

                    //reportClassMarksSheet report2 = new reportClassMarksSheet();

                    //report2.xrSubreport1.ReportSource = report1;

                    // report2.CreateDocument();
                    report2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    report2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    report2.Landscape = true;

                    PrinterSettings printerSettings = new PrinterSettings();
                    // Specify the PrinterName if the target printer is not the default one.
                    printerSettings.PrinterName = Properties.Settings.Default.ReportPrinter;

                    // Invoke a Print Preview for the created report document. 
                    ReportPrintTool printTool = new ReportPrintTool(report2);

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

        private void btnReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
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
            

            if (cmbAssessmentPeriod.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectAssessmentPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAssessmentPeriod.Focus();
                return;
            }            

            //get list of subjects for this class;
            GetSubjectsArray();
            //get subject maxima to calculate pass rate
            GetSubjectsMaxima();
            gridView1.Columns.Clear();
            if (listSubjects !="")
            {
                gridControlReportClassMarkSheet.DataSource = Getdata();

                CalculateCount();
                SchoolYear = cmbSchoolYear.Text;
                assessmentPeriodGlobalVariable = cmbAssessmentPeriod.Text;
                className = cmbClass.Text;

                SetGridColumnsCaption();
            }
                    

        }
       
        public DataView Getdata()
        {          

            DataSet SampleSource = new DataSet();
            DataView TableView = null;

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
                    //alias for the table, and get all the students with their marks
                    //Store the table in a temporary table valid for this connection only.
                    string ct = "SELECT S.StudentNumber, " +
                        "S.StudentSurname, " +
                        "S.StudentFirstNames, " +
                        "S.SchoolYear, " +
                        "S.Class, " +
                        "M.SubjectCode, " +
                        "M.AssessmentPeriod, " +
                        "M.SubjectMaxima, " +
                        "MarksObtained, " +
                        "SJ.SubjectName " +
                        "into ##tempTable " +
                        "from Students S " +
                        "INNER JOIN MarksEntry M ON S.StudentNumber = M.StudentNumber and S.Class = M.Class " +
                        "INNER JOIN Subject SJ ON M.SubjectCode = SJ.SubjectCode " +
                        "where M.SchoolYear = @d1 " +
                        "and M.Class =  @d2 " +
                        "and M.AssessmentPeriod = @d3 " +
                        "order by S.StudentSurname ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbClass.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", cmbAssessmentPeriod.Text.Trim());

                    //rdr = cmd.ExecuteReader();
                    cmd.ExecuteNonQuery();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
                    string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
                    string NameColumn = LocRM.GetString("strName").ToUpper();

                    //Next swap data from rows to columns to display subject names as columns using PIVOT.
                    // read more at: 
                    //1. https://www.codeproject.com/Questions/5300387/Sql-query-how-to-show-column-based-on-data-from-a
                    //2.Simple Way To Use Pivot In SQL Query: https://www.codeproject.com/Tips/500811/Simple-Way-To-Use-Pivot-In-SQL-Query
                    //3. Processing Loops in SQL Server (Pivot and dynamic pivot): https://www.codeproject.com/Articles/1177788/Processing-Loops-in-SQL-Server#tabular-results
                    // dynamic SelectQry = @"SELECT StudentNumber as [Student Number], StudentSurname as [Surname], StudentName as [Name], [Religion],[Microbiologie],[Education Physique]
                    dynamic SelectQry = @"SELECT StudentNumber as [" + StudentNoColumn + "], StudentSurname as [" + SurnameColumn + "], " +
                        "StudentName as [" + NameColumn + "], " + listSubjects + @"                   
                   
                                FROM
                                (
	                                select StudentNumber, StudentSurname, StudentFirstNames as StudentName, MarksObtained, SubjectName
	                                from ##tempTable
                                 ) SRC
                                PIVOT 
                                (
	                                SUM(MarksObtained) FOR SubjectName in (" + listSubjects + @")
                                ) PVT order by StudentSurname ";

                    
                    //populate tableview
                    SqlCommand SampleCommand = new SqlCommand();
                    dynamic SampleDataAdapter = new SqlDataAdapter();
                    SampleCommand.CommandText = SelectQry;
                    SampleCommand.Connection = con;
                    SampleCommand.ExecuteNonQuery();
                    SampleDataAdapter.SelectCommand = SampleCommand;
                    SampleDataAdapter.Fill(SampleSource);
                    TableView = SampleSource.Tables[0].DefaultView;
                    

                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    //drop/delete the global tempTable
                    string str = " Drop table ##tempTable";
                    cmd = new SqlCommand(str, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();

                    if ((con.State == ConnectionState.Open))
                    {
                        con.Close();
                        con.Dispose();
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
            return TableView;
        }
         private void SetGridColumnsCaption() ///this is importnat for the customheaderdrwar to display header names
        {

            gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].Caption = LocRM.GetString("strStudentNo").ToUpper();
            gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].AppearanceHeader.FontStyleDelta = FontStyle.Bold;
            gridView1.Columns[LocRM.GetString("strSurname").ToUpper()].Caption = LocRM.GetString("strSurname").ToUpper();
            gridView1.Columns[LocRM.GetString("strSurname").ToUpper()].AppearanceHeader.FontStyleDelta = FontStyle.Bold;
            gridView1.Columns[LocRM.GetString("strName").ToUpper()].Caption = LocRM.GetString("strName").ToUpper();
            gridView1.Columns[LocRM.GetString("strName").ToUpper()].AppearanceHeader.FontStyleDelta = FontStyle.Bold;
            //  gridView1.PopulateColumns();


            //get subjects culumns
            string[] entries = listSubjects.Split(',');
            foreach (string subject in entries)
            {                
                string extractSubject = subject.Remove(0, 1); //remove [
                extractSubject = extractSubject.Remove(extractSubject.Length - 1, 1); //remove ]
                gridView1.Columns[extractSubject].Caption = extractSubject;
                gridView1.Columns[extractSubject].AppearanceHeader.FontStyleDelta = FontStyle.Bold;
                //gridView1.Columns["Column2"].AppearanceCell.Font = new Font("Times New Roman", 15); 
                // gridView1.Columns[extractSubject].AppearanceHeader.FontSizeDelta = 10;
            }

        }

        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            gridView1.ColumnPanelRowHeight = (int)trackBarControl1.EditValue;
            Properties.Settings.Default.GridReportClassMarkSheetHeight = (int)trackBarControl1.EditValue;
            //GridReportClassMarkSheetHeight
        }

        private void frmReportClassMarkSheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            //save application settings
            Properties.Settings.Default.Save();            
        }
        //draw custom header manually to change the header column orientation to vertical
        private void gridView1_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
        {
            if (e.Column != null)
            {
                e.Info.Caption = "";
                e.Painter.DrawObject(e.Info);
                System.Drawing.Drawing2D.GraphicsState state = e.Graphics.Save();
                Rectangle r = e.Info.CaptionRect;
                StringFormat sf = new StringFormat();
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = sf.FormatFlags | StringFormatFlags.NoWrap;
                if (e.Column.Caption == "Horizontal")
                {
                    sf.LineAlignment = StringAlignment.Far;
                }
                else
                {
                    e.Graphics.RotateTransform(270);
                    SizeF stringSize = e.Graphics.MeasureString(e.Column.Caption, e.Appearance.Font);
                    int startX;
                    if (stringSize.Width > e.Bounds.Height)
                        startX = -e.Bounds.Bottom;
                    else
                        startX = -e.Bounds.Bottom + (e.Bounds.Height - (int)stringSize.Width) / 2;
                    r.X = startX;
                    r.Y = e.Info.CaptionRect.X + (e.Info.CaptionRect.Width - (int)stringSize.Height) / 2;
                    r.Width = e.Bounds.Height;
                    r.Height = e.Bounds.Width;
                }
                e.Handled = true;
                e.Graphics.DrawString(e.Column.Caption, e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), r, sf);
                e.Graphics.Restore(state);
            }
        }

        private void CalculateCount()
        {
            int numberStudents, totalPoints, totalPointsSubject;// passed ;
            double avarage;
            string subjectTotAvarage = "";

            //maximaSubject = 0;
            numberStudents = 0;
            totalPoints = 0;
            totalPointsSubject = 0;
           // passed = 0;
            avarage = 0;
            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    //display number of students owing
                    gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Count;                   
                    gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].SummaryItem.FieldName = gridView1.Columns[2].ToString();
                    gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].SummaryItem.DisplayFormat = LocRM.GetString("strNumberStudents").ToUpper() + ": {0}";
                    numberStudents = Convert.ToInt16(gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()].SummaryItem.SummaryValue);
                                        
                    //Display sum of school marks
                    string[] entries = listSubjects.Split(',');
                    int count = 0; 
                    foreach (string subject in entries)
                    {
                        // listSubjects = listSubjects.Remove(listSubjects.Length - 1, 1); //Remove the last ","
                        string extractSubject = subject.Remove(0, 1); //remove [
                        extractSubject = extractSubject.Remove(extractSubject.Length-1, 1); //remove ]
                        gridView1.Columns[extractSubject].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                        gridView1.Columns[extractSubject].SummaryItem.FieldName = gridView1.Columns[3 + count].ToString();
                        gridView1.Columns[extractSubject].SummaryItem.DisplayFormat = LocRM.GetString("strTotalPaid").ToUpper() + ": {0}";
                        count++;
                        totalPoints = totalPoints + Convert.ToInt16(gridView1.Columns[extractSubject].SummaryItem.SummaryValue);
                        totalPointsSubject = Convert.ToInt16(gridView1.Columns[extractSubject].SummaryItem.SummaryValue);
                        avarage = (double)totalPointsSubject / numberStudents;
                        avarage = Math.Round(avarage, 1);
                        subjectTotAvarage = subjectTotAvarage + extractSubject + ": " + LocRM.GetString("strTotal") + ": " + totalPointsSubject.ToString() + ", " + LocRM.GetString("strClassAvarage") + ": " + avarage.ToString() + Environment.NewLine;
                    }

                    totalPointsGlobalVariable = totalPoints;
                    subjectTotAvarage = subjectTotAvarage  + LocRM.GetString("strNumberSubjects") + ": " + count.ToString() + Environment.NewLine;
                    subjectTotAvarage = subjectTotAvarage + LocRM.GetString("strTotals") + ": " + totalPoints.ToString() + Environment.NewLine;
                    avarage = (double)totalPoints / (numberStudents * count);
                    avarage = Math.Round(avarage, 1);
                    subjectTotAvarage = subjectTotAvarage + LocRM.GetString("strClassAvarageTotal") + ": " + avarage.ToString() + Environment.NewLine;
                    
                    double passedPercentage = (double)(totalPoints * 100) / maximaSubject;
                    passedPercentage = Math.Round(passedPercentage, 1);
                    subjectTotAvarage = subjectTotAvarage + LocRM.GetString("strTotalPercentageClass") + ": " + passedPercentage.ToString()+ "%" + Environment.NewLine;

                    //calculate pass rate
                 //   double passedRatePercentage = (double)(passed * 100) / numberStudentsGlobalVariable;

                    //passedPercentage = Math.Round(passedPercentage, 1);
                    // passed
                    subjectTotAvarageGlobalVariable = subjectTotAvarage;

                    //hide the summary footer
                      gridView1.OptionsView.ShowFooter = false;

                   
                    //refresh summary
                    gridView1.UpdateSummary();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}