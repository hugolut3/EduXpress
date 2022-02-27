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
using System.Globalization;
using System.Net.Http;

namespace EduXpress.Settings
{
    public partial class userControlSMSsettings : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlSMSsettings).Assembly);

        public userControlSMSsettings()
        {
            InitializeClient(); //Initialize the API call

            InitializeComponent();
        }
        private void reset()
        {
            cmbSMSProvider.SelectedIndex = -1;
            txtCompanyName.Text = "";
            txtProductTocken.Text = "";
            cmbCountry.SelectedIndex = -1;
            lblInternationalCodeOther.Visible = false;
            txtOther.Visible = false;
            lblInternationalCode.Visible = false;
            picPhoneCode.Visible = false;
        }
        private void enableControls()
        {
            cmbSMSProvider.Enabled = true;
            cmbSMSProvider.ReadOnly = false;
            txtCompanyName.ReadOnly = false;
            txtProductTocken.ReadOnly = false;
            txtAccountID.ReadOnly = false;
            txtCostSMS.ReadOnly = false;
            cmbCountry.Enabled = true;
        }
        private void disableControls()
        {
            cmbSMSProvider.Enabled = false;
            cmbSMSProvider.ReadOnly = true;
            txtCompanyName.ReadOnly = true;
            txtProductTocken.ReadOnly = true;
            txtAccountID.ReadOnly = true;
            txtCostSMS.ReadOnly = true;
            cmbCountry.Enabled = false;
        }
        //Fill cmbSMSProvider 
        private void FillSMSProvider()
        {
           // fill countries
           fillCountries();

            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            try
            {
                // if (splashScreenManager1.IsSplashFormVisible == false)
                //  {
                //     splashScreenManager1.ShowWaitForm();
                // }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(Provider) FROM SMSSettings", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbSMSProvider.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSMSProvider.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSMSProvider.SelectedIndex = -1;
                    
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                    

              //  if (splashScreenManager1.IsSplashFormVisible == true)
               // {
               //     splashScreenManager1.CloseWaitForm();
               // }
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
            enableControls();
            btnSave.Enabled = true;
            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SMSProviderProfileExist)
            {
                updateSettings();
            }
            else
            {
                saveSettings();
            }
        }

        

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}
        private void saveSettings()
        {
            if (cmbSMSProvider.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSMSProvider"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSMSProvider.Focus();
                return;
            }
            if (cmbCountry.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectInternationalDiallingCode"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCountry.Focus();
                return;
            }
            if (txtCostSMS.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strCostSMS"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCostSMS.Focus();
                return;
            }
            
            if (cmbCountry.SelectedText == LocRM.GetString("strOther"))
            {
                if (txtOther.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectInternationalDiallingCode"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOther.Focus();
                    return;
                }
            }
            try
            {

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select Provider from SMSSettings where Provider=@d1 ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSMSProvider.Text);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strRecordExists"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // cmbClass.Text = "";
                        cmbSMSProvider.Focus();

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
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving"));
                }


                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "insert into SMSSettings(Provider,CompanyName,Token,CountryPhoneDialCode,AccountID,CostSMS) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbSMSProvider.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtCompanyName.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", pf.Encrypt(txtProductTocken.Text.Trim()));
                    if (cmbCountry.SelectedText == "Other")
                    {
                        cmd.Parameters.AddWithValue("@d4", txtOther.Text.Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@d4", lblInternationalCode.Text);
                    }
                    cmd.Parameters.AddWithValue("@d5", pf.Encrypt(txtAccountID.Text.Trim()));
                    cmd.Parameters.AddWithValue("@d6", decimal.Parse(txtCostSMS.Text, CultureInfo.CurrentCulture));
                    cmd.ExecuteReader();
                    
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                    
                //save SMS gateway profile in application settings
                Properties.Settings.Default.SettingSMSProvider = cmbSMSProvider.Text.Trim();
                Properties.Settings.Default.SettingSMSCompany = txtCompanyName.Text.Trim();
                Properties.Settings.Default.SettingSMSTocken = pf.Encrypt(txtProductTocken.Text.Trim());
                Properties.Settings.Default.AccountID = pf.Encrypt(txtAccountID.Text.Trim());
                Properties.Settings.Default.CostSMS = Convert.ToDecimal(txtCostSMS.Text.Trim(), CultureInfo.CurrentCulture);
                //Save International Code in application settings
                Properties.Settings.Default.InternationalCode = lblInternationalCode.Text;

                // ----- Save any updated settings.
                Properties.Settings.Default.Save();

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show( LocRM.GetString("strNewSMSProviderSaved") , LocRM.GetString("strSMSsettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strNewSMSProviderSaved") +": " + cmbSMSProvider.Text ;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //Clear all controls
               // reset();
                btnUpdate.Enabled = false;
                btnSave.Enabled = false;
                btnRemove.Enabled = false;
                btnNew.Enabled = true;
                disableControls();
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
            if (cmbSMSProvider.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterSMSProvider"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSMSProvider.Focus();
                return;
            }
            if (cmbCountry.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectInternationalDiallingCode"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCountry.Focus();
                return;
            }
            if (cmbCountry.SelectedText == LocRM.GetString("strOther"))
            {
                if (txtOther.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectInternationalDiallingCode"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOther.Focus();
                    return;
                }
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
                    string ct = "Update SMSSettings set Provider=@d2,CompanyName=@d3,Token=@d4, CountryPhoneDialCode=@d5, AccountID=@d6,CostSMS=@d7  where SMSSettingsID= @d1";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtID.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", cmbSMSProvider.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@d3", txtCompanyName.Text.ToString().Trim());
                    cmd.Parameters.AddWithValue("@d4", pf.Encrypt(txtProductTocken.Text.ToString().Trim()));
                    if (cmbCountry.SelectedText == "Other")
                    {
                        cmd.Parameters.AddWithValue("@d5", txtOther.Text.ToString().Trim());
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@d5", lblInternationalCode.Text);
                    }
                    cmd.Parameters.AddWithValue("@d6", pf.Encrypt(txtAccountID.Text.Trim()));
                    cmd.Parameters.AddWithValue("@d7", decimal.Parse(txtCostSMS.Text, CultureInfo.CurrentCulture));
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
                   

                //save SMS gateway profile in application settings
                Properties.Settings.Default.SettingSMSProvider = cmbSMSProvider.Text.Trim();
                Properties.Settings.Default.SettingSMSCompany = txtCompanyName.Text.Trim();
                Properties.Settings.Default.SettingSMSTocken = pf.Encrypt(txtProductTocken.Text.Trim());
                Properties.Settings.Default.AccountID= pf.Encrypt(txtAccountID.Text.Trim());
                Properties.Settings.Default.CostSMS = Convert.ToDecimal(txtCostSMS.Text.Trim(), CultureInfo.CurrentCulture);
                //Save International Code in application settings
                Properties.Settings.Default.InternationalCode = lblInternationalCode.Text;

                // ----- Save any updated settings.
                Properties.Settings.Default.Save();

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show( LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSMSsettings") , MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st =   LocRM.GetString("strSMSProvider") + ": " + cmbSMSProvider.Text + " " + LocRM.GetString("strHasBeenUpdated")  ;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //Clear all controls
               // reset();
               // btnUpdate.Enabled = false;
                btnSave.Enabled = false;
                btnRemove.Enabled = false;
                btnNew.Enabled = true;
                disableControls();
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
            enableControls();
            btnSave.Enabled = true;
            btnRemove.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show(LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    DeleteRecord();
                //Clear all controls
                reset();
                btnUpdate.Enabled = false;
                btnSave.Enabled = false;
                btnRemove.Enabled = false;
                btnNew.Enabled = true;
                disableControls();
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
                    string cq = "delete from SMSSettings where SMSSettingsID=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtID.Text);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSMSsettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strSMSProvider") + ": " + cmbSMSProvider.Text + " " + LocRM.GetString("strHasBeenDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strSMSsettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                        
                }
                    
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
       
        private void cmbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cmbCountry.Text== LocRM.GetString("strOther"))
            {
                lblInternationalCodeOther.Visible = true;
                txtOther.Visible = true;
                lblInternationalCode.Visible = false;
                picPhoneCode.Visible = false;
            }
            else
            {
                lblInternationalCodeOther.Visible = false;
                txtOther.Visible = false;
                lblInternationalCode.Visible = true;
                picPhoneCode.Visible = true;
            }

            if (cmbCountry.Text.Trim() == LocRM.GetString("strAngola"))
            {
                picPhoneCode.Image = Properties.Resources.flag_angola;
                lblInternationalCode.Text = "00244";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strBotswana"))
            {
                picPhoneCode.Image = Properties.Resources.flag_botswana;
                lblInternationalCode.Text = "00267";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strBurundi"))
            {
                picPhoneCode.Image = Properties.Resources.flag_burundi;
                lblInternationalCode.Text = "00257";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strDRC"))
            {
                picPhoneCode.Image = Properties.Resources.flag_congo_democratic_republic_new;
                lblInternationalCode.Text = "00243";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strKenya"))
            {
                picPhoneCode.Image = Properties.Resources.flag_kenya;
                lblInternationalCode.Text = "00254";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strLesotho"))
            {
                picPhoneCode.Image = Properties.Resources.flag_lesotho;
                lblInternationalCode.Text = "00266";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strMalawi"))
            {
                picPhoneCode.Image = Properties.Resources.flag_malawi;
                lblInternationalCode.Text = "00265";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strMozambique"))
            {
                picPhoneCode.Image = Properties.Resources.flag_mozambique;
                lblInternationalCode.Text = "00258";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strNamibia"))
            {
                picPhoneCode.Image = Properties.Resources.flag_namibia;
                lblInternationalCode.Text = "00264";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strRwanda"))
            {
                picPhoneCode.Image = Properties.Resources.flag_rwanda;
                lblInternationalCode.Text = "00250";
            }

            if (cmbCountry.Text.Trim() == LocRM.GetString("strSouthAfrica"))
            {
                picPhoneCode.Image = Properties.Resources.flag_south_africa;
                lblInternationalCode.Text = "0027";
            }

            if (cmbCountry.Text.Trim() == LocRM.GetString("strSwaziland"))
            {
                picPhoneCode.Image = Properties.Resources.flag_swaziland;
                lblInternationalCode.Text = "00268";
            }

            if (cmbCountry.Text.Trim() == LocRM.GetString("strTanzania"))
            {
                picPhoneCode.Image = Properties.Resources.flag_tanzania;
                lblInternationalCode.Text = "00255";
            }
            if (cmbCountry.Text.Trim() == LocRM.GetString("strZambia"))
            {
                picPhoneCode.Image = Properties.Resources.flag_zambia;
                lblInternationalCode.Text = "00260";
            }

            if (cmbCountry.Text.Trim() == LocRM.GetString("strZimbabwe"))
            {
                picPhoneCode.Image = Properties.Resources.flag_zimbabwe;
                lblInternationalCode.Text = "00263";
            }
        }
        //Telephone International Code
        private void selectCountry()
        {
            if (lblInternationalCode.Text == "00244")
            {
                cmbCountry.Text = LocRM.GetString("strAngola");
            }

            if (lblInternationalCode.Text == "00267")
            {
                cmbCountry.Text = LocRM.GetString("strBotswana");
            }
            if (lblInternationalCode.Text == "00257")
            {
                cmbCountry.Text = LocRM.GetString("strBurundi");
            }
            if (lblInternationalCode.Text == "00243")
            {
                cmbCountry.Text = LocRM.GetString("strDRC");
            }
            if (lblInternationalCode.Text == "00254")
            {
                cmbCountry.Text = LocRM.GetString("strKenya");
            }
            if (lblInternationalCode.Text == "00266")
            {
                cmbCountry.Text = LocRM.GetString("strLesotho");
            }
            if (lblInternationalCode.Text == "00265")
            {
                cmbCountry.Text = LocRM.GetString("strMalawi");
            }
            if (lblInternationalCode.Text == "00258")
            {
                cmbCountry.Text = LocRM.GetString("strMozambique");
            }
            if (lblInternationalCode.Text == "00264")
            {
                cmbCountry.Text = LocRM.GetString("strNamibia");
            }
            if (lblInternationalCode.Text == "00250")
            {
                cmbCountry.Text = LocRM.GetString("strRwanda");
            }

            if (lblInternationalCode.Text == "0027")
            {
                cmbCountry.Text = LocRM.GetString("strSouthAfrica");
            }
            if (lblInternationalCode.Text == "00268")
            {
                cmbCountry.Text = LocRM.GetString("strSwaziland");
            }
                       

            if (lblInternationalCode.Text == "00255")
            {
                cmbCountry.Text = LocRM.GetString("strTanzania");
            }
            if (lblInternationalCode.Text == "00260")
            {
                cmbCountry.Text = LocRM.GetString("strZambia");
            }

            if (lblInternationalCode.Text == "00263")
            {
                cmbCountry.Text = LocRM.GetString("strZimbabwe");
            }
           // else
            //{
            //    cmbCountry.Text = "Other";
            //}
        }

        private void userControlSMSsettings_Load(object sender, EventArgs e)
        {          
           FillSMSProvider();
           loadDetails();
           FillSMSTemplateName();
        }

        //Fill Countries
        private void fillCountries()
        {
            cmbCountry.Properties.Items.Clear();
            cmbCountry.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strAngola"),
            LocRM.GetString("strBotswana"),
            LocRM.GetString("strBurundi"),
            LocRM.GetString("strDRC"),
            LocRM.GetString("strKenya"),
            LocRM.GetString("strLesotho"),
            LocRM.GetString("strMalawi"),
            LocRM.GetString("strMozambique"),
            LocRM.GetString("strNamibia"),
            LocRM.GetString("strRwanda"),
            LocRM.GetString("strSouthAfrica"),
            LocRM.GetString("strSwaziland"),
            LocRM.GetString("strTanzania"),
            LocRM.GetString("strZambia"),
            LocRM.GetString("strZimbabwe"),
            LocRM.GetString("strOther")});
        }

        bool SMSProviderProfileExist = false;
        private void loadDetails()
        {
            try
            {

                //Check if Company Profile has data in database

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select * from SMSSettings ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        SMSProviderProfileExist = true;
                        btnUpdate.Enabled = true;
                        //load data from database to controls
                        if (rdr.Read())
                        {
                            txtID.Text = (rdr.GetValue(0).ToString().ToString().Trim());
                            cmbSMSProvider.Text = (rdr.GetString(1).ToString().Trim());
                            txtCompanyName.Text = (rdr.GetString(2).ToString().Trim());
                            txtProductTocken.Text = pf.Decrypt((rdr.GetString(3).ToString().Trim()));
                            lblInternationalCode.Text = (rdr.GetString(4).ToString().Trim());
                            txtAccountID.Text = pf.Decrypt((rdr.GetString(5).ToString().Trim()));
                            txtCostSMS.Text = (rdr.GetDecimal(6).ToString());
                            selectCountry();
                        }
                    }
                    else
                    {
                        SMSProviderProfileExist = false;
                        btnUpdate.Enabled = false;
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
                    

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void userControlSMSsettings_VisibleChanged(object sender, EventArgs e)
        {
            disableControls();
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnRemove.Enabled = false;
            btnUpdate.Enabled = true;
            resetMessage();
        }
        private static HttpClient apiClient { get; set; }
        //private decimal costSMS = 0;

        private static void InitializeClient()
        {
            Functions.PublicFunctions pf = new Functions.PublicFunctions();
            apiClient = new HttpClient();
            apiClient.DefaultRequestHeaders.Accept.Clear();

            apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            apiClient.DefaultRequestHeaders.Add("X-CM-PRODUCTTOKEN",pf.Decrypt( Properties.Settings.Default.SettingSMSTocken));
        }
        public async Task<Functions.balanceModel> LoadBalance()
        {
            string url = $"https://api.cmtelecom.com/accountbalance/v1.0/accountbalance/{ pf.Decrypt(Properties.Settings.Default.AccountID)}";


            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strGettingBalance"));
                }

                using (HttpResponseMessage response = await apiClient.GetAsync(url))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        Functions.balanceModel balance = await response.Content.ReadAsAsync<Functions.balanceModel>();
                        balance.Amount = Math.Round(balance.Amount, 2); //round up to 2 decimal point 
                        txtAccountBalance.Text = balance.Amount.ToString() + " " + balance.Currency;

                        if (Properties.Settings.Default.CostSMS > 0)
                        {
                            decimal test = Properties.Settings.Default.CostSMS;
                            decimal noSMS = balance.Amount / Properties.Settings.Default.CostSMS;
                            noSMS = Math.Round(noSMS, 2);
                            txtNoSMS.Text = noSMS.ToString(CultureInfo.CurrentCulture) + " SMS";
                        }

                        txtAccountType.Text = balance.BillingTypeID;
                        txtExpirationDate.Text = balance.ExpirationDate;
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        return balance;
                    }
                    else
                    {
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        XtraMessageBox.Show(response.ReasonPhrase, LocRM.GetString("strSMSsettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        throw new Exception(response.ReasonPhrase);
                    }

                }

            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                throw new Exception();
               // XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnBalance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //check if there is internet connection
            if (pf.CheckForInternetConnection() == true)
                {
                    LoadBalance();
                }

                else
                {
                    XtraMessageBox.Show(LocRM.GetString("strNoInternetToCheckSMSBalance"), LocRM.GetString("strSMSsettings"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void txtCostSMS_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;

            if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
            {
                if (char.IsControl(keyChar))
                {
                    //Allow all control characters.
                }
                else if (char.IsDigit(keyChar) || keyChar == ',')
                {
                    string text = this.txtCostSMS.Text;
                    int selectionStart = this.txtCostSMS.SelectionStart;
                    int selectionLength = this.txtCostSMS.SelectionLength;

                    text = text.Substring(0, selectionStart) + keyChar + text.Substring(selectionStart + selectionLength);

                    if (int.TryParse(text, out int result) && text.Length > 16)
                    {
                        //Reject an integer that is longer than 16 digits.
                        e.Handled = true;
                    }
                    else if (double.TryParse(text, out double results) && text.IndexOf(',') < text.Length - 3)
                    {
                        //Reject a real number with two many decimal places.
                        e.Handled = false;
                    }
                }
                else
                {
                    //Reject all other characters.
                    e.Handled = true;
                }
            }
            else
            {
                if (char.IsControl(keyChar))
                {
                    //Allow all control characters.
                }
                else if (char.IsDigit(keyChar) || keyChar == '.')
                {
                    string text = this.txtCostSMS.Text;
                    int selectionStart = this.txtCostSMS.SelectionStart;
                    int selectionLength = this.txtCostSMS.SelectionLength;

                    text = text.Substring(0, selectionStart) + keyChar + text.Substring(selectionStart + selectionLength);

                    if (int.TryParse(text, out int result) && text.Length > 16)
                    {
                        //Reject an integer that is longer than 16 digits.
                        e.Handled = true;
                    }
                    else if (double.TryParse(text, out double results) && text.IndexOf('.') < text.Length - 3)
                    {
                        //Reject a real number with two many decimal places.
                        e.Handled = false;
                    }
                }
                else
                {
                    //Reject all other characters.
                    e.Handled = true;
                }

            }
        }

        private void memoPaymentNotification_EditValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = memoTemplateMessage.Text;
        }
        int count = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            count = 160 - textBox1.TextLength;
            lblCount.Text = count.ToString() + LocRM.GetString("strCharactersRemaining");
        }

       
        private void enableControlsMessage()
        {
            comboTemplateName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            memoTemplateMessage.Properties.ReadOnly = false;
        }
        private void disableControlsMessage()
        {
            comboTemplateName.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            memoTemplateMessage.Properties.ReadOnly = true;
        }
        private void resetMessage()
        {
            comboTemplateName.SelectedIndex = -1;
            memoTemplateMessage.Text = "";
            lblMessageHolder.Text = "";
        }

        private void btnSaveMessage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (comboTemplateName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSMSTemplateName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboTemplateName.Focus();
                return;
            }
            if (memoTemplateMessage.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSMSTemplateMessageEmpty"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                memoTemplateMessage.Focus();
                return;
            }
            
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strUpdating"));
                }
                #region InsertPlaceHolder
                //string messageValueFinal = "";
                //string messageValue = memoTemplateMessage.Text.Trim();
                //// Find the place holder {2}.
                //int holderPosition = messageValue.IndexOf("{2}");
                // Make sure we have the place holder {2}
                //if (holderPosition >= 0)
                //{
                //    // Insert a string {3} after {2}.
                //    if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
                //    {
                //         messageValueFinal = messageValue.Insert(holderPosition + 3, "{3}");
                //    }
                //    else
                //    {
                //         messageValueFinal = messageValue.Insert(holderPosition, "{3}");
                //    }

                //} 
                #endregion
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "Update SMSMessageTemplates set TemplateName=@d2,TemplateMessage=@d3,MessageHolder=@d4,MessageID=@d5   where TemplatesID= @d1";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1",txtMessageID.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", comboTemplateName.Text.Trim());
                    cmd.Parameters.AddWithValue("@d3", memoTemplateMessage.Text.Trim());
                    cmd.Parameters.AddWithValue("@d4",lblMessageHolder.Text.Trim());
                    //Combobox message name Index 0 = Payment Notification SMS template message ID: 1
                    if(comboTemplateName.SelectedIndex==0)
                    {
                        cmd.Parameters.AddWithValue("@d5", 1);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@d5", 0); // No message sent
                    }                    

                    rdr = cmd.ExecuteReader();
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }

                    //save message in project settings
                    if (comboTemplateName.SelectedIndex==0)
                    {
                      Properties.Settings.Default.PaymentNotificationSMS= memoTemplateMessage.Text.Trim();
                    }
                    Properties.Settings.Default.Save();
                }
                
                
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSMSMessageTemplate"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strSMSMessageTemplate") + ": " + comboTemplateName.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //Clear all controls
                resetMessage();               
                btnSaveMessage.Enabled = false;                           
                disableControlsMessage();
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

        private void btnEditMessage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            enableControlsMessage();
            btnSaveMessage.Enabled = true;            
            btnEditMessage.Enabled = false;
        }
        //Fill SMS Template Name 
        private void FillSMSTemplateName()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            resetMessage();
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(TemplateName) FROM SMSMessageTemplates", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                   comboTemplateName.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        comboTemplateName.Properties.Items.Add(drow[0].ToString());
                    }
                    comboTemplateName.SelectedIndex = -1;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
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
        }

        private void comboTemplateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                if (comboTemplateName.SelectedIndex > -1)
                {
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "SELECT * FROM SMSMessageTemplates WHERE TemplateName = '" + comboTemplateName.Text.Trim() + "'";
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            txtMessageID.Text = Convert.ToString(rdr.GetValue(0));
                            comboTemplateName.Text = (rdr.GetString(1).Trim());
                           memoTemplateMessage.Text = (rdr.GetString(2).Trim());
                            lblMessageHolder.Text = (rdr.GetString(3).Trim());                           
                            
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
