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
using System.Security.Cryptography;
using CM.Sms;
using System.IO;
using System.Reflection;
using DevExpress.XtraEditors.Camera;
using static EduXpress.Functions.PublicVariables;
using System.Resources;
using DevExpress.Utils.Menu;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;
using DevExpress.Data.Camera;

namespace EduXpress.Admin
{
    public partial class userControlEmployeeRegistration : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlEmployeeRegistration).Assembly);


        public userControlEmployeeRegistration()
        {
            InitializeComponent();
        }

        //private void btnExitApplication_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}
        //reset controls 
        private void Reset()
        {
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtPhoneNo.Text = "";
            txtFirstNameEmployee.Text = "";
            txtSurnameEmployee.Text = "";
            txtEmail.Text = "";
            txtEmployeeNumber.Text = "";
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
            cmbUserType.SelectedIndex = -1;
            //dispose of images
            if (customPictureEdit1 != null && customPictureEdit1.Image != null)
            {
                customPictureEdit1.Image.Dispose();
            }
            customPictureEdit1.Image = null;
            groupControlEmployeeDetails.Enabled = false;
            btnConfirmPassword.Visible = false;
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
        //Fill dataGridViewEmployees
        public DataView Getdata()
        {
            string EmployeeNumberColumn = LocRM.GetString("strEmployeeNumber").ToUpper();
            string UserNameColumn = LocRM.GetString("strUserName").ToUpper();
            string UserTypeColumn = LocRM.GetString("strUserType").ToUpper();
            string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            string NameColumn = LocRM.GetString("strName").ToUpper();
            string EmailColumn = LocRM.GetString("strEmail").ToUpper();
            string PhoneNumberColumn = LocRM.GetString("strPhoneNumber").ToUpper();


            dynamic SelectQry = "SELECT  RTRIM(EmployeeNumber) as [" + EmployeeNumberColumn + "], " +
                "RTRIM(Surname) as [" + SurnameColumn + "]," +
                "RTRIM(Name) as [" + NameColumn + "],RTRIM(UserName) as [" + UserNameColumn + "]," +
                "RTRIM(UserType) as [" + UserTypeColumn + "],RTRIM(PhoneNumber) as [" + PhoneNumberColumn + "]," +
                "RTRIM(Email) as [" + EmailColumn + "] FROM Registration order by Surname";
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
        //Autocomplete
        private void Autocomplete()
        {
            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("SELECT UserName FROM Registration", con);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Registration");

                AutoCompleteStringCollection col = new AutoCompleteStringCollection();
                int i = 0;
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    col.Add(ds.Tables[0].Rows[i]["UserName"].ToString());

                }
                txtUserName.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtUserName.MaskBox.AutoCompleteCustomSource = col;
                txtUserName.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;

                con.Close();
            }
                
        }
        // call function to Generate unique Employee Number
        private void autoGenerateFileNumber()
        {
            txtEmployeeNumber.Text = "EM-" + GetUniqueKey(6);
        }
        //Generate unique File number
        public static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars =
            "123456789".ToCharArray();
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
        //register flag. 1= register, 2 = update
        int saveUpdate = 0;
        // send SMS
        private void sendSMS()
        {
            try
            {
                if (pf.CheckForInternetConnection() == true)
                {
                    /// There is also a Visual Studio NuGet Package available, see
                    /// https://get.cmtelecom.com/microsoft-dotnet-nuget-pack/
                    /// http://www.cmtelecom.com/mobile-messaging/plugins/microsoft-dotnet-nuget-pack
                    try
                    {

                        // validate PhoneNumber;
                        string cellNo = txtPhoneNo.Text;
                        cellNo = cellNo.Substring(1);

                        if (Properties.Settings.Default.InternationalCode != "")
                        {
                            cellNo = Properties.Settings.Default.InternationalCode + cellNo;
                        }
                        else
                        {
                            //read SMS settings from database
                            try
                            {
                                //Check if SMS Profile has data in database

                                using (con = new SqlConnection(databaseConnectionString))
                                {
                                    con.Open();
                                    string ct = "select * from SMSSettings ";

                                    cmd = new SqlCommand(ct);
                                    cmd.Connection = con;
                                    rdr = cmd.ExecuteReader();

                                    if (rdr.HasRows)
                                    {
                                        //load data from database to controls
                                        if (rdr.Read())
                                        {
                                            //save SMS gateway profile in application settings                                        
                                            Properties.Settings.Default.SettingSMSProvider = (rdr.GetString(1).ToString().Trim());
                                            Properties.Settings.Default.SettingSMSCompany = (rdr.GetString(2).ToString().Trim());
                                            Properties.Settings.Default.SettingSMSTocken = (rdr.GetString(3).ToString().Trim());
                                            Properties.Settings.Default.InternationalCode = (rdr.GetString(4).ToString().Trim());

                                            cellNo = (rdr.GetString(4).ToString().Trim()) + cellNo;

                                            // ----- Save any updated settings.
                                            Properties.Settings.Default.Save();
                                        }
                                    }
                                    else
                                    {
                                        XtraMessageBox.Show(LocRM.GetString("strNoSMSNotificationSent"), LocRM.GetString("strNotificationError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
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
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                            splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSendingSMS")); 
                        }
                        
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;

                        SmsGatewayClient smsGateway = new SmsGatewayClient(pf.Decrypt(Properties.Settings.Default.SettingSMSTocken));
                        if (saveUpdate == 1)
                        {
                            smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, cellNo, txtFirstNameEmployee.Text + " " + txtSurnameEmployee.Text + " " +   LocRM.GetString("strRegisteredSuccessfullyAs") + " " + cmbUserType.Text + ". " +  LocRM.GetString("strUserNameIs") + ": " + txtUserName.Text + ", " +  LocRM.GetString("strPasswordIs") +": " + txtPassword.Text);
                            //Log send SMS transaction in SMS logs
                            string st =  LocRM.GetString("strEmployeeRegistrationNotification") + ": " + LocRM.GetString("strSurname") + ": " + txtSurnameEmployee.Text + " " + LocRM.GetString("strName") + ": " + txtFirstNameEmployee.Text ;
                            pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, cellNo);
                        }
                        if (saveUpdate == 2)
                        {
                            smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, cellNo, txtFirstNameEmployee.Text + " " + txtSurnameEmployee.Text + " " + LocRM.GetString("strDetailsUpdated") + " "+LocRM.GetString("strAs")+ " " + cmbUserType.Text + ". " + LocRM.GetString("strUserNameIs") + ": " + txtUserName.Text + ", " + LocRM.GetString("strPasswordIs") + ": " + txtPassword.Text);
                            //Log send SMS transaction in SMS logs
                            string st = LocRM.GetString("strEmployeeUpdateNotification") + ": " + LocRM.GetString("strSurname") + ": " + txtSurnameEmployee.Text + " " + LocRM.GetString("strName") + ": " + txtFirstNameEmployee.Text;
                            pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, cellNo);
                        }
                        // saveUpdate = 0;
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

                else
                {
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    XtraMessageBox.Show(LocRM.GetString("strNoInternetToSendSMS"), LocRM.GetString("strSendSMS"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Failed to send SMS due to internet connection transaction in logs
                    if (saveUpdate == 1)
                    {
                        string st = LocRM.GetString("strNoInternetToSendSMS") + ". " +  LocRM.GetString("strNewEmployeeSaved") + ": " + txtFirstNameEmployee.Text + " " + txtSurnameEmployee.Text + " " +  LocRM.GetString("strRegisteredAs") + " " + cmbUserType.Text + ". " +  LocRM.GetString("strUserName") +": " + txtUserName.Text ;
                       // barStaticItemProcess.Caption = "No Internet Connection to send SMS notification";
                        pf.LogFunc(UserLoggedSurname, st);
                    }
                    if (saveUpdate == 2)
                    {
                        string st = LocRM.GetString("strNoInternetToSendSMS") + ". " + LocRM.GetString("strDetailsOf") + " " + txtFirstNameEmployee.Text + " " + txtSurnameEmployee.Text + " " + ". " + LocRM.GetString("strUserName") + ": " + txtUserName.Text + " " + LocRM.GetString("strHasBeenUpdated") ;
                       // barStaticItemProcess.Caption = "No Internet Connection to send SMS notification";
                        pf.LogFunc(UserLoggedSurname, st);
                    }
                    //  saveUpdate = 0;
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
        //send Email
        private void sendEmail()
        {
            try
            {
                if (pf.CheckForInternetConnection() == true)
                {
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSendingEmail"));
                    }

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ctn = "select RTRIM(Username),RTRIM(Password),RTRIM(SMTPAddress),(Port),RTRIM(SenderName) from EmailSetting where IsDefault='Yes' and IsActive='Yes'";
                        cmd = new SqlCommand(ctn);
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            if (saveUpdate == 1)
                            {
                                pf.SendMail(Convert.ToString(rdr.GetValue(0)), txtEmail.Text, txtFirstNameEmployee.Text + " " + txtSurnameEmployee.Text + " " + LocRM.GetString("strRegisteredSuccessfullyAs") + " " + cmbUserType.Text + ". " + LocRM.GetString("strUserNameIs") + ": " + txtUserName.Text + " " + LocRM.GetString("strPasswordIs") + ": " + txtPassword.Text + ". " + LocRM.GetString("strKeepInfoSave"), LocRM.GetString("strUserInfo"), Convert.ToString(rdr.GetValue(2)), Convert.ToInt16(rdr.GetValue(3)), Convert.ToString(rdr.GetValue(0)), pf.Decrypt(Convert.ToString(rdr.GetValue(1))), Convert.ToString(rdr.GetValue(4)));
                                //  barStaticItemProcess.Caption = "Email notification sent successfully";
                            }
                            if (saveUpdate == 2)
                            {
                                pf.SendMail(Convert.ToString(rdr.GetValue(0)), txtEmail.Text, txtFirstNameEmployee.Text + " " + txtSurnameEmployee.Text + " " + LocRM.GetString("strDetailsUpdated") +" " +  LocRM.GetString("strAs") + " " + cmbUserType.Text + ". " + LocRM.GetString("strUserNameIs") + ": " + txtUserName.Text + " " + LocRM.GetString("strPasswordIs") + ": " + txtPassword.Text + ". " + LocRM.GetString("strKeepInfoSave"), LocRM.GetString("strUserInfo"), Convert.ToString(rdr.GetValue(2)), Convert.ToInt16(rdr.GetValue(3)), Convert.ToString(rdr.GetValue(0)), pf.Decrypt(Convert.ToString(rdr.GetValue(1))), Convert.ToString(rdr.GetValue(4)));
                                // barStaticItemProcess.Caption = "Email notification sent successfully";
                            }
                            if ((rdr != null))
                            {
                                rdr.Close();
                            }
                            con.Close();
                            // saveUpdate = 0;
                        }
                    }
                       

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                }
                else
                {
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    MessageBox.Show(LocRM.GetString("strNoInternetToSendEmail"), LocRM.GetString("strUserInfo") , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Failed to send email due to internet connection transaction in logs
                    if (saveUpdate == 1)
                    {
                        string st = LocRM.GetString("strNoInternetToSendEmail") + ". " + LocRM.GetString("strNewEmployeeSaved") + ": " + txtFirstNameEmployee.Text + " " + txtSurnameEmployee.Text + " " + LocRM.GetString("strRegisteredAs") + " " + cmbUserType.Text + ". " + LocRM.GetString("strUserName") + ": " + txtUserName.Text;
                     //   barStaticItemProcess.Caption = "No Internet Connection to send Email notification";
                        pf.LogFunc(UserLoggedSurname, st);
                    }
                    if (saveUpdate == 2)
                    {
                        string st = LocRM.GetString("strNoInternetToSendEmail") + ". " + LocRM.GetString("strDetailsOf") + " " + txtFirstNameEmployee.Text + " " + txtSurnameEmployee.Text + " " + ". " + LocRM.GetString("strUserName") + ": " + txtUserName.Text + " " + LocRM.GetString("strHasBeenUpdated");
                      //  barStaticItemProcess.Caption = "No Internet Connection to send Email notification";
                        pf.LogFunc(UserLoggedSurname, st);
                    }
                    //  saveUpdate = 0;
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void btnEmployeeAvailability_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strErrorUsername"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Focus();
                    return;
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select UserName from Registration where UserName=@find";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.NChar, 30, "Username"));
                    cmd.Parameters["@find"].Value = txtUserName.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strUsernameNotAvailable"), LocRM.GetString("strUserInfo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strUsernameAvailable"), LocRM.GetString("strUserInfo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUserName.Focus();

                    }
                    if ((rdr != null))
                    {
                        rdr.Close();
                    }
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

        private void btnRemovePicture_Click(object sender, EventArgs e)
        {
            customPictureEdit1.EditValue = null;
        }

        private void btnBrowsePicture_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            //the name can be on of the following values: OnClickedLoad;OnClickedSave;OnClickedCut;OnClickedCopy;OnClickedPaste;OnClickedDelete
             
            try
            {
                //load picture to pictureEmployee
                InvokeMenuMethod(GetMenu(customPictureEdit1), "OnClickedLoad");

                //save employee picture in picture folders
                if (customPictureEdit1.Image != null)
                {
                    //invoke the pictureEdit copy image 
                    //the name can be on of the following values: OnClickedLoad;OnClickedSave;OnClickedCut;OnClickedCopy;OnClickedPaste;OnClickedDelete
                    InvokeMenuMethod(GetMenu(customPictureEdit1), "OnClickedCopy"); //save image to clipboard  
                                                                                 //save image from clipboard
                    if (Clipboard.GetDataObject() != null)
                    {
                        IDataObject data = Clipboard.GetDataObject();
                        string destinationDirectory = Properties.Settings.Default.StudentsPhotosDirectory;

                        if (destinationDirectory == null)
                        {
                            destinationDirectory = "C:\\Students_Photos\\";
                            bool exists = System.IO.Directory.Exists("C:\\Students_Photos\\");
                            if (!exists)
                                System.IO.Directory.CreateDirectory("C:\\Students_Photos\\");
                        }

                        if (data.GetDataPresent(DataFormats.Bitmap))
                        {
                            if (File.Exists(destinationDirectory + "\\" + txtEmployeeNumber.Text + "." + ".jpg"))
                            {
                                XtraMessageBox.Show(LocRM.GetString("strImageAlreadySaved"));
                            }
                            
                            using (Image image = (Image)data.GetData(DataFormats.Bitmap))
                            {
                                if (Properties.Settings.Default.DirectoryStudentPhotoWriteAccess == false)
                                {
                                    //check directory write access
                                    HasWritePermission(destinationDirectory);
                                }
                                image.Save(destinationDirectory + "\\" + txtEmployeeNumber.Text + ".jpg");
                            }
                            
                        }
                        else
                        {
                            XtraMessageBox.Show(LocRM.GetString("strNoImageClipboard"));
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strClipboardEmpty"));
                    }
                }
             }
                 catch (Exception ex)
                 {
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
            }
        
        private void btnWebcam_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            
            try
            {
                //TakePictureDialog d = new TakePictureDialog();

                //    if (d.ShowDialog() == DialogResult.OK)
                //    {
                //    Image i = d.Image;

                //        pictureEmployee.Image = i;                   

                //    }
                //Open the custom picture dialog
                customPictureEdit1.ShowTakePictureDialog();
                if (customPictureEdit1.Image != null)
                {
                    //invoke the pictureEdit copy image 
                    //the name can be on of the following values: OnClickedLoad;OnClickedSave;OnClickedCut;OnClickedCopy;OnClickedPaste;OnClickedDelete
                    InvokeMenuMethod(GetMenu(customPictureEdit1), "OnClickedCopy"); //save image to clipboard  

                    //save image from clipboard
                    if (Clipboard.GetDataObject() != null)
                    {
                        IDataObject data = Clipboard.GetDataObject();
                        string destinationDirectory = Properties.Settings.Default.StudentsPhotosDirectory;

                        //if (destinationDirectory=="")
                        if (destinationDirectory == null)
                        {
                            destinationDirectory = "C:\\Students_Photos\\";
                            bool exists = System.IO.Directory.Exists("C:\\Students_Photos\\");
                            if (!exists)
                                System.IO.Directory.CreateDirectory("C:\\Students_Photos\\");
                        }

                        if (data.GetDataPresent(DataFormats.Bitmap))
                        {
                            if (File.Exists(destinationDirectory + "\\" + txtEmployeeNumber.Text + "." + ".jpg"))
                            {
                                XtraMessageBox.Show(LocRM.GetString("strImageAlreadySaved"));
                            }

                            using (Image image = (Image)data.GetData(DataFormats.Bitmap, true))
                            {
                                if (Properties.Settings.Default.DirectoryStudentPhotoWriteAccess == false)
                                {
                                    //check directory write access
                                    HasWritePermission(destinationDirectory);
                                }
                                image.Save(destinationDirectory + "\\" + txtEmployeeNumber.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(LocRM.GetString("strNoImageClipboard"));
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strClipboardEmpty"));
                    }
                }
            }
            catch (Exception ex)
            {
                // GeneralError("userControlstudentEnrolmentForm.WebCam", ex);
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
       
        }     
        
                
            //check file write permission
            public static bool HasWritePermission(string tempfilepath)
        {
            ResourceManager LocRM1 = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof( userControlEmployeeRegistration).Assembly);
            try
            {
                System.IO.File.Create(tempfilepath + "temp.txt").Close() ;
                System.IO.File.Delete(tempfilepath + "temp.txt");
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Properties.Settings.Default.DirectoryStudentPhotoWriteAccess = false;
                XtraMessageBox.Show(LocRM1.GetString("strNoPermissionSavePhotos") + " " + Properties.Settings.Default.StudentsPhotosDirectory + ". " + LocRM1.GetString("strContactSystemAdministrator"));
                return false;
            }
            Properties.Settings.Default.DirectoryStudentPhotoWriteAccess = true;
            //XtraMessageBox.Show("has access");
            return true;
        }
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmail.Text))
                {
                    XtraMessageBox.Show( LocRM.GetString("strInvalidEmailAddress"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.SelectAll();
                    e.Cancel = true;
                }
            }
        }
        
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9_]");
            if (txtUserName.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtUserName.Text))
                {
                    XtraMessageBox.Show( LocRM.GetString("strOnlyLetters"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.SelectAll();
                    e.Cancel = true;
                }
            }
        }
        //Fill userTypes
        private void fillUserTypes()
        {
            //Administrator
            //Administrator Assistant
            //Accountant
            //Account & Admission
            //Account & HR
            //Account, Admission & HR
            //Admission Officer
            //Human Resources
            //Librarian
            //Librarian & Admission
            //Stock Clerk
            //Teacher
            //Teacher Administrator
            if (LicenseFeature == "0") //Trial Version        :0 
            {
                cmbUserType.Properties.Items.Clear();
                cmbUserType.Properties.Items.AddRange(new object[] { 
                LocRM.GetString("strAdministrator"),LocRM.GetString("strAssistantAdministrator") ,
                LocRM.GetString("strFinanceOfficer"),LocRM.GetString("strAdmissionOfficer"),
                LocRM.GetString("strFinanceAdmissionOfficer"),LocRM.GetString("strTeacher"), 
                LocRM.GetString("strTeacherAdministrator")
                });                

            }
            if (LicenseFeature == "2")//Standard Version     :2
            {

                cmbUserType.Properties.Items.Clear();
                cmbUserType.Properties.Items.AddRange(new object[] {
                LocRM.GetString("strAdministrator"),LocRM.GetString("strAssistantAdministrator") ,
                LocRM.GetString("strFinanceOfficer"),LocRM.GetString("strAdmissionOfficer"),
                LocRM.GetString("strFinanceAdmissionOfficer")});
            }
            if (LicenseFeature == "3")//Professional Version :3
            {
                cmbUserType.Properties.Items.Clear();
                cmbUserType.Properties.Items.AddRange(new object[] {
                LocRM.GetString("strAdministrator"),LocRM.GetString("strAssistantAdministrator") ,
                LocRM.GetString("strFinanceOfficer"),LocRM.GetString("strAdmissionOfficer"),
                LocRM.GetString("strFinanceAdmissionOfficer"),LocRM.GetString("strTeacher"),
                LocRM.GetString("strTeacherAdministrator")
                });

            }
            if (LicenseFeature == "4")//Lite Version         :4
            {
                cmbUserType.Properties.Items.Clear();
                cmbUserType.Properties.Items.AddRange(new object[] {
                LocRM.GetString("strAdministrator"),LocRM.GetString("strAssistantAdministrator") ,
                LocRM.GetString("strAdmissionOfficer")});

            }
            if (LicenseFeature == "5")//Ultimate Version     :5
            {
                cmbUserType.Properties.Items.Clear();
                cmbUserType.Properties.Items.AddRange(new object[] {
                LocRM.GetString("strAdministrator"),LocRM.GetString("strAssistantAdministrator") ,
                LocRM.GetString("strFinanceOfficer"),LocRM.GetString("strAdmissionOfficer"),
                LocRM.GetString("strFinanceAdmissionOfficer"),LocRM.GetString("strTeacher"),
                LocRM.GetString("strTeacherAdministrator")
                });
            }
            
        }
        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reset();
            if (groupControlEmployeeDetails.Enabled==false)
            {
                groupControlEmployeeDetails.Enabled = true;
                Autocomplete();
                fillUserTypes();
                txtUserName.Focus();
                gridControlEmployees.DataSource = Getdata();
            }       
                        
            btnSave.Enabled = true;
            //genearate new unique employee number
            autoGenerateFileNumber();
            //check if the employee number already exit
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select EmployeeNumber from Registration where EmployeeNumber=@find";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.VarChar, 30, "EmployeeNumber"));
                    cmd.Parameters["@find"].Value = txtEmployeeNumber.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        //genearate new unique employee number
                        autoGenerateFileNumber();
                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        return;
                    }
                }
                    
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (txtUserName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strErrorUsername"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (cmbUserType.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectUserType") , LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbUserType.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strErrorPassword") , LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                XtraMessageBox.Show(LocRM.GetString("strPasswordNotMatch"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
                return;
            }
            if (txtFirstNameEmployee.Text == "")
            {
                MessageBox.Show( LocRM.GetString("strEnterEmployeeName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstNameEmployee.Focus();
                return;
            }
            if (txtSurnameEmployee.Text == "")
            {
                MessageBox.Show( LocRM.GetString("strEnterEmployeeSurname"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurnameEmployee.Focus();
                return;
            }
            if (txtPhoneNo.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterEmployeeCellNo"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
                return;
            }
            if (txtEmail.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterEmployeeEmail"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            if (txtEmployeeNumber.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterEmployeeNo"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmployeeNumber.Focus();
                return;
            }
            if (customPictureEdit1.EditValue == null)
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectPhotoEmployee"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnWebcam.Focus();
                return;
            }

            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select username from Registration where UserName=@find";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SqlParameter("@find", System.Data.SqlDbType.VarChar, 30, "UserName"));
                    cmd.Parameters["@find"].Value = txtUserName.Text;
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strUsernameExists"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Text = "";
                        txtUserName.Focus();


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
                    splashScreenManager1.SetWaitFormDescription( LocRM.GetString("strSaving"));
                }


                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "insert into Registration(UserName,UserType,UserPassword,Name,Surname,PhoneNumber,Email,EmployeeNumber,Picture) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)";

                    cmd = new SqlCommand(cb);

                    cmd.Connection = con;

                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d2", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d3", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d4", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d5", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d6", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d7", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d8", System.Data.SqlDbType.VarChar);

                    cmd.Parameters["@d1"].Value = txtUserName.Text.Trim();
                                       
                    string userType = "";
                    if (cmbUserType.Text.Trim()== LocRM.GetString("strAdministrator"))
                    {
                        userType = "Administrator";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strAssistantAdministrator"))
                    {
                        userType = "Administrator Assistant";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strFinanceOfficer"))
                    {
                        userType = "Accountant";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strAdmissionOfficer"))
                    {
                        userType = "Admission Officer";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strFinanceAdmissionOfficer"))
                    {
                        userType = "Account & Admission";
                    }

                    if (cmbUserType.Text.Trim() == LocRM.GetString("strTeacher"))
                    {
                        userType = "Teacher";
                    }

                    if (cmbUserType.Text.Trim() == LocRM.GetString("strTeacherAdministrator"))
                    {
                        userType = "Teacher Administrator";
                    }

                    cmd.Parameters["@d2"].Value = userType;
                    cmd.Parameters["@d3"].Value = pf.Encrypt(txtPassword.Text);
                    cmd.Parameters["@d4"].Value = txtFirstNameEmployee.Text;
                    cmd.Parameters["@d5"].Value = txtSurnameEmployee.Text;
                    cmd.Parameters["@d6"].Value = txtPhoneNo.Text;
                    cmd.Parameters["@d7"].Value = txtEmail.Text;
                    cmd.Parameters["@d8"].Value = txtEmployeeNumber.Text;

                    // image content
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bmpImage = new Bitmap(customPictureEdit1.Image);
                        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] data = ms.GetBuffer();
                        SqlParameter p = new SqlParameter("@d9", SqlDbType.Binary);
                        p.Value = data;
                        cmd.Parameters.Add(p);
                    }
                        
                    cmd.ExecuteNonQuery();

                    con.Close();
                    //clear bitmap
                    //bmpImage.Dispose();
                    //ms.Dispose();
                }
                    

                //Log send SMS transaction in logs
                string st =  LocRM.GetString("strEmployee") + " " + LocRM.GetString("strWith") + " " + LocRM.GetString("strUserName")+ ": " + txtUserName.Text + ", " + LocRM.GetString("strSurname")  + ": " + txtSurnameEmployee.Text + ", " +  LocRM.GetString("strName")  + ": " + txtFirstNameEmployee.Text + " " + LocRM.GetString("strHasBeenSaved")  ;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show( LocRM.GetString("strSuccessfullySaved") ,  LocRM.GetString("strEmployeeRegistration"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                //send notification
                //0= no notification, 1 send SMS, 2= send email, 3 send both
                saveUpdate = 1;
                if (Properties.Settings.Default.RegistrationUpdateNotification == 1)
                {
                    
                    if (txtPhoneNo.Text != "")
                    {
                        //send SMS
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                        sendSMS();
                    }                
                }
                if (Properties.Settings.Default.RegistrationUpdateNotification == 2)
                {
                    if (txtEmail.Text != "")
                    {
                        //Send email
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                        sendEmail();
                    }
                }
                if (Properties.Settings.Default.RegistrationUpdateNotification == 3)
                {
                    //Send email & SMS
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;

                    if (txtPhoneNo.Text != "")
                    {
                        sendSMS();
                    }
                    if (txtEmail.Text != "")
                    {
                        sendEmail();
                    }
                }

                Autocomplete();
                gridControlEmployees.DataSource = Getdata();
                Reset();
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
            if (txtUserName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strErrorUsername"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (cmbUserType.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectUserType"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbUserType.Focus();
                return;
            }
            if (txtPassword.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strErrorPassword"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                XtraMessageBox.Show(LocRM.GetString("strPasswordNotMatch"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtConfirmPassword.Focus();
                return;
            }
            if (txtFirstNameEmployee.Text == "")
            {
                MessageBox.Show(LocRM.GetString("strEnterEmployeeName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFirstNameEmployee.Focus();
                return;
            }
            if (txtSurnameEmployee.Text == "")
            {
                MessageBox.Show(LocRM.GetString("strEnterEmployeeSurname"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSurnameEmployee.Focus();
                return;
            }
            if (txtPhoneNo.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterEmployeeCellNo"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
                return;
            }
            if (txtEmail.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterEmployeeEmail"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            if (txtEmployeeNumber.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterEmployeeNo"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmployeeNumber.Focus();
                return;
            }
            if (customPictureEdit1.EditValue == null)
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPhotoEmployee"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnWebcam.Focus();
                return;
            }

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(  LocRM.GetString("strUpdating"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "update Registration set UserType=@d2, UserPassword=@d3,Name=@d4,Surname =@d5,PhoneNumber=@d6,Email=@d7, EmployeeNumber=@d8, Picture=@d9 where UserName=@d1";

                    cmd = new SqlCommand(cb);

                    cmd.Connection = con;

                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d2", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d3", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d4", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d5", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d6", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d7", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d8", System.Data.SqlDbType.VarChar);

                    cmd.Parameters["@d1"].Value = txtUserName.Text.Trim();

                    string userType = "";
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strAdministrator"))
                    {
                        userType = "Administrator";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strAssistantAdministrator"))
                    {
                        userType = "Administrator Assistant";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strFinanceOfficer"))
                    {
                        userType = "Accountant";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strAdmissionOfficer"))
                    {
                        userType = "Admission Officer";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strFinanceAdmissionOfficer"))
                    {
                        userType = "Account & Admission";
                    }
                    if (cmbUserType.Text.Trim() == LocRM.GetString("strTeacher"))
                    {
                        userType = "Teacher";
                    }

                    if (cmbUserType.Text.Trim() == LocRM.GetString("strTeacherAdministrator"))
                    {
                        userType = "Teacher Administrator";
                    }

                    cmd.Parameters["@d2"].Value = userType;
                    cmd.Parameters["@d3"].Value = pf.Encrypt(txtPassword.Text);
                    cmd.Parameters["@d4"].Value = txtFirstNameEmployee.Text;
                    cmd.Parameters["@d5"].Value = txtSurnameEmployee.Text;
                    cmd.Parameters["@d6"].Value = txtPhoneNo.Text;
                    cmd.Parameters["@d7"].Value = txtEmail.Text;
                    cmd.Parameters["@d8"].Value = txtEmployeeNumber.Text;


                    // image content
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bmpImage = new Bitmap(customPictureEdit1.Image);
                        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] data = ms.GetBuffer();
                        SqlParameter p = new SqlParameter("@d9", SqlDbType.Binary);
                        p.Value = data;
                        cmd.Parameters.Add(p);
                    }
                        
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                  

                //Log transaction in logs
                string st = LocRM.GetString("strRecordsEmployee")  +": " + LocRM.GetString("strUserName") + ": " + txtUserName.Text + ", " + LocRM.GetString("strSurname") + ": " + txtSurnameEmployee.Text + ", " + LocRM.GetString("strName") + ": " + txtFirstNameEmployee.Text + " " + LocRM.GetString("strHasBeenUpdated") ;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show( LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strEmployeeRegistration"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                //send notification
                saveUpdate = 2;
                if (Properties.Settings.Default.RegistrationUpdateNotification == 1)
                {

                    if (txtPhoneNo.Text != "")
                    {
                        //send SMS
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                        sendSMS();
                    }
                }
                if (Properties.Settings.Default.RegistrationUpdateNotification == 2)
                {
                    if (txtEmail.Text != "")
                    {
                        //Send email
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                        sendEmail();
                    }
                }
                if (Properties.Settings.Default.RegistrationUpdateNotification == 3)
                {
                    //Send email & SMS
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;

                    if (txtPhoneNo.Text != "")
                    {
                        sendSMS();
                    }
                    if (txtEmail.Text != "")
                    {
                        sendEmail();
                    }
                }

                Autocomplete();
                gridControlEmployees.DataSource = Getdata();
                Reset();

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

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show( LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm") , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
            }
        }
        //Delete records
        private void delete_records()
        {
            try
            {

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strDeleting"));
                }

                int RowsAffected = 0;
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cq = "delete from Registration where UserName='" + txtUserName.Text + "'";
                    cmd = new SqlCommand(cq);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }

                    if (RowsAffected > 0)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strRecordsEmployee"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //Log send SMS transaction in logs
                        string st = LocRM.GetString("strRecordsEmployee") + ": " + LocRM.GetString("strUserName") + ": " + txtUserName.Text + ", " + LocRM.GetString("strSurname") + ": " + txtSurnameEmployee.Text + ", " + LocRM.GetString("strName") + ": " + txtFirstNameEmployee.Text + " " + LocRM.GetString("strHasBeenDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                        Autocomplete();
                        gridControlEmployees.DataSource = Getdata();
                        Reset();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strRecordsEmployee"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                        Autocomplete();
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

        
        private void gridControlEmployees_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                if (gridView1.DataRowCount > 0)
                {
                    // txtUserName.Text = txtUserName.Text.TrimEnd();
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "SELECT * FROM Registration WHERE UserName = '" + gridView1.GetFocusedRowCellValue(LocRM.GetString("strUserName").ToUpper()).ToString() + "'";
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            fillUserTypes();

                            txtFirstNameEmployee.Text = (rdr.GetString(4).Trim());
                            txtSurnameEmployee.Text = (rdr.GetString(5).Trim());
                            txtPhoneNo.Text = (rdr.GetString(6).Trim());
                            txtEmail.Text = (rdr.GetString(7).Trim());
                            txtEmployeeNumber.Text = (rdr.GetString(8).Trim());

                            string userType = "";
                            userType = (rdr.GetString(3).Trim());
                            
                            //convert from database to localized string
                            if (userType == "Administrator")
                            {
                                cmbUserType.Text = LocRM.GetString("strAdministrator");
                            }
                            if (userType == "Administrator Assistant")
                            {
                                cmbUserType.Text = LocRM.GetString("strAssistantAdministrator");
                            }
                            if (userType == "Accountant")
                            {
                                cmbUserType.Text = LocRM.GetString("strFinanceOfficer");
                            }
                            if (userType == "Admission Officer")
                            {
                                cmbUserType.Text = LocRM.GetString("strAdmissionOfficer");
                            }
                            if (userType == "Account & Admission")
                            {
                                cmbUserType.Text = LocRM.GetString("strFinanceAdmissionOfficer");
                            }                            
                            
                            txtUserName.Text = (rdr.GetString(1).Trim());
                            txtPassword.Text = "";
                            txtConfirmPassword.Text = "";
                            if (!Convert.IsDBNull(rdr["Picture"]))
                            {
                                byte[] x = (byte[])rdr["Picture"];
                                MemoryStream ms = new MemoryStream(x);
                                customPictureEdit1.Image = Image.FromStream(ms);
                                //   picturePatientImage.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                            }
                            else
                            {
                                customPictureEdit1.EditValue = null;
                            }
                            btnNew.Enabled = true;
                            btnSave.Enabled = false;
                            btnDelete.Enabled = true;
                            btnUpdate.Enabled = true;                            
                            
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

        private void btnLoadEmployees_Click(object sender, EventArgs e)
        {
            groupControlEmployeeDetails.Enabled = true;
            Autocomplete();
            // Reset();
            txtUserName.Focus();
            gridControlEmployees.DataSource = Getdata();           
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            frmUserTypeHelp frm = new frmUserTypeHelp();
            frm.ShowDialog();
        }

        private void userControlEmployeeRegistration_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                Reset();
                btnSave.Enabled = false;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.C) ||
                    e.KeyData == (Keys.Control | Keys.V))
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void txtConfirmPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.C) ||
        e.KeyData == (Keys.Control | Keys.V))
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void txtPassword_Properties_BeforeShowMenu(object sender, DevExpress.XtraEditors.Controls.BeforeShowMenuEventArgs e)
        {
            // e.Menu.Items.Clear();
            foreach (DXMenuItem item in e.Menu.Items)
            {
                if (item.Tag != null && (item.Tag.Equals(DevExpress.XtraEditors.Controls.StringId.TextEditMenuCopy)
                    || item.Tag.Equals(DevExpress.XtraEditors.Controls.StringId.TextEditMenuPaste)))
                {
                    item.Visible = false;
                }
            }
        }

        private void txtConfirmPassword_Properties_BeforeShowMenu(object sender, DevExpress.XtraEditors.Controls.BeforeShowMenuEventArgs e)
        {
            foreach (DXMenuItem item in e.Menu.Items)
            {
                if (item.Tag != null && (item.Tag.Equals(DevExpress.XtraEditors.Controls.StringId.TextEditMenuCopy)
                    || item.Tag.Equals(DevExpress.XtraEditors.Controls.StringId.TextEditMenuPaste)))
                {
                    item.Visible = false;
                }
            }
        }

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowGridPreview(gridControlEmployees);
        }
        //Print Preview datagridview
        private void ShowGridPreview(GridControl grid)
        {
            // Check whether the GridControl can be previewed.
            if (!grid.IsPrintingAvailable)
            {
                MessageBox.Show(LocRM.GetString("strPrintErrorLibraryNotFound"), LocRM.GetString("strError"));
                return;
            }
            try
            {
                // Open the Preview window.
                grid.UseWaitCursor = true;
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                grid.ShowRibbonPrintPreview();
                // grid.ShowPrintPreview();
                grid.UseWaitCursor = false;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                }
                PrintingSystem printingSystem = new PrintingSystem();
                PrintableComponentLink link = new PrintableComponentLink();
                link.Component = gridControlEmployees;
                printingSystem.Links.Add(link);
                link.Print(Properties.Settings.Default.ReportPrinter);
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

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                gridView1.OptionsView.ColumnAutoWidth = false;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".xlsx";
                    //Export to excel
                    gridControlEmployees.ExportToXlsx(fileName);
                    splashScreenManager1.ShowWaitForm();
                    Process.Start(fileName);
                    splashScreenManager1.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = true;
                gridView1.OptionsView.ColumnAutoWidth = false;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".pdf";
                    //Export to pdf
                    gridControlEmployees.ExportToPdf(fileName);
                    //open document
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strEmployeeRegistration"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                        }
                        Process.Start(fileName);
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void btnExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                gridView1.OptionsView.ColumnAutoWidth = false;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".docx";
                    //Export to docx
                    gridControlEmployees.ExportToDocx(fileName);
                    //open document
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strEmployeeRegistration"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                        }
                        Process.Start(fileName);
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void btnExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                gridView1.BestFitColumns();
                gridView1.OptionsPrint.AllowMultilineHeaders = true;
                gridView1.OptionsPrint.AutoWidth = false;
                gridView1.OptionsView.ColumnAutoWidth = false;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".html";
                    //Export to Html
                    gridControlEmployees.ExportToHtml(fileName);
                    //open document
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strEmployeeRegistration"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (splashScreenManager1.IsSplashFormVisible == false)
                        {
                            splashScreenManager1.ShowWaitForm();
                        }
                        Process.Start(fileName);
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void gridView1_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            (e.PrintingSystem as PrintingSystemBase).PageSettings.Landscape = true;
        }
        
        //Picture load event to load and select correct webcam if more used
        private void userControlEmployeeRegistration_Load(object sender, EventArgs e)
        {
          customPictureEdit1.TakePictureDialogShowing += PictureEdit1_TakePictureDialogShowing;
            //pictureEmployee.TakePictureDialogShowing += PictureEmployee_TakePictureDialogShowing;
        }
        //open the custom dialog to take a picture
        private void PictureEdit1_TakePictureDialogShowing (object sender, TakePictureDialogShowingEventArgs e )
        {
            // Load  saved camera settings.
            string cameraSettings = Properties.Settings.Default.CameraSettings;
            if (cameraSettings != "")
            {
                byte[] i = Convert.FromBase64String(cameraSettings);
                using (MemoryStream ms = new MemoryStream(i))
                {
                    e.Form.CameraControl.RestoreSettingsFromStream(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                }
            }
            //if (File.Exists("camera_settings.xml"))
            //{
            //    e.Form.CameraControl.RestoreSettingsFromXml("camera_settings.xml");            
            //}
            else
            {
                ///---select an existing camera
                ///---CameraDeviceInfo deviceInfo = CameraControl.GetDevices().Where(di => di.Name.Contains("Microsoft LifeCam HD-3000")).FirstOrDefault();

                //if (deviceInfo != null)
                //{
                //    CameraDevice device = new CameraDevice(deviceInfo);
                //    e.Device = device;
                //}----------------//
                //select none or default
                CameraDevice device = null;
                e.Device = device;
                e.Form.CameraControl.ShowSettingsButton = true;
            }
        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text!="")
            {
                btnConfirmPassword.Visible = true;
                if (txtConfirmPassword.Text != txtPassword.Text)
                {
                    btnConfirmPassword.ImageOptions.SvgImage = Properties.Resources.close;
                }
                else
                {
                    btnConfirmPassword.ImageOptions.SvgImage = Properties.Resources.actions_checkcircled;
                }
            }
            else
            {
                btnConfirmPassword.Visible = false;
            }
        }
    }

    //Create a new custom Picture Edit control with a diffent dialog with custom save button
    //To create a custom componnet either New item --> Custom COmponnet ---> Then chnage inherit from control, this will generate a new class.
    //To use the same file, add new class outside of public partial class userControlEmployeeRegistration{} 
    public class CustomPictureEdit : PictureEdit
    {
        protected override TakePictureDialog CreateTakePictureDialog()
        {
            return new CustomTakePictureDialog();
        }
    }
    public class CustomTakePictureDialog : TakePictureDialog
    {
        protected override TakePictureForm CreateTakePictureForm()
        {
            return new CustomTakePictureForm(this);
        }
    }
    public class CustomTakePictureForm : TakePictureForm
    {
        public CustomTakePictureForm(CustomTakePictureDialog dialog)
            : base(dialog)
        {
        }

        protected override CameraControl CreateCameraControl()
        {
            return new CustomCameraControl();
        }
    }
    public class CustomCameraControl : CameraControl
    {
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlEmployeeRegistration).Assembly);
        protected override XtraForm CreateSettingsForm()
        {
            XtraForm settingsForm = base.CreateSettingsForm();
            SimpleButton saveButton = new SimpleButton();
            saveButton.Text = LocRM.GetString("strSaveCameraSettings");
            //saveButton.AutoSize = true;
            saveButton.Click += SaveButton_Click;
            Control cameraSettingControl = settingsForm.Controls["CameraSettingsControl"];
            Control panelControl = cameraSettingControl.Controls["panelControl1"];
            System.Drawing.Point closeButtonLocation = panelControl.Controls["simpleButtonClose"].Location;
            
            saveButton.Location = new System.Drawing.Point(closeButtonLocation.X - saveButton.Width - saveButton.Margin.Right * 2, closeButtonLocation.Y);
            panelControl.Controls.Add(saveButton);
            return settingsForm;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                SaveSettingsToStream(ms);
                ms.Seek(0, SeekOrigin.Begin);
                Properties.Settings.Default.CameraSettings = Convert.ToBase64String(ms.ToArray());
                Properties.Settings.Default.Save();
            }
            //SaveSettingsToXml("camera_settings.xml");
        }
    }
}
