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
    public partial class frmExchangeRates : DevExpress.XtraEditors.XtraForm
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(frmExchangeRates).Assembly);
        public frmExchangeRates()
        {
            InitializeComponent();
        }

        
        private void btnConvert_Click(object sender, EventArgs e)
        {
            decimal exchangeValue = 0;
            if (exchangeRatesExist==true)
            {
                if ((cmbCurrencyFrom.SelectedIndex == 0) && (cmbCurrencyTo.SelectedIndex == 1))
                {
                    if(decimalUSDCDF !=0)
                    {
                        exchangeValue = Convert.ToDecimal(txtAmountConvert.Text, CultureInfo.CurrentCulture) / decimalUSDCDF;
                        exchangeValue = Math.Round(exchangeValue, 2);
                        txtDisplay.Text = exchangeValue.ToString();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSetExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnUpdateRates.Focus();
                        return;
                    }
                    
                }
                else if ((cmbCurrencyFrom.SelectedIndex == 1) && (cmbCurrencyTo.SelectedIndex == 0))
                {
                    if (decimalUSDCDF != 0)
                    {
                        exchangeValue = Convert.ToDecimal(txtAmountConvert.Text, CultureInfo.CurrentCulture) * decimalUSDCDF;
                        exchangeValue = Math.Round(exchangeValue, 2);
                        txtDisplay.Text = exchangeValue.ToString();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSetExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnUpdateRates.Focus();
                        return;
                    }

                    
                }
                else if ((cmbCurrencyFrom.SelectedIndex == 0) && (cmbCurrencyTo.SelectedIndex == 2))
                {
                    if (decimalEURCDF != 0)
                    {
                        exchangeValue = Convert.ToDecimal(txtAmountConvert.Text, CultureInfo.CurrentCulture) / decimalEURCDF;
                        exchangeValue = Math.Round(exchangeValue, 2);
                        txtDisplay.Text = exchangeValue.ToString();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSetExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnUpdateRates.Focus();
                        return;
                    }
                }
                else if ((cmbCurrencyFrom.SelectedIndex == 2) && (cmbCurrencyTo.SelectedIndex == 0))
                {
                    if (decimalEURCDF != 0)
                    {
                        exchangeValue = Convert.ToDecimal(txtAmountConvert.Text, CultureInfo.CurrentCulture) * decimalEURCDF;
                        exchangeValue = Math.Round(exchangeValue, 2);
                        txtDisplay.Text = exchangeValue.ToString();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSetExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnUpdateRates.Focus();
                        return;
                    }                    
                }
                else if ((cmbCurrencyFrom.SelectedIndex == 1) && (cmbCurrencyTo.SelectedIndex == 2))
                {
                    if (decimalUSDEUR != 0)
                    {
                        exchangeValue = Convert.ToDecimal(txtAmountConvert.Text, CultureInfo.CurrentCulture) / decimalUSDEUR;
                        exchangeValue = Math.Round(exchangeValue, 2);
                        txtDisplay.Text = exchangeValue.ToString();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSetExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnUpdateRates.Focus();
                        return;
                    }
                }
                else if ((cmbCurrencyFrom.SelectedIndex == 2) && (cmbCurrencyTo.SelectedIndex == 1))
                {
                    if (decimalUSDEUR != 0)
                    {
                        exchangeValue = Convert.ToDecimal(txtAmountConvert.Text, CultureInfo.CurrentCulture) * decimalUSDEUR;
                        exchangeValue = Math.Round(exchangeValue, 2);
                        txtDisplay.Text = exchangeValue.ToString();
                    }
                    else
                    {
                        XtraMessageBox.Show(LocRM.GetString("strSetExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnUpdateRates.Focus();
                        return;
                    }
                }
            }
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strSetExchangeRates"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnUpdateRates.Focus();
                    return;              
            }            
            
        }

        private void txtAmountConvert_KeyPress(object sender, KeyPressEventArgs e)
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
                    string text = this.txtAmountConvert.Text;
                    int selectionStart = this.txtAmountConvert.SelectionStart;
                    int selectionLength = this.txtAmountConvert.SelectionLength;

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
                    string text = this.txtAmountConvert.Text;
                    int selectionStart = this.txtAmountConvert.SelectionStart;
                    int selectionLength = this.txtAmountConvert.SelectionLength;

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

        private void frmExchangeRates_Load(object sender, EventArgs e)
        {
            loadExchangeRates();

            fillComboBoxes();
            cmbSelectCurrency.SelectedIndex = 0;
            cmbCurrencyFrom.SelectedIndex = 0;
            cmbCurrencyTo.SelectedIndex = 1;
        }
        private void fillComboBoxes()
        {

            cmbSelectCurrency.Properties.Items.Clear();
            // imageComboSelectCurrency.Properties.SmallImages = svgImageCollection1;
            cmbSelectCurrency.Properties.Items.AddRange(new object[] { LocRM.GetString("strUSD"),
            LocRM.GetString("strEUR") });

            cmbCurrencyFrom.Properties.Items.Clear();
            // comboCurrencyFrom.Properties.SmallImages = svgImageCollection1;
            cmbCurrencyFrom.Properties.Items.AddRange(new object[] { LocRM.GetString("strCDF"), 
            LocRM.GetString("strUSD"),LocRM.GetString("strEUR") });

            cmbCurrencyTo.Properties.Items.Clear();
            // comboCurrencyTo.Properties.SmallImages = svgImageCollection1;
            cmbCurrencyTo.Properties.Items.AddRange(new object[] { LocRM.GetString("strCDF"),
            LocRM.GetString("strUSD"),LocRM.GetString("strEUR") });
        }

        bool exchangeRatesExist = false;        
        decimal decimalUSDCDF, decimalUSDEUR, decimalEURCDF;
        private void loadExchangeRates()
        {
            try
            {

                //Check if Company Profile has data in database

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
                            //feePaid = decimal.Parse(rdr[5].ToString(), CultureInfo.CurrentCulture);
                            decimalUSDCDF = decimal.Parse(rdr[1].ToString(), CultureInfo.CurrentCulture);
                            decimalUSDEUR = decimal.Parse(rdr[2].ToString(), CultureInfo.CurrentCulture);
                            decimalEURCDF = decimal.Parse(rdr[3].ToString(), CultureInfo.CurrentCulture);
                            DateTime dt = (DateTime)rdr.GetValue(4);
                            lblLastUpdated.Text = LocRM.GetString("strLastUpdated") + ": " + dt.ToString("dddd, dd MMMM yyyy h:mm tt");

                            cmbSelectCurrency.SelectedIndex = 0;
                            if (decimalUSDCDF != 0)
                            {
                                lblConversion.Text = "1 = " + decimalUSDCDF.ToString();
                            }
                            else
                            {
                                lblConversion.Text = "0 = " + "0";
                            }
                        }
                    }
                    else
                    {
                        lblConversion.Text = "";
                        exchangeRatesExist = false;
                        lblLastUpdated.Text = "";
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
        private void imageComboSelectCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void cmbSelectCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectCurrency.SelectedIndex == 0)
            {
                pictureCurrency.EditValue = Properties.Resources.us;
                lblCurrency.Text = LocRM.GetString("strUnitedStatesDollar").ToUpper();
                if (decimalUSDCDF != 0)
                {
                    lblConversion.Text = "1 = " + decimalUSDCDF.ToString();
                }
                else
                {
                    lblConversion.Text = "0 = " + "0";
                }
            }
            else if (cmbSelectCurrency.SelectedIndex == 1)
            {
                pictureCurrency.EditValue = Properties.Resources.eu;
                lblCurrency.Text = LocRM.GetString("strEuro").ToUpper();
                if (decimalEURCDF != 0)
                {
                    lblConversion.Text = "1 = " + decimalEURCDF.ToString();
                }
                else
                {
                    lblConversion.Text = "0 = " + "0";
                }

            }
        }

        private void btnUpdateRates_Click(object sender, EventArgs e)
        {
            if ((Role == 1) || (Role == 2))//Administrator, Administrator Assistant only
            {
                Cursor = Cursors.WaitCursor;
                timer1.Enabled = true;
                frmCurrencyConverter frm = new frmCurrencyConverter();
                frm.ShowDialog();
                loadExchangeRates();
                
            }
            else
            {
                XtraMessageBox.Show(LocRM.GetString("strNotAllowedUpdateExchangeRates"), LocRM.GetString("strExchangeRates"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnUpdateRates.Focus();
                return;
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }
    }
}