using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace DeleteElements
{
    class WindowData
    {
        public string StyleCategory { get; }

        public List<FamilySymbol> Types { get; }

        public WindowTrimData Trim { get; }

        public string ShutterCategory { get; }

        public string KeyCategory { get; }

        public WindowData(string styleCategory, List<FamilySymbol> types, WindowTrimData trim, string shutterCategory, string keyCategory)
        {
            StyleCategory = styleCategory;
            Types = types;
            Trim = trim;
            ShutterCategory = shutterCategory;
            KeyCategory = keyCategory;
        }
    }
}
