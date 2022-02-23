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
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars.Ribbon;
using static EduXpress.Functions.PublicVariables;
using System.Data.SqlClient;
using EduXpress.Functions;
using System.Resources;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using System.Drawing.Printing;
using DevExpress.XtraPrinting.Drawing;
using System.Net.Mail;
using DevExpress.Pdf;

namespace EduXpress.Education
{
    public partial class userControlBulletins : DevExpress.XtraEditors.XtraUserControl, Functions.IMergeRibbons
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlBulletins).Assembly);


        //create global methods of ribons and status bar to merge when in main.
        //add the ImergeRibbons interface.
        public RibbonControl MainRibbon { get; set; }
        public RibbonStatusBar MainStatusBar { get; set; }
        public userControlBulletins()
        {
            InitializeComponent();
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

       
        bool formLoaded = false;
       
        private void userControlBulletins_Load(object sender, EventArgs e)
        {
            reset();
            AutocompleteAcademicYear();           
            formLoaded = true;
        }
        int currentYearComboIndex = 0;
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
                    if (cmbSchoolYear.Properties.Items.Count >0 )
                    {
                        currentYearComboIndex = cmbSchoolYear.Properties.Items.Count - 1;
                        cmbSchoolYear.SelectedIndex = currentYearComboIndex; //Select current school year.
                    }
                   // cmbSchoolYear.SelectedIndex = -1;
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

        private void cmbSchoolYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbCycle.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            clearControls();

            //clear gridcontrol
            gridControlSchoolReport.DataSource = null;

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

            //clear gridcontrol
            gridControlSchoolReport.DataSource = null;

            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

            if (cmbCycle.SelectedIndex >= 2)
            {
                //display culumns with 2 semesters
                gridControlSchoolReport.DataSource = CreateDataBranchesSecondary();
                gridView1.BestFitColumns();
                gridView1.OptionsView.ColumnAutoWidth = true;
                isSemester = true;
            }
            else
            {
                //display culumns with 3 trimesters
                gridControlSchoolReport.DataSource = CreateDataBranchesPrimary();
                gridView1.BestFitColumns();
                gridView1.OptionsView.ColumnAutoWidth = true;
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

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
           
            cmbAssessmentPeriod.Enabled = true;
            cmbFromPeriod.Enabled = true;
            cmbToPeriod.Enabled = true;
            loadStudents();           
            loadAssessmentPeriod();
        }
        int noStudents ;
        
        private void loadStudents()
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
                    
                        cmd = new SqlCommand("SELECT  StudentNumber,StudentSurname,StudentFirstNames,SchoolYear,Cycle,Section,Class,Gender,DateBirth,FatherSurname,FatherNames,MotherSurname,MotherNames,NotificationEmail  FROM Students where SchoolYear=@d1 and Cycle=@d2 and Section=@d3 and Class=@d4", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " SchoolYear").Value = cmbSchoolYear.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 80, " Cycle").Value = cmbCycle.Text.Trim();
                    cmd.Parameters.Add("@d3", SqlDbType.NChar, 80, " Section").Value = cmbSection.Text.Trim();
                    cmd.Parameters.Add("@d4", SqlDbType.NChar, 80, " Class").Value = cmbClass.Text.Trim();

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    cmbStudentSurnameName.Properties.Items.Clear();
                    cmbStudentNo.Properties.Items.Clear();
                    cmbSurname.Properties.Items.Clear();
                    cmbName.Properties.Items.Clear();
                    cmbParentNames.Properties.Items.Clear();
                    cmbNotificationEmail.Properties.Items.Clear();

                    noStudents = 0;                   

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbStudentSurnameName.Properties.Items.Add(drow[1].ToString() + " " + drow[2].ToString());
                        cmbStudentNo.Properties.Items.Add(drow[0].ToString());
                        cmbSurname.Properties.Items.Add(drow[1].ToString());
                        cmbName.Properties.Items.Add(drow[2].ToString());
                        noStudents++;

                        cmbParentNames.Properties.Items.Add(drow[9].ToString() + " " + drow[10].ToString() + " " + 
                            LocRM.GetString("strAnd") + " " + drow[11].ToString() + " " + drow[12].ToString());
                        cmbNotificationEmail.Properties.Items.Add(drow[13].ToString());
                    }
                    cmbStudentSurnameName.SelectedIndex = -1;
                    cmbStudentSurnameName.Enabled = true;
                    cmbStudentNo.SelectedIndex = -1;
                    cmbSurname.SelectedIndex = -1;
                    cmbName.SelectedIndex = -1;
                    cmbNotificationEmail.SelectedIndex = -1;
                    cmbParentNames.SelectedIndex = -1;
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
        bool isSemesterGlobalVariable = false;
        private void loadAssessmentPeriod()
        {
            if (cmbCycle.SelectedIndex >= 2)
            {
                //display culumns with 2 semesters
                cmbAssessmentPeriod.Properties.Items.Clear();
                cmbFromPeriod.Properties.Items.Clear();
                cmbToPeriod.Properties.Items.Clear();

                cmbAssessmentPeriod.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("str1eP"),
                LocRM.GetString("str2eP") ,
                LocRM.GetString("strExam1"),
                LocRM.GetString("str3eP"),
                LocRM.GetString("str4eP") ,
                LocRM.GetString("strExam2")
                });

                cmbFromPeriod.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("str1eP"),
                LocRM.GetString("str2eP") ,
                LocRM.GetString("strExam1"),
                LocRM.GetString("str3eP"),
                LocRM.GetString("str4eP") ,
                LocRM.GetString("strExam2")
                });

                cmbToPeriod.Properties.Items.AddRange(new object[]
                {
                LocRM.GetString("str1eP"),
                LocRM.GetString("str2eP") ,
                LocRM.GetString("strExam1"),
                LocRM.GetString("str3eP"),
                LocRM.GetString("str4eP") ,
                LocRM.GetString("strExam2")
                });

                isSemesterGlobalVariable = true;
                
            }
            else
            {
                //display culumns with 3 trimesters
                cmbAssessmentPeriod.Properties.Items.Clear();
                cmbFromPeriod.Properties.Items.Clear();
                cmbToPeriod.Properties.Items.Clear();

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

                cmbFromPeriod.Properties.Items.AddRange(new object[]
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

                cmbToPeriod.Properties.Items.AddRange(new object[]
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

                isSemesterGlobalVariable = false;
            }
        }
        private void clearControls()
        {  
            txtClass.Text = "";
            txtGender.Text = "";
            
            cmbAssessmentPeriod.SelectedIndex = -1;
            cmbFromPeriod.SelectedIndex = -1;
            cmbToPeriod.SelectedIndex = -1;
            cmbAssessmentPeriod.Properties.Items.Clear();
            rdOnePeriod.Checked = true;

            cmbStudentSurnameName.SelectedIndex = -1;
            cmbStudentSurnameName.Properties.Items.Clear();
            picStudent.EditValue = null;
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

            cmbSchoolReportTemplate.SelectedIndex = -1;
            ckAddDate.Checked = false;

            clearControls();
        }

        private void userControlBulletins_VisibleChanged(object sender, EventArgs e)
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
                    cmbSchoolYear.SelectedIndex = currentYearComboIndex; //Select current school year.
                }
            }
        }

        private void btnReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            cmbSchoolYear.SelectedIndex = currentYearComboIndex; //Select current school year.
            //clear gloabal multidimensional array that stores points
            Array.Clear(pointArrayGlobalVariable, 0, pointArrayGlobalVariable.Length);
        }
        int maximaPeriod, maximaExam, maximaTot, maximaTotGenSemester, maximaTotGenTrimester, maximaGeneraux, totaux;

        // Create a watermark with the specified text.
        private Watermark CreateTextWatermark(string text)
        {
            Watermark textWatermark = new Watermark();

            textWatermark.Text = text;
            textWatermark.TextDirection = DirectionMode.ForwardDiagonal;
            textWatermark.Font = new Font(textWatermark.Font.FontFamily, 40);
            textWatermark.ForeColor = Color.DodgerBlue;
            textWatermark.TextTransparency = 150;
            textWatermark.ShowBehind = false;

            return textWatermark;
        }
       

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }

                // show student photo
                if (ckStudentPhotoSchoolReport.Checked == true)
                {
                    studentPhotoGlobalVariable = true;
                }
                else
                {
                    studentPhotoGlobalVariable = false;
                }

                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }

                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;

                try
                {
                    // gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    // Create a new report instance.
                   reportSchoolReport report = new reportSchoolReport();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlSchoolReport;
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
                    Cursor = Cursors.Default;
                    timer1.Enabled = false;
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
                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }

                // show student photo
                if (ckStudentPhotoSchoolReport.Checked == true)
                {
                    studentPhotoGlobalVariable = true;
                }
                else
                {
                    studentPhotoGlobalVariable = false;
                }
                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }


                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                    // Create a new report instance.
                    reportSchoolReport report = new reportSchoolReport();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlSchoolReport;
                    report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                    report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                    report.Landscape = true;

                    // Invoke a Print Preview for the created report document. 
                    ReportPrintTool printTool = new ReportPrintTool(report);
                    printTool.ShowRibbonPreviewDialog();
                }
                catch (Exception ex)
                {
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    Cursor = Cursors.Default;
                    timer1.Enabled = false;
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPreview"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {

                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }

                // show student photo
                if (ckStudentPhotoSchoolReport.Checked == true)
                {
                    studentPhotoGlobalVariable = true;
                }
                else
                {
                    studentPhotoGlobalVariable = false;
                }

                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                    // gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 

                    // Create a new report instance.
                    reportSchoolReport report = new reportSchoolReport();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlSchoolReport;
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
                    Cursor = Cursors.Default;
                    timer1.Enabled = false;
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        int numberStudents, passed;

        private void btnBulletinPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                if (cmbSchoolReportTemplate.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectReportTemplate"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSchoolReportTemplate.Focus();
                    return;
                }
                //If it's template view, don't display marks on bulletin.
                isViewTemplateGlobalVariable = false;

                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }
                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }

                try
                {
                    if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère, 2ème Année Humanité Littéraire")
                    {
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinLIT_1_2 reportLIT_1_2 = new BulletinsRDC.reportBulletinLIT_1_2();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                        if (ckPrintFrontBack.Checked == true)
                        {
                            // Create the first report and generate its document.
                            reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_1_2.Landscape = false;
                            reportLIT_1_2.CreateDocument();

                            // Create the second report and generate its document.
                            reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportVerso.Landscape = false;
                            reportVerso.CreateDocument();


                            // Add all pages of the second report to the end of the first report.
                            reportLIT_1_2.ModifyDocument(x =>
                            {
                                x.AddPages(reportVerso.Pages);
                            });

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_1_2);
                            printTool.ShowRibbonPreviewDialog();

                        }
                        else
                        { 

                            reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_1_2.Landscape = false;

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_1_2);
                            printTool.ShowRibbonPreviewDialog();
                        }                        

                    }
                    else if (cmbSchoolReportTemplate.Text == "Bulletin de la 3ème Année Humanité Littéraire")
                    {
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinLIT_3 reportLIT_3 = new BulletinsRDC.reportBulletinLIT_3();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                        if (ckPrintFrontBack.Checked == true)
                        {
                            // Create the first report and generate its document.
                            reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_3.Landscape = false;
                            reportLIT_3.CreateDocument();

                            // Create the second report and generate its document.
                            reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportVerso.Landscape = false;
                            reportVerso.CreateDocument();


                            // Add all pages of the second report to the end of the first report.
                            reportLIT_3.ModifyDocument(x =>
                            {
                                x.AddPages(reportVerso.Pages);
                            });

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_3);
                            printTool.ShowRibbonPreviewDialog();

                        }
                        else
                        {

                            reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_3.Landscape = false;

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_3);
                            printTool.ShowRibbonPreviewDialog();
                        }

                    }
                    //Bulletin de la 1ère Année Humanité Commercial & Gestion
                    else if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère Année Humanité Commercial & Gestion")
                    {
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinCG_1 reportCG_1 = new BulletinsRDC.reportBulletinCG_1();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                        if (ckPrintFrontBack.Checked == true)
                        {
                            // Create the first report and generate its document.
                            reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportCG_1.Landscape = false;
                            reportCG_1.CreateDocument();

                            // Create the second report and generate its document.
                            reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportVerso.Landscape = false;
                            reportVerso.CreateDocument();


                            // Add all pages of the second report to the end of the first report.
                            reportCG_1.ModifyDocument(x =>
                            {
                                x.AddPages(reportVerso.Pages);
                            });

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportCG_1);
                            printTool.ShowRibbonPreviewDialog();

                        }
                        else
                        {

                            reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportCG_1.Landscape = false;

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportCG_1);
                            printTool.ShowRibbonPreviewDialog();
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
                XtraMessageBox.Show(LocRM.GetString("strNoDataPreview"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReportTemplateView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           

            //clear gridcontrol
            gridControlSchoolReport.DataSource = null;
            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();

         
            if (cmbSchoolReportTemplate.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectReportTemplate"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSchoolReportTemplate.Focus();
                    return;
                }

                //If it's template view, don't display marks on bulletin.
                isViewTemplateGlobalVariable = true;

                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }
                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }

            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
                {
                    if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère, 2ème Année Humanité Littéraire")
                    {
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinLIT_1_2 reportLIT_1_2 = new BulletinsRDC.reportBulletinLIT_1_2();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                        if (ckPrintFrontBack.Checked == true)
                        {
                            // Create the first report and generate its document.
                            reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_1_2.Landscape = false;
                            reportLIT_1_2.CreateDocument();

                            // Create the second report and generate its document.
                            reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportVerso.Landscape = false;
                            reportVerso.CreateDocument();


                            // Add all pages of the second report to the end of the first report.
                            reportLIT_1_2.ModifyDocument(x =>
                            {
                                x.AddPages(reportVerso.Pages);
                            });

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_1_2);
                            printTool.ShowRibbonPreviewDialog();

                        }
                        else
                        {

                            reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_1_2.Landscape = false;

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_1_2);
                            printTool.ShowRibbonPreviewDialog();
                        }
                    }
                else if (cmbSchoolReportTemplate.Text == "Bulletin de la 3ème Année Humanité Littéraire")
                {
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                           // Create a new report instance.
                    BulletinsRDC.reportBulletinLIT_3 reportLIT_3 = new BulletinsRDC.reportBulletinLIT_3();
                    BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                    if (ckPrintFrontBack.Checked == true)
                    {
                        // Create the first report and generate its document.
                        reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                        reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        reportLIT_3.Landscape = false;
                        reportLIT_3.CreateDocument();

                        // Create the second report and generate its document.
                        reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                        reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        reportVerso.Landscape = false;
                        reportVerso.CreateDocument();


                        // Add all pages of the second report to the end of the first report.
                        reportLIT_3.ModifyDocument(x =>
                        {
                            x.AddPages(reportVerso.Pages);
                        });

                        // Invoke a Print Preview for the created report document. 
                        ReportPrintTool printTool = new ReportPrintTool(reportLIT_3);
                        printTool.ShowRibbonPreviewDialog();

                    }
                    else
                    {
                        reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                        reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        reportLIT_3.Landscape = false;

                        // Invoke a Print Preview for the created report document. 
                        ReportPrintTool printTool = new ReportPrintTool(reportLIT_3);
                        printTool.ShowRibbonPreviewDialog();
                    }
                }
                else if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère Année Humanité Commercial & Gestion")
                {
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                           // Create a new report instance.
                    BulletinsRDC.reportBulletinCG_1 reportCG_1 = new BulletinsRDC.reportBulletinCG_1();
                    BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                    if (ckPrintFrontBack.Checked == true)
                    {
                        // Create the first report and generate its document.
                        reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                        reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        reportCG_1.Landscape = false;
                        reportCG_1.CreateDocument();

                        // Create the second report and generate its document.
                        reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                        reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        reportVerso.Landscape = false;
                        reportVerso.CreateDocument();


                        // Add all pages of the second report to the end of the first report.
                        reportCG_1.ModifyDocument(x =>
                        {
                            x.AddPages(reportVerso.Pages);
                        });

                        // Invoke a Print Preview for the created report document. 
                        ReportPrintTool printTool = new ReportPrintTool(reportCG_1);
                        printTool.ShowRibbonPreviewDialog();

                    }
                    else
                    {
                        reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                        reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                        reportCG_1.Landscape = false;

                        // Invoke a Print Preview for the created report document. 
                        ReportPrintTool printTool = new ReportPrintTool(reportCG_1);
                        printTool.ShowRibbonPreviewDialog();
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

        private void btnBulletinPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                if (cmbSchoolReportTemplate.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectReportTemplate"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSchoolReportTemplate.Focus();
                    return;
                }

                //If it's template view, don't display marks on bulletin.
                isViewTemplateGlobalVariable = false;

                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }
                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }


                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                    //Bulletin de la 1ère, 2ème Année Humanité Littéraire
                    if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère, 2ème Année Humanité Littéraire")
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                        }

                        // gridView1.BestFitColumns();
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // XtraReport.PrinterName
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinLIT_1_2 reportLIT_1_2 = new BulletinsRDC.reportBulletinLIT_1_2();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();
                        PrinterSettings printerSettings = new PrinterSettings();

                        if (ckPrintFrontBack.Checked == true)
                        {
                            // Create the first report and generate its document.
                            reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_1_2.Landscape = false;
                            reportLIT_1_2.CreateDocument();

                            // Create the second report and generate its document.
                            reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportVerso.Landscape = false;
                            reportVerso.CreateDocument();


                            // Add all pages of the second report to the end of the first report.
                            reportLIT_1_2.ModifyDocument(x =>
                            {
                                x.AddPages(reportVerso.Pages);
                            });

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_1_2);
                            printTool.ShowRibbonPreviewDialog();

                            printTool.Print(Properties.Settings.Default.ReportPrinter);

                        }
                        else
                        {

                            reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_1_2.Landscape = false;

                            // Specify the PrinterName if the target printer is not the default one.
                            printerSettings.PrinterName = Properties.Settings.Default.ReportPrinter;

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_1_2);
                            printTool.ShowRibbonPreviewDialog();

                            printTool.Print(Properties.Settings.Default.ReportPrinter);
                        }

                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }

                    }
                    //Bulletin de la 3ème Année Humanité Littéraire
                    else if (cmbSchoolReportTemplate.Text == "Bulletin de la 3ème Année Humanité Littéraire")
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                        }

                        // gridView1.BestFitColumns();
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // XtraReport.PrinterName
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinLIT_3 reportLIT_3 = new BulletinsRDC.reportBulletinLIT_3();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();
                        PrinterSettings printerSettings = new PrinterSettings();

                        if (ckPrintFrontBack.Checked == true)
                        {
                            // Create the first report and generate its document.
                            reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_3.Landscape = false;
                            reportLIT_3.CreateDocument();

                            // Create the second report and generate its document.
                            reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportVerso.Landscape = false;
                            reportVerso.CreateDocument();


                            // Add all pages of the second report to the end of the first report.
                            reportLIT_3.ModifyDocument(x =>
                            {
                                x.AddPages(reportVerso.Pages);
                            });

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_3);
                            printTool.ShowRibbonPreviewDialog();

                            printTool.Print(Properties.Settings.Default.ReportPrinter);
                        }
                        else
                        {

                            reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_3.Landscape = false;

                            // Specify the PrinterName if the target printer is not the default one.
                            printerSettings.PrinterName = Properties.Settings.Default.ReportPrinter;

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportLIT_3);
                            printTool.ShowRibbonPreviewDialog();

                            printTool.Print(Properties.Settings.Default.ReportPrinter);
                        }

                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }

                    }
                    //Bulletin de la 1ère Année Humanité Commercial & Gestion
                    else if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère Année Humanité Commercial & Gestion")
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                        }

                        // gridView1.BestFitColumns();
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // XtraReport.PrinterName
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinCG_1 reportCG_1 = new BulletinsRDC.reportBulletinCG_1();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();
                        PrinterSettings printerSettings = new PrinterSettings();

                        if (ckPrintFrontBack.Checked == true)
                        {
                            // Create the first report and generate its document.
                            reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportCG_1.Landscape = false;
                            reportCG_1.CreateDocument();

                            // Create the second report and generate its document.
                            reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportVerso.Landscape = false;
                            reportVerso.CreateDocument();


                            // Add all pages of the second report to the end of the first report.
                            reportCG_1.ModifyDocument(x =>
                            {
                                x.AddPages(reportVerso.Pages);
                            });

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportCG_1);
                            printTool.ShowRibbonPreviewDialog();

                            printTool.Print(Properties.Settings.Default.ReportPrinter);
                        }
                        else
                        {

                            reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportCG_1.Landscape = false;

                            // Specify the PrinterName if the target printer is not the default one.
                            printerSettings.PrinterName = Properties.Settings.Default.ReportPrinter;

                            // Invoke a Print Preview for the created report document. 
                            ReportPrintTool printTool = new ReportPrintTool(reportCG_1);
                            printTool.ShowRibbonPreviewDialog();

                            printTool.Print(Properties.Settings.Default.ReportPrinter);
                        }

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
                    Cursor = Cursors.Default;
                    timer1.Enabled = false;
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPrint"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBulletinExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                if (cmbSchoolReportTemplate.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectReportTemplate"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSchoolReportTemplate.Focus();
                    return;
                }
                //If it's template view, don't display marks on bulletin.
                isViewTemplateGlobalVariable = false;

                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }
                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }

                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;

                try
                {
                    if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère, 2ème Année Humanité Littéraire")
                    {
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinLIT_1_2 reportLIT_1_2 = new BulletinsRDC.reportBulletinLIT_1_2();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                        XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = saveFileDialog.FileName + ".pdf";

                            if (ckPrintFrontBack.Checked == true)
                            {
                                // Create the first report and generate its document.
                                reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportLIT_1_2.Landscape = false;
                                reportLIT_1_2.CreateDocument();

                                // Create the second report and generate its document.
                                reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportVerso.Landscape = false;
                                reportVerso.CreateDocument();


                                // Add all pages of the second report to the end of the first report.
                                reportLIT_1_2.ModifyDocument(x =>
                                {
                                    x.AddPages(reportVerso.Pages);
                                });

                                //Export to pdf
                                reportLIT_1_2.ExportToPdf(fileName);
                                if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                            else
                            {

                                reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportLIT_1_2.Landscape = false;

                                //Export to pdf
                                reportLIT_1_2.ExportToPdf(fileName);
                                if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                            

                    }
                    else if (cmbSchoolReportTemplate.Text == "Bulletin de la 3ème Année Humanité Littéraire")
                    {
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinLIT_3 reportLIT_3 = new BulletinsRDC.reportBulletinLIT_3();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                        XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = saveFileDialog.FileName + ".pdf";

                            if (ckPrintFrontBack.Checked == true)
                            {
                                // Create the first report and generate its document.
                                reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportLIT_3.Landscape = false;
                                reportLIT_3.CreateDocument();

                                // Create the second report and generate its document.
                                reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportVerso.Landscape = false;
                                reportVerso.CreateDocument();


                                // Add all pages of the second report to the end of the first report.
                                reportLIT_3.ModifyDocument(x =>
                                {
                                    x.AddPages(reportVerso.Pages);
                                });

                                //Export to pdf
                                reportLIT_3.ExportToPdf(fileName);
                                if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                            else
                            {

                                reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportLIT_3.Landscape = false;

                                //Export to pdf
                                reportLIT_3.ExportToPdf(fileName);
                                if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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


                    }
                    //Bulletin de la 1ère Année Humanité Commercial & Gestion
                    else if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère Année Humanité Commercial & Gestion")
                    {
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                               // Create a new report instance.
                        BulletinsRDC.reportBulletinCG_1 reportCG_1 = new BulletinsRDC.reportBulletinCG_1();
                        BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                        XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = saveFileDialog.FileName + ".pdf";

                            if (ckPrintFrontBack.Checked == true)
                            {
                                // Create the first report and generate its document.
                                reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportCG_1.Landscape = false;
                                reportCG_1.CreateDocument();

                                // Create the second report and generate its document.
                                reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportVerso.Landscape = false;
                                reportVerso.CreateDocument();


                                // Add all pages of the second report to the end of the first report.
                                reportCG_1.ModifyDocument(x =>
                                {
                                    x.AddPages(reportVerso.Pages);
                                });

                                //Export to pdf
                                reportCG_1.ExportToPdf(fileName);
                                if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                            else
                            {
                                reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportCG_1.Landscape = false;

                                //Export to pdf
                                reportCG_1.ExportToPdf(fileName);
                                if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

                    }

                }
                catch (Exception ex)
                {
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    Cursor = Cursors.Default;
                    timer1.Enabled = false;
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSendReportEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (gridView1.DataRowCount > 0)
            {
                if (cmbSchoolReportTemplate.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectReportTemplate"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSchoolReportTemplate.Focus();
                    return;
                }

                //If it's template view, don't display marks on bulletin.
                isViewTemplateGlobalVariable = false;

                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }
                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;

                try
                {
                    if (pf.CheckForInternetConnection() == true)
                    {
                        if (cmbNotificationEmail.Text == "")
                        {
                            XtraMessageBox.Show(LocRM.GetString("strNoParentEmail"), LocRM.GetString("strReportCard"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        //check if one period or a range of periods
                        string AssessmentPeriod = "";
                        if (rdOnePeriod.Checked == true)
                        {
                            AssessmentPeriod = cmbAssessmentPeriod.Text.Trim();
                        }
                        else
                        {
                            AssessmentPeriod = LocRM.GetString("strFrom") + ": " + PeriodStart + " " + LocRM.GetString("strTo") + ": " + cmbToPeriod.Text.Trim();
                        }

                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strExporting"));
                        }

                        // Create a new memory stream and export the report in PDF.
                        MemoryStream stream = new MemoryStream();

                        if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère, 2ème Année Humanité Littéraire")
                        {

                            BulletinsRDC.reportBulletinLIT_1_2 reportLIT_1_2 = new BulletinsRDC.reportBulletinLIT_1_2();
                            BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                            reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_1_2.Landscape = false;


                            if (ckPrintFrontBack.Checked == true)
                            {
                                // Create the first report and generate its document.
                                reportLIT_1_2.CreateDocument();

                                // Create the second report and generate its document.
                                reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportVerso.Landscape = false;
                                reportVerso.CreateDocument();

                                // Add all pages of the second report to the end of the first report.
                                reportLIT_1_2.ModifyDocument(x =>
                                {
                                    x.AddPages(reportVerso.Pages);
                                });

                                //Export the pdf to stream
                                reportLIT_1_2.ExportToPdf(stream);
                            }
                            else
                            {
                                //Export the pdf to stream
                                reportLIT_1_2.ExportToPdf(stream);
                            }
                        }

                        else if (cmbSchoolReportTemplate.Text == "Bulletin de la 3ème Année Humanité Littéraire")
                        {

                            BulletinsRDC.reportBulletinLIT_3 reportLIT_3 = new BulletinsRDC.reportBulletinLIT_3();
                            BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                            reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportLIT_3.Landscape = false;


                            if (ckPrintFrontBack.Checked == true)
                            {
                                // Create the first report and generate its document.
                                reportLIT_3.CreateDocument();

                                // Create the second report and generate its document.
                                reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportVerso.Landscape = false;
                                reportVerso.CreateDocument();

                                // Add all pages of the second report to the end of the first report.
                                reportLIT_3.ModifyDocument(x =>
                                {
                                    x.AddPages(reportVerso.Pages);
                                });

                                //Export the pdf to stream
                                reportLIT_3.ExportToPdf(stream);
                            }
                            else
                            {
                                //Export the pdf to stream
                                reportLIT_3.ExportToPdf(stream);
                            }
                        }

                        //Bulletin de la 1ère Année Humanité Commercial & Gestion
                        else if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère Année Humanité Commercial & Gestion")
                        {

                            BulletinsRDC.reportBulletinCG_1 reportCG_1 = new BulletinsRDC.reportBulletinCG_1();
                            BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                            reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                            reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                            reportCG_1.Landscape = false;


                            if (ckPrintFrontBack.Checked == true)
                            {
                                // Create the first report and generate its document.
                                reportCG_1.CreateDocument();

                                // Create the second report and generate its document.
                                reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                reportVerso.Landscape = false;
                                reportVerso.CreateDocument();

                                // Add all pages of the second report to the end of the first report.
                                reportCG_1.ModifyDocument(x =>
                                {
                                    x.AddPages(reportVerso.Pages);
                                });

                                //Export the pdf to stream
                                reportCG_1.ExportToPdf(stream);
                            }
                            else
                            {
                                //Export the pdf to stream
                                reportCG_1.ExportToPdf(stream);
                            }
                        }


                        // Create a new attachment and add the PDF document.
                        stream.Seek(0, System.IO.SeekOrigin.Begin);
                        Attachment attachedReport = new Attachment(stream, LocRM.GetString("strReportCard") + " " + AssessmentPeriod + ".pdf", "application/pdf");

                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }

                        using (con = new SqlConnection(databaseConnectionString))
                        {
                            con.Open();
                            string ctn = "select RTRIM(SenderName), RTRIM(Username),RTRIM(Password),RTRIM(SMTPAddress),(Port),(TLS_SSL_Required) from EmailSetting where IsDefault='Yes' and IsActive='Yes'";
                            cmd = new SqlCommand(ctn);
                            cmd.Connection = con;
                            rdr = cmd.ExecuteReader();
                            if (rdr.Read())
                            {
                                if (splashScreenManager1.IsSplashFormVisible == false)
                                {
                                    splashScreenManager1.ShowWaitForm();
                                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strEmailSending"));
                                }

                                // Create a new message and attach the PDF document.
                                MailMessage msg = new MailMessage();
                                msg.Attachments.Add(attachedReport);

                                // Specify the sender 
                                msg.From = new MailAddress(Convert.ToString(rdr.GetValue(1)), Convert.ToString(rdr.GetValue(0)));
                                msg.To.Add(new MailAddress(cmbNotificationEmail.Text.Trim(), cmbParentNames.Text.Trim()));

                                // Specify email subject.
                                string periodename = "";
                                if (rdOnePeriod.Checked == true)
                                {
                                    if (cmbAssessmentPeriod.Text == LocRM.GetString("str1eP"))
                                    {
                                        periodename = LocRM.GetString("strAssessment1stPeriod");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("str2eP"))
                                    {
                                        periodename = LocRM.GetString("strAssessment2ndPeriod");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("str3eP"))
                                    {
                                        periodename = LocRM.GetString("strAssessment3rdPeriod");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("str4eP"))
                                    {
                                        periodename = LocRM.GetString("strAssessment4thPeriod");
                                    }

                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("str5eP"))
                                    {
                                        periodename = LocRM.GetString("strAssessment5thPeriod");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("str6eP"))
                                    {
                                        periodename = LocRM.GetString("strAssessment6thPeriod");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("strExam1") && isSemester == true)
                                    {
                                        periodename = LocRM.GetString("strFirstSemesterExam");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("strExam1") && isSemester == false)
                                    {
                                        periodename = LocRM.GetString("strFirstTrimesterExam");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("strExam2") && isSemester == true)
                                    {
                                        periodename = LocRM.GetString("strSecondSemesterExam");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("strExam2") && isSemester == false)
                                    {
                                        periodename = LocRM.GetString("strSecondTrimesterExam");
                                    }
                                    else if (cmbAssessmentPeriod.Text == LocRM.GetString("strExam3") && isSemester == false)
                                    {
                                        periodename = LocRM.GetString("strThirdTrimesterExam");
                                    }
                                }
                                else
                                {
                                    periodename = AssessmentPeriod;
                                }
                                

                                msg.Subject = LocRM.GetString("strReportCard") + " " + periodename;
                                msg.Body = LocRM.GetString("strDearParents") + " " + cmbParentNames.Text + "\n\r" +
                                    LocRM.GetString("strEmailBulletinBody1") + " " +
                                    cmbStudentSurnameName.Text + "\n\r" + LocRM.GetString("strAssessmentPeriod") + ": " + periodename
                                    + "\n\r" + LocRM.GetString("strEmailBulletinBody2");

                                // Specify the SMTP server and send the message.
                                using (SmtpClient smt = new SmtpClient(Convert.ToString(rdr.GetValue(3))))
                                {
                                    smt.Port = Convert.ToInt16(rdr.GetValue(4));
                                    //
                                    if (Convert.ToString(rdr.GetString(5).Trim()) == "Yes")
                                    {
                                        smt.EnableSsl = true;
                                    }
                                    else
                                    {
                                        smt.EnableSsl = false;
                                    }

                                    // smt.UseDefaultCredentials = false;
                                    smt.Credentials = new System.Net.NetworkCredential(Convert.ToString(rdr.GetValue(1)), pf.Decrypt(Convert.ToString(rdr.GetValue(2))));
                                    smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                                    smt.Send(msg);
                                }

                                if ((rdr != null))
                                {
                                    rdr.Close();
                                }
                                if (con.State == ConnectionState.Open)
                                {
                                    con.Close();
                                }

                                // Close the memory stream.
                                stream.Close();
                                stream.Flush();

                                if (splashScreenManager1.IsSplashFormVisible == true)
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                                XtraMessageBox.Show(LocRM.GetString("strSchoolReportCardEmailedParent"), LocRM.GetString("strReportCard"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //Log password recovery transaction in logs
                                string st = LocRM.GetString("strEmailSentTo") + " " + cmbNotificationEmail.Text.Trim();
                                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                                reset();

                            }
                            else
                            {
                                if (splashScreenManager1.IsSplashFormVisible == true)
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                                XtraMessageBox.Show(LocRM.GetString("strEmailNotSent"), LocRM.GetString("strReportCard"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                        }

                    }
                    else
                    {
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        XtraMessageBox.Show(LocRM.GetString("strNoInternetEmailNotSent"), LocRM.GetString("strProgramTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(LocRM.GetString("strFailedSendReportCard") + " : " + ex.ToString() , LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataExport"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void ckAddDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAddDate.Checked==true)
            {
                dtDateReport.Visible = true;
                dtDateReport.EditValue = DateTime.Today;
            }
            else
            {
                dtDateReport.Visible = false;
            }
        }

        private void cmbFromPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rdOnePeriod_CheckedChanged(object sender, EventArgs e)
        {
            if(rdOnePeriod.Checked == true)
            {
                cmbFromPeriod.Visible = false;
                cmbToPeriod.Visible = false;
                lblFromPeriod.Visible = false;
                lblToPeriod.Visible = false;
                lblAssessmentPeriod.Visible = true;

                lblFromPeriodStar.Visible = false;
                lblToPeriodStar.Visible = false;
                lblAssessmentPeriodStar.Visible = true;

                cmbFromPeriod.SelectedIndex = -1;
                cmbToPeriod.SelectedIndex = -1;

                cmbAssessmentPeriod.Visible = true;
            }
            else
            {   cmbFromPeriod.Visible = true;
                cmbToPeriod.Visible = true;
                lblFromPeriod.Visible = true;
                lblToPeriod.Visible = true;
                lblAssessmentPeriod.Visible = false;

                lblFromPeriodStar.Visible = true;
                lblToPeriodStar.Visible = true;
                lblAssessmentPeriodStar.Visible = false;
                cmbAssessmentPeriod.Visible = false;
                cmbAssessmentPeriod.SelectedIndex = -1;
            }
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.DataRowCount > 0)
            {
                //Check if stamp checked to display stamp on bulletin
                if (ckSchoolStamp.Checked == true)
                {
                    useSchoolStampGlobalVariable = true;
                }
                else
                {
                    useSchoolStampGlobalVariable = false;
                }

                // show student photo
                if (ckStudentPhotoSchoolReport.Checked == true)
                {
                    studentPhotoGlobalVariable = true;
                }
                else
                {
                    studentPhotoGlobalVariable = false;
                }

                //Either print the date on report as well or leave dots for manual date with pen.
                if (ckAddDate.Checked == true)
                {
                    addTimeBulletinGlobalVariable = true;
                    DateTime dt = dtDateReport.DateTime;
                    reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
                }
                else
                {
                    addTimeBulletinGlobalVariable = false;
                }


                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                try
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                    }

                    // gridView1.BestFitColumns();
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                    // XtraReport.PrinterName
                    // Create a new report instance.
                    reportSchoolReport report = new reportSchoolReport();
                    PrinterSettings printerSettings = new PrinterSettings();

                    // Link the required control with the PrintableComponentContainers of a report.
                    report.printableComponentContainer1.PrintableComponent = gridControlSchoolReport;

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
                    Cursor = Cursors.Default;
                    timer1.Enabled = false;
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNoDataPrint"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string PeriodStart = "";
        private void btnLoadReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            if (cmbStudentSurnameName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectStudent"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbStudentSurnameName.Focus();
                return;
            }

            if ((cmbAssessmentPeriod.Text == "") && (rdOnePeriod.Checked==true) )
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectAssessmentPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAssessmentPeriod.Focus();
                return;
            }

            if (((cmbFromPeriod.Text == "") || (cmbToPeriod.Text == "")) && (rdRangePeriods.Checked == true))
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectStartEndAssessmentPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbFromPeriod.Focus();
                return;    
            }

            if (rdOnePeriod.Checked == true)
            {
                LoadOneAssessmentReport();
            }
            else
            {
                LoadRangeAssessmentReport();
            }            
        }

        private void btnPDFReportCardsAllStudents_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }           

            if ((cmbAssessmentPeriod.Text == "") && (rdOnePeriod.Checked == true))
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectAssessmentPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAssessmentPeriod.Focus();
                return;
            }

            if (((cmbFromPeriod.Text == "") || (cmbToPeriod.Text == "")) && (rdRangePeriods.Checked == true))
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectStartEndAssessmentPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbFromPeriod.Focus();
                return;
            }
            if (cmbSchoolReportTemplate.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectReportTemplate"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cmbSchoolReportTemplate.Focus();
                    return;
                }
            if(gridView1.DataRowCount > 0)
            {
                //clear gridcontrol
                gridControlSchoolReport.DataSource = null;
                //refresh gridview to rebuildnew columns
                gridView1.Columns.Clear();
                gridView1.PopulateColumns();

                if (cmbCycle.SelectedIndex >= 2)
                {
                    //display culumns with 2 semesters
                    gridControlSchoolReport.DataSource = CreateDataBranchesSecondary();
                    gridView1.BestFitColumns();
                    gridView1.OptionsView.ColumnAutoWidth = true;
                }
                else
                {
                    //display culumns with 3 trimesters
                    gridControlSchoolReport.DataSource = CreateDataBranchesPrimary();
                    gridView1.BestFitColumns();
                    gridView1.OptionsView.ColumnAutoWidth = true;
                }

                //set width of count column to fixed
                gridView1.Columns[LocRM.GetString("strBranches").ToUpper()].OptionsColumn.FixedWidth = true;
                gridView1.Columns[LocRM.GetString("strBranches").ToUpper()].Width = 180;
            }

            //If it's template view, don't display marks on bulletin.
            isViewTemplateGlobalVariable = false;

            //Check if stamp checked to display stamp on bulletin
            if (ckSchoolStamp.Checked == true)
            {
                useSchoolStampGlobalVariable = true;
            }
            else
            {
                useSchoolStampGlobalVariable = false;
            }
            //Either print the date on report as well or leave dots for manual date with pen.
            if (ckAddDate.Checked == true)
            {
                addTimeBulletinGlobalVariable = true;
                DateTime dt = dtDateReport.DateTime;
                reportDateGlobalVariable = dt.ToString("dd/MM/yyyy");
            }
            else
            {
                addTimeBulletinGlobalVariable = false;
            }

            Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;

                try
                {
                    //Bulletin de la 1ère, 2ème Année Humanité Littéraire
                    if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère, 2ème Année Humanité Littéraire")
                    {
                        gridView1.OptionsPrint.AllowMultilineHeaders = true;
                        gridView1.OptionsPrint.AutoWidth = true;
                        gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                                                                                              

                        XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();

                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            string fileName = saveFileDialog.FileName + ".pdf";
                            
                            using (PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor())
                            {
                            // PdfDocumentProcessor component, reference the  DevExpress.Data, DevExpress.Docs, DevExpress.Pdf.Core, and DevExpress.Pdf.Drawing assemblies.
                             //The PdfDocumentProcessorclass is declared in the DevExpress.Pdf namespace

                                pdfDocumentProcessor.CreateEmptyDocument(fileName);
                                int EvaluationPeriodIndex = cmbAssessmentPeriod.SelectedIndex;
                                int FromPeriodIndex = cmbFromPeriod.SelectedIndex;
                                int ToPeriodIndex = cmbToPeriod.SelectedIndex;

                                for (int i = 0; i < cmbStudentSurnameName.Properties.Items.Count; i++)
                                {
                                    cmbStudentSurnameName.SelectedIndex = i;
                                    cmbAssessmentPeriod.SelectedIndex = EvaluationPeriodIndex;
                                    cmbFromPeriod.SelectedIndex = FromPeriodIndex;
                                    cmbToPeriod.SelectedIndex = ToPeriodIndex;

                                    if (rdOnePeriod.Checked == true)
                                    {
                                        LoadOneAssessmentReport();
                                    }
                                    else
                                    {
                                        LoadRangeAssessmentReport();
                                    }
                                    string fileNamei = saveFileDialog.FileName + " " + cmbStudentSurnameName.Text + ".pdf";
                                    //Export to pdf
                                    BulletinsRDC.reportBulletinLIT_1_2 reportLIT_1_2 = new BulletinsRDC.reportBulletinLIT_1_2();
                                    BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                                    if (ckPrintFrontBack.Checked == true)
                                    {
                                        reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                        reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                        reportLIT_1_2.Landscape = false;
                                        reportLIT_1_2.CreateDocument();

                                        // Create the second report and generate its document.
                                        reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                        reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                        reportVerso.Landscape = false;
                                        reportVerso.CreateDocument();

                                        // Add all pages of the second report to the end of the first report.
                                        reportLIT_1_2.ModifyDocument(x =>
                                        {
                                            x.AddPages(reportVerso.Pages);
                                        });

                                        //Export to pdf
                                        reportLIT_1_2.ExportToPdf(fileNamei);
                                        //Append to main pdf file
                                        pdfDocumentProcessor.AppendDocument(fileNamei);
                                    }
                                    else
                                    {
                                        reportLIT_1_2.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                        reportLIT_1_2.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                        reportLIT_1_2.Landscape = false;

                                        //Export to pdf
                                        reportLIT_1_2.ExportToPdf(fileNamei);

                                        //Append to main pdf file
                                        pdfDocumentProcessor.AppendDocument(fileNamei);
                                    }                                    
                                }
                            }

                            if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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

                else if (cmbSchoolReportTemplate.Text == "Bulletin de la 3ème Année Humanité Littéraire")
                {
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                    
                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = saveFileDialog.FileName + ".pdf";

                        using (PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor())
                        {
                            // PdfDocumentProcessor component, reference the  DevExpress.Data, DevExpress.Docs, DevExpress.Pdf.Core, and DevExpress.Pdf.Drawing assemblies.
                            //The PdfDocumentProcessorclass is declared in the DevExpress.Pdf namespace

                            pdfDocumentProcessor.CreateEmptyDocument(fileName);
                            int EvaluationPeriodIndex = cmbAssessmentPeriod.SelectedIndex;
                            int FromPeriodIndex = cmbFromPeriod.SelectedIndex;
                            int ToPeriodIndex = cmbToPeriod.SelectedIndex;

                            for (int i = 0; i < cmbStudentSurnameName.Properties.Items.Count; i++)
                            {
                                cmbStudentSurnameName.SelectedIndex = i;
                                cmbAssessmentPeriod.SelectedIndex = EvaluationPeriodIndex;
                                cmbFromPeriod.SelectedIndex = FromPeriodIndex;
                                cmbToPeriod.SelectedIndex = ToPeriodIndex;

                                if (rdOnePeriod.Checked == true)
                                {
                                    LoadOneAssessmentReport();
                                }
                                else
                                {
                                    LoadRangeAssessmentReport();
                                }
                                string fileNamei = saveFileDialog.FileName + " " + cmbStudentSurnameName.Text + ".pdf";
                                //Export to pdf
                                BulletinsRDC.reportBulletinLIT_3 reportLIT_3 = new BulletinsRDC.reportBulletinLIT_3();
                                BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                                if (ckPrintFrontBack.Checked == true)
                                {
                                    reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                    reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                    reportLIT_3.Landscape = false;
                                    reportLIT_3.CreateDocument();

                                    // Create the second report and generate its document.
                                    reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                    reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                    reportVerso.Landscape = false;
                                    reportVerso.CreateDocument();

                                    // Add all pages of the second report to the end of the first report.
                                    reportLIT_3.ModifyDocument(x =>
                                    {
                                        x.AddPages(reportVerso.Pages);
                                    });

                                    //Export to pdf
                                    reportLIT_3.ExportToPdf(fileNamei);
                                    //Append to main pdf file
                                    pdfDocumentProcessor.AppendDocument(fileNamei);
                                }
                                else
                                {
                                    reportLIT_3.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                    reportLIT_3.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                    reportLIT_3.Landscape = false;

                                    //Export to pdf
                                    reportLIT_3.ExportToPdf(fileNamei);

                                    //Append to main pdf file
                                    pdfDocumentProcessor.AppendDocument(fileNamei);
                                }
                            }
                        }

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                //Bulletin de la 1ère Année Humanité Commercial & Gestion
                else if (cmbSchoolReportTemplate.Text == "Bulletin de la 1ère Année Humanité Commercial & Gestion")
                {
                    gridView1.OptionsPrint.AllowMultilineHeaders = true;
                    gridView1.OptionsPrint.AutoWidth = true;
                    gridView1.AppearancePrint.HeaderPanel.FontStyleDelta = FontStyle.Bold; //Set the column header text Bold 
                    
                    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string fileName = saveFileDialog.FileName + ".pdf";

                        using (PdfDocumentProcessor pdfDocumentProcessor = new PdfDocumentProcessor())
                        {
                            // PdfDocumentProcessor component, reference the  DevExpress.Data, DevExpress.Docs, DevExpress.Pdf.Core, and DevExpress.Pdf.Drawing assemblies.
                            //The PdfDocumentProcessorclass is declared in the DevExpress.Pdf namespace

                            pdfDocumentProcessor.CreateEmptyDocument(fileName);
                            int EvaluationPeriodIndex = cmbAssessmentPeriod.SelectedIndex;
                            int FromPeriodIndex = cmbFromPeriod.SelectedIndex;
                            int ToPeriodIndex = cmbToPeriod.SelectedIndex;

                            for (int i = 0; i < cmbStudentSurnameName.Properties.Items.Count; i++)
                            {
                                cmbStudentSurnameName.SelectedIndex = i;
                                cmbAssessmentPeriod.SelectedIndex = EvaluationPeriodIndex;
                                cmbFromPeriod.SelectedIndex = FromPeriodIndex;
                                cmbToPeriod.SelectedIndex = ToPeriodIndex;

                                if (rdOnePeriod.Checked == true)
                                {
                                    LoadOneAssessmentReport();
                                }
                                else
                                {
                                    LoadRangeAssessmentReport();
                                }
                                string fileNamei = saveFileDialog.FileName + " " + cmbStudentSurnameName.Text + ".pdf";
                                //Export to pdf
                                BulletinsRDC.reportBulletinCG_1 reportCG_1 = new BulletinsRDC.reportBulletinCG_1();
                                BulletinsRDC.reportBulletinVerso reportVerso = new BulletinsRDC.reportBulletinVerso();

                                if (ckPrintFrontBack.Checked == true)
                                {
                                    reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                    reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                    reportCG_1.Landscape = false;
                                    reportCG_1.CreateDocument();

                                    // Create the second report and generate its document.
                                    reportVerso.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                    reportVerso.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                    reportVerso.Landscape = false;
                                    reportVerso.CreateDocument();

                                    // Add all pages of the second report to the end of the first report.
                                    reportCG_1.ModifyDocument(x =>
                                    {
                                        x.AddPages(reportVerso.Pages);
                                    });

                                    //Export to pdf
                                    reportCG_1.ExportToPdf(fileNamei);
                                    //Append to main pdf file
                                    pdfDocumentProcessor.AppendDocument(fileNamei);

                                }
                                else
                                {
                                    reportCG_1.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                                    reportCG_1.PaperKind = System.Drawing.Printing.PaperKind.A4;
                                    reportCG_1.Landscape = false;

                                    //Export to pdf
                                    reportCG_1.ExportToPdf(fileNamei);

                                    //Append to main pdf file
                                    pdfDocumentProcessor.AppendDocument(fileNamei);
                                }
                            }
                        }

                        if (XtraMessageBox.Show(LocRM.GetString("strOpenFile"), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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


               }
                catch (Exception ex)  
                {
                    XtraMessageBox.Show(LocRM.GetString("strFailedGenerateReportCardsClass") + ": \n" + ex.ToString(), LocRM.GetString("strReportCard"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                }
            
        }

        private void LoadOneAssessmentReport()
        {
            numberStudents = 0;
            passed = 0;
            int maximaGenerauxTotalGlobal = 0;
            int columnPeriod = 0;
            // int TotauxTotalGlobal = 0;
            int maximaGenerauxTotalColumnGlobal = 0;
            double studentPercentageTotalColumnGlobal = 0;
            int passedTotalColumnGlobal = 0; //Number of passed students for total columns
            int passedTotalGenColumnGlobal = 0; //Number of passed students for total gen column
            int maximaGenerauxTotGen = 0;  // Maxima generaux for Tot Gen 

            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            try
            {

                //set gridview column header appearance: Appearance, headerPanel, font
                double studentPercentage = 0;
                bool isExam = false;

                //check if exam or trav jour
                if ((cmbAssessmentPeriod.Text == LocRM.GetString("strExam1"))
                                || (cmbAssessmentPeriod.Text == LocRM.GetString("strExam2"))
                                || (cmbAssessmentPeriod.Text == LocRM.GetString("strExam3")))
                {
                    isExam = true;
                }
                else
                {
                    isExam = false;
                }

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strCalculatingSchoolMarks"));
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string ct = "SELECT MarksEntry.StudentNumber, MarksEntry.SchoolYear,MarksEntry.Class,MarksEntry.SubjectCode," +
                        "MarksEntry.AssessmentPeriod,SubjectMaxima,MarksObtained,Subject.SubjectName,SubjectAssignment.SubjectPositionBulletin " +
                        "from MarksEntry,Subject,SubjectAssignment where MarksEntry.SubjectCode = Subject.SubjectCode " +
                        "and MarksEntry.SubjectCode = SubjectAssignment.SubjectCode and MarksEntry.SchoolYear= @d1 and MarksEntry.Class= @d2 and " +
                        "MarksEntry.StudentNumber= @d3 and MarksEntry.AssessmentPeriod = @d4  order by SubjectPositionBulletin";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbClass.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", cmbStudentNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@d4", cmbAssessmentPeriod.Text.Trim());

                    rdr = cmd.ExecuteReader();
                    maximaPeriod = 0;
                    maximaExam = 0;
                    maximaTot = 0;
                    maximaTotGenSemester = 0;
                    maximaTotGenTrimester = 0;
                    maximaGeneraux = 0;
                    totaux = 0;

                    int maximaOld = 0;
                    int rowHandle = 0;
                    int rowCount = 0;
                    int markTotal = 0;
                    int markTotalGen = 0;
                    int maximaGenerauxTotal = 0;
                    int totauxTotal = 0;
                    // string[,] pointArrays = new string [30,9];
                    int subjectPosition = 0;
                    // columnPeriod = 0;

                    while ((rdr.Read() == true))
                    {

                        //count row 
                        rowCount = gridView1.RowCount;

                        //maxima. Check if maxima is zero, add maxima row.
                        //chech if maxima is different to old maxima, add new row, assign this new maxima.
                        if (maximaPeriod == 0)
                        {
                            maximaPeriod = Convert.ToInt16(rdr.GetValue(5));

                            if (isExam == true)
                            {
                                maximaPeriod = maximaPeriod / 2;
                                maximaExam = maximaPeriod * 2;
                                maximaOld = maximaExam;
                                maximaTot = maximaPeriod * 4;
                                maximaTotGenSemester = maximaPeriod * 8;
                                maximaTotGenTrimester = maximaPeriod * 12;

                                #region MyRegionOld
                                //if (maximaPeriod > 0)
                                //    {


                                //    ////New option to be implemented
                                //    //maximaExam = maximaPeriod;
                                //    //maximaOld = maximaExam;
                                //    //maximaTot = maximaExam * 2;
                                //    //maximaTotGenSemester = maximaExam * 4;
                                //    //maximaTotGenTrimester = maximaExam * 6;

                                //    }

                                //    else //version 1.3.2 for subjects which don't have exams, only period and tots
                                //    {                                    
                                //    maximaExam = maximaPeriod ;
                                //    //maximaPeriod = maximaPeriod / 2;
                                //    maximaOld = maximaExam;
                                //    maximaTot = maximaPeriod * 2;
                                //    maximaTotGenSemester = maximaPeriod * 4;
                                //    maximaTotGenTrimester = maximaPeriod * 6;                                    
                                //    } 
                                #endregion

                            }
                            else
                            {
                                maximaOld = maximaPeriod;
                                maximaExam = maximaPeriod * 2;
                                maximaTot = maximaPeriod * 4;
                                maximaTotGenSemester = maximaPeriod * 8;
                                maximaTotGenTrimester = maximaPeriod * 12;
                            }

                            int intMaximValue = 0;
                            bool MaximaExist = false;
                            if (rowCount > 0)
                            {
                                //check if subject already exist
                                for (int i = 0; i < gridView1.DataRowCount; i++)
                                {
                                    int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                    string Maxima = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                    string MaximValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                                    if (MaximValue != "")
                                    {
                                        intMaximValue = Convert.ToInt16(MaximValue);
                                    }
                                    //if ((Maxima != LocRM.GetString("strMaxima").ToUpper()) && MaximValue == "")
                                    //{
                                    //    MaximaExist = false;
                                    //    break;
                                    //}

                                    if ((Maxima == LocRM.GetString("strMaxima").ToUpper()) && MaximValue == "")
                                    {
                                        MaximaExist = false;
                                        break;
                                    }

                                    else if ((Maxima == LocRM.GetString("strMaxima").ToUpper()) && (intMaximValue == maximaOld))
                                    {
                                        MaximaExist = true;
                                        rowHandle = i;
                                        break;
                                    }
                                    else
                                    {
                                        MaximaExist = false;
                                    }
                                }
                            }

                            if (!MaximaExist)
                            {
                                //add new maxima row
                                gridView1.AddNewRow();
                                gridView1.UpdateCurrentRow();
                                rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);


                                // Write Maxima and the Maxima value

                                //New code
                                #region MyRegion
                                //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strMaxima").ToUpper());
                                //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.Trim().ToUpper()], maximaPeriod.ToString());

                                //if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                                //{  
                                ////maxima TOT1 
                                //int maximaTotal = 0;
                                //int intMaximaTotal1P = 0;
                                //int intMaximaTotal2P = 0;
                                //string stMaximaTotal1P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]);
                                //string stMaximaTotal2P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]);
                                //bool canConvert1 = int.TryParse(stMaximaTotal1P, out intMaximaTotal1P);
                                //bool canConvert2 = int.TryParse(stMaximaTotal2P, out intMaximaTotal2P);
                                //maximaTotal = maximaPeriod + intMaximaTotal1P + intMaximaTotal2P;
                                ////gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaTot.ToString()); old
                                //gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaTotal.ToString());
                                //}

                                //else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                                //{
                                //    //maxima TOT2                            
                                //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], maximaTot.ToString());

                                //    if (cmbCycle.SelectedIndex >= 2)
                                //    {
                                //        //maxima GrandTotal                            
                                //        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenSemester.ToString());
                                //    }
                                //}                                

                                //else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                                //{
                                //    //maxima TOT3                            
                                //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], maximaTot.ToString());
                                //    //maxima GrandTotal                            
                                //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenTrimester.ToString());
                                //} 
                                #endregion

                                //maxima 1eP
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strMaxima").ToUpper());
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()], maximaPeriod.ToString());
                                //maxima 2eP                            
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()], maximaPeriod.ToString());
                                //maxima Exam1                            
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam1").ToUpper()], maximaExam.ToString());
                                //maxima TOT1                            
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaTot.ToString());
                                //maxima 3eP                            
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()], maximaPeriod.ToString());
                                //maxima 4eP                            
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()], maximaPeriod.ToString());
                                //maxima Exam2                            
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam2").ToUpper()], maximaExam.ToString());
                                //maxima TOT2                            
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], maximaTot.ToString());
                                //maxima GrandTotal                            
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenSemester.ToString());

                                if (cmbCycle.SelectedIndex < 2)
                                {
                                    //maxima 5eP                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima 6eP                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima Exam3                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam3").ToUpper()], maximaExam.ToString());
                                    //maxima TOT3                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], maximaTot.ToString());
                                    //maxima GrandTotal                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenTrimester.ToString());
                                }
                            }
                        // maximaGeneraux = maximaGeneraux + maximaPeriod;
                        }
                        else
                        {
                            maximaPeriod = Convert.ToInt16(rdr.GetValue(5));
                            if (maximaPeriod != maximaOld)
                            {

                                if (isExam == true)
                                {
                                    maximaPeriod = maximaPeriod / 2;
                                    maximaExam = maximaPeriod * 2;
                                    maximaOld = maximaExam;
                                    maximaTot = maximaPeriod * 4;
                                    maximaTotGenSemester = maximaPeriod * 8;
                                    maximaTotGenTrimester = maximaPeriod * 12;
                                    #region MyRegion
                                    //if (maximaPeriod > 0)
                                    //{
                                    //    maximaExam = maximaPeriod ;
                                    //    maximaOld = maximaExam;
                                    //    maximaTot = maximaExam * 2;
                                    //    maximaTotGenSemester = maximaExam * 4;
                                    //    maximaTotGenTrimester = maximaExam * 6;
                                    //}

                                    //else //version 1.3.2 for subjects which don't have exams, only period and tots
                                    //{

                                    //    maximaExam = maximaPeriod;
                                    //    //maximaPeriod = maximaPeriod / 2;
                                    //    maximaOld = maximaExam;
                                    //    maximaTot = maximaPeriod * 2;
                                    //    maximaTotGenSemester = maximaPeriod * 4;
                                    //    maximaTotGenTrimester = maximaPeriod * 6;

                                    //} 
                                    #endregion
                                }
                                else
                                {
                                    maximaOld = maximaPeriod;
                                    maximaExam = maximaPeriod * 2;
                                    maximaTot = maximaPeriod * 4;
                                    maximaTotGenSemester = maximaPeriod * 8;
                                    maximaTotGenTrimester = maximaPeriod * 12;
                                }

                                int intMaximValue = 0;
                                bool MaximaExist = false;
                                //string MaximaDatabase = rdr[5].ToString();
                                //check if subject already exist
                                for (int i = 0; i < gridView1.DataRowCount; i++)
                                {
                                    int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                    string Maxima = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                    string MaximValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                                    if (MaximValue != "")
                                    {
                                        intMaximValue = Convert.ToInt16(MaximValue);
                                    }
                                    //if ((Maxima != LocRM.GetString("strMaxima").ToUpper()) && MaximValue == "") 
                                    //{
                                    //    MaximaExist = false;
                                    //    break;
                                    //}

                                    if ((Maxima == LocRM.GetString("strMaxima").ToUpper()) && MaximValue == "")
                                    {
                                        MaximaExist = false;
                                        break;
                                    }
                                    else if ((Maxima == LocRM.GetString("strMaxima").ToUpper()) && (intMaximValue == maximaOld))
                                    {
                                        MaximaExist = true;
                                        rowHandle = i;
                                        break;
                                    }
                                    else
                                    {
                                        MaximaExist = false;
                                    }
                                }

                                if (!MaximaExist)
                                {
                                    //add new maxima row
                                    gridView1.AddNewRow();
                                    gridView1.UpdateCurrentRow();
                                    rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                                    // Write Maxima and the Maxima value
                                    //new
                                    #region MyRegion
                                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strMaxima").ToUpper());
                                    //gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.Trim().ToUpper()], maximaPeriod.ToString());

                                    //if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                                    //{  //maxima TOT1                            
                                    //   // gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaTot.ToString());//Old

                                    //    //maxima TOT1 
                                    //    int maximaTotal = 0;
                                    //    int intMaximaTotal1P = 0;
                                    //    int intMaximaTotal2P = 0;
                                    //    string stMaximaTotal1P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]);
                                    //    string stMaximaTotal2P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]);
                                    //    bool canConvert1 = int.TryParse(stMaximaTotal1P, out intMaximaTotal1P);
                                    //    bool canConvert2 = int.TryParse(stMaximaTotal2P, out intMaximaTotal2P);
                                    //    maximaTotal = maximaPeriod + intMaximaTotal1P + intMaximaTotal2P;
                                    //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaTotal.ToString());
                                    //}

                                    //else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                                    //{
                                    //    //maxima TOT2                            
                                    //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], maximaTot.ToString());

                                    //    if (cmbCycle.SelectedIndex >= 2)
                                    //    {
                                    //        //maxima GrandTotal                            
                                    //        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenSemester.ToString());
                                    //    }
                                    //}

                                    //else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                                    //{
                                    //    //maxima TOT3                            
                                    //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], maximaTot.ToString());
                                    //    //maxima GrandTotal                            
                                    //    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenTrimester.ToString());
                                    //} 
                                    #endregion

                                    //maxima 1eP
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strMaxima").ToUpper());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima 2eP                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima Exam1                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam1").ToUpper()], maximaExam.ToString());
                                    //maxima TOT1                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaTot.ToString());
                                    //maxima 3eP                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima 4eP                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima Exam2                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam2").ToUpper()], maximaExam.ToString());
                                    //maxima TOT2                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], maximaTot.ToString());
                                    //maxima GrandTotal                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenSemester.ToString());

                                    if (cmbCycle.SelectedIndex < 2)
                                    {
                                        //maxima 5eP                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()], maximaPeriod.ToString());
                                        //maxima 6eP                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()], maximaPeriod.ToString());
                                        //maxima Exam3                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam3").ToUpper()], maximaExam.ToString());
                                        //maxima TOT3                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], maximaTot.ToString());
                                        //maxima GrandTotal                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenTrimester.ToString());
                                    }

                                }

                                //maximaGeneraux = maximaGeneraux + maximaPeriod;
                            }
                        }
                        //rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                        ////check current row is previous rows exist(previous evaluation period), just add value with no new row
                        //if (rowHandle >= rowCount)
                        //{
                        //    //add new maxima row
                        //    gridView1.AddNewRow();
                        //    gridView1.UpdateCurrentRow();
                        //}

                        //rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        //string test2 = rdr[6].ToString();  //marks
                        //string test3 = rdr[7].ToString(); //subject name
                        //string test4 = rdr[8].ToString(); //SubjectPositionBulletin
                        bool SubjectExist = false;
                        //check if subject already exist
                        string subjectDatabase = rdr[7].ToString();
                        subjectPosition = (int)rdr.GetValue(8);
                        // columnPeriod = 0;
                        if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("str1eP").ToUpper())
                        {
                            columnPeriod = 0;
                        }
                        else if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("str2eP").ToUpper())
                        {
                            columnPeriod = 1;
                        }
                        else if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("strExam1").ToUpper())
                        {
                            columnPeriod = 2;
                        }
                        else if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("str3eP").ToUpper())
                        {
                            columnPeriod = 3;
                        }
                        else if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("str4eP").ToUpper())
                        {
                            columnPeriod = 4;
                        }
                        else if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("strExam2").ToUpper())
                        {
                            columnPeriod = 5;
                        }
                        else if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("str5eP").ToUpper())
                        {
                            columnPeriod = 6;
                        }
                        else if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("str6eP").ToUpper())
                        {
                            columnPeriod = 7;
                        }
                        else if (cmbAssessmentPeriod.Text.ToUpper() == LocRM.GetString("strExam3").ToUpper())
                        {
                            columnPeriod = 8;
                        }
                        pointArrayGlobalVariable[subjectPosition, columnPeriod] = rdr.GetValue(6).ToString();
                        // pointArrays[subjectPosition, columnPeriod] = rdr.GetValue(6).ToString();

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                            string subject = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                            string subjectValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                            if ((subject == subjectDatabase) && (subjectValue == ""))
                            {
                                SubjectExist = true;
                                rowHandle = i;
                                break;
                            }
                            else if (subject == "")
                            {
                                SubjectExist = false;
                                break;
                            }
                            else
                            {
                                SubjectExist = false;
                            }
                        }

                        if (!SubjectExist)
                        {
                            //add new maxima row
                            gridView1.AddNewRow();
                            gridView1.UpdateCurrentRow();
                            rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                        }

                        //evaluation period"
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], rdr[7].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.Trim().ToUpper()], rdr.GetValue(6).ToString());

                        //Update Totaux for all subjects
                        totaux = totaux + Convert.ToInt16(rdr.GetValue(6));
                        if (isExam == true)
                        {
                            //Update Maxima generaux
                            maximaGeneraux = maximaGeneraux + maximaExam;
                            int subjectTotalGen = 0;
                            //update tot column
                            if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                            {
                                // int markTotal = 0;
                                //int maximaGenerauxTotal = 0;
                                //int totauxTotal = 0;
                                //int markTotalGen = 0;  gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                                int intmarkTotal1P = 0;
                                int intmarkTotal2P = 0;
                                string stmarkTotal1P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]);
                                string stmarkTotal2P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]);
                                bool canConvert1 = int.TryParse(stmarkTotal1P, out intmarkTotal1P);
                                bool canConvert2 = int.TryParse(stmarkTotal2P, out intmarkTotal2P);
                                markTotal = Convert.ToInt16(rdr.GetValue(6)) + intmarkTotal1P + intmarkTotal2P;

                                // markTotal =Convert.ToInt16( rdr.GetValue(6)) + Convert.ToInt32( gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]));
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], markTotal.ToString());
                                pointArrayGlobalVariable[subjectPosition, 9] = markTotal.ToString(); //strTotal1 subject
                            }
                            else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                            {
                                int intmarkTotal3P = 0;
                                int intmarkTotal4P = 0;
                                string stmarkTotal3P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]);
                                string stmarkTotal4P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()]);
                                bool canConvert3 = int.TryParse(stmarkTotal3P, out intmarkTotal3P);
                                bool canConvert4 = int.TryParse(stmarkTotal4P, out intmarkTotal4P);
                                markTotal = Convert.ToInt16(rdr.GetValue(6)) + intmarkTotal3P + intmarkTotal4P;

                                //markTotal = Convert.ToInt32(rdr.GetValue(6)) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]));
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], markTotal.ToString());
                                pointArrayGlobalVariable[subjectPosition, 10] = markTotal.ToString(); //strTotal2 subject

                                if (isSemester == true)  //update subjects for TOT Gen column
                                {
                                    int intmarkSubjectTotal1 = 0;
                                    string stmarkSubjectTotal1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                    bool canConvertSubjectTot = int.TryParse(stmarkSubjectTotal1, out intmarkSubjectTotal1);
                                    subjectTotalGen = markTotal + intmarkSubjectTotal1;

                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], subjectTotalGen.ToString());
                                    pointArrayGlobalVariable[subjectPosition, 12] = subjectTotalGen.ToString(); // subjects for TOT Gen column

                                }

                            }
                            else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                            {
                                int intmarkTotal5P = 0;
                                int intmarkTotal6P = 0;
                                string stmarkTotal5P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]);
                                string stmarkTotal6P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()]);
                                bool canConvert5 = int.TryParse(stmarkTotal5P, out intmarkTotal5P);
                                bool canConvert6 = int.TryParse(stmarkTotal6P, out intmarkTotal6P);
                                markTotal = Convert.ToInt16(rdr.GetValue(6)) + intmarkTotal5P + intmarkTotal6P;


                                //markTotal = Convert.ToInt32(rdr.GetValue(6)) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]));
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], markTotal.ToString());
                                pointArrayGlobalVariable[subjectPosition, 11] = markTotal.ToString(); //strTotal3 subject

                                //update Totaux for TOT Gen column
                                int intmarkSubject1 = 0;
                                int intmarkSubject2 = 0;
                                string stmarkSubject1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                string stmarkSubject2 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()]);
                                bool canConvertSubjectTot1 = int.TryParse(stmarkSubject1, out intmarkSubject1);
                                bool canConvertSubjectTot2 = int.TryParse(stmarkSubject2, out intmarkSubject2);
                                subjectTotalGen = totauxTotal + intmarkSubject1 + intmarkSubject2;

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], subjectTotalGen.ToString());
                                pointArrayGlobalVariable[subjectPosition, 12] = subjectTotalGen.ToString(); // Subject Tot general
                            }
                        }
                        else
                        {
                            //Update Maxima generaux
                            maximaGeneraux = maximaGeneraux + maximaPeriod;
                        }
                    }

                    //maxima generaux for periodes/exams

                    bool MaximaGeneralExist = false;
                    //check if subject already exist
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                        string MaximaGeneral = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                        string MaximaGeneralValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                        if (MaximaGeneral.Equals(LocRM.GetString("strMaximaGeneral").ToUpper()) && MaximaGeneralValue == "")
                        {
                            MaximaGeneralExist = true;
                            rowHandle = i;
                            break;
                        }
                        else if (MaximaGeneral == "")
                        {
                            MaximaGeneralExist = false;
                            break;
                        }
                        else
                        {
                            MaximaGeneralExist = false;
                        }
                    }

                    if (!MaximaGeneralExist)
                    {
                        //add new maxima row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();
                        rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                    }

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strMaximaGeneral").ToUpper());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], maximaGeneraux.ToString());

                    pointArrayGlobalVariable[34, columnPeriod] = maximaGeneraux.ToString();

                    maximaGenerauxTotalGlobal = (int)maximaGeneraux; //To calculate pass rate for report
                    maximaGenerauxGlobalVariable = maximaGeneraux.ToString();

                    //update maxima generaux for TOT columns                    
                    if (isExam == true)
                    {

                        if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                        {
                            int intMaxima2P = 0;
                            int intMaxima1P = 0;
                            string stMaxima2P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]);
                            string stMaxima1P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]);
                            bool canConvert2 = int.TryParse(stMaxima2P, out intMaxima2P);
                            bool canConvert1 = int.TryParse(stMaxima1P, out intMaxima1P);

                            maximaGenerauxTotal = maximaGeneraux + intMaxima2P + intMaxima1P;
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaGenerauxTotal.ToString());

                            pointArrayGlobalVariable[34, 9] = maximaGenerauxTotal.ToString(); // maxima generaux
                        }
                        else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                        {
                            int intMaxima4P = 0;
                            int intMaxima3P = 0;
                            string stMaxima4P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()]);
                            string stMaxima3P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]);
                            bool canConvert4 = int.TryParse(stMaxima4P, out intMaxima4P);
                            bool canConvert3 = int.TryParse(stMaxima3P, out intMaxima3P);

                            maximaGenerauxTotal = maximaGeneraux + intMaxima4P + intMaxima3P;

                            //maximaGenerauxTotal = maximaGeneraux + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]));
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], maximaGenerauxTotal.ToString());
                            pointArrayGlobalVariable[34, 10] = maximaGenerauxTotal.ToString(); // maxima generaux

                            if (isSemester == true)  //update Maxima Generaux for TOT Gen column
                            {
                                int intmarkMaximaGeneraux1 = 0;
                                string stmarkMaximaGeneraux1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                bool canConvertTot = int.TryParse(stmarkMaximaGeneraux1, out intmarkMaximaGeneraux1);
                                maximaGenerauxTotGen = maximaGenerauxTotal + intmarkMaximaGeneraux1;

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaGenerauxTotGen.ToString());
                                pointArrayGlobalVariable[34, 12] = maximaGenerauxTotGen.ToString(); // Maxima Generaux Tot general to bulletins

                            }

                        }
                        else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                        {
                            int intMaxima6P = 0;
                            int intMaxima5P = 0;
                            string stMaxima6P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()]);
                            string stMaxima5P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]);
                            bool canConvert6 = int.TryParse(stMaxima6P, out intMaxima6P);
                            bool canConvert5 = int.TryParse(stMaxima5P, out intMaxima5P);

                            maximaGenerauxTotal = maximaGeneraux + intMaxima6P + intMaxima5P;
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], maximaGenerauxTotal.ToString());

                            pointArrayGlobalVariable[34, 11] = maximaGenerauxTotal.ToString(); // maxima generaux

                            //update maxima generaux for TOT Gen column
                            int intmarkMaximaGeneraux1 = 0;
                            int intmarkMaximaGeneraux2 = 0;
                            string stmarkMaximaGeneraux1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                            string stmarkMaximaGeneraux2 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()]);
                            bool canConvertMaximaGeneraux1 = int.TryParse(stmarkMaximaGeneraux1, out intmarkMaximaGeneraux1);
                            bool canConvertMaximaGeneraux2 = int.TryParse(stmarkMaximaGeneraux2, out intmarkMaximaGeneraux2);
                            maximaGenerauxTotGen = maximaGenerauxTotal + intmarkMaximaGeneraux1 + intmarkMaximaGeneraux2;

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaGenerauxTotGen.ToString());
                            pointArrayGlobalVariable[34, 12] = maximaGenerauxTotGen.ToString(); // totaux Tot general
                        }
                        //Get the maxima tot for total columns
                        maximaGenerauxTotalColumnGlobal = maximaGenerauxTotal;
                    }

                    //Add Totaux
                    bool TotalsExist = false;
                    //check if subject already exist
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                        string Totals = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                        string TotalsValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                        if (Totals.Equals(LocRM.GetString("strTotals").ToUpper()) && TotalsValue == "")
                        {
                            TotalsExist = true;
                            rowHandle = i;
                            break;
                        }
                        else if (Totals == "")
                        {
                            TotalsExist = false;
                            break;
                        }
                        else
                        {
                            TotalsExist = false;
                        }
                    }

                    if (!TotalsExist)
                    {
                        //add new maxima row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();
                        rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                    }


                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strTotals").ToUpper());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], totaux.ToString());

                    pointArrayGlobalVariable[35, columnPeriod] = totaux.ToString();

                    totauxGlobalVariable = totaux.ToString();

                    //update Totaux for TOT columns
                    int totauxTotalGen = 0;
                    if (isExam == true)
                    {
                        if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                        {
                            int intmarkTotal1P = 0;
                            int intmarkTotal2P = 0;
                            string stmarkTotal1P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]);
                            string stmarkTotal2P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]);
                            bool canConvert1 = int.TryParse(stmarkTotal1P, out intmarkTotal1P);
                            bool canConvert2 = int.TryParse(stmarkTotal2P, out intmarkTotal2P);
                            totauxTotal = totaux + intmarkTotal1P + intmarkTotal2P;

                            //totauxTotal = totaux + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]));
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], totauxTotal.ToString());

                            pointArrayGlobalVariable[35, 9] = totauxTotal.ToString(); // totaux generaux

                        }
                        else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                        {
                            int intmarkTotal3P = 0;
                            int intmarkTotal4P = 0;
                            string stmarkTotal3P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]);
                            string stmarkTotal4P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()]);
                            bool canConvert3 = int.TryParse(stmarkTotal3P, out intmarkTotal3P);
                            bool canConvert4 = int.TryParse(stmarkTotal4P, out intmarkTotal4P);
                            totauxTotal = totaux + intmarkTotal3P + intmarkTotal4P;

                            // totauxTotal = maximaGeneraux + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]));
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], totauxTotal.ToString());
                            pointArrayGlobalVariable[35, 10] = totauxTotal.ToString(); // totaux generaux

                            if (isSemester == true)  //update Totaux for TOT Gen column
                            {
                                int intmarkTotal1 = 0;
                                string stmarkTotal1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                bool canConvertTot = int.TryParse(stmarkTotal1, out intmarkTotal1);
                                totauxTotalGen = totauxTotal + intmarkTotal1;

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], totauxTotalGen.ToString());
                                pointArrayGlobalVariable[35, 12] = totauxTotalGen.ToString(); // totaux Tot general

                            }
                        }
                        else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                        {
                            int intmarkTotal5P = 0;
                            int intmarkTotal6P = 0;
                            string stmarkTotal5P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]);
                            string stmarkTotal6P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()]);
                            bool canConvert5 = int.TryParse(stmarkTotal5P, out intmarkTotal5P);
                            bool canConvert6 = int.TryParse(stmarkTotal6P, out intmarkTotal6P);
                            totauxTotal = totaux + intmarkTotal5P + intmarkTotal6P;

                            // totauxTotal = maximaGeneraux + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]));
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], totauxTotal.ToString());
                            pointArrayGlobalVariable[35, 11] = totauxTotal.ToString(); // totaux generaux

                            //update Totaux for TOT Gen column
                            int intmarkTotal1 = 0;
                            int intmarkTotal2 = 0;
                            string stmarkTotal1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                            string stmarkTotal2 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()]);
                            bool canConvertTot1 = int.TryParse(stmarkTotal1, out intmarkTotal1);
                            bool canConvertTot2 = int.TryParse(stmarkTotal2, out intmarkTotal2);
                            totauxTotalGen = totauxTotal + intmarkTotal1 + intmarkTotal2;

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], totauxTotalGen.ToString());
                            pointArrayGlobalVariable[35, 12] = totauxTotalGen.ToString(); // totaux Tot general
                        }
                    }


                    //Add Pourcentage
                    if (maximaGeneraux > 0)
                    {
                        studentPercentage = (double)(totaux * 100) / maximaGeneraux;
                        studentPercentage = Math.Round(studentPercentage, 1);
                    }
                    else
                    {
                        studentPercentage = 0;
                    }

                    bool PourcentageExist = false;
                    //check if Pourcentage already exist
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                        string Pourcentage = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                        string PourcentageValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                        if (Pourcentage.Equals(LocRM.GetString("strPercentage").ToUpper()) && PourcentageValue == "")
                        {
                            PourcentageExist = true;
                            rowHandle = i;
                            break;
                        }
                        else if (Pourcentage == "")
                        {
                            PourcentageExist = false;
                            break;
                        }
                        else
                        {
                            PourcentageExist = false;
                        }
                    }

                    if (!PourcentageExist)
                    {
                        //add new maxima row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();
                        rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                    }

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strPercentage").ToUpper());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], studentPercentage.ToString());
                    percentageGlobalVariable = studentPercentage.ToString();

                    pointArrayGlobalVariable[36, columnPeriod] = studentPercentage.ToString();

                    //update Percentage for TOT columns
                    if (isExam == true)
                    {
                        if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                        {
                            if (maximaGenerauxTotal > 0)
                            {
                                studentPercentage = (double)(totauxTotal * 100) / maximaGenerauxTotal;
                                studentPercentage = Math.Round(studentPercentage, 1);

                            }
                            else
                            {
                                studentPercentage = 0;
                            }

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], studentPercentage.ToString());
                            pointArrayGlobalVariable[36, 9] = studentPercentage.ToString(); // Percentage generaux
                        }
                        else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                        {
                            if (maximaGenerauxTotal > 0)
                            {
                                studentPercentage = (double)(totauxTotal * 100) / maximaGenerauxTotal;
                                studentPercentage = Math.Round(studentPercentage, 1);

                            }
                            else
                            {
                                studentPercentage = 0;
                            }

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], studentPercentage.ToString());

                            pointArrayGlobalVariable[36, 10] = studentPercentage.ToString(); // Percentage generaux

                            if (isSemester == true)  //update Percentage for TOT Gen column
                            {
                                if (maximaGenerauxTotGen > 0)
                                {
                                    studentPercentage = (double)(totauxTotalGen * 100) / maximaGenerauxTotGen;
                                    studentPercentage = Math.Round(studentPercentage, 1);

                                }
                                else
                                {
                                    studentPercentage = 0;
                                }

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], studentPercentage.ToString());

                                pointArrayGlobalVariable[36, 12] = studentPercentage.ToString(); // Percentage for TOT Gen column to bulletin

                            }

                        }
                        else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                        {
                            if (maximaGenerauxTotal > 0)
                            {
                                studentPercentage = (double)(totauxTotal * 100) / maximaGenerauxTotal;
                                studentPercentage = Math.Round(studentPercentage, 1);

                            }
                            else
                            {
                                studentPercentage = 0;
                            }


                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], studentPercentage.ToString());
                            pointArrayGlobalVariable[36, 11] = studentPercentage.ToString(); // Percentage for TOT Gen column to bulletin

                            //update Percentage for TOT Gen column
                            if (maximaGenerauxTotGen > 0)
                            {
                                studentPercentage = (double)(totauxTotalGen * 100) / maximaGenerauxTotGen;
                                studentPercentage = Math.Round(studentPercentage, 1);

                            }
                            else
                            {
                                studentPercentage = 0;
                            }

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], studentPercentage.ToString());
                            pointArrayGlobalVariable[36, 12] = studentPercentage.ToString(); // Percentage for TOT Gen column to bulletin

                        }
                    }

                    con.Close();
                }

                //Get students ranking for periods and exams

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    cmd = new SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY SUM (MarksObtained) desc) as RowNo, StudentNumber, " +
                        "SchoolYear, Class,AssessmentPeriod, SUM (MarksObtained) as Marks  from MarksEntry " +
                        "where SchoolYear = @d1 and Class = @d2 and AssessmentPeriod = @d3 " +
                        "group by StudentNumber,SchoolYear,Class,AssessmentPeriod", con);

                    cmd.Parameters.Add("@d1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                    cmd.Parameters.Add("@d2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                    cmd.Parameters.Add("@d3", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = cmbAssessmentPeriod.Text.Trim();

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    string studentRank = "";
                    double studentPercentageRank = 0;

                    foreach (DataRow drow in dtable.Rows)
                    {
                        //
                        if (maximaGenerauxTotalGlobal > 0)
                        {

                            studentPercentageRank = (double)((int)drow[5] * 100) / maximaGenerauxTotalGlobal;
                            studentPercentageRank = Math.Round(studentPercentageRank, 1);
                            if (studentPercentageRank >= 50)
                            {
                                passed++;
                            }
                            totalPointsGlobalVariable = totalPointsGlobalVariable + (int)drow[5];
                        }

                        if (drow[1].ToString() == cmbStudentNo.Text.Trim())
                        {
                            studentRank = drow[0].ToString(); //drow[0] = rank, drow[1] = student No, drow[5] tot marks
                        }
                    }
                    //calculate total avarage percentage of class
                    if ((noStudents > 0) && (maximaGenerauxTotalGlobal > 0))
                    {
                        double avarage = (double)totalPointsGlobalVariable / noStudents;
                        avarage = (avarage * 100) / maximaGenerauxTotalGlobal;
                        avarage = Math.Round(avarage, 1);

                        totalAvaragePercClassGlobalVariable = avarage.ToString();
                    }
                    
                    //Add student ranking  
                    int rowHandle = 0;
                    if (studentRank != "")
                    {
                        bool studentRankingExist = false;
                        //check if Pourcentage already exist
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                            string studentRanking = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                            string studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                            if (studentRanking.Equals(LocRM.GetString("strPlaceNoStudents").ToUpper()) && studentRankingValue == "")
                            {
                                studentRankingExist = true;
                                rowHandle = i;
                                break;
                            }
                            else if (studentRanking == "")
                            {
                                studentRankingExist = false;
                                break;
                            }
                            else
                            {
                                studentRankingExist = false;
                            }
                        }

                        if (!studentRankingExist)
                        {
                            //add new maxima row
                            gridView1.AddNewRow();
                            gridView1.UpdateCurrentRow();
                            rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                        }

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strPlaceNoStudents").ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], studentRank + " /" + noStudents.ToString());
                        placeGlobalVariable = studentRank + " /" + noStudents.ToString();

                        pointArrayGlobalVariable[37, columnPeriod] = studentRank + " /" + noStudents.ToString();
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

                //update Students rankings for TOT columns
                if (isExam == true)
                {
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();

                        cmd = new SqlCommand("" +
                            "SELECT ROW_NUMBER() OVER(ORDER BY (P1.TotP1 + P2.TotP2 + Exam.TotExam) desc) " +
                            "as Place, P1.StudentNumber, P1.TotP1, P2.TotP2,Exam.TotExam,(P1.TotP1 + P2.TotP2 + Exam.TotExam) as tot " +
                            "from (" +
                            "(select StudentNumber, SchoolYear, Class,AssessmentPeriod, SUM (MarksObtained) AS TotP1 " +
                            "FROM MarksEntry " +
                            "where (SchoolYear = @d1P1 and Class = @d2P1 and AssessmentPeriod = @d3P1) " +
                            "GROUP BY StudentNumber,SchoolYear,Class,AssessmentPeriod) as P1 " +
                            "JOIN " +
                            "(select StudentNumber, SchoolYear, Class,AssessmentPeriod, SUM (MarksObtained) AS TotP2 " +
                            "FROM MarksEntry " +
                            "where (SchoolYear = @d1P2 and Class = @d2P2 and AssessmentPeriod = @d3P2) " +
                            "GROUP BY StudentNumber,SchoolYear,Class,AssessmentPeriod) as P2 " +
                            "ON P1.StudentNumber = P2.StudentNumber " +
                            "JOIN " +
                            "(select StudentNumber, SchoolYear, Class,AssessmentPeriod, SUM (MarksObtained) AS TotExam " +
                            "FROM MarksEntry " +
                            "where (SchoolYear = @d1Exam and Class = @d2Exam and AssessmentPeriod = @d3Exam) " +
                            "GROUP BY StudentNumber,SchoolYear,Class,AssessmentPeriod) as Exam " +
                            "ON P1.StudentNumber = Exam.StudentNumber )", con);

                        if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                        {
                            cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str1eP");

                            cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str2eP");

                            cmd.Parameters.Add("@d1Exam", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2Exam", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3Exam", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam1");
                        }

                        else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                        {
                            cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str3eP");

                            cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str4eP");

                            cmd.Parameters.Add("@d1Exam", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2Exam", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3Exam", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam2");
                        }

                        else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                        {
                            cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str5eP");

                            cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str6eP");

                            cmd.Parameters.Add("@d1Exam", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                            cmd.Parameters.Add("@d2Exam", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                            cmd.Parameters.Add("@d3Exam", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam3");
                        }


                        SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                        DataSet myDataSet = new DataSet("ds");
                        DataTable dtable;
                        myDA.Fill(myDataSet);
                        dtable = myDataSet.Tables[0];
                        string studentRank = "";
                        int totalPointsTotalColumnGlobalVariable = 0;
                        foreach (DataRow drow in dtable.Rows)
                        {
                            //
                            if (maximaGenerauxTotalColumnGlobal > 0)
                            {
                                studentPercentageTotalColumnGlobal = (double)((int)drow[5] * 100) / maximaGenerauxTotalColumnGlobal;
                                studentPercentageTotalColumnGlobal = Math.Round(studentPercentageTotalColumnGlobal, 1);
                                if (studentPercentageTotalColumnGlobal >= 50)
                                {
                                    passedTotalColumnGlobal++;
                                }
                                totalPointsTotalColumnGlobalVariable = totalPointsTotalColumnGlobalVariable + (int)drow[5];
                            }

                            if (drow[1].ToString() == cmbStudentNo.Text.Trim())
                            {
                                studentRank = drow[0].ToString(); //drow[0] = rank, drow[1] = student No, drow[5] tot marks
                            }
                        }
                        //calculate total avarage percetage of class
                        double avarage = (double)totalPointsTotalColumnGlobalVariable / noStudents;
                        avarage = (avarage * 100) / maximaGenerauxTotalColumnGlobal;
                        avarage = Math.Round(avarage, 1);

                        totalAvaragePerClassSemTrimGlobalVariable = avarage.ToString();  //Total avarage percentage per semester/Class per class
                        //Add student ranking  
                        int rowHandle = 0;
                        if (studentRank != "")
                        {
                            bool studentRankingExist = false;
                            string actualTot = "";
                            //check if Pourcentage already exist
                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                string studentRankingValue = "";

                                int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                string studentRanking = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                                {
                                    studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                    actualTot = LocRM.GetString("strTotal1").ToUpper();
                                }
                                else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                                {
                                    studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()]);
                                    actualTot = LocRM.GetString("strTotal2").ToUpper();
                                }
                                else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                                {
                                    studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()]);
                                    actualTot = LocRM.GetString("strTotal3").ToUpper();
                                }

                                if (studentRanking.Equals(LocRM.GetString("strPlaceNoStudents").ToUpper()) && studentRankingValue == "")
                                {
                                    studentRankingExist = true;
                                    rowHandle = i;
                                    break;
                                }
                                else if (studentRanking == "")
                                {
                                    studentRankingExist = false;
                                    break;
                                }
                                else
                                {
                                    studentRankingExist = false;
                                }
                            }

                            if (!studentRankingExist)
                            {
                                //add new maxima row
                                gridView1.AddNewRow();
                                gridView1.UpdateCurrentRow();
                                rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                            }

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strPlaceNoStudents").ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[actualTot], studentRank + " /" + noStudents.ToString());
                            // placeGlobalVariable = studentRank + " /" + noStudents.ToString();

                            if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam1"))
                            {
                                //Save student rank in global array to update bulletin
                                pointArrayGlobalVariable[37, 9] = studentRank + " /" + noStudents.ToString();
                            }
                            else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2"))
                            {
                                //Save student rank in global array to update bulletin
                                pointArrayGlobalVariable[37, 10] = studentRank + " /" + noStudents.ToString();
                            }
                            else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                            {
                                //Save student rank in global array to update bulletin
                                pointArrayGlobalVariable[37, 11] = studentRank + " /" + noStudents.ToString();
                            }

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

                //update Students rankings for TOT Gen column
                if (isExam == true)
                {
                    if (((isSemester == true) && (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2")))
                        || ((isSemester == false) && (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))))
                    {
                        using (con = new SqlConnection(databaseConnectionString))
                        {
                            con.Open();

                            if (isSemester == true)  //Get Students rankings for TOT Gen column for semester section
                            {
                                cmd = new SqlCommand(" SELECT ROW_NUMBER() OVER(ORDER BY " +
                            "(P1.TotP1 + P2.TotP2 + Exam1.TotExam1 + P3.TotP3 + P4.TotP4  + Exam2.TotExam2) desc) " +
                            "as Place, P1.StudentNumber, P1.TotP1, P2.TotP2, Exam1.TotExam1, P3.TotP3, P4.TotP4, Exam2.TotExam2, " +
                            "(P1.TotP1 + P2.TotP2 + Exam1.TotExam1 + P3.TotP3 + P4.TotP4 + Exam2.TotExam2) as tot " +
                            "from( " +
                            "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP1 " +
                            "FROM MarksEntry " +
                            "where(SchoolYear = @d1P1 and Class = @d2P1 and AssessmentPeriod = @d3P1) " +
                            "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P1 " +
                            "JOIN " +
                            "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP2 " +
                            "FROM MarksEntry " +
                            "where(SchoolYear = @d1P2 and Class = @d2P2 and AssessmentPeriod = @d3P2) " +
                            "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P2 " +
                            "ON P1.StudentNumber = P2.StudentNumber " +
                            "JOIN " +
                            "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam1 " +
                            "FROM MarksEntry " +
                            "where(SchoolYear = @d1P3 and Class = @d2P3 and AssessmentPeriod = @d3P3) " +
                            "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam1 " +
                            "ON P1.StudentNumber = Exam1.StudentNumber " +
                            "" +
                            "JOIN " +
                            "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP3 " +
                            "FROM MarksEntry " +
                            "where (SchoolYear = @d1P4 and Class = @d2P4 and AssessmentPeriod = @d3P4) " +
                            "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P3 " +
                            "ON P1.StudentNumber = P3.StudentNumber " +
                            "JOIN " +
                            "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP4 " +
                            "FROM MarksEntry " +
                            "where (SchoolYear = @d1P5 and Class = @d2P5 and AssessmentPeriod = @d3P5) " +
                            "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P4 " +
                            "ON P1.StudentNumber = P4.StudentNumber " +
                            "JOIN " +
                            "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam2 " +
                            "FROM MarksEntry " +
                            "where (SchoolYear = @d1P6 and Class = @d2P6 and AssessmentPeriod = @d3P6) " +
                            "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam2 " +
                            "ON P1.StudentNumber = Exam2.StudentNumber )", con);


                                cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str1eP");

                                cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str2eP");

                                cmd.Parameters.Add("@d1P3", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P3", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P3", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam1");

                                cmd.Parameters.Add("@d1P4", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P4", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P4", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str3eP");

                                cmd.Parameters.Add("@d1P5", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P5", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P5", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str4eP");

                                cmd.Parameters.Add("@d1P6", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P6", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P6", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam2");

                            }
                            else
                            {
                                cmd = new SqlCommand("" +
                                    "SELECT ROW_NUMBER() OVER(ORDER BY " +
                                    "(P1.TotP1 + P2.TotP2 + Exam1.TotExam1 + P3.TotP3 + P4.TotP4  + Exam2.TotExam2 + P5.TotP5 + P6.TotP6  + Exam3.TotExam3) desc) " +
                                    "as Place, P1.StudentNumber, P1.TotP1, P2.TotP2, Exam1.TotExam1, P3.TotP3, P4.TotP4, Exam2.TotExam2, P5.TotP5, P6.TotP6, Exam3.TotExam3, " +
                                    "(P1.TotP1 + P2.TotP2 + Exam1.TotExam1 + P3.TotP3 + P4.TotP4 + Exam2.TotExam2 + P5.TotP5 + P6.TotP6 + Exam3.TotExam3) as tot " +
                                    "from( " +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP1 " +
                                    "FROM MarksEntry " +
                                    "where (SchoolYear = @d1P1 and Class = @d2P1 and AssessmentPeriod = @d3P1) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P1 " +
                                    "JOIN " +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP2 " +
                                    "FROM MarksEntry " +
                                    "where(SchoolYear = @d1P2 and Class = @d2P2 and AssessmentPeriod = @d3P2) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P2 " +
                                    "ON P1.StudentNumber = P2.StudentNumber " +
                                    "JOIN " +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam1 " +
                                    "FROM MarksEntry " +
                                    "where(SchoolYear = @d1P3 and Class = @d2P3 and AssessmentPeriod = @d3P3) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam1 " +
                                    "ON P1.StudentNumber = Exam1.StudentNumber " +
                                    "" +
                                    "JOIN " +
                                    "" +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP3 " +
                                    "FROM MarksEntry " +
                                    "where(SchoolYear = @d1P4 and Class = @d2P4 and AssessmentPeriod = @d3P4) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P3 " +
                                    "ON P1.StudentNumber = P3.StudentNumber " +
                                    "JOIN " +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP4 " +
                                    "FROM MarksEntry " +
                                    "where(SchoolYear = @d1P5 and Class = @d2P5 and AssessmentPeriod = @d3P5) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P4 " +
                                    "ON P1.StudentNumber = P4.StudentNumber " +
                                    "JOIN " +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam2 " +
                                    "FROM MarksEntry " +
                                    "where(SchoolYear = @d1P6 and Class = @d2P6 and AssessmentPeriod = @d3P6) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam2 " +
                                    "ON P1.StudentNumber = Exam2.StudentNumber " +
                                    "" +
                                    "JOIN " +
                                    "" +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP5 " +
                                    "FROM MarksEntry " +
                                    "where(SchoolYear = @d1P7 and Class = @d2P6 and AssessmentPeriod = @d3P7) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P5 " +
                                    "ON P1.StudentNumber = P5.StudentNumber " +
                                    "JOIN " +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP6 " +
                                    "FROM MarksEntry " +
                                    "where(SchoolYear = @d1P8 and Class = @d2P8 and AssessmentPeriod = @d3P8) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P6 " +
                                    "ON P1.StudentNumber = P6.StudentNumber " +
                                    "JOIN " +
                                    "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam3 " +
                                    "FROM MarksEntry " +
                                    "where(SchoolYear = @d1P9 and Class = @d2P9 and AssessmentPeriod = @d3P9) " +
                                    "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam3 " +
                                    "ON P1.StudentNumber = Exam3.StudentNumber " +
                                    " ) ", con);


                                cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str1eP");

                                cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str2eP");

                                cmd.Parameters.Add("@d1P3", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P3", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P3", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam1");

                                cmd.Parameters.Add("@d1P4", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P4", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P4", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str3eP");

                                cmd.Parameters.Add("@d1P5", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P5", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P5", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str4eP");

                                cmd.Parameters.Add("@d1P6", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P6", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P6", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam2");

                                cmd.Parameters.Add("@d1P7", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P7", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P7", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str5eP");

                                cmd.Parameters.Add("@d1P8", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P8", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P8", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str6eP");

                                cmd.Parameters.Add("@d1P9", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P9", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P9", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam3");

                            }


                            SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                            DataSet myDataSet = new DataSet("ds");
                            DataTable dtable;
                            myDA.Fill(myDataSet);
                            dtable = myDataSet.Tables[0];
                            string studentRank = "";
                            int totalPointsTotalGenColumnGlobalVariable = 0;
                            double studentPercentageTotalGenColumnGlobal = 0;
                            foreach (DataRow drow in dtable.Rows)
                            {
                                //
                                if (maximaGenerauxTotGen > 0)
                                {
                                    studentPercentageTotalGenColumnGlobal = (double)((int)drow[8] * 100) / maximaGenerauxTotGen;
                                    studentPercentageTotalGenColumnGlobal = Math.Round(studentPercentageTotalGenColumnGlobal, 1);
                                    if (studentPercentageTotalGenColumnGlobal >= 50)
                                    {
                                        passedTotalGenColumnGlobal++;
                                    }
                                    totalPointsTotalGenColumnGlobalVariable = totalPointsTotalGenColumnGlobalVariable + (int)drow[8];
                                }

                                if (drow[1].ToString() == cmbStudentNo.Text.Trim())
                                {
                                    studentRank = drow[0].ToString(); //drow[0] = rank, drow[1] = student No, drow[5] tot marks
                                }
                            }
                            //calculate total avarage percetage of class
                            double avarage = (double)totalPointsTotalGenColumnGlobalVariable / noStudents;
                            avarage = (avarage * 100) / maximaGenerauxTotGen;
                            avarage = Math.Round(avarage, 1);

                            totalAvaragePerClassTotGenGlobalVariable = avarage.ToString();  //Total avarage percentage per semester/Class per class
                                                                                            //Add student ranking  
                            int rowHandle = 0;
                            if (studentRank != "")
                            {
                                bool studentRankingExist = false;
                                // string actualTot = "";
                                //check if Pourcentage already exist
                                for (int i = 0; i < gridView1.DataRowCount; i++)
                                {
                                    string studentRankingValue = "";

                                    int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                    string studentRanking = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);

                                    if ((cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam2")) && (isSemester == true))
                                    {
                                        studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()]);

                                    }
                                    else if (cmbAssessmentPeriod.Text.Trim() == LocRM.GetString("strExam3"))
                                    {
                                        studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()]);
                                    }

                                    if (studentRanking.Equals(LocRM.GetString("strPlaceNoStudents").ToUpper()) && studentRankingValue == "")
                                    {
                                        studentRankingExist = true;
                                        rowHandle = i;
                                        break;
                                    }
                                    else if (studentRanking == "")
                                    {
                                        studentRankingExist = false;
                                        break;
                                    }
                                    else
                                    {
                                        studentRankingExist = false;
                                    }
                                }

                                if (!studentRankingExist)
                                {
                                    //add new maxima row
                                    gridView1.AddNewRow();
                                    gridView1.UpdateCurrentRow();
                                    rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                                }

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strPlaceNoStudents").ToUpper());
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], studentRank + " /" + noStudents.ToString());

                                //Save student rank in global array to update bulletin
                                pointArrayGlobalVariable[37, 12] = studentRank + " /" + noStudents.ToString();
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

                //Display application only on travail journaliers
                if (isExam == false)
                {
                    double studentPercentageApplication = studentPercentage;
                    studentPercentageApplication = Math.Round(studentPercentageApplication, 0);
                    //Get Application
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();

                        string ct = "SELECT Abbreviation, Minimum, Maximum  FROM Application where  @d1 BETWEEN Minimum AND Maximum ";
                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        cmd.Parameters.Add("@d1", SqlDbType.Decimal).Value = studentPercentageApplication;
                        //cmd.Parameters.Add("@d2", SqlDbType.Decimal, 100, " Maximum").Value = studentPercentage;

                        rdr = cmd.ExecuteReader();
                        string sStudentPercentage = "";
                        if (rdr.Read())
                        {
                            sStudentPercentage = rdr.GetValue(0).ToString();
                        }

                        //Add Application  
                        int rowHandle = 0;
                        if (sStudentPercentage != "")
                        {
                            bool ApplicationExist = false;
                            //check if Pourcentage already exist
                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                string Application = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                string ApplicationValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                                if (Application.Equals(LocRM.GetString("strApplication").ToUpper()) && ApplicationValue == "")
                                {
                                    ApplicationExist = true;
                                    rowHandle = i;
                                    break;
                                }
                                else if (Application == "")
                                {
                                    ApplicationExist = false;
                                    break;
                                }
                                else
                                {
                                    ApplicationExist = false;
                                }
                            }

                            if (!ApplicationExist)
                            {
                                //add new maxima row
                                gridView1.AddNewRow();
                                gridView1.UpdateCurrentRow();
                                rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                            }

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strApplication").ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], sStudentPercentage);
                            applicationGlobalVariable = sStudentPercentage;

                            pointArrayGlobalVariable[38, columnPeriod] = sStudentPercentage;
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
                //Display Conduite only on travail journaliers
                if (isExam == false)
                {
                    //Get Conduite
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();

                        string ct = "SELECT Conduct  FROM ConductsEntry " +
                            "where  SchoolYear = @d1 and Class = @d2 and StudentNumber = @d3 and AssessmentPeriod = @d4 ";
                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        cmd.Parameters.Add("@d1", SqlDbType.NVarChar, 60, " Maximum").Value = cmbSchoolYear.Text;
                        cmd.Parameters.Add("@d2", SqlDbType.NVarChar, 100, " Maximum").Value = cmbClass.Text;
                        cmd.Parameters.Add("@d3", SqlDbType.NVarChar, 60, " Maximum").Value = cmbStudentNo.Text;
                        cmd.Parameters.Add("@d4", SqlDbType.NVarChar, 60, " Maximum").Value = cmbAssessmentPeriod.Text;

                        rdr = cmd.ExecuteReader();
                        string conduite = "";
                        if (rdr.Read())
                        {
                            conduite = rdr.GetValue(0).ToString();
                        }

                        //Add Application  
                        int rowHandle = 0;
                        if (conduite != "")
                        {
                            bool conduiteExist = false;
                            //check if Pourcentage already exist
                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                string readConduite = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                string ConduiteValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                                if (readConduite.Equals(LocRM.GetString("strConduct").ToUpper()) && ConduiteValue == "")
                                {
                                    conduiteExist = true;
                                    rowHandle = i;
                                    break;
                                }
                                else if (readConduite == "")
                                {
                                    conduiteExist = false;
                                    break;
                                }
                                else
                                {
                                    conduiteExist = false;
                                }
                            }

                            if (!conduiteExist)
                            {
                                //add new maxima row
                                gridView1.AddNewRow();
                                gridView1.UpdateCurrentRow();
                                rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                            }

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strConduct").ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], conduite);
                            conduiteGlobalVariable = conduite;
                            pointArrayGlobalVariable[39, columnPeriod] = conduite;
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

                numberStudentsGlobalVariable = noStudents;

                SchoolYear = cmbSchoolYear.Text;
                assessmentPeriodGlobalVariable = cmbAssessmentPeriod.Text;

                className = cmbClass.Text;
                passedGlobalVariable = passed;

                studentNumber = cmbStudentNo.Text.Trim();

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
                Cursor = Cursors.Default;
                timer1.Enabled = false;
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadRangeAssessmentReport()
        {
            numberStudents = 0;
            passed = 0;
            int maximaGenerauxTotalGlobal = 0;
            int columnPeriod = 0;
            // int TotauxTotalGlobal = 0;
            int maximaGenerauxTotalColumnGlobal = 0;
            double studentPercentageTotalColumnGlobal = 0;
            int passedTotalColumnGlobal = 0; //Number of passed students for total columns
            int passedTotalGenColumnGlobal = 0; //Number of passed students for total gen column
            int maximaGenerauxTotGen = 0;  // Maxima generaux for Tot Gen          


            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            try
            {
                //set gridview column header appearance: Appearance, headerPanel, font
                double studentPercentage = 0;
                bool isExam = false;
                string AssessmentPeriod = "";

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strCalculatingSchoolMarks"));
                }

                PeriodStart = cmbFromPeriod.Text.Trim();
                for (int FromPeriodIndex = cmbFromPeriod.SelectedIndex; FromPeriodIndex <= cmbToPeriod.SelectedIndex; FromPeriodIndex++)
                {
                    cmbFromPeriod.SelectedIndex = FromPeriodIndex;
                    AssessmentPeriod = cmbFromPeriod.Text.Trim();

                    //check if exam or trav jour
                    if ((AssessmentPeriod == LocRM.GetString("strExam1"))
                                    || (AssessmentPeriod == LocRM.GetString("strExam2"))
                                    || (AssessmentPeriod == LocRM.GetString("strExam3")))
                    {
                        isExam = true;
                    }
                    else
                    {
                        isExam = false;
                    }


                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();

                        string ct = "SELECT MarksEntry.StudentNumber, MarksEntry.SchoolYear,MarksEntry.Class,MarksEntry.SubjectCode," +
                            "MarksEntry.AssessmentPeriod,SubjectMaxima,MarksObtained,Subject.SubjectName,SubjectAssignment.SubjectPositionBulletin " +
                            "from MarksEntry,Subject,SubjectAssignment where MarksEntry.SubjectCode = Subject.SubjectCode " +
                            "and MarksEntry.SubjectCode = SubjectAssignment.SubjectCode and MarksEntry.SchoolYear= @d1 and MarksEntry.Class= @d2 and " +
                            "MarksEntry.StudentNumber= @d3 and MarksEntry.AssessmentPeriod = @d4  order by SubjectPositionBulletin";

                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@d1", cmbSchoolYear.Text.Trim());
                        cmd.Parameters.AddWithValue("@d2", cmbClass.Text.Trim());
                        cmd.Parameters.AddWithValue("@d3", cmbStudentNo.Text.Trim());
                        cmd.Parameters.AddWithValue("@d4", AssessmentPeriod);

                        rdr = cmd.ExecuteReader();
                        maximaPeriod = 0;
                        maximaExam = 0;
                        maximaTot = 0;
                        maximaTotGenSemester = 0;
                        maximaTotGenTrimester = 0;
                        maximaGeneraux = 0;
                        totaux = 0;

                        int maximaOld = 0;
                        int rowHandle = 0;
                        int rowCount = 0;
                        int markTotal = 0;
                        int markTotalGen = 0;
                        int maximaGenerauxTotal = 0;
                        int totauxTotal = 0;
                        // string[,] pointArrays = new string [30,9];
                        int subjectPosition = 0;
                        // columnPeriod = 0;

                        while ((rdr.Read() == true))
                        {

                            //count row 
                            rowCount = gridView1.RowCount;

                            //maxima. Check if maxima is zero, add maxima row.
                            //chech if maxima is different to old maxima, add new row, assign this new maxima.
                            if (maximaPeriod == 0)
                            {
                                maximaPeriod = Convert.ToInt16(rdr.GetValue(5));
                                if (isExam == true)
                                {
                                    maximaPeriod = maximaPeriod / 2;
                                    maximaExam = maximaPeriod * 2;
                                    maximaOld = maximaExam;
                                    maximaTot = maximaPeriod * 4;
                                    maximaTotGenSemester = maximaPeriod * 8;
                                    maximaTotGenTrimester = maximaPeriod * 12;
                                }
                                else
                                {
                                    maximaOld = maximaPeriod;
                                    maximaExam = maximaPeriod * 2;
                                    maximaTot = maximaPeriod * 4;
                                    maximaTotGenSemester = maximaPeriod * 8;
                                    maximaTotGenTrimester = maximaPeriod * 12;
                                }

                                int intMaximValue = 0;
                                bool MaximaExist = false;
                                if (rowCount > 0)
                                {

                                    //check if subject already exist
                                    for (int i = 0; i < gridView1.DataRowCount; i++)
                                    {
                                        int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                        string Maxima = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                        string MaximValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                                        if (MaximValue != "")
                                        {
                                            intMaximValue = Convert.ToInt16(MaximValue);
                                        }
                                        //if ((Maxima != LocRM.GetString("strMaxima").ToUpper()) && MaximValue == "")
                                        //{
                                        //    MaximaExist = false;
                                        //    break;
                                        //}
                                        if ((Maxima == LocRM.GetString("strMaxima").ToUpper()) && MaximValue == "")
                                        {
                                            MaximaExist = false;
                                            break;
                                        }
                                        else if ((Maxima == LocRM.GetString("strMaxima").ToUpper()) && (intMaximValue == maximaOld))
                                        {
                                            MaximaExist = true;
                                            rowHandle = i;
                                            break;
                                        }
                                        else
                                        {
                                            MaximaExist = false;
                                        }
                                    }
                                }

                                if (!MaximaExist)
                                {
                                    //add new maxima row
                                    gridView1.AddNewRow();
                                    gridView1.UpdateCurrentRow();
                                    rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);


                                    //maxima 1eP
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strMaxima").ToUpper());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima 2eP                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima Exam1                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam1").ToUpper()], maximaExam.ToString());
                                    //maxima TOT1                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaTot.ToString());
                                    //maxima 3eP                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima 4eP                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()], maximaPeriod.ToString());
                                    //maxima Exam2                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam2").ToUpper()], maximaExam.ToString());
                                    //maxima TOT2                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], maximaTot.ToString());
                                    //maxima GrandTotal                            
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenSemester.ToString());

                                    if (cmbCycle.SelectedIndex < 2)
                                    {
                                        //maxima 5eP                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()], maximaPeriod.ToString());
                                        //maxima 6eP                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()], maximaPeriod.ToString());
                                        //maxima Exam3                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam3").ToUpper()], maximaExam.ToString());
                                        //maxima TOT3                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], maximaTot.ToString());
                                        //maxima GrandTotal                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenTrimester.ToString());
                                    }
                                }
                                // maximaGeneraux = maximaGeneraux + maximaPeriod;
                            }
                            else
                            {
                                maximaPeriod = Convert.ToInt16(rdr.GetValue(5));
                                if (maximaPeriod != maximaOld)
                                {

                                    if (isExam == true)
                                    {
                                        maximaPeriod = maximaPeriod / 2;
                                        maximaExam = maximaPeriod * 2;
                                        maximaOld = maximaExam;
                                        maximaTot = maximaPeriod * 4;
                                        maximaTotGenSemester = maximaPeriod * 8;
                                        maximaTotGenTrimester = maximaPeriod * 12;

                                    }
                                    else
                                    {
                                        maximaOld = maximaPeriod;
                                        maximaExam = maximaPeriod * 2;
                                        maximaTot = maximaPeriod * 4;
                                        maximaTotGenSemester = maximaPeriod * 8;
                                        maximaTotGenTrimester = maximaPeriod * 12;
                                    }

                                    int intMaximValue = 0;
                                    bool MaximaExist = false;
                                    //string MaximaDatabase = rdr[5].ToString();
                                    //check if subject already exist
                                    for (int i = 0; i < gridView1.DataRowCount; i++)
                                    {
                                        int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                        string Maxima = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                        string MaximValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                                        if (MaximValue != "")
                                        {
                                            intMaximValue = Convert.ToInt16(MaximValue);
                                        }
                                        //if ((Maxima != LocRM.GetString("strMaxima").ToUpper()) && MaximValue == "")
                                        //{
                                        //    MaximaExist = false;
                                        //    break;
                                        //}
                                        if ((Maxima == LocRM.GetString("strMaxima").ToUpper()) && MaximValue == "")
                                        {
                                            MaximaExist = false;
                                            break;
                                        }
                                        else if ((Maxima == LocRM.GetString("strMaxima").ToUpper()) && (intMaximValue == maximaOld))
                                        {
                                            MaximaExist = true;
                                            rowHandle = i;
                                            break;
                                        }
                                        else
                                        {
                                            MaximaExist = false;
                                        }
                                    }

                                    if (!MaximaExist)
                                    {
                                        //add new maxima row
                                        gridView1.AddNewRow();
                                        gridView1.UpdateCurrentRow();
                                        rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);


                                        //maxima 1eP
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strMaxima").ToUpper());
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()], maximaPeriod.ToString());
                                        //maxima 2eP                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()], maximaPeriod.ToString());
                                        //maxima Exam1                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam1").ToUpper()], maximaExam.ToString());
                                        //maxima TOT1                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaTot.ToString());
                                        //maxima 3eP                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()], maximaPeriod.ToString());
                                        //maxima 4eP                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()], maximaPeriod.ToString());
                                        //maxima Exam2                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam2").ToUpper()], maximaExam.ToString());
                                        //maxima TOT2                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], maximaTot.ToString());
                                        //maxima GrandTotal                            
                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenSemester.ToString());

                                        if (cmbCycle.SelectedIndex < 2)
                                        {
                                            //maxima 5eP                            
                                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()], maximaPeriod.ToString());
                                            //maxima 6eP                            
                                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()], maximaPeriod.ToString());
                                            //maxima Exam3                            
                                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam3").ToUpper()], maximaExam.ToString());
                                            //maxima TOT3                            
                                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], maximaTot.ToString());
                                            //maxima GrandTotal                            
                                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaTotGenTrimester.ToString());
                                        }
                                    }
                                    
                                }
                            }
                            
                            bool SubjectExist = false;
                            //check if subject already exist
                            string subjectDatabase = rdr[7].ToString();
                            subjectPosition = (int)rdr.GetValue(8);
                            // columnPeriod = 0;
                            if (AssessmentPeriod.ToUpper() == LocRM.GetString("str1eP").ToUpper())
                            {
                                columnPeriod = 0;
                            }
                            else if (AssessmentPeriod.ToUpper() == LocRM.GetString("str2eP").ToUpper())
                            {
                                columnPeriod = 1;
                            }
                            else if (AssessmentPeriod.ToUpper() == LocRM.GetString("strExam1").ToUpper())
                            {
                                columnPeriod = 2;
                            }
                            else if (AssessmentPeriod.ToUpper() == LocRM.GetString("str3eP").ToUpper())
                            {
                                columnPeriod = 3;
                            }
                            else if (AssessmentPeriod.ToUpper() == LocRM.GetString("str4eP").ToUpper())
                            {
                                columnPeriod = 4;
                            }
                            else if (AssessmentPeriod.ToUpper() == LocRM.GetString("strExam2").ToUpper())
                            {
                                columnPeriod = 5;
                            }
                            else if (AssessmentPeriod.ToUpper() == LocRM.GetString("str5eP").ToUpper())
                            {
                                columnPeriod = 6;
                            }
                            else if (AssessmentPeriod.ToUpper() == LocRM.GetString("str6eP").ToUpper())
                            {
                                columnPeriod = 7;
                            }
                            else if (AssessmentPeriod.ToUpper() == LocRM.GetString("strExam3").ToUpper())
                            {
                                columnPeriod = 8;
                            }
                            pointArrayGlobalVariable[subjectPosition, columnPeriod] = rdr.GetValue(6).ToString();
                            // pointArrays[subjectPosition, columnPeriod] = rdr.GetValue(6).ToString();

                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                string subject = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                string subjectValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                                if ((subject == subjectDatabase) && (subjectValue == ""))
                                {
                                    SubjectExist = true;
                                    rowHandle = i;
                                    break;
                                }
                                else if (subject == "")
                                {
                                    SubjectExist = false;
                                    break;
                                }
                                else
                                {
                                    SubjectExist = false;
                                }
                            }

                            if (!SubjectExist)
                            {
                                //add new maxima row
                                gridView1.AddNewRow();
                                gridView1.UpdateCurrentRow();
                                rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                            }


                            //evaluation period"
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], rdr[7].ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()], rdr.GetValue(6).ToString());

                            //Update Totaux for all subjects
                            totaux = totaux + Convert.ToInt16(rdr.GetValue(6));
                            if (isExam == true)
                            {
                                //Update Maxima generaux
                                maximaGeneraux = maximaGeneraux + maximaExam;
                                int subjectTotalGen = 0;
                                //update tot column
                                if (AssessmentPeriod == LocRM.GetString("strExam1"))
                                {
                                    // int markTotal = 0;
                                    //int maximaGenerauxTotal = 0;
                                    //int totauxTotal = 0;
                                    //int markTotalGen = 0;  gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()]);
                                    int intmarkTotal1P = 0;
                                    int intmarkTotal2P = 0;
                                    string stmarkTotal1P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]);
                                    string stmarkTotal2P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]);
                                    bool canConvert1 = int.TryParse(stmarkTotal1P, out intmarkTotal1P);
                                    bool canConvert2 = int.TryParse(stmarkTotal2P, out intmarkTotal2P);
                                    markTotal = Convert.ToInt16(rdr.GetValue(6)) + intmarkTotal1P + intmarkTotal2P;

                                    // markTotal =Convert.ToInt16( rdr.GetValue(6)) + Convert.ToInt32( gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]));
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], markTotal.ToString());
                                    pointArrayGlobalVariable[subjectPosition, 9] = markTotal.ToString(); //strTotal1 subject
                                }
                                else if (AssessmentPeriod == LocRM.GetString("strExam2"))
                                {
                                    int intmarkTotal3P = 0;
                                    int intmarkTotal4P = 0;
                                    string stmarkTotal3P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]);
                                    string stmarkTotal4P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()]);
                                    bool canConvert3 = int.TryParse(stmarkTotal3P, out intmarkTotal3P);
                                    bool canConvert4 = int.TryParse(stmarkTotal4P, out intmarkTotal4P);
                                    markTotal = Convert.ToInt16(rdr.GetValue(6)) + intmarkTotal3P + intmarkTotal4P;

                                    //markTotal = Convert.ToInt32(rdr.GetValue(6)) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]));
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], markTotal.ToString());
                                    pointArrayGlobalVariable[subjectPosition, 10] = markTotal.ToString(); //strTotal2 subject


                                    if (isSemester == true)  //update subjects for TOT Gen column
                                    {
                                        int intmarkSubjectTotal1 = 0;
                                        string stmarkSubjectTotal1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                        bool canConvertSubjectTot = int.TryParse(stmarkSubjectTotal1, out intmarkSubjectTotal1);
                                        subjectTotalGen = markTotal + intmarkSubjectTotal1;

                                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], subjectTotalGen.ToString());
                                        pointArrayGlobalVariable[subjectPosition, 12] = subjectTotalGen.ToString(); // subjects for TOT Gen column

                                    }

                                }
                                else if (AssessmentPeriod == LocRM.GetString("strExam3"))
                                {
                                    int intmarkTotal5P = 0;
                                    int intmarkTotal6P = 0;
                                    string stmarkTotal5P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]);
                                    string stmarkTotal6P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()]);
                                    bool canConvert5 = int.TryParse(stmarkTotal5P, out intmarkTotal5P);
                                    bool canConvert6 = int.TryParse(stmarkTotal6P, out intmarkTotal6P);
                                    markTotal = Convert.ToInt16(rdr.GetValue(6)) + intmarkTotal5P + intmarkTotal6P;


                                    //markTotal = Convert.ToInt32(rdr.GetValue(6)) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]));
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], markTotal.ToString());
                                    pointArrayGlobalVariable[subjectPosition, 11] = markTotal.ToString(); //strTotal3 subject

                                    //update Totaux for TOT Gen column
                                    int intmarkSubject1 = 0;
                                    int intmarkSubject2 = 0;
                                    string stmarkSubject1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                    string stmarkSubject2 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()]);
                                    bool canConvertSubjectTot1 = int.TryParse(stmarkSubject1, out intmarkSubject1);
                                    bool canConvertSubjectTot2 = int.TryParse(stmarkSubject2, out intmarkSubject2);
                                    subjectTotalGen = totauxTotal + intmarkSubject1 + intmarkSubject2;

                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], subjectTotalGen.ToString());
                                    pointArrayGlobalVariable[subjectPosition, 12] = subjectTotalGen.ToString(); // Subject Tot general
                                }
                            }
                            else
                            {
                                //Update Maxima generaux
                                maximaGeneraux = maximaGeneraux + maximaPeriod;
                            }
                        }



                        //maxima generaux for periodes/exams

                        bool MaximaGeneralExist = false;
                        //check if subject already exist
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                            string MaximaGeneral = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                            string MaximaGeneralValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                            if (MaximaGeneral.Equals(LocRM.GetString("strMaximaGeneral").ToUpper()) && MaximaGeneralValue == "")
                            {
                                MaximaGeneralExist = true;
                                rowHandle = i;
                                break;
                            }
                            else if (MaximaGeneral == "")
                            {
                                MaximaGeneralExist = false;
                                break;
                            }
                            else
                            {
                                MaximaGeneralExist = false;
                            }
                        }

                        if (!MaximaGeneralExist)
                        {
                            //add new maxima row
                            gridView1.AddNewRow();
                            gridView1.UpdateCurrentRow();
                            rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                        }

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strMaximaGeneral").ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()], maximaGeneraux.ToString());

                        pointArrayGlobalVariable[34, columnPeriod] = maximaGeneraux.ToString();

                        maximaGenerauxTotalGlobal = (int)maximaGeneraux; //To calculate pass rate for report
                        maximaGenerauxGlobalVariable = maximaGeneraux.ToString();

                        //update maxima generaux for TOT columns                    
                        if (isExam == true)
                        {

                            if (AssessmentPeriod == LocRM.GetString("strExam1"))
                            {
                                int intMaxima2P = 0;
                                int intMaxima1P = 0;
                                string stMaxima2P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]);
                                string stMaxima1P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]);
                                bool canConvert2 = int.TryParse(stMaxima2P, out intMaxima2P);
                                bool canConvert1 = int.TryParse(stMaxima1P, out intMaxima1P);

                                maximaGenerauxTotal = maximaGeneraux + intMaxima2P + intMaxima1P;
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], maximaGenerauxTotal.ToString());

                                pointArrayGlobalVariable[34, 9] = maximaGenerauxTotal.ToString(); // maxima generaux
                            }
                            else if (AssessmentPeriod == LocRM.GetString("strExam2"))
                            {
                                int intMaxima4P = 0;
                                int intMaxima3P = 0;
                                string stMaxima4P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()]);
                                string stMaxima3P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]);
                                bool canConvert4 = int.TryParse(stMaxima4P, out intMaxima4P);
                                bool canConvert3 = int.TryParse(stMaxima3P, out intMaxima3P);

                                maximaGenerauxTotal = maximaGeneraux + intMaxima4P + intMaxima3P;

                                //maximaGenerauxTotal = maximaGeneraux + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]));
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], maximaGenerauxTotal.ToString());
                                pointArrayGlobalVariable[34, 10] = maximaGenerauxTotal.ToString(); // maxima generaux

                                if (isSemester == true)  //update Maxima Generaux for TOT Gen column
                                {
                                    int intmarkMaximaGeneraux1 = 0;
                                    string stmarkMaximaGeneraux1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                    bool canConvertTot = int.TryParse(stmarkMaximaGeneraux1, out intmarkMaximaGeneraux1);
                                    maximaGenerauxTotGen = maximaGenerauxTotal + intmarkMaximaGeneraux1;

                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaGenerauxTotGen.ToString());
                                    pointArrayGlobalVariable[34, 12] = maximaGenerauxTotGen.ToString(); // Maxima Generaux Tot general to bulletins

                                }

                            }
                            else if (AssessmentPeriod == LocRM.GetString("strExam3"))
                            {
                                int intMaxima6P = 0;
                                int intMaxima5P = 0;
                                string stMaxima6P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()]);
                                string stMaxima5P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]);
                                bool canConvert6 = int.TryParse(stMaxima6P, out intMaxima6P);
                                bool canConvert5 = int.TryParse(stMaxima5P, out intMaxima5P);

                                maximaGenerauxTotal = maximaGeneraux + intMaxima6P + intMaxima5P;
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], maximaGenerauxTotal.ToString());

                                pointArrayGlobalVariable[34, 11] = maximaGenerauxTotal.ToString(); // maxima generaux

                                //update maxima generaux for TOT Gen column
                                int intmarkMaximaGeneraux1 = 0;
                                int intmarkMaximaGeneraux2 = 0;
                                string stmarkMaximaGeneraux1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                string stmarkMaximaGeneraux2 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()]);
                                bool canConvertMaximaGeneraux1 = int.TryParse(stmarkMaximaGeneraux1, out intmarkMaximaGeneraux1);
                                bool canConvertMaximaGeneraux2 = int.TryParse(stmarkMaximaGeneraux2, out intmarkMaximaGeneraux2);
                                maximaGenerauxTotGen = maximaGenerauxTotal + intmarkMaximaGeneraux1 + intmarkMaximaGeneraux2;

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], maximaGenerauxTotGen.ToString());
                                pointArrayGlobalVariable[34, 12] = maximaGenerauxTotGen.ToString(); // totaux Tot general
                            }
                            //Get the maxima tot for total columns
                            maximaGenerauxTotalColumnGlobal = maximaGenerauxTotal;
                        }

                        //Add Totaux
                        bool TotalsExist = false;
                        //check if subject already exist
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                            string Totals = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                            string TotalsValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                            if (Totals.Equals(LocRM.GetString("strTotals").ToUpper()) && TotalsValue == "")
                            {
                                TotalsExist = true;
                                rowHandle = i;
                                break;
                            }
                            else if (Totals == "")
                            {
                                TotalsExist = false;
                                break;
                            }
                            else
                            {
                                TotalsExist = false;
                            }
                        }

                        if (!TotalsExist)
                        {
                            //add new maxima row
                            gridView1.AddNewRow();
                            gridView1.UpdateCurrentRow();
                            rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                        }


                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strTotals").ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()], totaux.ToString());

                        pointArrayGlobalVariable[35, columnPeriod] = totaux.ToString();

                        totauxGlobalVariable = totaux.ToString();

                        //update Totaux for TOT columns
                        int totauxTotalGen = 0;
                        if (isExam == true)
                        {
                            if (AssessmentPeriod == LocRM.GetString("strExam1"))
                            {
                                int intmarkTotal1P = 0;
                                int intmarkTotal2P = 0;
                                string stmarkTotal1P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]);
                                string stmarkTotal2P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()]);
                                bool canConvert1 = int.TryParse(stmarkTotal1P, out intmarkTotal1P);
                                bool canConvert2 = int.TryParse(stmarkTotal2P, out intmarkTotal2P);
                                totauxTotal = totaux + intmarkTotal1P + intmarkTotal2P;

                                //totauxTotal = totaux + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()]));
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], totauxTotal.ToString());

                                pointArrayGlobalVariable[35, 9] = totauxTotal.ToString(); // totaux generaux

                            }
                            else if (AssessmentPeriod == LocRM.GetString("strExam2"))
                            {
                                int intmarkTotal3P = 0;
                                int intmarkTotal4P = 0;
                                string stmarkTotal3P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]);
                                string stmarkTotal4P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()]);
                                bool canConvert3 = int.TryParse(stmarkTotal3P, out intmarkTotal3P);
                                bool canConvert4 = int.TryParse(stmarkTotal4P, out intmarkTotal4P);
                                totauxTotal = totaux + intmarkTotal3P + intmarkTotal4P;

                                // totauxTotal = maximaGeneraux + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()]));
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], totauxTotal.ToString());
                                pointArrayGlobalVariable[35, 10] = totauxTotal.ToString(); // totaux generaux

                                if (isSemester == true)  //update Totaux for TOT Gen column
                                {
                                    int intmarkTotal1 = 0;
                                    string stmarkTotal1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                    bool canConvertTot = int.TryParse(stmarkTotal1, out intmarkTotal1);
                                    totauxTotalGen = totauxTotal + intmarkTotal1;

                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], totauxTotalGen.ToString());
                                    pointArrayGlobalVariable[35, 12] = totauxTotalGen.ToString(); // totaux Tot general

                                }
                            }
                            else if (AssessmentPeriod == LocRM.GetString("strExam3"))
                            {
                                int intmarkTotal5P = 0;
                                int intmarkTotal6P = 0;
                                string stmarkTotal5P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]);
                                string stmarkTotal6P = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()]);
                                bool canConvert5 = int.TryParse(stmarkTotal5P, out intmarkTotal5P);
                                bool canConvert6 = int.TryParse(stmarkTotal6P, out intmarkTotal6P);
                                totauxTotal = totaux + intmarkTotal5P + intmarkTotal6P;

                                // totauxTotal = maximaGeneraux + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()])) + Convert.ToInt32(gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()]));
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], totauxTotal.ToString());
                                pointArrayGlobalVariable[35, 11] = totauxTotal.ToString(); // totaux generaux

                                //update Totaux for TOT Gen column
                                int intmarkTotal1 = 0;
                                int intmarkTotal2 = 0;
                                string stmarkTotal1 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                string stmarkTotal2 = gridView1.GetRowCellDisplayText(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()]);
                                bool canConvertTot1 = int.TryParse(stmarkTotal1, out intmarkTotal1);
                                bool canConvertTot2 = int.TryParse(stmarkTotal2, out intmarkTotal2);
                                totauxTotalGen = totauxTotal + intmarkTotal1 + intmarkTotal2;

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], totauxTotalGen.ToString());
                                pointArrayGlobalVariable[35, 12] = totauxTotalGen.ToString(); // totaux Tot general
                            }
                        }


                        //Add Pourcentage
                        if (maximaGeneraux > 0)
                        {
                            studentPercentage = (double)(totaux * 100) / maximaGeneraux;
                            studentPercentage = Math.Round(studentPercentage, 1);
                        }
                        else
                        {
                            studentPercentage = 0;
                        }

                        bool PourcentageExist = false;
                        //check if Pourcentage already exist
                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                            string Pourcentage = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                            string PourcentageValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                            if (Pourcentage.Equals(LocRM.GetString("strPercentage").ToUpper()) && PourcentageValue == "")
                            {
                                PourcentageExist = true;
                                rowHandle = i;
                                break;
                            }
                            else if (Pourcentage == "")
                            {
                                PourcentageExist = false;
                                break;
                            }
                            else
                            {
                                PourcentageExist = false;
                            }
                        }

                        if (!PourcentageExist)
                        {
                            //add new maxima row
                            gridView1.AddNewRow();
                            gridView1.UpdateCurrentRow();
                            rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                        }

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strPercentage").ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()], studentPercentage.ToString());
                        percentageGlobalVariable = studentPercentage.ToString();

                        pointArrayGlobalVariable[36, columnPeriod] = studentPercentage.ToString();

                        //update Percentage for TOT columns
                        if (isExam == true)
                        {
                            if (AssessmentPeriod == LocRM.GetString("strExam1"))
                            {
                                if (maximaGenerauxTotal > 0)
                                {
                                    studentPercentage = (double)(totauxTotal * 100) / maximaGenerauxTotal;
                                    studentPercentage = Math.Round(studentPercentage, 1);

                                }
                                else
                                {
                                    studentPercentage = 0;
                                }

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], studentPercentage.ToString());
                                pointArrayGlobalVariable[36, 9] = studentPercentage.ToString(); // Percentage generaux
                            }
                            else if (AssessmentPeriod == LocRM.GetString("strExam2"))
                            {
                                if (maximaGenerauxTotal > 0)
                                {
                                    studentPercentage = (double)(totauxTotal * 100) / maximaGenerauxTotal;
                                    studentPercentage = Math.Round(studentPercentage, 1);

                                }
                                else
                                {
                                    studentPercentage = 0;
                                }

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], studentPercentage.ToString());

                                pointArrayGlobalVariable[36, 10] = studentPercentage.ToString(); // Percentage generaux

                                if (isSemester == true)  //update Percentage for TOT Gen column
                                {
                                    if (maximaGenerauxTotGen > 0)
                                    {
                                        studentPercentage = (double)(totauxTotalGen * 100) / maximaGenerauxTotGen;
                                        studentPercentage = Math.Round(studentPercentage, 1);

                                    }
                                    else
                                    {
                                        studentPercentage = 0;
                                    }

                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], studentPercentage.ToString());

                                    pointArrayGlobalVariable[36, 12] = studentPercentage.ToString(); // Percentage for TOT Gen column to bulletin

                                }

                            }
                            else if (AssessmentPeriod == LocRM.GetString("strExam3"))
                            {
                                if (maximaGenerauxTotal > 0)
                                {
                                    studentPercentage = (double)(totauxTotal * 100) / maximaGenerauxTotal;
                                    studentPercentage = Math.Round(studentPercentage, 1);

                                }
                                else
                                {
                                    studentPercentage = 0;
                                }


                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], studentPercentage.ToString());
                                pointArrayGlobalVariable[36, 11] = studentPercentage.ToString(); // Percentage for TOT Gen column to bulletin

                                //update Percentage for TOT Gen column
                                if (maximaGenerauxTotGen > 0)
                                {
                                    studentPercentage = (double)(totauxTotalGen * 100) / maximaGenerauxTotGen;
                                    studentPercentage = Math.Round(studentPercentage, 1);

                                }
                                else
                                {
                                    studentPercentage = 0;
                                }

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], studentPercentage.ToString());
                                pointArrayGlobalVariable[36, 12] = studentPercentage.ToString(); // Percentage for TOT Gen column to bulletin

                            }
                        }

                        con.Close();
                    }

                    //Get students ranking for periods and exams

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();

                        cmd = new SqlCommand("SELECT ROW_NUMBER() OVER(ORDER BY SUM (MarksObtained) desc) as RowNo, StudentNumber, " +
                            "SchoolYear, Class,AssessmentPeriod, SUM (MarksObtained) as Marks  from MarksEntry " +
                            "where SchoolYear = @d1 and Class = @d2 and AssessmentPeriod = @d3 " +
                            "group by StudentNumber,SchoolYear,Class,AssessmentPeriod", con);

                        cmd.Parameters.Add("@d1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                        cmd.Parameters.Add("@d3", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = AssessmentPeriod;

                        SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                        DataSet myDataSet = new DataSet("ds");
                        DataTable dtable;
                        myDA.Fill(myDataSet);
                        dtable = myDataSet.Tables[0];
                        string studentRank = "";
                        double studentPercentageRank = 0;

                        foreach (DataRow drow in dtable.Rows)
                        {
                            //
                            if (maximaGenerauxTotalGlobal > 0)
                            {

                                studentPercentageRank = (double)((int)drow[5] * 100) / maximaGenerauxTotalGlobal;
                                studentPercentageRank = Math.Round(studentPercentageRank, 1);
                                if (studentPercentageRank >= 50)
                                {
                                    passed++;
                                }
                                totalPointsGlobalVariable = totalPointsGlobalVariable + (int)drow[5];
                            }

                            if (drow[1].ToString() == cmbStudentNo.Text.Trim())
                            {
                                studentRank = drow[0].ToString(); //drow[0] = rank, drow[1] = student No, drow[5] tot marks
                            }
                        }
                        //calculate total avarage percentage of class
                        double avarage = (double)totalPointsGlobalVariable / noStudents;
                        avarage = (avarage * 100) / maximaGenerauxTotalGlobal;
                        avarage = Math.Round(avarage, 1);

                        totalAvaragePercClassGlobalVariable = avarage.ToString();
                        //Add student ranking  
                        int rowHandle = 0;
                        if (studentRank != "")
                        {
                            bool studentRankingExist = false;
                            //check if Pourcentage already exist
                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                string studentRanking = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                string studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                                if (studentRanking.Equals(LocRM.GetString("strPlaceNoStudents").ToUpper()) && studentRankingValue == "")
                                {
                                    studentRankingExist = true;
                                    rowHandle = i;
                                    break;
                                }
                                else if (studentRanking == "")
                                {
                                    studentRankingExist = false;
                                    break;
                                }
                                else
                                {
                                    studentRankingExist = false;
                                }
                            }

                            if (!studentRankingExist)
                            {
                                //add new maxima row
                                gridView1.AddNewRow();
                                gridView1.UpdateCurrentRow();
                                rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                            }

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strPlaceNoStudents").ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()], studentRank + " /" + noStudents.ToString());
                            placeGlobalVariable = studentRank + " /" + noStudents.ToString();

                            pointArrayGlobalVariable[37, columnPeriod] = studentRank + " /" + noStudents.ToString();
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

                    //update Students rankings for TOT columns
                    if (isExam == true)
                    {
                        using (con = new SqlConnection(databaseConnectionString))
                        {
                            con.Open();

                            cmd = new SqlCommand("" +
                                "SELECT ROW_NUMBER() OVER(ORDER BY (P1.TotP1 + P2.TotP2 + Exam.TotExam) desc) " +
                                "as Place, P1.StudentNumber, P1.TotP1, P2.TotP2,Exam.TotExam,(P1.TotP1 + P2.TotP2 + Exam.TotExam) as tot " +
                                "from (" +
                                "(select StudentNumber, SchoolYear, Class,AssessmentPeriod, SUM (MarksObtained) AS TotP1 " +
                                "FROM MarksEntry " +
                                "where (SchoolYear = @d1P1 and Class = @d2P1 and AssessmentPeriod = @d3P1) " +
                                "GROUP BY StudentNumber,SchoolYear,Class,AssessmentPeriod) as P1 " +
                                "JOIN " +
                                "(select StudentNumber, SchoolYear, Class,AssessmentPeriod, SUM (MarksObtained) AS TotP2 " +
                                "FROM MarksEntry " +
                                "where (SchoolYear = @d1P2 and Class = @d2P2 and AssessmentPeriod = @d3P2) " +
                                "GROUP BY StudentNumber,SchoolYear,Class,AssessmentPeriod) as P2 " +
                                "ON P1.StudentNumber = P2.StudentNumber " +
                                "JOIN " +
                                "(select StudentNumber, SchoolYear, Class,AssessmentPeriod, SUM (MarksObtained) AS TotExam " +
                                "FROM MarksEntry " +
                                "where (SchoolYear = @d1Exam and Class = @d2Exam and AssessmentPeriod = @d3Exam) " +
                                "GROUP BY StudentNumber,SchoolYear,Class,AssessmentPeriod) as Exam " +
                                "ON P1.StudentNumber = Exam.StudentNumber )", con);

                            if (AssessmentPeriod == LocRM.GetString("strExam1"))
                            {
                                cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str1eP");

                                cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str2eP");

                                cmd.Parameters.Add("@d1Exam", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2Exam", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3Exam", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam1");
                            }

                            else if (AssessmentPeriod == LocRM.GetString("strExam2"))
                            {
                                cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str3eP");

                                cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str4eP");

                                cmd.Parameters.Add("@d1Exam", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2Exam", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3Exam", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam2");
                            }

                            else if (AssessmentPeriod == LocRM.GetString("strExam3"))
                            {
                                cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str5eP");

                                cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str6eP");

                                cmd.Parameters.Add("@d1Exam", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters.Add("@d2Exam", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                cmd.Parameters.Add("@d3Exam", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam3");
                            }


                            SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                            DataSet myDataSet = new DataSet("ds");
                            DataTable dtable;
                            myDA.Fill(myDataSet);
                            dtable = myDataSet.Tables[0];
                            string studentRank = "";
                            int totalPointsTotalColumnGlobalVariable = 0;
                            foreach (DataRow drow in dtable.Rows)
                            {
                                //
                                if (maximaGenerauxTotalColumnGlobal > 0)
                                {
                                    studentPercentageTotalColumnGlobal = (double)((int)drow[5] * 100) / maximaGenerauxTotalColumnGlobal;
                                    studentPercentageTotalColumnGlobal = Math.Round(studentPercentageTotalColumnGlobal, 1);
                                    if (studentPercentageTotalColumnGlobal >= 50)
                                    {
                                        passedTotalColumnGlobal++;
                                    }
                                    totalPointsTotalColumnGlobalVariable = totalPointsTotalColumnGlobalVariable + (int)drow[5];
                                }

                                if (drow[1].ToString() == cmbStudentNo.Text.Trim())
                                {
                                    studentRank = drow[0].ToString(); //drow[0] = rank, drow[1] = student No, drow[5] tot marks
                                }
                            }
                            //calculate total avarage percetage of class
                            double avarage = (double)totalPointsTotalColumnGlobalVariable / noStudents;
                            avarage = (avarage * 100) / maximaGenerauxTotalColumnGlobal;
                            avarage = Math.Round(avarage, 1);

                            totalAvaragePerClassSemTrimGlobalVariable = avarage.ToString();  //Total avarage percentage per semester/Class per class
                                                                                             //Add student ranking  
                            int rowHandle = 0;
                            if (studentRank != "")
                            {
                                bool studentRankingExist = false;
                                string actualTot = "";
                                //check if Pourcentage already exist
                                for (int i = 0; i < gridView1.DataRowCount; i++)
                                {
                                    string studentRankingValue = "";

                                    int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                    string studentRanking = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                    if (AssessmentPeriod == LocRM.GetString("strExam1"))
                                    {
                                        studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()]);
                                        actualTot = LocRM.GetString("strTotal1").ToUpper();
                                    }
                                    else if (AssessmentPeriod == LocRM.GetString("strExam2"))
                                    {
                                        studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()]);
                                        actualTot = LocRM.GetString("strTotal2").ToUpper();
                                    }
                                    else if (AssessmentPeriod == LocRM.GetString("strExam3"))
                                    {
                                        studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()]);
                                        actualTot = LocRM.GetString("strTotal3").ToUpper();
                                    }

                                    if (studentRanking.Equals(LocRM.GetString("strPlaceNoStudents").ToUpper()) && studentRankingValue == "")
                                    {
                                        studentRankingExist = true;
                                        rowHandle = i;
                                        break;
                                    }
                                    else if (studentRanking == "")
                                    {
                                        studentRankingExist = false;
                                        break;
                                    }
                                    else
                                    {
                                        studentRankingExist = false;
                                    }
                                }

                                if (!studentRankingExist)
                                {
                                    //add new maxima row
                                    gridView1.AddNewRow();
                                    gridView1.UpdateCurrentRow();
                                    rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                                }

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strPlaceNoStudents").ToUpper());
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[actualTot], studentRank + " /" + noStudents.ToString());
                                // placeGlobalVariable = studentRank + " /" + noStudents.ToString();

                                if (AssessmentPeriod == LocRM.GetString("strExam1"))
                                {
                                    //Save student rank in global array to update bulletin
                                    pointArrayGlobalVariable[37, 9] = studentRank + " /" + noStudents.ToString();
                                }
                                else if (AssessmentPeriod == LocRM.GetString("strExam2"))
                                {
                                    //Save student rank in global array to update bulletin
                                    pointArrayGlobalVariable[37, 10] = studentRank + " /" + noStudents.ToString();
                                }
                                else if (AssessmentPeriod == LocRM.GetString("strExam3"))
                                {
                                    //Save student rank in global array to update bulletin
                                    pointArrayGlobalVariable[37, 11] = studentRank + " /" + noStudents.ToString();
                                }

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

                    //update Students rankings for TOT Gen column
                    if (isExam == true)
                    {
                        if (((isSemester == true) && (AssessmentPeriod == LocRM.GetString("strExam2")))
                            || ((isSemester == false) && (AssessmentPeriod == LocRM.GetString("strExam3"))))
                        {
                            using (con = new SqlConnection(databaseConnectionString))
                            {
                                con.Open();

                                if (isSemester == true)  //Get Students rankings for TOT Gen column for semester section
                                {
                                    cmd = new SqlCommand(" SELECT ROW_NUMBER() OVER(ORDER BY " +
                                "(P1.TotP1 + P2.TotP2 + Exam1.TotExam1 + P3.TotP3 + P4.TotP4  + Exam2.TotExam2) desc) " +
                                "as Place, P1.StudentNumber, P1.TotP1, P2.TotP2, Exam1.TotExam1, P3.TotP3, P4.TotP4, Exam2.TotExam2, " +
                                "(P1.TotP1 + P2.TotP2 + Exam1.TotExam1 + P3.TotP3 + P4.TotP4 + Exam2.TotExam2) as tot " +
                                "from( " +
                                "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP1 " +
                                "FROM MarksEntry " +
                                "where(SchoolYear = @d1P1 and Class = @d2P1 and AssessmentPeriod = @d3P1) " +
                                "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P1 " +
                                "JOIN " +
                                "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP2 " +
                                "FROM MarksEntry " +
                                "where(SchoolYear = @d1P2 and Class = @d2P2 and AssessmentPeriod = @d3P2) " +
                                "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P2 " +
                                "ON P1.StudentNumber = P2.StudentNumber " +
                                "JOIN " +
                                "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam1 " +
                                "FROM MarksEntry " +
                                "where(SchoolYear = @d1P3 and Class = @d2P3 and AssessmentPeriod = @d3P3) " +
                                "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam1 " +
                                "ON P1.StudentNumber = Exam1.StudentNumber " +
                                "" +
                                "JOIN " +
                                "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP3 " +
                                "FROM MarksEntry " +
                                "where (SchoolYear = @d1P4 and Class = @d2P4 and AssessmentPeriod = @d3P4) " +
                                "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P3 " +
                                "ON P1.StudentNumber = P3.StudentNumber " +
                                "JOIN " +
                                "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP4 " +
                                "FROM MarksEntry " +
                                "where (SchoolYear = @d1P5 and Class = @d2P5 and AssessmentPeriod = @d3P5) " +
                                "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P4 " +
                                "ON P1.StudentNumber = P4.StudentNumber " +
                                "JOIN " +
                                "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam2 " +
                                "FROM MarksEntry " +
                                "where (SchoolYear = @d1P6 and Class = @d2P6 and AssessmentPeriod = @d3P6) " +
                                "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam2 " +
                                "ON P1.StudentNumber = Exam2.StudentNumber )", con);


                                    cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str1eP");

                                    cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str2eP");

                                    cmd.Parameters.Add("@d1P3", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P3", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P3", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam1");

                                    cmd.Parameters.Add("@d1P4", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P4", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P4", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str3eP");

                                    cmd.Parameters.Add("@d1P5", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P5", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P5", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str4eP");

                                    cmd.Parameters.Add("@d1P6", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P6", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P6", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam2");

                                }
                                else
                                {
                                    cmd = new SqlCommand("" +
                                        "SELECT ROW_NUMBER() OVER(ORDER BY " +
                                        "(P1.TotP1 + P2.TotP2 + Exam1.TotExam1 + P3.TotP3 + P4.TotP4  + Exam2.TotExam2 + P5.TotP5 + P6.TotP6  + Exam3.TotExam3) desc) " +
                                        "as Place, P1.StudentNumber, P1.TotP1, P2.TotP2, Exam1.TotExam1, P3.TotP3, P4.TotP4, Exam2.TotExam2, P5.TotP5, P6.TotP6, Exam3.TotExam3, " +
                                        "(P1.TotP1 + P2.TotP2 + Exam1.TotExam1 + P3.TotP3 + P4.TotP4 + Exam2.TotExam2 + P5.TotP5 + P6.TotP6 + Exam3.TotExam3) as tot " +
                                        "from( " +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP1 " +
                                        "FROM MarksEntry " +
                                        "where (SchoolYear = @d1P1 and Class = @d2P1 and AssessmentPeriod = @d3P1) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P1 " +
                                        "JOIN " +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP2 " +
                                        "FROM MarksEntry " +
                                        "where(SchoolYear = @d1P2 and Class = @d2P2 and AssessmentPeriod = @d3P2) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P2 " +
                                        "ON P1.StudentNumber = P2.StudentNumber " +
                                        "JOIN " +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam1 " +
                                        "FROM MarksEntry " +
                                        "where(SchoolYear = @d1P3 and Class = @d2P3 and AssessmentPeriod = @d3P3) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam1 " +
                                        "ON P1.StudentNumber = Exam1.StudentNumber " +
                                        "" +
                                        "JOIN " +
                                        "" +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP3 " +
                                        "FROM MarksEntry " +
                                        "where(SchoolYear = @d1P4 and Class = @d2P4 and AssessmentPeriod = @d3P4) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P3 " +
                                        "ON P1.StudentNumber = P3.StudentNumber " +
                                        "JOIN " +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP4 " +
                                        "FROM MarksEntry " +
                                        "where(SchoolYear = @d1P5 and Class = @d2P5 and AssessmentPeriod = @d3P5) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P4 " +
                                        "ON P1.StudentNumber = P4.StudentNumber " +
                                        "JOIN " +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam2 " +
                                        "FROM MarksEntry " +
                                        "where(SchoolYear = @d1P6 and Class = @d2P6 and AssessmentPeriod = @d3P6) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam2 " +
                                        "ON P1.StudentNumber = Exam2.StudentNumber " +
                                        "" +
                                        "JOIN " +
                                        "" +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP5 " +
                                        "FROM MarksEntry " +
                                        "where(SchoolYear = @d1P7 and Class = @d2P6 and AssessmentPeriod = @d3P7) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P5 " +
                                        "ON P1.StudentNumber = P5.StudentNumber " +
                                        "JOIN " +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotP6 " +
                                        "FROM MarksEntry " +
                                        "where(SchoolYear = @d1P8 and Class = @d2P8 and AssessmentPeriod = @d3P8) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as P6 " +
                                        "ON P1.StudentNumber = P6.StudentNumber " +
                                        "JOIN " +
                                        "(select StudentNumber, SchoolYear, Class, AssessmentPeriod, SUM(MarksObtained) AS TotExam3 " +
                                        "FROM MarksEntry " +
                                        "where(SchoolYear = @d1P9 and Class = @d2P9 and AssessmentPeriod = @d3P9) " +
                                        "GROUP BY StudentNumber, SchoolYear, Class, AssessmentPeriod) as Exam3 " +
                                        "ON P1.StudentNumber = Exam3.StudentNumber " +
                                        " ) ", con);


                                    cmd.Parameters.Add("@d1P1", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P1", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P1", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str1eP");

                                    cmd.Parameters.Add("@d1P2", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P2", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P2", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str2eP");

                                    cmd.Parameters.Add("@d1P3", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P3", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P3", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam1");

                                    cmd.Parameters.Add("@d1P4", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P4", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P4", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str3eP");

                                    cmd.Parameters.Add("@d1P5", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P5", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P5", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str4eP");

                                    cmd.Parameters.Add("@d1P6", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P6", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P6", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam2");

                                    cmd.Parameters.Add("@d1P7", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P7", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P7", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str5eP");

                                    cmd.Parameters.Add("@d1P8", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P8", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P8", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("str6eP");

                                    cmd.Parameters.Add("@d1P9", SqlDbType.NVarChar, 60, "SchoolYear").Value = cmbSchoolYear.Text.Trim();
                                    cmd.Parameters.Add("@d2P9", SqlDbType.NVarChar, 60, "Class").Value = cmbClass.Text.Trim();
                                    cmd.Parameters.Add("@d3P9", SqlDbType.NVarChar, 60, "AssessmentPeriod").Value = LocRM.GetString("strExam3");

                                }


                                SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                                DataSet myDataSet = new DataSet("ds");
                                DataTable dtable;
                                myDA.Fill(myDataSet);
                                dtable = myDataSet.Tables[0];
                                string studentRank = "";
                                int totalPointsTotalGenColumnGlobalVariable = 0;
                                double studentPercentageTotalGenColumnGlobal = 0;
                                foreach (DataRow drow in dtable.Rows)
                                {
                                    //
                                    if (maximaGenerauxTotGen > 0)
                                    {
                                        studentPercentageTotalGenColumnGlobal = (double)((int)drow[8] * 100) / maximaGenerauxTotGen;
                                        studentPercentageTotalGenColumnGlobal = Math.Round(studentPercentageTotalGenColumnGlobal, 1);
                                        if (studentPercentageTotalGenColumnGlobal >= 50)
                                        {
                                            passedTotalGenColumnGlobal++;
                                        }
                                        totalPointsTotalGenColumnGlobalVariable = totalPointsTotalGenColumnGlobalVariable + (int)drow[8];
                                    }

                                    if (drow[1].ToString() == cmbStudentNo.Text.Trim())
                                    {
                                        studentRank = drow[0].ToString(); //drow[0] = rank, drow[1] = student No, drow[5] tot marks
                                    }
                                }
                                //calculate total avarage percetage of class
                                double avarage = (double)totalPointsTotalGenColumnGlobalVariable / noStudents;
                                avarage = (avarage * 100) / maximaGenerauxTotGen;
                                avarage = Math.Round(avarage, 1);

                                totalAvaragePerClassTotGenGlobalVariable = avarage.ToString();  //Total avarage percentage per semester/Class per class
                                                                                                //Add student ranking  
                                int rowHandle = 0;
                                if (studentRank != "")
                                {
                                    bool studentRankingExist = false;
                                    // string actualTot = "";
                                    //check if Pourcentage already exist
                                    for (int i = 0; i < gridView1.DataRowCount; i++)
                                    {
                                        string studentRankingValue = "";

                                        int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                        string studentRanking = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);

                                        if ((AssessmentPeriod == LocRM.GetString("strExam2")) && (isSemester == true))
                                        {
                                            studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()]);

                                        }
                                        else if (AssessmentPeriod == LocRM.GetString("strExam3"))
                                        {
                                            studentRankingValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()]);
                                        }

                                        if (studentRanking.Equals(LocRM.GetString("strPlaceNoStudents").ToUpper()) && studentRankingValue == "")
                                        {
                                            studentRankingExist = true;
                                            rowHandle = i;
                                            break;
                                        }
                                        else if (studentRanking == "")
                                        {
                                            studentRankingExist = false;
                                            break;
                                        }
                                        else
                                        {
                                            studentRankingExist = false;
                                        }
                                    }

                                    if (!studentRankingExist)
                                    {
                                        //add new maxima row
                                        gridView1.AddNewRow();
                                        gridView1.UpdateCurrentRow();
                                        rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                                    }

                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strPlaceNoStudents").ToUpper());
                                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], studentRank + " /" + noStudents.ToString());

                                    //Save student rank in global array to update bulletin
                                    pointArrayGlobalVariable[37, 12] = studentRank + " /" + noStudents.ToString();
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

                    //Display application only on travail journaliers
                    if (isExam == false)
                    {
                        double studentPercentageApplication = studentPercentage;
                        studentPercentageApplication = Math.Round(studentPercentageApplication, 0);
                        //Get Application
                        using (con = new SqlConnection(databaseConnectionString))
                        {
                            con.Open();

                            string ct = "SELECT Abbreviation, Minimum, Maximum  FROM Application where  @d1 BETWEEN Minimum AND Maximum ";
                            cmd = new SqlCommand(ct);
                            cmd.Connection = con;
                            cmd.Parameters.Add("@d1", SqlDbType.Decimal).Value = studentPercentageApplication;
                            //cmd.Parameters.Add("@d2", SqlDbType.Decimal, 100, " Maximum").Value = studentPercentage;

                            rdr = cmd.ExecuteReader();
                            string sStudentPercentage = "";
                            if (rdr.Read())
                            {
                                sStudentPercentage = rdr.GetValue(0).ToString();
                            }

                            //Add Application  
                            int rowHandle = 0;
                            if (sStudentPercentage != "")
                            {
                                bool ApplicationExist = false;
                                //check if Pourcentage already exist
                                for (int i = 0; i < gridView1.DataRowCount; i++)
                                {
                                    int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                    string Application = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                    string ApplicationValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                                    if (Application.Equals(LocRM.GetString("strApplication").ToUpper()) && ApplicationValue == "")
                                    {
                                        ApplicationExist = true;
                                        rowHandle = i;
                                        break;
                                    }
                                    else if (Application == "")
                                    {
                                        ApplicationExist = false;
                                        break;
                                    }
                                    else
                                    {
                                        ApplicationExist = false;
                                    }
                                }

                                if (!ApplicationExist)
                                {
                                    //add new maxima row
                                    gridView1.AddNewRow();
                                    gridView1.UpdateCurrentRow();
                                    rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                                }

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strApplication").ToUpper());
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()], sStudentPercentage);
                                applicationGlobalVariable = sStudentPercentage;

                                pointArrayGlobalVariable[38, columnPeriod] = sStudentPercentage;
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
                    //Display Conduite only on travail journaliers
                    if (isExam == false)
                    {
                        //Get Conduite
                        using (con = new SqlConnection(databaseConnectionString))
                        {
                            con.Open();

                            string ct = "SELECT Conduct  FROM ConductsEntry " +
                                "where  SchoolYear = @d1 and Class = @d2 and StudentNumber = @d3 and AssessmentPeriod = @d4 ";
                            cmd = new SqlCommand(ct);
                            cmd.Connection = con;
                            cmd.Parameters.Add("@d1", SqlDbType.NVarChar, 60, " Maximum").Value = cmbSchoolYear.Text;
                            cmd.Parameters.Add("@d2", SqlDbType.NVarChar, 100, " Maximum").Value = cmbClass.Text;
                            cmd.Parameters.Add("@d3", SqlDbType.NVarChar, 60, " Maximum").Value = cmbStudentNo.Text;
                            cmd.Parameters.Add("@d4", SqlDbType.NVarChar, 60, " Maximum").Value = AssessmentPeriod;

                            rdr = cmd.ExecuteReader();
                            string conduite = "";
                            if (rdr.Read())
                            {
                                conduite = rdr.GetValue(0).ToString();
                            }

                            //Add Application  
                            int rowHandle = 0;
                            if (conduite != "")
                            {
                                bool conduiteExist = false;
                                //check if Pourcentage already exist
                                for (int i = 0; i < gridView1.DataRowCount; i++)
                                {
                                    int currentRowHandle = gridView1.GetVisibleRowHandle(i);
                                    string readConduite = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()]);
                                    string ConduiteValue = gridView1.GetRowCellDisplayText(currentRowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()]);
                                    if (readConduite.Equals(LocRM.GetString("strConduct").ToUpper()) && ConduiteValue == "")
                                    {
                                        conduiteExist = true;
                                        rowHandle = i;
                                        break;
                                    }
                                    else if (readConduite == "")
                                    {
                                        conduiteExist = false;
                                        break;
                                    }
                                    else
                                    {
                                        conduiteExist = false;
                                    }
                                }

                                if (!conduiteExist)
                                {
                                    //add new maxima row
                                    gridView1.AddNewRow();
                                    gridView1.UpdateCurrentRow();
                                    rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                                }

                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], LocRM.GetString("strConduct").ToUpper());
                                gridView1.SetRowCellValue(rowHandle, gridView1.Columns[AssessmentPeriod.ToUpper()], conduite);
                                conduiteGlobalVariable = conduite;
                                pointArrayGlobalVariable[39, columnPeriod] = conduite;
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

                    numberStudentsGlobalVariable = noStudents;

                    SchoolYear = cmbSchoolYear.Text;
                    assessmentPeriodGlobalVariable = AssessmentPeriod;

                    className = cmbClass.Text;
                    passedGlobalVariable = passed;

                    studentNumber = cmbStudentNo.Text.Trim();

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
                Cursor = Cursors.Default;
                timer1.Enabled = false;
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DataTable CreateDataBranchesPrimary()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strBranches").ToUpper(), typeof(string));
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
            dt.Columns.Add(LocRM.GetString("strBranches").ToUpper(), typeof(string));           
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
        private void cmbStudentSurnameName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbStudentNo.SelectedIndex = cmbStudentSurnameName.SelectedIndex;
            cmbSurname.SelectedIndex = cmbStudentSurnameName.SelectedIndex;
            cmbName.SelectedIndex = cmbStudentSurnameName.SelectedIndex;
            cmbParentNames.SelectedIndex = cmbStudentSurnameName.SelectedIndex;
            cmbNotificationEmail.SelectedIndex = cmbStudentSurnameName.SelectedIndex;

            //clear gloabal multidimensional array that stores points
            Array.Clear(pointArrayGlobalVariable, 0, pointArrayGlobalVariable.Length);

            // clearControls();

            cmbAssessmentPeriod.SelectedIndex = -1;
            cmbAssessmentPeriod.Properties.Items.Clear();            
            picStudent.EditValue = null;
            loadAssessmentPeriod();
            
            //clear gridcontrol
            gridControlSchoolReport.DataSource = null;
            //refresh gridview to rebuildnew columns
            gridView1.Columns.Clear();
            gridView1.PopulateColumns();            

            if (cmbCycle.SelectedIndex >= 2)
            {
                //display culumns with 2 semesters
                gridControlSchoolReport.DataSource = CreateDataBranchesSecondary();
                gridView1.BestFitColumns();
                gridView1.OptionsView.ColumnAutoWidth = true;
            }
            else
            {
                //display culumns with 3 trimesters
                gridControlSchoolReport.DataSource = CreateDataBranchesPrimary();
                gridView1.BestFitColumns();
                gridView1.OptionsView.ColumnAutoWidth = true;
            }

            //set width of count column to fixed
            gridView1.Columns[LocRM.GetString("strBranches").ToUpper()].OptionsColumn.FixedWidth = true;
            gridView1.Columns[LocRM.GetString("strBranches").ToUpper()].Width = 180;

            //load picture
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                if (cmbStudentSurnameName.Text!="")
                {                   
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "SELECT StudentNumber,StudentPicture FROM Students WHERE StudentNumber = @d1 ";
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 80, " StudentNumber").Value = cmbStudentNo.Text.Trim();
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {                           

                            if (!Convert.IsDBNull(rdr["StudentPicture"]))
                            {
                                byte[] x = (byte[])rdr["StudentPicture"];
                                MemoryStream ms = new MemoryStream(x);
                                picStudent.Image = Image.FromStream(ms);
                                picStudent.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                            }
                            else
                            {
                                picStudent.EditValue = null;
                            }
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
        //style on rows. set the Maxima row font black and bold
        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string rowMaxima = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strBranches").ToUpper()]);
                if (rowMaxima == LocRM.GetString("strMaxima").ToUpper())
                {
                    //e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
                //string rowMaximaGeneraux = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strBranches").ToUpper()]);
                //if (rowMaximaGeneraux == LocRM.GetString("strMaximaGeneral").ToUpper())
                //{
                   
                //    //e.Appearance.BackColor = Color.LightGreen;
                //    e.Appearance.ForeColor = Color.Black;
                //    e.Appearance.FontStyleDelta = FontStyle.Bold;
                  
                //}
                //if ((e.Column.FieldName == LocRM.GetString("str1eP").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            }
        }

        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView view = sender as GridView;
            string rowMaximaGeneraux = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strBranches").ToUpper()]);
            //if (rowMaximaGeneraux == LocRM.GetString("strMaximaGeneral").ToUpper())
            if(e.Column.FieldName == LocRM.GetString("strMaximaGeneral").ToUpper())
            {

                //e.Appearance.BackColor = Color.LightGreen;
                e.Appearance.ForeColor = Color.Black;
                e.Appearance.FontStyleDelta = FontStyle.Bold;
            }
            //else if (e.Column.FieldName == LocRM.GetString("strMaximaGeneral").ToUpper() && (rowMaximaGeneraux != LocRM.GetString("strMaximaGeneral").ToUpper()))
            //{
            //    e.Appearance.ForeColor = Color.Red;
            //    e.Appearance.FontStyleDelta = FontStyle.Regular;
            //}
            //    string rowMaxima = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strBranches").ToUpper()]);
            //    //Change Cell and Row Appearances Dynamically. Change font to Red when marks below 50%  
            //    try
            //    {
            //        if ((e.Column.FieldName == LocRM.GetString("str1eP").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str1eP").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaPeriod);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //        else if ((e.Column.FieldName == LocRM.GetString("str2eP").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str2eP").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaPeriod);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //        else if ((e.Column.FieldName == LocRM.GetString("str3eP").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str3eP").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaPeriod);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //        else if ((e.Column.FieldName == LocRM.GetString("str4eP").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str4eP").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaPeriod);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //        else if ((e.Column.FieldName == LocRM.GetString("str5eP").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str5eP").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaPeriod);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //        else if ((e.Column.FieldName == LocRM.GetString("str6eP").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str6eP").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaPeriod);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //        else if ((e.Column.FieldName == LocRM.GetString("strExam1").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strExam1").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaExam);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //        else if ((e.Column.FieldName == LocRM.GetString("strExam2").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strExam2").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaExam);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //        else if ((e.Column.FieldName == LocRM.GetString("strExam3").ToUpper()) && (rowMaxima != LocRM.GetString("strMaxima").ToUpper()))
            //        {
            //            string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strExam3").ToUpper()]);
            //            int intMark = 0;
            //            bool canConvert = int.TryParse(stringMark, out intMark);
            //            if (canConvert == true)
            //            {
            //                intMark = (intMark * 100) / Convert.ToInt16(maximaExam);

            //                if (intMark >= 50)
            //                {
            //                    e.Appearance.ForeColor = Color.Black;
            //                }
            //                else
            //                {
            //                    e.Appearance.ForeColor = Color.Red;
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
        }
    }
}
