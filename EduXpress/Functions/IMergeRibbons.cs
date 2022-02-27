using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraBars.Ribbon;

namespace EduXpress.Functions
{
    interface IMergeRibbons
    {
        RibbonControl MainRibbon { get; set; }
        void MergeRibbon();

        RibbonStatusBar MainStatusBar { get; set; }
        void MergeStatusBar();
    }
}
