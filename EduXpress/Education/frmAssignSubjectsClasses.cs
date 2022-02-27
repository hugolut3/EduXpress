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
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
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
            gridView1.OptionsView.ColumnAutoWidth = true;
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

            //string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
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
           // gridControlAssignSubjects.DataSource = null;
            //load datagrid
           // gridControlAssignSubjects.DataSource = CreateDataBranches();
            
            cmbCycle.SelectedIndex = -1;
            cmbCycle.Enabled = false;
            cmbSection.SelectedIndex = -1;
            cmbSection.Enabled = false;
            cmbClass.SelectedIndex = -1;
            cmbClass.Enabled = false;
            gridLookUpSubject.Text = "";
            cmbTeacher.SelectedIndex = -1;
            cmbTeacher.Enabled = false;
           
            txtSubjectPositionBulletin.Text = "";
            txtSubjectPositionBulletin.Enabled = false;

            txtMaximaPeriode.Text = "";
            txtMaximaExam.Text = "";
            txtClass.Text = "";
            txtSubjectCode.Text = "";

            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            cmbCycle.Enabled= true;
            fillSection();          
            
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
                    
                    cmd = new SqlCommand("SELECT Surname,Name,UserType, UserNameID  FROM Registration where UserType=@d1 ", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " UserType").Value = "Teacher";

                    adp = new SqlDataAdapter(cmd);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbTeacher.Properties.Items.Clear();
                    cmbTeacherID.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbTeacher.Properties.Items.Add(drow[0].ToString() + " " + drow[1].ToString());
                        cmbTeacherID.Properties.Items.Add(drow[3].ToString());
                    }
                    cmbTeacher.SelectedIndex = -1;
                    cmbTeacherID.SelectedIndex = -1;
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
                txtSubjectCode.Text = row[LocRM.GetString("strSubjectCode").ToUpper()].ToString();
                txtSubjectPositionBulletin.Enabled = true;
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
            dt.Columns.Add(LocRM.GetString("strPosition").ToUpper(), typeof(int));
            dt.Columns.Add(LocRM.GetString("strBranches").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strDailyWork").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strExam").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strClassName").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strTeacher").ToUpper(), typeof(string));
            return dt;
        }
        private void addSubjectGrid()
        {
           
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                    //add new product row and add Subject
                gridView1.AddNewRow();
                gridView1.UpdateCurrentRow();
                int rowHandle1 = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                gridView1.SetRowCellValue(rowHandle1, gridView1.Columns[LocRM.GetString("strPosition").ToUpper()],Convert.ToInt16( txtSubjectPositionBulletin.Text));
                gridView1.SetRowCellValue(rowHandle1, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], gridLookUpSubject.Text);
                gridView1.SetRowCellValue(rowHandle1, gridView1.Columns[LocRM.GetString("strDailyWork").ToUpper()], txtMaximaPeriode.Text);
                gridView1.SetRowCellValue(rowHandle1, gridView1.Columns[LocRM.GetString("strExam").ToUpper()], txtMaximaExam.Text);
                gridView1.SetRowCellValue(rowHandle1, gridView1.Columns[LocRM.GetString("strClassName").ToUpper()], cmbClass.Text);
                gridView1.SetRowCellValue(rowHandle1, gridView1.Columns[LocRM.GetString("strTeacher").ToUpper()], cmbTeacher.Text);
                
                gridLookUpSubject.Text = "";                

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
        

        private void removeSubjectGrid()
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }
        

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
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

            if (cmbTeacher.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectTeacher"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTeacher.Focus();
                return;
            }

            if (txtSubjectPositionBulletin.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterPositionNoBulletin"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubjectPositionBulletin.Focus();
                return;
            }

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select SubjectCode from SubjectAssignment where SubjectCode=@find";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.VarChar, 100, "SubjectCode"));
                    cmd.Parameters["@find"].Value = txtSubjectCode.Text.Trim();
                    rdr = cmd.ExecuteReader();
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSubjectAllocatedExist"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        gridLookUpSubject.Focus();

                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        return;
                    }
                }

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "insert into SubjectAssignment(Cycle,Class,SubjectCode,InstructorID,SubjectPositionBulletin ) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d2", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d3", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d4", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@d5", System.Data.SqlDbType.Int);

                    cmd.Parameters["@d1"].Value = cmbCycle.Text.Trim();
                    cmd.Parameters["@d2"].Value = cmbClass.Text.Trim();
                    cmd.Parameters["@d3"].Value = txtSubjectCode.Text.Trim();
                    cmd.Parameters["@d4"].Value = Convert.ToInt16(cmbTeacherID.Text.Trim());
                    cmd.Parameters["@d5"].Value = Convert.ToInt16(txtSubjectPositionBulletin.Text.Trim());
                    cmd.ExecuteNonQuery();

                    con.Close();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(LocRM.GetString("strSubjectAllocated"), LocRM.GetString("strSchoolSubjectsAllocation"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strNewSubject") + ": " + gridLookUpSubject.Text + " " + LocRM.GetString("strHasBeenAllocatedClass") + " " + cmbClass.Text;
                pf.LogFunc(Functions.PublicVariables.userLogged, st);

                addSubjectGrid();
                //Clear all controls
                reset();
              
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

        private void cmbTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbTeacherID.SelectedIndex = cmbTeacher.SelectedIndex;
        }

        
       
        private void loadDatabyTeacher()
        {
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
                    string ct = "SELECT SubjectPositionBulletin,SubjectAssignment.SubjectCode, SubjectAssignment.Class," +
                        "InstructorID ,SubjectName, MaximaPeriode,MaximaExam, Surname, Name,UserNameID " +
                        "from SubjectAssignment,Subject,Registration where SubjectAssignment.SubjectCode=Subject.SubjectCode and " +
                        "InstructorID=UserNameID and InstructorID=@d1 order by Class,SubjectPositionBulletin";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.Int, 60, "InstructorID").Value =Convert.ToInt16(cmbTeacherID.Text.Trim());

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPosition").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], rdr[4].ToString());

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDailyWork").ToUpper()], rdr[5].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam").ToUpper()], rdr[6].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClassName").ToUpper()], rdr[2].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTeacher").ToUpper()], rdr[7].ToString() + " " + rdr[8].ToString());                       

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

        private void btnLoadSubjectsTeacher_Click(object sender, EventArgs e)
        {           

            if (cmbTeacher.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectTeacher"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTeacher.Focus();
                return;
            }
            //clear gridcontrol
            gridControlAssignSubjects.DataSource = null;
            //load datagrid
            gridControlAssignSubjects.DataSource = CreateDataBranches();

            loadDatabyTeacher();
        }
        private void loadDatabyClass()
        {
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
                    string ct = "SELECT SubjectPositionBulletin,SubjectAssignment.SubjectCode, SubjectAssignment.Class," +
                        "InstructorID ,SubjectName, MaximaPeriode,MaximaExam, Surname, Name,UserNameID " +
                        "from SubjectAssignment,Subject,Registration where SubjectAssignment.SubjectCode=Subject.SubjectCode and " +
                        "InstructorID=UserNameID and SubjectAssignment.Class=@d1 order by Class,SubjectPositionBulletin";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, "SubjectAssignment.Class").Value = cmbClass.Text.Trim();

                    rdr = cmd.ExecuteReader();

                    while ((rdr.Read() == true))
                    {
                        //add new product row
                        gridView1.AddNewRow();
                        gridView1.UpdateCurrentRow();

                        var row = gridView1.GetRow(gridView1.GetVisibleRowHandle(gridView1.RowCount - 1));
                        int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strPosition").ToUpper()], rdr[0].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strBranches").ToUpper()], rdr[4].ToString());

                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strDailyWork").ToUpper()], rdr[5].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strExam").ToUpper()], rdr[6].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strClassName").ToUpper()], rdr[2].ToString());
                        gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strTeacher").ToUpper()], rdr[7].ToString() + " " + rdr[8].ToString());

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

        private void btnLoadSubjectsClass_Click(object sender, EventArgs e)
        {
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            //clear gridcontrol
             gridControlAssignSubjects.DataSource = null;
            //load datagrid
             gridControlAssignSubjects.DataSource = CreateDataBranches();

            loadDatabyClass();
        }

        private void gridControlAssignSubjects_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (gridView1.DataRowCount > 0)
                {
                    txtClass.Enabled = true;
                    txtClass.Text = "";
                    cmbCycle.Enabled = true;
                    txtSubjectPositionBulletin.Enabled = true;
                    fillSection();                    

                    txtSubjectPositionBulletin.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strPosition").ToUpper()).ToString();
                    gridLookUpSubject.EditValue = gridView1.GetFocusedRowCellValue(LocRM.GetString("strBranches").ToUpper()).ToString();
                    cmbTeacher.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strTeacher").ToUpper()).ToString();
                    cmbClass.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strClassName").ToUpper()).ToString();
                    
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                    btnNew.Enabled = true;

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

            if (cmbTeacher.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectTeacher"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTeacher.Focus();
                return;
            }

            if (txtSubjectPositionBulletin.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterPositionNoBulletin"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubjectPositionBulletin.Focus();
                return;
            }

            try
            {
                
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "Update SubjectAssignment set Cycle=@d1, Class=@d2, InstructorID=@d3, SubjectPositionBulletin=@d4 where SubjectCode=@d5";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    
                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d2", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d3", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@d4", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@d5", System.Data.SqlDbType.VarChar);

                    cmd.Parameters["@d1"].Value = cmbCycle.Text.Trim();
                    cmd.Parameters["@d2"].Value = cmbClass.Text.Trim();                    
                    cmd.Parameters["@d3"].Value = Convert.ToInt16(cmbTeacherID.Text.Trim());
                    cmd.Parameters["@d4"].Value = Convert.ToInt16(txtSubjectPositionBulletin.Text.Trim());
                    cmd.Parameters["@d5"].Value = txtSubjectCode.Text.Trim();
                    cmd.ExecuteNonQuery();

                    con.Close();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(LocRM.GetString("strUpdatedSuccessfully"), LocRM.GetString("strSchoolSubjectsAllocation"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strSubject") + ": " + gridLookUpSubject.Text + " " + LocRM.GetString("strHasBeenUpdated") ;
                pf.LogFunc(Functions.PublicVariables.userLogged, st);

              //  addSubjectGrid();
                //Clear all controls
                reset();

                 //clear gridcontrol
                 gridControlAssignSubjects.DataSource = null;
                //load datagrid
                // gridControlAssignSubjects.DataSource = CreateDataBranches();

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

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show(LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    DeleteRecord();
                reset();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteRecord()
        {
            try
            {
                int RowsAffected = 0;
                
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cq = "delete from SubjectAssignment where SubjectCode=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtSubjectCode.Text.Trim());
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolSubjectsAllocation"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strSubject") + ": " + gridLookUpSubject.Text + " " + LocRM.GetString("strSuccessfullyDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        //Clear all controls
                        reset();

                        //clear gridcontrol
                        gridControlAssignSubjects.DataSource = null;

                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strSchoolSections"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                    }
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}