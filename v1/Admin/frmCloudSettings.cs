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
using System.IO;

namespace EduXpress.Admin
{    
    public partial class frmCloudSettings : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmCloudSettings).Assembly);

        public frmCloudSettings()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
        private const string PathServiceAccountFile = "";
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmail.Text))
                {
                    XtraMessageBox.Show(LocRM.GetString("strInvalidEmailAddress"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.SelectAll();
                    e.Cancel = true;
                }
            }
        }
        string jsonAuthFile = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            XtraOpenFileDialog OpenFile = new XtraOpenFileDialog();

            try
            {
                OpenFile.FileName = "";
                OpenFile.Title = LocRM.GetString("strAuthenticationFile") + ": ";
                OpenFile.Filter = LocRM.GetString("strJSONFile") + ": " + "(*.json)|*.json| " + LocRM.GetString("strAllFiles") + " (*.*)|*.*";
                DialogResult res = OpenFile.ShowDialog();
                if (res == DialogResult.OK)
                {
                    txtAuthenticationFile.Text = OpenFile.FileName;
                    jsonAuthFile = File.ReadAllText(txtAuthenticationFile.Text);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool CloudSettingsExist = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterGoogleDriveServiceAccountEmail"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            if (txtDirectoryId.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterGoogleDriveDirectoryId"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDirectoryId.Focus();
                return;
            }
            if (jsonAuthFile == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectAuthorisationFile"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAuthenticationFile.Focus();
                return;
            }

            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select Provider from CloudSettings where Provider=@d1 ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbCloudProvider.Text);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        CloudSettingsExist = true;
                    }
                    else
                    {
                        CloudSettingsExist = false;
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

            if (CloudSettingsExist)
            {
                updateSettings();
            }
            else
            {
                saveSettings();
            }
        }
        private void saveSettings()
        {
            try
            { 
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "insert into CloudSettings(Provider,AccountEmail,FormDirectoryId,AuthenticationFile) VALUES (@d1,@d2,@d3,@d4)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbCloudProvider.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", txtDirectoryId.Text.Trim());
                    cmd.Parameters.AddWithValue("@d4", jsonAuthFile);                    
                    
                    cmd.ExecuteReader();

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

                XtraMessageBox.Show(LocRM.GetString("strNewCloudSettingsSaved"), LocRM.GetString("strCloudSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strNewCloudSettingsSaved") + ": " + cmbCloudProvider.Text;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                
                groupGoogleDriveSettings.Enabled = false;
                btnSave.Enabled = false;
                
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

        private void cmbCloudProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCloudProvider.SelectedIndex ==0)
            {
                groupGoogleDriveSettings.Enabled = true;
                btnSave.Enabled = true;
                PopulateGoogleDriveSettings();
            }
            else
            {
                reset();
            }
            
        }
        private void reset()
        {
            groupGoogleDriveSettings.Enabled = false;
            btnSave.Enabled = false;
           
            txtEmail.Text = "";
            txtDirectoryId.Text = "";
            txtAuthenticationFile.Text = "";
            jsonAuthFile = "";            
        }
        //Populate Cloud Settings 
        private void PopulateGoogleDriveSettings()
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
                    string ct = "SELECT Provider,AccountEmail,FormDirectoryId,AuthenticationFile FROM CloudSettings where Provider =@d1 ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbCloudProvider.Text);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        txtEmail.Text = rdr[1].ToString();
                        txtDirectoryId.Text = rdr[2].ToString();
                        jsonAuthFile = rdr[3].ToString();
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
        private void updateSettings()
        {            
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
                    string ct = "Update CloudSettings set AccountEmail=@d2,FormDirectoryId=@d3,AuthenticationFile=@d4  where Provider= @d1";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbCloudProvider.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtEmail.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@d3", txtDirectoryId.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@d4", jsonAuthFile);
                    
                    rdr = cmd.ExecuteReader();
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

                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strCloudSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strCloudSettings") + ": " + cmbCloudProvider.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                groupGoogleDriveSettings.Enabled = false;
                btnSave.Enabled = false;
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