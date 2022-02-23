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
using CM.Sms;
using static EduXpress.Functions.PublicVariables;
using System.Resources;
using EduXpress.Functions;

namespace EduXpress.Reports
{
    public partial class frmReportSMS : DevExpress.XtraEditors.XtraForm
    {
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmReportSMS).Assembly);
        public frmReportSMS()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            clear();
        }
        private void clear()
        {  
            textBox1.Text = "";
            txtPhoneNo.Text = "";
            memoMessage.Text = "";
            memoTemplateMessage.Text = "";
            comboMessageID.SelectedIndex = -1;
            comboTemplateName.SelectedIndex = -1;
        }

        private void frmReportSMS_Load(object sender, EventArgs e)
        {
            FillSMSTemplateName();
            txtPhoneNo.Text = NotificationNo;
        }

        private void btnUnsupportedCharacters_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            Office.frmUnsupportedCharacters frm = new Office.frmUnsupportedCharacters();
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
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
                                if (textBox1.TextLength <= 160)
                                {
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, memoMessage.Text);
                                    //Log send SMS transaction in SMS logs
                                    string st = LocRM.GetString("strSMSsentTo") + " " + SMSCellNo;
                                    pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, SMSCellNo);
                                }
                                if ((textBox1.TextLength > 160) && (textBox1.TextLength <= 310))
                                {
                                    string message1 = textBox1.Text.Substring(0, 155);
                                    message1 = "(1/2)" + message1;
                                    string message2 = textBox1.Text.Substring(155, textBox1.TextLength - 155);
                                    message2 = "(2/2)" + message2;
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, message1);
                                    smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, SMSCellNo, message2);

                                    //Log send SMS transaction in SMS logs. 2 sms sent
                                    string st = "";
                                    for (int i = 1; i < 3; i++)
                                    {
                                        st = "(" + i + "/2) " + LocRM.GetString("strSMSsentTo") + " " + SMSCellNo;
                                        pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, SMSCellNo);
                                    }

                                }
                                if ((textBox1.TextLength > 310) && (textBox1.TextLength <= 465))
                                {
                                    string message1 = textBox1.Text.Substring(0, 155);
                                    message1 = "(1/3)" + message1;
                                    string message2 = textBox1.Text.Substring(155, 155);
                                    message2 = "(2/3)" + message2;
                                    string message3 = textBox1.Text.Substring(310, textBox1.TextLength - 310);
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

                        string st = LocRM.GetString("strNoInternetToSendSMS");
                        // barStaticItemProcess.Caption = "No Internet Connection to send SMS notification";
                        pf.LogFunc(UserLoggedSurname, st);
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (splashScreenManager1.IsSplashFormVisible == true)
            {
                splashScreenManager1.CloseWaitForm();
            }
            locker1.IsCanceled = false;
            if (e.Error != null)
            {
                string s2 = string.Format("{0} " + LocRM.GetString("strSMSsent"), indexSentSMS);
                //Log send SMS transaction in logs
                string st = s2 + " " + e.Error.Message;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                XtraMessageBox.Show(s2 + " " + e.Error.Message); return;
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
        Functions.Locker locker1 = new Functions.Locker();
        private void btnSend_Click(object sender, EventArgs e)
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

            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation to send the SMS
                backgroundWorker1.RunWorkerAsync(locker1);
            }
        }
    }
}