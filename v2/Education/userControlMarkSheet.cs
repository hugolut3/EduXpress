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
using DevExpress.XtraBars.Ribbon;

namespace EduXpress.Education
{
    public partial class userControlMarkSheet : DevExpress.XtraEditors.XtraUserControl, Functions.IMergeRibbons
    {
        //create global methods of ribons and status bar to merge when in main.
        //add the ImergeRibbons interface.
        public RibbonControl MainRibbon { get; set; }
        public RibbonStatusBar MainStatusBar { get; set; }
        public userControlMarkSheet()
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

        private void btnAssignSubjectsClasses_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmAssignSubjectsClasses frm = new frmAssignSubjectsClasses();
            frm.ShowDialog();
        }
    }
}
