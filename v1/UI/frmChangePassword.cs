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


namespace EduXpress.UI
{
    public partial class frmChangePassword : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmChangePassword).Assembly);

        public frmChangePassword()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
          //  frmLogin frm = new frmLogin();
          //  frm.StartPosition = FormStartPosition.CenterParent;
          //  frm.ShowDialog();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text="";
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmNewPassword.Text = "";
            txtUserName.Focus();
            btnClear.Enabled = false;
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {
            btnClear.Enabled = true;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtUserName;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                int RowsAffected = 0;
                if (txtUserName.Text == "")
                {
                    XtraMessageBox.Show(LocRM.GetString("strErrorUsername"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Focus();
                    return;
                }
                if (txtOldPassword.Text == "") 
                {
                    XtraMessageBox.Show(LocRM.GetString("strEnterOldPassword"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtOldPassword.Focus();
                    return;
                }
                if (txtNewPassword.Text == "") 
                {
                    XtraMessageBox.Show(LocRM.GetString("strEnterNewPassword"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewPassword.Focus();
                    return;
                }
                if (txtConfirmNewPassword.Text == "") 
                {
                    XtraMessageBox.Show(LocRM.GetString("strEnterConfirmNewPassword"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtConfirmNewPassword.Focus();
                    return;
                }
                // if (txtNewPassword.Properties.TextLength < 5))
                //  {
                //     MessageBox.Show("The Password Should be of at least 6 Characters", LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    txtNewPassword.Text = "";
                //     txtConfirmPassword.Text = "";
                //   txtNewPassword.Focus();
                //    return;
                //  }
                else if (txtNewPassword.Text != txtConfirmNewPassword.Text) 
                {
                    XtraMessageBox.Show(LocRM.GetString("strPasswordNotMatch"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewPassword.Text = "";
                    txtOldPassword.Text = "";
                    txtConfirmNewPassword.Text = "";
                    txtOldPassword.Focus();
                    return;
                }
                else if ((txtOldPassword.Text == txtNewPassword.Text)) 
                {
                    XtraMessageBox.Show(LocRM.GetString("strPasswordSame"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNewPassword.Text = "";
                    txtConfirmNewPassword.Text = "";
                    txtNewPassword.Focus();
                    return;
                }

                con = new SqlConnection(databaseConnectionString);
                con.Open();
                string co = "Update Registration set UserPassword = '" + pf.Encrypt(txtNewPassword.Text.Trim()) + "'where UserName='" + txtUserName.Text.Trim() + "' and UserPassword = '" + pf.Encrypt(txtOldPassword.Text.Trim()) + "'";

                cmd = new SqlCommand(co);
                cmd.Connection = con;
                RowsAffected = cmd.ExecuteNonQuery();
                if ((RowsAffected > 0)) 
                {
                    XtraMessageBox.Show(LocRM.GetString("strPasswordChanged"), LocRM.GetString("strUserInfo"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    txtUserName.Text = "";
                    txtNewPassword.Text = "";
                    txtOldPassword.Text = "";
                    txtConfirmNewPassword.Text = "";

                }
                else 
                {
                    XtraMessageBox.Show(LocRM.GetString("strUsernamePasswordInvalid"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUserName.Text = "";
                    txtNewPassword.Text = "";
                    txtOldPassword.Text = "";
                    txtConfirmNewPassword.Text = "";
                    txtUserName.Focus();
                }
                if ((con.State == ConnectionState.Open))
                {
                    con.Close();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}