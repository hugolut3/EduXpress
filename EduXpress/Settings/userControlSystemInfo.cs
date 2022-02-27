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
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Data.SqlClient;
using DevExpress.XtraPrinting;
using System.Resources;

namespace EduXpress.Settings
{
    public partial class userControlSystemInfo : DevExpress.XtraEditors.XtraUserControl
    {
        DataTable dtable = new DataTable();
        SqlConnection con = null;
        SqlCommand cmd = null;
        DataTable dt = new DataTable();
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlSystemInfo).Assembly);


        public userControlSystemInfo()
        {
            InitializeComponent();
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void btnRenewIPaddress_Click(object sender, EventArgs e)  
        {
            DialogResult dialogResult = XtraMessageBox.Show(LocRM.GetString("strInternetWillBeLost"), LocRM.GetString("strSystemInfo"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            try
            {                
                var proc1 = new ProcessStartInfo();
                string anyCommand;
                anyCommand = "ipconfig /release & ipconfig /renew";
                proc1.UseShellExecute = true;

                proc1.WorkingDirectory = @"C:\Windows\System32";

                proc1.FileName = @"C:\Windows\System32\cmd.exe";
                proc1.Verb = "runas";
                proc1.Arguments = "/c " + anyCommand;
                // proc1.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(proc1);
                //barStatusLabel.Caption = "";
                //barStatusLabel.Caption = "Done! IP Address renewed!";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"));
                //barStatusLabel.Caption = "";
               // barStatusLabel.Caption = "Failed to renew the IP Address due to an error: " + ex.Message;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            txtComputerName.Text = "";
            txtIPaddres.Text = "";
            txtMACaddres.Text = "";
            macAddresses = "";
            Network();
         //   barStatusLabel.Caption = "";
         //   barStatusLabel.Caption = "Refreshed!";
        }
        string macAddresses = "";
        private void Network()
        {
            try
            {
                //Computer name
                String strHostName = string.Empty; //getting the host name of the machine.
                strHostName = Dns.GetHostName();
                txtComputerName.Text = strHostName;

                //IP Address
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName); // getting the ip address of the host name.
                IPAddress[] addr = ipEntry.AddressList;   //fill it into array.

                for (int i = 0; i < addr.Length; i++)
                {
                    txtIPaddres.Text = addr[i].ToString();  //print it into textbox.
                }

                //MAC Address
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        macAddresses += nic.GetPhysicalAddress().ToString();
                        break;
                    }
                }

                this.txtMACaddres.Text = macAddresses;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"));
                System.Environment.Exit(0);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
        // Display information about the selected drive.
        private void cmbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (splashScreenManager1.IsSplashFormVisible == false)
            {
                splashScreenManager1.ShowWaitForm();
            }

            string drive_letter = cmbDrives.Text.Substring(0, 1);
            DriveInfo di = new DriveInfo(drive_letter);
            txtIsReadys.Text = di.IsReady.ToString();
            txtTypes.Text = di.DriveType.ToString();
            txtDriveNames.Text = di.Name;
            txtRoorDirectorys.Text = di.RootDirectory.Name;
            if (di.IsReady)
            {
                txtFormats.Text = di.DriveFormat;
                txtAvailableFreeSpaces.Text = di.AvailableFreeSpace.ToString() + " " + "bytes" + "   " + FormatBytes(di.AvailableFreeSpace).ToString();
                txtDriveTotalFreeSizes.Text = di.TotalFreeSpace.ToString() + " " + "bytes" + "   " + FormatBytes(di.TotalFreeSpace).ToString();
                txtDriveTotalSizes.Text = di.TotalSize.ToString() + " " + "bytes" + "   " + FormatBytes(di.TotalSize).ToString();
                txtVolumeLabels.Text = di.VolumeLabel;
            }
            else
            {
                txtFormats.Text = "";
                txtAvailableFreeSpaces.Text = "";
                txtDriveTotalFreeSizes.Text = "";
                txtDriveTotalSizes.Text = "";
                txtVolumeLabels.Text = "";
            }
            if (splashScreenManager1.IsSplashFormVisible == true)
            {
                splashScreenManager1.CloseWaitForm();
            }
            // barStatusLabel.Caption = "";
            // barStatusLabel.Caption = cmbDrives.Text + " Drive selected";
        }

        private void userControlSystemInfo_Load(object sender, EventArgs e)
        {
            tabPane1.SelectedPageIndex = 0;
            try
            {
                //  System.Management.ManagementObject i;
                System.Management.ManagementObjectSearcher searchInfo_Processor = new System.Management.ManagementObjectSearcher("Select * from Win32_Processor");
                foreach (System.Management.ManagementObject i in searchInfo_Processor.Get())
                {
                    txtProcessorNam.Text = i["Name"].ToString();
                    txtProcessorIDs.Text = i["ProcessorID"].ToString();
                    txtProcessorDescriptions.Text = i["Description"].ToString();
                    txtProcessorManufacturers.Text = i["Manufacturer"].ToString();
                    txtProcessorL2CacheSizes.Text = i["L2CacheSize"].ToString();
                    txtProcessorClockSpeeds.Text = i["CurrentClockSpeed"].ToString() + " Mhz";
                    txtProcessorDataWidths.Text = i["DataWidth"].ToString();
                    txtProcessorExtClocks.Text = i["ExtClock"].ToString() + " Mhz";
                    txtProcessorFamilies.Text = i["Family"].ToString();
                }
                System.Management.ManagementObjectSearcher searchInfo_Board = new System.Management.ManagementObjectSearcher("Select * from Win32_BaseBoard");
                foreach (System.Management.ManagementObject i in searchInfo_Board.Get())
                {
                    txtBoardDescriptions.Text = i["Description"].ToString();
                    txtBoardManufacturerName.Text = i["Manufacturer"].ToString();
                    txtMotherboardName.Text = i["Name"].ToString();
                    txtBoardSerialNumbers.Text = i["SerialNumber"].ToString();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"));
                System.Environment.Exit(0);
            }
        }
        //Get database size
        int sum = 0;
        public void GetDbSize()
        {
            if (splashScreenManager1.IsSplashFormVisible == false)
            {
                splashScreenManager1.ShowWaitForm();
            }
            //Suspend layout: Basically it's if you want to adjust multiple layout-related properties - 
            //or add multiple children - but avoid the layout system repeatedly reacting to your changes. 
            //You want it to only perform the layout at the very end, when everything's "ready".
            //this.be
            this.SuspendLayout();
            sum = 0;
            // Database Connection String
            // string sConnectionString = "Server = .; Integrated Security = true; database = HKS";

            // SQL Command [Same command discussed in section-B of this article]
            string sSqlquery = "EXEC sp_MSforeachtable @command1=\"EXEC sp_spaceused '?'\" ";

            DataSet oDataSet = new DataSet();

            // Executing SQL Command using ADO.Net
            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(sSqlquery, con))
                {
                    cmd.ExecuteNonQuery();
                    SqlDataAdapter executeAdapter = new SqlDataAdapter(cmd);
                    executeAdapter.Fill(oDataSet);
                }

                con.Close();
            }

            // Iterating each table
            for (int i = 0; i < oDataSet.Tables.Count; i++)
            {
                // We want to add only "data" column value of each table
                sum = sum + Convert.ToInt32(oDataSet.Tables[i].Rows[0]["data"].ToString().Replace("KB", "").Trim());

            }
            sum = sum * 1024; //convert

            // txtDatabaseSize.Text = sum.ToString();
            txtDatabaseSizes.Text = FormatBytes(sum).ToString();

            //Resume layout
            this.ResumeLayout();
            if (splashScreenManager1.IsSplashFormVisible == true)
            {
                splashScreenManager1.CloseWaitForm();
            }
        }
        //Convert to B, KB, MB, GB, TB
        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }


        private void loadDiskInfo()
        {
            if (splashScreenManager1.IsSplashFormVisible == false)
            {
                splashScreenManager1.ShowWaitForm();
            }
            // Make a list of drives.
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                cmbDrives.Properties.Items.Add(di.Name);
                cmbDrives.SelectedIndex = 0;
            }
            if (splashScreenManager1.IsSplashFormVisible == true)
            {
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void btnSaveFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                FileStream fs = new FileStream("temp.txt", FileMode.Create, FileAccess.Write);
                StreamWriter w = new StreamWriter(fs);
                w.Write("****** Processor Information ******");
                w.WriteLine();
                w.WriteLine();
                w.WriteLine("Name");
                w.WriteLine(txtProcessorNam.Text);
                w.WriteLine();
                w.WriteLine("ID");
                w.WriteLine(txtProcessorIDs.Text);
                w.WriteLine();
                w.WriteLine("Description");
                w.WriteLine(txtProcessorDescriptions.Text);
                w.WriteLine();
                w.WriteLine("Manufacturer");
                w.WriteLine(txtProcessorManufacturers.Text);
                w.WriteLine();
                w.WriteLine("L2 Cache Size");
                w.WriteLine(txtProcessorL2CacheSizes.Text);
                w.WriteLine();
                w.WriteLine("Clock Speed");
                w.WriteLine(txtProcessorClockSpeeds.Text);
                w.WriteLine();
                w.WriteLine("Data Width");
                w.WriteLine(txtProcessorDataWidths.Text);
                w.WriteLine();
                w.WriteLine("Ext Clock");
                w.WriteLine(txtProcessorExtClocks.Text);
                w.WriteLine();
                w.WriteLine("Family");
                w.WriteLine(txtProcessorFamilies.Text);
                w.WriteLine();
                w.WriteLine("****** MotherBoard Information *****");
                w.WriteLine();
                w.WriteLine("Name");
                w.WriteLine(txtBoardDescriptions.Text);
                w.WriteLine();
                w.WriteLine("Manufacturer");
                w.WriteLine(txtBoardManufacturerName.Text);
                w.WriteLine();
                w.WriteLine("Description");
                w.WriteLine(txtBoardDescriptions.Text);
                w.WriteLine();
                w.WriteLine("Serial Number");
                w.WriteLine(txtBoardSerialNumbers.Text);
                w.WriteLine();
                w.Write("****** Network ******");
                w.WriteLine();
                w.WriteLine();
                w.WriteLine("Computer Name");
                w.WriteLine(txtComputerName.Text);
                w.WriteLine();
                w.WriteLine("IP Address");
                w.WriteLine(txtIPaddres.Text);
                w.WriteLine();
                w.WriteLine("MAC Address");
                w.WriteLine(txtMACaddres.Text);
                w.WriteLine();
                w.Write("****** Hard Drive ******");
                w.WriteLine();
                w.WriteLine();
                w.WriteLine("Drive selected");
                w.WriteLine(cmbDrives.Text);
                w.WriteLine();
                w.WriteLine("Name");
                w.WriteLine(txtDriveNames.Text);
                w.WriteLine();
                w.WriteLine("Total size");
                w.WriteLine(txtDriveTotalSizes.Text);
                w.WriteLine();
                w.WriteLine("Total free size");
                w.WriteLine(txtDriveTotalFreeSizes.Text);
                w.WriteLine();
                w.WriteLine("Available free size");
                w.WriteLine(txtAvailableFreeSpaces.Text);
                w.WriteLine();
                w.WriteLine("Volume Label");
                w.WriteLine(txtVolumeLabels.Text);
                w.WriteLine();
                w.WriteLine("Disk usage by database tables");
                w.WriteLine(txtDatabaseSizes.Text);
                w.Flush();
                w.Close();
                {
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.DefaultExt = "txt";
                    // saveFileDialog1.InitialDirectory =  My.Computer.FileSystem.SpecialDirectories.MyDocuments;
                    saveFileDialog.InitialDirectory = System.Environment.SpecialFolder.MyDocuments.ToString();
                    saveFileDialog.FileName = "SystemInfo";
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files|*.*";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.Title = "SystemInfo - Save file";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //  My.Computer.FileSystem.MoveFile("temp.txt", saveFileDialog1.FileName, true);
                        File.Move("temp.txt", saveFileDialog.FileName);
                      //  barStatusLabel.Caption = "";
                       // barStatusLabel.Caption = "Done! File saved to file";
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"));
              //  barStatusLabel.Caption = "";
              //  barStatusLabel.Caption = "Failed to save to file du to an error: " + ex.Message;
            }
        }

        private void tabPane1_SelectedPageIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            if (tabPane1.SelectedPage == tabNavigationNetwork)
            {
                txtComputerName.Text = "";
                txtIPaddres.Text = "";
                txtMACaddres.Text = "";
                macAddresses = "";
                Network();
            }
            
            if (tabPane1.SelectedPage == tabNavigationHardDrive)
            {
               // splashScreenManager1.ShowWaitForm();
               // lblDisplay.Text = "";
                GetDbSize();
                loadDiskInfo();
              //  splashScreenManager1.CloseWaitForm();
            }
          //  barStatusLabel.Caption = "";
          //  barStatusLabel.Caption = "Ready";
        }
        //export to pdf
        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                richEditControl1.CreateNewDocument();
                richEditControl1.Text = "****** " + LocRM.GetString("strProcessorInfo").ToUpper() + " ******" + Environment.NewLine + Environment.NewLine;
                // 
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strSystemName") + Environment.NewLine; 
                richEditControl1.Text = richEditControl1.Text + txtProcessorNam.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strProcessorID") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtProcessorIDs.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strDescription") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtProcessorDescriptions.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strManufacturer") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtProcessorManufacturers.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strL2CacheSize") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtProcessorL2CacheSizes.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strClocSpeed") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtProcessorClockSpeeds.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strDataWidth") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtProcessorDataWidths.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strExtClock") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtProcessorExtClocks.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strFamily") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtProcessorFamilies.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + "****** "+ LocRM.GetString("strMotherboardInfo").ToUpper() + " *****" + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strMotherboardName") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtBoardDescriptions.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strManufacturer") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtBoardManufacturerName.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strDescription") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtBoardDescriptions.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strSerialNumber") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtBoardSerialNumbers.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + "****** "+ LocRM.GetString("strNetwork").ToUpper() + " ******" + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strComputerName") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtComputerName.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strIPAddressEthernet") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtIPaddres.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strMACAddress") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtMACaddres.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + "****** "+LocRM.GetString("strHardDrive").ToUpper()+" ******" + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strHardDriveSelected") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + cmbDrives.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strNameGeneral") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtDriveNames.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strTotalSize") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtDriveTotalSizes.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strTotalFreeSize") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtDriveTotalFreeSizes.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strAvailableFreeSize") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtAvailableFreeSpaces.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strVolumeLabel") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtVolumeLabels.Text + Environment.NewLine;
                richEditControl1.Document.AppendText(Environment.NewLine);
                richEditControl1.Text = richEditControl1.Text + LocRM.GetString("strDatabaseDiskUsage") + Environment.NewLine;
                richEditControl1.Text = richEditControl1.Text + txtDatabaseSizes.Text + Environment.NewLine;

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                saveFileDialog.Title = LocRM.GetString("strExportDocumentPDF");
                saveFileDialog.CheckPathExists = true;
                saveFileDialog.DefaultExt = "pdf";
                saveFileDialog.Filter = "PDF (*.pdf)|*.pdf| All Files (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".pdf";
                    //Set the required export options:
                    PdfExportOptions options = new PdfExportOptions();
                    options.DocumentOptions.Author = Functions.PublicVariables.UserLoggedSurname;
                    options.Compressed = false;
                    options.ImageQuality = PdfJpegImageQuality.High;
                    //Export the document to the file:
                    richEditControl1.ExportToPdf(fileName, options);
                    
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strSystemInfo"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
              //  barStatusLabel.Caption = "";
              //  barStatusLabel.Caption = "Failed to export to PDF due to an error: " + ex.Message;
            }
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                if (richEditControl1.IsPrintingAvailable)
                {
                    richEditControl1.ShowPrintDialog();
                  //  barStatusLabel.Caption = "";
                  //  barStatusLabel.Caption = "Done! System info printed";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
              //  barStatusLabel.Caption = "";
              //  barStatusLabel.Caption = "Failed to print due to an error: " + ex.Message;
            }
        }

        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {

                if (richEditControl1.IsPrintingAvailable)
                {
                    richEditControl1.ShowPrintPreview();
                    //  barStatusLabel.Caption = "";
                    //  barStatusLabel.Caption = "Done! System info printed";
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                //  barStatusLabel.Caption = "";
                //  barStatusLabel.Caption = "Failed to print due to an error: " + ex.Message;
            }
        }
    }
}
