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
using DevExpress.XtraSpellChecker;
using System.IO;
using System.Reflection;
using System.Globalization;

namespace EduXpress.Office
{
    public partial class userControlOfficeWord : DevExpress.XtraEditors.XtraUserControl
    {
        public userControlOfficeWord()
        {
            InitializeComponent();            
        }

        //private void btnExit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        //{
        //    ((Form)this.TopLevelControl).Close();
        //}

    
        private void userControlOfficeWord_Load(object sender, EventArgs e)
        {
           //Create stream objects for each dictionary file 
            SpellCheckerOpenOfficeDictionary dictionaryEnglish = new SpellCheckerOpenOfficeDictionary();
           // SpellCheckerOpenOfficeDictionary dictionaryEnglishParent = new SpellCheckerOpenOfficeDictionary();
            SpellCheckerOpenOfficeDictionary dictionaryFrench = new SpellCheckerOpenOfficeDictionary();
            //SpellCheckerOpenOfficeDictionary dictionaryFrenchParent = new SpellCheckerOpenOfficeDictionary();
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
            dictionaryEnglish.Culture= new  CultureInfo("en-US");

          //  dictionaryEnglishParent.LoadFromStream(dicStreamEN, affStreamEN, alphabetStreamEN);
           // dictionaryEnglishParent.Culture = new CultureInfo("en");

            dictionaryFrench.LoadFromStream(dicStreamFR, affStreamFR, alphabetStreamFR);
            dictionaryFrench.Culture = new CultureInfo("fr-FR");

           // dictionaryFrenchParent.LoadFromStream(dicStreamFR, affStreamFR, alphabetStreamFR);
           // dictionaryFrenchParent.Culture = new CultureInfo("fr");

            spellChecker1.Dictionaries.Add(dictionaryEnglish);
            //spellChecker1.Dictionaries.Add(dictionaryEnglishParent);
            spellChecker1.Dictionaries.Add(dictionaryFrench);
          //  spellChecker1.Dictionaries.Add(dictionaryFrenchParent);
            spellChecker1.Dictionaries.Add(customDictionaryEnglish);
            spellChecker1.Dictionaries.Add(customDictionaryFrench);

            richEditControl1.Focus();
        }       

    }
}
