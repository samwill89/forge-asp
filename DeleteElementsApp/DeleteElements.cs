/////////////////////////////////////////////////////////////////////
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
using System.Collections.Generic;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using DesignAutomationFramework;
using System.Linq;
using Autodesk.Revit.DB.Structure;

namespace DeleteElements
{
    /// <summary>
    /// Delete elements depends on the input parameters
    /// </summary>
   [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
   [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
   public class DeleteElementsApp : IExternalDBApplication
   {
      public ExternalDBApplicationResult OnStartup(Autodesk.Revit.ApplicationServices.ControlledApplication app)
      {
         DesignAutomationBridge.DesignAutomationReadyEvent += HandleDesignAutomationReadyEvent;
         return ExternalDBApplicationResult.Succeeded;
      }

      public ExternalDBApplicationResult OnShutdown(Autodesk.Revit.ApplicationServices.ControlledApplication app)
      {
         return ExternalDBApplicationResult.Succeeded;
      }

      public void HandleDesignAutomationReadyEvent(object sender, DesignAutomationReadyEventArgs e)
      {
         e.Succeeded = true;
         DeleteAllElements(e.DesignAutomationData);
      }


        //       public static void DeleteAllElements(DesignAutomationData data)
        //       {





        //           //logic goes in here!

        //           if (data == null) throw new ArgumentNullException(nameof(data));

        //           Application rvtApp = data.RevitApp;
        //           if (rvtApp == null) throw new InvalidDataException(nameof(rvtApp));

        //           string modelPath = data.FilePath;
        //           if (String.IsNullOrWhiteSpace(modelPath)) throw new InvalidDataException(nameof(modelPath));

        //           Document revitDoc = data.RevitDoc;
        //           if (revitDoc == null) throw new InvalidOperationException("Could not open document.");


        //           FilteredElementCollector wallCollector = new FilteredElementCollector(revitDoc).OfClass(typeof(Wall));
        //           FilteredElementCollector roofCollector = new FilteredElementCollector(revitDoc).OfClass(typeof(RoofBase));
        //           //elemCollector.OfClass(typeof(Wall));
        //           List<Wall> wallElements = new List<Wall>();
        //           List<RoofBase> roofElements = new List<RoofBase>();
        //           ICollection<ElementId> walls = wallCollector.ToElementIds();
        //           ICollection<ElementId> roofs = roofCollector.ToElementIds();
        //           Element elmnt;
        //           foreach (var i in walls)
        //           {
        //               elmnt = revitDoc.GetElement(i);
        //               wallElements.Add(elmnt as Wall);
        //           }

        //           foreach (var i in roofs)
        //           {
        //               elmnt = revitDoc.GetElement(i);
        //               roofElements.Add(elmnt as RoofBase);
        //           }


        //           //Collecting all types to be used in model
        //           string wcName;
        //           FilteredElementCollector wallTypeColl = new FilteredElementCollector(revitDoc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType();
        //           List<string> wallTypeNames = new List<string>();
        //           foreach (var wc in wallTypeColl)
        //           {
        //               wcName = wc.LookupParameter("Type Name").AsString();
        //               //TaskDialog.Show("WCNAME WALLTYPE", wcName);
        //               wallTypeNames.Add(wcName);
        //           }
        //           string wtName;
        //           FilteredElementCollector windowTypeColl = new FilteredElementCollector(revitDoc).OfCategory(BuiltInCategory.OST_Windows).WhereElementIsElementType();
        //           List<string> windowTypeNames = new List<string>();
        //           foreach (var wt in windowTypeColl)
        //           {
        //               wtName = wt.LookupParameter("Type Name").AsString();
        //               //TaskDialog.Show("WTNAME WALLTYPE", wtName);
        //               windowTypeNames.Add(wtName);
        //           }
        //           string rcName;
        //           FilteredElementCollector roofTypeColl = new FilteredElementCollector(revitDoc).OfCategory(BuiltInCategory.OST_Roofs).WhereElementIsElementType();
        //           List<string> roofTypeName = new List<string>();
        //           foreach (var rc in roofTypeColl)
        //           {
        //               rcName = rc.LookupParameter("Type Name").AsString();
        //               //TaskDialog.Show("RTYPE ROOF TYPE", rcName);
        //               roofTypeName.Add(rcName);
        //           }
        //           //Form1 new_form = new Form1();
        //           //new_form.ShowDialog();

        //           DeleteElementsParams countItParams = DeleteElementsParams.Parse("params.json");

        //           string extMaterials = countItParams.extMaterials;
        //           string materialColor = countItParams.materialColor;
        //           string roofMaterial = countItParams.roofMaterial;
        //           string fenestrationTrim = countItParams.fenestrationTrim;
        //           string columnStyle = countItParams.columnStyle;
        //           //TaskDialog.Show("ext material", extMaterials);
        //           //List<string> wallTypeColl_list = wallTypeColl.ToList();
        //           var wtlist = wallTypeColl.ToList();
        //           //TaskDialog.Show("walltypenamecount", wallTypeNames.Count.ToString());
        //           Element insertElement;
        //           List<Element> ws = new List<Element>();
        //           List<Element> ds = new List<Element>();
        //           Transaction t = new Transaction(revitDoc, "Edit walls");
        //           t.Start();
        //           foreach (var wall in wallElements)
        //           {
        //               ws.Clear();
        //               ds.Clear();
        //               for (int i = 0; i < wallTypeNames.Count; i++)
        //               {
        //                   if (wallTypeNames[i].Contains(extMaterials) & wallTypeNames[i].Contains(materialColor))
        //                   {
        //                       wall.WallType = wallTypeColl.First<Element>(x => x.Name.Equals(wallTypeNames[i].ToString())) as WallType;
        //                   }
        //               }
        //               IList<ElementId> allInserts = wall.FindInserts(true, false, false, false);
        //               foreach (var ai in allInserts)
        //               {
        //                   insertElement = revitDoc.GetElement(ai);
        //                   if (insertElement.Category != null)
        //                   {
        //                       if (insertElement.Category.Name == "Windows")
        //                       {
        //                           ws.Add(insertElement);
        //                       }
        //                       if (insertElement.Category.Name == "Doors")
        //                       {
        //                           ds.Add(insertElement);
        //                       }
        //                   }
        //               }
        //               foreach (Element window in ws)
        //               {
        //                   for (int i = 0; i < windowTypeNames.Count; i++)
        //                   {
        //                       if (windowTypeNames[i].Contains(fenestrationTrim))
        //                       {
        //                           //    window. = windowTypeColl.First<Element>(x => x.Name.Equals(windowTypeNames[i].ToString())) as FamilySymbol;
        //                       }
        //                   }
        //               }
        //           }
        //           t.Commit();

        //           t.Start();

        //           foreach (var roof in roofElements)
        //           {
        //               for (int i = 0; i < roofTypeName.Count; i++)
        //               {
        //                   if (roofTypeName[i].Contains(roofMaterial))
        //                   {
        //                       roof.RoofType = roofTypeColl.First<Element>(x => x.Name.Equals(roofTypeName[i].ToString())) as RoofType;
        //                   }
        //               }

        //           }

        //           t.Commit();

        //           ModelPath path = ModelPathUtils.ConvertUserVisiblePathToModelPath("result.rvt");
        //           revitDoc.SaveAs(path, new SaveAsOptions());

















        //           //=====================================================================
















        //           /*



        //	if (data == null) throw new ArgumentNullException(nameof(data));

        //        Application rvtApp = data.RevitApp;
        //        if (rvtApp == null) throw new InvalidDataException(nameof(rvtApp));

        //        string modelPath = data.FilePath;
        //        if (String.IsNullOrWhiteSpace(modelPath)) throw new InvalidDataException(nameof(modelPath));

        //        Document doc = data.RevitDoc;
        //        if (doc == null) throw new InvalidOperationException("Could not open document.");


        //       // For CountIt workItem: If RvtParameters is null, count all types
        //       DeleteElementsParams deleteElementsParams = DeleteElementsParams.Parse("params.json");

        //       using (Transaction transaction = new Transaction(doc))
        //        {
        //               transaction.Start("Delete Elements");
        //               if (deleteElementsParams.walls)
        //               {
        //                   FilteredElementCollector col = new FilteredElementCollector(doc).OfClass(typeof(Wall));
        //                   doc.Delete(col.ToElementIds());

        //               }
        //               if (deleteElementsParams.floors)
        //               {
        //                   FilteredElementCollector col = new FilteredElementCollector(doc).OfClass(typeof(Floor));
        //                   doc.Delete(col.ToElementIds());
        //               }
        //               if (deleteElementsParams.doors)
        //               {
        //                   FilteredElementCollector collector = new FilteredElementCollector(doc);
        //                   ICollection<ElementId> collection = collector.OfClass(typeof(FamilyInstance))
        //                                                      .OfCategory(BuiltInCategory.OST_Doors)
        //                                                      .ToElementIds();
        //                   doc.Delete(collection);
        //               }
        //               if (deleteElementsParams.windows)
        //               {
        //                   FilteredElementCollector collector = new FilteredElementCollector(doc);
        //                   ICollection<ElementId> collection = collector.OfClass(typeof(FamilyInstance))
        //                                                      .OfCategory(BuiltInCategory.OST_Windows)
        //                                                      .ToElementIds();
        //                   doc.Delete(collection);
        //               }
        //               transaction.Commit();
        //        }

        //        ModelPath path = ModelPathUtils.ConvertUserVisiblePathToModelPath("result.rvt");
        //        doc.SaveAs(path, new SaveAsOptions());

        //*/
        //       }


        public Document _doc;
        private Transaction _tt;

        private CsvData _csvData;

        private DeleteElementsParams countItParams;

        /// <summary>
        /// delete elements depends on the input params.json file
        /// </summary>
        /// <param name="data"></param>
        public void DeleteAllElements(DesignAutomationData data)
      {

			if (data == null) throw new ArgumentNullException(nameof(data));

			Application rvtApp = data.RevitApp;
			if (rvtApp == null) throw new InvalidDataException(nameof(rvtApp));

			string modelPath = data.FilePath;
			if (String.IsNullOrWhiteSpace(modelPath)) throw new InvalidDataException(nameof(modelPath));

			_doc = data.RevitDoc;
			if (_doc == null) throw new InvalidOperationException("Could not open document.");
        

            _tt = new Transaction(_doc, "Generate Facades");

            countItParams =  DeleteElementsParams.Parse("params.json");


            GenerateFacade();


            ModelPath path = ModelPathUtils.ConvertUserVisiblePathToModelPath("result.rvt");
            _doc.SaveAs(path, new SaveAsOptions());


            
      }


        // Convert user input in inches to feet
        private double InchesToFeet(int inches)
        {
            return (double)inches / 12;
        }

        private string[] TerminusFieldSplit(string field)
        {
            return field.Split(' ');
        }

        private string StringArrayToString(string[] items)
        {
            return items.Length == 1
                ? items[0]
                : $"[{string.Join(", ", items.Select(item => $"'{item}'"))}]";
        }

        // Line length or segment distance on XY plane
        private double SegmentDistance(double lsX, double lsY, double leX, double leY)
        {
            return Math.Sqrt(Math.Pow(leX - lsX, 2) + Math.Pow(leY - lsY, 2));
        }

        // Function to find the centroid of a face
        private XYZ GetFaceCentroid(Face face)
        {
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();
            List<double> zValues = new List<double>();

            EdgeArrayArray frameEdgeLoop = face.EdgeLoops;

            foreach (EdgeArray edgeArray in frameEdgeLoop)
            {
                foreach (Edge edge in edgeArray)
                {
                    double edgeP1X = edge.AsCurve().GetEndPoint(0).X;
                    double edgeP2X = edge.AsCurve().GetEndPoint(1).X;
                    double edgeMidX = (edgeP1X + edgeP2X) / 2;

                    double edgeP1Y = edge.AsCurve().GetEndPoint(0).Y;
                    double edgeP2Y = edge.AsCurve().GetEndPoint(1).Y;
                    double edgeMidY = (edgeP1Y + edgeP2Y) / 2;

                    double edgeP1Z = edge.AsCurve().GetEndPoint(0).Z;
                    double edgeP2Z = edge.AsCurve().GetEndPoint(1).Z;
                    double edgeMidZ = (edgeP1Z + edgeP2Z) / 2;

                    xValues.Add(edgeMidX);
                    yValues.Add(edgeMidY);
                    zValues.Add(edgeMidZ);
                }
            }

            double xCen = xValues.Sum() / xValues.Count;
            double yCen = yValues.Sum() / yValues.Count;
            double zCen = zValues.Sum() / zValues.Count;

            XYZ faceCentroid = new XYZ(xCen, yCen, zCen);

            return faceCentroid;
        }

        private string[] GetColor(string colSelection)
        {
            string[] colorBoreal = { "Dark Green", "Medium Blue" };
            string[] colorWhite = { "White", "Eggshell" };
            string[] colorDark = { "Black", "Dark Blue" };
            string[] colorEarth = { "Red", "Brown" };
            string[] colorDesert = { "Yellow", "Orange" };

            string[][] materialColors = { colorBoreal, colorWhite, colorDark, colorEarth, colorDesert };
            string[] materialColNames = { "Boreal", "White", "Dark", "Earth", "Desert" };

            string[] colRange = null;

            if (colSelection != null)
            {
                for (int i = 0; i < materialColors.Length; i++)
                {
                    if (materialColNames[i].Contains(colSelection))
                    {
                        colRange = materialColors[i];
                    }
                }
            }

            return colRange;
        }

        private string GetProfile(string profileSelection)
        {
            string profile = null;

            string[] profileRegal = { "Regal" };
            string[] profileMedium = { "Medium1", "Medium2" };
            string[] profileRoyal = { "Royal" };

            string[][] profileCategories = { profileRegal, profileMedium, profileRoyal };

            foreach (string[] profiles in profileCategories)
            {
                if (profiles.Contains(profileSelection))
                {
                    profile = profiles[0];
                    break;
                }
            }

            return profile;
        }

        private WallData GetWallTypes()
        {
            List<WallType> wallElemTypes = _doc.CollectTypes<WallType>(BuiltInCategory.OST_Walls);

            // Getting the categories from the indices
            string[] mainMaterialCat = TerminusFieldSplit(countItParams.MainMaterial_T);
            string[] baseMaterialCat = TerminusFieldSplit(countItParams.BaseMaterial_T);
            string[] accentMaterialCat = TerminusFieldSplit(countItParams.AccentMaterial_T);

            // Getting the colors from the categories
            string[] mainMaterialColor = TerminusFieldSplit(countItParams.MainMaterialColor_T);
            string[] baseMaterialColor = TerminusFieldSplit(countItParams.BaseMaterialColor_T);
            string[] accentMaterialColor = TerminusFieldSplit(countItParams.AccentMaterialColor_T);

            string[] mMatColRange = GetColor(mainMaterialColor[0]);
            string[] bMatColRange = GetColor(baseMaterialColor[0]);
            string[] aMatColRange = GetColor(accentMaterialColor[0]);

            WallType mainWallType = null;
            WallType baseWallType = null;
            WallType accentWallType = null;

            string mainMatCatDefault = null;
            string mainMatCatColDefault = null;
            string baseMatCatDefault = null;
            string baseMatCatColDefault = null;

            foreach (WallType type in wallElemTypes)
            {
                // Get the main material & color
                if (mainMaterialCat[0] != null && type.Name.Contains(mainMaterialCat[0]))
                {
                    mainMatCatDefault = mainMaterialCat[0];

                    if (mMatColRange[0] != null && type.Name.Contains(mMatColRange[0]))
                    {
                        mainMatCatColDefault = mMatColRange[0];
                        mainWallType = type;
                    }
                }

                // Get the base material & color
                if (baseMaterialCat[0] != null && type.Name.Contains(baseMaterialCat[0]))
                {
                    baseMatCatDefault = baseMaterialCat[0];

                    if (bMatColRange[0] != null && type.Name.Contains(bMatColRange[0]))
                    {
                        baseMatCatColDefault = bMatColRange[0];
                        baseWallType = type;
                    }
                }

                // Get the accent material & color
                if (accentMaterialCat[0] != null && type.Name.Contains(accentMaterialCat[0]))
                {
                    if (aMatColRange[0] != null && type.Name.Contains(aMatColRange[0]))
                    {
                        accentWallType = type;
                    }
                }
            }

            return new WallData(mainWallType, baseWallType, accentWallType,
                mainMatCatDefault, mainMatCatColDefault, baseMatCatDefault, baseMatCatColDefault);
        }

        // Setting the desired wall types & colors
        private void SetExtWallTypes(WallData wallData)
        {
            // Gathering wall data
            IEnumerable<Wall> walls = _doc.CollectElements<Wall>(BuiltInCategory.OST_Walls);

            // Collecting exterior walls
            IEnumerable<Wall> exteriorWalls = walls.Where(wall => wall.IsValidObject && wall.Name.Contains("Exterior"));

            // Changing the walls to match desired type
            _tt.Start();

            foreach (Wall exteriorWall in exteriorWalls)
            {
                if (exteriorWall.Name.Contains("Base"))
                {
                    exteriorWall.WallType = wallData.BaseWallType;
                }
                else if (exteriorWall.Name.Contains("Accent"))
                {
                    exteriorWall.WallType = wallData.AccentWallType;
                }
                else
                {
                    exteriorWall.WallType = wallData.MainWallType;
                }
            }

            _tt.Commit();
        }

        private FamilySymbol CreateColumnType(string specColType)
        {
            List<FamilySymbol> columnTypes = _doc.CollectTypes<FamilySymbol>(BuiltInCategory.OST_Columns);
            List<Material> materialElements = _doc.CollectElements<Material>(BuiltInCategory.OST_Materials);

            string[] colTypeData = specColType.Split('-');

            _tt.Start();

            // Identify the main column type to duplicate
            FamilySymbol targetType = columnTypes.First(type => type.Name.Contains(colTypeData[0]));
            FamilySymbol newColType = (FamilySymbol)targetType.Duplicate(specColType);

            _tt.Commit();

            _tt.Start();

            // Setting the parameters for the new column type
            Material columnMaterial = materialElements.First(
                material => material.Name.Contains(colTypeData[3]) && material.Name.Contains(colTypeData[3]));

            Material plinthMaterial = materialElements.First(
                material => material.Name.Contains(colTypeData[5]) && material.Name.Contains(colTypeData[6]));

            newColType.SetParameter("Top Width Diameter", InchesToFeet(int.Parse(colTypeData[1])));
            newColType.SetParameter("Base Width Diameter", InchesToFeet(int.Parse(colTypeData[2])));
            newColType.SetParameter("Column Material", columnMaterial.Id);
            newColType.SetParameter("Plinth Material", plinthMaterial.Id);

            _tt.Commit();

            return newColType;
        }

        // Getting the columns
        private ColumnData GetColumnData(WallData wallData)
        {
            List<FamilySymbol> columnTypes = _doc.CollectTypes<FamilySymbol>(BuiltInCategory.OST_Columns);

            // Getting the categories from the indices
            string[] columnStyleCat = TerminusFieldSplit(countItParams.ColumnStyle_F);
            string[] columnMatCat = TerminusFieldSplit(countItParams.ColumnMat_F);
            string[] columnColorCat = TerminusFieldSplit(countItParams.ColumnColor_T);
            string[] columnCapitalCat = TerminusFieldSplit(countItParams.ColumnCapitalStyle_T);
            string[] columnBaseCat = TerminusFieldSplit(countItParams.ColumnBaseStyle_T);
            string[] columnPlinthHtCat = TerminusFieldSplit(countItParams.ColumnPlinthHeight_T);
            string[] columnTopWdCat = TerminusFieldSplit(countItParams.ColumnTopWidth_F);
            string[] columnBaseWdCat = TerminusFieldSplit(countItParams.ColumnBaseWidth_F);
            string[] columnPlinthStyleCat = TerminusFieldSplit(countItParams.ColumnPlinthStyle_F);

            string[] colColRange = GetColor(columnColorCat[0]);

            string colCapName = null;
            string colBaseName = null;

            if (columnCapitalCat != null)
            {
                colCapName = GetProfile(columnCapitalCat[0]);
            }

            if (columnBaseCat != null)
            {
                colBaseName = GetProfile(columnBaseCat[0]);
            }

            // Column Type name is type-topWd-bottomWd-topMat-bottomMat; eg "Round-0.85-1.1-Stone-Stone"
            // Plinth material matches base material for the wall

            if (wallData.BaseMaterialCategory == null)
            {
                wallData.BaseMaterialCategory = wallData.MainMaterialCategory;
                wallData.BaseMaterialColor = wallData.MainMaterialColor;
            }

            string specColType = $"{StringArrayToString(columnStyleCat)}-{StringArrayToString(columnTopWdCat)}-{StringArrayToString(columnBaseWdCat)}-{columnMatCat[0]}-{colColRange[0]}-{wallData.BaseMaterialCategory}-{wallData.BaseMaterialColor}";

            FamilySymbol columnType = columnTypes.All(type => type.Name != specColType)
                ? CreateColumnType(specColType)
                : columnTypes.FirstOrDefault(type => type.Name.Contains(specColType));

            var column = new ColumnData(
                columnType,
                colCapName,
                colBaseName,
                columnPlinthStyleCat[0],
                int.Parse(columnBaseWdCat[0]),
                int.Parse(columnPlinthHtCat[0]));

            return column;
        }

        private void SetColumnData(ColumnData column)
        {
            // Collecting all columns in the model
            List<FamilyInstance> columnElements = _doc.CollectElements<FamilyInstance>(BuiltInCategory.OST_Columns);

            // Get the height of the column from the level information
            List<Level> levelColl = _doc.CollectElements<Level>(BuiltInCategory.OST_Levels);

            double levelOneEle = 0;
            double levelTwoEle = 0;

            foreach (Level level in levelColl)
            {
                if (level.Name.Contains("LEVEL 1") || level.Name.Contains("LEVEL ONE"))
                {
                    levelOneEle = level.Elevation;
                }
                else if (level.Name.Contains("LEVEL 2") || level.Name.Contains("LEVEL TWO"))
                {
                    levelTwoEle = level.Elevation;
                }
            }

            double height = levelTwoEle - levelOneEle;

            // Set all columns to match updated column type
            _tt.Start();

            foreach (FamilyInstance element in columnElements)
            {
                element.Symbol = column.Type;

                // Column instances are base & capital styles, plinth height, plinth width, plinth style
                element.SetParameter("Base Style", column.BaseName);
                element.SetParameter("Capital Style", column.CapitalName);
                element.SetParameter("Plinth Style", column.PlinthName);
                element.SetParameter("Plinth Width", InchesToFeet(column.PlinthWidth + 3));
                element.SetParameter("Plinth Height", column.PlinthHeight);
                element.SetParameter("Height", height);
            }

            _tt.Commit();
        }

        private ElementType CreateTrimType(ElementType type, string trimCol, string shutCol)
        {
            List<Material> materialElements = _doc.CollectElements<Material>(BuiltInCategory.OST_Materials);

            string newTypeName = $"{type.FamilyName}-{trimCol}-{shutCol}";

            _tt.Start();

            ElementType newTrimType = type.Duplicate(newTypeName);

            // Retrieving the materials to match the desired type
            Material trimMat = null;
            Material shutterMat = null;

            foreach (Material material in materialElements.Where(material => material.Name.Contains("Wood")))
            {
                if (material.Name.Contains(trimCol))
                {
                    trimMat = material;
                }

                if (material.Name.Contains(shutCol))
                {
                    shutterMat = material;
                }
            }

            // Setting the parameters for the new trim type
            if (trimMat != null)
            {
                newTrimType.SetParameter("Material", trimMat.Id);
            }

            if (shutterMat != null)
            {
                newTrimType.SetParameter("Shutter Material", shutterMat.Id);
            }

            _tt.Commit();

            return newTrimType;
        }

        private WindowData GetWindowTypes()
        {
            // Collect existing types
            List<FamilySymbol> windowTypes = _doc.CollectTypes<FamilySymbol>(BuiltInCategory.OST_Windows);
            List<ElementType> genericTypes = _doc.CollectTypes<ElementType>(BuiltInCategory.OST_GenericModel);

            // Getting the categories from the indices
            string[] winStyleCat = TerminusFieldSplit(countItParams.WinStyle_T);
            string[] winComboCat = TerminusFieldSplit(countItParams.WinCombo_F);
            string[] winTrimTopCat = TerminusFieldSplit(countItParams.WinTrimTop_T);
            string[] winTrimSideCat = TerminusFieldSplit(countItParams.WinTrimSide_T);
            string[] winTrimBottomCat = TerminusFieldSplit(countItParams.WinTrimBottom_T);
            string[] winTrimColorCat = TerminusFieldSplit(countItParams.WinTrimColor_T);
            string[] winShutterColorCat = TerminusFieldSplit(countItParams.WinShutterColor_T);
            string[] winShutterCat = TerminusFieldSplit(countItParams.WinShutter_T);
            string[] winKeyCat = TerminusFieldSplit(countItParams.WinKey_T);

            // Getting and creating new window trim types
            string newTrimTypeName = $"Window Framing-{winTrimColorCat[0]}-{winShutterColorCat[0]}";

            ElementType trimType = genericTypes.First(type => type.Name.Contains("Window Framing"));
            ElementType newTrimType = genericTypes.First(type => type.Name.Contains(newTrimTypeName));

            if (genericTypes.All(type => type.Name != newTrimTypeName) && winTrimColorCat[0] != null)
            {
                newTrimType = CreateTrimType(trimType, winTrimColorCat[0], winShutterColorCat[0]);
            }

            // Filter to window style types
            List<FamilySymbol> winStyleTypes = windowTypes.Where(type => type.Name.Contains(StringArrayToString(winStyleCat))).ToList();

            // ADD - Output the "WinCombo" data and merge with related function
            var trim = new WindowTrimData(newTrimType, winTrimTopCat[0], winTrimSideCat[0], winTrimBottomCat[0]);
            var window = new WindowData(winStyleCat[0], winStyleTypes, trim, winShutterCat[0], winKeyCat[0]);

            return window;
        }

        private FamilySymbol CreateWindowType(FamilySymbol type, string winStyleCat, double width, double height)
        {
            string newTypeName = $"{winStyleCat}-{Math.Round((double)((int)width * 12))}-{Math.Round((double)((int)height * 12))}";

            List<FamilySymbol> windowTypes = _doc.CollectTypes<FamilySymbol>(BuiltInCategory.OST_Windows);
            FamilySymbol newWinType = windowTypes.FirstOrDefault(windowType => windowType.Name == newTypeName);

            if (newWinType == null)
            {
                newWinType = (FamilySymbol)type.Duplicate(newTypeName);

                // Setting the parameters for the new window type
                newWinType.SetParameter("Width", width);
                newWinType.SetParameter("Height", height);
            }

            return newWinType;
        }

        private void SetWindowData(WindowData windowData)
        {
            // Collect existing windows
            List<FamilyInstance> windowElements = _doc.CollectElements<FamilyInstance>(BuiltInCategory.OST_Windows);

            List<FamilyInstance> newTrims = new List<FamilyInstance>();

            _tt.Start();

            // Get existing window sizes
            foreach (FamilyInstance window in windowElements)
            {
                // Get the type of window to retrieve it's data
                Element wnTp = _doc.GetElement(window.GetTypeId());
                double winHt = wnTp.get_Parameter(BuiltInParameter.WINDOW_HEIGHT).AsDouble();
                double winWd = wnTp.get_Parameter(BuiltInParameter.WINDOW_WIDTH).AsDouble();
                HostObject winHost = window.Host as HostObject;
                Level winLevel = (Level)_doc.GetElement(window.LevelId);
                XYZ winLocation = ((LocationPoint)window.Location).Point;

                string newTypeName = $"{windowData.StyleCategory}-{Math.Round((double)((int)winWd * 12))}-{Math.Round((double)((int)winHt * 12))}";

                window.Symbol = windowData.Types.FirstOrDefault(type => type.Name == newTypeName)
                                ?? CreateWindowType(windowData.Types[0], windowData.StyleCategory, winWd, winHt);

                // Create new window trim at window locations on the exterior face of the host wall if on a window of specific size ranges
                if (windowData.Trim?.Type != null && (winWd > 1.9 && winWd < 5.9) && (winHt > 1.9 && winHt < 5.9))
                {
                    IList<Reference> sideFaces = HostObjectUtils.GetSideFaces(winHost, ShellLayerType.Exterior);
                    Face face = _doc.GetElement(sideFaces[0]).GetGeometryObjectFromReference(sideFaces[0]) as Face;
                    UV uvLoc = face.Project(winLocation).UVPoint;
                    XYZ normal = face.ComputeNormal(uvLoc);
                    XYZ refDir = normal.CrossProduct(XYZ.BasisZ);
                    FamilyInstance winTrim = _doc.Create.NewFamilyInstance(winLocation, (FamilySymbol)windowData.Trim.Type, winHost, StructuralType.NonStructural);

                    newTrims.Add(winTrim);

                    // Change the width and height of the newly created trims
                    winTrim.SetParameter("Opening Height", winHt);
                    winTrim.SetParameter("Opening Width", winWd);
                }
            }

            // Setting the parameters for the newly placed window trim
            if (newTrims.Count > 0)
            {
                foreach (FamilyInstance trim in newTrims)
                {
                    trim.SetParameter("Top Profile", windowData.Trim.TopCategory);
                    trim.SetParameter("Bottom Profile", windowData.Trim.BottomCategory);
                    trim.SetParameter("Side Profile", windowData.Trim.SideCategory);
                    trim.SetParameter("Shutter", windowData.ShutterCategory);
                    trim.SetParameter("Top Key", windowData.KeyCategory);
                }
            }

            _tt.Commit();
        }

        private (string[], string[], Element) GetWallDatumData()
        {
            List<ElementType> cornicesTypes = _doc.CollectTypes<ElementType>(BuiltInCategory.OST_Cornices);

            // Getting the categories from the indices
            string[] datumSplBaseCat = TerminusFieldSplit(countItParams.DatumSplBase_F);
            string[] datumSplTopCat = TerminusFieldSplit(countItParams.DatumSplTop_F);
            string[] datumSplBaseProfileCat = TerminusFieldSplit(countItParams.DatumSplBaseProfile_T);

            // Find the profile which matches the value
            Element profileType = null;

            // for sn, st in zip(sweepTypeNames, sweepElemTypes):
            // if str(datumSplBaseProfileCatIdx[0]) in sn:
            // profileType = st

            return (datumSplBaseCat, datumSplTopCat, profileType);
        }

        private Wall CreateNewWall(Wall oldWall, bool baseBool, object dbData)
        {
            // Getting wall data from old wall for use in new wall
            string locationLine = "Core Face: Interior";

            oldWall.SetParameterAsString("Location Line", locationLine);

            Parameter baseOffset = oldWall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            Parameter topOffset = oldWall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET);
            Parameter isTopAttached = oldWall.get_Parameter(BuiltInParameter.WALL_TOP_IS_ATTACHED);

            string oldWallBaseConstraint = oldWall.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT).AsValueString();
            string oldWallTopConstraint = oldWall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsValueString();

            double unconnectedHeight = oldWall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble();

            Curve wallLoc = ((LocationCurve)oldWall.Location).Curve;

            // Create new wall
            Wall newWall = Wall.Create(_doc, wallLoc.Clone(), oldWall.LevelId, false);

            // Get and set new parameters
            newWall.SetParameterAsString("Location Line", locationLine);
            newWall.SetParameter("Base Constraint", oldWallBaseConstraint);
            newWall.SetParameter("Top Constraint", oldWallTopConstraint);

            // Setting the offsets based on the db inputs
            Parameter topOffParam = newWall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET);
            Parameter baseOffParam = newWall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            Parameter topAttachParam = newWall.get_Parameter(BuiltInParameter.WALL_TOP_IS_ATTACHED);
            Parameter unconnectedHeightParam = newWall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM);

            if (baseBool)
            {
                if (dbData is string dbDataAsString && dbDataAsString == "LevelTop")
                {
                    baseOffset.Set(unconnectedHeight - 0.5);
                    topOffParam.Set(-0.5);
                }
                else if (dbData is double dbDataAsDouble)
                {
                    baseOffset.Set(dbDataAsDouble);
                    unconnectedHeightParam.Set(dbDataAsDouble);
                }
            }
            else
            {
                if (dbData is string dbDataAsString && dbDataAsString == "LevelTop")
                {
                    topOffset.Set(-0.5);
                    baseOffParam.Set(unconnectedHeight - 0.5);
                }
                else if (dbData is double dbDataAsDouble)
                {
                    topOffset.Set(dbDataAsDouble);
                    baseOffParam.Set(unconnectedHeight - dbDataAsDouble);
                }

                topAttachParam.Set(isTopAttached.AsValueString());
                isTopAttached.Set("No");
            }

            return newWall;
        }

        private void SetWallDatumData(string[] datumSplBaseCat, string[] datumSplTopCat, WallType baseWallType, WallType accentWallType, Element profileType)
        {
            List<Wall> walls = _doc.CollectElements<Wall>(BuiltInCategory.OST_Walls);
            List<Level> levels = _doc.CollectElements<Level>(BuiltInCategory.OST_Levels);

            // Collect and sort Exterior walls
            List<Wall> exteriorWalls = walls.Where(wall => wall.Name.Contains("Exterior")).ToList();

            // Place according to levels
            List<Wall> levelOneWalls = new List<Wall>();
            List<Wall> levelTwoWalls = new List<Wall>();
            List<Wall> levelThreeWalls = new List<Wall>();

            foreach (Wall wall in exteriorWalls)
            {
                string levelName = _doc.GetElement(wall.LevelId).Name;

                if (levelName.Contains("1") || levelName.Contains("ONE"))
                {
                    levelOneWalls.Add(wall);
                }
                else if (levelName.Contains("2") || levelName.Contains("TWO"))
                {
                    levelTwoWalls.Add(wall);
                }
                else if (levelName.Contains("3") || levelName.Contains("THREE"))
                {
                    levelThreeWalls.Add(wall);
                }
            }

            // Determine which level is the top most level
            List<Wall> topLevelWalls = levelThreeWalls.Count == 0 ? levelTwoWalls : levelThreeWalls;

            // Creating and setting the wall data
            if (!StringArrayToString(datumSplBaseCat).Contains("None"))
            {
                int datumSplBaseCatAsInteger;

                if (datumSplBaseCat[0] == "LevelTop")
                {
                    datumSplBaseCatAsInteger = 108;
                }
                else if (int.TryParse(datumSplBaseCat[0], out int value))
                {
                    datumSplBaseCatAsInteger = value;
                }
                else
                {
                    datumSplBaseCatAsInteger = 38;
                }

                _tt.Start();
                foreach (Wall wall in levelOneWalls)
                {
                    Wall newBaseWall = CreateNewWall(wall, true, InchesToFeet(datumSplBaseCatAsInteger));
                    newBaseWall.WallType = baseWallType;
                }
                _tt.Commit();
            }

            if (!StringArrayToString(datumSplTopCat).Contains("None"))
            {
                int datumSplTopCatAsInteger;

                if (datumSplTopCat[0] == "LevelTop")
                {
                    datumSplTopCatAsInteger = 108;
                }
                else if (int.TryParse(datumSplTopCat[0], out int value))
                {
                    datumSplTopCatAsInteger = value;
                }
                else
                {
                    datumSplTopCatAsInteger = 108;
                }

                _tt.Start();
                foreach (Wall wall in topLevelWalls)
                {
                    Wall newTopWall = CreateNewWall(wall, false, InchesToFeet(datumSplTopCatAsInteger));
                    newTopWall.WallType = accentWallType;
                }
                _tt.Commit();
            }

            // ADD - Addition of profile into CreateNewWall function
            // ADD - Function to remove and set back if new facade style doesn't call for a base wall
        }

        private void GenerateFacade()
        {
            WallData wallData = GetWallTypes();

            string[] datumSplBaseCat;
            string[] datumSplTopCat;
            Element profileType;

            (datumSplBaseCat, datumSplTopCat, profileType) = GetWallDatumData();
            SetWallDatumData(datumSplBaseCat, datumSplTopCat, wallData.BaseWallType, wallData.AccentWallType, profileType);

            SetExtWallTypes(wallData);

            ColumnData columnData = GetColumnData(wallData);
            SetColumnData(columnData);

            //WindowData windowData = GetWindowTypes();
            //SetWindowData(windowData);
        }

    }
}
