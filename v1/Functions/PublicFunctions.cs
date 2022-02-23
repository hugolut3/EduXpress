using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Web;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using static EduXpress.Functions.PublicVariables;
using System.Globalization;
using System.Resources;
using System.Xml;
using System.Security.Cryptography.Xml;
using System.Reflection;

namespace EduXpress.Functions
{
    class PublicFunctions
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;

        // ----- Public constants.
        private static string ActiveConnectionString = "";
        public const string DefaultLicenseFile = "EduXpressLicense.lic";

        
        public static SqlConnection ConnectDatabase()
        {           

            // ----- Connect to the database. Throw exception on failure.
            SqlConnection EduXpressDB;
            bool configChanged;

            // ----- Initialize.
            configChanged = false;

            // ----- Obtain the connection string.
            if (ActiveConnectionString.Length == 0)
            {
                if ((Properties.Settings.Default.DatabaseConnection + "").Trim().Length == 0)
                {
                    ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(PublicFunctions).Assembly);
                    
                    // ----- Inform the user about the need to configure the database and the company profile.
                    //if (XtraMessageBox.Show("Cette copie de l'application n'a pas été configurée pour se connecter à la base de données du " +
                    if (XtraMessageBox.Show(sqlServerStatus + ". "+LocRM.GetString("strAppDatabaseNotConfigured") + "\r\n" +  LocRM.GetString("strLaunchWizard") +
                            "\r\n" + LocRM.GetString("strDoYouWantContinue")  , LocRM.GetString("strProgramTitle"),
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return null;

                    // ----- Prompt for the new connection details.
                    UI.ConfigurationWizard configWizard = new UI.ConfigurationWizard();
                    configWizard.ShowDialog();
                    ActiveConnectionString = Properties.Settings.Default.DatabaseConnection;
                    if (ActiveConnectionString.Length == 0)
                        return null;
                    configChanged = true;
                }
                else
                {
                    ActiveConnectionString = Properties.Settings.Default.DatabaseConnection;
                }
            }

            TryConnectingAgain:
            // ----- Attempt to open the database.
            try
            {
                 EduXpressDB = new SqlConnection(ActiveConnectionString);
                 EduXpressDB.Open();                
            }
            catch
            {
                ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(PublicFunctions).Assembly);

                // ----- Perhaps it is just a configuration issue.
                if (XtraMessageBox.Show(sqlServerStatus + ". " + LocRM.GetString("strDatabaseConnectionFailed") + "\r\n" + LocRM.GetString("strChangeDatabaseConfiguration")  ,
                    LocRM.GetString("strProgramTitle"), MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) != DialogResult.Yes)                    
                    throw;

                
                // ----- Prompt for the new connection details.
                UI.ConfigurationWizard configWizard = new UI.ConfigurationWizard();
                configWizard.ShowDialog();
                ActiveConnectionString = Properties.Settings.Default.DatabaseConnection;                

                if (ActiveConnectionString.Length == 0)
                    throw;
                configChanged = true;
                goto TryConnectingAgain;
            }

            // ----- Save the updated configuration if needed.
            if (configChanged == true)
                Properties.Settings.Default.DatabaseConnection = ActiveConnectionString;            

            // ----- Success.
            return EduXpressDB;  
        }
        public static void Preferences()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader rdr = null;           
            string LanguageValue = "";
            bool LanguageAutoStatus = false;
            bool CurrencySymbolBefore = false;
            int NotificationRegUpDelValue = 0;
            int NotificationInvoiceValue = 0;
            string CurrencySymbol = "";
            ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(PublicFunctions).Assembly);            

           try
            {
                //Check if Preferencees has data in database
                using (connection = new SqlConnection(Properties.Settings.Default.DatabaseConnection))
                {
                    connection.Open();
                    string ct = "select * from Preferences ";

                    command = new SqlCommand(ct);
                    command.Connection = connection;
                    rdr = command.ExecuteReader();

                    if (rdr.HasRows)
                    {                      
                        //load data from database to controls
                        if (rdr.Read())
                        {                           
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
                                CurrencySymbol = (rdr.GetString(5).ToString().Trim());
                            }
                            else
                            {
                                CurrencySymbol = "";
                            }
                            if (!Convert.IsDBNull(rdr["CurrencySymbolPositionBefore"]))
                            {
                                CurrencySymbolBefore = (rdr.GetBoolean(6));
                            }
                            else
                            {
                                CurrencySymbolBefore = false;
                            }
                            //save details in application settings 
                            Properties.Settings.Default.Language = LanguageValue.Trim();
                            Properties.Settings.Default.LanguageAuto = LanguageAutoStatus;
                            Properties.Settings.Default.RegistrationUpdateNotification = NotificationRegUpDelValue;
                            Properties.Settings.Default.InvoiceNotification = NotificationInvoiceValue;
                            Properties.Settings.Default.CurrencySymbol = CurrencySymbol;
                            Properties.Settings.Default.CurrencySymbolPositionBefore = CurrencySymbolBefore;
                            Properties.Settings.Default.PreferencesExist = true;
                            // ----- Save any updated settings.
                            Properties.Settings.Default.Save();
                        }
                    }
                    
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }                
            }
            catch (Exception ex)
            {              
               XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            public static void InitializeSystem()
        {
            // Check if there is a change in database schema, update the few changes
            //check if preferences exist then load from database to application settings. Used string connection lenght of >10 randomly as will be greater than 10
            //if ((Properties.Settings.Default.PreferencesExist == false) && ((Properties.Settings.Default.DatabaseConnection + "").Trim().Length > 10))
            //{
            //    ooooooo
            //    Preferences();
            //}

            // ----- Ensure we have the latest settings when upgrading the application to a more current release (current assembly version number)
            if (Properties.Settings.Default.SettingsUpgraded == false)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.SettingsUpgraded = true;
            }
        }
        public static void GeneralError(string routineName, Exception theError)
        {
            ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(PublicFunctions).Assembly);
            // ----- Report an error to the user.
            // XtraMessageBox.Show("The following error occurred at location '" + routineName + "':" +
            XtraMessageBox.Show(LocRM.GetString("strErrorOccurred") + routineName + ": " +
                Environment.NewLine + Environment.NewLine + theError.Message,
                LocRM.GetString("strProgramTitle"), MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        //public bool IsConnectionAvailable()
        //{
        //    System.Uri objUrl = new System.Uri("http://www.google.com");
        //    System.Net.WebRequest objWebReq;
        //    objWebReq = System.Net.WebRequest.Create(objUrl);
        //    System.Net.WebResponse objresp;

        //    try
        //    {
        //        objresp = objWebReq.GetResponse();
        //        objresp.Close();
        //        objresp = null;
        //        return true;

        //    }
        //    catch (Exception ex)
        //    {
        //        objresp = null;
        //        objWebReq = null;
        //        return false;
        //    }
        //}
        //send mail
        public void SendMail(string s1, string s2, string s3, string s5, string s6, int s7, string s8, string s9,string s10)
        {
            MailMessage msg = new MailMessage();
            try
            {
                msg.From = new MailAddress(s1,s10); //s10: Sender Name
                msg.To.Add(s2);
                msg.Body = s3;
                msg.IsBodyHtml = true;
                msg.Subject = s5;
                using (SmtpClient smt = new SmtpClient(s6))
                {
                    smt.Port = s7;
                    smt.EnableSsl = true;

                    // smt.UseDefaultCredentials = false;
                    smt.Credentials = new System.Net.NetworkCredential(s8, s9);
                    // smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smt.Send(msg);
                }
                    
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, ("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public string Encrypt(string password)
        {
            try
            {
                string strmsg = string.Empty;
                byte[] encode = new byte[]
                    {
                    Convert.ToByte( (password.Length - 1))};
                encode = Encoding.UTF8.GetBytes(password);
                strmsg = Convert.ToBase64String(encode);
                return strmsg;
            }
            catch (Exception)
            {

                throw;
            }
        }        
        public string Decrypt(string encryptpwd)
        {
            try
            {
                string decryptpwd = string.Empty;
                UTF8Encoding encodepwd = new UTF8Encoding();
                Decoder Decode = encodepwd.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
                int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                decryptpwd = new String(decoded_char);
                return decryptpwd;
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Log transanction data
        public void LogFunc(string st1, string st2)
        {
            try
            {               
                DateTime currentdateTime = Convert.ToDateTime(DateTime.Now.ToString(CultureInfo.CurrentCulture));
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cb = "insert into Logs(UserID,Date,Operation,Location) VALUES (@d1,@d2,@d3,@d4)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", st1);
                    cmd.Parameters.AddWithValue("@d2", currentdateTime);
                    cmd.Parameters.AddWithValue("@d3", st2);
                    cmd.Parameters.AddWithValue("@d4", Environment.MachineName);
                    cmd.ExecuteReader();
                    con.Close();
                }
                    
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, ("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Log send SMS transanctions
        public void LogSMS(string st1, string st2, string st3)
        {
            try
            {
                DateTime currentdateTime = Convert.ToDateTime(DateTime.Now.ToString(CultureInfo.CurrentCulture));
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cb = "insert into SMSLogs(UserID,Date,Operation,PhoneNumber) VALUES (@d1,@d2,@d3,@d4)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", st1);
                    cmd.Parameters.AddWithValue("@d2", currentdateTime);
                    cmd.Parameters.AddWithValue("@d3", st2);
                    cmd.Parameters.AddWithValue("@d4", st3);
                    cmd.ExecuteReader();
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, ("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LedgerSave(DateTime a, string b, string c, string d, decimal e, decimal f, string g)
        {
            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                string cb = "insert into LedgerBook(Date, Name, LedgerNo, Label,Debit,Credit,PartyID) Values (@d1,@d2,@d3,@d4,@d5,@d6,@d7)";
                cmd = new SqlCommand(cb);
                cmd.Parameters.AddWithValue("@d1", a);
                cmd.Parameters.AddWithValue("@d2", b);
                cmd.Parameters.AddWithValue("@d3", c);
                cmd.Parameters.AddWithValue("@d4", d);
                cmd.Parameters.AddWithValue("@d5", e);
                cmd.Parameters.AddWithValue("@d6", f);
                cmd.Parameters.AddWithValue("@d7", g);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
            }
                
        }

        public void LedgerDelete(string a, string b)
        {
            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                string cb = "delete from LedgerBook where LedgerNo=@d1 and Label=@d2";
                cmd = new SqlCommand(cb);
                cmd.Parameters.AddWithValue("@d1", a);
                cmd.Parameters.AddWithValue("@d2", b);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
            }
                
        }
        public void LedgerUpdate(DateTime a, string b, decimal c, decimal d, string e, string f, string g)
        {
            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                string cb = "Update LedgerBook set Date=@d1, Name=@d2,Debit=@d3,Credit=@d4,PartyID=@d5 where LedgerNo=@d6 and Label=@d7";
                cmd = new SqlCommand(cb);
                cmd.Parameters.AddWithValue("@d1", a);
                cmd.Parameters.AddWithValue("@d2", b);
                cmd.Parameters.AddWithValue("@d3", c);
                cmd.Parameters.AddWithValue("@d4", d);
                cmd.Parameters.AddWithValue("@d5", e);
                cmd.Parameters.AddWithValue("@d6", f);
                cmd.Parameters.AddWithValue("@d7", g);
                cmd.Connection = con;
                cmd.ExecuteReader();
                con.Close();
            }
                
        }
        //license
        public enum LicenseStatus : int
        {
            ValidLicense,
            MissingLicenseFile,
            CorruptLicenseFile,
            InvalidSignature,
            NotYetLicensed,
            LicenseExpired,
            VersionMismatch
        }
        public struct LicenseFileDetail
        {
            public LicenseStatus Status;
            public string Licensee;
            public DateTime LicenseDate;
            public DateTime ExpireDate;
            public string CoveredVersion;
            public string SerialNumber;
            public string Feature;
        }
        public static LicenseFileDetail ExamineLicense()
        {
            // ----- Examine the application's license file, and report back
            //       what's inside.
            LicenseFileDetail result = new LicenseFileDetail();
            string usePath;
            XmlDocument licenseContent;
            RSA publicKey;
            SignedXml signedDocument;
            XmlNodeList matchingNodes;
            string[] versionParts;
            int holdPart;
            int comparePart;
            string appPath;
            Assembly currentAssembly;

            // ----- See if the license file exists.
            result.Status = LicenseStatus.MissingLicenseFile;
            usePath = Properties.Settings.Default.LicenseFileLocation;
            if (usePath.Length == 0)
            {
                // ----- Look in the application's directory.
                currentAssembly = Assembly.GetEntryAssembly();
                if (currentAssembly == null)
                    currentAssembly = Assembly.GetCallingAssembly();
                appPath = System.IO.Path.GetDirectoryName(currentAssembly.Location);
                usePath = System.IO.Path.Combine(appPath, DefaultLicenseFile);
            }
            if (System.IO.File.Exists(usePath) == false)
                return result;

            // ----- Try to read in the file.
            result.Status = LicenseStatus.CorruptLicenseFile;
            try
            {
                licenseContent = new XmlDocument();
                licenseContent.Load(usePath);
            }
            catch
            {
                // ----- Silent error.
                return result;
            }

            // ----- Prepare the public key resource for use.
            publicKey = RSA.Create();
            publicKey.FromXmlString(Properties.Resources.EduXpressPublicKey);

            // ----- Confirm the digital signature.
            try
            {
                signedDocument = new SignedXml(licenseContent);
                matchingNodes = licenseContent.GetElementsByTagName("Signature");
                signedDocument.LoadXml((XmlElement)(matchingNodes[0]));
            }
            catch
            {
                // ----- Still a corrupted document.
                return result;
            }
            if (signedDocument.CheckSignature(publicKey) == false)
            {
                result.Status = LicenseStatus.InvalidSignature;
                return result;
            }

            // ----- The license file is valid. Extract its members.
            try
            {
                // ----- Get the licensee name.
                matchingNodes = licenseContent.GetElementsByTagName("Licensee");
                result.Licensee = matchingNodes[0].InnerText;

                // ----- Save Licensee name to global variable.                
                companyName = result.Licensee;

                // ----- Get the license date.
                matchingNodes = licenseContent.GetElementsByTagName("LicenseDate");
                result.LicenseDate = Convert.ToDateTime(matchingNodes[0].InnerText);

                // ----- Get the expiration date.
                matchingNodes = licenseContent.GetElementsByTagName("ExpireDate");
                result.ExpireDate = Convert.ToDateTime(matchingNodes[0].InnerText);

                // ----- Get the version number.
                matchingNodes = licenseContent.GetElementsByTagName("CoveredVersion");
                result.CoveredVersion = matchingNodes[0].InnerText;

                // ----- Get the serial number.
                matchingNodes = licenseContent.GetElementsByTagName("SerialNumber");
                result.SerialNumber = matchingNodes[0].InnerText;

                // ----- Get the features.
                matchingNodes = licenseContent.GetElementsByTagName("Feature");
                result.Feature = matchingNodes[0].InnerText;
            }
            catch
            {
                // ----- Still a corrupted document.
                return result;
            }

            // ----- Check for out-of-range dates.
            //var currentDate = DateTime.Today.ToString();
            //var parsedCurrentDate = DateTimeOffset.ParseExact(currentDate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
            //var formattedParsedCurrentDate = parsedCurrentDate.ToString("O");
            // if (result.LicenseDate > DateTime.Today)
            if (result.LicenseDate > DateTime.Today)
            {
                result.Status = LicenseStatus.NotYetLicensed;
                return result;
            }
            if (result.ExpireDate < DateTime.Today)
            {
                result.Status = LicenseStatus.LicenseExpired;
                return result;
            }

            // ----- Check the version.
            versionParts = result.CoveredVersion.Split(new[] { "." }, StringSplitOptions.None);
            currentAssembly = Assembly.GetEntryAssembly();
            if (currentAssembly == null)
                currentAssembly = Assembly.GetCallingAssembly();
            for (int counter = 0; counter < versionParts.Length; counter++)
            {
                if (int.TryParse(versionParts[counter], out holdPart) == true)
                {
                    // ----- The version format is major.minor.build.revision.
                    switch (counter)
                    {
                        case 0:
                            comparePart = currentAssembly.GetName().Version.Major;
                            break;
                        case 1:
                            comparePart = currentAssembly.GetName().Version.Minor;
                            break;
                        case 2:
                            comparePart = currentAssembly.GetName().Version.Build;
                            break;
                        case 3:
                            comparePart = currentAssembly.GetName().Version.Revision;
                            break;
                        default:
                            // ----- Corrupt version number.
                            return result;
                    }
                    if (comparePart != holdPart)
                    {
                        result.Status = LicenseStatus.VersionMismatch;
                        return result;
                    }
                }
            }

            // ----- Everything seems to be in order.
            result.Status = LicenseStatus.ValidLicense;
            return result;
        }
    }
}

