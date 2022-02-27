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
using static EduXpress.Functions.PublicVariables;
using static EduXpress.Functions.PublicFunctions;

namespace EduXpress.UI
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();

        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmLogin).Assembly);
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Size = new System.Drawing.Size(654, 248);
          //  this.WindowState = FormWindowState.Normal;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // this.Hide();
            this.DialogResult = DialogResult.Cancel;
            loginCanceled = true;
            Role = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            

            // ----- Check for a valid entry.
            Role = 0;
            if (txtUserName.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strErrorUsername"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (txtPassword.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strErrorPassword"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Focus();
                return;
            }

            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                if (splashScreenManagerWait.IsSplashFormVisible == false)
                {
                    splashScreenManagerWait.ShowWaitForm();
                }
                con = new SqlConnection(databaseConnectionString);
                con.Open();
                cmd = con.CreateCommand();
                cmd.CommandText = "SELECT RTRIM(UserName),RTRIM(UserPassword) FROM Registration where UserName = @d1 and UserPassword=@d2 ";
                cmd.Parameters.AddWithValue("@d1", txtUserName.Text);
                cmd.Parameters.AddWithValue("@d2", pf.Encrypt(txtPassword.Text));
                rdr = cmd.ExecuteReader();

                if (splashScreenManagerWait.IsSplashFormVisible == true)
                {
                    splashScreenManagerWait.CloseWaitForm();
                }
                if (rdr.Read())
                {
                    if (pf.Encrypt(txtPassword.Text).ToString() == rdr.GetValue(1).ToString())
                    {
                        if (splashScreenManagerWait.IsSplashFormVisible == false)
                        {
                            splashScreenManagerWait.ShowWaitForm();
                        }

                        con = new SqlConnection(databaseConnectionString);
                        con.Open();
                        cmd = con.CreateCommand();
                        cmd.CommandText = "SELECT UserType,Surname,Name  FROM Registration where UserName=@d3 and UserPassword=@d4";
                        cmd.Parameters.AddWithValue("@d3", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@d4", pf.Encrypt(txtPassword.Text));
                        rdr = cmd.ExecuteReader();

                        if (splashScreenManagerWait.IsSplashFormVisible == true)
                        {
                            splashScreenManagerWait.CloseWaitForm();
                        }

                        if (rdr.Read())
                        {
                            if (splashScreenManagerWait.IsSplashFormVisible == false)
                            {
                                splashScreenManagerWait.ShowWaitForm();
                            }

                            UserType = rdr.GetValue(0).ToString().Trim();
                            UserLoggedSurname = rdr.GetValue(1).ToString().Trim();
                            UserLoggedName = rdr.GetValue(2).ToString().Trim();
                            if ((rdr != null))
                            {
                                rdr.Close();
                            }
                            if (con.State == ConnectionState.Open)
                            {
                                con.Close();
                                con.Dispose();
                            }
                            if (UserType == "Administrator")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 1;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }

                            if (UserType == "Administrator Assistant")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 2;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Accountant")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 3;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Account & Admission")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 4;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Account & HR")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 5;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Account, Admission & HR")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 6;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Admission Officer")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 7;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Human Resources")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 8;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Librarian")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 9;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Librarian & Admission")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 10;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Stock Clerk")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 11;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (UserType == "Teacher")
                            {
                                this.Hide();
                                UserLogged = txtUserName.Text;
                                Role = 12;
                                authenticated = true;
                                ChangePassword = false;
                                loginCanceled = false;
                                txtUserName.Text = "";
                                txtPassword.Text = "";
                                txtUserName.Focus();
                            }
                            if (splashScreenManagerWait.IsSplashFormVisible == true)
                            {
                                splashScreenManagerWait.CloseWaitForm();
                            }

                            //Log login transaction in logs                            
                            string st = UserLoggedSurname + " " + UserLoggedName + " " + LocRM.GetString("strConnected");
                            pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                            this.DialogResult = DialogResult.OK;
                        }
                        else
                        {
                            XtraMessageBox.Show(LocRM.GetString("strLoginErrorWrongUserDetails"), LocRM.GetString("strProgramTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtUserName.Focus();
                           // this.DialogResult = DialogResult.Cancel;
                        }

                    }
                }
                else
                {
                    XtraMessageBox.Show(LocRM.GetString("strLoginErrorDenied"), LocRM.GetString("strProgramTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    txtUserName.Focus();
                  //  this.DialogResult = DialogResult.Cancel;
                }
                cmd.Dispose();
                con.Close();
                con.Dispose();
               
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

        private void linklblForgetPasswordUser_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRecoveryPassword frm = new frmRecoveryPassword();
            frm.ShowDialog();
        }

        private void linkChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword = true;
           // Authenticated = false;
            this.Hide();
            frmChangePassword frm = new frmChangePassword();
            frm.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
        private void showControls()
        {
            panelIntro.Visible = true;
            pictureLogo.Visible = true;
            lblLoginMessage.Visible = true;
            lblUserName.Visible = true;
            txtUserName.Visible = true;
            lblPassword.Visible = true;
            txtPassword.Visible = true;
            btnLogin.Visible = true;
            btnCancel.Visible = true;
            linklblForgetPasswordUser.Visible = true;
            linkChangePassword.Visible = true;
        }
        private void hideControls()
        {
            panelIntro.Visible = false;
            pictureLogo.Visible = false;
            lblLoginMessage.Visible = false;
            lblUserName.Visible = false;
            txtUserName.Visible = false;
            lblPassword.Visible = false;
            txtPassword.Visible = false;
            btnLogin.Visible = false;
            btnCancel.Visible = false;
            linklblForgetPasswordUser.Visible = false;
            linkChangePassword.Visible = false;
        }
        private void frmLogin_Shown(object sender, EventArgs e)
        {
            showControls();
            //this.StartPosition = FormStartPosition.CenterParent;
            txtPassword.Text = "";
            txtUserName.Text = "";

            this.BringToFront();
            this.TopMost = true;
            this.Activate();
            //this.ActiveControl = txtUserName;
            //txtUserName.Focus();

        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            hideControls();
        }
       
    }
}