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
    public partial class frmApplication : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmApplication).Assembly);
        public frmApplication()
        {
            InitializeComponent();
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            btnNew.Enabled = false;
            btnSave.Enabled = true;            
            txtAchievementDescription.Enabled = true;
            txtAchievementAbbreviation.Enabled = true;
            //rangeTrackBarPercentage.Enabled = true;
        }
        private void reset()
        {
            txtAchievementDescription.Text = "";
            txtCurrentAchievement.Text = "";
            txtAchievementAbbreviation.Text = "";
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtAchievementDescription.Focus();
            txtAchievementDescription.Enabled = false;
            txtAchievementAbbreviation.Enabled = false;
            rangeControlPercentage.SelectedRange.Minimum = 0;
            rangeControlPercentage.SelectedRange.Maximum = 0;            
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtAchievementDescription.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterAchievementDescription"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAchievementDescription.Focus();
                return;
            }
            if (txtAchievementAbbreviation.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterAchievementAbbreviation"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAchievementAbbreviation.Focus();
                return;
            }
            if (Convert.ToInt16(rangeControlPercentage.SelectedRange.Maximum)==0)
            {
                XtraMessageBox.Show(LocRM.GetString("strMaxPercentageRangeCannotBeZero"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                rangeControlPercentage.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select Abbreviation from Application where Abbreviation=@d1";
                    cmd = new SqlCommand(ct);
                    cmd.Parameters.AddWithValue("@d1", txtAchievementAbbreviation.Text);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strAchievementExists"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtAchievementDescription.Text = "";
                        txtAchievementDescription.Focus();
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

                    string cb = "insert into Application(Description,Abbreviation,Minimum,Maximum) VALUES (@d1,@d2,@d3,@d4)";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtAchievementDescription.Text);
                    cmd.Parameters.AddWithValue("@d2", txtAchievementAbbreviation.Text);
                    cmd.Parameters.AddWithValue("@d3", Convert.ToInt16(rangeControlPercentage.SelectedRange.Minimum));
                    cmd.Parameters.AddWithValue("@d4", Convert.ToInt16(rangeControlPercentage.SelectedRange.Maximum));
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                }

                //Log record transaction in logs
                string st = LocRM.GetString("strNewAchievement") + ": " + txtAchievementAbbreviation.Text + " " + LocRM.GetString("strHasBeenSaved");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(LocRM.GetString("strSuccessfullySaved"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                reset();
                gridControlApplication.DataSource = Getdata();

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

        private void gridControlApplication_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (gridView1.DataRowCount > 0)
                {
                    txtCurrentAchievement.Text = "";
                    txtAchievementDescription.Enabled = true;
                    txtAchievementDescription.Text = "";
                    txtAchievementAbbreviation.Text = "";                    
                    txtAchievementAbbreviation.Enabled = true;

                    txtCurrentAchievement.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strAchievementAbbreviation").ToUpper()).ToString();
                    txtAchievementDescription.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strAchievement").ToUpper()).ToString();
                    txtAchievementAbbreviation.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strAchievementAbbreviation").ToUpper()).ToString();
                    rangeControlPercentage.SelectedRange.Minimum = Convert.ToInt16( gridView1.GetFocusedRowCellValue(LocRM.GetString("strMinimumPercentage").ToUpper()).ToString());
                    rangeControlPercentage.SelectedRange.Maximum = Convert.ToInt16(gridView1.GetFocusedRowCellValue(LocRM.GetString("strMaximumPercentage").ToUpper()).ToString());
                    
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

        private void frmApplication_Load(object sender, EventArgs e)
        {
            gridControlApplication.DataSource = Getdata();
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
            string AchievementAbbreviationColumn = LocRM.GetString("strAchievementAbbreviation").ToUpper();
            string AchievementColumn = LocRM.GetString("strAchievement").ToUpper();
            string MinimumPercentageColumn = LocRM.GetString("strMinimumPercentage").ToUpper();
            string MaximumPercentageColumn = LocRM.GetString("strMaximumPercentage").ToUpper();
            

            dynamic SelectQry = "SELECT Id as [" + NoColumn + "],RTRIM(Description) as [" + AchievementColumn + "], " +
                "RTRIM(Abbreviation) as [" + AchievementAbbreviationColumn + "], Minimum as [" + MinimumPercentageColumn + "]," +
                "Maximum as [" + MaximumPercentageColumn + "]  FROM Application order by Description";
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
                    string cq = "delete from Application where Abbreviation=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtAchievementAbbreviation.Text.Trim());
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strAchievement") + ": " + txtAchievementDescription.Text + " " + LocRM.GetString("strSuccessfullyDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        gridControlApplication.DataSource = Getdata();

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

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtAchievementDescription.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterAchievementDescription"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAchievementDescription.Focus();
                return;
            }
            if (txtAchievementAbbreviation.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterAchievementAbbreviation"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAchievementAbbreviation.Focus();
                return;
            }
            if (Convert.ToInt16(rangeControlPercentage.SelectedRange.Maximum) == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strMaxPercentageRangeCannotBeZero"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                rangeControlPercentage.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "Update Application set Description=@d1, Abbreviation=@d2, Minimum=@d3, Maximum=@d4  where Abbreviation=@d5";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtAchievementDescription.Text);
                    cmd.Parameters.AddWithValue("@d2", txtAchievementAbbreviation.Text);
                    cmd.Parameters.AddWithValue("@d3", Convert.ToInt16(rangeControlPercentage.SelectedRange.Minimum));
                    cmd.Parameters.AddWithValue("@d4", Convert.ToInt16(rangeControlPercentage.SelectedRange.Maximum));
                    cmd.Parameters.AddWithValue("@d5", txtCurrentAchievement.Text.Trim());
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }


                //Log record transaction in logs
                string st = LocRM.GetString("strAchievement") + ": " + txtAchievementDescription.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolSubjects"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                gridControlApplication.DataSource = Getdata();
                reset();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}