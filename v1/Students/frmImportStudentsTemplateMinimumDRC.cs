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
using DevExpress.Spreadsheet;

namespace EduXpress.Students
{
    public partial class frmImportStudentsTemplateMinimumDRC : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmImportStudentsTemplateMinimumDRC()
        {
            InitializeComponent();
        }

        private void frmImportStudentsTemplateMinimumDRC_Load(object sender, EventArgs e)
        {
            IWorkbook workbook = spreadsheetControl1.Document;
            // Load the template document into the SpreadsheetControl. 
            //Load the  template based on language
            string templatePath = Application.StartupPath + @"\Students\ImportStudentsTemplates";
            
                workbook.LoadDocument(templatePath + @"\Modèle importer les élèves.xls");
                //workbook.LoadDocument(@"Reports\Template\EduXpressData_fr.xlsx");
            
            Worksheet sheet = workbook.Worksheets[0];
        }
    }
}