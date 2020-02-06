using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace DeleteElements
{
    class WindowTrimData
    {
        public ElementType Type { get; }

        public string TopCategory { get; }

        public string SideCategory { get; }

        public string BottomCategory { get; }

        public WindowTrimData(ElementType type, string topCategory, string sideCategory, string bottomCategory)
        {
            Type = type;
            TopCategory = topCategory;
            SideCategory = sideCategory;
            BottomCategory = bottomCategory;
        }
    }
}
