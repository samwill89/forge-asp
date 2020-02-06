using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace DeleteElements
{
    class ColumnData
    {
        public FamilySymbol Type { get; }

        public string CapitalName { get; }

        public string BaseName { get; }

        public string PlinthName { get; }

        public int PlinthWidth { get; }

        public int PlinthHeight { get; }

        public ColumnData(FamilySymbol type, string capitalName, string baseName, string plinthName, int plinthWidth, int plinthHeight)
        {
            Type = type;
            CapitalName = capitalName != null ? $"Capital Style : {capitalName}" : null;
            BaseName = baseName != null ? $"Base Style : {baseName}" : null;
            PlinthName = plinthName != null ? $"Plinth Style : {plinthName}" : null;
            PlinthWidth = plinthWidth;
            PlinthHeight = plinthHeight;
        }
    }
}
