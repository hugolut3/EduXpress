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
    public partial class frmConduct : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmApplication).Assembly);
        public frmConduct()
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
            txtConductDescription.Enabled = true;
            txtConductAbbreviation.Enabled = true;
        }
        private void reset()
        {
            txtConductDescription.Text = "";
            txtCurrentConduct.Text = "";
            txtConductAbbreviation.Text = "";
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtConductDescription.Focus();
            txtConductDescription.Enabled = false;
            txtConductAbbreviation.Enabled = false;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtConductDescription.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterConductDescription"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConductDescription.Focus();
                return;
            }
            if (txtConductAbbreviation.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterConductAbbreviation"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConductAbbreviation.Focus();
                return;
            }            

            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select Abbreviation from Conduct where Abbreviation=@d1";
                    cmd = new SqlCommand(ct);
                    cmd.Parameters.AddWithValue("@d1", txtConductAbbreviation.Text);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strConductExists"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtConductDescription.Text = "";
                        txtConductDescription.Focus();
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

                    string cb = "insert into Conduct(Description,Abbreviation) VALUES (@d1,@d2)";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtConductDescription.Text);
                    cmd.Parameters.AddWithValue("@d2", txtConductAbbreviation.Text);                    
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                }

                //Log record transaction in logs
                string st = LocRM.GetString("strNewConduct") + ": " + txtConductAbbreviation.Text + " " + LocRM.GetString("strHasBeenSaved");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(LocRM.GetString("strSuccessfullySaved"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                reset();
                gridControlConduct.DataSource = Getdata();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtConductDescription.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterConductDescription"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConductDescription.Focus();
                return;
            }
            if (txtConductAbbreviation.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterConductAbbreviation"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConductAbbreviation.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "Update Conduct set Description=@d1, Abbreviation=@d2  where Abbreviation=@d3";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtConductDescription.Text);
                    cmd.Parameters.AddWithValue("@d2", txtConductAbbreviation.Text);                    
                    cmd.Parameters.AddWithValue("@d3", txtCurrentConduct.Text.Trim());
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }

                //Log record transaction in logs
                string st = LocRM.GetString("strConduct") + ": " + txtConductDescription.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                gridControlConduct.DataSource = Getdata();
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
                //using (con = new SqlConnection(databaseConnectionString))
                //{
                //    con.Open();

                //    string cl = "select Abbreviation from Application  where  Abbreviation=@d1";

                //    cmd = new SqlCommand(cl);
                //    cmd.Connection = con;
                //    cmd.Parameters.AddWithValue("@d1", txtAchievementAbbreviation.Text.Trim());
                //    rdr = cmd.ExecuteReader();
                //    if (rdr.Read())
                //    {
                //        XtraMessageBox.Show(LocRM.GetString("strUnableDeleteSection"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        if (rdr != null)
                //            rdr.Close();
                //        return;
                //    }
                //}

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cq = "delete from Conduct where Abbreviation=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtConductAbbreviation.Text.Trim());
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strConduct") + ": " + txtConductDescription.Text + " " + LocRM.GetString("strSuccessfullyDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        gridControlConduct.DataSource = Getdata();

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

        private void frmConduct_Load(object sender, EventArgs e)
        {

            gridControlConduct.DataSource = Getdata();
            //hide course ID column
            gridView1.Columns[0].Visible = false;
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
            string ConductAbbreviationColumn = LocRM.GetString("strConductAbbreviation").ToUpper();
            string ConductColumn = LocRM.GetString("strConduct").ToUpper();
           
            dynamic SelectQry = "SELECT Id as [" + NoColumn + "],RTRIM(Description) as [" + ConductColumn + "], " +
                "RTRIM(Abbreviation) as [" + ConductAbbreviationColumn + "]  FROM Conduct order by Description";
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

        private void gridControlConduct_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (gridView1.DataRowCount > 0)
                {
                    txtCurrentConduct.Text = "";
                    txtConductDescription.Enabled = true;
                    txtConductDescription.Text = "";
                    txtConductAbbreviation.Text = "";
                    txtConductAbbreviation.Enabled = true;

                    txtCurrentConduct.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strConductAbbreviation").ToUpper()).ToString();
                    txtConductDescription.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strConduct").ToUpper()).ToString();
                    txtConductAbbreviation.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strConductAbbreviation").ToUpper()).ToString();

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