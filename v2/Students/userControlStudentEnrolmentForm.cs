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
using DevExpress.XtraEditors.Camera;
using System.IO;
using CM.Sms;
using static EduXpress.Functions.PublicVariables;
using DevExpress.XtraPrinting;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Resources;
using System.Diagnostics;
using static EduXpress.Functions.PublicFunctions;
using DevExpress.XtraReports.UI;
using System.Drawing.Printing;
using System.Globalization;
using DevExpress.XtraBars.Ribbon;

namespace EduXpress.Students
{
    public partial class userControlStudentEnrolmentForm : DevExpress.XtraEditors.XtraUserControl, Functions.IMergeRibbons 
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlStudentEnrolmentForm).Assembly);

        //create global methods of ribons and status bar to merge when in main.
        //add the ImergeRibbons interface.
        public RibbonControl MainRibbon { get; set; }
        public RibbonStatusBar MainStatusBar { get; set; }
        public userControlStudentEnrolmentForm()
        {
            InitializeComponent();
        }
        //Merge ribbon and statusBar
        public void MergeRibbon()
        {
            if (MainRibbon != null)
            {
                MainRibbon.MergeRibbon(this.ribbonControl1);
            }
        }
        public void MergeStatusBar()
        {
            if (MainStatusBar != null)
            {
                MainStatusBar.MergeStatusBar(this.ribbonStatusBar1);
            }
        }
        private void clearDates()
        {
            dtEnrolmentDate.EditValue = null;
            dtDateBirth.EditValue = null;
            txtStudentAge.Text = "";
        }
        private void setDates()
        {
            dtEnrolmentDate.EditValue = DateTime.Now;
            dtDateBirth.EditValue = DateTime.Now;
            txtStudentAge.Text = "";
        }
        private void enableControls()
        {
            cmbSection.Enabled = true;
            cmbSection.Properties.ReadOnly = false;
            cmbClass.Enabled = true;
            txtStudentAge.Enabled = true;
            txtStudentAge.Properties.ReadOnly = false;
            txtSiblingsNo.Enabled = true;
            txtSiblingsNo.Properties.ReadOnly = false;
            txtParentNotificationNo.Enabled = true;
            txtParentNotificationNo.Properties.ReadOnly = false;
            txtParentNotificationEmail.Enabled = true;
            txtParentNotificationEmail.Properties.ReadOnly = false;
        }
        private void disableControls()
        {
            cmbSection.Enabled = false;
            cmbSection.Properties.ReadOnly = true;
            cmbClass.Enabled = false;
            txtStudentAge.Enabled = false;
            txtStudentAge.Properties.ReadOnly = true;
            txtSiblingsNo.Enabled = false;
            txtSiblingsNo.Properties.ReadOnly = true;
            txtParentNotificationNo.Enabled = false;
            txtParentNotificationNo.Properties.ReadOnly = true;
            txtParentNotificationEmail.Enabled = false;
            txtParentNotificationEmail.Properties.ReadOnly = true;
        }
        private void hideControlsNotPrinted()
        {
            layoutControlWebcam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlBrowse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlRemove.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlItemRequiredFields.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;


        }
        private void showhidenControlsNotPrinted()
        {
            layoutControlWebcam.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlBrowse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlRemove.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            layoutControlItemRequiredFields.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

        }
        private void btnPrintPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                hideControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    enableLayoutGroups();
                    clearDates();
                    enableControls();
                }
                // Create a new report instance.
                XtraReportStudentEnrolmentForm report = new XtraReportStudentEnrolmentForm();
                report.DisplayName = LocRM.GetString("strStudentEnrolmentForm");

                // Link the required control with the PrintableComponentContainers of a report.
                layoutControl1.OptionsPrint.OldPrinting = true;
                layoutControl1.OptionsPrint.AllowFitToPage = true;
                report.printableComponentContainer1.PrintableComponent = layoutControl1;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                report.Landscape = true;

                // Invoke a Print Preview for the created report document. 
                ReportPrintTool printTool = new ReportPrintTool(report);
                printTool.ShowRibbonPreviewDialog();
                showhidenControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    disableLayoutGroups();
                    setDates();
                    disableControls();
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
            //Print Preview with Grid
            #region PrintPreviewGrid
            //// Check whether the LayoutControl can be previewed.
            //if (!layoutControl1.IsPrintingAvailable)
            //{
            //    XtraMessageBox.Show(LocRM.GetString("strPrintErrorLibraryNotFound"), LocRM.GetString("strError")); 
            //    return;
            //}
            //try
            //{
            //    if (txtStudentNumber.Text == "")
            //    {
            //        enableLayoutGroups();
            //        clearDates();
            //        enableControls();
            //    }
            //    hideControlsNotPrinted();
            //    // Open the Preview window.
            //    //layoutControlPatientRegistration.ShowPrintPreview();
            //    layoutControl1.OptionsPrint.OldPrinting = true;
            //    layoutControl1.OptionsPrint.AllowFitToPage = true;
            //    //Print with OldPrinting to follow the "WYSIWYG" approach. It is printed/exported as is, keeping the item arrangement
            //    layoutControl1.ShowRibbonPrintPreview();
            //    showhidenControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        disableLayoutGroups();
            //        setDates();
            //        disableControls();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        private void btnPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                hideControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    enableLayoutGroups();
                    clearDates();
                    enableControls();
                }
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
                }

                // Create a new report instance.
                XtraReportStudentEnrolmentForm report = new XtraReportStudentEnrolmentForm();
                report.DisplayName = LocRM.GetString("strStudentEnrolmentForm");
                PrinterSettings printerSettings = new PrinterSettings();

                // Link the required control with the PrintableComponentContainers of a report.
                layoutControl1.OptionsPrint.OldPrinting = true;
                layoutControl1.OptionsPrint.AllowFitToPage = true;
                report.printableComponentContainer1.PrintableComponent = layoutControl1;

                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
                report.PaperKind = System.Drawing.Printing.PaperKind.A4;
                report.Landscape = true;

                // Specify the PrinterName if the target printer is not the default one.
                printerSettings.PrinterName = Properties.Settings.Default.ReportPrinter;

                // Invoke a Print Preview for the created report document. 
                ReportPrintTool printTool = new ReportPrintTool(report);
                // preview.ShowRibbonPreview();
                printTool.Print(Properties.Settings.Default.ReportPrinter);

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                showhidenControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    disableLayoutGroups();
                    setDates();
                    disableControls();
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

            //Print with Grid
            #region PrintGrid
            //// Check whether the LayoutControl can be previewed.
            //if (!layoutControl1.IsPrintingAvailable)
            //{
            //    XtraMessageBox.Show(LocRM.GetString("strPrintErrorLibraryNotFound"), LocRM.GetString("strError"));
            //    return;
            //}
            //try
            //{
            //    if (txtStudentNumber.Text == "")
            //    {
            //        enableLayoutGroups();
            //        clearDates();
            //        enableControls();
            //    }
            //    hideControlsNotPrinted();
            //    if (splashScreenManager1.IsSplashFormVisible == false)
            //    {
            //        splashScreenManager1.ShowWaitForm();
            //        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strPrinting"));
            //    }
            //    //Print manually 
            //    layoutControl1.OptionsPrint.OldPrinting = true;
            //    layoutControl1.OptionsPrint.AllowFitToPage = true;

            //    PrintingSystem printingSystem = new PrintingSystem();
            //    PrintableComponentLink link = new PrintableComponentLink();
            //    link.Component = layoutControl1;
            //    link.Landscape = true;
            //   // printingSystem.ExecCommand(PrintingSystemCommand.Scale, new object[] { 1 });
            //    printingSystem.Links.Add(link);
            //    //  Print to selected saved printer name
            //    link.Print(Properties.Settings.Default.ReportPrinter);


            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //    showhidenControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        disableLayoutGroups();
            //        setDates();
            //        disableControls();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //    XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            //} 
            #endregion
        }

        private void btnExportPDF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Export to PDF with report 
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                hideControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    enableLayoutGroups();
                    clearDates();
                    enableControls();
                }
                // Create a new report instance
                XtraReportStudentEnrolmentForm report = new XtraReportStudentEnrolmentForm();
                // Link the required control with the PrintableComponentContainers of a report.
                layoutControl1.OptionsPrint.OldPrinting = true;
                layoutControl1.OptionsPrint.AllowFitToPage = true;
                report.printableComponentContainer1.PrintableComponent = layoutControl1;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".pdf";
                    //Export to pdf
                    report.ExportToPdf(fileName);
                    //open document
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strStudentEnrolmentForm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                showhidenControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    disableLayoutGroups();
                    setDates();
                    disableControls();
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

            //Export to PDF with GridView
            #region ExportPDFGridView
            // Check whether the LayoutControl can be previewed.
            //if (!layoutControl1.IsPrintingAvailable)
            //{
            //    XtraMessageBox.Show(LocRM.GetString("strExportErrorLibraryNotFound"), LocRM.GetString("strError"));
            //    return;
            //}
            //try
            //{
            //    hideControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        enableLayoutGroups();
            //        clearDates();
            //        enableControls();
            //    }
            //    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
            //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        layoutControl1.OptionsPrint.OldPrinting = true;
            //        layoutControl1.OptionsPrint.AllowFitToPage = true;
            //        string fileName = saveFileDialog.FileName + ".pdf";
            //        //change page orientation to Landscape

            //        PrintingSystem ps = new PrintingSystem();
            //        PrintableComponentLink printableComponentLink = new PrintableComponentLink(ps);
            //        printableComponentLink.Component = layoutControl1;
            //        printableComponentLink.Landscape = true;
            //        //printableComponentLink.Margins.Bottom = 500;

            //        //Export to pdf
            //        printableComponentLink.ExportToPdf(fileName);
            //        //open document
            //        if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strStudentEnrolment"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            if (splashScreenManager1.IsSplashFormVisible == false)
            //            {
            //                splashScreenManager1.ShowWaitForm();
            //            }
            //            Process.Start(fileName);
            //            if (splashScreenManager1.IsSplashFormVisible == true)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //            }
            //        }
            //    }
            //    showhidenControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        disableLayoutGroups();
            //        setDates();
            //        disableControls();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.ToString());
            //} 
            #endregion
        }

        private void btnExportExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Export to Excel with GridView
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                hideControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    enableLayoutGroups();
                    clearDates();
                    enableControls();
                }
                // Create a new report instance
                XtraReportStudentEnrolmentForm report = new XtraReportStudentEnrolmentForm();
                // Link the required control with the PrintableComponentContainers of a report.
                layoutControl1.OptionsPrint.OldPrinting = true;
                layoutControl1.OptionsPrint.AllowFitToPage = true;
                report.printableComponentContainer1.PrintableComponent = layoutControl1;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".xlsx";
                    //Export to Excel
                    report.ExportToXlsx(fileName);
                    //open document
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strStudentEnrolmentForm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                showhidenControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    disableLayoutGroups();
                    setDates();
                    disableControls();
                }

            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }

            //Export to Excel with GridView
            #region ExportExcelGridView
            // Check whether the LayoutControl can be previewed.
            //if (!layoutControl1.IsPrintingAvailable)
            //{
            //    XtraMessageBox.Show(LocRM.GetString("strExportErrorLibraryNotFound"), LocRM.GetString("strError"));
            //    return;
            //}
            //try
            //{
            //    hideControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        enableLayoutGroups();
            //        clearDates();
            //        enableControls();
            //    }
            //    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
            //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string fileName = saveFileDialog.FileName + ".xlsx";
            //        //Export to excel
            //        layoutControl1.ExportToXlsx(fileName);
            //        //open document
            //        if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strStudentEnrolment"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            if (splashScreenManager1.IsSplashFormVisible == false)
            //            {
            //                splashScreenManager1.ShowWaitForm();
            //            }
            //            Process.Start(fileName);
            //            if (splashScreenManager1.IsSplashFormVisible == true)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //            }
            //        }
            //    }
            //    showhidenControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        disableLayoutGroups();
            //        setDates();
            //        disableControls();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //    XtraMessageBox.Show(ex.ToString());
            //} 
            #endregion
        }

        private void btnExportRTF_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Export to Word with GridView
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                hideControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    enableLayoutGroups();
                    clearDates();
                    enableControls();
                }
                // Create a new report instance
                XtraReportStudentEnrolmentForm report = new XtraReportStudentEnrolmentForm();
                // Link the required control with the PrintableComponentContainers of a report.
                layoutControl1.OptionsPrint.OldPrinting = true;
                layoutControl1.OptionsPrint.AllowFitToPage = true;
                report.printableComponentContainer1.PrintableComponent = layoutControl1;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".docx";
                    //Export to Word
                    report.ExportToDocx(fileName);
                    //open document
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strStudentEnrolmentForm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                showhidenControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    disableLayoutGroups();
                    setDates();
                    disableControls();
                }

            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
            //Export to Word with GridView
            #region ExportWordGridView
            //// Check whether the LayoutControl can be previewed.
            //if (!layoutControl1.IsPrintingAvailable)
            //{
            //    XtraMessageBox.Show(LocRM.GetString("strExportErrorLibraryNotFound"), LocRM.GetString("strError"));
            //    return;
            //}
            //try
            //{
            //    hideControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        enableLayoutGroups();
            //        clearDates();
            //        enableControls();
            //    }
            //    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
            //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string fileName = saveFileDialog.FileName + ".docx";
            //        //Export to RTF
            //        layoutControl1.ExportToDocx(fileName);
            //        //open document
            //        if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strStudentEnrolment"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            if (splashScreenManager1.IsSplashFormVisible == false)
            //            {
            //                splashScreenManager1.ShowWaitForm();
            //            }
            //            Process.Start(fileName);
            //            if (splashScreenManager1.IsSplashFormVisible == true)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //            }
            //        }
            //    }
            //    showhidenControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        disableLayoutGroups();
            //        setDates();
            //        disableControls();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    XtraMessageBox.Show(ex.ToString());
            //} 
            #endregion
        }

        private void btnExportHTML_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Export to Word with GridView
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                hideControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    enableLayoutGroups();
                    clearDates();
                    enableControls();
                }
                // Create a new report instance
                XtraReportStudentEnrolmentForm report = new XtraReportStudentEnrolmentForm();
                // Link the required control with the PrintableComponentContainers of a report.
                layoutControl1.OptionsPrint.OldPrinting = true;
                layoutControl1.OptionsPrint.AllowFitToPage = true;
                report.printableComponentContainer1.PrintableComponent = layoutControl1;
                report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width

                XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName + ".html";
                    //Export to HTML
                    report.ExportToHtml(fileName);
                    //open document
                    if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strStudentEnrolmentForm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                showhidenControlsNotPrinted();
                if (txtStudentNumber.Text == "")
                {
                    disableLayoutGroups();
                    setDates();
                    disableControls();
                }

            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }

            //Export to HTML with GridView
            #region ExportHTMLGridView
            //// Check whether the LayoutControl can be previewed.
            //if (!layoutControl1.IsPrintingAvailable)
            //{
            //    XtraMessageBox.Show(LocRM.GetString("strExportErrorLibraryNotFound"), LocRM.GetString("strError"));
            //    return;
            //}
            //try
            //{
            //    hideControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        enableLayoutGroups();
            //        clearDates();
            //        enableControls();
            //    }
            //    XtraSaveFileDialog saveFileDialog = new XtraSaveFileDialog();
            //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //    {
            //        string fileName = saveFileDialog.FileName + ".html";
            //        //Export to HTML
            //        layoutControl1.ExportToHtml(fileName);
            //        //open document
            //        if (XtraMessageBox.Show(LocRM.GetString("strWantOpenFile"), LocRM.GetString("strStudentEnrolment"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            //        {
            //            if (splashScreenManager1.IsSplashFormVisible == false)
            //            {
            //                splashScreenManager1.ShowWaitForm();
            //            }
            //            Process.Start(fileName);
            //            if (splashScreenManager1.IsSplashFormVisible == true)
            //            {
            //                splashScreenManager1.CloseWaitForm();
            //            }
            //        }
            //    }
            //    showhidenControlsNotPrinted();
            //    if (txtStudentNumber.Text == "")
            //    {
            //        disableLayoutGroups();
            //        setDates();
            //        disableControls();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (splashScreenManager1.IsSplashFormVisible == true)
            //    {
            //        splashScreenManager1.CloseWaitForm();
            //    }
            //    XtraMessageBox.Show(ex.ToString());
            //} 
            #endregion
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            fillCycle();
            fillGender();
            fillReligion();
            fillLanguage();
            fillResults();
            fillCountries();
            FillAcademicYear();
            enableLayoutGroups();
            dtEnrolmentDate.DateTime = DateTime.Now;
            GenerateStudentNumber();
            barcode();                      
            
            clear();
            btnSave.Enabled = true;
            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;           
            
        }
        private void enableLayoutGroups()
        {
            layoutControlGroupStudentNumber.Enabled = true;
            layoutControlGroupStudentDetails.Enabled = true;
            layoutControlGroupParentGuardian.Enabled = true;
            layoutControlGroupSchoolExcursion.Enabled = true;
        }
        private void disableLayoutGroups()
        {
            layoutControlGroupStudentNumber.Enabled = false;
            layoutControlGroupStudentDetails.Enabled = false;
            layoutControlGroupParentGuardian.Enabled = false;
            layoutControlGroupSchoolExcursion.Enabled = false;
        }
        private void barcode()
        {
            barCodeControlStudentNumber.Text = txtStudentNumber.Text;
            barCodeControlStudentNumber.BackColor = Color.White;
            barCodeControlStudentNumber.ForeColor = Color.Black;
        }
        private void clear()
        {
            dtEnrolmentDate.DateTime = DateTime.Now;
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            cmbCycle.SelectedIndex = -1;
            cmbAcademicYear.SelectedIndex = -1;
            txtStudentSurname.Text = "";
            txtStudentFirstNames.Text = "";
            dtDateBirth.DateTime = DateTime.Now;
            txtStudentAge.Text = "";
            cmbStudentGender.SelectedIndex = -1;
            cmbNationality.SelectedIndex = -1;
            txtLastSchoolAttended.Text = "";
            cmbLastSchoolResult.SelectedIndex = -1;
            txtPassPercentage.Text = "";
            txtStudentEmail.Text = "";
            txtStudentPhoneNumber.Text = "";
            cmbHomeLanguage.SelectedIndex = -1;
            cmbReligion.SelectedIndex = -1;
            txtMedicalAllergies.Text = "";
            ckStudentSibling.Checked = false;
            txtSiblingsNo.Text = "";
            txtSiblingsNo.ReadOnly = true;
            txtTutorName.Text = "";
            txtTutorProfession.Text = "";
            txtFatherProfession.Text = "";
            txtMotherProfession.Text = "";

            //dispose of images
            if (pictureStudent != null && pictureStudent.Image != null)
            {
                pictureStudent.Image.Dispose();
            }

            pictureStudent.EditValue = null;
            txtFatherSurname.Text = "";
            txtFatherNames.Text = "";
            txtFatherContactNo.Text = "";
            txtFatherEmail.Text = "";
            ckFatherNotificationNo.Checked = false;
            ckFatherNotificationNo.Enabled = false;
            ckFatherNotificationEmail.Checked = false;
            ckFatherNotificationEmail.Enabled = false;
            txtMotherSurname.Text = "";
            txtMotherNames.Text = "";
            txtMotherContactNo.Text = "";
            txtMotherEmail.Text = "";
            ckMotherNotificationNo.Checked = false;
            ckMotherNotificationEmail.Checked = false;
            ckMotherNotificationNo.Enabled = false;
            ckMotherNotificationEmail.Enabled = false;
            txtParentNotificationNo.Text = "";
            txtParentNotificationEmail.Text = "";
            txtHomeAddress.Text = "";
            txtAlternativeEmergencyPhoneNo.Text = "";
            txtEmergencyPhoneNo.Text = "";
            ckSchoolActivities.Checked = false;
            ckSchoolPhotos.Checked = false;
            ckBirthCertificate.Checked = false;
            ckParentIdentityDocuments.Checked = false;
            ckPreviousSchoolDocuments.Checked = false;            
        }
        private void Reset()
        {
            clear();
            txtStudentNumber.Text = "";
            barCodeControlStudentNumber.Text = "";
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            
        }
        //Generate Student Number
        string StudentNumber;
        private void GenerateStudentNumber()
        {
            DateTime moment = DateTime.Now;
            string year = moment.Year.ToString();
            try
            {
                StudentNumber = GenerateID();
                // txtStudentNumber.Text = year + GenerateID();
                txtStudentNumber.Text = year + StudentNumber;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GenerateID()
        {
            
            string value = "00";
            int IDindex = 0;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    // Fetch the latest ID from the database
                    con.Open();
                    cmd = new SqlCommand("SELECT TOP 1 StudentID FROM Students order BY StudentID DESC", con);
                    rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        IDindex = Convert.ToInt16(rdr["StudentID"]);
                    }
                    rdr.Close();
                }
                    
                IDindex++;
                // Because incrementing a string with an integer removes 0's
                // we need to replace them. If necessary.
                if (IDindex <= 9)
                {
                    value = "00" + value + IDindex.ToString();
                }
                else if (IDindex <= 99)
                {
                    value = "0" + value + IDindex.ToString();
                }
                else if (IDindex <= 999)
                {
                    //value = "00" + value + IDindex.ToString();
                    value = value + IDindex.ToString();
                }
                else if (IDindex <= 9999)
                {
                    value = "0" + IDindex.ToString();
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

                // If an error occurs, check the connection state and close it if necessary.
                if (con.State == ConnectionState.Open)
                    con.Close();
                value = "00";
            }
            return value;
        }
        //Fill student Gender
        private void fillGender()
        {
            cmbStudentGender.Properties.Items.Clear();
            cmbStudentGender.Properties.Items.AddRange(new object[] { LocRM.GetString("strMale"),
            LocRM.GetString("strFemale") });
        }
        //Fill Results
        private void fillResults()
        {
            cmbLastSchoolResult.Properties.Items.Clear();
            cmbLastSchoolResult.Properties.Items.AddRange(new object[] { LocRM.GetString("strPassed"),
            LocRM.GetString("strFailed"),LocRM.GetString("strN/A")  });
        }
        //Fill Countries
        private void fillCountries()
        {
            cmbNationality.Properties.Items.Clear();
            cmbNationality.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strAngola"),
            LocRM.GetString("strBotswana"),
            LocRM.GetString("strBurundi"),
            LocRM.GetString("strCameroon"),
            LocRM.GetString("strCAR"),
            LocRM.GetString("strChad"),
            LocRM.GetString("strCongoBrazzaville"),
            LocRM.GetString("strDRC"),
            LocRM.GetString("strEquatorialGuinea"),
            LocRM.GetString("strEthiopia"),
            LocRM.GetString("strGabon"),
            LocRM.GetString("strKenya"),
            LocRM.GetString("strLesotho"),
            LocRM.GetString("strMadagascar"),
            LocRM.GetString("strMalawi"),
            LocRM.GetString("strMauritius"),
            LocRM.GetString("strMozambique"),
            LocRM.GetString("strNamibia"),
            LocRM.GetString("strRwanda"),
            LocRM.GetString("strSãoToméPríncipe"),
            LocRM.GetString("strSouthAfrica"),
            LocRM.GetString("strSwaziland"),
            LocRM.GetString("strTanzania"),
            LocRM.GetString("strUganda"),
            LocRM.GetString("strZambia"),
            LocRM.GetString("strZimbabwe"),
            LocRM.GetString("strOther")});
        }


        //Fill Religion
        private void fillReligion()
        {
            cmbReligion.Properties.Items.Clear();
            cmbReligion.Properties.Items.AddRange(new object[] { 
            LocRM.GetString("strBahai"),
            LocRM.GetString("strBuddhism"),
            LocRM.GetString("strCatholic"),
            LocRM.GetString("strHindu"),
            LocRM.GetString("strIslam"),
            LocRM.GetString("strJudaism"),
            LocRM.GetString("strKimbangu"),
            LocRM.GetString("strProtestant"),
            LocRM.GetString("strPentecost"),
            LocRM.GetString("strAfricanTraditional"),
            LocRM.GetString("strOther")});
        }
        //Fill Language
        private void fillLanguage()
        {
            cmbHomeLanguage.Properties.Items.Clear();
            cmbHomeLanguage.Properties.Items.AddRange(new object[] {
            LocRM.GetString("strArabic"),
            LocRM.GetString("strEnglish"),
            LocRM.GetString("strFrench"),
            LocRM.GetString("strKikongo"),
            LocRM.GetString("strLingala"),
            LocRM.GetString("strPortuguese"),
            LocRM.GetString("strSpanish"),
            LocRM.GetString("strSwahili"),
            LocRM.GetString("strTshiluba"),            
            LocRM.GetString("strOther")});
        }
        //Fill cmbcycle
        private void fillCycle()
        {
            cmbCycle.Properties.Items.Clear();
            cmbCycle.Properties.Items.AddRange(new object[] 
            { 
                LocRM.GetString("strMaternelle"),
                LocRM.GetString("strPrimaire") , 
                LocRM.GetString("strSecondOrientation"),
                LocRM.GetString("strSecondHuma"),
                LocRM.GetString("strTVETCollege")
            });    
        }
        //Fill cmbSection with Sections
        private void FillSection()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
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
        //Fill cmbClass with School Classes
        private void FillClass()
        {            
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    if (cmbSection.SelectedIndex != -1)
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1 and SectionName=@d2", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = cmbCycle.Text.Trim();
                        cmd.Parameters.Add("@d2", SqlDbType.NChar, 60, " SectionName").Value = cmbSection.Text.Trim();
                    }
                    else
                    {
                        cmd = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes where Cycle=@d1", con);
                        cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = cmbCycle.Text.Trim();
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
        //Fill Academic  Year
        private void FillAcademicYear()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(SchoolYear) FROM AcademicYear", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbAcademicYear.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbAcademicYear.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbAcademicYear.SelectedIndex = -1;
                    con.Close();
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
        private void btnRemovePicture_Click(object sender, EventArgs e)
        {
            pictureStudent.EditValue = null;
        }
        //check file write permission
        public static bool HasWritePermission(string tempfilepath)
        {
            ResourceManager LocRM1 = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlStudentEnrolmentForm).Assembly);
            try
            {
                System.IO.File.Create(tempfilepath + "temp.txt").Close();
                System.IO.File.Delete(tempfilepath + "temp.txt");
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Properties.Settings.Default.DirectoryStudentPhotoWriteAccess = false;
                XtraMessageBox.Show(LocRM1.GetString("strNoPermissionSavePhotos")  + " "+ Properties.Settings.Default.StudentsPhotosDirectory +". "+ LocRM1.GetString("strContactSystemAdministrator") );
                return false;
            }
            Properties.Settings.Default.DirectoryStudentPhotoWriteAccess = true;
            //XtraMessageBox.Show("has access");
            return true;
        }

        private void btnBrowsePicture_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

              try
              {

            //the name can be on of the following values: OnClickedLoad;OnClickedSave;OnClickedCut;OnClickedCopy;OnClickedPaste;OnClickedDelete
                   InvokeMenuMethod(GetMenu(pictureStudent), "OnClickedLoad");

            #region openfile dialog
        //    XtraOpenFileDialog OpenFile = new XtraOpenFileDialog();
         //   //
        //    OpenFile.FileName = "";
         //              OpenFile.Title = "Photo:";
         //              OpenFile.Filter = "Image files: (*.jpg)|*.jpg|(*.jpeg)|*.jpeg|(*.png)|*.png|(*.Gif)|*.Gif|(*.bmp)|*.bmp| All Files (*.*)|*.*";
         //              DialogResult res = OpenFile.ShowDialog();
         //              if (res == DialogResult.OK)
          //             {
          //                 this.pictureStudent.Image = System.Drawing.Image.FromFile(OpenFile.FileName);
           //            }
                #endregion
                //resize image
                if (pictureStudent.Image != null)
                {
                  //  using (Bitmap currentImage = pictureStudent.EditValue as Bitmap)
                   // {
                    //    Bitmap savedImage = new Bitmap(currentImage, pictureStudent.ClientSize.Width, pictureStudent.ClientSize.Height);
                     //   savedImage.Save(txtStudentNumber.Text + ".jpg");
                    //    pictureStudent.Image = savedImage;
                        //  currentImage.Dispose();

                        //invoke the pictureEdit copy image 
                        //the name can be on of the following values: OnClickedLoad;OnClickedSave;OnClickedCut;OnClickedCopy;OnClickedPaste;OnClickedDelete
                        InvokeMenuMethod(GetMenu(pictureStudent), "OnClickedCopy"); //save image to clipboard  

                        //save image from clipboard
                        if (Clipboard.GetDataObject() != null)
                        {
                            IDataObject data = Clipboard.GetDataObject();
                            string destinationDirectory = Properties.Settings.Default.StudentsPhotosDirectory;

                            if (destinationDirectory == null)
                            {
                                destinationDirectory = "C:\\Students_Photos\\";
                                bool exists = System.IO.Directory.Exists("C:\\Students_Photos\\");
                                if (!exists)
                                    System.IO.Directory.CreateDirectory("C:\\Students_Photos\\");
                            }

                            if (data.GetDataPresent(DataFormats.Bitmap))
                            {
                                if (File.Exists(destinationDirectory + "\\" + txtStudentNumber.Text + "." + ".jpg"))
                                {
                                    XtraMessageBox.Show(LocRM.GetString("strImageAlreadySaved"));
                                }

                            //  Image image = (Image)data.GetData(DataFormats.Bitmap, true);
                            using (Image image = (Image)data.GetData(DataFormats.Bitmap))
                            {
                                if (Properties.Settings.Default.DirectoryStudentPhotoWriteAccess == false)
                                {
                                    //check directory write access
                                    HasWritePermission(destinationDirectory);
                                }
                                // image.Save(destinationDirectory + "\\" + txtStudentNumber.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                image.Save(destinationDirectory + "\\" + txtStudentNumber.Text + ".jpg");
                            }                                                            
                                //dispose of image
                                // image.Dispose();

                            }
                            else
                            {
                                XtraMessageBox.Show(LocRM.GetString("strNoImageClipboard"));
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(LocRM.GetString("strClipboardEmpty"));
                        }
                   // }
                        
                    
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWebcam_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            try
            {
                TakePictureDialog d = new TakePictureDialog();
                Image i;
            if (d.ShowDialog() == DialogResult.OK)
            {
                     i = d.Image;                    
                    pictureStudent.Image = i; 
                    
            }
                
                //resize image
                if (pictureStudent.Image != null)
            {

                  //  using (Bitmap currentImage = pictureStudent.EditValue as Bitmap)
                   // {
                       // Bitmap savedImage = new Bitmap(currentImage, pictureStudent.ClientSize.Width, pictureStudent.ClientSize.Height);
                       // savedImage.Save(txtStudentNumber.Text + ".jpg");
                     //   pictureStudent.Image = savedImage;
                        //currentImage.Dispose();

                        //invoke the pictureEdit copy image 
                        //the name can be on of the following values: OnClickedLoad;OnClickedSave;OnClickedCut;OnClickedCopy;OnClickedPaste;OnClickedDelete
                        InvokeMenuMethod(GetMenu(pictureStudent), "OnClickedCopy"); //save image to clipboard  

                        //save image from clipboard
                        if (Clipboard.GetDataObject() != null)
                        {
                            IDataObject data = Clipboard.GetDataObject();
                            string destinationDirectory = Properties.Settings.Default.StudentsPhotosDirectory;

                            //if (destinationDirectory=="")
                            if (destinationDirectory == null)
                            {
                                destinationDirectory = "C:\\Students_Photos\\";
                                bool exists = System.IO.Directory.Exists("C:\\Students_Photos\\");
                                if (!exists)
                                    System.IO.Directory.CreateDirectory("C:\\Students_Photos\\");
                            }

                            if (data.GetDataPresent(DataFormats.Bitmap))
                            {
                                if (File.Exists(destinationDirectory + "\\" + txtStudentNumber.Text + "." + ".jpg"))
                                {
                                    XtraMessageBox.Show(LocRM.GetString("strImageAlreadySaved"));
                                }

                            using (Image image = (Image)data.GetData(DataFormats.Bitmap, true))
                            {
                                if (Properties.Settings.Default.DirectoryStudentPhotoWriteAccess == false)
                                {
                                    //check directory write access
                                    HasWritePermission(destinationDirectory);
                                }
                                image.Save(destinationDirectory + "\\" + txtStudentNumber.Text + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                            } 
                                //dispose of image
                                // image.Dispose();
                            }
                            else
                            {
                                XtraMessageBox.Show(LocRM.GetString("strNoImageClipboard"));
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show(LocRM.GetString("strClipboardEmpty"));
                        }

                 //   }
                    
                }
                

            }
            catch (Exception ex)
            {
               // GeneralError("userControlstudentEnrolmentForm.WebCam", ex);
                 XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private DevExpress.XtraEditors.Controls.PictureMenu GetMenu(DevExpress.XtraEditors.PictureEdit edit)
        {
            PropertyInfo pi = typeof(DevExpress.XtraEditors.PictureEdit).GetProperty("Menu", BindingFlags.NonPublic | BindingFlags.Instance);
            if (pi != null)
                return pi.GetValue(edit, null) as DevExpress.XtraEditors.Controls.PictureMenu;
            return null;
        }

        private void InvokeMenuMethod(DevExpress.XtraEditors.Controls.PictureMenu menu, String name)
        {
            MethodInfo mi = typeof(DevExpress.XtraEditors.Controls.PictureMenu).GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
            if (mi != null && menu != null)
                mi.Invoke(menu, new object[] { menu, new EventArgs() });
        }
        
        
        private void dtDateBirth_EditValueChanged(object sender, EventArgs e)
        {
            txtStudentAge.Text = GetPersonAge(dtDateBirth.DateTime, DateTime.Now).ToString();
            globalAge = Convert.ToInt16(txtStudentAge.Text);
        }
        int globalAge;
        public static int GetPersonAge(DateTime birthdate, DateTime AgeOnThisDate)
        {
            int age = AgeOnThisDate.Year - birthdate.Year;
            if (AgeOnThisDate.Month < birthdate.Month || (AgeOnThisDate.Month == birthdate.Month && AgeOnThisDate.Day < birthdate.Day))
                age--;
            if (age < 0)
                return 0;
            else
                return age;
        }

        private void ckStudentSibling_CheckedChanged(object sender, EventArgs e)
        {
            if (ckStudentSibling.Checked)
            {
                txtSiblingsNo.ReadOnly = false;
            }
            else
            {
                txtSiblingsNo.Text = "0";
                txtSiblingsNo.ReadOnly = true;
            }
        }

        private void ckFatherNotificationNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ckFatherNotificationNo.Checked)
            {
                txtParentNotificationNo.Text = txtFatherContactNo.Text;
                ckMotherNotificationNo.Checked = false;
            }
            else
            {
                txtParentNotificationNo.Text = "";               
            }
        }

        private void ckFatherNotificationEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (ckFatherNotificationEmail.Checked)
            {
                txtParentNotificationEmail.Text = txtFatherEmail.Text;
                ckMotherNotificationEmail.Checked = false;
            }
            else
            {
                txtParentNotificationEmail.Text = "";
            }
        }

        private void ckMotherNotificationNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMotherNotificationNo.Checked)
            {
                txtParentNotificationNo.Text = txtMotherContactNo.Text;
                ckFatherNotificationNo.Checked = false;
            }
            else
            {
                txtParentNotificationNo.Text = "";
            }
        }

        private void ckMotherNotificationEmail_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMotherNotificationEmail.Checked)
            {
                txtParentNotificationEmail.Text = txtMotherEmail.Text;
                ckFatherNotificationEmail.Checked = false;
            }
            else
            {
                txtParentNotificationEmail.Text = "";
            }
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
        //flag. 1= save, 2 = update
        int saveUpdate = 0;
        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbCycle.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectCycle"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCycle.Focus();
                return;
            }
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            if (cmbAcademicYear.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbAcademicYear.Focus();
                return;
            }
            if (txtStudentSurname.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSurname"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentSurname.Focus();
                return;
            }
            if (txtStudentFirstNames.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentFirstNames.Focus();
                return;
            }
            if (cmbStudentGender.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectGender"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbStudentGender.Focus();
                return;
            }
            if (txtStudentAge.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectDateBirth"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtDateBirth.Focus();
                return;
            }
            if (pictureStudent.EditValue == null)
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPhoto"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnWebcam.Focus();  
                return;
            }
            if (txtEmergencyPhoneNo.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterPhoneNoEmergency"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmergencyPhoneNo.Focus();
                return;
            }
            if (txtAlternativeEmergencyPhoneNo.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterPhoneNoAbsence"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAlternativeEmergencyPhoneNo.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
            {
                //check if this student exist
                string surname = "";
                string firstNames = "";
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select StudentSurname,StudentFirstNames from Students where StudentSurname=@d1 and StudentFirstNames=@d2";
                    cmd = new SqlCommand(ct);
                    cmd.Parameters.AddWithValue("@d1",txtStudentSurname.Text);
                    cmd.Parameters.AddWithValue("@d2",txtStudentFirstNames.Text);
                    cmd.Connection = con;

                    rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                       while (rdr.Read())
                       {
                            surname = rdr[0].ToString().Trim().ToLower();
                            firstNames = rdr[1].ToString().Trim().ToLower();
                        }
                        if ((txtStudentSurname.Text.Trim().ToLower() == surname) && (txtStudentFirstNames.Text.Trim().ToLower() == firstNames))
                        {
                            XtraMessageBox.Show(LocRM.GetString("strStudentExists"), LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtStudentSurname.Focus();
                            if ((rdr != null))
                                rdr.Close();
                            return;
                        }
                    }
                }

                //start saving new student
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "insert into Students(StudentNumber,Section,EnrolmentDate,Class,StudentSurname,StudentFirstNames,Gender," +
                        "DateBirth,Age,Nationality,LastSchoolAttended,LastSchoolResult,PassPercentage,StudentEmail,StudentPhoneNumber," +
                        "HomeLanguage,Religion,MedicalAllergies,StudentSibling,SiblingsNo,DocumentSubmitted,FatherSurname,FatherNames," +
                        "FatherContactNo,FatherEmail,NotificationNo,NotificationEmail,MotherSurname,MotherNames,MotherContactNo,MotherEmail," +
                        "HomeAddress,EmergencyPhoneNo,AbsencePhoneNo,SchoolActivities,SchoolPhotos,SchoolYear,Cycle,TutorName, TutorProfession,FatherProfession,MotherProfession,StudentPicture ) " +
                        "VALUES (@d1,@d2,@d3,@d4,@d5,@d6,@d7,@d8,@d9,@d10,@d11,@d12,@d13,@d14,@d15,@d16,@d17,@d18,@d19,@d20,@d21,@d22,@d23," +
                        "@d24,@d25,@d26,@d27,@d28,@d29,@d30,@d31,@d32,@d33,@d34,@d35,@d36,@d37,@d38,@d39,@d40, @d41, @d42, @d43)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;

                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d2", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d3", System.Data.SqlDbType.Date);
                    cmd.Parameters.Add("@d4", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d5", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d6", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d7", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d8", System.Data.SqlDbType.Date);
                    cmd.Parameters.Add("@d9", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d10", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d11", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d12", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d13", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d14", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d15", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d16", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d17", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d18", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d19", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d20", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d21", System.Data.SqlDbType.TinyInt);
                    cmd.Parameters.Add("@d22", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d23", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d24", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d25", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d26", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d27", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d28", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d29", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d30", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d31", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d32", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d33", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d34", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d35", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d36", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d37", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d38", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d39", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d40", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d41", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d42", System.Data.SqlDbType.VarChar);

                    cmd.Parameters["@d1"].Value = txtStudentNumber.Text.Trim();
                    cmd.Parameters["@d2"].Value = cmbSection.Text;
                    cmd.Parameters["@d3"].Value = dtEnrolmentDate.EditValue;
                    cmd.Parameters["@d4"].Value = cmbClass.Text;
                    cmd.Parameters["@d5"].Value = txtStudentSurname.Text.Trim();
                    cmd.Parameters["@d6"].Value = txtStudentFirstNames.Text.Trim();
                    cmd.Parameters["@d7"].Value = cmbStudentGender.Text;
                    cmd.Parameters["@d8"].Value = dtDateBirth.EditValue;
                    cmd.Parameters["@d9"].Value = txtStudentAge.Text;
                    cmd.Parameters["@d10"].Value = cmbNationality.Text;
                    cmd.Parameters["@d11"].Value = txtLastSchoolAttended.Text.Trim();
                    cmd.Parameters["@d12"].Value = cmbLastSchoolResult.Text; ;
                    cmd.Parameters["@d13"].Value = txtPassPercentage.Text.Trim();
                    cmd.Parameters["@d14"].Value = txtStudentEmail.Text.Trim();
                    cmd.Parameters["@d15"].Value = txtStudentPhoneNumber.Text.Trim();
                    cmd.Parameters["@d16"].Value = cmbHomeLanguage.Text;
                    cmd.Parameters["@d17"].Value = cmbReligion.Text;
                    cmd.Parameters["@d18"].Value = txtMedicalAllergies.Text.Trim();
                    string StudentHasSibling;
                    if (ckStudentSibling.Checked)
                    {
                        StudentHasSibling = "Yes";
                    }
                    else
                    {
                        StudentHasSibling = "No";
                    }
                    cmd.Parameters["@d19"].Value = StudentHasSibling;
                    cmd.Parameters["@d20"].Value = txtSiblingsNo.Text.Trim();

                    cmd.Parameters["@d21"].Value = submittedDocuments;
                    cmd.Parameters["@d22"].Value = txtFatherSurname.Text.Trim();
                    cmd.Parameters["@d23"].Value = txtFatherNames.Text.Trim();
                    cmd.Parameters["@d24"].Value = txtFatherContactNo.Text.Trim();
                    cmd.Parameters["@d25"].Value = txtFatherEmail.Text.Trim();
                    cmd.Parameters["@d26"].Value = txtParentNotificationNo.Text.Trim();
                    cmd.Parameters["@d27"].Value = txtParentNotificationEmail.Text.Trim();
                    cmd.Parameters["@d28"].Value = txtMotherSurname.Text.Trim();
                    cmd.Parameters["@d29"].Value = txtMotherNames.Text.Trim();
                    cmd.Parameters["@d30"].Value = txtMotherContactNo.Text.Trim();
                    cmd.Parameters["@d31"].Value = txtMotherEmail.Text.Trim();
                    cmd.Parameters["@d32"].Value = txtHomeAddress.Text.Trim();

                    cmd.Parameters["@d33"].Value = txtEmergencyPhoneNo.Text.Trim();
                    cmd.Parameters["@d34"].Value = txtAlternativeEmergencyPhoneNo.Text.Trim();
                    string SchoolActivities, SchoolPhotos;
                    if (ckSchoolActivities.Checked)
                    {
                        SchoolActivities = "Yes";
                    }
                    else
                    {
                        SchoolActivities = "No";
                    }
                    cmd.Parameters["@d35"].Value = SchoolActivities;
                    if (ckSchoolPhotos.Checked)
                    {
                        SchoolPhotos = "Yes";
                    }
                    else
                    {
                        SchoolPhotos = "No";
                    }
                    cmd.Parameters["@d36"].Value = SchoolPhotos;
                    cmd.Parameters["@d37"].Value = cmbAcademicYear.Text;
                    cmd.Parameters["@d38"].Value = cmbCycle.Text;
                    cmd.Parameters["@d39"].Value = txtTutorName.Text;
                    cmd.Parameters["@d40"].Value = txtTutorProfession.Text;
                    cmd.Parameters["@d41"].Value = txtFatherProfession.Text;
                    cmd.Parameters["@d42"].Value = txtMotherProfession.Text;
                    // image content
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bmpImage = new Bitmap(pictureStudent.Image);
                        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] data = ms.GetBuffer();
                        SqlParameter p = new SqlParameter("@d43", SqlDbType.Image);
                        p.Value = data;
                        cmd.Parameters.Add(p);
                    }                       

                    cmd.ExecuteNonQuery();
                    con.Close(); 
                }
                    

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(LocRM.GetString("strStudentRecordsSave"), LocRM.GetString("strStudentEnrolment"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                // barStaticItemProcess.Caption = "Student records saved";
                //Log record transaction in logs
                string st = LocRM.GetString("strStudent") + " " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strHasBeenEnrolled") + ". " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //send notification
                //RegistrationUpdateNotification 0= no notification, 1 send SMS, 2= send email, 3 send both
                saveUpdate = 1;
                if (Properties.Settings.Default.RegistrationUpdateNotification == 1)
                {
                    if (txtParentNotificationNo.Text.Trim() != "")
                    {
                        //send SMS
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;                   
                        sendSMS();                    
                    }

                }
                if (Properties.Settings.Default.RegistrationUpdateNotification == 2)
                {
                    if (txtParentNotificationEmail.Text.Trim() != "")
                    {
                        //Send email
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;                      
                        sendEmail();                     
                    }
                }
                if (Properties.Settings.Default.RegistrationUpdateNotification == 3)
                {
                    //Send email & SMS
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                   
                    if (txtParentNotificationNo.Text != "")
                    {
                        sendSMS();
                    }
                    if (txtParentNotificationEmail.Text != "")
                    {
                        sendEmail();
                    }                   
                }
                
                //Clear all controls
                Reset();

                //Disable Controls
                disableLayoutGroups();
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // send SMS
        private void sendSMS()
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
                        
                        // validate PhoneNumber;
                        string cellNo = txtParentNotificationNo.Text;
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

                                            cellNo = (rdr.GetString(4).ToString().Trim()) + cellNo;

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
                            splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSendingSMS"));
                        }
                        
                        SmsGatewayClient smsGateway = new SmsGatewayClient(pf.Decrypt(Properties.Settings.Default.SettingSMSTocken));
                        if (saveUpdate == 1)
                        {                            
                            smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, cellNo, LocRM.GetString("strStudent") + " " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strHasBeenEnrolled") + ". " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text);
                            //Log send SMS transaction in SMS logs
                            string st = LocRM.GetString("strStudentEnrolmentNotification") + ": " + LocRM.GetString("strSurname") + ": " + txtStudentSurname.Text + " " + LocRM.GetString("strName") + ": " + txtStudentFirstNames.Text;
                            pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, cellNo);
                        }
                        if (saveUpdate == 2)
                        {
                            smsGateway.SendSms(Properties.Settings.Default.SettingSMSCompany, cellNo, LocRM.GetString("strStudentRecordsOf") + " " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text + " " + LocRM.GetString("strHasBeenUpdated"));
                            //Log send SMS transaction in SMS logs
                            string st = LocRM.GetString("strStudentEnrolmentUpdateNotification") + ": " + LocRM.GetString("strSurname") + ": " + txtStudentSurname.Text + " " + LocRM.GetString("strName") + ": " + txtStudentFirstNames.Text;
                            pf.LogSMS(Functions.PublicVariables.UserLoggedSurname, st, cellNo);
                        }
                        // saveUpdate = 0;
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
                    XtraMessageBox.Show(LocRM.GetString("strNoInternetToSendSMS"), LocRM.GetString("strSendSMS"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Failed to send SMS due to internet connection transaction in logs 
                    if (saveUpdate == 1)
                    {
                        string st = LocRM.GetString("strNoInternetToSendSMS") + ". " + LocRM.GetString("strNewStudentEnrolled") + ": " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text;
                        // barStaticItemProcess.Caption = "No Internet Connection to send SMS notification";
                        pf.LogFunc(UserLoggedSurname, st);
                    }
                    if (saveUpdate == 2)
                    {
                        string st = LocRM.GetString("strNoInternetToSendSMS") + ". " + LocRM.GetString("strDetailsOf") + " " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text + " " + LocRM.GetString("strHasBeenUpdated");
                        // barStaticItemProcess.Caption = "No Internet Connection to send SMS notification";
                        pf.LogFunc(UserLoggedSurname, st);
                    }
                    //  saveUpdate = 0;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //send Email
        private void sendEmail()
        {
            try
            {
                if (pf.CheckForInternetConnection() == true)
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSendingEmail"));
                    }

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ctn = "select RTRIM(Username),RTRIM(Password),RTRIM(SMTPAddress),(Port),RTRIM(SenderName) from EmailSetting where IsDefault='Yes' and IsActive='Yes'";
                        cmd = new SqlCommand(ctn);
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();
                        if (rdr.Read())
                        {
                            if (saveUpdate == 1)
                            {
                                pf.SendMail(Convert.ToString(rdr.GetValue(0)), txtParentNotificationEmail.Text, LocRM.GetString("strStudent") + " " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strHasBeenEnrolled") + ". " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text, LocRM.GetString("strStudentEnrolment"), Convert.ToString(rdr.GetValue(2)), Convert.ToInt16(rdr.GetValue(3)), Convert.ToString(rdr.GetValue(0)), pf.Decrypt(Convert.ToString(rdr.GetValue(1))), Convert.ToString(rdr.GetValue(4)));
                                //  barStaticItemProcess.Caption = "Email notification sent successfully";
                            }
                            if (saveUpdate == 2)
                            {
                                pf.SendMail(Convert.ToString(rdr.GetValue(0)), txtParentNotificationEmail.Text, LocRM.GetString("strDetailsOf") + " " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text + " " + LocRM.GetString("strHasBeenUpdated"), LocRM.GetString("strStudentEnrolment"), Convert.ToString(rdr.GetValue(2)), Convert.ToInt16(rdr.GetValue(3)), Convert.ToString(rdr.GetValue(0)), pf.Decrypt(Convert.ToString(rdr.GetValue(1))), Convert.ToString(rdr.GetValue(4)));
                                // barStaticItemProcess.Caption = "Email notification sent successfully";
                            }
                            if ((rdr != null))
                            {
                                rdr.Close();
                            }
                            con.Close();
                            // saveUpdate = 0;
                        }


                    }

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                }
                else
                {
                    MessageBox.Show(LocRM.GetString("strNoInternetToSendEmail"), LocRM.GetString("strStudentEnrolment"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Failed to send email due to internet connection transaction in logs
                    if (saveUpdate == 1)
                    {
                        string st = LocRM.GetString("strNoInternetToSendEmail") + ". " + LocRM.GetString("strNewStudentEnrolled") + ": " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text;
                        // barStaticItemProcess.Caption = "No Internet Connection to send Email notification";
                        pf.LogFunc(UserLoggedSurname, st);
                    }
                    if (saveUpdate == 2)
                    {
                        string st = LocRM.GetString("strNoInternetToSendEmail") + ". " + LocRM.GetString("strDetailsOf") + " " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text + " " + LocRM.GetString("strHasBeenUpdated");
                        // barStaticItemProcess.Caption = "No Internet Connection to send Email notification";
                        pf.LogFunc(UserLoggedSurname, st);
                    }
                    //  saveUpdate = 0;
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

        private void txtFatherContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ckFatherNotificationNo.Enabled = true;
        }

        private void txtFatherEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            ckFatherNotificationEmail.Enabled = true;
        }

        private void txtMotherContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            ckMotherNotificationNo.Enabled = true;
        }

        private void txtMotherEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            ckMotherNotificationEmail.Enabled = true;
        }
         int submittedDocuments=0;
        //0 = no document submitted
        //1= only Birth Certificate Submitted
        //4= only Parents IDs Submitted
        //8= only Previous school Docs Submitted            
        //5 = Birth Certificate & Parents IDs Submitted
        //9 = Birth Certificate & Previous school Docs Submitted
        //12= only Parents IDs Submitted + & Previous school Docs Submitted
        //13 = All documents submitted
        private void ckBirthCertificate_CheckedChanged(object sender, EventArgs e)
        {
            if (ckBirthCertificate.Checked)
            {
                submittedDocuments = submittedDocuments+1;
            }
            else
            {
                submittedDocuments = submittedDocuments - 1;
            }
        }

        private void ckParentIdentityDocuments_CheckedChanged(object sender, EventArgs e)
        {
            if (ckParentIdentityDocuments.Checked)
            {
                submittedDocuments = submittedDocuments + 4;
            }
            else
            {
                submittedDocuments = submittedDocuments - 4;
            }
        }

        private void ckPreviousSchoolDocuments_CheckedChanged(object sender, EventArgs e)
        {
            if (ckPreviousSchoolDocuments.Checked)
            {
                submittedDocuments = submittedDocuments + 8;
            }
            else
            {
                submittedDocuments = submittedDocuments - 8;
            }
        }
        Students.frmListStudents listStudents;
        private void btnGetData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            if (listStudents == null)
            {
                listStudents = new frmListStudents(); //Create form if not created
                //Save customer data before frmlistStudents close
                listStudents.FormClosing +=new FormClosingEventHandler(ListStudentsFormClosing);
                listStudents.FormClosed += ListStudents_FormClosed; //Add eventhandler to cleanup after form closes
            }
            
            listStudents.ShowDialog(this);  //Show Form assigning this form as the forms owner          
        }

        private void ListStudents_FormClosed(object sender, FormClosedEventArgs e)
        {
            listStudents = null;  //If form is closed make sure reference is set to null           
            //Show();
        }
        private void ListStudentsFormClosing(object sender, FormClosingEventArgs e)
        {
           txtStudentNumber.Text = studentNumber; 
            if (txtStudentNumber.Text!="")
            {
                fillCycle();
                fillGender();
                fillReligion();
                fillLanguage();
                fillResults();
                fillCountries();
                FillAcademicYear();
                FillClass();
                cmbClass.Enabled = true;
                FillSection();
                populateForm();
            }
        }
        private void populateForm()
        {            
           try
          {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                clear();                
                barCodeControlStudentNumber.Text = "";
                txtStudentNumber.Text = txtStudentNumber.Text.TrimEnd();
                barcode();

                //populate contols

                //  FillAcademicYear();


                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = con.CreateCommand();

                    cmd.CommandText = "SELECT * FROM Students WHERE StudentNumber = '" + txtStudentNumber.Text + "'";
                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        txtStudentNumber.Text = (rdr.GetString(1).Trim());
                        cmbCycle.Text = (rdr.GetString(38).Trim());
                        cmbSection.Text = (rdr.GetString(2).Trim());
                        string sectionText = (rdr.GetString(2).Trim());
                        if (sectionText == "")
                        {
                            cmbSection.Enabled = false;
                            cmbSection.Properties.ReadOnly = true;
                        }
                        else
                        {
                            cmbSection.Enabled = true;
                            cmbSection.Properties.ReadOnly = false;
                            cmbSection.Text = sectionText;
                        }

                        cmbClass.Text = (rdr.GetString(4).Trim());
                        txtStudentSurname.Text = (rdr.GetString(5).Trim());
                        txtStudentFirstNames.Text = (rdr.GetString(6).Trim());
                        dtEnrolmentDate.EditValue = (rdr.GetValue(3));
                        dtDateBirth.EditValue = (rdr.GetValue(8));
                        txtStudentAge.Text = (rdr.GetString(9).Trim());
                        cmbStudentGender.Text = (rdr.GetString(7).Trim());

                        cmbNationality.Text = (rdr.GetString(10).Trim());
                        txtLastSchoolAttended.Text = (rdr.GetString(11).Trim());
                        cmbLastSchoolResult.Text = (rdr.GetString(12).Trim());

                        txtPassPercentage.Text = (rdr.GetString(13).Trim());
                        txtStudentEmail.Text = (rdr.GetString(14).Trim());
                        txtStudentPhoneNumber.Text = (rdr.GetString(15).Trim());
                        cmbHomeLanguage.Text = (rdr.GetString(16).Trim());
                        cmbReligion.Text = (rdr.GetString(17).Trim());
                        txtMedicalAllergies.Text = (rdr.GetString(18).Trim());

                        string siblings = (rdr.GetString(19).Trim());
                        if (siblings == "Yes")
                        {
                            ckStudentSibling.Checked = true;
                        }
                        else
                        {
                            ckStudentSibling.Checked = false;
                        }

                        txtSiblingsNo.Text = Convert.ToString((rdr.GetValue(20)));
                        string submittedDocuments;
                        //0 = no document submitted
                        //1= only Birth Certificate Submitted
                        //4= only Parents IDs Submitted
                        //8= only Previous school Docs Submitted            
                        //5 = Birth Certificate & Parents IDs Submitted
                        //9 = Birth Certificate & Previous school Docs Submitted
                        //12= only Parents IDs Submitted + & Previous school Docs Submitted
                        //13 = All documents submitted
                        submittedDocuments = Convert.ToString((rdr.GetValue(21)));
                        if (submittedDocuments == "0")
                        {
                            ckBirthCertificate.Checked = false;
                            ckParentIdentityDocuments.Checked = false;
                            ckPreviousSchoolDocuments.Checked = false;
                        }
                        if (submittedDocuments == "1")
                        {
                            ckBirthCertificate.Checked = true;
                            ckParentIdentityDocuments.Checked = false;
                            ckPreviousSchoolDocuments.Checked = false;
                        }
                        if (submittedDocuments == "4")
                        {
                            ckBirthCertificate.Checked = false;
                            ckParentIdentityDocuments.Checked = true;
                            ckPreviousSchoolDocuments.Checked = false;
                        }
                        if (submittedDocuments == "5")
                        {
                            ckBirthCertificate.Checked = true;
                            ckParentIdentityDocuments.Checked = true;
                            ckPreviousSchoolDocuments.Checked = false;
                        }
                        if (submittedDocuments == "8")
                        {
                            ckBirthCertificate.Checked = false;
                            ckParentIdentityDocuments.Checked = false;
                            ckPreviousSchoolDocuments.Checked = true;
                        }
                        if (submittedDocuments == "9")
                        {
                            ckBirthCertificate.Checked = true;
                            ckParentIdentityDocuments.Checked = false;
                            ckPreviousSchoolDocuments.Checked = true;
                        }
                        if (submittedDocuments == "12")
                        {
                            ckBirthCertificate.Checked = false;
                            ckParentIdentityDocuments.Checked = true;
                            ckPreviousSchoolDocuments.Checked = true;
                        }
                        if (submittedDocuments == "13")
                        {
                            ckBirthCertificate.Checked = true;
                            ckParentIdentityDocuments.Checked = true;
                            ckPreviousSchoolDocuments.Checked = true;
                        }
                        txtFatherSurname.Text = (rdr.GetString(22).Trim());
                        txtFatherNames.Text = (rdr.GetString(23).Trim());
                        txtFatherContactNo.Text = (rdr.GetString(24).Trim());
                        txtFatherEmail.Text = (rdr.GetString(25).Trim());
                        txtParentNotificationNo.Text = (rdr.GetString(26).Trim());
                        txtParentNotificationEmail.Text = (rdr.GetString(27).Trim());
                        txtMotherSurname.Text = (rdr.GetString(28).Trim());
                        txtMotherNames.Text = (rdr.GetString(29).Trim());
                        txtMotherContactNo.Text = (rdr.GetString(30).Trim());
                        txtMotherEmail.Text = (rdr.GetString(31).Trim());
                        txtHomeAddress.Text = (rdr.GetString(32).Trim());

                        txtEmergencyPhoneNo.Text = (rdr.GetString(33).Trim());
                        txtAlternativeEmergencyPhoneNo.Text = (rdr.GetString(34).Trim());

                        string SchoolActivities = (rdr.GetString(35).Trim());
                        if (SchoolActivities == "Yes")
                        {
                            ckSchoolActivities.Checked = true;
                        }
                        else
                        {
                            ckSchoolActivities.Checked = false;
                        }
                        string SchoolPhotos = (rdr.GetString(36).Trim());
                        if (SchoolPhotos == "Yes")
                        {
                            ckSchoolPhotos.Checked = true;
                        }
                        else
                        {
                            ckSchoolPhotos.Checked = false;
                        }
                        cmbAcademicYear.Text = (rdr.GetString(37).Trim());
                        txtTutorName.Text = (rdr.GetString(39).Trim());
                        txtTutorProfession.Text = (rdr.GetString(40).Trim());
                        txtFatherProfession.Text = (rdr.GetString(41).Trim());
                        txtMotherProfession.Text = (rdr.GetString(42).Trim());

                        if (!Convert.IsDBNull(rdr["StudentPicture"]))
                        {
                            byte[] x = (byte[])rdr["StudentPicture"];
                            MemoryStream ms = new MemoryStream(x);
                            pictureStudent.Image = Image.FromStream(ms);
                            pictureStudent.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
                        }
                        else
                        {
                            pictureStudent.EditValue = null;
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


                //if ((Role == 1) || (Role == 2))//Administrator, Administrator Assistant only
                //enable controls and buttons
                enableLayoutGroups();
                // if (UserType == "Administrator")
                if ((Role == 1) || (Role == 2)) //Administrator, Administrator Assistant only
                {
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else if ((Role == 4) || (Role == 6) || (Role == 7))//4 Account & Admission, 6 Account, Admission & HR,  7 Admission Officer
                {
                    btnUpdate.Enabled = true;
                }
                else
                {
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                }                

                btnNew.Enabled = true;
                btnSave.Enabled = false;
                

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

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbCycle.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectCycle"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbCycle.Focus();
                return;
            }
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            if (txtStudentSurname.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterSurname"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentSurname.Focus();
                return;
            }
            if (txtStudentFirstNames.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtStudentFirstNames.Focus();
                return;
            }
            if (cmbStudentGender.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectGender"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbStudentGender.Focus();
                return;
            }
            if (txtStudentAge.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectDateBirth"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtDateBirth.Focus();
                return;
            }
            if (pictureStudent.EditValue == null)
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPhoto"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnWebcam.Focus();
                return;
            }
            if (txtEmergencyPhoneNo.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterPhoneNoEmergency"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmergencyPhoneNo.Focus();
                return;
            }
            if (txtAlternativeEmergencyPhoneNo.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterPhoneNoAbsence"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtAlternativeEmergencyPhoneNo.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strUpdating"));
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();

                    string cb = "update Students set Section=@d2,EnrolmentDate=@d3,Class=@d4,StudentSurname=@d5,StudentFirstNames=@d6," +
                        "Gender=@d7,DateBirth=@d8,Age=@d9,Nationality=@d10,LastSchoolAttended=@d11,LastSchoolResult=@d12,PassPercentage=@d13," +
                        "StudentEmail=@d14,StudentPhoneNumber=@d15,HomeLanguage=@d16,Religion=@d17,MedicalAllergies=@d18,StudentSibling=@d19," +
                        "SiblingsNo=@d20,DocumentSubmitted=@d21,FatherSurname=@d22,FatherNames=@d23,FatherContactNo=@d24,FatherEmail=@d25," +
                        "NotificationNo=@d26,NotificationEmail=@d27,MotherSurname=@d28,MotherNames=@d29,MotherContactNo=@d30," +
                        "MotherEmail=@d31,HomeAddress=@d32,EmergencyPhoneNo=@d33,AbsencePhoneNo=@d34,SchoolActivities=@d35," +
                        "SchoolPhotos=@d36,SchoolYear=@d37,Cycle=@d38, TutorName=@d39, TutorProfession=@d40, FatherProfession=@d41, MotherProfession=@d42, StudentPicture=@d43  where StudentNumber=@d1";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;

                    cmd.Parameters.Add("@d1", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d2", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d3", System.Data.SqlDbType.Date);
                    cmd.Parameters.Add("@d4", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d5", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d6", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d7", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d8", System.Data.SqlDbType.Date);
                    cmd.Parameters.Add("@d9", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d10", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d11", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d12", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d13", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d14", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d15", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d16", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d17", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d18", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d19", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d20", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d21", System.Data.SqlDbType.TinyInt);
                    cmd.Parameters.Add("@d22", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d23", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d24", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d25", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d26", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d27", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d28", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d29", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d30", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d31", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d32", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d33", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d34", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d35", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d36", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d37", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d38", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d39", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d40", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d41", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@d42", System.Data.SqlDbType.VarChar);

                    cmd.Parameters["@d1"].Value = txtStudentNumber.Text.Trim();
                    cmd.Parameters["@d2"].Value = cmbSection.Text;
                    cmd.Parameters["@d3"].Value = dtEnrolmentDate.EditValue;
                    cmd.Parameters["@d4"].Value = cmbClass.Text;
                    cmd.Parameters["@d5"].Value = txtStudentSurname.Text;
                    cmd.Parameters["@d6"].Value = txtStudentFirstNames.Text;
                    cmd.Parameters["@d7"].Value = cmbStudentGender.Text;
                    cmd.Parameters["@d8"].Value = dtDateBirth.EditValue;
                    cmd.Parameters["@d9"].Value = txtStudentAge.Text;
                    cmd.Parameters["@d10"].Value = cmbNationality.Text;
                    cmd.Parameters["@d11"].Value = txtLastSchoolAttended.Text;
                    cmd.Parameters["@d12"].Value = cmbLastSchoolResult.Text; ;
                    cmd.Parameters["@d13"].Value = txtPassPercentage.Text;
                    cmd.Parameters["@d14"].Value = txtStudentEmail.Text;
                    cmd.Parameters["@d15"].Value = txtStudentPhoneNumber.Text;
                    cmd.Parameters["@d16"].Value = cmbHomeLanguage.Text;
                    cmd.Parameters["@d17"].Value = cmbReligion.Text;
                    cmd.Parameters["@d18"].Value = txtMedicalAllergies.Text;
                    string StudentHasSibling;
                    if (ckStudentSibling.Checked)
                    {
                        StudentHasSibling = "Yes";
                    }
                    else
                    {
                        StudentHasSibling = "No";
                    }
                    cmd.Parameters["@d19"].Value = StudentHasSibling;
                    cmd.Parameters["@d20"].Value = txtSiblingsNo.Text;

                    cmd.Parameters["@d21"].Value = submittedDocuments;
                    cmd.Parameters["@d22"].Value = txtFatherSurname.Text;
                    cmd.Parameters["@d23"].Value = txtFatherNames.Text;
                    cmd.Parameters["@d24"].Value = txtFatherContactNo.Text;
                    cmd.Parameters["@d25"].Value = txtFatherEmail.Text;
                    cmd.Parameters["@d26"].Value = txtParentNotificationNo.Text;
                    cmd.Parameters["@d27"].Value = txtParentNotificationEmail.Text;
                    cmd.Parameters["@d28"].Value = txtMotherSurname.Text;
                    cmd.Parameters["@d29"].Value = txtMotherNames.Text;
                    cmd.Parameters["@d30"].Value = txtMotherContactNo.Text;
                    cmd.Parameters["@d31"].Value = txtMotherEmail.Text;
                    cmd.Parameters["@d32"].Value = txtHomeAddress.Text;

                    cmd.Parameters["@d33"].Value = txtEmergencyPhoneNo.Text;
                    cmd.Parameters["@d34"].Value = txtAlternativeEmergencyPhoneNo.Text;
                    string SchoolActivities, SchoolPhotos;
                    if (ckSchoolActivities.Checked)
                    {
                        SchoolActivities = "Yes";
                    }
                    else
                    {
                        SchoolActivities = "No";
                    }
                    cmd.Parameters["@d35"].Value = SchoolActivities;
                    if (ckSchoolPhotos.Checked)
                    {
                        SchoolPhotos = "Yes";
                    }
                    else
                    {
                        SchoolPhotos = "No";
                    }
                    cmd.Parameters["@d36"].Value = SchoolPhotos;
                    cmd.Parameters["@d37"].Value = cmbAcademicYear.Text;
                    cmd.Parameters["@d38"].Value = cmbCycle.Text;
                    cmd.Parameters["@d39"].Value = txtTutorName.Text;
                    cmd.Parameters["@d40"].Value = txtTutorProfession.Text;
                    cmd.Parameters["@d41"].Value = txtFatherProfession.Text;
                    cmd.Parameters["@d42"].Value = txtMotherProfession.Text;

                    // image content
                    using (MemoryStream ms = new MemoryStream())
                    {
                        Bitmap bmpImage = new Bitmap(pictureStudent.Image);
                        bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        byte[] data = ms.GetBuffer();
                        SqlParameter p = new SqlParameter("@d43", SqlDbType.Image);
                        p.Value = data;
                        cmd.Parameters.Add(p);
                    }                        

                    cmd.ExecuteNonQuery();

                    con.Close();
                }
                 

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show( LocRM.GetString("strStudentRecordsUpdated"), LocRM.GetString("strStudentEnrolment") , MessageBoxButtons.OK, MessageBoxIcon.Information);

                //Log transaction in logs
                string st = LocRM.GetString("strStudentRecordsOf")  +" " + txtStudentSurname.Text + " " + txtStudentFirstNames.Text + " " + LocRM.GetString("strStudentNo") + ": " + txtStudentNumber.Text + " " + LocRM.GetString("strHasBeenUpdated") ;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //send notification
                saveUpdate = 2;
                if (Properties.Settings.Default.RegistrationUpdateNotification == 1)
                {
                    if (txtParentNotificationNo.Text != "")
                    {
                        //send SMS
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                     
                        sendSMS();                     
                    }

                }
                if (Properties.Settings.Default.RegistrationUpdateNotification == 2)
                {
                    if (txtParentNotificationEmail.Text != "")
                    {
                        //Send email
                        Cursor = Cursors.WaitCursor;
                        timer1.Enabled = true;
                     
                        sendEmail();                      
                    }
                }
                if (Properties.Settings.Default.RegistrationUpdateNotification == 3)
                {
                    //Send email & SMS
                    Cursor = Cursors.WaitCursor;
                    timer1.Enabled = true;
                  
                    if (txtParentNotificationNo.Text != "")
                    {
                        sendSMS();
                    }
                    if (txtParentNotificationEmail.Text != "")
                    {
                        sendEmail();
                    }
                  
                }

                //Clear all controls
                Reset();
                //Disable Controls
                disableLayoutGroups();
            }
            catch (Exception ex)
            {
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                Cursor = Cursors.Default;
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (XtraMessageBox.Show(LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                delete_records();
                //Disable Controls
                disableLayoutGroups();
            }
        }
        private void delete_records()
        {
             Cursor = Cursors.WaitCursor;
             timer1.Enabled = true;
            
            try
            {
                
                int RowsAffected = 0;
                using (con = new SqlConnection(databaseConnectionString))
                {
                    if (splashScreenManager1.IsSplashFormVisible == false)
                    {
                        splashScreenManager1.ShowWaitForm();
                        splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strDeleting"));
                    }

                    con.Open();
                    string cq = "delete from Students where StudentNumber='" + txtStudentNumber.Text.Trim() + "'";
                    cmd = new SqlCommand(cq);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();

                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm();
                    }
                    if (RowsAffected > 0)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strStudentRecords"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log send SMS transaction in logs

                        string st = LocRM.GetString("strStudentRecords") + ": " + ("strStudentNo") + ": " + txtStudentNumber.Text + ", " + LocRM.GetString("strSurname") + ": " + txtStudentSurname.Text + ", " + LocRM.GetString("strName") + ": " + txtStudentFirstNames.Text + " " + LocRM.GetString("strHasBeenDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                        Reset();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strStudentRecords"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Reset();
                    }
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
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

        private void txtStudentEmail_Validating(object sender, CancelEventArgs e)
        {
            Regex rEMail = new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtStudentEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtStudentEmail.Text))
                {
                    XtraMessageBox.Show(LocRM.GetString("strInvalidEmailAddress"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtStudentEmail.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void txtFatherEmail_Validating(object sender, CancelEventArgs e)
        {
            Regex rEMail = new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtFatherEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtFatherEmail.Text))
                {
                    XtraMessageBox.Show(LocRM.GetString("strInvalidEmailAddress"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtFatherEmail.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void txtMotherEmail_Validating(object sender, CancelEventArgs e)
        {
            Regex rEMail = new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (txtMotherEmail.Text.Length > 0)
            {
                if (!rEMail.IsMatch(txtMotherEmail.Text))
                {
                    XtraMessageBox.Show(LocRM.GetString("strInvalidEmailAddress"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMotherEmail.SelectAll();
                    e.Cancel = true;
                }
            }
        }

        private void userControlStudentEnrolmentForm_VisibleChanged(object sender, EventArgs e)
        {
           if (this.Visible==true)
            {
                Reset();           
            }          
        }

        private void cmbCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbSection.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            if (cmbCycle.SelectedIndex >= 3 )
            {
                cmbClass.Properties.Items.Clear();
                cmbSection.Enabled = true;
                cmbSection.Properties.ReadOnly = false;
                FillSection();                
            }
            else
            {
                cmbClass.Enabled = true;
                FillClass();
                cmbSection.Enabled = false;
                cmbSection.Properties.ReadOnly = true;
                cmbSection.Properties.Items.Clear();
            }
        }

        private void cmbSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbClass.Enabled = true;
            FillClass();
        }

        private void userControlStudentEnrolmentForm_Load(object sender, EventArgs e)
        {
            //// Display Tutor info when in DRC only
            //if (Properties.Settings.Default.Country == LocRM.GetString("strDRC"))
            //{
            //    layoutControlTutorProfession.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //    layoutControlTutorName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            //}
            //else
            //{
            //    layoutControlTutorProfession.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    layoutControlTutorName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //}
        }
    }
}
