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

namespace EduXpress.Students
{
    public partial class frmSection : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmSection).Assembly);

        public frmSection()
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
            txtSection.Enabled = true;
            txtSectionAbbreviation.Enabled = true;
        }
        private void reset()
        {
            txtSection.Text = "";
            txtCurrentSection.Text = "";            
            txtSectionAbbreviation.Text = "";           
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            txtSection.Focus();
            txtSection.Enabled = false;
            txtSectionAbbreviation.Enabled = false;
        }

        private void frmSection_Load(object sender, EventArgs e)
        {
            gridControlSection.DataSource = Getdata();
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtSection.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterSection"), LocRM.GetString("strInputError")  , MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSection.Focus();
                return;
            }
            if (txtSectionAbbreviation.Text == "")
            {
                XtraMessageBox.Show(  LocRM.GetString("strEnterSectionAbbreviation"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSectionAbbreviation.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select SectionName from Sections where SectionName=@d1";
                    cmd = new SqlCommand(ct);
                    cmd.Parameters.AddWithValue("@d1", txtSection.Text);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSectionExists"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSection.Text = "";
                        txtSection.Focus();
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

                    string cb = "insert into Sections(SectionName, SectionNameAbbreviation) VALUES (@d1,@d2)";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtSection.Text);
                    cmd.Parameters.AddWithValue("@d2", txtSectionAbbreviation.Text);
                    cmd.Connection = con;
                    cmd.ExecuteReader();
                    con.Close();
                }
                    
                //Log record transaction in logs
                string st =LocRM.GetString("strNewSection")  +": " + txtSection.Text + " " +  LocRM.GetString("strHasBeenSaved");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show( LocRM.GetString("strSuccessfullySaved"),  LocRM.GetString("strSchoolSections"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                reset();
                gridControlSection.DataSource = Getdata();
                
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
            string SectionColumn = LocRM.GetString("strSection").ToUpper();
            string SectionAbbreviationColumn = LocRM.GetString("strSectionAbbreviation").ToUpper();
            

            dynamic SelectQry = "SELECT  RTRIM(SectionName) as [" + SectionColumn + "]," +
                "RTRIM(SectionNameAbbreviation) as [" + SectionAbbreviationColumn + "] FROM Sections order by SectionName";
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
            if (txtSection.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSection"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSection.Focus();
                return;
            }
            if (txtSectionAbbreviation.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSectionAbbreviation"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSectionAbbreviation.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "Update Sections set SectionName=@d1, SectionNameAbbreviation=@d2  where SectionName=@d3";
                    cmd = new SqlCommand(cb);
                    cmd.Parameters.AddWithValue("@d1", txtSection.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtSectionAbbreviation.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", txtCurrentSection.Text.Trim());
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }
                    

                //Log record transaction in logs
                string st = LocRM.GetString("strSection") + ": " + txtSection.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolSections"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                gridControlSection.DataSource = Getdata();
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

                    string cl = "select Sections.SectionName, Classes.SectionName from Sections ,Classes where Classes.SectionName = Sections.SectionName  and Sections.SectionName=@d1";

                    cmd = new SqlCommand(cl);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtSection.Text.Trim());
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strUnableDeleteSection"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (rdr != null)
                            rdr.Close();
                        return;
                    }
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cq = "delete from Sections where SectionName=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtSection.Text.Trim());
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {

                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolSections"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strSection") + ": " + txtSection.Text + " " + LocRM.GetString("strSuccessfullyDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        gridControlSection.DataSource = Getdata();

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

        private void gridControlSection_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
               // if (splashScreenManager1.IsSplashFormVisible == false)
              //  {
               //     splashScreenManager1.ShowWaitForm();
               // }

                if (gridView1.DataRowCount > 0)
                {
                    txtSection.Enabled = true;
                    txtSectionAbbreviation.Enabled = true;
                    txtSection.EditValue = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSection").ToUpper());
                    txtSectionAbbreviation.EditValue = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSectionAbbreviation").ToUpper());
                    txtCurrentSection.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strSection").ToUpper()).ToString();
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnSave.Enabled = false;
                    btnNew.Enabled = true;

                }
              //  if (splashScreenManager1.IsSplashFormVisible == true)
               // {
              //      splashScreenManager1.CloseWaitForm();
              //  }
            }
            catch (Exception ex)
            {
               // if (splashScreenManager1.IsSplashFormVisible == true)
               // {
               //     splashScreenManager1.CloseWaitForm();
               // }

                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}