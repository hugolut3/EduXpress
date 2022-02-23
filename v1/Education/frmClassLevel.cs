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

namespace EduXpress.Education
{
    public partial class frmClassLevel : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmClassLevel).Assembly);

        public frmClassLevel()
        {
            InitializeComponent();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            btnNew.Enabled = false;
            btnSave.Enabled = true;
            txtClassLevel.Enabled = true;
        }
        private void reset()
        {
            txtClassLevel.Text = "";
            txtCurrentClassLevel.Text = "";
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtClassLevel.Focus();
            txtClassLevel.Enabled = false;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtClassLevel.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterClassLevel"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClassLevel.Focus();
                return;
            }
            
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select Class from ClassLevel where Class=@d1";
                    cmd = new SqlCommand(ct);
                    cmd.Parameters.AddWithValue("@d1", txtClassLevel.Text);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strClassLevelExists"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtClassLevel.Text = "";
                        txtClassLevel.Focus();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        return;
                    }
                }


                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "insert into ClassLevel(Class) VALUES (@d1)";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtClassLevel.Text);                   
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                }

                //Log record transaction in logs
                string st = LocRM.GetString("strNewClassLevel") + ": " + txtClassLevel.Text + " " + LocRM.GetString("strHasBeenSaved");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(LocRM.GetString("strSuccessfullySaved"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                reset();
                gridControlClassLevel.DataSource = Getdata();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        //Fill dataGridViewEmails
        public DataView Getdata()
        {
            string ClassLevelColumn = LocRM.GetString("strClassLevel").ToUpper();

            dynamic SelectQry = "SELECT  RTRIM(Class) as [" + ClassLevelColumn + "] FROM ClassLevel order by Class";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
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
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return TableView;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtClassLevel.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterClassLevel"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClassLevel.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "Update ClassLevel set Class=@d1  where Class=@d2";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtClassLevel.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtCurrentClassLevel.Text.Trim());
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }


                //Log record transaction in logs
                string st = LocRM.GetString("strClassLevel") + ": " + txtClassLevel.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                gridControlClassLevel.DataSource = Getdata();
                reset();
            }
            catch (Exception ex)
            {
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

                    string cl = "select ClassLevel.Class, Subject.Class from ClassLevel ,Subject where ClassLevel.Class = Subject.Class  and ClassLevel.Class=@d1";

                    cmd = new SqlCommand(cl);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtClassLevel.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strUnableDeleteClassLevel"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (rdr != null)
                            rdr.Close();
                        return;
                    }
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cq = "delete from ClassLevel where Class=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtClassLevel.Text.Trim());
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strClassLevel") + ": " + txtClassLevel.Text + " " + LocRM.GetString("strSuccessfullyDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        gridControlClassLevel.DataSource = Getdata();

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

        private void frmClassLevel_Load(object sender, EventArgs e)
        {
            gridControlClassLevel.DataSource = Getdata();
        }

        private void gridControlClassLevel_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (gridView1.DataRowCount > 0)
                {
                    txtCurrentClassLevel.Text = "";
                    txtClassLevel.Enabled = true;
                    txtClassLevel.Text = "";
                    
                    txtCurrentClassLevel.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strClassLevel").ToUpper()).ToString();
                    txtClassLevel.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strClassLevel").ToUpper()).ToString();                    

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
    }
}