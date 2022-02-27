using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors.ViewInfo;
using DevExpress.XtraEditors.Repository;
using System.Drawing;

namespace EduXpress.Students.CustomCheckComboBoxEditor
{
    public class MyCheckedComboBoxEditViewInfo : ButtonEditViewInfo, IHeightAdaptable
    {
        public MyCheckedComboBoxEditViewInfo(RepositoryItem item)
            : base(item)
        {

        }

        protected override void CalcTextSize(Graphics g, bool useDisplayText)
        {
            base.CalcTextSize(g, true);
        }

        #region IHeightAdaptable Members

        int IHeightAdaptable.CalcHeight(DevExpress.Utils.Drawing.GraphicsCache cache, int width)
        {
            CalcTextSize(cache.Graphics);
            return TextSize.Height;
        }

        #endregion

    }
}
