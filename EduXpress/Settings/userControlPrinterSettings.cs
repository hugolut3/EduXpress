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
using System.Drawing.Printing;
using System.Resources;
using System.Management;

namespace EduXpress.Settings
{
    public partial class userControlPrinterSettings : DevExpress.XtraEditors.XtraUserControl
    {
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlPrinterSettings).Assembly);

        public userControlPrinterSettings()
        {
            InitializeComponent();
        }

        //private void btnExitApplication_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void btnEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            groupControlSelect.Enabled = true;
            btnSave.Enabled = true;
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (comboPrinterType.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectTypePrinter"), LocRM.GetString("strPrinterSettings"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboPrinterType.Focus();
                return;
            }
            if (comboLocalPrinterName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPrinter"), LocRM.GetString("strPrinterSettings"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboLocalPrinterName.Focus();
                return;
            }
            //save printer name
            Properties.Settings.Default.ReportPrinter = txtReportPrinter.Text.Trim();
            Properties.Settings.Default.ReceiptPrinter=txtReceiptPrinter.Text.Trim();

            if (comboPrinterType.SelectedIndex == 1)
            {
                Properties.Settings.Default.ReceiptPrinterType = "58mm";
                if (checkMarginWarnings.Checked == true)
                {
                    Properties.Settings.Default.Printer58mmMarginWarnings = true;
                }
                else
                {
                    Properties.Settings.Default.Printer58mmMarginWarnings = false;
                }
            }
            if (comboPrinterType.SelectedIndex == 2)
            {
                Properties.Settings.Default.ReceiptPrinterType = "80mm";
                if (checkMarginWarnings.Checked == true)
                {
                    Properties.Settings.Default.Printer80mmMarginWarnings = true;
                }
                else
                {
                    Properties.Settings.Default.Printer80mmMarginWarnings = false;
                }
            }
            if (comboPrinterType.SelectedIndex == 3)
            {
                Properties.Settings.Default.ReceiptPrinterType = "40W_30Hmm";
                if(checkWatermark.Checked==true)
                {
                    Properties.Settings.Default.PrinterWatermark = true;
                    Properties.Settings.Default.PrinterWatermarkTransparency =Convert.ToInt16( trackBarTransparency.EditValue);
                }                 

            }

            // ----- Save any updated settings.
            Properties.Settings.Default.Save();

            XtraMessageBox.Show(LocRM.GetString("strPrintersNamesSaved"), LocRM.GetString("strPrinterSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            groupControlSelect.Enabled = false; 
            btnSave.Enabled = false;
        }

        private void comboPrinterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLocalPrinterName.SelectedIndex != -1)
            {
                if (comboPrinterType.SelectedIndex == 0)
                {
                    txtReportPrinter.Text = comboLocalPrinterName.Text.Trim();
                }
                if (comboPrinterType.SelectedIndex == 1)
                {
                    txtReceiptPrinter.Text = comboLocalPrinterName.Text.Trim();
                }
                if (comboPrinterType.SelectedIndex == 2)
                {
                    txtReceiptPrinter.Text = comboLocalPrinterName.Text.Trim();
                }
                if (comboPrinterType.SelectedIndex == 3)
                {
                    txtReceiptPrinter.Text = comboLocalPrinterName.Text.Trim();
                }
            }
            
        }
       
        private void comboPrinterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPrinterType.SelectedIndex!=-1)
            {
                comboLocalPrinterName.Enabled = true;
               
                 populatePrinterNameCombo();
                // populatePrinterNames();

                //show warning margin outside printing area of page option            
                if (comboPrinterType.SelectedIndex == 1)  // If 58mm selected
                {
                    groupPaperRollPrinter.Visible = true;
                    groupStickerPrinter.Visible = false;
                    checkMarginWarnings.Checked = Properties.Settings.Default.Printer58mmMarginWarnings;
                }
                else if (comboPrinterType.SelectedIndex == 2)  // If 80mm  selected
                {
                    groupPaperRollPrinter.Visible = true;
                    groupStickerPrinter.Visible = false;
                    checkMarginWarnings.Checked = Properties.Settings.Default.Printer80mmMarginWarnings;
                }
                //show watermark for sticker printer
                else if (comboPrinterType.SelectedIndex == 3)
                {
                    groupPaperRollPrinter.Visible = false;
                    groupStickerPrinter.Visible = true;
                    trackBarTransparency.EditValue = Properties.Settings.Default.PrinterWatermarkTransparency;
                    lblTransparency.Text = trackBarTransparency.EditValue.ToString();
                }
                else
                {
                    groupStickerPrinter.Visible = false;
                    groupPaperRollPrinter.Visible = false;
                }
            }
            else
            {
                comboLocalPrinterName.Enabled = false;
            }            
            
            
        }
        private void populatePrinterNames()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strLoadingPrinters"));
                    
                }

               // ManagementScope objMS = new ManagementScope(ManagementPath.DefaultPath);
              //  objMS.Connect();
                SelectQuery objQuery = new SelectQuery("SELECT * FROM Win32_Printer");
                //   ManagementObjectSearcher objMOS = new ManagementObjectSearcher(objMS, objQuery);
                ManagementObjectSearcher objMOS = new ManagementObjectSearcher( objQuery);
                ManagementObjectCollection objMOC = objMOS.Get();
                foreach (ManagementObject Printers in objMOC)
                {
                    if (Convert.ToBoolean(Printers["Local"]))       // LOCAL PRINTERS.
                    {
                        comboLocalPrinterName.Properties.Items.Add(Printers["Name"]);
                    }
                    if (Convert.ToBoolean(Printers["Network"]))     // ALL NETWORK PRINTERS.
                    {
                     //   comboNetworkPrinterName.Properties.Items.Add(Printers["Name"]);
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

                MessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void populatePrinterNameCombo()
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strLoadingPrinters"));

                }
                // Add list of installed printers found to the combo box.
                // The pkInstalledPrinters string will be used to provide the display string.
                int i;
                string pkInstalledPrinters;
                comboLocalPrinterName.Properties.Items.Clear();
                for (i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                    comboLocalPrinterName.Properties.Items.Add(pkInstalledPrinters);
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
                MessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Fill comboPrinterType
        private void fillPrinterTypee()
        {
            comboPrinterType.Properties.Items.Clear();
            comboPrinterType.Properties.Items.AddRange(new object[] { LocRM.GetString("strReportPrinterA4"),
            LocRM.GetString("strReceiptPrinter58Roll") , LocRM.GetString("strReceiptPrinter80Roll"), LocRM.GetString("strReceiptPrinter40By30"), });
        }
        private void userControlPrinterSettings_Load(object sender, EventArgs e)
        {
            //load printer names
            txtReportPrinter.Text = Properties.Settings.Default.ReportPrinter.ToString();
            txtReceiptPrinter.Text = Properties.Settings.Default.ReceiptPrinter.ToString();
            fillPrinterTypee();
        }

        private void checkWatermark_CheckedChanged(object sender, EventArgs e)
        {
            if(checkWatermark.Checked==true)
            {
                trackBarTransparency.Enabled = true;
            }
            else
            {
                trackBarTransparency.Enabled = false;
            }
        }

        private void trackBarTransparency_EditValueChanged(object sender, EventArgs e)
        {
            lblTransparency.Text = trackBarTransparency.EditValue.ToString();
        }
    }
}
