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
using DevExpress.XtraBars.Ribbon;
using System.Resources;
using System.Data.SqlClient;
using EduXpress.Functions;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using static EduXpress.Functions.PublicVariables;

namespace EduXpress.Education
{    
    public partial class userControlMarkSheet : DevExpress.XtraEditors.XtraUserControl, Functions.IMergeRibbons
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlMarkSheet).Assembly);


        //create global methods of ribons and status bar to merge when in main.
        //add the ImergeRibbons interface.
        public RibbonControl MainRibbon { get; set; }
        public RibbonStatusBar MainStatusBar { get; set; }
        public userControlMarkSheet()
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

        private void btnLoadStudents_Click(object sender, EventArgs e)
        {
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

            try
            {
                //clear gridcontrol
                gridControlMarkSheet.DataSource = null;

                //refresh gridview to rebuildnew columns
                gridView1.Columns.Clear();
                gridView1.PopulateColumns();

                if (cmbCycle.SelectedIndex >= 2)
                {
                    //display culumns with 2 semesters
                    gridControlMarkSheet.DataSource = CreateDataBranchesSecondary();
                    gridView1.BestFitColumns();
                    gridView1.OptionsView.ColumnAutoWidth = true;
                }
                else
                {
                    //display culumns with 3 trimesters
                    gridControlMarkSheet.DataSource = CreateDataBranchesPrimary();
                    gridView1.BestFitColumns();
                    gridView1.OptionsView.ColumnAutoWidth = true;
                }

                //add Maxima row
                addSubjectGrid();

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strLoading"));
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string ct = "SELECT StudentNumber, StudentSurname,StudentFirstNames,SchoolYear,Class from Students where SchoolYear= @d1 and Class= @d2 order by StudentSurname"; 

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbClass.Text.Trim());
                    
                    rdr = cmd.ExecuteReader();
                    
                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1); 

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle).ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString().ToUpper());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper()); //merge surname and name                        
                        
                    }
                    con.Close();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                enableDisableColumns();
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
        //Get the current school year
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
                    if (cmbSchoolYear.Properties.Items.Count > 0)
                    {
                        
                        cmbSchoolYear.SelectedIndex = cmbSchoolYear.Properties.Items.Count - 1; //Select current school year.
                    }
                    //cmbSchoolYear.SelectedIndex = -1;
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


        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            clearControls();
            gridLookUpSubject.Enabled = true;
            cmbAssessmentPeriod.Enabled = true;
            loadSubjects();
            gridLookUpSubject.ForceInitialize();
            gridLookUpEdit1View.BestFitColumns();
            loadAssessmentPeriod();
            gridLookUpSubject.EditValue = null; //Clear gridLookUpEdit selection and avoid auto select previous item.
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

                    if((txtTeacherUserType.Text.Trim()== "Administrator") || 
                        (txtTeacherUserType.Text.Trim() == "Administrator Assistant") || 
                        (txtTeacherUserType.Text.Trim() == "Teacher Administrator")) //Show all subjects for this class as admin, Administrator Assistant or Teacher Administrator
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
        
        private void addSubjectGrid()
        {

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
              
                gridView1.AddNewRow();                
                gridView1.UpdateCurrentRow();
                int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                if (cmbCycle.SelectedIndex >= 2)
                {
                    //display culumns with 2 semesters
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle).ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], "**********"); 
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], LocRM.GetString("strMaxima").ToUpper());

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam1").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()],txtTot.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam2").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], txtTot.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], txtTotGen.Text);
                }
                else
                {
                    //display culumns with 3 trimesters
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle).ToString());
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], "**********");
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], LocRM.GetString("strMaxima").ToUpper());

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam1").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], txtTot.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam2").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], txtTot.Text);

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam3").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], txtTot.Text);

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], txtTotGen.Text);
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

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            cmbSchoolYear.Enabled = true;
            AutocompleteAcademicYear();
            getTeacherID();
        }
        private void clearControls()
        {
            gridLookUpSubject.Properties.DataSource = null; //clear all contents of gridLookUpSubject
            //clear gridcontrol
            gridControlMarkSheet.DataSource = null;

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
        private void reset()
        {          

            cmbSchoolYear.SelectedIndex = -1;
            cmbSchoolYear.Enabled = false;

            cmbCycle.SelectedIndex = -1;
            cmbCycle.Enabled = false;
            cmbSection.SelectedIndex = -1;
            cmbSection.Enabled = false;
            cmbClass.SelectedIndex = -1;
            cmbClass.Enabled = false;
            gridLookUpSubject.Text = "";
            
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            clearControls();
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

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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


            try
            {
                if (gridView1.DataRowCount > 0)
                {
                    for (int i = 1; i < gridView1.DataRowCount; i++) //start index at 1 because index 0 is used for maxima
                    {
                        if (!gridView1.IsNewItemRow(i))
                        {
                            if (splashScreenManager1.IsSplashFormVisible == false)
                            {
                                splashScreenManager1.ShowWaitForm();
                            }
                            using (con = new SqlConnection(databaseConnectionString))
                            {
                                con.Open();
                                string ct = "select SchoolYear, Class, StudentNumber,SubjectCode,AssessmentPeriod " +
                                    "from MarksEntry where SchoolYear=@find1 and StudentNumber=@find2 and " +
                                    "Class=@find3 and SubjectCode=@find4 and AssessmentPeriod=@find5";

                                cmd = new SqlCommand(ct);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@find1", System.Data.SqlDbType.VarChar, 100, "SchoolYear"));
                                cmd.Parameters.Add(new SqlParameter("@find2", System.Data.SqlDbType.VarChar, 100, "StudentNumber"));
                                cmd.Parameters.Add(new SqlParameter("@find3", System.Data.SqlDbType.VarChar, 100, "Class"));
                                cmd.Parameters.Add(new SqlParameter("@find4", System.Data.SqlDbType.VarChar, 100, "SubjectCode"));
                                cmd.Parameters.Add(new SqlParameter("@find5", System.Data.SqlDbType.VarChar, 100, "AssessmentPeriod"));

                                cmd.Parameters["@find1"].Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters["@find2"].Value = gridView1.GetRowCellValue(i, LocRM.GetString("strStudentNo").ToUpper()).ToString();
                                cmd.Parameters["@find3"].Value = cmbClass.Text.Trim();
                                cmd.Parameters["@find4"].Value = txtSubjectCode.Text.Trim();
                                cmd.Parameters["@find5"].Value = cmbAssessmentPeriod.Text.ToUpper().Trim();

                                rdr = cmd.ExecuteReader();

                                if (splashScreenManager1.IsSplashFormVisible == true)
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                                if (rdr.Read())
                                {
                                    XtraMessageBox.Show(LocRM.GetString("strMarksAlreadyAllocated"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    if ((rdr != null))
                                    {
                                        rdr.Close();
                                    }
                                    return;
                                }
                            }
                        }
                        
                    }
                    //start saving new marks
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving"));
                    }

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        //Create a transanction object, only commit to database if insert into CourseFeePayment and insert into CourseFeePayment_Join
                        // are succeful otherwise rollback.
                        SqlTransaction transaction = con.BeginTransaction();
                        try
                        {

                            string cb = "insert into MarksEntry(SchoolYear,Class,StudentNumber,SubjectCode,AssessmentPeriod,SubjectMaxima,MarksObtained ) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7)";
                            cmd = new SqlCommand(cb, con, transaction);
                            cmd.Connection = con;

                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.VarChar, 100, "SchoolYear"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.VarChar, 100, "Class"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.VarChar, 100, "StudentNumber"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 100, "SubjectCode"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 100, "AssessmentPeriod"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.Int, 100, "SubjectMaxima"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 100, "MarksObtained"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            for (int i = 1; i < gridView1.DataRowCount; i++) //start index at 1 because index 0 is used for maxima
                            {
                                if (!gridView1.IsNewItemRow(i))
                                {
                                    cmd.Parameters["@d1"].Value = cmbSchoolYear.Text;
                                    cmd.Parameters["@d2"].Value = cmbClass.Text;
                                    cmd.Parameters["@d3"].Value = gridView1.GetRowCellValue(i, LocRM.GetString("strStudentNo").ToUpper()).ToString();
                                    cmd.Parameters["@d4"].Value = txtSubjectCode.Text;
                                    cmd.Parameters["@d5"].Value = cmbAssessmentPeriod.Text;
                                    cmd.Parameters["@d6"].Value = Convert.ToInt16(gridView1.GetRowCellValue(0, cmbAssessmentPeriod.Text.ToUpper()));
                                    cmd.Parameters["@d7"].Value = Convert.ToInt16(gridView1.GetRowCellValue(i, cmbAssessmentPeriod.Text.ToUpper()));

                                    cmd.ExecuteNonQuery();
                                    //cmd.Parameters.Clear();
                                }
                            }
                            //commit the transanction
                            transaction.Commit();
                            //close the connection
                            con.Close();
                        }

                        catch (Exception ex)
                        {
                            //rollback the transanction form the pending state
                            transaction.Rollback();

                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                            if (ex is InvalidCastException)
                            {
                                XtraMessageBox.Show(LocRM.GetString("strErrorMarkSheetEmpty") + " " + "\n\r" + LocRM.GetString("strErrorGenerated") + ": " + ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                
                            }
                            else if (ex is SqlException)
                            {
                                XtraMessageBox.Show(LocRM.GetString("strErrorMarkSheetEmpty") + " " + "\n\r" + LocRM.GetString("strErrorGenerated") + ": " + ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            return;
                           
                        }
                    }

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    XtraMessageBox.Show(LocRM.GetString("strSuccessfullySaved"), LocRM.GetString("strSchoolMarks"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Log record transaction in logs
                    string st = LocRM.GetString("strNewMarks") + " " + LocRM.GetString("strFor") + ": " + LocRM.GetString("strForSubject") + ": " + gridLookUpSubject.Text + " " + LocRM.GetString("strHaveBeenSaved");
                    pf.LogFunc(Functions.PublicVariables.userLogged, st);

                    //Clear all controls
                    clearControls();
                    btnNew.Enabled = true;
                    btnSave.Enabled = false;
                }

                else
                {
                    XtraMessageBox.Show(LocRM.GetString("strLoadStudentsFirst"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnLoadStudents.Focus();
                    return;
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
        
        private void enableDisableColumns()
        {
            //ReadOnly and AllowEdit works only if gridview optionsBehavior Editable is set to True
            if (cmbAssessmentPeriod.SelectedIndex == 0) //enable str1eP
            {
                //gridView1.Columns[2].OptionsColumn.AllowEdit = true;
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("str1eP").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
               
            }
            else if (cmbAssessmentPeriod.SelectedIndex == 1) //enable str2eP
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("str2eP").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
                
            }
            else if (cmbAssessmentPeriod.SelectedIndex == 2) //enable strExam1
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("strExam1").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
                
            }
            else if (cmbAssessmentPeriod.SelectedIndex == 3) //enable str3eP
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("str3eP").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
               
            }
            else if (cmbAssessmentPeriod.SelectedIndex == 4) //enable str4eP
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("str4eP").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
               
            }
            else if (cmbAssessmentPeriod.SelectedIndex == 5) //enable strExam2
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("strExam2").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
                
            }
            else if (cmbAssessmentPeriod.SelectedIndex == 6) //enable str5eP
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("str5eP").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
               
            }
            else if (cmbAssessmentPeriod.SelectedIndex == 7) //enable str6eP
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("str6eP").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
               
            }
            else if (cmbAssessmentPeriod.SelectedIndex == 8) //enable strExam3
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {
                    if (column.FieldName == LocRM.GetString("strExam3").ToUpper())
                        column.OptionsColumn.ReadOnly = false;
                    else
                        column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
                
            }
            else  //disable all
            {
                gridView1.BeginUpdate();
                foreach (GridColumn column in gridView1.VisibleColumns)
                {                  
                  column.OptionsColumn.ReadOnly = true;
                }
                gridView1.EndUpdate();
                
            }
        }
        private void cmbAssessmentPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            enableDisableColumns();
        }
        //disable editing on top maxima row
        private void gridView1_ShownEditor(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            if (view.FocusedColumn.FieldName == LocRM.GetString("str1eP").ToUpper() && view.FocusedRowHandle == 0)
                view.ActiveEditor.Properties.ReadOnly = true;
            if (view.FocusedColumn.FieldName == LocRM.GetString("str2eP").ToUpper() && view.FocusedRowHandle == 0)
                view.ActiveEditor.Properties.ReadOnly = true;
            if (view.FocusedColumn.FieldName == LocRM.GetString("strExam1").ToUpper() && view.FocusedRowHandle == 0)
                view.ActiveEditor.Properties.ReadOnly = true;
            if (view.FocusedColumn.FieldName == LocRM.GetString("strTotal1").ToUpper() && view.FocusedRowHandle == 0)
                view.ActiveEditor.Properties.ReadOnly = true;
        }
        
        
        //https://docs.devexpress.com/WindowsForms/115548/controls-and-libraries/data-grid/appearance-and-conditional-formatting?utm_source=SupportCenter&utm_medium=website&utm_campaign=docs-feedback&utm_content=T989951#change-cell-and-row-appearances-dynamically
        private void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {    
            
            GridView view = sender as GridView;
            //Change Cell and Row Appearances Dynamically. Change font to Red when marks below 50%  
            try
            {
                if (e.Column.FieldName == LocRM.GetString("str1eP").ToUpper())
                {
                    string stringMark = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("str1eP").ToUpper()]);
                    int intMark = 0;
                    bool canConvert = int.TryParse(stringMark, out intMark);
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) > 0))
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
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) > 0))
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
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) > 0))
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
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) > 0))
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
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) > 0))
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
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) > 0))
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
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) >0))
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
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) > 0))
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
                    if ((canConvert == true) && (Convert.ToInt16(txtMaximaExam.Text) > 0))
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
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        bool formLoaded = false;
        private void userControlMarkSheet_VisibleChanged(object sender, EventArgs e)
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
                    CheckTeacherRole();
                }
            }
        }
        //fires once a user edits a cell value and attempts to leave this cell to validate data in cell
        //https://docs.devexpress.com/WindowsForms/DevExpress.XtraGrid.Views.Base.BaseView.ValidatingEditor?utm_source=SupportCenter&utm_medium=website&utm_campaign=docs-feedback&utm_content=T989952
        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            try
            {
                if ((gridView1.FocusedColumn.FieldName == LocRM.GetString("str1eP").ToUpper()) || 
                    (gridView1.FocusedColumn.FieldName == LocRM.GetString("str2eP").ToUpper()) ||
                    (gridView1.FocusedColumn.FieldName == LocRM.GetString("str3eP").ToUpper()) ||
                    (gridView1.FocusedColumn.FieldName == LocRM.GetString("str4eP").ToUpper()) ||
                    (gridView1.FocusedColumn.FieldName == LocRM.GetString("str5eP").ToUpper()) ||
                    (gridView1.FocusedColumn.FieldName == LocRM.GetString("str6eP").ToUpper()) )
                {
                    int cellValue = 0;
                    //chek if is numerical                    
                    if (!int.TryParse(e.Value as string, out cellValue))
                    {
                        e.Valid = false;
                        e.ErrorText = LocRM.GetString("strMarksNumericError");
                    }
                    //check if value above 0 and below or equal to maxima period                     
                    else if ((Convert.ToInt16(e.Value) < 0) || (Convert.ToInt16(e.Value) > Convert.ToInt16(txtMaximaPeriode.Text)))
                    {
                        e.Valid = false;
                        e.ErrorText = LocRM.GetString("strMarksValidValueError");
                    }
                    
                }
                if ((gridView1.FocusedColumn.FieldName == LocRM.GetString("strExam1").ToUpper()) ||
                    (gridView1.FocusedColumn.FieldName == LocRM.GetString("strExam2").ToUpper()) ||
                    (gridView1.FocusedColumn.FieldName == LocRM.GetString("strExam3").ToUpper()) )
                {
                    int cellValue = 0;
                    //chek if is numerical                    
                    if (!int.TryParse(e.Value as string, out cellValue))
                    {
                        e.Valid = false;
                        e.ErrorText = LocRM.GetString("strMarksNumericError");
                    }
                    //check if value above 0 and below or equal to maxima period                     
                    else if ((Convert.ToInt16(e.Value) < 0) || (Convert.ToInt16(e.Value) > Convert.ToInt16(txtMaximaExam.Text)))
                    {
                        e.Valid = false;
                        e.ErrorText = LocRM.GetString("strMarksValidValueError");
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //ColumnView view = sender as ColumnView;
            //GridColumn column = (e as EditFormValidateEditorEventArgs)?.Column ?? view.FocusedColumn;
            //if ((column.Name != LocRM.GetString("str1eP").ToUpper()) || (column.Name != LocRM.GetString("str2eP").ToUpper())) return;
            //if ((Convert.ToInt16(e.Value) < 0) || (Convert.ToInt16(e.Value) > Convert.ToInt16( txtMaximaPeriode.Text)))
            //    e.Valid = false;
        }
        

        private void btnGetMarks_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
                    gridControlMarkSheet.DataSource = null;

                    //refresh gridview to rebuildnew columns
                    gridView1.Columns.Clear();
                    gridView1.PopulateColumns();

                    if (cmbCycle.SelectedIndex >= 2)
                    {
                        //display culumns with 2 semesters
                        gridControlMarkSheet.DataSource = CreateDataBranchesSecondary();
                        gridView1.BestFitColumns();
                        gridView1.OptionsView.ColumnAutoWidth = true;
                    }
                    else
                    {
                        //display culumns with 3 trimesters
                        gridControlMarkSheet.DataSource = CreateDataBranchesPrimary();
                        gridView1.BestFitColumns();
                        gridView1.OptionsView.ColumnAutoWidth = true;
                    }                    

                    while ((rdr.Read() == true))
                    {
                        //add new  row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                        if (rowHandle==0)
                        {
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle).ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], "**********");
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], LocRM.GetString("strMaxima").ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], rdr.GetValue(9).ToString());

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

                    }
                    con.Close();
                }
                if (gridView1.DataRowCount > 0)
                {
                    if (txtTeacherUserType.Text.Trim() == "Administrator") //Show all subjects for this class as admin
                    {
                        btnUpdate.Enabled = true;
                        btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnUpdate.Enabled = false;
                        btnDelete.Enabled = false;
                    }
                }
                
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                enableDisableColumns();
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


            try
            {
                if (gridView1.DataRowCount > 0)
                {                    
                    //start saving new marks
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strUpdating"));
                    }

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        //Create a transanction object, only commit to database if insert into CourseFeePayment and insert into CourseFeePayment_Join
                        // are succeful otherwise rollback.
                        SqlTransaction transaction = con.BeginTransaction();
                        try
                        {

                            string cb = "update MarksEntry set SchoolYear=@d1, Class=@d2, StudentNumber=@d3, SubjectCode=@d4, " +
                                "AssessmentPeriod=@d5,SubjectMaxima=@d6,MarksObtained=@d7 " +
                                "where SchoolYear=@d1 and StudentNumber=@d3 and AssessmentPeriod=@d5 and SubjectCode=@d4";
                            cmd = new SqlCommand(cb, con, transaction);
                            cmd.Connection = con;

                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.VarChar, 100, "SchoolYear"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.VarChar, 100, "Class"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.VarChar, 100, "StudentNumber"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 100, "SubjectCode"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 100, "AssessmentPeriod"));
                            cmd.Parameters.Add(new SqlParameter("@d6", System.Data.SqlDbType.Int, 100, "SubjectMaxima"));
                            cmd.Parameters.Add(new SqlParameter("@d7", System.Data.SqlDbType.Int, 100, "MarksObtained"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            for (int i = 1; i < gridView1.DataRowCount; i++) //start index at 1 because index 0 is used for maxima
                            {
                                if (!gridView1.IsNewItemRow(i))
                                {
                                    cmd.Parameters["@d1"].Value = cmbSchoolYear.Text;
                                    cmd.Parameters["@d2"].Value = cmbClass.Text;
                                    cmd.Parameters["@d3"].Value = gridView1.GetRowCellValue(i, LocRM.GetString("strStudentNo").ToUpper()).ToString();
                                    cmd.Parameters["@d4"].Value = txtSubjectCode.Text;
                                    cmd.Parameters["@d5"].Value = cmbAssessmentPeriod.Text;
                                    cmd.Parameters["@d6"].Value = Convert.ToInt16(gridView1.GetRowCellValue(0, cmbAssessmentPeriod.Text.ToUpper()));
                                    cmd.Parameters["@d7"].Value = Convert.ToInt16(gridView1.GetRowCellValue(i, cmbAssessmentPeriod.Text.ToUpper()));

                                    cmd.ExecuteNonQuery();
                                    //cmd.Parameters.Clear();
                                }
                            }
                            //commit the transanction
                            transaction.Commit();
                            //close the connection
                            con.Close();
                        }

                        catch (Exception ex)
                        {
                            //rollback the transanction form the pending state
                            transaction.Rollback();

                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }

                            XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolMarks"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Log record transaction in logs
                    string st = LocRM.GetString("strSchoolMarks") + " " + LocRM.GetString("strFor")+ ": " + LocRM.GetString("strForSubject") + ": " + gridLookUpSubject.Text + " " + LocRM.GetString("strHaveBeenUpdated");
                    pf.LogFunc(Functions.PublicVariables.userLogged, st);

                    //Clear all controls
                    clearControls();
                    btnNew.Enabled = true;
                    btnUpdate.Enabled = false;
                }

                else
                {
                    XtraMessageBox.Show(LocRM.GetString("strLoadStudentsFirst"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnLoadStudents.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                if (ex is InvalidCastException)
                {
                    XtraMessageBox.Show(LocRM.GetString("strErrorMarkSheetEmpty") + " " + "\n\r" + LocRM.GetString("strErrorGenerated") + ": " + ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else if (ex is SqlException)
                {
                    XtraMessageBox.Show(LocRM.GetString("strErrorMarkSheetEmpty") + " " + "\n\r" + LocRM.GetString("strErrorGenerated") + ": " + ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }
        }
        int tot, totGen = 0;

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show(LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    DeleteRecord();

                //Clear all controls
                clearControls();
                btnNew.Enabled = true;
                btnDelete.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteRecord()
        {
            using (con = new SqlConnection(databaseConnectionString))
            {
                
                    int RowsAffected = 0;

                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                try
                {
                    string cq = "delete from MarksEntry where SchoolYear=@d1 and Class=@d2 and StudentNumber=@d3 and  " +
                        "SubjectCode=@d4 and AssessmentPeriod=@d5";
                    cmd = new SqlCommand(cq, con, transaction);
                    cmd.Connection = con;

                    // Add Parameters to Command Parameters collection
                    cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.VarChar, 100, "SchoolYear"));
                    cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.VarChar, 100, "Class"));
                    cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.VarChar, 100, "StudentNumber"));
                    cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 100, "SubjectCode"));
                    cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 100, "AssessmentPeriod"));

                    // Prepare command for repeated execution
                    cmd.Prepare();
                    // Data to be inserted
                    for (int i = 1; i < gridView1.DataRowCount; i++) //start index at 1 because index 0 is used for maxima
                    {
                        if (!gridView1.IsNewItemRow(i))
                        {
                            cmd.Parameters["@d1"].Value = cmbSchoolYear.Text;
                            cmd.Parameters["@d2"].Value = cmbClass.Text;
                            cmd.Parameters["@d3"].Value = gridView1.GetRowCellValue(i, LocRM.GetString("strStudentNo").ToUpper()).ToString();
                            cmd.Parameters["@d4"].Value = txtSubjectCode.Text;
                            cmd.Parameters["@d5"].Value = cmbAssessmentPeriod.Text;

                            RowsAffected = cmd.ExecuteNonQuery();
                        }
                    }
                    
                    if (RowsAffected > 0)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolMarks"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strSchoolMarks") + " " + LocRM.GetString("strForStudentsIn")   + " " + cmbClass.Text + " "+ LocRM.GetString("strOf") + " " + cmbAssessmentPeriod.Text  + " " +LocRM.GetString("strHaveBeenDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strSchoolMarks"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                    }
                    //commit the transanction
                    transaction.Commit();
                    if (con.State == ConnectionState.Open)
                        con.Close();


                }
                catch (Exception ex)
                {
                    //rollback the transanction form the pending state
                    transaction.Rollback();
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        //style on rows. set the Maxima row font black and bold
        private void gridView1_RowStyle(object sender, RowStyleEventArgs e)
        {          
            GridView view = sender as GridView;           
            if (e.RowHandle>=0)
            {
                string studentNo = view.GetRowCellDisplayText(e.RowHandle, view.Columns[LocRM.GetString("strStudentNo").ToUpper()]);
                if (studentNo== "**********")
                {
                    //e.Appearance.BackColor = Color.LightGreen;
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.FontStyleDelta = FontStyle.Bold;
                }
            }            
        }

        private void btnReportAssessmentPeriod_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            frmReportAssessmentPeriod frm = new frmReportAssessmentPeriod();
            frm.ShowDialog();
        }

        private void btnReportClassMarkSheet_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            frmReportClassMarkSheet frm = new frmReportClassMarkSheet();
            frm.ShowDialog();
        }

        private void btnConduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            frmStudentConduct frm = new frmStudentConduct();
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void userControlMarkSheet_Load(object sender, EventArgs e)
        {
            formLoaded = true;
            CheckTeacherRole();
        }
        private void CheckTeacherRole()
        {
            if ((Role == 1) || (Role == 2) || (Role == 13)) //Administrator, Administrator Assistant,Teacher Administrator
            {
                ribbonPageGroupConduct.Visible = true;
                btnReportClassMarkSheet.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                btnSupplementaryExam.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                ribbonPageGroupConduct.Visible = false;
                btnReportClassMarkSheet.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                btnSupplementaryExam.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void btnSupplementaryExam_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

         frmSupplementaryExam frm = new frmSupplementaryExam();
            frm.ShowDialog();
        }

        private void txtMaximaPeriode_TextChanged(object sender, EventArgs e)
        {
            tot =( 2 * Convert.ToInt16(txtMaximaPeriode.Text)) + Convert.ToInt16(txtMaximaExam.Text);
            txtTot.Text = tot.ToString();
            if (cmbCycle.SelectedIndex >= 2)
            {
                totGen = tot * 2;                
            }
            else
            {
                totGen = tot * 3;
            }
            txtTotGen.Text = totGen.ToString();

            if (txtMaximaPeriode.Text == "0")
            {
                return;
            }
            else
            {
                //select top maxima row, change maxima values then update
                int rowHandle = -1;
                ColumnView view = gridControlMarkSheet.MainView as ColumnView;
                GridColumn colMaxima = view.Columns[LocRM.GetString("str1eP").ToUpper()];
                rowHandle = view.LocateByDisplayText(rowHandle + 1, colMaxima, "*****");
                view.SelectRow(rowHandle);
                
                    if (cmbCycle.SelectedIndex >= 2)  //secondaire
                {
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strExam1").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], tot.ToString());

                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strExam2").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], tot.ToString());

                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], totGen.ToString());
                }
                else
                {
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str1eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str2eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strExam1").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strTotal1").ToUpper()], tot.ToString());

                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str3eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str4eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strExam2").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strTotal2").ToUpper()], tot.ToString());

                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str5eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("str6eP").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strExam3").ToUpper()], txtMaximaExam.Text);
                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strTotal3").ToUpper()], tot.ToString());

                    gridView1.SetRowCellValue(0, gridView1.Columns[LocRM.GetString("strGrandTotal").ToUpper()], totGen.ToString());

                }
                gridView1.UpdateCurrentRow();
            }
            
        }
    }
}
