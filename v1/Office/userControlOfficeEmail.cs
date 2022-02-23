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
using System.Text.RegularExpressions;
using System.Collections;
using System.Data.SqlClient;
using System.Net.Mail;
using DevExpress.XtraRichEdit.Export;
using DevExpress.Office.Utils;
using DevExpress.Utils;
using DevExpress.XtraRichEdit;
using System.IO;
using System.Net.Mime;
using DevExpress.Office.Services;
using System.Resources;
using DevExpress.XtraSpellChecker;
using System.Reflection;
using System.Globalization;

namespace EduXpress.Office
{
    public partial class userControlOfficeEmail : DevExpress.XtraEditors.XtraUserControl
    {
        SqlConnection con = null;
        string databaseConnectionString = Properties.Settings.Default.DatabaseConnection;
        SqlCommand cmd = null;
        SqlDataReader rdr = null;
        Functions.PublicFunctions pf = new Functions.PublicFunctions();
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlOfficeEmail).Assembly);
        Regex MailRegex = new Regex("^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(?:\\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@(?:[a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\\.)*(?:aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$", RegexOptions.Compiled);
        
        public userControlOfficeEmail()
        {
            InitializeComponent();
            this.sendToEditSize = tokTo.Size;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            timer1.Enabled = false;
        }

        private void userControlOfficeEmail_Load(object sender, EventArgs e)
        {
            tokTo.Focus();
            // spellChecker1.Culture = System.Globalization.CultureInfo.CurrentCulture;

            //Create stream objects for each dictionary file 
            SpellCheckerOpenOfficeDictionary dictionaryEnglish = new SpellCheckerOpenOfficeDictionary();
          //  SpellCheckerOpenOfficeDictionary dictionaryEnglishParent = new SpellCheckerOpenOfficeDictionary();
            SpellCheckerOpenOfficeDictionary dictionaryFrench = new SpellCheckerOpenOfficeDictionary();
           // SpellCheckerOpenOfficeDictionary dictionaryFrenchParent = new SpellCheckerOpenOfficeDictionary();
            SpellCheckerCustomDictionary customDictionaryEnglish = new SpellCheckerCustomDictionary();
            SpellCheckerCustomDictionary customDictionaryFrench = new SpellCheckerCustomDictionary();

            //Create stream objects for en-US dictionary file 
            Stream affStreamEN = Assembly.GetExecutingAssembly().GetManifestResourceStream("EduXpress.Dictionaries.en_US.aff");
            Stream dicStreamEN = Assembly.GetExecutingAssembly().GetManifestResourceStream("EduXpress.Dictionaries.en_US.dic");
            Stream alphabetStreamEN = Assembly.GetExecutingAssembly().GetManifestResourceStream("EduXpress.Dictionaries.EnglishAlphabet.txt");

            //Create stream objects for fr-FR dictionary file
            Stream affStreamFR = Assembly.GetExecutingAssembly().GetManifestResourceStream("EduXpress.Dictionaries.fr_FR-1990.aff");
            Stream dicStreamFR = Assembly.GetExecutingAssembly().GetManifestResourceStream("EduXpress.Dictionaries.fr_FR-1990.dic");
            Stream alphabetStreamFR = Assembly.GetExecutingAssembly().GetManifestResourceStream("EduXpress.Dictionaries.FrenchAlphabet.txt");

            // load english custom dictionary

            customDictionaryEnglish.AlphabetPath = @"Dictionaries.EnglishAlphabet.txt";
            customDictionaryEnglish.DictionaryPath = @"Dictionaries\CustomEnglishDictionary.dic";
            customDictionaryEnglish.Culture = new CultureInfo("en-US");

            // load French custom dictionary

            customDictionaryFrench.AlphabetPath = @"Dictionaries.FrenchAlphabet.txt";
            customDictionaryFrench.DictionaryPath = @"Dictionaries\CustomFrenchDictionary.dic";
            customDictionaryFrench.Culture = new CultureInfo("fr-FR");

            // load dictionaries from memory
            dictionaryEnglish.LoadFromStream(dicStreamEN, affStreamEN, alphabetStreamEN);
            dictionaryEnglish.Culture = new CultureInfo("en-US");

           // dictionaryEnglishParent.LoadFromStream(dicStreamEN, affStreamEN, alphabetStreamEN);
           // dictionaryEnglishParent.Culture = new CultureInfo("en");

            dictionaryFrench.LoadFromStream(dicStreamFR, affStreamFR, alphabetStreamFR);
            dictionaryFrench.Culture = new CultureInfo("fr-FR");

           // dictionaryFrenchParent.LoadFromStream(dicStreamFR, affStreamFR, alphabetStreamFR);
           // dictionaryFrenchParent.Culture = new CultureInfo("fr");

            spellChecker1.Dictionaries.Add(dictionaryEnglish);
           // spellChecker1.Dictionaries.Add(dictionaryEnglishParent);
            spellChecker1.Dictionaries.Add(dictionaryFrench);
           // spellChecker1.Dictionaries.Add(dictionaryFrenchParent);
            spellChecker1.Dictionaries.Add(customDictionaryEnglish);
            spellChecker1.Dictionaries.Add(customDictionaryFrench);            

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            timer1.Enabled = true;
            try
             {
                
                if (pf.CheckForInternetConnection() == true)
                {
                    // ----- Check for a valid entry.
                    if (tokTo.EditValue == null)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strEnterRecipientEmail"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tokTo.Focus();
                        return;
                    }
                    bool isEmpty = false;
                    switch (tokTo.Properties.EditValueType)
                    {
                        case TokenEditValueType.List:
                            isEmpty = (tokTo.EditValue as IList).Count == 0;
                            break;
                        case TokenEditValueType.String:
                            isEmpty = tokTo.EditValue.ToString() == string.Empty;
                            break;
                        case TokenEditValueType.Enum:
                            isEmpty = (int)tokTo.EditValue == 0;
                            break;
                    }
                    if (isEmpty)
                    {
                        XtraMessageBox.Show(LocRM.GetString("strEnterRecipientEmail"), LocRM.GetString("strInputError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tokTo.Focus();
                        return;
                    }

                    if (txtSubject.Text.Trim().Length == 0)
                    {
                        if (XtraMessageBox.Show(LocRM.GetString("strSendMessageWithoutSubject"), LocRM.GetString("strInputError"), MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            txtSubject.Focus();
                            return;
                        }

                    }
                    if (richEditControlEmail.Text.Trim().Length == 0)
                    {
                        if (XtraMessageBox.Show(LocRM.GetString("strSendEmptyMessage"), LocRM.GetString("strInputError"), MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                        {
                            richEditControlEmail.Focus();
                            return;
                        }

                    }

                    //if (splashScreenManager1.IsSplashFormVisible == false)
                    //{
                    //    splashScreenManager1.ShowWaitForm();
                    //}

                    using (con = new SqlConnection(databaseConnectionString))
                    {
                        con.Open();
                        string ctn = "select RTRIM(SenderName), RTRIM(Username),RTRIM(Password),RTRIM(SMTPAddress),(Port),(TLS_SSL_Required) from EmailSetting where IsDefault='Yes' and IsActive='Yes'";
                        cmd = new SqlCommand(ctn);
                        cmd.Connection = con;
                        rdr = cmd.ExecuteReader();

                        //if (splashScreenManager1.IsSplashFormVisible == true)
                        //{
                        //    splashScreenManager1.CloseWaitForm();
                        //}
                        if (rdr.Read())
                        {
                            
                            if (splashScreenManager1.IsSplashFormVisible == false)
                            {
                                splashScreenManager1.ShowWaitForm();
                                splashScreenManager1.SetWaitFormDescription(LocRM.GetString("strEmailSending"));
                            }
                            MailMessage msg = new MailMessage();
                            string ToEmail = tokTo.EditValue.ToString().Trim();
                            //spilt To Email if sent to multiple emails
                            string[] ToMultipleEmails = ToEmail.Split(',');
                            foreach (string ToEMailId in ToMultipleEmails)
                            {
                                msg.To.Add(new MailAddress(ToEMailId)); //adding multiple TO Email Id                            
                            }

                            msg.From = new MailAddress(Convert.ToString(rdr.GetValue(1)), Convert.ToString(rdr.GetValue(0)));
                            //Add CC email
                            if (tokCc.EditValue != null)
                            {
                                //spilt To Email if CC to multiple emails
                                string ToCc = tokCc.EditValue.ToString().Trim();
                                string[] ToCCEmails = ToCc.Split(',');
                                foreach (string ToCcEMailId in ToCCEmails)
                                {
                                    msg.CC.Add(new MailAddress(ToCcEMailId)); //adding multiple TO CC Email Id                           
                                }
                            }
                            //Add Bcc email
                            string ToBcc = txtBCC.Text;
                            if ((txtBCC.Visible) && (txtBCC.Text.Trim().Length > 0))
                            {
                                //spilt To Email if CC to multiple emails
                                string[] ToBCEmails = ToBcc.Split(',');
                                foreach (string ToBcEMailId in ToBCEmails)
                                {
                                    msg.Bcc.Add(new MailAddress(ToBcEMailId)); //adding multiple TO BCC Email Id
                                }
                            }
                            //Add attachment                       
                            if (txtAttached.Text != "")
                            {
                                foreach (string sAttachment in attachmentString.Split(";".ToCharArray()))
                                {
                                    Attachment attachment = new Attachment(sAttachment);
                                    msg.Attachments.Add(attachment);
                                }
                            }


                            if (highImportance)
                            {
                                msg.Priority = MailPriority.High;
                            }
                            if (lowImportance)
                            {
                                msg.Priority = MailPriority.Low;
                            }
                            if (highImportance && lowImportance)
                            {
                                msg.Priority = MailPriority.Normal;
                            }
                            //Format Body of email
                            RichEditMailMessageExporter exporter = new RichEditMailMessageExporter(richEditControlEmail, msg);
                            exporter.Export();

                            msg.Subject = txtSubject.Text;
                            using (SmtpClient smt = new SmtpClient(Convert.ToString(rdr.GetValue(3))))
                            {
                                smt.Port = Convert.ToInt16(rdr.GetValue(4));
                                //
                                if (Convert.ToString(rdr.GetString(5).Trim()) == "Yes")
                                {
                                    smt.EnableSsl = true;
                                }
                                else
                                {
                                    smt.EnableSsl = false;
                                }

                                // smt.UseDefaultCredentials = false;
                                smt.Credentials = new System.Net.NetworkCredential(Convert.ToString(rdr.GetValue(1)), pf.Decrypt(Convert.ToString(rdr.GetValue(2))));
                                // smt.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smt.Send(msg);
                            }
                                

                            //   pf.SendMail(Convert.ToString(rdr.GetValue(0)), txtTo.Text, richEditControl.Text, edtSubject.Text, Convert.ToString(rdr.GetValue(2)), Convert.ToInt16(rdr.GetValue(3)), Convert.ToString(rdr.GetValue(0)), pf.Decrypt(Convert.ToString(rdr.GetValue(1))));
                            if ((rdr != null))
                            {
                                rdr.Close();
                            }

                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                            XtraMessageBox.Show(LocRM.GetString("strEmailSent"), LocRM.GetString("strProgramTitle"), MessageBoxButtons.OK, MessageBoxIcon.Information);


                            //Log password recovery transaction in logs
                            string st = LocRM.GetString("strEmailSentTo") + " " + tokTo.EditValue + tokCc.EditValue;
                            pf.LogFunc(Functions.PublicVariables.UserLoggedSurname, st);

                            con.Close();
                            reset();
                        }
                        else
                        {
                            if (splashScreenManager1.IsSplashFormVisible == true)
                            {
                                splashScreenManager1.CloseWaitForm();
                            }
                            XtraMessageBox.Show(LocRM.GetString("strEmailNotSent"), LocRM.GetString("strProgramTitle"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                       
                }
                else
                {
                    if (splashScreenManager1.IsSplashFormVisible == true)
                    {
                        splashScreenManager1.CloseWaitForm(); 
                    }
                    XtraMessageBox.Show(LocRM.GetString("strNoInternetEmailNotSent"), LocRM.GetString("strProgramTitle"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnSend.Focus();
                    return;
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
        private void reset()
        {
            tokTo.EditValue = null;
            tokCc.EditValue = null;
            txtSubject.Text = "";
            txtAttached.Text = "";
            richEditControlEmail.Text = "";
            tokTo.Focus();

        }
        // Building a mail application with the RichEditControl
        ////https://www.devexpress.com/Support/Center/Example/Details/E2216/building-a-mail-application-with-the-richeditcontrol
        /// <summary>
        /// 
        /// </summary>
        public class RichEditMailMessageExporter : IUriProvider
        {
            readonly RichEditControl control;
            readonly MailMessage message;
            List<AttachementInfo> attachments;
            int imageId;

            public RichEditMailMessageExporter(RichEditControl control, MailMessage message)
            {
                Guard.ArgumentNotNull(control, "control");
                Guard.ArgumentNotNull(message, "message");
                this.control = control;
                this.message = message;
            }

            public virtual void Export()
            {
                this.attachments = new List<AttachementInfo>();

                AlternateView htmlView = CreateHtmlView();
                message.AlternateViews.Add(htmlView);
                message.IsBodyHtml = true;
            }

            protected internal virtual AlternateView CreateHtmlView()
            {
                control.BeforeExport += OnBeforeExport;
                string htmlBody = control.Document.GetHtmlText(control.Document.Range, this);
                AlternateView view = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
                control.BeforeExport -= OnBeforeExport;

                int count = attachments.Count;
                for (int i = 0; i < count; i++)
                {
                    AttachementInfo info = attachments[i];
                    LinkedResource resource = new LinkedResource(info.Stream, info.MimeType);
                    resource.ContentId = info.ContentId;
                    view.LinkedResources.Add(resource);
                }
                return view;
            }

            void OnBeforeExport(object sender, BeforeExportEventArgs e)
            {
                HtmlDocumentExporterOptions options = e.Options as HtmlDocumentExporterOptions;
                if (options != null)
                {
                    options.Encoding = Encoding.UTF8;
                }
            }


            #region IUriProvider Members

            public string CreateCssUri(string rootUri, string styleText, string relativeUri)
            {
                return String.Empty;
            }
            public string CreateImageUri(string rootUri, OfficeImage image, string relativeUri)
            {
                string imageName = String.Format("image{0}", imageId);
                imageId++;

                OfficeImageFormat imageFormat = GetActualImageFormat(image.RawFormat);
                Stream stream = new MemoryStream(image.GetImageBytes(imageFormat));
                string mediaContentType = OfficeImage.GetContentType(imageFormat);
                AttachementInfo info = new AttachementInfo(stream, mediaContentType, imageName);
                attachments.Add(info);

                return "cid:" + imageName;
            }

            OfficeImageFormat GetActualImageFormat(OfficeImageFormat _officeImageFormat)
            {
                if (_officeImageFormat == OfficeImageFormat.Exif ||
                    _officeImageFormat == OfficeImageFormat.MemoryBmp)
                    return OfficeImageFormat.Png;
                else
                    return _officeImageFormat;
            }
            #endregion
        }
        public class AttachementInfo
        {
            Stream stream;
            string mimeType;
            string contentId;

            public AttachementInfo(Stream stream, string mimeType, string contentId)
            {
                this.stream = stream;
                this.mimeType = mimeType;
                this.contentId = contentId;
            }

            public Stream Stream { get { return stream; } }
            public string MimeType { get { return mimeType; } }
            public string ContentId { get { return contentId; } }
        }
        string attachmentString;
        string attachmentStringSize;
        private void attachFile()
        {
            if (!lblAttached.Visible)
            {
                //Display attachment box

                lblAttached.Visible = true;
                txtAttached.Visible = true;
            }
            //add attachment
            XtraOpenFileDialog OpenFile = new XtraOpenFileDialog();
            try
            {
                OpenFile.FileName = "";
                OpenFile.Title = "Insert File";
                OpenFile.Filter = "All Files (*.*)|*.*";
                DialogResult res = OpenFile.ShowDialog();
                if (res == DialogResult.OK)
                {
                    var size = FormatBytes(new FileInfo(OpenFile.FileName).Length).ToString();
                    if (txtAttached.Text == "")
                    {

                        attachmentStringSize = OpenFile.FileName.ToString() + " (" + size + ")";
                        attachmentString = OpenFile.FileName.ToString();


                    }
                    else
                    {
                        attachmentStringSize = attachmentStringSize + ";" + OpenFile.FileName.ToString() + " (" + size + ")";
                        attachmentString = attachmentString + ";" + OpenFile.FileName.ToString();
                    }
                    txtAttached.Text = attachmentStringSize;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, LocRM.GetString("strError"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Convert to B, KB, MB, GB, TB
        private static string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }

        private void barButtonAttachFile_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            attachFile();
        }

        private void barButtonAttachFileInsert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            attachFile();
        }

        private void tokTo_ValidateToken(object sender, TokenEditValidateTokenEventArgs e)
        {
            e.IsValid = MailRegex.IsMatch(e.Description);
        }
        Size sendToEditSize = Size.Empty;

        private void tokTo_SizeChanged(object sender, EventArgs e)
        {
            Size newSize = ((TokenEdit)sender).Size;
            panelControl.Height += (newSize.Height - sendToEditSize.Height);
            this.sendToEditSize = newSize;
        }

        private void txtAttached_VisibleChanged(object sender, EventArgs e)
        {
            panelControl.Height = panelControl.Height + txtAttached.Height;

            tokCc.Location = new Point(this.tokCc.Location.X, this.tokTo.Location.Y + 26);
            lblCC.Location = new Point(this.lblCC.Location.X, this.lblTo.Location.Y + 26);
            txtSubject.Location = new Point(this.txtSubject.Location.X, this.tokCc.Location.Y + 26);
            lblSubject.Location = new Point(this.lblSubject.Location.X, this.lblCC.Location.Y + 26);
            txtAttached.Location = new Point(this.txtAttached.Location.X, this.txtSubject.Location.Y + 26);
            lblAttached.Location = new Point(this.lblAttached.Location.X, this.lblSubject.Location.Y + 26);
        }
        Size sendToEditSizeCC = Size.Empty;

        private void tokCc_SizeChanged(object sender, EventArgs e)
        {
            Size newSize = ((TokenEdit)sender).Size;
            panelControl.Height += (newSize.Height - sendToEditSizeCC.Height);
            this.sendToEditSize = newSize;
        }

        private void tokCc_ValidateToken(object sender, TokenEditValidateTokenEventArgs e)
        {
            e.IsValid = MailRegex.IsMatch(e.Description);
        }
        bool lowImportance = false;
        bool highImportance = false;
        private void barCheckHighImportance_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (highImportance)
            {
                barCheckHighImportance.Checked = false;
                highImportance = false;
            }
            else
            {
                barCheckHighImportance.Checked = true;
                barCheckLowImportance.Checked = false;
                highImportance = true;
            }
        }

        private void barCheckLowImportance_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            if (lowImportance)
            {
                barCheckLowImportance.Checked = false;
                lowImportance = false;
            }
            else
            {
                barCheckLowImportance.Checked = true;
                barCheckHighImportance.Checked = false;
                lowImportance = true;
            }
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

        
    }
}
