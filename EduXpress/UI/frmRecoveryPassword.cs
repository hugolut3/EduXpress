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
using System.Resources;

namespace EduXpress.UI
{
    public partial class frmRecoveryPassword : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmRecoveryPassword).Assembly);


        public frmRecoveryPassword()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void frmRecoveryPassword_Load(object sender, EventArgs e)
        {
            rdEmail.Checked = true;
          //  txtEmail.Focus();
            this.ActiveControl = txtEmail;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {            
            this.Hide();
           // frmLogin frm = new frmLogin();
          //  frm.StartPosition = FormStartPosition.CenterParent;
          //  frm.ShowDialog();
        }

        private void rdEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (rdEmail.Checked)
            {
                groupEmail.Visible = true;
                groupSMS.Visible = false;
            }
            else
            {
                groupEmail.Visible = false;
                groupSMS.Visible = true;
            }
        }

        private void rdSMS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdSMS.Checked)
            {
                groupEmail.Visible = false;
                groupSMS.Visible = true;
            }
            else
            {
                groupEmail.Visible = true;
                groupSMS.Visible = false;
            }
        }

        private void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterEmail"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                if (pf.CheckForInternetConnection() == true)
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription( LocRM.GetString("strEmailSending"));
                    }

                    DataSet ds = new DataSet();
                    con = new SqlConnection(databaseConnectionString);
                    con.Open();
                    cmd = new SqlCommand("SELECT UserPassword FROM Registration Where Email = '" + txtEmail.Text.Trim() + "'", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    //  con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rdr = cmd.ExecuteReader();
                        con = new SqlConnection(databaseConnectionString);
                        con.Open();
                        string ctn = "select RTRIM(Username),RTRIM(Password),RTRIM(SMTPAddress),(Port),RTRIM(SenderName) from EmailSetting where IsDefault='Yes' and IsActive='Yes'";
                        cmd = new SqlCommand(ctn);
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            pf.SendMail(Convert.ToString(rdr.GetValue(0)), txtEmail.Text,  LocRM.GetString("strYourPassword") + ": " + pf.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["UserPassword"])) + "",  LocRM.GetString("strPassword"), Convert.ToString(rdr.GetValue(2)), Convert.ToInt16(rdr.GetValue(3)), Convert.ToString(rdr.GetValue(0)), pf.Decrypt(Convert.ToString(rdr.GetValue(1))), Convert.ToString(rdr.GetValue(4)));
                            if ((rdr != null))
                            {
                                rdr.Close();
                            }
                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                            XtraMessageBox.Show((  LocRM.GetString("strPasswordSent") + ("\r\n" +  LocRM.GetString("strCheckEmail"))),  LocRM.GetString("strThankYou"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Log password recovery transaction in logs
                            string st = LocRM.GetString("strEmailPasswordRecoverySent")  +" " + txtEmail.Text;
                            pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                            this.Hide();
                            con.Close();
                        }


                    }
                    else
                    {
                        if (splashScreenManager1.IsSplashFormVisible == true)
                        {
                            splashScreenManager1.CloseWaitForm();
                        }
                        XtraMessageBox.Show(( LocRM.GetString("strNoEmailProfileFound") + ". " + ("\r\n" + LocRM.GetString("strCheckEnteredDetailsCorrectly") )), LocRM.GetString("strThankYou"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                    }
                }
                else
                {
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    XtraMessageBox.Show( LocRM.GetString("strNoInternetEmailNotSent"),  LocRM.GetString("strPasswordRecovery"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneNo.Focus();
                    return;
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

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            if (txtPhoneNo.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterCellphoneNo") + " ", LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhoneNo.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                if (pf.CheckForInternetConnection() == true)
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
                            //Check if Company Profile has data in database

                            con = new SqlConnection(databaseConnectionString);
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
                                XtraMessageBox.Show( LocRM.GetString("strNoSMSNotificationSent"), LocRM.GetString("strNotificationError")  , MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription( LocRM.GetString("strSendingSMS"));
                    }

                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;

                    DataSet ds = new DataSet();
                    con = new SqlConnection(databaseConnectionString);
                    con.Open();
                    cmd = new SqlCommand("SELECT UserPassword FROM Registration Where PhoneNumber = '" + txtPhoneNo.Text + "'", con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    //  con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rdr = cmd.ExecuteReader();

                        /// There is also a Visual Studio NuGet Package available, see
                        /// https://get.cmtelecom.com/microsoft-dotnet-nuget-pack/
                        /// http://www.cmtelecom.com/mobile-messaging/plugins/microsoft-dotnet-nuget-pack
                        try
                        {

                            SmsGatewayClient smsGateway = new SmsGatewayClient(pf.Decrypt(Properties.Settings.Default.SettingSMSTocken));
                            smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, cellNo, LocRM.GetString("strYourPassword")  +" : " + pf.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["UserPassword"])));

                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                            XtraMessageBox.Show((  LocRM.GetString("strPasswordSent") +". " + ("\r\n" +  LocRM.GetString("strCheckPhone"))), LocRM.GetString("strThankYou")  , MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();

                            //Log password recovery transaction in logs
                            string st = LocRM.GetString("strSMSPasswordRecoverySent")  +" " + txtPhoneNo.Text;
                            pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                            //Log send SMS transaction in SMS logs
                            pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, cellNo);

                            con.Close();
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
                        XtraMessageBox.Show(( LocRM.GetString("strNoPhoneProfileFound") +". " + ("\r\n" +  LocRM.GetString("strCheckEnteredDetailsCorrectly"))), LocRM.GetString("strThankYou") , MessageBoxButtons.OK, MessageBoxIcon.Information);
                        con.Close();
                    }
                }
                else
                {
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    XtraMessageBox.Show( LocRM.GetString("strNoInternetToSendSMS"),  LocRM.GetString("strPasswordRecovery") , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPhoneNo.Focus();
                    return;
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

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex rEMail = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtEmail.Text))
                {
                    XtraMessageBox.Show( LocRM.GetString("strInvalidEmailAddress") , LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmail.SelectAll();
                    e.Cancel = true;
                }
            }
        }
    }
}