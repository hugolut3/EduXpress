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
using DevExpress.XtraReports.UI;
using DevExpress.XtraBars.Ribbon;

namespace EduXpress.Education
{
    public partial class userControlBulletins : DevExpress.XtraEditors.XtraUserControl, Functions.IMergeRibbons
    {
        //create global methods of ribons and status bar to merge when in main.
        //add the ImergeRibbons interface.
        public RibbonControl MainRibbon { get; set; }
        public RibbonStatusBar MainStatusBar { get; set; }
        public userControlBulletins()
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Education.BulletinsRDC.reportBulletinLIT_1_2 report = new Education.BulletinsRDC.reportBulletinLIT_1_2();
            //report.DisplayName = LocRM.GetString("strStudentEnrolmentForm");


            report.PrintingSystem.Document.AutoFitToPagesWidth = 1; //force to print on  page width
            report.PaperKind = System.Drawing.Printing.PaperKind.A4;
            report.Landscape = false;

            // Invoke a Print Preview for the created report document. 
            ReportPrintTool printTool = new ReportPrintTool(report);
            printTool.ShowRibbonPreviewDialog();
        }
    }
}
