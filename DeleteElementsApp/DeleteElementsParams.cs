﻿/////////////////////////////////////////////////////////////////////
// Copyright (c) Autodesk, Inc. All rights reserved
// Written by Forge Partner Development
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
/////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using Newtonsoft.Json;


namespace DeleteElements
{
    /// <summary>
    /// DeleteElementsParams is used to parse the input json parameters
    /// </summary>
    internal class DeleteElementsParams
    {
        public string Style { get; set; }
        public string MainMaterial_T { get; set; }
        public string MainMaterialColor_T { get; set; }
        public string BaseMaterial_T { get; set; }
        public string BaseMaterialColor_T { get; set; }
        public string AccentMaterial_T { get; set; }
        public string AccentMaterialColor_T { get; set; }
        public string ColumnStyle_F { get; set; }
        public string ColumnPairing_F { get; set; }
        public string ColumnMat_F { get; set; }
        public string ColumnColor_T { get; set; }
        public string ColumnCapitalStyle_T { get; set; }
        public string ColumnBaseStyle_T { get; set; }
        public string ColumnTopWidth_F { get; set; }
        public string ColumnBaseWidth_F { get; set; }
        public string ColumnPlinthHeight_T { get; set; }
        public string ColumnPlinthStyle_F { get; set; }
        public string PorchStyle_T { get; set; }
        public string PorchSpan_T { get; set; }
        public string PorchSpanProfile_F { get; set; }
        public string RailTop_T { get; set; }
        public string RailBottom_T { get; set; }
        public string RailBal_T { get; set; }
        public string WinStyle_T { get; set; }
        public string WinCombo_F { get; set; }
        public string WinTrimTop_T { get; set; }
        public string WinTrimSide_T { get; set; }
        public string WinTrimBottom_T { get; set; }
        public string WinTrimColor_T { get; set; }
        public string WinShutterColor_T { get; set; }
        public string WinShutter_T { get; set; }
        public string WinKey_T { get; set; }
        public string WinBay_T { get; set; }
        public string DoorStyle_T { get; set; }
        public string DoorTrimTop_T { get; set; }
        public string DoorTrimSide_T { get; set; }
        public string DoorTrimColor_T { get; set; }
        public string DatumSplBase_F { get; set; }
        public string DatumSplTop_F { get; set; }
        public string DatumSplBaseProfile_T { get; set; }
        public string Roof_F { get; set; }
        public string RoofCorniceProfile_F { get; set; }
        public string Chimney_F { get; set; }
        public string EmbedCol_F { get; set; }
        public string Corner_F { get; set; }
        public string Dormer_F { get; set; }
        public string FacadeArtic_F { get; set; }



        static public DeleteElementsParams Parse(string jsonPath)
        {
            try
            {
                //if (!File.Exists(jsonPath))
                //    return new DeleteElementsParams {
                //        Style = "Bungalow",
                //        MainMaterial_T = "Siding",
                //        MainMaterialColor_T = "Boreal",
                //        BaseMaterial_T = "Brick",
                //        BaseMaterialColor_T = "Earth",
                //        AccentMaterial_T = "Siding",
                //        AccentMaterialColor_T = "Earth",
                //        ColumnStyle_F = "Rectangular",
                //        ColumnPairing_F = "Single",
                //        ColumnMat_F = "Wood",
                //        ColumnColor_T = "White",
                //        ColumnCapitalStyle_T = "Regal",
                //        ColumnBaseStyle_T = "Regal",
                //        ColumnTopWidth_F = "8",
                //        ColumnBaseWidth_F = "11",
                //        ColumnPlinthHeight_T = "38",
                //        ColumnPlinthStyle_F = "Royal",
                //        PorchStyle_T = "Linear",
                //        PorchSpan_T = "Arch Beam",
                //        PorchSpanProfile_F = "Default",
                //        RailTop_T = "Default",
                //        RailBottom_T = "Default",
                //        RailBal_T = "Default",
                //        WinStyle_T = "SH2x-Regal",
                //        WinCombo_F = "FALSE",
                //        WinTrimTop_T = "Regal",
                //        WinTrimSide_T = "Regal",
                //        WinTrimBottom_T = "Regal",
                //        WinTrimColor_T = "White",
                //        WinShutterColor_T = "None",
                //        WinShutter_T = "No",
                //        WinKey_T = "No",
                //        WinBay_T = "FALSE",
                //        DoorStyle_T = "Default",
                //        DoorTrimTop_T = "Regal",
                //        DoorTrimSide_T = "Regal",
                //        DoorTrimColor_T = "White",
                //        DatumSplBase_F = "38",
                //        DatumSplTop_F = "LevelTop",
                //        DatumSplBaseProfile_T = "None",
                //        Roof_F = "",
                //        RoofCorniceProfile_F = "",
                //        Chimney_F = "",
                //        EmbedCol_F = "",
                //        Corner_F = "",
                //        Dormer_F = "",
                //        FacadeArtic_F = ""
                //    };

                string jsonContents = File.ReadAllText(jsonPath);
                return JsonConvert.DeserializeObject<DeleteElementsParams>(jsonContents);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception when parsing json file: " + ex);
                return null;
            }
        }


    }
}
