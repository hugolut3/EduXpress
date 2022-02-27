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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using System.Data.SqlClient;
using System.Resources;
using System.IO;
using System.Diagnostics;

namespace EduXpress.Students
{
    public partial class frmDownloadFormCloud : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmDownloadFormCloud).Assembly);
        public frmDownloadFormCloud()
        {
            InitializeComponent();
        }

        private void frmDownloadFormCloud_Load(object sender, EventArgs e)
        {
            loadForms();
        }
        private string ServiceAccountAuthFile = "";
        //private string ServiceAccountEmail = "";
        private string DirectoryId = "";
        
        private void loadForms()
        {
            //download Google.Apis.Drive.v3 will also install Google.Apis, Google.Apis.Auth, Google.Apis.Core and possibly Newtonsoft.Json

            //load settings
            LoadeGoogleDriveSettings();

            //Load the service account credentials and define the scope of its access
            try
            {
                //Cursor = Cursors.WaitCursor;
                //timer1.Enabled = true;
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                //var credetial = GoogleCredential.FromFile(PathServiceAccountFile).CreateScoped(
                //    new[] { DriveService.ScopeConstants.Drive });  //Load from local file 

                var credetial = GoogleCredential.FromJson(ServiceAccountAuthFile).CreateScoped(
                    new[] { DriveService.ScopeConstants.Drive });  //Drive gives full access, we could limit it to read only but because it's a service account we control, dosn't harm to give full access

                //Create the drive service object to make all calls to google drive api
                //var service = new DriveService(new BaseClientService.Initializer()
                service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credetial
                });

                //Search for Google sheet files in the directory in my account           
                var request = service.Files.List();
                request.Q = "parents in '" + DirectoryId + "' and mimeType = 'application/vnd.google-apps.spreadsheet'"; //text file: mineType = 'text/Plain', image: image/jpg
                                                                                                                         //var response = await request.ExecuteAsync();
                var response = request.Execute();

                cmbFormName.Properties.Items.Clear();
                cmbFormID.Properties.Items.Clear();
                foreach (var driveFile in response.Files)
                {
                    if (response.Files.Count > 0)
                    {
                        //File ID, File name and File mimetype: $"{driveFile.Id} {driveFile.Name} {driveFile.MimeType}";
                        cmbFormName.Properties.Items.Add(driveFile.Name);
                        cmbFormID.Properties.Items.Add(driveFile.Id);
                    }
                    else
                    {
                        MessageBox.Show(LocRM.GetString("strNoFormFoundDirectoryCloud"));
                    }
                }
                cmbFormName.SelectedIndex = -1;
                cmbFormID.SelectedIndex = -1;
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                //Cursor = Cursors.Default;
                //timer1.Enabled = false;
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                //Cursor = Cursors.Default;
                //timer1.Enabled = false;
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Populate Cloud Settings 
        private void LoadeGoogleDriveSettings()
        {
            try
            {
                //Cursor = Cursors.WaitCursor;
                //timer1.Enabled = true;
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "SELECT FormDirectoryId,AuthenticationFile FROM CloudSettings ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbFormName.Text);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {                        
                        DirectoryId = rdr[0].ToString();
                        ServiceAccountAuthFile = rdr[1].ToString();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strCloudSettingsNotFound"), LocRM.GetString("strCloudSettings"), MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                //Cursor = Cursors.Default;
                //timer1.Enabled = false;
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                //Cursor = Cursors.Default;
                //timer1.Enabled = false;
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbFormName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFormID.SelectedIndex = cmbFormName.SelectedIndex;
        }
        DriveService service;
        string fileName = "";
        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (cmbFormName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectCloudFormDownload"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbFormName.Focus();
                return;
            }

            try
            {
                //var downloadFile = response.Files.FirstOrDefault();
                var getRequest = service.Files.Export(cmbFormID.Text.Trim(), "text/csv");
                using (var fileStream = new FileStream(cmbFormName.Text.Trim() + ".csv", FileMode.Create, FileAccess.Write))
                {
                    getRequest.Download(fileStream);
                    fileName = cmbFormName.Text + ".csv";

                    if (MessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strDownloadFormsCloudStorage"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                    fileName = "";
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
        
        private static void DownloadFile(Google.Apis.Drive.v3.DriveService service, Google.Apis.Drive.v3.Data.File file, string saveTo)
        {

            var request = service.Files.Export(file.Id, "text/csv"); //To pdf: "application/pdf"
            var stream = new MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged += (Google.Apis.Download.IDownloadProgress progress) =>
            {
                switch (progress.Status)
                {
                    case Google.Apis.Download.DownloadStatus.Downloading:
                        {
                            Console.WriteLine(progress.BytesDownloaded);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Completed:
                        {
                            Console.WriteLine("Download complete.");
                            SaveStream(stream, saveTo);
                            break;
                        }
                    case Google.Apis.Download.DownloadStatus.Failed:
                        {
                            Console.WriteLine("Download failed.");
                            break;
                        }
                }
            };
            request.Download(stream);
            
        }
        private static void SaveStream(System.IO.MemoryStream stream, string saveTo)
        {
            using (System.IO.FileStream file = new System.IO.FileStream(saveTo + ".csv", System.IO.FileMode.Create, System.IO.FileAccess.Write))
            {
                stream.WriteTo(file);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
    }
}