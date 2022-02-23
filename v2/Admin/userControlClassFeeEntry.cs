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
using System.Resources;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;

namespace EduXpress.Admin
{
    public partial class userControlClassFeeEntry : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlClassFeeEntry).Assembly);

        public userControlClassFeeEntry()
        {
            InitializeComponent();
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            reset();
            btnNew.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            btnSave.Enabled = true;
            FillClass();
            cmbClass.Enabled = true;
            checkFeeStructure();
        }
        private void disable()
        {
            cmbClass.Enabled = false;
            cmbFeeName.Enabled = false;
            cmbMonth.Enabled = false;
            txtFee.Enabled = false;
        }
        private void clear()
        {
            cmbClass.SelectedIndex = -1;
            cmbFeeName.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            txtID.Text = "";
            txtFee.Text = "";
            cmbClass.Focus();
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
        }
        private void reset()
        {
            cmbClass.SelectedIndex = -1;
            cmbFeeName.SelectedIndex = -1;
            cmbMonth.SelectedIndex = -1;
            txtID.Text = "";
            txtFee.Text = "";
            cmbClass.Focus();
            btnNew.Enabled = true;
            btnSave.Enabled = false;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;
            gridControlFees.DataSource = Getdata();
        }
        //Fill cmbClass with School Classes
        private void FillClass()
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
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(ClassName) FROM Classes", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
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

        private void btnNewFee_Click(object sender, EventArgs e)
        {
            frmListFees frm = new frmListFees();
            frm.ShowDialog();
            FillFees();
            gridControlFees.DataSource = Getdata();
        }
        private void FillFees()
        {
            SqlDataAdapter adp;
            DataSet ds;
            DataTable dtable;
            try
            {
                // splashScreenManager1.ShowWaitForm();
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    adp = new SqlDataAdapter();
                    adp.SelectCommand = new SqlCommand("SELECT distinct RTRIM(FeeName) FROM Fee", con);
                    ds = new DataSet("ds");
                    adp.Fill(ds);
                    dtable = ds.Tables[0];
                    cmbFeeName.Properties.Items.Clear();

                    foreach (DataRow drow in dtable.Rows)
                    {
                        cmbFeeName.Properties.Items.Add(drow[0].ToString());
                    }
                    cmbFeeName.SelectedIndex = -1;
                    con.Close();
                    // con.Dispose();
                }

                //  splashScreenManager1.CloseWaitForm();
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

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFeeName.Enabled = true;
            FillFees();
        }

        private void cmbFeeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth.Enabled = true;
            fillMonth();
            txtFee.Enabled = true;
        }
        //ckeck school fees structure
        private void checkFeeStructure()
        {
            try
            {
                //Check if fees structure has data in database

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select feeStructurePerMonth from CourseFee ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        if (rdr.Read())
                        {
                            feeStructurePerMonth = rdr.GetBoolean(0);
                            if (feeStructurePerMonth == true)
                            {
                                radioPerMonth.Checked = true;
                            }
                            else
                            {
                                radioQuarterly.Checked = true;
                            }                          

                        }
                    }
                    else
                    {
                        feeStructurePerMonth = false;//false = Quaterly. Start default fees structure to Quaterly 
                        radioQuarterly.Checked = true;
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
        //Fill cmbMonth
        private void fillMonth()
        {
            if(radioPerMonth.Checked==true)
            {
                cmbMonth.Properties.Items.Clear();
                cmbMonth.Properties.Items.AddRange(new object[] { LocRM.GetString("strJanuary"),
                LocRM.GetString("strFebruary") , LocRM.GetString("strMarch")
            , LocRM.GetString("strApril"), LocRM.GetString("strMay")
            , LocRM.GetString("strJune"), LocRM.GetString("strJuly")
            , LocRM.GetString("strAugust"), LocRM.GetString("strSeptember")
            , LocRM.GetString("strOctober"), LocRM.GetString("strNovember")
            , LocRM.GetString("strDecember")});
            }
            else
            {
                cmbMonth.Properties.Items.Clear();
                cmbMonth.Properties.Items.AddRange(new object[] { LocRM.GetString("str1stQuarter"),
                LocRM.GetString("str2ndQuarter") , LocRM.GetString("str3rdQuarter")});
            }
            
        }

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClassName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            if (cmbFeeName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectFeeName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbFeeName.Focus();
                return;
            }
            if (cmbMonth.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMonth.Focus();
                return;
            }
            if (txtFee.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterFee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFee.Focus();
                return;
            }

            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select CourseFee.ClassName,FeeName,Month from CourseFee where ClassName=@d1 and FeeName=@d2 and Month=@d3 ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbClass.Text);
                    cmd.Parameters.AddWithValue("@d2", cmbFeeName.Text);
                    cmd.Parameters.AddWithValue("@d3", cmbMonth.Text);
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        XtraMessageBox.Show(LocRM.GetString("strRecordExists"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // cmbClass.Text = "";
                        cmbClass.Focus();

                        if ((rdr != null))
                        {
                            rdr.Close();
                        }
                        return;
                    }
                }
                   

                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    int monthNo = cmbMonth.SelectedIndex + 1;
                    con.Open();

                    string cb = "insert into CourseFee(ClassName,FeeName,Month,Fee,MonthNo,feeStructurePerMonth) VALUES (@d1,@d2,@d3,@d4,@d5,@d6)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbClass.Text);
                    cmd.Parameters.AddWithValue("@d2", cmbFeeName.Text);
                    cmd.Parameters.AddWithValue("@d3", cmbMonth.Text);
                    cmd.Parameters.AddWithValue("@d4", decimal.Parse( txtFee.Text,CultureInfo.CurrentCulture));
                    cmd.Parameters.AddWithValue("@d5", monthNo); 
                    cmd.Parameters.AddWithValue("@d6", feeStructurePerMonth);
                    cmd.ExecuteReader();

                    con.Close();
                }
                    
                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }
                XtraMessageBox.Show(LocRM.GetString("strNewClassFeeSaved"), LocRM.GetString("strSchoolFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strNewClassFee") + ": " + cmbFeeName.Text + " " + LocRM.GetString("strforClass") + " " + cmbClass.Text + " " + LocRM.GetString("strforTheMonth") + " " + cmbMonth.Text + " " + LocRM.GetString("strHasBeenSaved");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //Clear all controls
                reset();
                btnSave.Enabled = false;
                btnNew.Enabled = true;
                cmbClass.Enabled = false;
                cmbFeeName.Enabled = false;
                cmbMonth.Enabled = false;
                txtFee.Enabled = false;
               // gridControlFees.DataSource = Getdata();
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
        //sql connection
        private SqlConnection Connection
        {
            get
            {
                SqlConnection ConnectionToFetch = new SqlConnection(databaseConnectionString);
                ConnectionToFetch.Open();
                return ConnectionToFetch;
            }
        }
        //Fill dataGridViewEmails
        public DataView Getdata()
        {
            string NoColumn = LocRM.GetString("strNo").ToUpper();
            string ClassNameColumn = LocRM.GetString("strClassName").ToUpper();
            string FeeNameColumn = LocRM.GetString("strFeeName").ToUpper();
            string PeriodColumn = LocRM.GetString("strPeriod").ToUpper();
            string FeeColumn = LocRM.GetString("strFee").ToUpper();            

            dynamic SelectQry = "SELECT RTRIM(CourseFeeID) as [" + NoColumn + "],RTRIM(ClassName) as [" + ClassNameColumn + "], " +
                "RTRIM(FeeName) as [" + FeeNameColumn + "], RTRIM(Month) as [" + PeriodColumn + "], " +
                "RTRIM(Fee) as [" + FeeColumn + "] FROM CourseFee order by ClassName";
            DataSet SampleSource = new DataSet();
            DataView TableView = null;
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                SqlCommand SampleCommand = new SqlCommand();
                dynamic SampleDataAdapter = new SqlDataAdapter();
                SampleCommand.CommandText = SelectQry;
                SampleCommand.Connection = Connection;
                SampleDataAdapter.SelectCommand = SampleCommand;
                SampleDataAdapter.Fill(SampleSource);
                TableView = SampleSource.Tables[0].DefaultView;
                if ((Connection.State == ConnectionState.Open))
                {
                    Connection.Close();
                    Connection.Dispose();
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
            return TableView;
        }

        private void userControlClassFeeEntry_Load(object sender, EventArgs e)
        {
            gridControlFees.DataSource = Getdata();
            //hide course ID column
            gridView1.Columns[0].Visible = false;
        }

        private void btnUpdate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (cmbClass.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectClassName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClass.Focus();
                return;
            }
            if (cmbFeeName.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectFeeName"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbFeeName.Focus();
                return;
            }
            if (cmbMonth.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectPeriod"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbMonth.Focus();
                return;
            }
            if (txtFee.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterFee"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtFee.Focus();
                return;
            }

            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strUpdating"));
                }
                using (con = new SqlConnection(databaseConnectionString))
                {
                    int monthNo = cmbMonth.SelectedIndex + 1;
                    con.Open();
                    string ct = "Update CourseFee set ClassName=@d1,FeeName=@d2,Month=@d3,Fee=@d4, MonthNo=@d5, feeStructurePerMonth=@d6 where CourseFeeID=@d7 ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@d1", cmbClass.Text);
                    cmd.Parameters.AddWithValue("@d2", cmbFeeName.Text);
                    cmd.Parameters.AddWithValue("@d3", cmbMonth.Text);
                    cmd.Parameters.AddWithValue("@d4", decimal.Parse(txtFee.Text, CultureInfo.CurrentCulture));                    
                    cmd.Parameters.AddWithValue("@d5", monthNo);
                    cmd.Parameters.AddWithValue("@d6", feeStructurePerMonth);
                    cmd.Parameters.AddWithValue("@d7", txtID.Text);
                    rdr = cmd.ExecuteReader();
                    con.Close();
                }
                    

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(LocRM.GetString("strSuccessfullyUpdated"), LocRM.GetString("strSchoolFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
               string st = LocRM.GetString("strNewClassFee") + ": " + cmbFeeName.Text + " " + LocRM.GetString("strforClass") + " " + cmbClass.Text + " " + LocRM.GetString("strforTheMonth") + " " + cmbMonth.Text + " " + LocRM.GetString("strHasBeenUpdated");
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                //Clear all controls
                reset();
                btnUpdate.Enabled = false;
                btnSave.Enabled = false;
                btnRemove.Enabled = false;
                btnNew.Enabled = true;
                cmbClass.Enabled = false;
                cmbFeeName.Enabled = false;
                cmbMonth.Enabled = false;
                txtFee.Enabled = false;
                gridControlFees.DataSource = Getdata();
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

        private void btnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show(LocRM.GetString("strDeleteConfirmQuestion"), LocRM.GetString("strDeleteConfirm"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                    DeleteRecord();
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnRemove.Enabled = false;
                btnNewFee.Enabled = true;
                cmbClass.Enabled = false;
                cmbFeeName.Enabled = false;
                cmbMonth.Enabled = false;
                txtFee.Enabled = false;
                gridControlFees.DataSource = Getdata();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DeleteRecord()
        {
            try
            {
                int RowsAffected = 0;

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string cq = "delete from CourseFee where CourseFeeID=@d1";
                    cmd = new SqlCommand(cq);
                    cmd.Parameters.AddWithValue("@d1", txtID.Text);
                    cmd.Connection = con;
                    RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSuccessfullyDeleted"), LocRM.GetString("strSchoolFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //Log record transaction in logs
                        string st = LocRM.GetString("strNewClassFee") + ": " + cmbFeeName.Text + " " + LocRM.GetString("strforClass") + " " + cmbClass.Text + " " + LocRM.GetString("strforTheMonth") + " " + cmbMonth.Text + " " + LocRM.GetString("strHasBeenDeleted");
                        pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);                        
                        reset();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strNoRecordFound"), LocRM.GetString("strSchoolFees"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                        reset();
                    }
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
                    
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridControlFees_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }

                if (gridView1.DataRowCount > 0)
                {
                  //  System.Globalization.CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentCulture;
                  //  string decimalSeparator = ci.NumberFormat.CurrencyDecimalSeparator;
                  //  string decimalSeparatorNo = ci.NumberFormat.NumberDecimalSeparator;

                    string fees;
                    cmbClass.Enabled = false;
                    cmbFeeName.Enabled = false;
                    cmbMonth.Enabled = false;
                    txtFee.Enabled = true;
                    txtID.Text = gridView1.GetFocusedRowCellValue(LocRM.GetString("strNo").ToUpper()).ToString();
                    cmbClass.EditValue = gridView1.GetFocusedRowCellValue(LocRM.GetString("strClassName").ToUpper());
                    cmbFeeName.EditValue = gridView1.GetFocusedRowCellValue(LocRM.GetString("strFeeName").ToUpper());
                    cmbMonth.EditValue = gridView1.GetFocusedRowCellValue(LocRM.GetString("strPeriod").ToUpper());
                    fees =  gridView1.GetFocusedRowCellValue(LocRM.GetString("strFee").ToUpper()).ToString();

                    double dFess = double.Parse(fees, new NumberFormatInfo() { NumberDecimalSeparator = "." });                    
                    txtFee.Text = dFess.ToString("N", CultureInfo.CurrentCulture);                  


                    btnUpdate.Enabled = true;
                    btnRemove.Enabled = true;
                    btnSave.Enabled = false;
                    btnNewFee.Enabled = true;

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

        private void txtSearchByFeeName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string NoColumn = LocRM.GetString("strNo").ToUpper();
                string ClassNameColumn = LocRM.GetString("strClassName").ToUpper();
                string FeeNameColumn = LocRM.GetString("strFeeName").ToUpper();
                string MonthColumn = LocRM.GetString("strPeriod").ToUpper();
                string FeeColumn = LocRM.GetString("strFee").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT RTRIM(CourseFeeID) as [" + NoColumn + "],RTRIM(ClassName) as [" + ClassNameColumn + "], " +
                    "RTRIM(FeeName) as [" + FeeNameColumn + "], RTRIM(Month) as [" + MonthColumn + "], " +
                    "RTRIM(Fee) as [" + FeeColumn + "] FROM CourseFee where FeeName like '%" + txtSearchByFeeName.Text + "%' order by ClassName,FeeName", con);

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "CourseFee");
                    gridControlFees.DataSource = myDataSet.Tables["CourseFee"].DefaultView;

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

        private void txtFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            char keyChar = e.KeyChar;

            if (CultureInfo.CurrentCulture.ToString() == "fr-FR")
            {
                if (char.IsControl(keyChar))
                {
                    //Allow all control characters.
                }
                else if (char.IsDigit(keyChar) || keyChar == ',')
                {
                    string text = this.txtFee.Text;
                    int selectionStart = this.txtFee.SelectionStart;
                    int selectionLength = this.txtFee.SelectionLength;

                    text = text.Substring(0, selectionStart) + keyChar + text.Substring(selectionStart + selectionLength);

                    if (int.TryParse(text, out int result) && text.Length > 16)
                    {
                        //Reject an integer that is longer than 16 digits.
                        e.Handled = true;
                    }
                    else if (double.TryParse(text, out double results) && text.IndexOf(',') < text.Length - 3)
                    {
                        //Reject a real number with two many decimal places.
                        e.Handled = false;
                    }
                }
                else
                {
                    //Reject all other characters.
                    e.Handled = true;
                }
            }
            else
            {
                if (char.IsControl(keyChar))
                {
                    //Allow all control characters.
                }
                else if (char.IsDigit(keyChar) || keyChar == '.')
                {
                    string text = this.txtFee.Text;
                    int selectionStart = this.txtFee.SelectionStart;
                    int selectionLength = this.txtFee.SelectionLength;

                    text = text.Substring(0, selectionStart) + keyChar + text.Substring(selectionStart + selectionLength);

                    if (int.TryParse(text, out int result) && text.Length > 16)
                    {
                        //Reject an integer that is longer than 16 digits.
                        e.Handled = true;
                    }
                    else if (double.TryParse(text, out double results) && text.IndexOf('.') < text.Length - 3)
                    {
                        //Reject a real number with two many decimal places.
                        e.Handled = false;
                    }
                }
                else
                {
                    //Reject all other characters.
                    e.Handled = true;
                }

            }
        }

        private void userControlClassFeeEntry_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible == true)
            {
                clear();
                disable();
                gridControlFees.DataSource = Getdata();
            }
           
        }

        private void txtSearchByClass_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                }
                string NoColumn = LocRM.GetString("strNo").ToUpper();
                string ClassNameColumn = LocRM.GetString("strClassName").ToUpper();
                string FeeNameColumn = LocRM.GetString("strFeeName").ToUpper();
                string MonthColumn = LocRM.GetString("strPeriod").ToUpper();
                string FeeColumn = LocRM.GetString("strFee").ToUpper();

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    cmd = new SqlCommand("SELECT RTRIM(CourseFeeID) as [" + NoColumn + "],RTRIM(ClassName) as [" + ClassNameColumn + "], " +
                    "RTRIM(FeeName) as [" + FeeNameColumn + "], RTRIM(Month) as [" + MonthColumn + "], " +
                    "RTRIM(Fee) as [" + FeeColumn + "] FROM CourseFee where ClassName like '%" + txtSearchByClass.Text + "%' order by ClassName,FeeName", con);

                    SqlDataAdapter myDA = new SqlDataAdapter(cmd);
                    DataSet myDataSet = new DataSet();

                    myDA.Fill(myDataSet, "CourseFee");
                    gridControlFees.DataSource = myDataSet.Tables["CourseFee"].DefaultView;

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

        private void btnEditFeeStructure_Click(object sender, EventArgs e)
        {
            groupSchoolFeeStructure.Enabled = true;
        }
        bool feeStructurePerMonth = false;//false = Quaterly, True = per month 
        private void radioQuarterly_CheckedChanged(object sender, EventArgs e)
        {
            if(radioQuarterly.Checked == true)
            {
                feeStructurePerMonth = false;
                groupSchoolFeeStructure.Enabled = false;
                fillMonth();
            }
        }

        private void radioPerMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPerMonth.Checked == true)
            {
                feeStructurePerMonth = true;
                groupSchoolFeeStructure.Enabled = false;
                fillMonth();
            }
        }
    }
}
