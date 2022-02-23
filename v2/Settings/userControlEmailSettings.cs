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
using System.Text.RegularExpressions;
using DevExpress.XtraGrid.Views.Grid;
using System.Resources;

namespace EduXpress.Settings
{
    public partial class userControlEmailSettings : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable dtable = new DataTable();
        DataTable dt = new DataTable();
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlEmailSettings).Assembly);

        public userControlEmailSettings()
        {
            InitializeComponent();
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reset();
            fillYesNo();
            fillServerName();
        }

        // fill ServerName
        private void fillServerName()
        {
            cmbServerName.Properties.Items.Clear();
            cmbServerName.Properties.Items.AddRange(new object[] {
            "Yahoo","GMail","Hotmail(Outlook)","Office365","AOL",
            LocRM.GetString("strOther"), });
            cmbServerName.SelectedIndex = -1;
        }
        // fill YesNo
        private void fillYesNo()
        {
            cmbTSRequired.Properties.Items.Clear();
            cmbTSRequired.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strYes"),
            LocRM.GetString("strNoYes"), });
            cmbTSRequired.SelectedIndex = -1;
        }
        void Reset()
        {
            txtSMTPAddress.Text = "";
            cmbServerName.SelectedIndex = -1;
            txtEmailID.Text = "";
            txtPassword.Text = "";
            txtPort.Text = "";
            txtDisplayName.Text = "";
            txtRetypePassword.Text = "";
            cmbTSRequired.SelectedIndex = 0;
            chkIsDefault.Checked = false;
            chkIsEnabled.Checked = true;
            btnSave.Enabled = true;
            btnRemove.Enabled = false;
            btnUpdate.Enabled = false;
            cmbServerName.Focus();
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
        //Fill dataGridViewEmails
        public DataView Getdata()
        {
            string ServerNameColumn = LocRM.GetString("strServerName").ToUpper();
            string SMTPAddressColumn = LocRM.GetString("strSMTPAddress").ToUpper();
            string YourNameColumn = LocRM.GetString("strYourName").ToUpper();
            string EmailColumn = LocRM.GetString("strEmail").ToUpper();
            string PasswordColumn = LocRM.GetString("strPassword").ToUpper();
            string PortColumn = LocRM.GetString("strPort").ToUpper();
            string SSLRequiredColumn = LocRM.GetString("strSSLRequired").ToUpper();
            string IsDefaultColumn = LocRM.GetString("strIsDefault").ToUpper();
            string IsEnabledColumn = LocRM.GetString("strIsEnabled").ToUpper();                
                

            dynamic SelectQry = "SELECT (EmailSettingID) as [ID] , RTRIM(ServerName) as [" + ServerNameColumn + "]," +
                "RTRIM(SMTPAddress) as [" + SMTPAddressColumn + "],RTRIM(SenderName) as [" + YourNameColumn + "]," +
                "RTRIM(Username) as [" + EmailColumn + "],RTRIM(Password) as [" + PasswordColumn + "],RTRIM(Port) as [" + PortColumn + "]," +
                "RTRIM(TLS_SSL_Required) as [" + SSLRequiredColumn + "],RTRIM(IsDefault) as [" + IsDefaultColumn + "]," +
                "RTRIM(IsActive) as [" + IsEnabledColumn + "] FROM EmailSetting order by ServerName";
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

        private void userControlEmailSettings_Load(object sender, EventArgs e)
        {
            dataGridViewEmails.DataSource = Getdata();
        }
        string st1, st2, TSRequired;

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbServerName.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectEmailServerName"), LocRM.GetString("strEmailSetting") , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbServerName.Focus();
                return;
            }

            if (txtSMTPAddress.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterSMTP"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSMTPAddress.Focus();
                return;
            }

            if (txtEmailID.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterSchoolEmail") , LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailID.Focus();
                return;
            }

            if (txtPassword.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strErrorPassword") , LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            if (txtRetypePassword.Text.Trim() != txtPassword.Text.Trim())
            {
                XtraMessageBox.Show( LocRM.GetString("strPasswordNotMatch"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRetypePassword.Focus();
                return;
            }

            if (txtPort.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show( LocRM.GetString("strEnterPortNo"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPort.Focus();
                return;
            }

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strUpdating"));
                }
                if ((chkIsDefault.Checked == true))
                {
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ct = "Update EmailSetting set IsDefault=\'No\'";
                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        cmd.ExecuteReader();
                        con.Close();
                    }
                        
                }

                if (chkIsDefault.Checked == true)
                {
                    st1 = "Yes";
                }
                else
                {
                    st1 = "No";
                }

                if (chkIsEnabled.Checked == true)
                {
                    st2 = "Yes";
                }
                else
                {
                    st2 = "No";
                }

                if (cmbTSRequired.Text == LocRM.GetString("strYes"))
                {
                    TSRequired = "Yes";
                }
                else
                {
                    TSRequired = "No";
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cb = ("Update EmailSetting set ServerName=@d1, SMTPAddress=@d2, SenderName=@d3, Username=@d4, Password=@d5, Port=@d6, TLS_SSL_Required=@d7, IsDefault=@d8, IsActive=@d9 where ServerName=@d1");
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbServerName.Text);
                    cmd.Parameters.AddWithValue("@d2", txtSMTPAddress.Text);
                    cmd.Parameters.AddWithValue("@d3", txtDisplayName.Text);
                    cmd.Parameters.AddWithValue("@d4", txtEmailID.Text);
                    cmd.Parameters.AddWithValue("@d5", pf.Encrypt(txtPassword.Text));
                    cmd.Parameters.AddWithValue("@d6", txtPort.Text);
                    cmd.Parameters.AddWithValue("@d7", TSRequired);
                    cmd.Parameters.AddWithValue("@d8", st1);
                    cmd.Parameters.AddWithValue("@d9", st2);
                    cmd.ExecuteReader();
                    con.Close();
                }
                    
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show( LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strEmailSetting") , MessageBoxButtons.OK, MessageBoxIcon.Information);
               // barStaticItemProcess.Caption = "Email Server settings updated successfully";
                btnUpdate.Enabled = false;
                dataGridViewEmails.DataSource = Getdata();
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

        private void btnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if ((XtraMessageBox.Show( LocRM.GetString("strDeleteConfirmQuestion"),  LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes))
                {
                    DeleteRecord();
                }

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
                    string cq = "delete from EmailSetting where ServerName=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbServerName.Text);
                    RowsAffected = cmd.ExecuteNonQuery();
                    if ((RowsAffected > 0))
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //barStaticItemProcess.Caption = "Email Server record removed successfully";
                        dataGridViewEmails.DataSource = Getdata();
                        Reset();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                    }

                    if ((con.State == ConnectionState.Open))
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

        private void cmbServerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cmbServerName.SelectedIndex == 0))
            {
                txtSMTPAddress.Text = "smtp.mail.yahoo.com";
                txtPort.Text = "587";
            }

            if ((cmbServerName.SelectedIndex == 1))
            {
                txtSMTPAddress.Text = "smtp.gmail.com";
                txtPort.Text = "587";
            }

            if ((cmbServerName.SelectedIndex == 2))
            {
                txtSMTPAddress.Text = "smtp.live.com";
                txtPort.Text = "587";
            }

            if ((cmbServerName.SelectedIndex == 3))
            {
                txtSMTPAddress.Text = "smtp.office365.com";
                txtPort.Text = "587";
            }

            if ((cmbServerName.SelectedIndex == 4))
            {
                txtSMTPAddress.Text = "smtp.aol.com";
                txtPort.Text = "587";
            }
            if ((cmbServerName.SelectedIndex == 5))
            {
                txtSMTPAddress.Text = "";
                txtPort.Text = "";
            }
        }

        private void txtEmailID_Validating(object sender, CancelEventArgs e)
        {
            string pattern = "^[a-z][a-z|0-9|]*([_][a-z|0-9]+)*([.][a-z|0-9]+([_][a-z|0-9]+)*)?@[a-z][a-z|0-9|]*\\.([a-z][a-z|0-9]*(" +
"\\.[a-z][a-z|0-9]*)?)$";
            System.Text.RegularExpressions.Match match = Regex.Match(txtEmailID.Text.Trim(), pattern, RegexOptions.IgnoreCase);
            
            if (match.Success)
            {

            }
            else
            {
                XtraMessageBox.Show( LocRM.GetString("strInvalidEmailAddress"), LocRM.GetString("strEmailSetting"));
                txtEmailID.Text = "";
            }
        }

        private void txtEmailID_KeyPress(object sender, KeyPressEventArgs e)
        {
            string ac = "@";
            if ((e.KeyChar != ((char)(Keys.Back))))
            {
                if (((Convert.ToInt16(e.KeyChar) < 97)
                            || (Convert.ToInt16(e.KeyChar) > 122)))
                {
                    if (((Convert.ToInt16(e.KeyChar) != 46)
                                && (Convert.ToInt16(e.KeyChar) != 95)))
                    {
                        if (((Convert.ToInt16(e.KeyChar) < 48)
                                    || (Convert.ToInt16(e.KeyChar) > 57)))
                        {
                            if ((ac.IndexOf(e.KeyChar) == -1))
                            {
                                e.Handled = true;
                            }
                            else if ((txtEmailID.Text.Contains("@")
                                        && (e.KeyChar == '@')))
                            {
                                e.Handled = true;
                            }

                        }

                    }

                }

            }
        }

        private void txtPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((((e.KeyChar < '0')
                        || (e.KeyChar > '9'))
                        && (e.KeyChar != '8')))
            {
                e.Handled = true;
            }
        }

        //display "*" in password fields
        List<int> list = new List<int>();
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            //GridView view = sender as GridView;
            //if ((e.Column.FieldName != LocRM.GetString("strPassword").ToUpper())) return;
            //int dataSourceRowIndex = view.GetDataSourceRowIndex(4);
            //if (list.Contains(dataSourceRowIndex)) return;
            //e.DisplayText = "*****";
            //Replace password with ****
            GridView view = sender as GridView;
            if ((e.Column.FieldName == LocRM.GetString("strPassword").ToUpper()))
            {
                int dataSourceRowIndex = view.GetDataSourceRowIndex(4);
                if (list.Contains(dataSourceRowIndex)) return;
                e.DisplayText = "*****";
            }
            
            //Localize the Yes and No
            if ((e.Column.FieldName == LocRM.GetString("strSSLRequired").ToUpper()))
            {
               // int dataSourceRowIndex = view.GetDataSourceRowIndex(6);
               // if (list.Contains(dataSourceRowIndex)) return;
                if (e.DisplayText == "Yes") // LocRM.GetString("strSSLRequired").ToUpper()) e.DisplayText==)
                {
                    e.DisplayText = LocRM.GetString("strYes");
                }
                if (e.DisplayText == "No")
                {
                    e.DisplayText = LocRM.GetString("strNoYes");
                }
            }

            if ((e.Column.FieldName == LocRM.GetString("strIsDefault").ToUpper()))
            {
               // int dataSourceRowIndex = view.GetDataSourceRowIndex(7);
              //  if (list.Contains(dataSourceRowIndex)) return;
                if (e.DisplayText == "Yes") // LocRM.GetString("strSSLRequired").ToUpper()) e.DisplayText==)
                {
                    e.DisplayText = LocRM.GetString("strYes");
                }
                if (e.DisplayText == "No")
                {
                    e.DisplayText = LocRM.GetString("strNoYes");
                }
            }

            if ((e.Column.FieldName == LocRM.GetString("strIsEnabled").ToUpper()))
            {
               // int dataSourceRowIndex = view.GetDataSourceRowIndex(8);
              //  if (list.Contains(dataSourceRowIndex)) return;
                if (e.DisplayText == "Yes") // LocRM.GetString("strSSLRequired").ToUpper()) e.DisplayText==)
                {
                    e.DisplayText = LocRM.GetString("strYes");
                }
                if (e.DisplayText == "No")
                {
                    e.DisplayText = LocRM.GetString("strNoYes");
                }
            }

        }

        private void dataGridViewEmails_MouseClick(object sender, MouseEventArgs e)
        {
            btnUpdate.Enabled = true;
            btnRemove.Enabled = true;
            btnSave.Enabled = false;
            fillYesNo();
            fillServerName(); 

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                if (gridView1.DataRowCount > 0)
                {
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        cmd = con.CreateCommand();

                        cmd.CommandText = "SELECT * FROM EmailSetting WHERE ServerName = '" + gridView1.GetFocusedRowCellValue(LocRM.GetString("strServerName").ToUpper()).ToString() + "'";
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            txtID.Text = Convert.ToString(rdr.GetValue(0));
                            cmbServerName.Text = (rdr.GetString(1).Trim());
                            txtSMTPAddress.Text = (rdr.GetString(2).Trim());
                            txtDisplayName.Text = (rdr.GetString(3).Trim());
                            txtEmailID.Text = (rdr.GetString(4).Trim());
                            txtPassword.Text =pf.Decrypt (rdr.GetString(5).Trim());
                            txtPort.Text = Convert.ToString(rdr.GetValue(6));
                            //cmbTSRequired.Text = (rdr.GetString(7).Trim());
                            string TSRequired = (rdr.GetString(7).Trim());
                            if (TSRequired == "Yes")
                            {
                                cmbTSRequired.Text = LocRM.GetString("strYes");
                            }
                            else
                            {
                                cmbTSRequired.Text = LocRM.GetString("strNoYes");
                            }
                            if (rdr.GetString(8).Trim() == "Yes")
                            {
                                chkIsDefault.Checked = true;
                            }
                            else
                            {
                                chkIsDefault.Checked = false;
                            }

                            if (rdr.GetString(9).Trim() == "Yes")
                            {
                                chkIsEnabled.Checked = true;
                            }
                            else
                            {
                                chkIsEnabled.Checked = false;
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

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbServerName.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectEmailServerName"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbServerName.Focus();
                return;
            }

            if (txtSMTPAddress.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSMTP"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSMTPAddress.Focus();
                return;
            }

            if (txtEmailID.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSchoolEmail"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmailID.Focus();
                return;
            }

            if (txtPassword.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strErrorPassword"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }
            if (txtRetypePassword.Text.Trim() != txtPassword.Text.Trim())
            {
                XtraMessageBox.Show(LocRM.GetString("strPasswordNotMatch"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRetypePassword.Focus();
                return;
            }

            if (txtPort.Text.Trim().Length == 0)
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterPortNo"), LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPort.Focus();
                return;
            }

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving"));
                }
                if (chkIsDefault.Checked == true)
                {
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ct = "select IsDefault from EmailSetting where IsDefault=\'Yes\'";
                        cmd = new SqlCommand(ct);
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            XtraMessageBox.Show(LocRM.GetString("strOtherEmailDefault"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            if ((rdr != null))
                            {
                                rdr.Close();
                            }
                            return;
                        }
                    }
                        

                }

                if (chkIsDefault.Checked == true)  
                {
                    st1 = "Yes";
                }
                else
                {
                    st1 = "No";
                }

                if (chkIsEnabled.Checked == true)
                {
                    st2 = "Yes";
                }
                else
                {
                    st2 = "No";
                }
                if (cmbTSRequired.Text== LocRM.GetString("strYes"))
                {
                    TSRequired = "Yes";
                }
                else
                {
                    TSRequired = "No";
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cb = "insert into EmailSetting(ServerName, SMTPAddress,SenderName, Username, Password, Port, TLS_SSL_Required, IsDefault,IsActive) VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbServerName.Text);
                    cmd.Parameters.AddWithValue("@d2", txtSMTPAddress.Text);
                    cmd.Parameters.AddWithValue("@d3", txtDisplayName.Text);
                    cmd.Parameters.AddWithValue("@d4", txtEmailID.Text);
                    cmd.Parameters.AddWithValue("@d5", pf.Encrypt(txtPassword.Text));
                    cmd.Parameters.AddWithValue("@d6", txtPort.Text);
                    cmd.Parameters.AddWithValue("@d7", TSRequired);
                    cmd.Parameters.AddWithValue("@d8", st1);
                    cmd.Parameters.AddWithValue("@d9", st2);

                    cmd.ExecuteReader();
                    con.Close();
                }
                    
                // barStaticItemProcess.Caption = "New Email Server settings saved successfully";
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show( LocRM.GetString("strSuccessfullySaved"),  LocRM.GetString("strEmailSetting"), MessageBoxButtons.OK, MessageBoxIcon.Information);                
                btnSave.Enabled = false;
                dataGridViewEmails.DataSource = Getdata();
                
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
