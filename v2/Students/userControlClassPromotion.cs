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
using static EduXpress.Functions.PublicVariables;
using System.Resources;
using DevExpress.XtraBars.Ribbon;

namespace EduXpress.Students
{
    public partial class userControlClassPromotion : DevExpress.XtraEditors.XtraUserControl, Functions.IMergeRibbons
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlClassPromotion).Assembly);

        //create global methods of ribons and status bar to merge when in main.
        public RibbonControl MainRibbon { get; set; }
        public RibbonStatusBar MainStatusBar { get; set; }
        public userControlClassPromotion()
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

        private void userControlClassPromotion_Load(object sender, EventArgs e)
        {
            FillAcademicYear();
            fillListView();
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
                    comboAcademicYear.Properties.Items.Clear();
                    comboNewAcademicYear.Properties.Items.Clear();
                    foreach (DataRow drow in dtable.Rows)
                    {
                        comboAcademicYear.Properties.Items.Add(drow[0].ToString());
                        comboNewAcademicYear.Properties.Items.Add(drow[0].ToString());
                    }
                    comboAcademicYear.SelectedIndex = -1;
                    comboNewAcademicYear.SelectedIndex = -1;
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

        private void comboCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboClass.Properties.Items.Clear();
                comboClass.Enabled = true;
                FillClass();
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
                    cmd = new SqlCommand("SELECT distinct RTRIM(Class) FROM Students where Cycle = @d1", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 60, " Cycle").Value = comboCycle.Text.Trim();

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];

                    comboClass.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        comboClass.Properties.Items.Add(drow[0].ToString());
                    }
                    comboClass.SelectedIndex = -1;
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

        private void comboAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                comboClass.Properties.Items.Clear();
                comboClass.SelectedIndex = -1;
                comboCycle.Properties.Items.Clear();
                comboCycle.Enabled = true;
                FillCycle();
                listViewStudents.Items.Clear();
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

        //Fill Cycle
        private void FillCycle()
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
                    cmd = new SqlCommand("SELECT distinct RTRIM(Cycle) FROM Students where SchoolYear = @d1", con);
                    cmd.Parameters.Add("@d1", SqlDbType.NChar, 30, " SchoolYear").Value = comboAcademicYear.Text.Trim();

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet("ds");
                    DataTable dtable;
                    myDA.Fill(myDataSet);
                    dtable = myDataSet.Tables[0];

                    comboCycle.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        comboCycle.Properties.Items.Add(drow[0].ToString());
                    }
                    comboCycle.SelectedIndex = -1;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            if (comboAcademicYear.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboAcademicYear.Focus();
                return;
            }
            if (comboCycle.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectCycle"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboCycle.Focus();
                return;
            }
            if (comboClass.Text == "")
            {
                XtraMessageBox.Show( LocRM.GetString("strSelectClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboClass.Focus();
                return;
            }
            btnUpdate.Enabled = true;

            using (con = new SqlConnection(databaseConnectionString))
            {
                con.Open();
                cmd = new SqlCommand("select StudentNumber,StudentSurname,StudentFirstNames from Students where SchoolYear=@d1 and Cycle=@d2 and Class=@d3 order by StudentSurname", con);
                cmd.Parameters.AddWithValue("@d1", comboAcademicYear.Text.Trim());
                cmd.Parameters.AddWithValue("@d2", comboCycle.Text.Trim());
                cmd.Parameters.AddWithValue("@d3", comboClass.Text.Trim());
                rdr = cmd.ExecuteReader();
                listViewStudents.Items.Clear();
                while (rdr.Read())
                {
                    string[] arr = new string[4];
                    ListViewItem itm;
                    //add items to ListView
                    arr[0] = rdr[0].ToString().Trim();
                    arr[1] = rdr[1].ToString().Trim();
                    arr[2] = rdr[2].ToString().Trim();
                    itm = new ListViewItem(arr);
                    listViewStudents.Items.Add(itm);
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

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void comboNewAcademicYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboNewAcademicYear.SelectedIndex!=-1)
            {
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }
        Students.frmListClasses listClasses;
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (listClasses == null)
            {
                listClasses = new frmListClasses(); //Create form if not created
                //Save list  data before frmListClasses close
                listClasses.FormClosing += new FormClosingEventHandler(ListClassesFormClosing);
                listClasses.FormClosed += ListClasses_FormClosed;//Add eventhandler to cleanup after form closes  
            }
            listClasses.ShowDialog(this);  //Show Form assigning this form as the forms owner 
        }

        private void ListClasses_FormClosed(object sender, FormClosedEventArgs e)
        {
            listClasses = null;  //If form is closed make sure reference is set to null 
        }
        private void ListClassesFormClosing(object sender, FormClosingEventArgs e)
        {
            txtNewCycle.Text = educationCycle;
            txtNewClass.Text = className;
            txtSection.Text = section;
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Reset();
            
        }
        private void fillListView()
        {
            var _with1 = listViewStudents;
            _with1.Clear();
            _with1.Columns.Add(LocRM.GetString("strStudentNo").ToUpper(), 200, HorizontalAlignment.Left);
            _with1.Columns.Add(LocRM.GetString("strSurname").ToUpper(), 250, HorizontalAlignment.Left);
            _with1.Columns.Add(LocRM.GetString("strName").ToUpper(), 250, HorizontalAlignment.Left);
        }
        public void Reset()
        {
            comboAcademicYear.SelectedIndex = -1;
            comboCycle.SelectedIndex = -1;
            comboClass.SelectedIndex = -1;
            comboNewAcademicYear.SelectedIndex = -1;
            txtNewCycle.Text = "";
            txtNewClass.Text = "";
            txtSection.Text = "";
            listViewStudents.Items.Clear();
            comboCycle.Enabled = false;
            comboClass.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (comboNewAcademicYear.Text == "")
                {
                    XtraMessageBox.Show( LocRM.GetString("strSelectTransferredSchoolYear"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    comboAcademicYear.Focus();
                    return;
                }
                if (txtNewClass.Text == "")
                {
                    XtraMessageBox.Show( LocRM.GetString("StrSelectNewClass"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSelect.Focus();
                    return;
                }
                if (listViewStudents.Items.Count == 0)
                {
                    XtraMessageBox.Show( LocRM.GetString("strNoStudentSelected"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                for (int i = listViewStudents.Items.Count - 1; i >= 0; i += -1)
                {
                    if (listViewStudents.Items[i].Checked == true)
                    {
                        using (con = new SqlConnection(databaseConnectionString))
                        {
                            string cd = "update students set Section=@d1,Class=@d2, Cycle=@d3,SchoolYear=@d4 where StudentNumber=@d5";
                            cmd = new SqlCommand(cd);
                            cmd.Parameters.AddWithValue("@d1", txtSection.Text.Trim());
                            cmd.Parameters.AddWithValue("@d2", txtNewClass.Text.Trim());
                            cmd.Parameters.AddWithValue("@d3", txtNewCycle.Text.Trim());
                            cmd.Parameters.AddWithValue("@d4", comboNewAcademicYear.Text.Trim());
                            cmd.Parameters.AddWithValue("@d5", listViewStudents.Items[i].SubItems[0].Text);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                            
                    }
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strStudentRecords"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset(); 
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

        private void userControlClassPromotion_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                Reset();
                btnNew.Enabled = true;
            }
        }
    }
     
}
