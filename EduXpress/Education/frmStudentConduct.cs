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
using static EduXpress.Functions.PublicVariables;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;

namespace EduXpress.Education
{
    public partial class frmStudentConduct : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        PublicFunctions pf = new PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmStudentConduct).Assembly);


        public frmStudentConduct()
        {
            InitializeComponent();
        }
        bool formLoaded = false;
        private void frmStudentConduct_Load(object sender, EventArgs e)
        {
           formLoaded = true;
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
            gridControlStudentConduct.DataSource = null;

            cmbAssessmentPeriod.SelectedIndex = -1;
            cmbAssessmentPeriod.Properties.Items.Clear();
        }

        private void frmStudentConduct_VisibleChanged(object sender, EventArgs e)
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
                LocRM.GetString("str3eP"),
                LocRM.GetString("str4eP") 
                
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
                LocRM.GetString("str3eP"),
                LocRM.GetString("str4eP") ,                
                LocRM.GetString("str5eP"),
                LocRM.GetString("str6eP") 
               
                });
            }
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
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

        private DataTable CreateDataBranchesPrimary()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(LocRM.GetString("strNo").ToUpper(), typeof(int));
            dt.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("strNameSurname").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str1eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str2eP").ToUpper(), typeof(string));            

            dt.Columns.Add(LocRM.GetString("str3eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str4eP").ToUpper(), typeof(string));            

            dt.Columns.Add(LocRM.GetString("str5eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str6eP").ToUpper(), typeof(string));
          
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

            dt.Columns.Add(LocRM.GetString("str3eP").ToUpper(), typeof(string));
            dt.Columns.Add(LocRM.GetString("str4eP").ToUpper(), typeof(string));
            
            return dt;
        }
        //embed comboedit in gridview
        private void comboInGrid()
        {
            RepositoryItemComboBox riComboBox = new RepositoryItemComboBox();


            //load conduites
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
                    adp.SelectCommand = new SqlCommand("SELECT  Abbreviation FROM Conduct", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];

                    riComboBox.Items.Clear();
                    
                    foreach (DataRow drow in dtable.Rows)
                    {
                        riComboBox.Items.Add(drow[0].ToString());                       
                    }
                    //riComboBox.sele.SelectedIndex = -1;
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


            gridControlStudentConduct.RepositoryItems.Add(riComboBox);
            if (isSemester)
            {               
                
                gridView1.Columns[3].ColumnEdit = riComboBox;
                gridView1.Columns[4].ColumnEdit = riComboBox;
                gridView1.Columns[5].ColumnEdit = riComboBox;
                gridView1.Columns[6].ColumnEdit = riComboBox;
            }
            else
            {
                gridView1.Columns[3].ColumnEdit = riComboBox;
                gridView1.Columns[4].ColumnEdit = riComboBox;
                gridView1.Columns[5].ColumnEdit = riComboBox;
                gridView1.Columns[6].ColumnEdit = riComboBox;
                gridView1.Columns[7].ColumnEdit = riComboBox;
                gridView1.Columns[8].ColumnEdit = riComboBox;
            }
            

            gridView1.BestFitColumns();
           
        }
        bool isSemester = false;
        private void btnLoadStudents_Click(object sender, EventArgs e)
        {
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

            try
            {
                //clear gridcontrol
                gridControlStudentConduct.DataSource = null;

                //refresh gridview to rebuildnew columns
                gridView1.Columns.Clear();
                gridView1.PopulateColumns();

                if (cmbCycle.SelectedIndex >= 2)
                {
                    //display culumns with 2 semesters
                    gridControlStudentConduct.DataSource = CreateDataBranchesSecondary();
                    gridView1.BestFitColumns();
                    gridView1.OptionsView.ColumnAutoWidth = true;
                    isSemester = true;
                }
                else
                {
                    //display culumns with 3 trimesters
                    gridControlStudentConduct.DataSource = CreateDataBranchesPrimary();
                    gridView1.BestFitColumns();
                    gridView1.OptionsView.ColumnAutoWidth = true;
                    isSemester = false;
                }

                comboInGrid();
                //add Maxima row
                //  addSubjectGrid();

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
            
            else if (cmbAssessmentPeriod.SelectedIndex == 2) //enable str3eP
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
            else if (cmbAssessmentPeriod.SelectedIndex == 3) //enable str4eP
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
            
            else if (cmbAssessmentPeriod.SelectedIndex == 4) //enable str5eP
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
            else if (cmbAssessmentPeriod.SelectedIndex == 5) //enable str6eP
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
                                string ct = "select SchoolYear, Class, StudentNumber,AssessmentPeriod " +
                                    "from ConductsEntry where SchoolYear=@find1 and StudentNumber=@find2 and " +
                                    "Class=@find3 and AssessmentPeriod=@find4";

                                cmd = new SqlCommand(ct);
                                cmd.Connection = con;
                                cmd.Parameters.Add(new SqlParameter("@find1", System.Data.SqlDbType.VarChar, 100, "SchoolYear"));
                                cmd.Parameters.Add(new SqlParameter("@find2", System.Data.SqlDbType.VarChar, 100, "StudentNumber"));
                                cmd.Parameters.Add(new SqlParameter("@find3", System.Data.SqlDbType.VarChar, 100, "Class"));                                
                                cmd.Parameters.Add(new SqlParameter("@find4", System.Data.SqlDbType.VarChar, 100, "AssessmentPeriod"));

                                cmd.Parameters["@find1"].Value = cmbSchoolYear.Text.Trim();
                                cmd.Parameters["@find2"].Value = gridView1.GetRowCellValue(i, LocRM.GetString("strStudentNo").ToUpper()).ToString();
                                cmd.Parameters["@find3"].Value = cmbClass.Text.Trim();                                
                                cmd.Parameters["@find4"].Value = cmbAssessmentPeriod.Text.ToUpper().Trim();

                                rdr = cmd.ExecuteReader();

                                if (splashScreenManager1.IsSplashFormVisible == true)
                                {
                                    splashScreenManager1.CloseWaitForm();
                                }
                                if (rdr.Read())
                                {
                                    XtraMessageBox.Show(LocRM.GetString("strConductsAlreadyAllocated"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);

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

                            string cb = "insert into ConductsEntry(SchoolYear,Class,StudentNumber,Conduct,AssessmentPeriod ) VALUES (@d1,@d2,@d3,@d4,@d5)";
                            cmd = new SqlCommand(cb, con, transaction);
                            cmd.Connection = con;

                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.VarChar, 100, "SchoolYear"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.VarChar, 100, "Class"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.VarChar, 100, "StudentNumber"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 100, "Conduct"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 100, "AssessmentPeriod"));                           

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            for (int i = 0; i < gridView1.DataRowCount; i++) 
                            {
                                if (!gridView1.IsNewItemRow(i))
                                {
                                    cmd.Parameters["@d1"].Value = cmbSchoolYear.Text;
                                    cmd.Parameters["@d2"].Value = cmbClass.Text;
                                    cmd.Parameters["@d3"].Value = gridView1.GetRowCellValue(i, LocRM.GetString("strStudentNo").ToUpper()).ToString();
                                    cmd.Parameters["@d4"].Value = gridView1.GetRowCellValue(i, cmbAssessmentPeriod.Text.ToUpper());
                                    cmd.Parameters["@d5"].Value = cmbAssessmentPeriod.Text; 

                                    cmd.ExecuteNonQuery();
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
                                XtraMessageBox.Show(LocRM.GetString("strErrorConductSheetEmpty") + " " + "\n\r" + LocRM.GetString("strErrorGenerated") + ": " + ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    XtraMessageBox.Show(LocRM.GetString("strSuccessfullySaved"), LocRM.GetString("strStudentConducts"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Log record transaction in logs
                    string st = LocRM.GetString("strNewConduct") + " " + LocRM.GetString("strFor") + ": " + LocRM.GetString("strStudents") + LocRM.GetString("strHaveBeenSaved");
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

                            string cb = "update ConductsEntry set Class=@d2,Conduct=@d4 where SchoolYear=@d1 and StudentNumber=@d3 and AssessmentPeriod=@d5";
                            cmd = new SqlCommand(cb, con, transaction);
                            cmd.Connection = con;

                            // Add Parameters to Command Parameters collection
                            cmd.Parameters.Add(new SqlParameter("@d1", System.Data.SqlDbType.VarChar, 100, "SchoolYear"));
                            cmd.Parameters.Add(new SqlParameter("@d2", System.Data.SqlDbType.VarChar, 100, "Class"));
                            cmd.Parameters.Add(new SqlParameter("@d3", System.Data.SqlDbType.VarChar, 100, "StudentNumber"));
                            cmd.Parameters.Add(new SqlParameter("@d4", System.Data.SqlDbType.VarChar, 100, "Conduct"));
                            cmd.Parameters.Add(new SqlParameter("@d5", System.Data.SqlDbType.VarChar, 100, "AssessmentPeriod"));

                            // Prepare command for repeated execution
                            cmd.Prepare();
                            // Data to be inserted
                            for (int i = 0; i < gridView1.DataRowCount; i++) 
                            {
                                if (!gridView1.IsNewItemRow(i))
                                {
                                    cmd.Parameters["@d1"].Value = cmbSchoolYear.Text;
                                    cmd.Parameters["@d2"].Value = cmbClass.Text;
                                    cmd.Parameters["@d3"].Value = gridView1.GetRowCellValue(i, LocRM.GetString("strStudentNo").ToUpper()).ToString();
                                    cmd.Parameters["@d4"].Value = gridView1.GetRowCellValue(i, cmbAssessmentPeriod.Text.ToUpper());
                                    cmd.Parameters["@d5"].Value = cmbAssessmentPeriod.Text;

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
                                XtraMessageBox.Show(LocRM.GetString("strErrorConductSheetEmpty") + " " + "\n\r" + LocRM.GetString("strErrorGenerated") + ": " + ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strStudentConducts"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Log record transaction in logs
                    string st = LocRM.GetString("strStudentConducts") + " " + LocRM.GetString("strFor") + ": " + LocRM.GetString("strStudents")  + LocRM.GetString("strHaveBeenUpdated");
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

                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGetConducts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

            enableDisableColumns();
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
                        "Students.SchoolYear,Students.Class, Cycle,Section,ConductsEntry.Conduct,ConductsEntry.AssessmentPeriod" +
                        " from Students,ConductsEntry where Students.StudentNumber= ConductsEntry.StudentNumber and " +
                        " Students.Class= ConductsEntry.Class and ConductsEntry.SchoolYear= @d1 and ConductsEntry.Class= @d2 " +
                        "and ConductsEntry.AssessmentPeriod= @d3 order by Students.StudentSurname";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSchoolYear.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbClass.Text.Trim());                    
                    cmd.Parameters.AddWithValue("@d3", cmbAssessmentPeriod.Text.Trim());

                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection); //CommandBehavior.CloseConnection usefull when you use a connection over and over again in a loop, the connection will remain "open" until the Garbage collector picks it up, and only then will it be released back to the ADO.net Connection pool to be re-used

                    //clear gridcontrol
                    gridControlStudentConduct.DataSource = null;

                    //refresh gridview to rebuildnew columns
                    gridView1.Columns.Clear();
                    gridView1.PopulateColumns();

                    if (cmbCycle.SelectedIndex >= 2)
                    {
                        //display culumns with 2 semesters
                        gridControlStudentConduct.DataSource = CreateDataBranchesSecondary();
                        gridView1.BestFitColumns();
                        gridView1.OptionsView.ColumnAutoWidth = true;
                    }
                    else
                    {
                        //display culumns with 3 trimesters
                        gridControlStudentConduct.DataSource = CreateDataBranchesPrimary();
                        gridView1.BestFitColumns();
                        gridView1.OptionsView.ColumnAutoWidth = true;
                    }

                    while ((rdr.Read() == true))
                    {
                            int rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);
                       
                            //add new  row
                            gridView1.AddNewRow();
                            gridView1.UpdateCurrentRow();
                            rowHandle = gridView1.GetVisibleRowHandle(gridView1.RowCount - 1);

                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNo").ToUpper()], (rowHandle).ToString());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strStudentNo").ToUpper()], rdr[0].ToString().ToUpper());
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[LocRM.GetString("strNameSurname").ToUpper()], rdr[1].ToString().ToUpper() + " " + rdr[2].ToString().ToUpper()); //merge surname and name
                            gridView1.SetRowCellValue(rowHandle, gridView1.Columns[cmbAssessmentPeriod.Text.ToUpper()], rdr.GetValue(7).ToString());
                        
                    }
                    con.Close();
                }
                if (gridView1.DataRowCount > 0)
                {
                    if (txtTeacherUserType.Text.Trim() == "Administrator") //Allow delete or update only for admins
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
    }
}