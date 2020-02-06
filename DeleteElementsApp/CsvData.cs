using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace DeleteElements
{
    class CsvData
    {
        private readonly List<IDictionary<string, object>> _facadesDb;
        private readonly List<string> _keys;

        public Dictionary<string, string> Values { get; private set; }

        public CsvData()
        {
            //using (var reader = new StreamReader(path))
            //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            //{
            //    _facadesDb = csv.GetRecords<dynamic>().Select(row => (IDictionary<string, object>)row).ToList();

            //    _keys = _facadesDb[0].Keys.ToList();
            //}
        }

        public void SetStyle(string style)
        {
            Values = new Dictionary<string, string>();
            Values.Add("MainMaterial-T", "Siding BoardBatten");
            Values.Add("MainMaterialColor-T", "White");
            Values.Add("BaseMaterial-T", "None");
            Values.Add("BaseMaterialColor-T", "None");
            Values.Add("AccentMaterial-T", "None");
            Values.Add("AccentMaterialColor-T", "None");
            Values.Add("ColumnStyle-F", "Rectangular");
            Values.Add("ColumnPairing-F", "Single Double");
            Values.Add("ColumnMat-F", "Wood");
            Values.Add("ColumnColor-T", "White");
            Values.Add("ColumnCapitalStyle-T", "Regal");
            Values.Add("ColumnBaseStyle-T", "Regal");
            Values.Add("ColumnTopWidth-F", "8");
            Values.Add("ColumnBaseWidth-F", "8");
            Values.Add("ColumnPlinthHeight-T", "0");
            Values.Add("ColumnPlinthStyle-F", "None");
            Values.Add("PorchStyle-T", "Linear Wrap");
            Values.Add("PorchSpan-T", "Beam");
            Values.Add("PorchSpanProfile-F", "Thin Beam");
            Values.Add("RailTop-T", "Default");
            Values.Add("RailBottom-T", "Default");
            Values.Add("RailBal-T", "Default");
            Values.Add("WinStyle-T", "SH2x-Regal");
            Values.Add("WinCombo-F", "False");
            Values.Add("WinTrimTop-T", "None");
            Values.Add("WinTrimSide-T", "None");
            Values.Add("WinTrimBottom-T", "None Royal");
            Values.Add("WinTrimColor-T", "None White");
            Values.Add("WinShutterColor-T", "None");
            Values.Add("WinShutter-T", "No");
            Values.Add("WinKey-T", "No");
            Values.Add("WinBay-T", "False");
            Values.Add("DoorStyle-T", "Default");
            Values.Add("DoorTrimTop-T", "None");
            Values.Add("DoorTrimSide-T", "None");
            Values.Add("DoorTrimColor-T", "None");
            Values.Add("DatumSplBase-F", "None");
            Values.Add("DatumSplTop-F", "None");
            Values.Add("DatumSplBaseProfile-T", "None");
            Values.Add("Roof-F", "");
            Values.Add("RoofCorniceProfile-F", "");
            Values.Add("Chimney-F", "");
            Values.Add("EmbedCol-F", "");
            Values.Add("Corner-F", "");
            Values.Add("Dormer-F", "");
            Values.Add("FacadeArtic-F", "");


            //int styleIndex = _keys.IndexOf(style);

            //if (styleIndex == -1)
            //{
            //    throw new ArgumentException($"Style '{style}' cannot be find.");
            //}

            //IEnumerable<List<string>> table = _facadesDb.Select(row => row.Values.Select(value => value.ToString()).ToList());

            //Values = table.ToDictionary(row => row[0], row => row[styleIndex]);
        }
    }
}
