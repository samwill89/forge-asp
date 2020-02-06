using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace DeleteElements
{
    class WallData
    {
        public WallType MainWallType { get; }

        public WallType BaseWallType { get; }

        public WallType AccentWallType { get; }

        public string MaterialCategory { get; }

        public string MaterialColor { get; }

        public WallData(WallType mainWallType, WallType baseWallType, WallType accentWallType, string materialCategory, string materialColor)
        {
            MainWallType = mainWallType ?? throw new ArgumentNullException(nameof(mainWallType));
            BaseWallType = baseWallType ?? mainWallType;
            AccentWallType = accentWallType ?? mainWallType;
            MaterialCategory = materialCategory;
            MaterialColor = materialColor;
        }
    }
}
