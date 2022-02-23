using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Registrator;

namespace EduXpress.Students.CustomCheckComboBoxEditor
{
    [UserRepositoryItem("Register")]
    public class RepositoryItemMyCheckedComboBoxEdit : RepositoryItemCheckedComboBoxEdit
    {
        static RepositoryItemMyCheckedComboBoxEdit()
        {
            Register();
        }
        public RepositoryItemMyCheckedComboBoxEdit()
        {
            this.SeparatorChar =  '\n';
        }

        internal const string EditorName = "MyCheckedComboBoxEdit";

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(MyCheckedComboBoxEdit),
                typeof(RepositoryItemMyCheckedComboBoxEdit), typeof(MyCheckedComboBoxEditViewInfo),
                new MyButtonEditPainter(), true, null));
        }
        public override string EditorTypeName
        {
            get { return EditorName; }
        }

        //public virtual object MyFormatEditValue(object value)
        //{
        //    if (value == null)
        //        return value;
        //    return value.ToString().Replace(Environment.NewLine, String.Format("{0} ", SeparatorChar));
        //}

        //public virtual object MyParseEditValue(object value)
        //{
        //    if (value == null)
        //        return value;
        //    return value.ToString().Replace(String.Format("{0} ", SeparatorChar), Environment.NewLine);
        //}

        //protected override object DoFormatEditValue(object val, out bool handled)
        //{
        //    object formattedEditValue = MyFormatEditValue(val);
        //    handled = true;
        //    return formattedEditValue;
        //}

        //protected override object DoParseEditValue(object val, out bool handled)
        //{
        //    object parsedEditValue = MyParseEditValue(val);
        //    handled = true;
        //    return parsedEditValue;
        //}
    }
}
