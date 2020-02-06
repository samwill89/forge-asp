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

        /// <summary>
        /// delete elements depends on the input params.json file
        /// </summary>
        /// <param name="data"></param>
        public void DeleteAllElements(DesignAutomationData data)
      {





			//logic goes in here!

			if (data == null) throw new ArgumentNullException(nameof(data));

			Application rvtApp = data.RevitApp;
			if (rvtApp == null) throw new InvalidDataException(nameof(rvtApp));

			string modelPath = data.FilePath;
			if (String.IsNullOrWhiteSpace(modelPath)) throw new InvalidDataException(nameof(modelPath));

			_doc = data.RevitDoc;
			if (_doc == null) throw new InvalidOperationException("Could not open document.");
        

            _tt = new Transaction(_doc, "Generate Facades");
            _csvData = new CsvData();
            _csvData.SetStyle("FarmHouse");


            GenerateFacade();


            ModelPath path = ModelPathUtils.ConvertUserVisiblePathToModelPath("result.rvt");
            _doc.SaveAs(path, new SaveAsOptions());


            
      }


        private double InchesToFeet(int inches)
        {
            return (double)inches / 12;
        }

        private string GetFirstTerm(string field, char separator = ' ')
        {
            return field.Split(separator)[0];
        }

        private string GetColor(string colSelection)
        {
            string color = null;

            string[] colorBoreal = { "Dark Green", "Medium Blue" };
            string[] colorWhite = { "White", "Eggshell" };
            string[] colorDark = { "Black", "Dark Blue" };
            string[] colorEarth = { "Red", "Brown" };
            string[] colorDesert = { "Yellow", "Orange" };

            string[][] materialColors = { colorBoreal, colorWhite, colorDark, colorEarth, colorDesert };

            foreach (string[] colors in materialColors)
            {
                if (colors.Contains(colSelection))
                {
                    color = colors[0];
                    break;
                }
            }

            return color;
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
            string mainMaterialCat = GetFirstTerm(_csvData.Values["MainMaterial-T"]);
            string baseMaterialCat = GetFirstTerm(_csvData.Values["BaseMaterial-T"]);
            string accentMaterialCat = GetFirstTerm(_csvData.Values["AccentMaterial-T"]);

            // Getting the colors from the categories
            string mainMaterialColor = GetColor(_csvData.Values["MainMaterialColor-T"]);
            string baseMaterialColor = GetColor(_csvData.Values["BaseMaterialColor-T"]);
            string accentMaterialColor = GetColor(_csvData.Values["AccentMaterialColor-T"]);

            WallType mainWallType = null;
            WallType baseWallType = null;
            WallType accentWallType = null;

            foreach (WallType type in wallElemTypes)
            {
                // Get the main material & color
                if (type.Name.Contains(mainMaterialCat) && type.Name.Contains(mainMaterialColor))
                {
                    mainWallType = type;
                }

                // Get the base material & color
                if (type.Name.Contains(baseMaterialCat) && type.Name.Contains(baseMaterialColor))
                {
                    baseWallType = type;
                }

                // Get the accent material & color
                if (type.Name.Contains(accentMaterialCat) && type.Name.Contains(accentMaterialColor))
                {
                    accentWallType = type;
                }
            }

            return new WallData(mainWallType, baseWallType, accentWallType, baseMaterialCat, baseMaterialColor);
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
            FamilySymbol targetType = columnTypes.FirstOrDefault(type => type.Name.Contains(colTypeData[0]));
            FamilySymbol newColType = (FamilySymbol)targetType.Duplicate(specColType);

            _tt.Commit();

            // Setting the parameters for the new column type
            IList<Parameter> topWdParam = newColType.GetParameters("Top Width Diameter");
            foreach (Parameter parameter in topWdParam)
            {
                parameter.Set(InchesToFeet(int.Parse(colTypeData[1])));
            }

            IList<Parameter> baseWidthParam = newColType.GetParameters("Base Width Diameter");
            foreach (Parameter parameter in baseWidthParam)
            {
                parameter.Set(InchesToFeet(int.Parse(colTypeData[2])));
            }

            IList<Parameter> colMatParam = newColType.GetParameters("Column Material");
            IList<Parameter> plinthMatParam = newColType.GetParameters("Plinth Material");

            Material columnMaterial = materialElements.FirstOrDefault(
                material => material.Name.Contains(colTypeData[3]) && material.Name.Contains(colTypeData[4]));

            Material plinthMaterial = materialElements.FirstOrDefault(
                material => material.Name.Contains(colTypeData[5]) && material.Name.Contains(colTypeData[6]));

            _tt.Start();

            foreach (Parameter parameter in colMatParam)
            {
                parameter.Set(columnMaterial.Id);
            }

            foreach (Parameter parameter in plinthMatParam)
            {
                parameter.Set(plinthMaterial.Id);
            }

            _tt.Commit();

            return newColType;
        }

        // Getting the columns
        private ColumnData GetColumnData(WallData wallData)
        {
            List<FamilySymbol> columnTypes = _doc.CollectTypes<FamilySymbol>(BuiltInCategory.OST_Columns);

            // Getting the categories from the indices
            string columnStyleCat = _csvData.Values["ColumnStyle-F"];
            string columnMatCat = _csvData.Values["ColumnMat-F"];
            string columnColorCat = _csvData.Values["ColumnColor-T"];
            string columnCapitalCat = _csvData.Values["ColumnCapitalStyle-T"];
            string columnBaseCat = _csvData.Values["ColumnBaseStyle-T"];
            string columnPlinthHtCat = _csvData.Values["ColumnPlinthHeight-T"];
            string columnTopWdCat = _csvData.Values["ColumnTopWidth-F"];
            string columnPlinthWdCat = _csvData.Values["ColumnBaseWidth-F"];
            string columnPlinthStyleCat = _csvData.Values["ColumnPlinthStyle-F"];

            string colCapName = null;
            string colBaseName = null;

            if (columnCapitalCat != null)
            {
                colCapName = GetProfile(columnCapitalCat);
            }

            if (columnBaseCat != null)
            {
                colBaseName = GetProfile(columnBaseCat);
            }

            // Column Type name is type-topWd-bottomWd-topMat-bottomMat; eg "Round-0.85-1.1-Stone-Stone"
            // Plinth material matches base material for the wall
            string specColType = $"{columnStyleCat}-{columnTopWdCat}-{columnPlinthWdCat}-{columnMatCat}-{columnColorCat}-{wallData.MaterialCategory}-{wallData.MaterialColor}";

            FamilySymbol columnType = columnTypes.All(type => type.Name != specColType)
                ? CreateColumnType(specColType)
                : columnTypes.FirstOrDefault(type => type.Name.Contains(specColType));

            var column = new ColumnData(
                columnType,
                colCapName,
                colBaseName,
                columnPlinthStyleCat,
                int.Parse(columnPlinthWdCat),
                int.Parse(columnPlinthHtCat));

            return column;
        }

        private void SetColumnData(ColumnData column)
        {
            // Collecting all columns in the model
            List<FamilyInstance> columnElements = _doc.CollectElements<FamilyInstance>(BuiltInCategory.OST_Columns);

            // Set all columns to match updated column type
            _tt.Start();

            foreach (FamilyInstance element in columnElements)
            {
                element.Symbol = column.Type;

                // Column instances are base & capital styles, plinth height, plinth width, plinth style
                IList<Parameter> colBaseParam = element.GetParameters("Base Style");
                foreach (Parameter parameter in colBaseParam)
                {
                    parameter.Set(column.BaseName);
                }

                IList<Parameter> colCapParam = element.GetParameters("Capital Style");
                foreach (Parameter parameter in colCapParam)
                {
                    parameter.Set(column.CapitalName);
                }

                IList<Parameter> colPlinthParam = element.GetParameters("Plinth Style");
                foreach (Parameter parameter in colPlinthParam)
                {
                    parameter.Set(column.PlinthName);
                }

                IList<Parameter> colPWidParam = element.GetParameters("Plinth Width");
                foreach (Parameter parameter in colPWidParam)
                {
                    parameter.Set(InchesToFeet(column.PlinthWidth + 3));
                }

                IList<Parameter> colPHtParam = element.GetParameters("Plinth Height");
                foreach (Parameter parameter in colPHtParam)
                {
                    parameter.Set(InchesToFeet(column.PlinthHeight));
                }
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
                IList<Parameter> matParam = newTrimType.GetParameters("Material");
                foreach (Parameter parameter in matParam)
                {
                    parameter.Set(trimMat.Id);
                }
            }

            if (shutterMat != null)
            {
                IList<Parameter> smParam = newTrimType.GetParameters("Shutter Material");
                foreach (Parameter parameter in smParam)
                {
                    parameter.Set(shutterMat.Id);
                }
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
            string winStyleCat = GetFirstTerm(_csvData.Values["WinStyle-T"]);
            string winTrimColorCat = GetFirstTerm(_csvData.Values["WinTrimColor-T"]);
            string winShutterColorCat = GetFirstTerm(_csvData.Values["WinShutterColor-T"]);
            string winTrimTopCat = _csvData.Values["WinTrimTop-T"];
            string winTrimSideCat = _csvData.Values["WinTrimSide-T"];
            string winTrimBottomCat = _csvData.Values["WinTrimBottom-T"];
            string winShutterCat = _csvData.Values["WinShutter-T"];
            string winKeyCat = _csvData.Values["WinKey-T"];

            // Getting and creating new window trim types
            string newTrimTypeName = $"Window Framing-{winTrimColorCat}-{winShutterColorCat}";

            ElementType trimType = genericTypes.FirstOrDefault(type => type.Name.Contains("Window Framing"));
            ElementType newTrimType = genericTypes.FirstOrDefault(type => type.Name.Contains(newTrimTypeName));

            if (genericTypes.All(type => type.Name != newTrimTypeName))
            {
                newTrimType = CreateTrimType(trimType, winTrimColorCat, winShutterColorCat);
            }

            // Filter to window style types
            List<FamilySymbol> winStyleTypes = windowTypes.Where(type => type.Name.Contains(winStyleCat)).ToList();

            // ADD - Output the "WinCombo" data and merge with related function
            var trim = new WindowTrimData(newTrimType, winTrimTopCat, winTrimSideCat, winTrimBottomCat);
            var window = new WindowData(winStyleCat, winStyleTypes, trim, winShutterCat, winKeyCat);

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
                Parameter widthParameter = newWinType.get_Parameter(BuiltInParameter.WINDOW_WIDTH);
                widthParameter.Set(width);

                Parameter heightParameter = newWinType.get_Parameter(BuiltInParameter.WINDOW_HEIGHT);
                heightParameter.Set(height);
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
                Element winHost = window.Host;
                Level winLevel = (Level)_doc.GetElement(window.LevelId);
                XYZ winLocation = ((LocationPoint)window.Location).Point;

                string newTypeName = $"{windowData.StyleCategory}-{Math.Round((double)((int)winWd * 12))}-{Math.Round((double)((int)winHt * 12))}";

                window.Symbol = windowData.Types.FirstOrDefault(type => type.Name == newTypeName)
                                ?? CreateWindowType(windowData.Types[0], windowData.StyleCategory, winWd, winHt);

                // Create new window trim at window locations
                FamilyInstance winTrim = _doc.Create.NewFamilyInstance(winLocation, (FamilySymbol)windowData.Trim.Type, winHost, winLevel, StructuralType.NonStructural);

                // #ADD - Find a way to orient the trim on the same face as its referenced window. Get center line of host wall via face with the highest z-value, center of edges with shortest lengths
                // Then use that to create a plane to mirror the trim family if the FacingOrientations of the windows and the trims are not equal
                // winTrim.FacingOrientation = winOrientation
                newTrims.Add(winTrim);
            }

            // Setting the parameters for the newly placed window trim
            foreach (FamilyInstance trim in newTrims)
            {
                IList<Parameter> trimTopParam = trim.GetParameters("Top Profile");
                foreach (Parameter parameter in trimTopParam)
                {
                    parameter.Set(windowData.Trim.TopCategory);
                }

                IList<Parameter> trimBottomParam = trim.GetParameters("Bottom Profile");
                foreach (Parameter parameter in trimBottomParam)
                {
                    parameter.Set(windowData.Trim.BottomCategory);
                }

                IList<Parameter> trimSideParam = trim.GetParameters("Side Profile");
                foreach (Parameter parameter in trimSideParam)
                {
                    parameter.Set(windowData.Trim.SideCategory);
                }

                IList<Parameter> trimShutterParam = trim.GetParameters("Shutter");
                foreach (Parameter parameter in trimShutterParam)
                {
                    parameter.Set(windowData.ShutterCategory);
                }

                IList<Parameter> trimKeyParam = trim.GetParameters("Top Key");
                foreach (Parameter parameter in trimKeyParam)
                {
                    parameter.Set(windowData.KeyCategory);
                }
            }

            _tt.Commit();
        }

        private (string, string, Element) GetWallDatumData()
        {
            List<ElementType> cornicesTypes = _doc.CollectTypes<ElementType>(BuiltInCategory.OST_Cornices);

            // Getting the categories from the idices
            string datumSplBaseCat = _csvData.Values["DatumSplBase-F"];
            string datumSplTopCat = _csvData.Values["DatumSplTop-F"];
            string datumSplBaseProfileCat = _csvData.Values["DatumSplBaseProfile-T"];

            // Find the profile which matches the value
            Element profileType = null;

            // for sn, st in zip(sweepTypeNames, sweepElemTypes):
            // if str(datumSplBaseProfileCatIdx[0]) in sn:
            // profileType = st

            return (datumSplBaseCat, datumSplTopCat, profileType);
        }

        private Wall CreateNewWall(Wall oldWall, bool baseBool, double dbData)
        {
            // Getting wall data from old wall for use in new wall
            string locationLine = "Core Face: Interior";

            Parameter oldLocationLineParameter = oldWall.get_Parameter(BuiltInParameter.WALL_KEY_REF_PARAM);
            oldLocationLineParameter.Set(locationLine);

            Parameter baseOffset = oldWall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            Parameter topOffset = oldWall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET);
            Parameter isTopAttached = oldWall.get_Parameter(BuiltInParameter.WALL_TOP_IS_ATTACHED);

            ElementId oldWallBaseConstraint = oldWall.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT).AsElementId();
            ElementId oldWallTopConstraint = oldWall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsElementId();

            double unconnectedHeight = oldWall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM).AsDouble();

            Curve wallLoc = ((LocationCurve)oldWall.Location).Curve;

            // Create new wall
            Wall newWall = Wall.Create(_doc, wallLoc, oldWall.LevelId, false);

            // Get and set new parameters
            Parameter locationLineParameter = newWall.get_Parameter(BuiltInParameter.WALL_KEY_REF_PARAM);
            locationLineParameter.Set(locationLine);

            Parameter wallBaseConstraint = newWall.get_Parameter(BuiltInParameter.WALL_BASE_CONSTRAINT);
            wallBaseConstraint.Set(oldWallBaseConstraint);

            Parameter wallTopConstraint = newWall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE);
            wallTopConstraint.Set(oldWallTopConstraint);

            // Setting the offsets based on the db inputs
            Parameter topOffParam = newWall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET);
            Parameter baseOffParam = newWall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET);
            Parameter topAttachParam = newWall.get_Parameter(BuiltInParameter.WALL_TOP_IS_ATTACHED);

            if (baseBool)
            {
                // TODO: fix
                // if (dbData == "LevelTop")
                {
                    baseOffset.Set(unconnectedHeight - 0.5);
                    topOffParam.Set(-0.5);
                }
                // TODO: fix
                //else
                {
                    baseOffset.Set(unconnectedHeight - dbData);
                    topOffParam.Set(dbData);
                }
            }
            else
            {
                // TODO: fix
                // if (dbData == "LevelTop")
                {
                    topOffset.Set(-0.5);
                    baseOffParam.Set(unconnectedHeight - 0.5);
                }
                // TODO: fix
                // else
                {
                    topOffset.Set(dbData);
                    baseOffParam.Set(unconnectedHeight - dbData);
                }

                topAttachParam.Set(isTopAttached.AsValueString());
                isTopAttached.Set("No");
            }

            return newWall;
        }

        private void SetWallDatumData(string datumSplBaseCat, string datumSplTopCat, Element profileType)
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
                // TODO: fix

                //                if (wall.LevelId.Name.Contains("FIRST"))
                //                {
                //                    levelOneWalls.Add(wall);
                //                }
                //                else if (wall.LevelId.Name.Contains("SECOND"))
                //                {
                //                    levelTwoWalls.Add(wall);
                //                }
                //                else if (wall.LevelId.Name.Contains("THIRD"))
                //                {
                //                    levelThreeWalls.Add(wall);
                //                }
            }

            // Determine which level is the top most level
            List<Wall> topLevelWalls = levelThreeWalls.Count == 0 ? levelTwoWalls : levelThreeWalls;

            // Creating and setting the wall data
            if (datumSplBaseCat != null)
            {
                foreach (Wall wall in levelOneWalls)
                {
                    Wall newBaseWall = CreateNewWall(wall, true, InchesToFeet(int.Parse(datumSplBaseCat)));
                }
            }

            if (datumSplTopCat != null)
            {
                foreach (Wall wall in topLevelWalls)
                {
                    Wall newTopWall = CreateNewWall(wall, false, InchesToFeet(int.Parse(datumSplTopCat)));
                }
            }

            // ADD - Addition of profile into CreateNewWall function
            // ADD - Function to remove and set back if new facade style doesn't call for a base wall
        }

        private void GenerateFacade()
        {
            WallData wallData = GetWallTypes();
            SetExtWallTypes(wallData);

            ColumnData columnData = GetColumnData(wallData);
            SetColumnData(columnData);

            WindowData windowData = GetWindowTypes();
            SetWindowData(windowData);

            string datumSplBaseCat;
            string datumSplTopCat;
            Element profileType;

            (datumSplBaseCat, datumSplTopCat, profileType) = GetWallDatumData();
            SetWallDatumData(datumSplBaseCat, datumSplTopCat, profileType);
        }


    }
}
