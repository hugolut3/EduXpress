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
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using System.Security.Cryptography;
using DevExpress.XtraBars.Ribbon;

namespace EduXpress.Education
{
    public partial class userControlSubjectsDRC : DevExpress.XtraEditors.XtraUserControl, Functions.IMergeRibbons
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlSubjectsDRC).Assembly);

        //create global methods of ribons and status bar to merge when in main.
        //add the ImergeRibbons interface.
        public RibbonControl MainRibbon { get; set; }
        public RibbonStatusBar MainStatusBar { get; set; }
        public userControlSubjectsDRC()
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

        private void btnAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            btnSaveSubject.Enabled = true;
            FillClassLevel();
            cmbClass.Enabled = true;
        }
        private void reset()
        {
            cmbClass.SelectedIndex = -1;
            txtSubjectCode.Text = "";
            txtSubjectName.Text = "";            
            txtID.Text = "";
            txtMaximaExam.Text = "";
            txtMaximaPeriod.Text = "";

            cmbClass.Focus();
            btnAdd.Enabled = true;
            btnSaveSubject.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;

            txtSubjectName.Enabled = false;
            cmbClass.Enabled = false;
            txtMaximaExam.Enabled = false;
            txtMaximaPeriod.Enabled = false;
            txtSubjectCode.Properties.ReadOnly = true;

          //  gridControlSubjects.DataSource = Getdata();
        }
        
        //sql connection
        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(databaseConnectionString);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        //Fill dataGridViewSubjects
        public DataView Getdata()
        {
            string NoColumn = LocRM.GetString("strNo").ToUpper();
            string SubjectCodeColumn = LocRM.GetString("strSubjectCode").ToUpper();
            string SubjectColumn = LocRM.GetString("strSubject").ToUpper();             
            string ClassNameColumn = LocRM.GetString("strClassName").ToUpper();
            string MaximaPeriodColumn = LocRM.GetString("strMaximaPeriod").ToUpper();
            string MaximaExamColumn = LocRM.GetString("strMaximaExam").ToUpper();

            dynamic SelectQry = "SELECT Id as [" + NoColumn + "],RTRIM(SubjectCode) as [" + SubjectCodeColumn + "], " +
                "RTRIM(SubjectName) as [" + SubjectColumn + "], RTRIM(Class) as [" + ClassNameColumn + "]," +
                "MaximaPeriode as [" + MaximaPeriodColumn + "], MaximaExam as [" + MaximaExamColumn + "]  FROM Subject order by SubjectName";
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
        bool formLoaded = false;
        private void userControlSubjects_Load(object sender, EventArgs e)
        {
            gridControlSubjects.DataSource = Getdata();
            //hide course ID column
            gridView1.Columns[0].Visible = false;
            formLoaded = true;
        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {          
           
            txtSubjectName.Enabled = true;         

        }
        // call function to Generate unique Subject Code
        private void autoGenerateSubjectCode()
        {
            string str = txtSubjectName.Text.ToUpper();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                if ((str[i] >= 'A' && str[i] <= 'z'))
                {
                    sb.Append(str[i]);
                }
            }
            str= sb.ToString();
            //character more than 3 lenghth
            if (str.Length>3)
            {
                txtSubjectCode.Text = str.Substring(0, 3) + GetUniqueKey(3);
            }
            else
            {
                txtSubjectCode.Text = GetUniqueKey(3);
            }
            
            
        }
        //Generate unique File number
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtSubjectCode.Properties.ReadOnly = false;
        }

        

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtSubjectName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSubjectName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubjectName.Focus();
                return;
            }
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            if (txtSubjectCode.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSubjectCode"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubjectCode.Focus();
                return;
            }

            if (txtMaximaPeriod.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterMaximaMarksPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaximaPeriod.Focus();
                return;
            }

            if (txtMaximaExam.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterMaximaMarksExam"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaximaExam.Focus();
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
                    string ct1 = "Update Subject set SubjectCode=@d1,SubjectName=@d2,Class=@d3, MaximaPeriode=@d4 ,MaximaExam=@d5 where SubjectCode=@d6";

                    cmd = new SqlCommand(ct1, con, transaction);
                    // cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtSubjectCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtSubjectName.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", cmbClass.Text.Trim());
                    cmd.Parameters.AddWithValue("@d4", Convert.ToInt16(txtMaximaPeriod.Text.Trim()));
                    cmd.Parameters.AddWithValue("@d5", Convert.ToInt16(txtMaximaExam.Text.Trim()));
                    cmd.Parameters.AddWithValue("@d6", txtID.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    //close the reader
                    if (rdr != null)
                    {
                        rdr.Close();
                    }

                    //execute 2nd query
                    //string ct2 = "Update Students set Class=@d1,Section=@d2 ,Cycle=@d4 where Class=@d5";
                    //cmd = new SqlCommand(ct2, con, transaction);
                    //cmd.Parameters.AddWithValue("@d1", txtClassName.Text.Trim());
                    //cmd.Parameters.AddWithValue("@d2", cbSection.Text.Trim());
                    //cmd.Parameters.AddWithValue("@d4", cbCycle.Text.Trim());
                    //cmd.Parameters.AddWithValue("@d5", txtID.Text.Trim());
                    //rdr = cmd.ExecuteReader();
                    ////close the reader
                    //if (rdr != null)
                    //{
                    //    rdr.Close();
                    //}

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

                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strSubject") + ": " + txtSubjectName.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //Clear all controls
                reset();                
                gridControlSubjects.DataSource = Getdata();
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

        
        string subjectNameText = "";
        private void txtSubjectName_TextChanged(object sender, EventArgs e)
        {
            subjectNameText = txtSubjectName.Text.Trim();
            txtMaximaPeriod.Enabled = true;
            txtMaximaExam.Enabled = true;
            if ((txtSubjectName.Text != "") && (subjectNameText.Length >= 3))
            {
                //genearate new unique Subject Code
                autoGenerateSubjectCode();
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
                btnSaveSubject.Enabled = false;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                btnAdd.Enabled = true;
                gridControlSubjects.DataSource = Getdata();
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
                //    using (con = new SqlConnection(databaseConnectionString))
                //    {
                //        con.Open();
                //        string cl = "select Subject.SubjectCode, Students.Class from Subject ,Students where Classes.ClassName=Students.Class and Classes.ClassName=@d1";
                //        cmd = new SqlCommand(cl);
                //        cmd.Connection = con;
                //        cmd.Parameters.AddWithValue("@d1", txtID.Text.Trim());
                //        rdr = cmd.ExecuteReader();
                //        if (rdr.Read())
                //        {
                //            XtraMessageBox.Show(LocRM.GetString("strUnableDeleteUseStudents"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //            if (rdr != null)
                //                rdr.Close();
                //            return;
                //        }
                //    }

                using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                string cq = "delete from Subject where SubjectCode=@d1";
                cmd = new SqlCommand(cq);
                cmd.Parameters.AddWithValue("@d1", txtID.Text.Trim());
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if (RowsAffected > 0)
                {
                    XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //Log record transaction in logs
                    string st = LocRM.GetString("strSubject") + ": " + txtSubjectName.Text + " " + LocRM.GetString("strHasBeenDeleted");
                    pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                }
                else
                {
                    XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void userControlSubjects_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                if (formLoaded == true)
                {
                    formLoaded = false;
                }
                else
                {
                    btnAdd.Enabled = true;
                    btnSaveSubject.Enabled = false;
                    btnUpdate.Enabled = false;
                    btnRemove.Enabled = false;

                    cmbClass.SelectedIndex = -1;
                    txtSubjectCode.Text = "";
                    txtSubjectName.Text = "";
                    
                    txtID.Text = "";
                    txtMaximaExam.Text = "";
                    txtMaximaPeriod.Text = "";
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                }
            }

            //if (this.Visible == true)
            //{
            //    // reset();

            //    btnAdd.Enabled = true;
            //    btnSaveSubject.Enabled = false;
            //    btnUpdate.Enabled = false;
            //    btnRemove.Enabled = false;

            //    cmbClass.SelectedIndex = -1;
            //    txtSubjectCode.Text = "";
            //    txtSubjectName.Text = "";
            //   // txtSearchByClassLevel.Text = "";
            //   // txtSearchBySubjectName.Text = "";
            //    txtID.Text = "";
            //    txtMaximaExam.Text = "";
            //    txtMaximaPeriod.Text = "";
            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //}
        }

        private void gridControlSubjects_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (gridView1.DataRowCount > 0)
                {
                    cmbClass.SelectedIndex = -1;
                    cmbClass.Enabled = true;
                    txtSubjectCode.Text = "";
                    txtSubjectName.Text = "";
                    txtSubjectName.Enabled = true;
                    //txtSearchByClassLevel.Text = "";
                   // txtSearchBySubjectName.Text = "";
                    txtID.Text = "";
                    FillClassLevel();

                    txtID.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSubjectCode").ToUpper()).ToString();
                    txtSubjectName.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSubject").ToUpper()).ToString();
                    txtSubjectCode.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSubjectCode").ToUpper()).ToString();                    
                    cmbClass.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strClassName").ToUpper()).ToString();
                    txtMaximaPeriod.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strMaximaPeriod").ToUpper()).ToString();
                    txtMaximaExam.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strMaximaExam").ToUpper()).ToString();

                    btnUpdate.Enabled = true;
                    btnRemove.Enabled = true;
                    btnSaveSubject.Enabled = false;
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

        private void btnSaveSubject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtSubjectName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSubjectName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubjectName.Focus();
                return;
            }
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            if (txtSubjectCode.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSubjectCode"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubjectCode.Focus();
                return; 
            }

            if (txtMaximaPeriod.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterMaximaMarksPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaximaPeriod.Focus();
                return;
            }

            if (txtMaximaExam.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterMaximaMarksExam"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaximaExam.Focus();
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
                    string ct = "select SubjectCode from Subject where SubjectCode=@find";

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
                        XtraMessageBox.Show(LocRM.GetString("strSubjectExist"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                        txtSubjectName.Focus();

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

                    string cb = "insert into Subject(SubjectCode,SubjectName,Class,MaximaPeriode,MaximaExam ) VALUES (@d1,@d2,@d3,@d4,@d5)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d2", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d3", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d4", System.Data.SqlDbType.Int);
                    cmd.Parameters.Add("@d5", System.Data.SqlDbType.Int);

                    cmd.Parameters["@d1"].Value = txtSubjectCode.Text.Trim();
                    cmd.Parameters["@d2"].Value = txtSubjectName.Text.Trim();
                    cmd.Parameters["@d3"].Value = cmbClass.Text.Trim();
                    cmd.Parameters["@d4"].Value =Convert.ToInt16( txtMaximaPeriod.Text.Trim());
                    cmd.Parameters["@d5"].Value = Convert.ToInt16(txtMaximaExam.Text.Trim());
                    cmd.ExecuteNonQuery();

                    con.Close();
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(LocRM.GetString("strNewSubjectAdded"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strNewSubject") + ": " + txtSubjectName.Text + " " + LocRM.GetString("strHasBeenAdded");
                pf.LogFunc(Functions.PublicVariables.userLogged, st);

                //Clear all controls
                reset();
                
                gridControlSubjects.DataSource = Getdata();
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

        private void btnAddClassLevel_Click(object sender, EventArgs e)
        {
            frmClassLevel frm = new frmClassLevel();
            frm.ShowDialog();
            FillClassLevel();
        }
        private void FillClassLevel()
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
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Class) FROM ClassLevel", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
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

        private void btnApplication_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmApplication frm = new frmApplication();
            frm.ShowDialog();
        }

        private void btnConduct_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          frmConduct frm = new frmConduct();
            frm.ShowDialog();
        }

        private void btnAssignSubjectsClasses_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAssignSubjectsClasses frm = new frmAssignSubjectsClasses();
            frm.ShowDialog();
        }
    }
}
