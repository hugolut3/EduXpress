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
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Data.Sql;
using System.Resources;
using System.Reflection;
using Microsoft.SqlServer.Management.Smo.Wmi;
using static EduXpress.Functions.PublicFunctions;
using static EduXpress.Functions.PublicVariables;


namespace EduXpress.UI
{
    public partial class ConfigurationWizard : DevExpress.XtraEditors.XtraForm
    {
        string SqlConnStr = null;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string st = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(ConfigurationWizard).Assembly);

        private bool LocationModified = false;
        public ConfigurationWizard()
        {
            InitializeComponent();
        }

       
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            if (cmbServerName.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectServerName"), LocRM.GetString("strSQLSettings") , MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbServerName.Focus();
                return;
            }
            if (cmbAuthentication.Text == "")
            {
                XtraMessageBox.Show(  LocRM.GetString("strSelectAuthentication"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbAuthentication.Focus();
                return;
            }
            if (cmbAuthentication.SelectedIndex == 1)
            {
                if (txtUserName.Text.Length == 0)
                {
                    XtraMessageBox.Show(LocRM.GetString("strErrorUsername") , LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Focus();
                    return;
                }
                if (txtPassword.Text.Length == 0)
                {
                    XtraMessageBox.Show(LocRM.GetString("strErrorPassword"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPassword.Focus();
                    return;
                }
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            SqlConnection SqlConn = new SqlConnection();

            if (cmbAuthentication.SelectedIndex == 0)
            {
                SqlConnStr = @"Data Source=  '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;Integrated Security=True";

            }

            if (cmbAuthentication.SelectedIndex == 1)
            {
                SqlConnStr = @"Data Source=  '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;User ID=  '" + txtUserName.Text.ToString() + "';Password='" + txtPassword.Text.ToString() + "'";
            }
            if (SqlConn.State == ConnectionState.Closed)
            {
                SqlConn.ConnectionString = SqlConnStr;
                try
                {
                    SqlConn.Open();
                    XtraMessageBox.Show( LocRM.GetString("strSuccessfulConnectionDatabase") , LocRM.GetString("strDatabaseConnectionTest") , MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCreateDatabase.Enabled = true;
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show( LocRM.GetString("strInvalidDatabaseConnection") + "\r\n" + ex.Message, LocRM.GetString("strDatabaseConnectionTest"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCreateDatabase.Enabled = false;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        //Fill authotentication
        private void fillAuthentication()
        {
            cmbAuthentication.Properties.Items.Clear();
            cmbAuthentication.Properties.Items.AddRange(new object[] {
                LocRM.GetString("strWindowsAuthentication"),
                LocRM.GetString("strSQLAuthentication") });
        }
        //Fill Countries
        private void fillCountries()
        {
            comboCountry.Properties.Items.Clear();
            comboCountry.Properties.Items.AddRange(new object[] {
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

        public bool ChangeLicense()
        {
            this.ShowDialog();
            wizardControl1.SelectedPage = wizardPageLicense;
            return LocationModified;
        }
        private void DisplayLicenseStatus()
        {
            // ----- Show the current status of the license.
            LicenseFileDetail licenseDetails;
            Assembly currentAssembly;
            Version versionInfo;

            // ----- Get the license details.
            LicenseStatusColor.BackColor = Color.Red;
            licenseDetails = ExamineLicense();
            switch (licenseDetails.Status)
            {
                case LicenseStatus.ValidLicense:
                    LicenseStatusColor.BackColor = Color.Green;
                    LicenseStatusText.Text = LocRM.GetString("strLicenseValid");
                    break;
                case LicenseStatus.MissingLicenseFile:
                    LicenseStatusText.Text = LocRM.GetString("strLicenseFileNotFound");
                    break;
                case LicenseStatus.CorruptLicenseFile:
                    LicenseStatusText.Text = LocRM.GetString("strLicenseFileCorruptInvalid") ;
                    break;
                case LicenseStatus.InvalidSignature:
                    LicenseStatusText.Text = LocRM.GetString("strLicenseFileNotMatchDigitalSignature") ;
                    break;
                case LicenseStatus.NotYetLicensed:
                    LicenseStatusText.Text = LocRM.GetString("strLicenseValidCannotBeUsedUntil")  + " " +
                        licenseDetails.LicenseDate.ToString("D") + ".";
                    break;
                case LicenseStatus.LicenseExpired:
                    LicenseStatusText.Text = LocRM.GetString("strLicenseValidExpired") + " " +
                        licenseDetails.ExpireDate.ToString("D") + ".";
                    break;
                case LicenseStatus.VersionMismatch:
                    currentAssembly = Assembly.GetEntryAssembly();
                    if (currentAssembly == null)
                        currentAssembly = Assembly.GetCallingAssembly();
                    versionInfo = currentAssembly.GetName().Version;
                    LicenseStatusText.Text =
                        LocRM.GetString("strApplicationVersionOf") +$"   { versionInfo}  "+
                        LocRM.GetString("strDoesNotMatchLicensedVersion")+" " +
                        licenseDetails.CoveredVersion + ".";
                    break;
            }
        }
        bool restartDatabaseConnection = false;
        private void wizardControl1_SelectedPageChanged(object sender, DevExpress.XtraWizard.WizardPageChangedEventArgs e)
        {
            //check if Databese page is selected
            if (wizardControl1.SelectedPage == wizardPageDatabase)
            {
                wizardPageDatabase.AllowNext = false;
                fillAuthentication();
                if (Properties.Settings.Default.DatabaseConnection.ToString() != "")
                {
                    wizardPageDatabase.AllowNext = true;
                    btnEditDatabaseConnection.Visible = true;
                    linkSearchSQL.Enabled = false;
                    btnTestConnection.Enabled = false;
                    btnCreateDatabase.Enabled = false;
                }
                else
                {
                    btnEditDatabaseConnection.Visible = false;
                    linkSearchSQL.Enabled = true;
                    btnTestConnection.Enabled = true;
                    btnCreateDatabase.Enabled = true;                    
                }
            }

            //check if License page is selected
            if (wizardControl1.SelectedPage == wizardPageLicense)
            {
                //check if restart is required after database connection
                if (restartDatabaseConnection==true)
                {
                    Application.Restart();
                    Application.ExitThread();
                }
                else
                {
                    // ----- Display the current status.
                    DisplayLicenseStatus();

                    // ----- Also display the license location.
                    LicensePath.Text = Properties.Settings.Default.LicenseFileLocation;
                }                
            }

            //check if Profile page is selected
            if (wizardControl1.SelectedPage == wizardPageProfile)
            {
                fillCountries();
                try
                {
                    //Check if Company Profile has data in database
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ct = "select SchoolName from CompanyProfile ";

                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            companyProfileExist = true;
                            wizardControl1.SelectedPage = completionWizardPage1;
                        }
                        else
                        {
                            companyProfileExist = false;
                            validateCompanyProfile();
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
           
            if (wizardControl1.SelectedPage == completionWizardPage1)
            {
                if (!companyProfileExist)
                {
                    //save previous pages (company profile)
                    saveCompanyProfile();
                }                
            }

        }
        private void validateCompanyProfile()
        {
            if (comboCountry.SelectedIndex != -1  && txtSchoolName.Text != "")
            {
                wizardPageProfile.AllowNext = true;
            }
            else
            {
                wizardPageProfile.AllowNext = false;
            }
        }
       
       
        bool companyProfileExist = false;
        private void saveCompanyProfile()
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cb = "insert into CompanyProfile( SchoolName, SchoolMotto, OfficePhone, OfficeFax, Email, " +
                        "Cellphone, SchoolWebsite,AddressStreet, AddressSuburb, AddressTown,Country,SchoolLogo) VALUES " +
                        "(@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", txtSchoolName.Text.Trim());
                    cmd.Parameters.AddWithValue("@d2", txtSchoolMoto.Text);
                    cmd.Parameters.AddWithValue("@d3", txtOfficePhone.Text);
                    cmd.Parameters.AddWithValue("@d4", txtCellphoneNo.Text);
                    cmd.Parameters.AddWithValue("@d5", txtOfficeFax.Text);
                    cmd.Parameters.AddWithValue("@d6", txtEmailAddress.Text);
                    cmd.Parameters.AddWithValue("@d7", txtSchoolWebsite.Text);
                    cmd.Parameters.AddWithValue("@d8", txtStreetName.Text);
                    cmd.Parameters.AddWithValue("@d9", txtSuburbName.Text);
                    cmd.Parameters.AddWithValue("@d10", txtTownName.Text);
                    cmd.Parameters.AddWithValue("@d11", comboCountry.Text);


                    MemoryStream ms = new MemoryStream();
                    Bitmap bmpImage = new Bitmap(pictureSchoolLogo.Image);
                    bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] data = ms.GetBuffer();
                    SqlParameter p = new SqlParameter("@d12", SqlDbType.Image);
                    p.Value = data;
                    cmd.Parameters.Add(p);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                    

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthentication.SelectedIndex == 0)
            {
                txtUserName.ReadOnly = true;
                txtPassword.ReadOnly = true;
                txtUserName.Text = "";
                txtPassword.Text = "";
            }
            if (cmbAuthentication.SelectedIndex == 1)
            {
                txtUserName.ReadOnly = false;
                txtPassword.ReadOnly = false;
            }
        }

        private void linkSearchSQL_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                    splashScreenManagerWait.SetWaitFormDescription(LocRM.GetString("strSearching")); 
                }
                //Clear combobox boxes
                cmbServerName.Items.Clear();
                comboInstanceName.Properties.Items.Clear();

                //Load SQL servers with instance name. If SQL is main instance, the instance will be empty
                DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                for (int i = 0; i < servers.Rows.Count; i++)
                {
                    cmbServerName.Items.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                  
                    cmbServerName.SelectedIndex = 0;
                }

                //Load SQL instance name. Look through all services than filter by SQL
                ManagedComputer mc = new ManagedComputer(); //Assembly: Microsoft.SqlServer.SqlWmiManagement.dll
                ServiceCollection servicecoll = mc.Services;
                foreach (Service serv in servicecoll)
                {
                    if (serv.Type.Equals(ManagedServiceType.SqlServer))  //Type : Assembly:Microsoft.SqlServer.WmiEnum.dll
                    {
                        comboInstanceName.Properties.Items.Add(serv.Name);
                    }
                }

                //Disable Export button
                btnExportSettings.Enabled = false;

                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
            }
            catch (Exception)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(( LocRM.GetString("strNoSQLServerInstance") + ("\r\n" +  LocRM.GetString("strEnterSQLInstance"))), LocRM.GetString("strSQLSettings")  , MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            string databaseConnectionString = "";
            try
            {
                if (cmbServerName.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectServerName"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbServerName.Focus();
                    return;
                }
                if (comboInstanceName.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strSelectSQLinstance"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboInstanceName.Focus();
                    return;
                }
                if (cmbAuthentication.SelectedIndex == 1)
                {
                    if (txtUserName.Text.Length == 0)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strErrorUsername"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUserName.Focus();
                        return;
                    }
                    if (txtPassword.Text.Length == 0)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strErrorPassword"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Focus();
                        return;
                    }
                }
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                if (cmbAuthentication.SelectedIndex == 0)
                {
                    con = new SqlConnection(@"Data source= '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;Integrated Security=True;");
                }
                if (cmbAuthentication.SelectedIndex == 1)
                {
                    con = new SqlConnection(@"Data Source=  '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;User ID=  '" + txtUserName.Text.ToString() + "';Password='" + txtPassword.Text.ToString() + "'");
                }
                con.Open();
                if ((con.State == ConnectionState.Open))
                {
                    
                    if (XtraMessageBox.Show( LocRM.GetString("strSaveDatabaseConnection"), LocRM.GetString("strSQLSettings") , MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {

                        databaseConnectionString = "Data Source=" + cmbServerName.Text.Trim() + ";Initial Catalog=" + "EduXpressDB".Trim();
                        if (cmbAuthentication.SelectedIndex == 0)
                        {
                            // ----- Use Windows security.
                            databaseConnectionString += ";Integrated Security=true";
                        }
                        else
                        {
                            // -----Use SQL Server security.
                            databaseConnectionString += ";User ID=" + txtUserName.Text.Trim() + ";Password=" + txtPassword.Text.Trim();
                        }
                        Properties.Settings.Default.DatabaseConnection = databaseConnectionString; //save databse connection string
                        Properties.Settings.Default.SQLServiceName= comboInstanceName.Text.Trim(); //service name of SQL Server (SQL server Instance name)                       
                        
                        Properties.Settings.Default.IsRestarting = true;//We will wait for 3 seconds in program main to elease this instance before starting new one
                        Properties.Settings.Default.Save();
                        XtraMessageBox.Show(LocRM.GetString("strDatabaseConnectionSaved"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Enable Export button
                        btnExportSettings.Enabled = true;
                        //Restart the application after the NEXT button
                        restartDatabaseConnection = true;

                        //Create database from script
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                        CreateDB();

                       // XtraMessageBox.Show(LocRM.GetString("strDatabaseCreatedSuccessfully") + "\r\n" + LocRM.GetString("strApplicationRestarted"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);                                                                      

                    }
                    else
                    {
                        //  System.Environment.Exit(0);
                        this.Hide();
                    }
                }
               // XtraMessageBox.Show( LocRM.GetString("strDatabaseConnectionSaved") , LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                wizardPageDatabase.AllowNext = true;
                // btnEditDatabaseConnection.Enabled = true;
                // System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show( LocRM.GetString("strUnableConnectSQL") + "\r\n" + ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
        private string GetSql(string Name)
        {
            try
            {
                // Gets the current assembly.
                Assembly Asm = Assembly.GetExecutingAssembly();

                // Resources are named using a fully qualified name.
                Stream strm = Asm.GetManifestResourceStream(Asm.GetName().Name + "." + Name);

                // Reads the contents of the embedded file.
                StreamReader reader = new StreamReader(strm);
                return reader.ReadToEnd();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show( ex.Message);
                throw ex;
            }
        }
        private void ExecuteSql(string DatabaseName, string Sql)
        {
            con = new SqlConnection();
            cmd = new SqlCommand(Sql, con);

            // Initialize the connection, open it, and set it to the "master" database
            con.ConnectionString = @"Data source= '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;Integrated Security=True; ";

            cmd.Connection.Open();
            cmd.Connection.ChangeDatabase(DatabaseName);
            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                // Closing the connection should be done in a Finally block
                cmd.Connection.Close();
            }
        }
        
        void CreateDB()
        {
            try
            {
                con = new SqlConnection(@"Data source= '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;Integrated Security=True;");
                con.Open();
                string cb2 = "Select * from sysdatabases where name='EduXpressDB'";
                cmd = new SqlCommand(cb2);
                cmd.Connection = con;
                rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {

                    #region OverwriteDatabase
                    //using (con = new SqlConnection(@"Data source= '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;Integrated Security=True;"))
                    //{
                    //    con.Open();
                    //    string cb1 = "Drop Database EduXpressDB";
                    //    cmd = new SqlCommand(cb1);
                    //    cmd.Connection = con;
                    //    cmd.ExecuteNonQuery();
                    //    con.Close();
                    //    con = new SqlConnection(@"Data source= '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;Integrated Security=True;");
                    //    con.Open();
                    //    string cb = "Create Database EduXpressDB";
                    //    cmd = new SqlCommand(cb);
                    //    cmd.Connection = con;
                    //    cmd.ExecuteNonQuery();


                    //    Assembly assembly = Assembly.GetExecutingAssembly();
                    //    Stream objStream = assembly.GetManifestResourceStream("EduXpress.SQL.EduExpressDBscript.sql");
                    //    StreamReader objReader = new StreamReader(objStream);

                    //    st = objReader.ReadToEnd();
                    //    Server server = new Server(new ServerConnection(con));
                    //    server.ConnectionContext.ExecuteNonQuery(st);
                    //    con.Close();
                    //}  
                    #endregion
                    XtraMessageBox.Show(LocRM.GetString("strThereIsDatabase"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // make small schema changes

                    // Creates the database.
                   // ExecuteSql("master", "CREATE DATABASE EduXpressDB");
                    // Creates the tables.
                    //ExecuteSql("EduXpressDB", GetSql("SQL.sqlCompareSchema.txt"));

                    //XtraMessageBox.Show("Schema chages succesfully", LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                        splashScreenManagerWait.SetWaitFormDescription(LocRM.GetString("strCreatingDatabase"));
                    }
                    #region CreateDatabaseFromScript
                    //using (con = new SqlConnection(@"Data source= '" + cmbServerName.Text.ToString() + "';Initial Catalog=master;Integrated Security=True; "))
                    //{
                    //    con.Open();
                    //    string cb3 = "Create Database EduXpressDB";
                    //    cmd = new SqlCommand(cb3);
                    //    cmd.Connection = con;
                    //    cmd.ExecuteNonQuery();

                    //    Assembly assembly = Assembly.GetExecutingAssembly();
                    //    Stream objStream = assembly.GetManifestResourceStream("EduXpress.SQL.EduExpressDBscript.sql");
                    //    StreamReader objReader = new StreamReader(objStream);
                    //    st = objReader.ReadToEnd();
                    //    Server server = new Server(new ServerConnection(con));
                    //    server.ConnectionContext.ExecuteNonQuery(st);
                    //    con.Close();
                    //} 
                    #endregion
                    // Creates the database.
                    ExecuteSql("master", "CREATE DATABASE EduXpressDB");
                    // Creates the tables.
                    ExecuteSql("EduXpressDB", GetSql("SQL.sql.txt"));
                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                    XtraMessageBox.Show(LocRM.GetString("strDatabaseCreatedSuccessfully") + "\r\n" + LocRM.GetString("strApplicationRestarted"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);                                        
                }

            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((rdr != null))
                {
                    rdr.Close();
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
                }               
            }

        }

        private void btnEditDatabaseConnection_Click(object sender, EventArgs e)
        {
            linkSearchSQL.Enabled = true;
            btnTestConnection.Enabled = true;
            
            btnEditDatabaseConnection.Enabled = false;
        }

        private void cmbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbAuthentication.Enabled = true;
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            //the name can be on of the following values: OnClickedLoad;OnClickedSave;OnClickedCut;OnClickedCopy;OnClickedPaste;OnClickedDelete
            InvokeMenuMethod(GetMenu(pictureSchoolLogo), "OnClickedLoad");

            try
            {
                if (pictureSchoolLogo.Image != null)
                {
                    Bitmap currentImage = pictureSchoolLogo.EditValue as Bitmap;
                    Bitmap savedImage = new Bitmap(currentImage, pictureSchoolLogo.ClientSize.Width, pictureSchoolLogo.ClientSize.Height);
                    // string imageExtension = Path.GetExtension(OpenFile.FileName);
                    savedImage.Save("School-Logo" + ".jpg");
                    pictureSchoolLogo.Image = savedImage;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private DevExpress.XtraEditors.Controls.PictureMenu GetMenu(DevExpress.XtraEditors.PictureEdit edit)
        {
            PropertyInfo pi = typeof(DevExpress.XtraEditors.PictureEdit).GetProperty("Menu", BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi != null)
                return pi.GetValue(edit, null) as DevExpress.XtraEditors.Controls.PictureMenu;
            return null;
        }

        private void InvokeMenuMethod(DevExpress.XtraEditors.Controls.PictureMenu menu, String name)
        {
            MethodInfo mi = typeof(DevExpress.XtraEditors.Controls.PictureMenu).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
            if (mi != null && menu != null)
                mi.Invoke(menu, new object[] { menu, new EventArgs() });
        }

        private void btnRemoveImage_Click(object sender, EventArgs e)
        {
            pictureSchoolLogo.EditValue = null;
        }

        private void txtEmailAddress_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtEmailAddress.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmailAddress.Text))
                {
                    XtraMessageBox.Show(LocRM.GetString("strInvalidEmailAddress"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmailAddress.SelectAll();
                    e.Cancel = true;
                }
            }
        }
       
        private void btnExportSettings_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            XtraFolderBrowserDialog folderBrowse = new XtraFolderBrowserDialog();
            
            try
            {             
              
                if (folderBrowse.ShowDialog() == DialogResult.OK)
                {
                    string destinationDirectory = folderBrowse.SelectedPath;
                    string filePath = String.Format("{0}\\{1}", destinationDirectory, "SQLSettings.dat");
                  
                        if (splashScreenManagerWait.IsSplashFormVisible == false)
                        {
                            splashScreenManagerWait.ShowWaitForm();
                            splashScreenManagerWait.SetWaitFormDescription(LocRM.GetString("strExporting"));
                        }
                        // Create a file to write to.
                        using (StreamWriter sw = File.CreateText(filePath))
                        {
                        sw.WriteLine(pf.Encrypt(Properties.Settings.Default.DatabaseConnection));//Write databse connection string
                        sw.WriteLine(pf.Encrypt(Properties.Settings.Default.SQLServiceName));//Write service name of SQL Server (SQL server Instance name)
                        //sw.WriteLine(Properties.Settings.Default.DatabaseConnection);//Write databse connection string
                        //sw.WriteLine(Properties.Settings.Default.SQLServiceName);//Write service name of SQL Server (SQL server Instance name)
                        }
                        if (splashScreenManagerWait.IsSplashFormVisible == true)
                        {
                            splashScreenManagerWait.CloseWaitForm();
                        }
                        XtraMessageBox.Show(LocRM.GetString("strExported"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImportSettings_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            XtraOpenFileDialog OpenFile = new XtraOpenFileDialog();
            try
            {
                OpenFile.Title = LocRM.GetString("strSQLSettings"); 
                OpenFile.Filter = LocRM.GetString("strSQLSettings")+ ": "+ "(*.dat)|*.dat| "+ LocRM.GetString("strAllFiles") +" (*.*)|*.*";
                if (OpenFile.ShowDialog() == DialogResult.OK)
                {
                    // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(OpenFile.FileName))
                    {
                        // Read the stream to a string, and write the string to the console.
                        String lineConnectionString = sr.ReadLine();
                        String lineServiceName = sr.ReadLine();

                        Properties.Settings.Default.DatabaseConnection = pf.Decrypt(lineConnectionString); //save databse connection string
                        Properties.Settings.Default.SQLServiceName = pf.Decrypt(lineServiceName); //service name of SQL Server (SQL server Instance name)  
                        //Properties.Settings.Default.DatabaseConnection = lineConnectionString; //save databse connection string
                        //Properties.Settings.Default.SQLServiceName = lineServiceName; //service name of SQL Server (SQL server Instance name) 

                        Properties.Settings.Default.IsRestarting = true;//We will wait for 3 seconds in program main to elease this instance before starting new one
                        Properties.Settings.Default.Save();
                        XtraMessageBox.Show(LocRM.GetString("strImported") + "\r\n" + LocRM.GetString("strApplicationRestarted"), LocRM.GetString("strSQLSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Restart aplication after NEXT
                        restartDatabaseConnection = true;
                        wizardPageDatabase.AllowNext = true;
                    }
                }
                   
            }
            catch (Exception ex)
            {
                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLocate_Click(object sender, EventArgs e)
        {
            // ----- Prompt the user for a new license file.
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            XtraOpenFileDialog locateLicenseDialog;

            locateLicenseDialog = new XtraOpenFileDialog();
            locateLicenseDialog.Filter = "License Files|*.lic|XML Files|*.xml|All Files|*.*";
            locateLicenseDialog.Title = LocRM.GetString("strOpenLicenseFile"); 
            if (locateLicenseDialog.ShowDialog() != DialogResult.OK)
                return;

            // ----- Store the new path.
            Properties.Settings.Default.LicenseFileLocation = locateLicenseDialog.FileName;
            LocationModified = true;

            // ----- Update the display.
            DisplayLicenseStatus();
            LicensePath.Text = Properties.Settings.Default.LicenseFileLocation;

            // ----- Save Licensee name to project settings.
            Properties.Settings.Default.BusinessName = companyName;
            // ----- Save any updated settings.
            Properties.Settings.Default.Save();

            // ----- Save Licensee name in database.
            checkCompanyProfile();
            if (!companyProfileExist)
            {
                //save School profile
                try
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                        splashScreenManagerWait.SetWaitFormDescription(LocRM.GetString("strSaving"));
                    }

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string cb = "insert into CompanyProfile( SchoolName, SchoolMotto, OfficePhone, OfficeFax, Email, " +
                            "Cellphone, SchoolWebsite,AddressStreet, AddressSuburb, AddressTown,Country,RegistrationNo," +
                            "TaxNumber,VATNumber,VATRate,VATRegistered,BankName,BankAccName,AccountNumber,AccountType," +
                            "Branch,BranchCode,SwiftCode,SchoolLogo) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10," +
                            "@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23, @d24)";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@d1", pf.Encrypt(companyName));
                        cmd.Parameters.AddWithValue("@d2", LocRM.GetString("strNotConfigured")); 
                        cmd.Parameters.AddWithValue("@d3", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d4", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d5", pf.Encrypt(LocRM.GetString("strNotConfigured")));
                        cmd.Parameters.AddWithValue("@d6", pf.Encrypt(LocRM.GetString("strNotConfigured")));
                        cmd.Parameters.AddWithValue("@d7", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d8", pf.Encrypt(LocRM.GetString("strNotConfigured")));
                        cmd.Parameters.AddWithValue("@d9", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d10", pf.Encrypt(LocRM.GetString("strNotConfigured")));
                        cmd.Parameters.AddWithValue("@d11", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d12", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d13", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d14", LocRM.GetString("strNotConfigured"));
                        
                        cmd.Parameters.AddWithValue("@d15", Convert.ToDecimal("0"));
                        cmd.Parameters.AddWithValue("@d16", false);                        

                        cmd.Parameters.AddWithValue("@d17", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d18", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d19", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d20", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d21", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d22", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d23", LocRM.GetString("strNotConfigured"));

                        cmd.Parameters.Add("@d24", SqlDbType.Image).Value = DBNull.Value;

                        cmd.ExecuteNonQuery();
                        con.Close();                        
                    }


                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                    
                    //Log record transaction in logs
                    string st = LocRM.GetString("strNewSchoolProfile") + ": " + companyName + " " + LocRM.GetString("strHasBeenSaved");
                    pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                    
                }

                catch (Exception ex)
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    //Update company profile

                    if (splashScreenManagerWait.IsSplashFormVisible == false)
                    {
                        splashScreenManagerWait.ShowWaitForm();
                        splashScreenManagerWait.SetWaitFormDescription(LocRM.GetString("strUpdating"));
                    }

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string cb = "update  CompanyProfile set SchoolName =@d1,SchoolMotto=@d2, OfficePhone=@d3, OfficeFax=@d4, Email=@d5, " +
                            "Cellphone=@d6, SchoolWebsite=@d7, AddressStreet=@d8,AddressSuburb=@d9,AddressTown=@d10," +
                            "Country=@d11,RegistrationNo=@d12,TaxNumber=@d13,VATNumber=@d14,VATRate=@d15,VATRegistered=@d16," +
                            "BankName=@d17,BankAccName=@d18,AccountNumber=@d19,AccountType=@d20,Branch=@d21,BranchCode=@d22," +
                            "SwiftCode=@d23,SchoolLogo=@d24 where SchoolName ='" + currentCompanyName + "'";                        

                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@d1", pf.Encrypt(companyName));
                        cmd.Parameters.AddWithValue("@d2", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d3", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d4", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d5", pf.Encrypt(LocRM.GetString("strNotConfigured")));
                        cmd.Parameters.AddWithValue("@d6", pf.Encrypt(LocRM.GetString("strNotConfigured")));
                        cmd.Parameters.AddWithValue("@d7", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d8", pf.Encrypt(LocRM.GetString("strNotConfigured")));
                        cmd.Parameters.AddWithValue("@d9", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d10", pf.Encrypt(LocRM.GetString("strNotConfigured")));
                        cmd.Parameters.AddWithValue("@d11", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d12", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d13", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d14", LocRM.GetString("strNotConfigured"));

                        cmd.Parameters.AddWithValue("@d15", Convert.ToDecimal("0"));
                        cmd.Parameters.AddWithValue("@d16", false);

                        cmd.Parameters.AddWithValue("@d17", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d18", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d19", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d20", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d21", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d22", LocRM.GetString("strNotConfigured"));
                        cmd.Parameters.AddWithValue("@d23", LocRM.GetString("strNotConfigured"));


                        cmd.Parameters.Add("@d24", SqlDbType.Image).Value = DBNull.Value;

                        cmd.ExecuteNonQuery();
                        con.Close();
                    }


                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                    
                    //Log record transaction in logs
                    string st = LocRM.GetString("strSchoolProfile") + ": " + companyName + " " + LocRM.GetString("strHasBeenUpdated");
                    pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                   
                }
                catch (Exception ex)
                {
                    if (splashScreenManagerWait.IsSplashFormVisible == true)
                    {
                        splashScreenManagerWait.CloseWaitForm();
                    }
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

        //Check if Company Profile has data in database
        string currentCompanyName = "";
        private void checkCompanyProfile()
        {
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select SchoolName from CompanyProfile ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        companyProfileExist = true;
                        if (rdr.Read())
                        {
                            // currentCompanyName = pf.Decrypt(rdr.GetString(0));
                            currentCompanyName = rdr.GetString(0);
                        }                            
                    }
                    else
                    {
                        companyProfileExist = false;
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
    }
}