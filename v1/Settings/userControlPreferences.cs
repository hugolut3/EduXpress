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
using System.Resources;
using System.Data.SqlClient;
using DevExpress.XtraReports.UI;

namespace EduXpress.Settings

{
    public partial class userControlPreferences : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();

        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlPreferences).Assembly);

        public userControlPreferences()
        {
            InitializeComponent();

           // loadPreferencesDatabase();
        }
        //Fill comboLanguage
        private void fillLanguage()
        {
            comboLanguage.Properties.Items.Clear();
            comboLanguage.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strAuto"),
            LocRM.GetString("strEnglish"),
            LocRM.GetString("strFrench") });
        }
        //Fill cmbSkins
        private void fillTheme()
        {
            cmbSkins.Properties.Items.Clear();
            cmbSkins.Properties.Items.AddRange(new object[] { LocRM.GetString("strBlue"),
            LocRM.GetString("strLightBlue"),LocRM.GetString("strSkyBlue"),LocRM.GetString("strSilver"),
            LocRM.GetString("strGray"),LocRM.GetString("strDarkGray"),LocRM.GetString("strBlack"),
            LocRM.GetString("strWhite"),LocRM.GetString("strCoffee"),LocRM.GetString("strCaramel"),
            LocRM.GetString("strGreen"),LocRM.GetString("strPink")});
        }
        
        private void loadControls()
        {
            loadPreferencesDatabase();
            fillLanguage();
            fillTheme();
            loadErrorLog();
            
            loadTheme();            
            
            selectLanguage();
            languageChanged = false;
            if (Functions.PublicVariables.Role == 1 || (Functions.PublicVariables.Role == 2)) //Administrator, Administrator Assistant
            {
                groupLanguage.Visible = true;
                groupCurrencySymbol.Visible = true;
                btnResetPreferencesSettings.Visible = true;
                groupPhotoDirectory.Visible = true;
            }
            else if(Functions.PublicVariables.Role == 4 || (Functions.PublicVariables.Role == 7))//Acount & Admission, Admission Officer
            {
                groupPhotoDirectory.Visible = true;
                groupLanguage.Visible = false;
                groupCurrencySymbol.Visible = false;
                btnResetPreferencesSettings.Visible = false;               
            }
            else
            {
                groupPhotoDirectory.Visible = false;
                groupLanguage.Visible = false;
                groupCurrencySymbol.Visible = false;
                btnResetPreferencesSettings.Visible = false;              
            }
            try
            {
                //load Students Photos Directory:               

                if (Properties.Settings.Default.StudentsPhotosDirectory == null)
                {
                    txtStudentsPhotosDirectory.Text = "C:\\Students_Photos\\";
                }
                else
                {
                    txtStudentsPhotosDirectory.Text = Properties.Settings.Default.StudentsPhotosDirectory;
                }

                //Notification user
                selectNotification();
                //Notification send invoice
                selectNotificationInvoice();

                btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool PreferencesExist = false;
        private void loadPreferencesDatabase()
        {
            if (Functions.PublicVariables.Role == 1 || (Functions.PublicVariables.Role == 2)) //Administrator, Administrator Assistant
            {
                groupRegUpDelUsers.Visible = true;
                groupInvoice.Visible = true;
            }
            else
            {
                groupRegUpDelUsers.Visible = false;
                groupInvoice.Visible = false;
            }

           try
            {
                //Check if Preferencees has data in database
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select * from Preferences ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        PreferencesExist = true;
                        //load data from database to controls
                        if (rdr.Read())
                        {
                            txtID.Text = (rdr.GetValue(0).ToString().Trim());
                            if (!Convert.IsDBNull(rdr["LanguageAuto"]))
                            {
                                LanguageAutoStatus = (rdr.GetBoolean(1));
                            }
                            else
                            {
                                LanguageAutoStatus = true;
                            }

                            if (!Convert.IsDBNull(rdr["Language"]))
                            {
                                LanguageValue = (rdr.GetString(2).ToString().Trim());
                            }
                            else
                            {
                                LanguageValue = "";
                            }

                            if (!Convert.IsDBNull(rdr["NotificationRegUpDel"]))
                            {
                                NotificationRegUpDelValue = Convert.ToInt16(rdr.GetValue(3));
                            }
                            else
                            {
                                NotificationRegUpDelValue = 0;
                            }

                            if (!Convert.IsDBNull(rdr["NotificationInvoice"]))
                            {
                                NotificationInvoiceValue = Convert.ToInt16(rdr.GetValue(4));
                            }
                            else
                            {
                                NotificationInvoiceValue = 0;
                            }

                            if (!Convert.IsDBNull(rdr["CurrencySymbol"]))
                            {
                                txtCurrencySymbol.Text = (rdr.GetString(5).ToString().Trim());
                            }
                            else
                            {
                                txtCurrencySymbol.Text = "";
                            }
                           if (!Convert.IsDBNull(rdr["CurrencySymbolPositionBefore"]))
                           {
                            CurrencySymbolBefore = (rdr.GetBoolean(6));
                           }
                           else
                           {
                           CurrencySymbolBefore  = false;
                           }
                           if (CurrencySymbolBefore==true)
                           {
                            radioCurrencySymbolBefore.Checked = true;

                           }
                           else
                           {
                            radioCurrencySymbolAfter.Checked = true;
                           }
                        
                        }
                    }
                    else
                    {
                        PreferencesExist = false;
                        LanguageAutoStatus = true;
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
        string LanguageValue="";        
        bool LanguageAutoStatus=false;
        int  NotificationRegUpDelValue =0;
        int NotificationInvoiceValue = 0;
        private void selectLanguage()
        {            
           try
            {
                //select current language
                if (LanguageAutoStatus == true)
                {
                    comboLanguage.Text = LocRM.GetString("strAuto");
                }
            else
            {
                if (LanguageValue == "en-US")
                {
                    comboLanguage.Text = LocRM.GetString("strEnglish");
                }
                if (LanguageValue == "fr-FR")
                {
                    comboLanguage.Text = LocRM.GetString("strFrench");
                }
            }
            }
           catch (Exception ex)
            {
               XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
        }

        private void loadTheme()
        {
            try
            {
                
                //load application skin               
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "VS2010")
                {
                    AppSkin = LocRM.GetString("strBlue");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Visual Studio 2013 Blue")
                {
                    AppSkin = LocRM.GetString("strLightBlue");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Office 2013 Dark Gray")
                {
                    AppSkin = LocRM.GetString("strGray");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Office 2010 Black")
                {
                    AppSkin = LocRM.GetString("strDarkGray");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Office 2013")
                {
                    AppSkin = LocRM.GetString("strWhite");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Dark Side")
                {
                    AppSkin = LocRM.GetString("strBlack");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Coffee")
                {
                    AppSkin = LocRM.GetString("strCoffee");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Liquid Sky")
                {
                    AppSkin = LocRM.GetString("strSkyBlue");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Caramel")
                {
                    AppSkin = LocRM.GetString("strCaramel");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Office 2007 Silver")
                {
                    AppSkin = LocRM.GetString("strSilver");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Office 2007 Green")
                {
                    AppSkin = LocRM.GetString("strGreen");
                }
                if (Properties.Settings.Default.ApplicationSkin.ToString() == "Office 2007 Pink")
                {
                    AppSkin = LocRM.GetString("strPink");
                }
                cmbSkins.Text="";
                cmbSkins.SelectedText = AppSkin;  
                
           }
            catch (Exception ex)
            {
               XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        
            

         bool ErrorLogStatus;
        private void loadErrorLog()
        {
            if (Functions.PublicVariables.Role == 1 || (Functions.PublicVariables.Role == 2)) //Administrator, Administrator Assistant
            {
                groupErrorLogging.Visible = true;
            }
            else
            {
                groupErrorLogging.Visible = false;
            }
            
            ErrorLogStatus = Properties.Settings.Default.LogErrors;
            if (ErrorLogStatus==true)
            {
                radioLogErrors.Checked = true;
                radioNoLogErrors.Checked = false;
            }
            else
            {
                radioLogErrors.Checked = false;
                radioNoLogErrors.Checked = true;
            }
        }

        //Application skins
        string AppSkin = "";
        private void appSkins()
        {
          try
          {

            if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strBlue"))
            {
                AppSkin = "VS2010";
            }
            else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strLightBlue"))
            {
                AppSkin = "Visual Studio 2013 Blue";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strGray"))
            {
                AppSkin = "Office 2013 Dark Gray";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strDarkGray"))
            {
                AppSkin = "Office 2010 Black";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strWhite"))
            {
                AppSkin = "Office 2013";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strBlack"))
            {
                AppSkin = "Dark Side";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strCoffee"))
            {
                AppSkin = "Coffee";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strSkyBlue"))
            {
                AppSkin = "Liquid Sky";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strCaramel"))
            {
                AppSkin = "Caramel";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strSilver"))
            {
                AppSkin = "Office 2007 Silver";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strGreen"))
            {
                AppSkin = "Office 2007 Green";
            }
                else if (cmbSkins.SelectedItem.ToString() == LocRM.GetString("strPink"))
            {
                AppSkin = "Office 2007 Pink";
            }
        }
           catch (Exception ex)
           {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
      }
        private void selectNotificationInvoice()
        {
            if (NotificationInvoiceValue == 0)
            {
                rdNoNotificationInvoice.Checked = true;
            }
            if (NotificationInvoiceValue == 1)
            {
                rdSendSMSInvoice.Checked = true;
            }
            if (NotificationInvoiceValue == 2)
            {
                rdSendEmailInvoice.Checked = true;
            }
            if (NotificationInvoiceValue == 3)
            {
                rdSendSMSEmailInvoice.Checked = true;
            }
        }
        private void selectNotification()
        {
            if (NotificationRegUpDelValue == 0)
            {
                rdNoNotificationUsers.Checked = true;
            }
            if (NotificationRegUpDelValue == 1)
            {
                rdSendSMSUsers.Checked = true;
            }
            if (NotificationRegUpDelValue == 2)
            {
                rdSendEmailUsers.Checked = true;
            }
            if (NotificationRegUpDelValue == 3)
            {
                rdSendSMSEmailUsers.Checked = true;
            }
        }
       
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PreferencesExist)
            {
                updatePreferences();
            }
            else
            {
                savePreferences();
            }

            try
            {
                //Save application Skin
                appSkins();
                Properties.Settings.Default.ApplicationSkin = AppSkin;
                if (txtStudentsPhotosDirectory.Text.Trim() != "")
                {
                    //save Students Photos Directory
                    Properties.Settings.Default.StudentsPhotosDirectory = txtStudentsPhotosDirectory.Text.Trim();
                }

                //----- save error log settings
                if (groupErrorLogging.Visible == true)
                {
                    if (radioLogErrors.Checked == true)
                    {
                        ErrorLogStatus = true;
                    }
                    else
                    {
                        ErrorLogStatus = false;
                    }
                    Properties.Settings.Default.LogErrors = ErrorLogStatus;
                }
                
            
            if (languageChanged==true)
            {
                    if (XtraMessageBox.Show(LocRM.GetString("strPreferencesSaved"), LocRM.GetString("strApplicationPreferences"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        //We will wait for 3 seconds in program main to release this instance before starting new one
                        Properties.Settings.Default.IsRestarting = true;
                        Properties.Settings.Default.Save();
                        btnSave.Enabled = false;
                        languageChanged = false;
                        Application.Restart();
                        Application.ExitThread();
                    }
            }
            else
                {
                    XtraMessageBox.Show(LocRM.GetString("strPreferencesSavedNoLanguage"), LocRM.GetString("strApplicationPreferences"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    languageChanged = false;
                }
            
            // ----- Save any updated settings.
            Properties.Settings.Default.Save();
            btnSave.Enabled = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void savePreferences()
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
                    string ct = "insert into Preferences(LanguageAuto,Language,NotificationRegUpDel,NotificationInvoice,CurrencySymbol,CurrencySymbolPositionBefore) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                   
                    //---- save language
                    if (comboLanguage.SelectedIndex != -1)
                    {
                        if (comboLanguage.SelectedIndex == 0)
                        {
                        cmd.Parameters.AddWithValue("@d1", true);
                        cmd.Parameters.AddWithValue("@d2", LanguageValue.Trim());
                        Properties.Settings.Default.LanguageAuto = true;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@d1", false);
                            cmd.Parameters.AddWithValue("@d2", LanguageValue.Trim());
                            Properties.Settings.Default.Language = LanguageValue.Trim();
                            Properties.Settings.Default.LanguageAuto = false;
                        }

                    }
                    cmd.Parameters.AddWithValue("@d3", NotificationRegUpDelValue);
                    cmd.Parameters.AddWithValue("@d4", NotificationInvoiceValue);
                    cmd.Parameters.AddWithValue("@d5", txtCurrencySymbol.Text.Trim());
                    cmd.Parameters.AddWithValue("@d6", CurrencySymbolBefore);
                    cmd.ExecuteReader();
                    
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }


                //save details in application settings               
                Properties.Settings.Default.RegistrationUpdateNotification = NotificationRegUpDelValue;
                Properties.Settings.Default.InvoiceNotification = NotificationInvoiceValue;
                Properties.Settings.Default.CurrencySymbol = txtCurrencySymbol.Text.Trim();
                Properties.Settings.Default.CurrencySymbolPositionBefore = CurrencySymbolBefore;

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                //Log record transaction in logs
                string st = LocRM.GetString("strApplicationPreferences") + " " + LocRM.GetString("strHasBeenSaved");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
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

        private void updatePreferences()
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
                    string ct = "Update Preferences set LanguageAuto=@d2,Language=@d3,NotificationRegUpDel=@d4, NotificationInvoice=@d5, CurrencySymbol=@d6, CurrencySymbolPositionBefore=@d7 where PreferencesID=@d1";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtID.Text.Trim());
                    //---- save language
                    if (comboLanguage.SelectedIndex != -1)
                    {
                        if (comboLanguage.SelectedIndex == 0)
                        {
                            cmd.Parameters.AddWithValue("@d2", true);
                            cmd.Parameters.AddWithValue("@d3", LanguageValue.Trim());
                        Properties.Settings.Default.LanguageAuto = true;
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@d2", false);
                            cmd.Parameters.AddWithValue("@d3", LanguageValue.Trim());                           
                            Properties.Settings.Default.Language = LanguageValue.Trim();
                            Properties.Settings.Default.LanguageAuto = false;
                        }

                    }                    
                    cmd.Parameters.AddWithValue("@d4", NotificationRegUpDelValue);
                    cmd.Parameters.AddWithValue("@d5", NotificationInvoiceValue);
                    cmd.Parameters.AddWithValue("@d6", txtCurrencySymbol.Text.Trim());
                    cmd.Parameters.AddWithValue("@d7", CurrencySymbolBefore);

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

                //save details in application settings               
                Properties.Settings.Default.RegistrationUpdateNotification = NotificationRegUpDelValue;
                Properties.Settings.Default.InvoiceNotification = NotificationInvoiceValue;
                Properties.Settings.Default.CurrencySymbol = txtCurrencySymbol.Text.Trim();
                Properties.Settings.Default.CurrencySymbolPositionBefore = CurrencySymbolBefore;

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                //Log record transaction in logs
                string st = LocRM.GetString("strApplicationPreferences") + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);               
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

        private void cmbSkins_SelectedIndexChanged(object sender, EventArgs e)
        {
            appSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(AppSkin);
            btnSave.Enabled = true;
        }       

        private void rdNoNotificationUsers_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            NotificationRegUpDelValue = 0;
        }

        private void rdSendSMSUsers_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            NotificationRegUpDelValue = 1;
        }

        private void rdSendEmailUsers_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            NotificationRegUpDelValue = 2;
        }

        private void rdNoNotificationInvoice_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            NotificationInvoiceValue = 0;
        }

        private void rdSendSMSInvoice_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            NotificationInvoiceValue = 1;
        }

        private void rdSendEmailInvoice_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            NotificationInvoiceValue = 2;
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            // string destinationDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            XtraFolderBrowserDialog OpenFolder = new XtraFolderBrowserDialog();
            try
            { 
                DialogResult res = OpenFolder.ShowDialog();
                if (res == DialogResult.OK)
                {
                    txtStudentsPhotosDirectory.Text = OpenFolder.SelectedPath.Trim();  
                }
                btnSave.Enabled = true;
            }
           catch (Exception ex)
           {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Application language  
        bool languageChanged = false;
        private void comboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            appLanguage();           
            btnSave.Enabled = true;
            languageChanged = true;
        }
        private void appLanguage()
        {
            if (comboLanguage.SelectedItem.ToString().Trim() == LocRM.GetString("strEnglish"))
            {
                LanguageValue = "en-US";
            }
            if (comboLanguage.SelectedItem.ToString().Trim() == LocRM.GetString("strFrench"))
            {
                LanguageValue = "fr-FR";
            }
            
        }

        private void radioLogErrors_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void radioNoLogErrors_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
        }

        private void userControlPreferences_VisibleChanged(object sender, EventArgs e)
        {
            //loadErrorLog();
            //languageChanged = false;

            //if (Functions.PublicVariables.Role == 1 || (Functions.PublicVariables.Role == 2)) //Administrator, Administrator Assistant
            //{
            //    groupLanguage.Visible = true;
            //    groupCurrencySymbol.Visible = true;
            //    groupRegUpDelUsers.Visible = true;
            //    groupInvoice.Visible = true;
            //}
            //else
            //{
            //    groupLanguage.Visible = false;
            //    groupCurrencySymbol.Visible = false;
            //    groupRegUpDelUsers.Visible = false;
            //    groupInvoice.Visible = false;
            //}
            hideControls();
        }

        private void rdSendSMSEmailUsers_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            NotificationRegUpDelValue = 3;
        }

        private void rdSendSMSEmailInvoice_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            NotificationInvoiceValue = 3;
        }

        private void txtCurrencySymbol_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;            
        }

        private void radioCurrencySymbolBefore_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;            
            if (radioCurrencySymbolBefore.Checked==true)
            {
                CurrencySymbolBefore = true;
            }
        }
        bool CurrencySymbolBefore = false;
        private void radioCurrencySymbolAfter_CheckedChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            if (radioCurrencySymbolAfter.Checked == true)
            {
                CurrencySymbolBefore = false;
            }
        }
        private void showControls()
        {
            groupTheme.Visible = true;
            groupLanguage.Visible = true;
            groupErrorLogging.Visible = true;
            groupCurrencySymbol.Visible = true;
            groupPhotoDirectory.Visible = true;
            groupRegUpDelUsers.Visible = true;
            groupInvoice.Visible = true;
            btnResetPreferencesSettings.Visible = true;
        }
        private void hideControls()
        {
            groupTheme.Visible = false;
            groupLanguage.Visible = false;
            groupErrorLogging.Visible = false;
            groupCurrencySymbol.Visible = false;
            groupPhotoDirectory.Visible = false;
            groupRegUpDelUsers.Visible = false;
            groupInvoice.Visible = false;
            btnResetPreferencesSettings.Visible = false;
        }
        private void btnLoadPreferences_Click(object sender, EventArgs e)
        {
            showControls();
            loadControls();
        }

        private void btnResetPreferencesSettings_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(LocRM.GetString("strPreferencesChangedQuestion"), LocRM.GetString("strApplicationPreferences"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                //Log record transaction in logs
                string st = LocRM.GetString("strResettingPreferences");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //copy current language settings
                string language = Properties.Settings.Default.Language;
                bool languageDefault = Properties.Settings.Default.LanguageAuto;
                //Reset user Preferences
                Properties.Settings.Default.Reset();
                //Set default values
                Properties.Settings.Default.ApplicationSkin= "VS2010";
                Properties.Settings.Default.Language = language;
                Properties.Settings.Default.LanguageAuto = languageDefault;
                Properties.Settings.Default.IsRestarting = true;
                Properties.Settings.Default.PrinterWatermarkTransparency = 200;

                
                XtraMessageBox.Show(LocRM.GetString("strPreferencesHasBeenReset"), LocRM.GetString("strApplicationPreferences"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //restart the application
                Application.Restart();
                Application.ExitThread();

            }
        }

        
    }
}
