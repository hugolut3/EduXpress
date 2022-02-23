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
using System.Resources;
using System.Data.SqlClient;


namespace EduXpress.Admin
{
    public partial class frmBackupRestore : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        

        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmBackupRestore).Assembly);
        public frmBackupRestore()
        {
            InitializeComponent();
        }

        private void btnBrowseBackupFolder_Click(object sender, EventArgs e)
        {
            // XtraOpenFileDialog OpenFolder = new XtraOpenFileDialog();
            XtraFolderBrowserDialog OpenFolder = new XtraFolderBrowserDialog();

            try
            {                
                DialogResult res = OpenFolder.ShowDialog();
                if (res == DialogResult.OK)
                {
                    txtBackupDirectory.Text = OpenFolder.SelectedPath.Trim();
                }
                Properties.Settings.Default.BackupRestoreFolder = OpenFolder.SelectedPath.Trim();
                btnBackup.Enabled = true;                
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        string Filename;
        private void btnBackup_Click(object sender, EventArgs e)
        {
            if (txtBackupDirectory.Text == "")
            {
               XtraMessageBox.Show(LocRM.GetString("strSelectBackupFolder"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
               btnBrowseBackupFolder.Focus();
                return;
            }

            string fullFilename;
            string databaseBackupFileName = "EduXpressDB_backup_" + DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss") + ".bak";            
                
                fullFilename = txtBackupDirectory.Text.Trim()+"\\"+ databaseBackupFileName;
                Filename = txtBackupDirectory.Text.Trim();               

            if (splashScreenManager1.IsSplashFormVisible == false)
            {
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strBackingUpDatabase"));
            }

            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    //Create a full SQL Server backup to disk
                    string cb = "backup database EduXpressDB to disk='" + fullFilename + "'with init,stats=10";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.ExecuteReader();                   

                    //close the connection
                    con.Close();
                }  
                
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(LocRM.GetString("strBackupSavedTo")+": "+ Filename, LocRM.GetString("strDatabaseBackupRestore"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strDatabaseBackupSaved");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //verify database backup
                #region VerifyDatabase
                //Verify database
                //bool verifyStatus = false;
                //using (con = new SqlConnection(databaseConnectionString))
                //{
                //    con.Open();                   

                //    //Verify database
                //    string cb1 = "restore verifyonly from disk='" + fullFilename + "'";
                //    cmd = new SqlCommand(cb1);
                //    cmd.Connection = con;
                //    cmd.ExecuteReader();

                //    MessageBox.Show(cmd.CommandType.ToString());
                //  //  MessageBox.Show(cmd.);
                //    //close the connection
                //    con.Close();
                //} 
                #endregion

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
       

        private void btnRestore_Click(object sender, EventArgs e)
        {
            XtraOpenFileDialog OpenFileFolder = new XtraOpenFileDialog();
            if (txtBackupDirectory.Text.Trim() == "")
            {
                OpenFileFolder.InitialDirectory = "c:\\";
            }
            else
            {
                OpenFileFolder.InitialDirectory = Properties.Settings.Default.BackupRestoreFolder;
            }

            OpenFileFolder.Filter = "bak files (*.bak)|*.bak|All files (*.*)|*.*";
            OpenFileFolder.FilterIndex = 1;
            OpenFileFolder.RestoreDirectory = true;
            OpenFileFolder.Title = LocRM.GetString("strRestoreDatabase");

            //Clear the file name
            OpenFileFolder.FileName = "";            

            if (OpenFileFolder.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strRestoringDatabase"));
                    }

                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;

                    SqlConnection.ClearAllPools();
                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string cb = "USE Master ALTER DATABASE EduXpressDB SET Single_User WITH Rollback Immediate Restore database EduXpressDB FROM disk='" + OpenFileFolder.FileName + "' WITH REPLACE ALTER DATABASE EduXpressDB SET Multi_User ";
                        cmd = new SqlCommand(cb);
                        cmd.Connection = con;
                        cmd.ExecuteReader();
                        con.Close();
                    }

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }

                    XtraMessageBox.Show(LocRM.GetString("strSuccessfullyPerformedDatabaseRestore"), LocRM.GetString("strDatabaseBackupRestore"), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //Log record transaction in logs
                    string st = LocRM.GetString("strSuccessfullyPerformedDatabaseRestore");
                    pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                }
                catch (Exception ex)
                {
                    Cursor = Cursors.Default;
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void frmBackupRestore_Load(object sender, EventArgs e)
        {
            //load backup Directory:               

            if (Properties.Settings.Default.BackupRestoreFolder == null)
            {
                txtBackupDirectory.Text = "";
                btnBackup.Enabled = false;
            }
            else
            {
                txtBackupDirectory.Text = Properties.Settings.Default.BackupRestoreFolder;
                btnBackup.Enabled = true;
            }
        }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmBackupRestore_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}