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
using CM.Sms;
using static EduXpress.Functions.PublicVariables;
using System.Resources;

namespace EduXpress.Office
{
    public partial class userControlOfficeSMS : DevExpress.XtraEditors.XtraUserControl
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlOfficeSMS).Assembly);
        public userControlOfficeSMS()
        {
            InitializeComponent();
            backgroundWorker1.WorkerSupportsCancellation = true;
        }
        bool filter = false;
        bool isStudent = false;
        bool isEmployee = false;
        private void btnLoadStudents_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (filter ==false)
            {
                fillCycle();
                filter = true;
            }
            groupSectionClass.Enabled = true;
            gridControlListStudents.Enabled = true;
            gridControlListStudents.Visible = true;
            gridControlListEmployees.Visible = false;
            comboCycle.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;

            gridControlListStudents.DataSource = GetdataStudents();
            btnPhoneNumbers.Enabled = true;
            isStudent = true;
            isEmployee = false;
            txtName.Text = "";
            txtPhoneNo.Text = "";
            memoMessage.Text = "";
            //Fill SMS Template Name 
            FillSMSTemplateName();
        }
        private void getPhoneNumbers()
        {            
            string phoneNumbers="";
            string phoneNo = "";
            txtName.Text = "";
            txtPhoneNo.Text = "";
            try
            {
                if(isStudent)
                {
                    for (int i = 0; i < gridView1.DataRowCount; ++i)
                    {
                        DataRow row = gridView1.GetDataRow(i);
                        if (gridView1.DataRowCount > 0)
                        {
                            phoneNo = row[LocRM.GetString("strNotificationNo").ToUpper()].ToString();
                            if (phoneNo != "")
                            {
                                //phoneNumbers = phoneNumbers + row[LocRM.GetString("strNotificationNo").ToUpper()].ToString() + ",";
                                phoneNumbers = phoneNumbers + phoneNo + ",";
                            }                                
                        }

                    }

                }
                if (isEmployee)
                {
                    for (int i = 0; i < gridView2.DataRowCount; ++i)
                    {
                        DataRow row = gridView2.GetDataRow(i);
                        if (gridView2.DataRowCount > 0)
                        {
                            phoneNo = row[LocRM.GetString("strPhoneNumber").ToUpper()].ToString();
                            if (phoneNo != "")
                            {
                                phoneNumbers = phoneNumbers + phoneNo + ",";
                            }                              
                        }

                    }
                }

                if (phoneNumbers != "")
                {
                    txtPhoneNo.Text = phoneNumbers.Remove(phoneNumbers.Length - 1, 1); //Remove the last ","
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
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
       
        //Fill dataGridViewStudents
        public DataView GetdataStudents()
        {
            string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();            
            string ClassColumn = LocRM.GetString("strClass").ToUpper();
            string CycleColumn = LocRM.GetString("strCycle").ToUpper();
            string SectionColumn = LocRM.GetString("strSection").ToUpper();
            string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
            string NameColumn = LocRM.GetString("strName").ToUpper();         
            string FatherSurnameColumn = LocRM.GetString("strFatherSurname").ToUpper();
            string FatherNameColumn = LocRM.GetString("strFatherName").ToUpper();           
            string NotificationNoColumn = LocRM.GetString("strNotificationNo").ToUpper();            
            string MotherSurnameColumn = LocRM.GetString("strMotherSurname").ToUpper();
            string MotherNamesColumn = LocRM.GetString("strMotherNames").ToUpper();           
            string EmergencyPhoneNoColumn = LocRM.GetString("strEmergencyPhoneNo").ToUpper();
            string AbsencePhoneNoColumn = LocRM.GetString("strAbsencePhoneNo").ToUpper();            

            dynamic SelectQry = "SELECT RTRIM(StudentSurname) as [" + SurnameColumn + "]," +
                "RTRIM(StudentFirstNames) as [" + NameColumn + "], RTRIM(StudentNumber) as [" + StudentNoColumn + "]," +
                "RTRIM(NotificationNo) as [" + NotificationNoColumn + "], RTRIM(EmergencyPhoneNo) as [" + EmergencyPhoneNoColumn + "]," +
                "RTRIM(AbsencePhoneNo) as [" + AbsencePhoneNoColumn + "],RTRIM(FatherSurname) as [" + FatherSurnameColumn + "]," +
                "RTRIM(FatherNames) as [" + FatherNameColumn + "] ,RTRIM(MotherSurname) as [" + MotherSurnameColumn + "]," +
                "RTRIM(MotherNames) as [" + MotherNamesColumn + "], RTRIM(Class) as [" + ClassColumn + "]," +
                "RTRIM(Section) as [" + SectionColumn + "],RTRIM(Cycle) as [" + CycleColumn + "]  " +
                "FROM Students order by StudentSurname";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
                if (splashScreenManager2.IsSplashFormVisible == false)
                {
                    splashScreenManager2.ShowWaitForm();
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
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }

                //check which parent is notification SMS
            }
            catch (Exception ex)
            {
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return TableView;
        }
        string notificationName = "";
        private void gridControlListStudents_MouseClick(object sender, MouseEventArgs e)
        {
            string fatherSurname, fatherNames, fatherContactNo, motherSurname, motherNames, motherContactNo = "";
            try
            {
                if (splashScreenManager2.IsSplashFormVisible == false)
                {
                    splashScreenManager2.ShowWaitForm();
                }
                if (gridView1.DataRowCount > 0)
                {
                    txtPhoneNo.Text = txtPhoneNo.Text.TrimEnd();
                    
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT RTRIM(StudentNumber),RTRIM(StudentSurname),RTRIM(StudentFirstNames) ,RTRIM(NotificationNo)," +
                            "RTRIM(FatherSurname),RTRIM(FatherNames) ,RTRIM(FatherContactNo),RTRIM(MotherSurname),RTRIM(MotherNames) ,RTRIM(MotherContactNo) " +
                            "FROM Students WHERE StudentNumber = '" + gridView1.GetFocusedRowCellValue(LocRM.GetString("strStudentNo").ToUpper()).ToString() + "'";
                       
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {                            
                            txtName.Text = (rdr.GetString(1).Trim()) + " " + (rdr.GetString(2).Trim());
                            txtPhoneNo.Text = (rdr.GetString(3).Trim());

                            fatherSurname = (rdr.GetString(4).Trim());
                            fatherNames = (rdr.GetString(5).Trim());
                            fatherContactNo = (rdr.GetString(6).Trim());
                            motherSurname = (rdr.GetString(7).Trim());
                            motherNames = (rdr.GetString(8).Trim());
                            motherContactNo = (rdr.GetString(9).Trim());

                            //check which parent contact number is notification number
                            if (txtPhoneNo.Text.Trim() == fatherContactNo)
                            {
                                notificationName = fatherSurname + " " + fatherNames;
                            }
                            else
                            {
                                notificationName = motherSurname + " " + motherNames;
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
                   //update template with new place holders
                   if(memoTemplateMessage.Text!="")
                    {
                        memoMessage.Text = memoTemplateMessage.Text.Trim();
                    //change the place holder with names in template message
                    //isStudent = true;

                        string PaymentNotificationTemplate = memoMessage.Text.Trim();
                       // if (comboTemplateName.Text == "Invitation Parent")
                       if(comboMessageID.Text=="2")
                        {
                            if (isStudent == true)
                            {
                                PaymentNotificationTemplate = string.Format(PaymentNotificationTemplate, notificationName.ToUpper(), txtName.Text.ToUpper());
                                memoMessage.Text = PaymentNotificationTemplate;
                            }
                            else
                            {
                                PaymentNotificationTemplate = string.Format(PaymentNotificationTemplate, "---", "---");
                            }

                        }
                    }

                }
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }

                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void memoMessage_EditValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = memoMessage.Text;
        }
        int count = 0;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            count = 160 - textBox1.TextLength;
            lblCount.Text = count.ToString() + LocRM.GetString("strCharactersRemaining");
        }

        private void btnClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            clear();
        }
        private void clear()
        {
            txtName.Text = "";
            txtPhoneNo.Text = "";
            textBox1.Text = "";
            memoMessage.Text = "";
            memoTemplateMessage.Text = "";
            comboMessageID.SelectedIndex = -1;
            comboTemplateName.SelectedIndex = -1;
        }
        Functions.Locker locker1 = new Functions.Locker();
        private void btnSendSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // ----- Check for a valid entry.
            if (txtPhoneNo.Text.Trim() == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterCellphoneNoMessage"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
                return;
            }
            if (memoMessage.Text.Trim() == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strMessageEmpty"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                memoMessage.Focus();
                return;
            }

            if (textBox1.TextLength > 465)
            {                
                XtraMessageBox.Show(LocRM.GetString("strSMSLong"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                memoMessage.Focus();
                return;
            }

            //unsupported GSM characters
            char[] unsupportedCharacters = { '’', '`', 'ç', 'œ', 'Œ', 'À', 'È', 'Ì', 'Ò', 'Ù', 'ð', 'Ð', 'á', 'í', 'ó', 'ú', 'ý', 'Á', 'Í', 'Ó', 'Ú', 'Ý', 'ã', 'õ', 'Õ', 'Ã', 'â', 'ê', 'î', 'ô', 'û', 'Â', 'Ê', 'Î', 'Ô', 'Û', 'ë', 'ï', 'ÿ', 'Ë', 'Ï', 'Ÿ' };
            if (textBox1.Text.IndexOfAny(unsupportedCharacters) >= 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strSMSCharactersNotSupported"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                memoMessage.Focus();
                return;
            }


            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation to send the SMS
                backgroundWorker1.RunWorkerAsync(locker1);
            }

        }
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (splashScreenManager1.IsSplashFormVisible == true)
            {
                splashScreenManager1.CloseWaitForm();
            }
            locker1.IsCanceled = false;
            if (e.Error != null)
            {
                string s2 =  LocRM.GetString("strSMSNotSent");                
                //Log send SMS transaction in logs
                string st = s2+" "+ e.Error.Message;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(s2 + " " + e.Error.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }

            else if (e.Cancelled == true)
            {
                string s2 = LocRM.GetString("strSMSNotSent");
                //Log send SMS transaction in logs

                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, s2 +" " + sendSMSCancelInternet);
                XtraMessageBox.Show(s2 + " " + sendSMSCancelInternet, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                sendSMSCancelInternet = "";                
                return;
            }
            else
            {
                string s2 = string.Format("{0} " + LocRM.GetString("strSMSsent"), indexSentSMS);
                XtraMessageBox.Show(s2, LocRM.GetString("strSendingSMSMessage"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                //Log SMS transaction in logs
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, s2);
                clear();
                resetMessage();
            }
        }
        int indexSentSMS;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (worker != null)
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
                          
                            if (Properties.Settings.Default.InternationalCode == "")
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
                                splashScreenManager1.SendCommand(Dialogs.WaitForm2.WaitFormCommand.SendObject, locker1);
                                // splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSendingSMSMessageSplash"));
                            }

                           // Cursor = Cursors.WaitCursor;
                          ///  timer1.Enabled = true;

                            // validate and spilt PhoneNumber if sent to multiple PhoneNumbers 
                            string cellNo = txtPhoneNo.Text.Trim();
                            string[] ToMultiplePhoneNumbers = cellNo.Split(',');
                            var strings = from string entry in ToMultiplePhoneNumbers select entry.Substring(1);

                            SmsGatewayClient smsGateway = new SmsGatewayClient(pf.Decrypt(Properties.Settings.Default.SettingSMSTocken));
                            int numberSMS = ToMultiplePhoneNumbers.Length;
                            string SMSCellNo;
                             indexSentSMS = 0;
                            foreach (string start in strings)
                            {
                                indexSentSMS++;
                                string s = string.Format(LocRM.GetString("strSendingSMSMessageSplash") + " {0} " + LocRM.GetString("strOf") + " {1}", indexSentSMS, numberSMS);
                                splashScreenManager1.SetWaitFormDescription(s);
                                SMSCellNo = Properties.Settings.Default.InternationalCode + start;

                                //chek if sms is longer the 160 characters, send it in parts:
                                if(textBox1.TextLength <=160)
                                {
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, memoMessage.Text);                                    
                                    //Log send SMS transaction in SMS logs
                                    string st = LocRM.GetString("strSMSsentTo") + " " + SMSCellNo;
                                    pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, SMSCellNo);
                                }
                                if( (textBox1.TextLength > 160) && (textBox1.TextLength <=310))
                                {
                                    string message1 = textBox1.Text.Substring(0, 155);
                                    message1 = "(1/2)" + message1;                                    
                                    string message2 = textBox1.Text.Substring(155, textBox1.TextLength-155);
                                    message2 = "(2/2)" + message2;
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, message1);                                    
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, message2);

                                    //Log send SMS transaction in SMS logs. 2 sms sent
                                    string st = "";
                                    for (int i =1;i<3;i++)
                                    {
                                        st ="("+i+"/2) "+ LocRM.GetString("strSMSsentTo") + " " + SMSCellNo;
                                        pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, SMSCellNo);
                                    }
                                     
                                }
                                if ((textBox1.TextLength > 310) && (textBox1.TextLength <= 465))
                                {
                                    string message1 = textBox1.Text.Substring(0, 155);
                                    message1 = "(1/3)" + message1;
                                    string message2 = textBox1.Text.Substring(155, 155);
                                    message2 = "(2/3)" + message2;
                                    string message3 = textBox1.Text.Substring(310, textBox1.TextLength-310);
                                    message3 = "(3/3)" + message3;
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, message1.Trim());
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, message2.Trim());
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, message3.Trim());

                                    //Log send SMS transaction in SMS logs. 3 sms sent
                                    string st = "";
                                    for (int i = 1; i < 4; i++)
                                    {
                                        st = "(" + i + "/3) " + LocRM.GetString("strSMSsentTo") + " " + SMSCellNo;
                                        pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, SMSCellNo);
                                    }
                                }                          

                                
                                
                                if (((Functions.ILocked)e.Argument).IsCanceled)
                                {
                                    if (splashScreenManager1.IsSplashFormVisible == true)
                                    {
                                        splashScreenManager1.CloseWaitForm();
                                    }
                                    e.Cancel = true;
                                    break;
                                }
                            }

                            // barStaticItemProcess.Caption = "SMS notification sent successfuly";
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
                            e.Cancel = true;
                        }
                    }

                    else
                    {
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        XtraMessageBox.Show(LocRM.GetString("strNoInternetToSendSMSMessage"), LocRM.GetString("strSendSMS"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //Failed to send SMS due to internet connection transaction in logs

                        string st = LocRM.GetString("strNoInternetToSendSMSMessage");
                        // barStaticItemProcess.Caption = "No Internet Connection to send SMS notification";
                        pf.LogFunc(UserLoggedSurname, st);
                        sendSMSCancelInternet = LocRM.GetString("strNoInternetToSendSMSMessage");
                        e.Cancel = true;
                        
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
        string sendSMSCancelInternet="";
        private void btnLoadEmployees_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupSectionClass.Enabled = false;

            gridControlListEmployees.Enabled = true;
            gridControlListEmployees.Visible = true;
            gridControlListStudents.Visible = false;
            comboCycle.SelectedIndex = -1;
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            gridControlListEmployees.DataSource = GetdataEmployees();
            btnPhoneNumbers.Enabled = true;
            isStudent = false;
            isEmployee = true;
            txtName.Text = "";
            txtPhoneNo.Text = "";
            memoMessage.Text = "";
            //Fill SMS Template Name 
            FillSMSTemplateName();
        }
        //Fill dataGridViewEmployees
        public DataView GetdataEmployees()
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
                if (splashScreenManager2.IsSplashFormVisible == false)
                {
                    splashScreenManager2.ShowWaitForm();
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
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return TableView;
        }

        private void userControlOfficeSMS_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                gridControlListStudents.DataSource = null;  //clear datagridview
                gridControlListEmployees.DataSource = null;  //clear datagridview
                gridControlListStudents.Enabled = false;
                gridControlListEmployees.Enabled = false;
                gridControlListEmployees.Visible = false;
                gridControlListStudents.Visible = true;
                groupSectionClass.Enabled = false;
                comboCycle.SelectedIndex = -1;
                cmbSection.SelectedIndex = -1;
                cmbClass.SelectedIndex = -1;
                btnPhoneNumbers.Enabled = false;
                isStudent = false;
                isEmployee = false;
                clear();
            }           

        }
        
        private void gridControlListEmployees_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager2.IsSplashFormVisible == false)
                {
                    splashScreenManager2.ShowWaitForm();
                }
                if (gridView2.DataRowCount > 0)
                {
                    txtPhoneNo.Text = txtPhoneNo.Text.TrimEnd();

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();
                        
                        cmd.CommandText = "SELECT RTRIM(EmployeeNumber),RTRIM(Surname),RTRIM(Name) ,RTRIM(PhoneNumber)" +
                            "FROM Registration WHERE EmployeeNumber = '" + gridView2.GetFocusedRowCellValue(LocRM.GetString("strEmployeeNumber").ToUpper()).ToString() + "'";
                       
                        rdr = cmd.ExecuteReader();

                        if (rdr.Read())
                        {
                            txtName.Text = (rdr.GetString(1).Trim()) + " " + (rdr.GetString(2).Trim());
                            txtPhoneNo.Text = (rdr.GetString(3).Trim());
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
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }

                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                if (comboCycle.SelectedIndex >= 3)
                {
                    cmbSection.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClass.Enabled = true;
                    AutocompleteClass();
                    cmbSection.Enabled = false;
                    cmbSection.Properties.Items.Clear();
                }
            }
            else
            {
                if (comboCycle.SelectedIndex >= 4)
                {
                    cmbSection.Enabled = true;
                    AutocompleteSection();
                }
                else
                {
                    cmbClass.Enabled = true;
                    AutocompleteClass();
                    cmbSection.Enabled = false;
                    cmbSection.Properties.Items.Clear();
                }

            }
            
        }
        //Autocomplete Section
        private void AutocompleteSection()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            try
            {
                if (splashScreenManager2.IsSplashFormVisible == false)
                {
                    splashScreenManager2.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SectionName) FROM Sections", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbSection.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbSection.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbSection.SelectedIndex = -1;
                    con.Close();
                }

                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Autocomplete Class
        private void AutocompleteClass()
        {
            try
            {
                if (splashScreenManager2.IsSplashFormVisible == false)
                {
                    splashScreenManager2.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    if (cmbSection.SelectedIndex != -1)
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1 and SectionName=@d2", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCycle.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 60, " SectionName").Value = cmbSection.Text.Trim();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCycle.Text.Trim();
                    }
                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];
                    cmbClass.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbClass.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbClass.SelectedIndex = -1;
                    con.Close();
                }

                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Fill cmbcycle
        private void fillCycle()
        {
            comboCycle.Properties.Items.Clear();

            if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            {
                comboCycle.Properties.Items.AddRange(new object[] 
                {
                    LocRM.GetString("strMaternelle"),
                    LocRM.GetString("strPrimaire") ,
                    LocRM.GetString("strSecondOrientation"),
                    LocRM.GetString("strSecondHuma"),
                    LocRM.GetString("strTVETCollege")
                });
            }
            else
            {
                comboCycle.Properties.Items.AddRange(new object[] 
                {
                    LocRM.GetString("strPrePrimary"),
                    LocRM.GetString("strPreparatory") ,
                    LocRM.GetString("strHighSchoolGET"),
                    LocRM.GetString("strHighSchoolFET"),
                    LocRM.GetString("strTVETCollege")
                });

            }
            
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbClass.Enabled = true;
            AutocompleteClass();
        }

        private void btnSearchSectionClass_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPhoneNo.Text = "";
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            try
            {
                if (splashScreenManager2.IsSplashFormVisible == false)
                {
                    splashScreenManager2.ShowWaitForm();
                }

                string StudentNoColumn = LocRM.GetString("strStudentNo").ToUpper();
                string ClassColumn = LocRM.GetString("strClass").ToUpper();
                string CycleColumn = LocRM.GetString("strCycle").ToUpper();
                string SectionColumn = LocRM.GetString("strSection").ToUpper();
                string SurnameColumn = LocRM.GetString("strSurname").ToUpper();
                string NameColumn = LocRM.GetString("strName").ToUpper();
                string FatherSurnameColumn = LocRM.GetString("strFatherSurname").ToUpper();
                string FatherNameColumn = LocRM.GetString("strFatherName").ToUpper();
                string NotificationNoColumn = LocRM.GetString("strNotificationNo").ToUpper();
                string MotherSurnameColumn = LocRM.GetString("strMotherSurname").ToUpper();
                string MotherNamesColumn = LocRM.GetString("strMotherNames").ToUpper();
                string EmergencyPhoneNoColumn = LocRM.GetString("strEmergencyPhoneNo").ToUpper();
                string AbsencePhoneNoColumn = LocRM.GetString("strAbsencePhoneNo").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT RTRIM(StudentSurname) as [" + SurnameColumn + "]," +
                "RTRIM(StudentFirstNames) as [" + NameColumn + "], RTRIM(StudentNumber) as [" + StudentNoColumn + "]," +
                "RTRIM(NotificationNo) as [" + NotificationNoColumn + "], RTRIM(EmergencyPhoneNo) as [" + EmergencyPhoneNoColumn + "]," +
                "RTRIM(AbsencePhoneNo) as [" + AbsencePhoneNoColumn + "],RTRIM(FatherSurname) as [" + FatherSurnameColumn + "]," +
                "RTRIM(FatherNames) as [" + FatherNameColumn + "] ,RTRIM(MotherSurname) as [" + MotherSurnameColumn + "]," +
                "RTRIM(MotherNames) as [" + MotherNamesColumn + "], RTRIM(Class) as [" + ClassColumn + "]," +
                "RTRIM(Section) as [" + SectionColumn + "],RTRIM(Cycle) as [" + CycleColumn + "]  " +
                    "FROM Students where Section=@d1 and Class=@d2 order by Class ", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Section").Value = cmbSection.Text;
                    cmd.Parameters.Add("@d2", SqlDbType.NChar, 30, " Class").Value = cmbClass.Text;

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "Students");
                    gridControlListStudents.DataSource = myDataSet.Tables["Students"].DefaultView;

                    con.Close();
                }                

                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
            }
            catch (Exception ex)
            {
                if (splashScreenManager2.IsSplashFormVisible == true)
                {
                    splashScreenManager2.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPhoneNumbers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            getPhoneNumbers();
        }


        //// This event handler updates the progress.
        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
        //}
        private void resetMessage()
        {
            comboTemplateName.SelectedIndex = -1;
            memoTemplateMessage.Text = "";
        }
        //Fill SMS Template Name 
        
        private void FillSMSTemplateName()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            resetMessage();
            int indexCount = -1;
            int indexNotificationPayment = -1;
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(TemplateName), MessageID FROM SMSMessageTemplates", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                   // dtable2 = ds.Tables[1];
                    comboTemplateName.Properties.Items.Clear();
                    comboMessageID.Properties.Items.Clear();
                    
                    //fill first comboTemplateName
                    foreach (DataRow drow in dtable.Rows)
                    {
                        comboTemplateName.Properties.Items.Add(drow[0].ToString());
                        comboMessageID.Properties.Items.Add(drow[1].ToString());
                        
                        indexCount++;
                        if (drow[1].ToString() == "1")
                        {
                            indexNotificationPayment = indexCount;
                        }
                    }
                    
                    comboTemplateName.SelectedIndex = -1;
                    comboMessageID.SelectedIndex = -1;

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
                //remove notification message template from the list
               comboTemplateName.Properties.Items.RemoveAt(indexNotificationPayment);
                comboMessageID.Properties.Items.RemoveAt(indexNotificationPayment);
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

        private void txtPhoneNo_TextChanged(object sender, EventArgs e)
        {
            if(txtPhoneNo.Text=="")
            {
                comboTemplateName.Enabled = false;
            }
            else
            {
                comboTemplateName.Enabled = true;
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
                            textBox1.Text = Convert.ToString(rdr.GetValue(0));
                            comboTemplateName.Text = rdr.GetString(1).Trim();
                            memoTemplateMessage.Text = rdr.GetString(2).Trim();
                            memoMessage.Text = rdr.GetString(2).Trim();
                            comboMessageID.Text = rdr.GetValue(4).ToString();
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

                    //change the place holder with names in template message
                   // isStudent = true;                 


                    string PaymentNotificationTemplate = memoMessage.Text.Trim();
                    //  if (comboTemplateName.Text == "Invitation Parent")
                    if (comboMessageID.Text == "2")
                    {
                        if (isStudent == true)
                        {
                            PaymentNotificationTemplate = string.Format(PaymentNotificationTemplate, notificationName.ToUpper(), txtName.Text.ToUpper());
                            memoMessage.Text = PaymentNotificationTemplate;
                        }
                        else
                        {
                            PaymentNotificationTemplate = string.Format(PaymentNotificationTemplate, "---", "---");
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

        private void btnUnsupportedCharacters_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            frmUnsupportedCharacters frm = new frmUnsupportedCharacters();
            frm.ShowDialog();
        }

        private void userControlOfficeSMS_Load(object sender, EventArgs e)
        {
            txtName.Properties.AdvancedModeOptions.Label = LocRM.GetString("strNameGeneral") + ": ";
            txtPhoneNo.Properties.AdvancedModeOptions.Label = LocRM.GetString("strPhoneNumber") + ": ";
        }
    }
}
