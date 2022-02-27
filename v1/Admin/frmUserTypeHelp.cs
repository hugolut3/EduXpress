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

namespace EduXpress.Admin
{
    public partial class frmUserTypeHelp : DevExpress.XtraEditors.XtraForm
    {
        public frmUserTypeHelp()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

       
    }
}