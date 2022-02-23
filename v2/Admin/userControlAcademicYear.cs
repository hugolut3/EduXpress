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
using DevExpress.Data.Mask;
using System.Globalization;

namespace EduXpress.Admin
{
    public partial class userControlAcademicYear : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlAcademicYear).Assembly);

        public userControlAcademicYear()
        {
            InitializeComponent();
        }
                
        //private void btnExitApplication_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            btnNew.Enabled = false;            
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            txtAcademicYear.Enabled = true;
            reset();
            // Remove mask type when in english
            if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
            {
                txtAcademicYear.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Simple; //
                txtAcademicYear.Properties.Mask.EditMask = "0000-0000";
                txtAcademicYear.Properties.Mask.BeepOnError = true;
                txtAcademicYear.Properties.Mask.IgnoreMaskBlank = true;
                txtAcademicYear.Properties.Mask.SaveLiteral = true;
            }
            else
            {
                txtAcademicYear.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            }

            
        }
        private void reset()
        {            
            txtAcademicYear.Text = "";
            txtAcademicYear.Focus();                       
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
        //Fill dataGridViewAcademicYear
        public DataView Getdata()
        {
            string SchoolYearColumn = LocRM.GetString("strSchoolYear").ToUpper();

            dynamic SelectQry = "SELECT  RTRIM(SchoolYear) as [" + SchoolYearColumn + "] FROM AcademicYear order by SchoolYear";
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

        private void userControlAcademicYear_Load(object sender, EventArgs e)
        {
            gridControlAcademicYear.DataSource = Getdata();
        }
        

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //validate AcademicYear
            if (this.txtAcademicYear.EditValue == null || this.txtAcademicYear.EditValue.ToString() == string.Empty || !this.txtAcademicYear.DoValidate())
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterCurrentSchoolYear"), LocRM.GetString("strSchoolYear"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAcademicYear.Focus();
                return;
            }

            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select SchoolYear from AcademicYear where SchoolYear=@d1";
                    cmd = new SqlCommand(ct);
                    cmd.Parameters.AddWithValue("@d1", txtAcademicYear.Text);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSchoolYearExists"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAcademicYear.Text = "";
                        txtAcademicYear.Focus();
                        if ((rdr != null))
                            rdr.Close();
                        return;
                    }
                }
                    

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "insert into AcademicYear(SchoolYear) VALUES (@d1)";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtAcademicYear.Text);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                }
                    

                //Log record transaction in logs  
                string st = LocRM.GetString("strNewSchoolYear") +": " + txtAcademicYear.Text + " "+ LocRM.GetString("strHasBeenAdded");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show( LocRM.GetString("strSuccessfullySaved"), LocRM.GetString("strSchoolYear"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                   btnSave.Enabled = false;               
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                btnNew.Enabled = true;
                txtAcademicYear.Enabled = false;
                reset();
                gridControlAcademicYear.DataSource = Getdata();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtAcademicYear.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterCurrentSchoolYear"), LocRM.GetString("strSchoolYear"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAcademicYear.Focus();
                return;
            }
            if (txtAcademicYear.Text.Trim() == txtCurrentAcademicYear.Text.Trim())
            {
                XtraMessageBox.Show(LocRM.GetString("strUpdateWithSameSchoolYear"), LocRM.GetString("strSchoolYear"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAcademicYear.Focus();
                return;
            }

            if (XtraMessageBox.Show(LocRM.GetString("strSchoolYearEditWarning"), LocRM.GetString("strSchoolYear"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strUpdating"));
                    }

                    Cursor = Cursors.WaitCursor;
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();

                        //Create a transanction object, only commit to database if update into MarksEntry,
                        //CourseFeePayment, and AcademicYear are succeful otherwise rollback. 
                        SqlTransaction transaction = con.BeginTransaction();

                        try
                        {
                            //execute 1st query
                            string cb1 = "Update MarksEntry set SchoolYear=@d1 where SchoolYear=@d2";
                            cmd = new SqlCommand(cb1, con, transaction);
                            cmd.Parameters.AddWithValue("@d1", txtAcademicYear.Text.Trim());
                            cmd.Parameters.AddWithValue("@d2", txtCurrentAcademicYear.Text.Trim());
                            rdr = cmd.ExecuteReader();
                            //close the reader
                            if (rdr != null)
                            {
                                rdr.Close();
                            }

                            //execute 2nd query
                            string cb2 = "Update CourseFeePayment set SchoolYear=@d1 where SchoolYear=@d2";
                            cmd = new SqlCommand(cb2, con, transaction);
                            cmd.Parameters.AddWithValue("@d1", txtAcademicYear.Text.Trim());
                            cmd.Parameters.AddWithValue("@d2", txtCurrentAcademicYear.Text.Trim());
                            rdr = cmd.ExecuteReader();
                            //close the reader
                            if (rdr != null)
                            {
                                rdr.Close();
                            }

                            //No need to execute this 3nd query because it's linked with foreign key to 
                            //query 4 in cascade mode, 4 will update 3 automatically. 
                            //string cb3 = "Update Students set SchoolYear=@d1 where SchoolYear=@d2";
                            //cmd = new SqlCommand(cb3, con, transaction);
                            //cmd.Parameters.AddWithValue("@d1", txtAcademicYear.Text.Trim());
                            //cmd.Parameters.AddWithValue("@d2", txtCurrentAcademicYear.Text.Trim());
                            //rdr = cmd.ExecuteReader();
                            ////close the reader
                            //if (rdr != null)
                            //{
                            //    rdr.Close();
                            //}

                            //execute 4rd query
                            string cb4 = "Update AcademicYear set SchoolYear=@d1 where SchoolYear=@d2";
                            cmd = new SqlCommand(cb4, con, transaction);
                            cmd.Parameters.AddWithValue("@d1", txtAcademicYear.Text.Trim());
                            cmd.Parameters.AddWithValue("@d2", txtCurrentAcademicYear.Text.Trim());
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

                            Cursor = Cursors.Default;
                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }

                            //Log record transaction in logs
                            string st = LocRM.GetString("strSchoolYear") + ": " + txtAcademicYear.Text + " " + LocRM.GetString("strHasBeenUpdated");
                            pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                            XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolYear"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnUpdate.Enabled = false;
                            btnRemove.Enabled = false;
                            btnSave.Enabled = false;
                            txtAcademicYear.Enabled = false;
                            btnNew.Enabled = true;
                            gridControlAcademicYear.DataSource = Getdata();
                            reset();
                        }
                        catch (Exception ex)
                        {
                            //rollback the transanction form the pending state
                            transaction.Rollback();

                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                            Cursor = Cursors.Default;
                            XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                return; 
            }            
        }

        private void gridControlAcademicYear_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                splashScreenManager1.ShowWaitForm();

                if (gridView1.DataRowCount > 0)
                {
                    txtAcademicYear.Enabled = true;
                    txtAcademicYear.EditValue = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSchoolYear").ToUpper());
                    txtCurrentAcademicYear.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSchoolYear").ToUpper()).ToString();
                    btnUpdate.Enabled = true;
                    btnRemove.Enabled = true;
                    btnSave.Enabled = false;                    
                    btnNew.Enabled = true;

                }
                splashScreenManager1.CloseWaitForm();
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
                if (XtraMessageBox.Show( LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    DeleteRecord();
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                btnNew.Enabled = true;
                txtAcademicYear.Enabled = false;
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
                    string cl = "select AcademicYear.SchoolYear from AcademicYear ,Students where AcademicYear.SchoolYear=Students.SchoolYear and AcademicYear.SchoolYear=@d1";
                    cmd = new SqlCommand(cl);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtCurrentAcademicYear.Text);
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
                    string cq = "delete from AcademicYear where SchoolYear=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtCurrentAcademicYear.Text);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        //Log record transaction in logs
                        string st = LocRM.GetString("strSchoolYear") + ": " + txtAcademicYear.Text + " " + LocRM.GetString("strHasBeenDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolYear"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        gridControlAcademicYear.DataSource = Getdata();
                        reset();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strSchoolYear"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void userControlAcademicYear_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                reset();
                btnNew.Enabled = true;
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                txtAcademicYear.Enabled = false;
            }
        }
    }
}
