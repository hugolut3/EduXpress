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

namespace EduXpress.Admin
{
    public partial class frmListFees : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmListFees).Assembly);

        public frmListFees()
        {
            InitializeComponent();
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }

        private void btnNewFee_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnNewFee.Enabled = false;
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            txtFeeName.Enabled = true;
            reset();
        }
        private void reset()
        {
            txtFeeName.Text = "";
            txtFeeName.Focus();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtFeeName.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterNameFee"), LocRM.GetString("strInputError")  , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFeeName.Focus();
                return;
            }

            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select FeeName from Fee where FeeName=@d1";
                    cmd = new SqlCommand(ct);
                    cmd.Parameters.AddWithValue("@d1", txtFeeName.Text);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strFeeNameExists"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtFeeName.Text = "";
                        txtFeeName.Focus();
                        if ((rdr != null))
                            rdr.Close();
                        return;
                    }
                }
                    

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "insert into Fee(FeeName) VALUES (@d1)";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtFeeName.Text);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                }
                    

                //Log record transaction in logs
                string st = LocRM.GetString("strNewClassFee")  +": " + txtFeeName.Text + " " + LocRM.GetString("strHasBeenAdded")  ;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show( LocRM.GetString("strSuccessfullySaved"),  LocRM.GetString("strListFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                btnNewFee.Enabled = true;
                txtFeeName.Enabled = false;
                reset();
                gridControlFeeName.DataSource = Getdata();
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
            string FeeNameColumn = LocRM.GetString("strFeeName").ToUpper();

            dynamic SelectQry = "SELECT  RTRIM(FeeName) as [" + FeeNameColumn + "] FROM Fee order by FeeName";
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

        private void frmListFees_Load(object sender, EventArgs e)
        {
            gridControlFeeName.DataSource = Getdata();
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtFeeName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterNameFee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFeeName.Focus();
                return;
            }

            try
           {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cb = "Update Fee set FeeName=@d1 where FeeName=@d2";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtFeeName.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtCurrentFeeName.Text.Trim());
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }
                    

                //Log record transaction in logs
                string st =  LocRM.GetString("strFee") + ": " + txtFeeName.Text + " " + LocRM.GetString("strHasBeenUpdated")  ;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strListFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                btnSave.Enabled = false;
                txtFeeName.Enabled = false;
                btnNewFee.Enabled = true;
                gridControlFeeName.DataSource = Getdata();
                reset();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show( LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm")  , MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    DeleteRecord();
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                btnNewFee.Enabled = true;
                txtFeeName.Enabled = false;
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
                    string cl = "select Fee.FeeName from CourseFee,Fee where CourseFee.FeeName=Fee.FeeName and Fee.FeeName=@d1";
                    cmd = new SqlCommand(cl);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtFeeName.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strUnableDeleteFee"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (rdr != null)
                            rdr.Close();
                        return;
                    }
                }
                   
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cq = "delete from Fee where FeeName=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtCurrentFeeName.Text);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strListFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strFee") + ": " + txtFeeName.Text + " " + LocRM.GetString("strSuccessfullyDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        gridControlFeeName.DataSource = Getdata();
                        reset();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strListFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void gridControlFeeName_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (gridView1.DataRowCount > 0)
                {
                    txtFeeName.Enabled = true;
                    txtFeeName.EditValue = gridView1.GetFocusedRowCellValue(LocRM.GetString("strFeeName").ToUpper());
                    txtCurrentFeeName.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strFeeName").ToUpper()).ToString();
                    btnUpdate.Enabled = true;
                    btnRemove.Enabled = true;
                    btnSave.Enabled = false;
                    btnNewFee.Enabled = true;

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
