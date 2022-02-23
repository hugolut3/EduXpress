using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Controls;
using System.Resources;

namespace EduXpress.Students.CustomCheckComboBoxEditor
{
    /// <summary>
	/// MyCheckedComboBoxEdit is a descendant from CheckedComboBoxEdit.
	/// It displays a dialog form below the text box when the edit button is clicked.
	/// </summary>
    public  class MyCheckedComboBoxEdit : CheckedComboBoxEdit
    {
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit fProperties;
        ResourceManager LocRM = new ResourceManager("EduXpress.Language.EduXpressStrings", typeof(userControlStudentEnrolmentFormSA).Assembly);
        static MyCheckedComboBoxEdit()
        {
            RepositoryItemMyCheckedComboBoxEdit.Register();
        }

        protected override CheckedListBoxControl CreateCheckedListBox(bool forFilter)
        {
            var result = base.CreateCheckedListBox(forFilter);
            result.ItemChecking += InnterCheckedListBoxItemChecking;
            return result;
        }
        //if the None is selected, desect the rest of the items
        private void InnterCheckedListBoxItemChecking(object sender, ItemCheckingEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                if (e.Index == 1)
                {
                    foreach (var item in InternalListBox.Items) //  "Select All"
                    {
                        if ((item as CheckedListBoxItem).Value.ToString() != LocRM.GetString("strSelectAll") && (item as CheckedListBoxItem).Value.ToString() != "1")
                            (item as CheckedListBoxItem).CheckState = CheckState.Unchecked;
                    }
                }
                //else
                //    InternalListBox.Items["1"].CheckState = CheckState.Unchecked;
            }
        }

        public override string EditorTypeName
        {
            get { return RepositoryItemMyCheckedComboBoxEdit.EditorName; }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemMyCheckedComboBoxEdit Properties
        {
            get { return base.Properties as RepositoryItemMyCheckedComboBoxEdit; }
        }

        //public override object EditValue
        //{
        //    get
        //    {
        //        object editValue = base.EditValue;
        //        if (isParsing)
        //            editValue = Properties.MyFormatEditValue(editValue);
        //        return editValue;
        //    }
        //    set
        //    {
        //        base.EditValue = value;
        //    }
        //}

       // bool isParsing;

        //protected override void DoSynchronizeEditValueWithCheckedItems()
        //{
        //    isParsing = true;
        //    base.DoSynchronizeEditValueWithCheckedItems();
        //    isParsing = false;
        //}

        //to be able to resize the control override the OnPopupClosed(PopupCloseMode closeMode) method.
        //to resize when placed on a form:  

        //protected override void OnPopupClosed(PopupCloseMode closeMode)
        //{
        //    base.OnPopupClosed(closeMode);
        //    BaseControlViewInfo vi = this.GetViewInfo();
        //    MyCheckedComboBoxEditViewInfo viCheck = vi as MyCheckedComboBoxEditViewInfo;
        //    IHeightAdaptable ah = viCheck as IHeightAdaptable;        
        //    this.Height = ah.CalcHeight(viCheck.GInfo.Cache, viCheck.MaskBoxRect.Width) + this.Margin.Vertical;
        //}

        //to resize when placed inside a layout control below override method: protected override void OnPopupClosed(PopupCloseMode closeMode)
        //+  private int GetNewHeight() + protected override Size CalcSizeableMaxSize() + protected override Size CalcSizeableMinSize()
        protected override void OnPopupClosed(PopupCloseMode closeMode)
        {
            base.OnPopupClosed(closeMode);
            this.Height = GetNewHeight();
            LayoutChanged();
            RaiseSizeableChanged();
        }
        private int GetNewHeight()
        {
            BaseControlViewInfo vi = this.GetViewInfo();
            if (vi == null || vi.GInfo.Cache == null) return Height;
            MyCheckedComboBoxEditViewInfo viCheck = vi as MyCheckedComboBoxEditViewInfo;
            IHeightAdaptable ah = viCheck as IHeightAdaptable;
            int height = ah.CalcHeight(viCheck.GInfo.Cache, viCheck.MaskBoxRect.Width) + this.Margin.Vertical;
            return height;
        }
        protected override Size CalcSizeableMinSize()
        {
            var result = base.CalcSizeableMinSize();
            return new Size(result.Width, GetNewHeight());
        }

        protected override Size CalcSizeableMaxSize()
        {
            var result = base.CalcSizeableMaxSize();
            return new Size(result.Width, GetNewHeight());
        }

        protected override void OnEditValueChanged()
        {
            base.OnEditValueChanged();
            this.Height = GetNewHeight();
            LayoutChanged();
            RaiseSizeableChanged();
        }

        private void InitializeComponent()
        {
            this.fProperties = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).BeginInit();
            this.SuspendLayout();
            // 
            // fProperties
            // 
            this.fProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fProperties.Name = "fProperties";
            ((System.ComponentModel.ISupportInitialize)(this.fProperties)).EndInit();
            this.ResumeLayout(false);

        }
    }
    
}
