using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using static EduXpress.Functions.PublicFunctions;

namespace EduXpress.Dialogs
{
    public partial class WaitForm2 : WaitForm
    {
        public WaitForm2()
        {
            InitializeComponent();
            this.progressPanel1.AutoHeight = true;
        }
        object locker;
        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
            WaitFormCommand command = (WaitFormCommand)cmd;
            if (command == WaitFormCommand.SendObject)
            {
                locker = arg;
            }
        }

        #endregion
        
        public enum WaitFormCommand
        {
            SendObject
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if ( locker != null)
                ((Functions.ILocked)locker).IsCanceled = true;
        }
    }
}