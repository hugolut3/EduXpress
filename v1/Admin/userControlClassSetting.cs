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
using System.Resources;

namespace EduXpress.Admin
{
    public partial class userControlClassSetting : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlClassSetting).Assembly);

        public userControlClassSetting()
        {
            InitializeComponent();
        }
        private void FillSection()
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
                    cbSection.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cbSection.Properties.Items.Add(drow[0].ToString());
                    }
                    cbSection.SelectedIndex = -1;
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
        private void FillSectionAbbreviation()
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
                    string ct = "select SectionNameAbbreviation from Sections where SectionName=@d1";
                    cmd = new SqlCommand(ct);
                    cmd.Parameters.AddWithValue("@d1", cbSection.Text.Trim());
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        txtSectionAbbreviation.Text = rdr.GetValue(0).ToString();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        return;
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
        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            enable();
            fillCycle();
            btnSaveClass.Enabled = true;
            btnAdd.Enabled = false;
        }
        //Fill cmbcycle
        private void fillCycle()
        {
            cbCycle.Properties.Items.Clear();
            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                cbCycle.Properties.Items.AddRange(new object[] 
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
                cbCycle.Properties.Items.AddRange(new object[] 
                {
                    LocRM.GetString("strPrePrimary"),
                    LocRM.GetString("strPreparatory") ,
                    LocRM.GetString("strHighSchoolGET"),
                    LocRM.GetString("strHighSchoolFET"),
                    LocRM.GetString("strTVETCollege")
                });
            }
           
            
        }
        private void reset()
        {
            txtClassName.Text = "";
            txtClassName.Properties.ReadOnly = true;
            cbCycle.SelectedIndex = -1;
            cbSection.SelectedIndex = -1;
            txtSectionAbbreviation.Text = "";
            txtClass.Text = "";
            txtClass.Enabled = false;
            txtCapacity.Text = "";
            txtCapacity.Enabled = false;
        }

        private void enable()
        {
            cbCycle.Enabled = true;
        }
        private void disable()
        {
            cbCycle.Enabled = false;
        }

        private void btnSaveClass_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtClass.Text == "") 
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterClassName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClass.Focus();
                return;
            }
            if ((txtCapacity.Text == "") || (txtCapacity.Text == "0"))
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterClassCapacity"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCapacity.Focus();
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
                    string ct = "select ClassName from Classes where ClassName=@find";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.VarChar, 30, "ClassName"));
                    cmd.Parameters["@find"].Value = txtClassName.Text.Trim();
                    rdr = cmd.ExecuteReader();
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strClassExist"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtClass.Text = "";
                        txtClass.Focus();

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

                    string cb = "insert into Classes(ClassName,SectionName,SectionAbrev,Cycle,Capacity ) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d2", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d3", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d4", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d5", System.Data.SqlDbType.TinyInt);

                    cmd.Parameters["@d1"].Value = txtClassName.Text.Trim();
                    cmd.Parameters["@d2"].Value = cbSection.Text.Trim();
                    cmd.Parameters["@d3"].Value = txtSectionAbbreviation.Text.Trim();
                    cmd.Parameters["@d4"].Value = cbCycle.Text.Trim();
                    cmd.Parameters["@d5"].Value = txtCapacity.Text.Trim();
                    cmd.ExecuteNonQuery();

                    con.Close();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(LocRM.GetString("strNewClassSaved"), LocRM.GetString("strSchoolClasses"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strNewClass") +": " + txtClassName.Text + " " + LocRM.GetString("strHasBeenSaved");
                pf.LogFunc(Functions.PublicVariables.userLogged, st); 

                //Clear all controls
                reset();
                btnSaveClass.Enabled = false;
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;                
                gridControlClasses.DataSource = Getdata();
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

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void btnNewSection_Click(object sender, EventArgs e)
        {
            Students.frmSection frm = new Students.frmSection();
            frm.ShowDialog();
            FillSection();
        }
        private void cbCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbSection.SelectedIndex = -1;
            txtClassName.Text = "";
            txtClass.Text = "";
            txtSectionAbbreviation.Text = "";
            txtClass.Enabled = true;
            txtCapacity.Text = "";
            txtCapacity.Enabled = true;
            //if (cbSection.Properties.Items.Count < 1)
            //{
            //    splashScreenManager1.ShowWaitForm();
            //    FillSection();
            //    splashScreenManager1.CloseWaitForm();
            //}
            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (cbCycle.SelectedIndex < 2 )
                {
                    cbSection.Enabled = false;
                }
                else  //C.O. and Humanites
                {
                    cbSection.Enabled = true;
                    FillSection();
                }
            }
            else
            {
                if (cbCycle.SelectedIndex < 4)
                {
                    cbSection.Enabled = false;
                }
                else  //only College
                {
                    cbSection.Enabled = true;
                    FillSection();
                }
            }
            
        }
        private void cbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtClassName.Text = "";
            txtClass.Text = "";
            txtSectionAbbreviation.Text = "";
            FillSectionAbbreviation();
        }

        private void userControlClassSetting_Load(object sender, EventArgs e)
        {
            reset();
            disable();
            btnAdd.Enabled = true;
            btnSaveClass.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            gridControlClasses.DataSource = Getdata();
        }

        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(databaseConnectionString);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        //Fill dataGridView
        public DataView Getdata()
        {
          
            string ClassColumn = LocRM.GetString("strClass").ToUpper();
            string CycleColumn = LocRM.GetString("strCycle").ToUpper();
            string SectionColumn = LocRM.GetString("strSection").ToUpper(); 
            string SectionAbbreviationColumn = LocRM.GetString("strSectionAbbreviation").ToUpper(); 
            string CapacityColumn = LocRM.GetString("strCapacity").ToUpper();

            dynamic SelectQry = "SELECT  RTRIM(ClassName) as [" + ClassColumn + "], RTRIM(SectionName) as [" + SectionColumn + "], " +
                "RTRIM(SectionAbrev) as [" + SectionAbbreviationColumn + "], " +
                "RTRIM(Cycle) as [" + CycleColumn + "], Capacity as [" + CapacityColumn + "]  FROM Classes order by ClassName";
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

        private void txtClass_TextChanged(object sender, EventArgs e)
        {
            txtClassName.Text = txtClass.Text + " " + txtSectionAbbreviation.Text;
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterClassName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClass.Focus();
                return;
            }

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strUpdating"));
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    //Create a transanction object, only commit to database if update into Classes and
                    //Students are succeful otherwise rollback. 
                    SqlTransaction transaction = con.BeginTransaction();

                    //execute 1st query
                    string ct1 = "Update Classes set ClassName=@d1,SectionName=@d2,SectionAbrev=@d3 ,Cycle=@d4,Capacity=@d5 where ClassName=@d6";

                    cmd = new SqlCommand(ct1, con, transaction);
                   // cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtClassName.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cbSection.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", txtSectionAbbreviation.Text.Trim());
                    cmd.Parameters.AddWithValue("@d4", cbCycle.Text.Trim());
                    cmd.Parameters.AddWithValue("@d5", txtCapacity.Text.Trim());
                    cmd.Parameters.AddWithValue("@d6", txtID.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    //close the reader
                    if (rdr != null)
                    {
                        rdr.Close();
                    }

                    //execute 2nd query
                    string ct2 = "Update Students set Class=@d1,Section=@d2 ,Cycle=@d4 where Class=@d5";
                    cmd = new SqlCommand(ct2, con, transaction);
                    cmd.Parameters.AddWithValue("@d1", txtClassName.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cbSection.Text.Trim());                    
                    cmd.Parameters.AddWithValue("@d4", cbCycle.Text.Trim());                   
                    cmd.Parameters.AddWithValue("@d5", txtID.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    //close the reader
                    if (rdr != null)
                    {
                        rdr.Close();
                    }

                    //execute 3rd query
                    string ct3 = "Update SubjectAssignment set Class=@d1, Cycle=@d2 where Class=@d3";
                    cmd = new SqlCommand(ct3, con, transaction);
                    cmd.Parameters.AddWithValue("@d1", txtClassName.Text.Trim());                    
                    cmd.Parameters.AddWithValue("@d2", cbCycle.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", txtID.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    //close the reader
                    if (rdr != null)
                    {
                        rdr.Close();
                    }

                    //execute 4th query
                    string ct4 = "Update MarksEntry set Class=@d1 where Class=@d2";
                    cmd = new SqlCommand(ct4, con, transaction);
                    cmd.Parameters.AddWithValue("@d1", txtClassName.Text.Trim());                  
                    cmd.Parameters.AddWithValue("@d2", txtID.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    //close the reader
                    if (rdr != null)
                    {
                        rdr.Close();
                    }

                    //commit the transanction
                    transaction.Commit();
                    //close the connection
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolClasses"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strClassName") + ": " + txtClassName.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //Clear all controls
                reset();
                btnSaveClass.Enabled = false;
                btnAdd.Enabled = true;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                gridControlClasses.DataSource = Getdata();
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

        private void btnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show(LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    DeleteRecord();
                //Clear all controls
                reset();
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                btnAdd.Enabled = true;                
                gridControlClasses.DataSource = Getdata();
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
                    string cl = "select Classes.ClassName, Students.Class, MarksEntry.Class from Classes ,Students, MarksEntry where Classes.ClassName=Students.Class and Classes.ClassName=MarksEntry.Class and Classes.ClassName=@d1";
                    cmd = new SqlCommand(cl);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtID.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strUnableDeleteUseStudents"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (rdr != null)
                            rdr.Close();
                        return;
                    }
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cq = "delete from Classes where ClassName=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtID.Text.Trim());
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolClasses"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strClassName") + ": " + txtClassName.Text + " " + LocRM.GetString("strHasBeenDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);                        
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strSchoolFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void gridControlClasses_MouseClick(object sender, MouseEventArgs e)
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
                    txtCapacity.Text = "";
                    txtCapacity.Enabled = true;
                    cbCycle.Enabled = true;
                    fillCycle();

                    txtID.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strClass").ToUpper()).ToString();
                    txtClassName.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strClass").ToUpper()).ToString();
                    cbCycle.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strCycle").ToUpper()).ToString();
                    cbSection.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSection").ToUpper()).ToString();
                    txtSectionAbbreviation.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSectionAbbreviation").ToUpper()).ToString();
                    txtCapacity.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strCapacity").ToUpper()).ToString();
                    
                    btnUpdate.Enabled = true;
                    btnRemove.Enabled = true;
                    btnSave.Enabled = false;
                    btnAdd.Enabled = true;
                    
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

        private void userControlClassSetting_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                reset();
                disable();
                btnAdd.Enabled = true;
                btnSaveClass.Enabled = false;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtClassName.Properties.ReadOnly = false;
        }
    }
}
