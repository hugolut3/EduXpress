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
using System.Resources;
using DevExpress.XtraGrid.Columns;

namespace EduXpress.Education
{
    public partial class frmAssignSubjectsClasses : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmAssignSubjectsClasses).Assembly);
        public frmAssignSubjectsClasses()
        {
            InitializeComponent();

            //initialize gridlookupeditSubject
            gridLookUpSubject.Properties.DataSource = GetDataTableSubject();
            gridLookUpSubject.Properties.PopulateViewColumns();
            gridLookUpSubject.Properties.ValueMember = LocRM.GetString("strSubjectCode").ToUpper();
            gridLookUpSubject.Properties.DisplayMember = LocRM.GetString("strSubject").ToUpper();
            gridLookUpSubject.Properties.SearchMode = DevExpress.XtraEditors.Repository.GridLookUpSearchMode.AutoSuggest;
            gridLookUpSubject.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;

            //initialize branches grid
            gridControlAssignSubjects.DataSource = CreateDataBranches();
            gridView1.OptionsView.ColumnAutoWidth = false;
            gridView1.BestFitColumns();
        }

        public  DataTable GetDataTableSubject()  //public  static DataTable GetDataTableSubject() 
        {
            //ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmAssignSubjectsClasses).Assembly);
                        
            DataTable dtable = new DataTable();
            dtable.Columns.Add(LocRM.GetString("strSubjectCode").ToUpper(), typeof(string));
            dtable.Columns.Add(LocRM.GetString("strSubject").ToUpper(), typeof(string));
            dtable.Columns.Add(LocRM.GetString("strClassName").ToUpper(), typeof(string));
            dtable.Columns.Add(LocRM.GetString("strMaximaPeriod").ToUpper(), typeof(int));
            dtable.Columns.Add(LocRM.GetString("strMaximaExam").ToUpper(), typeof(int));

            string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
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
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT SubjectCode, RTRIM(SubjectName),RTRIM(Class)," +
                        "MaximaPeriode,MaximaExam FROM Subject order by SubjectName,Class", con);
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

                        string test = drow[1].ToString();
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
        private void reset()
        {
            //clear gridcontrol
            gridControlAssignSubjects.DataSource = null;
            //load datagrid
            gridControlAssignSubjects.DataSource = CreateDataBranches();
            chkFirstSubjectMaxima.Enabled = false;
            cmbCycle.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            gridLookUpSubject.Text = "";
            cmbTeacher.SelectedIndex = -1;
            chkFirstSubjectMaxima.Checked = false;
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            cmbCycle.Enabled= true;
            fillSection();
            reset();
            chkFirstSubjectMaxima.Enabled = true;
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

        //Autocomplete Option
        private void AutocompleteTeacher()
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
                    
                    cmd = new SqlCommand("SELECT Surname,Name,UserType  FROM Registration where UserType=@d1 ", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " UserType").Value = "Teacher";

                    adp = new SqlDataAdapter(cmd);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbTeacher.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbTeacher.Properties.Items.Add(drow[0].ToString() + " " + drow[1].ToString());
                    }
                    cmbTeacher.SelectedIndex = -1;
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

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
               AutocompleteTeacher();
            cmbTeacher.Enabled = true;
        }
       

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutocompleteClass();
            cmbClass.Enabled = true;            
        }

        private void gridLookUpSubject_EditValueChanged(object sender, EventArgs e)
        {
            
            GridLookUpEdit lookup = sender as GridLookUpEdit;
            DataRowView dataRowView = lookup.GetSelectedDataRow() as DataRowView;
            if (dataRowView != null)
            {
                DataRow row = dataRowView.Row;
                txtMaximaPeriode.Text = row[LocRM.GetString("strMaximaPeriod").ToUpper()].ToString();
                
                txtMaximaExam.Text = row[LocRM.GetString("strMaximaExam").ToUpper()].ToString();
                txtClass.Text = row[LocRM.GetString("strClassName").ToUpper()].ToString();
                
            }
        }

       
        private void frmAssignSubjectsClasses_Load(object sender, EventArgs e)
        {
            gridLookUpSubject.ForceInitialize();
            gridLookUpEdit1View.BestFitColumns();
        }
                
        private DataTable CreateDataBranches()
        {
            DataTable dt = new DataTable();           
            dt.Columns.Add(LocRM.GetString("strBranches").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strDailyWork").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strExam").ToUpper(), typeof(string));            
            return dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string maxima = "MAXIMA";
            
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (chkFirstSubjectMaxima.Checked)
                {
                    //add new product row and add Maxima
                    gridView1.AddNewRow();
                    gridView1.UpdateCurrentRow();
                    int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], maxima);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDailyWork").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam").ToUpper()], txtMaximaExam.Text);

                    //add new product row and add Subject
                    gridView1.AddNewRow();
                    gridView1.UpdateCurrentRow();
                    int rowHandle2 = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                    gridView1.SetRowCellValue(rowHandle2, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], gridLookUpSubject.Text);
                    gridView1.SetRowCellValue(rowHandle2, gridView1.Columns[LocRM.GetString("strDailyWork").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle2, gridView1.Columns[LocRM.GetString("strExam").ToUpper()], txtMaximaExam.Text);
                }
                else
                {
                    //add new product row
                    gridView1.AddNewRow();
                    gridView1.UpdateCurrentRow();                   
                    int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], gridLookUpSubject.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDailyWork").ToUpper()], txtMaximaPeriode.Text);
                    gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam").ToUpper()], txtMaximaExam.Text);
                    
                }
                gridLookUpSubject.Text = "";                
                chkFirstSubjectMaxima.Checked = false;

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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }
    }
}