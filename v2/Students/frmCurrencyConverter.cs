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
using System.Globalization;
using System.Resources;
using System.IO;
using System.Data.SqlClient;
using static EduXpress.Functions.PublicVariables;

namespace EduXpress.Students
{
    public partial class frmCurrencyConverter : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmCurrencyConverter).Assembly);
        public frmCurrencyConverter()
        {
            InitializeComponent();
        }

        private void btnNew_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            btnSave.Enabled = true;
            btnNew.Enabled = false;
            txtRate.Properties.ReadOnly = false;
        }

        private void btnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
        }                

        private void frmCurrencyConverter_Load(object sender, EventArgs e)
        {
            loadExchangeRates();
            fillComboBoxes();
            cmbSelectCurrency.SelectedIndex = 0;
            gridControlCurrencyConverter.DataSource = Getdata();
        }
        private void fillComboBoxes()
        {
            cmbSelectCurrency.Properties.Items.Clear();
            cmbSelectCurrency.Properties.Items.AddRange(new object[] { LocRM.GetString("strUSD"),
            LocRM.GetString("strEUR") });
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
        public DataView Getdata()
        {
            string USDtoCDFColumn = LocRM.GetString("str1USDtoCDF").ToUpper();
            string EURtoCDFColumn = LocRM.GetString("str1EURtoCDF").ToUpper();
            string LastUpdatedColumn = LocRM.GetString("strLastUpdated").ToUpper();

            dynamic SelectQry = "SELECT  USDCDF as [" + USDtoCDFColumn + "], EURCDF as [" + EURtoCDFColumn + "]," +
                " LastUpdated as [" + LastUpdatedColumn + "] FROM ExchangeRates ";
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
        bool exchangeRatesExist = false;
        decimal decimalUSDCDF, decimalUSDEUR, decimalEURCDF;

        private void btnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (exchangeRatesExist)
            {
                updateSettings();
            }
            else
            {
                saveSettings();
            }
        }
        private void saveSettings()
        {
            if (txtRate.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRate.Focus();
                return;
            }
            if (cmbSelectCurrency.SelectedIndex == -1)
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectMainCurrency"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSelectCurrency.Focus();
                return;
            }

            try
            {                
                if (splashScreenManager1.IsSplashFormVisible == false)
                {
                    splashScreenManager1.ShowWaitForm();
                    splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strSaving"));
                }

                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    
                    string cb = "insert into ExchangeRates(USDCDF,USDEUR,EURCDF,LastUpdated) VALUES (@d1,@d2,@d3,@d4)";
                    cmd = new SqlCommand(cb);
                    cmd.Connection = con;
                    if (cmbSelectCurrency.SelectedIndex == 0)
                    {
                        cmd.Parameters.AddWithValue("@d1", decimal.Parse(txtRate.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d2", decimalUSDEUR);
                        cmd.Parameters.AddWithValue("@d3", decimalEURCDF);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@d1", decimalUSDCDF);
                        cmd.Parameters.AddWithValue("@d2", decimalUSDEUR);
                        cmd.Parameters.AddWithValue("@d3", decimal.Parse(txtRate.Text, CultureInfo.CurrentCulture));
                    }
                    DateTime currentdateTime = Convert.ToDateTime(DateTime.Now.ToString(CultureInfo.CurrentCulture));
                    cmd.Parameters.AddWithValue("@d4", currentdateTime);
                    cmd.ExecuteReader();

                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }

                if (splashScreenManager1.IsSplashFormVisible == true)
                {
                    splashScreenManager1.CloseWaitForm();
                }

                XtraMessageBox.Show(LocRM.GetString("strNewExchangeRateSaved"), LocRM.GetString("strCurrencyConversionSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strNewSMSProviderSaved") + ": " + lblCurrency.Text;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);
                
                btnSave.Enabled = false;               
                btnNew.Enabled = true;
                txtRate.Properties.ReadOnly = false;
                gridControlCurrencyConverter.DataSource = Getdata();
                loadExchangeRates();
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

        private void cmbSelectCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectCurrency.SelectedIndex == 0)
            {
                pictureCurrency.EditValue = Properties.Resources.us;
                lblCurrency.Text = LocRM.GetString("strUnitedStatesDollar").ToUpper();
                if (decimalUSDCDF != 0)
                {
                    txtRate.Text =  decimalUSDCDF.ToString();
                }
                else
                {
                    txtRate.Text = "";
                }
            }
            else if (cmbSelectCurrency.SelectedIndex == 1)
            {
                pictureCurrency.EditValue = Properties.Resources.eu;
                lblCurrency.Text = LocRM.GetString("strEuro").ToUpper();
                if (decimalEURCDF != 0)
                {
                    txtRate.Text =  decimalEURCDF.ToString();
                }
                else
                {
                    txtRate.Text = "";
                }

            }
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
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
                    string text = this.txtRate.Text;
                    int selectionStart = this.txtRate.SelectionStart;
                    int selectionLength = this.txtRate.SelectionLength;

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
                    string text = this.txtRate.Text;
                    int selectionStart = this.txtRate.SelectionStart;
                    int selectionLength = this.txtRate.SelectionLength;

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

        private void updateSettings()
        {
            if (txtRate.Text == "")
            {
                XtraMessageBox.Show(LocRM.GetString("strEnterExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtRate.Focus();
                return;
            }
            if (cmbSelectCurrency.SelectedIndex == -1)
            {
                XtraMessageBox.Show(LocRM.GetString("strSelectMainCurrency"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbSelectCurrency.Focus();
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
                    con.Open();
                    string ct = "Update ExchangeRates set USDCDF=@d2,USDEUR=@d3,EURCDF=@d4, LastUpdated=@d5  where id= @d1";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("@d1", txtID.Text.Trim());
                    if (cmbSelectCurrency.SelectedIndex == 0)
                    {
                        cmd.Parameters.AddWithValue("@d2", decimal.Parse(txtRate.Text, CultureInfo.CurrentCulture));
                        cmd.Parameters.AddWithValue("@d3", decimalUSDEUR);
                        cmd.Parameters.AddWithValue("@d4", decimalEURCDF);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@d2", decimalUSDCDF);
                        cmd.Parameters.AddWithValue("@d3", decimalUSDEUR);
                        cmd.Parameters.AddWithValue("@d4", decimal.Parse(txtRate.Text, CultureInfo.CurrentCulture));
                    }
                    DateTime currentdateTime = Convert.ToDateTime(DateTime.Now.ToString(CultureInfo.CurrentCulture));
                    cmd.Parameters.AddWithValue("@d5", currentdateTime);
                    rdr = cmd.ExecuteReader();
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

                XtraMessageBox.Show(LocRM.GetString("strNewExchangeRateSaved"), LocRM.GetString("strCurrencyConversionSettings"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Log record transaction in logs
                string st = LocRM.GetString("strNewSMSProviderSaved") + ": " + lblCurrency.Text;
                pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                btnSave.Enabled = false;
                btnNew.Enabled = true;
                txtRate.Properties.ReadOnly = false;
                gridControlCurrencyConverter.DataSource = Getdata();
                loadExchangeRates();
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

        private void loadExchangeRates()
        {
            try
            {
                using (con = new SqlConnection(databaseConnectionString))
                {
                    con.Open();
                    string ct = "select * from ExchangeRates ";

                    cmd = new SqlCommand(ct);
                    cmd.Connection = con;
                    rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        exchangeRatesExist = true;
                        //load data from database to controls
                        if (rdr.Read())
                        {
                            txtID.Text = (rdr.GetValue(0).ToString().Trim());
                            decimalUSDCDF = decimal.Parse(rdr[1].ToString(), CultureInfo.CurrentCulture);
                            decimalUSDEUR = decimal.Parse(rdr[2].ToString(), CultureInfo.CurrentCulture);
                            decimalEURCDF = decimal.Parse(rdr[3].ToString(), CultureInfo.CurrentCulture);
                            DateTime dt = (DateTime)rdr.GetValue(4);
                            lblLastUpdated.Text = LocRM.GetString("strLastUpdated") + ": " + dt.ToString("dddd, dd MMMM yyyy h:mm tt");

                            cmbSelectCurrency.SelectedIndex = 0;
                            //if (decimalUSDCDF != 0)
                            //{
                            //    txtRate.Text =  decimalUSDCDF.ToString();
                            //}
                            //else
                            //{
                            //    txtRate.Text = "";
                            //}
                        }
                    }
                    else
                    {
                        txtRate.Text = "";
                        exchangeRatesExist = false;
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
    }
}