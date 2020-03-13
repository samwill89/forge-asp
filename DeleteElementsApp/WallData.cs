using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace DeleteElements
{
    public class WallData
    {
        public WallType MainWallType { get; set; }

        public WallType BaseWallType { get; set; }

        public WallType AccentWallType { get; set; }

        public string MainMaterialCategory { get; set; }

        public string MainMaterialColor { get; set; }

        public string BaseMaterialCategory { get; set; }

        public string BaseMaterialColor { get; set; }

        public WallData(WallType mainWallType, WallType baseWallType, WallType accentWallType,
            string mainMaterialCategory, string mainMaterialColor, string baseMaterialCategory, string baseMaterialColor)
        {
            MainWallType = mainWallType ?? throw new ArgumentNullException(nameof(mainWallType));
            BaseWallType = baseWallType ?? mainWallType;
            AccentWallType = accentWallType ?? mainWallType;

            MainMaterialCategory = mainMaterialCategory;
            MainMaterialColor = mainMaterialColor;
            BaseMaterialCategory = baseMaterialCategory;
            BaseMaterialColor = baseMaterialColor;
        }
    }
}
