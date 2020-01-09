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

        /// <summary>
        /// delete elements depends on the input params.json file
        /// </summary>
        /// <param name="data"></param>
        public static void DeleteAllElements(DesignAutomationData data)
      {





			//logic goes in here!

			if (data == null) throw new ArgumentNullException(nameof(data));

			Application rvtApp = data.RevitApp;
			if (rvtApp == null) throw new InvalidDataException(nameof(rvtApp));

			string modelPath = data.FilePath;
			if (String.IsNullOrWhiteSpace(modelPath)) throw new InvalidDataException(nameof(modelPath));

			Document revitDoc = data.RevitDoc;
			if (revitDoc == null) throw new InvalidOperationException("Could not open document.");


			FilteredElementCollector wallCollector = new FilteredElementCollector(revitDoc).OfClass(typeof(Wall));
			FilteredElementCollector roofCollector = new FilteredElementCollector(revitDoc).OfClass(typeof(RoofBase));
			//elemCollector.OfClass(typeof(Wall));
			List<Wall> wallElements = new List<Wall>();
			List<RoofBase> roofElements = new List<RoofBase>();
			ICollection<ElementId> walls = wallCollector.ToElementIds();
			ICollection<ElementId> roofs = roofCollector.ToElementIds();
			Element elmnt;
			foreach (var i in walls)
			{
				elmnt = revitDoc.GetElement(i);
				wallElements.Add(elmnt as Wall);
			}

			foreach (var i in roofs)
			{
				elmnt = revitDoc.GetElement(i);
				roofElements.Add(elmnt as RoofBase);
			}


			//Collecting all types to be used in model
			string wcName;
			FilteredElementCollector wallTypeColl = new FilteredElementCollector(revitDoc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsElementType();
			List<string> wallTypeNames = new List<string>();
			foreach (var wc in wallTypeColl)
			{
				wcName = wc.LookupParameter("Type Name").AsString();
				//TaskDialog.Show("WCNAME WALLTYPE", wcName);
				wallTypeNames.Add(wcName);
			}
			string wtName;
			FilteredElementCollector windowTypeColl = new FilteredElementCollector(revitDoc).OfCategory(BuiltInCategory.OST_Windows).WhereElementIsElementType();
			List<string> windowTypeNames = new List<string>();
			foreach (var wt in windowTypeColl)
			{
				wtName = wt.LookupParameter("Type Name").AsString();
				//TaskDialog.Show("WTNAME WALLTYPE", wtName);
				windowTypeNames.Add(wtName);
			}
			string rcName;
			FilteredElementCollector roofTypeColl = new FilteredElementCollector(revitDoc).OfCategory(BuiltInCategory.OST_Roofs).WhereElementIsElementType();
			List<string> roofTypeName = new List<string>();
			foreach (var rc in roofTypeColl)
			{
				rcName = rc.LookupParameter("Type Name").AsString();
				//TaskDialog.Show("RTYPE ROOF TYPE", rcName);
				roofTypeName.Add(rcName);
			}
			//Form1 new_form = new Form1();
			//new_form.ShowDialog();

			DeleteElementsParams countItParams = DeleteElementsParams.Parse("params.json");

			string extMaterials = countItParams.extMaterials;
			string materialColor = countItParams.materialColor;
			string roofMaterial = countItParams.roofMaterial;
			string fenestrationTrim = countItParams.fenestrationTrim;
			string columnStyle = countItParams.columnStyle;
			//TaskDialog.Show("ext material", extMaterials);
			//List<string> wallTypeColl_list = wallTypeColl.ToList();
			var wtlist = wallTypeColl.ToList();
			//TaskDialog.Show("walltypenamecount", wallTypeNames.Count.ToString());
			Element insertElement;
			List<Element> ws = new List<Element>();
			List<Element> ds = new List<Element>();
			Transaction t = new Transaction(revitDoc, "Edit walls");
			t.Start();
			foreach (var wall in wallElements)
			{
				ws.Clear();
				ds.Clear();
				for (int i = 0; i < wallTypeNames.Count; i++)
				{
					if (wallTypeNames[i].Contains(extMaterials) & wallTypeNames[i].Contains(materialColor))
					{
						wall.WallType = wallTypeColl.First<Element>(x => x.Name.Equals(wallTypeNames[i].ToString())) as WallType;
					}
				}
				IList<ElementId> allInserts = wall.FindInserts(true, false, false, false);
				foreach (var ai in allInserts)
				{
					insertElement = revitDoc.GetElement(ai);
					if (insertElement.Category != null)
					{
						if (insertElement.Category.Name == "Windows")
						{
							ws.Add(insertElement);
						}
						if (insertElement.Category.Name == "Doors")
						{
							ds.Add(insertElement);
						}
					}
				}
				foreach (Element window in ws)
				{
					for (int i = 0; i < windowTypeNames.Count; i++)
					{
						if (windowTypeNames[i].Contains(fenestrationTrim))
						{
							//    window. = windowTypeColl.First<Element>(x => x.Name.Equals(windowTypeNames[i].ToString())) as FamilySymbol;
						}
					}
				}
			}
			t.Commit();

			t.Start();

			foreach (var roof in roofElements)
			{
				for (int i = 0; i < roofTypeName.Count; i++)
				{
					if (roofTypeName[i].Contains(roofMaterial))
					{
						roof.RoofType = roofTypeColl.First<Element>(x => x.Name.Equals(roofTypeName[i].ToString())) as RoofType;
					}
				}

			}

			t.Commit();

			ModelPath path = ModelPathUtils.ConvertUserVisiblePathToModelPath("result.rvt");
			revitDoc.SaveAs(path, new SaveAsOptions());

















			//=====================================================================
















			/*



		if (data == null) throw new ArgumentNullException(nameof(data));

         Application rvtApp = data.RevitApp;
         if (rvtApp == null) throw new InvalidDataException(nameof(rvtApp));

         string modelPath = data.FilePath;
         if (String.IsNullOrWhiteSpace(modelPath)) throw new InvalidDataException(nameof(modelPath));

         Document doc = data.RevitDoc;
         if (doc == null) throw new InvalidOperationException("Could not open document.");


        // For CountIt workItem: If RvtParameters is null, count all types
        DeleteElementsParams deleteElementsParams = DeleteElementsParams.Parse("params.json");

        using (Transaction transaction = new Transaction(doc))
         {
                transaction.Start("Delete Elements");
                if (deleteElementsParams.walls)
                {
                    FilteredElementCollector col = new FilteredElementCollector(doc).OfClass(typeof(Wall));
                    doc.Delete(col.ToElementIds());

                }
                if (deleteElementsParams.floors)
                {
                    FilteredElementCollector col = new FilteredElementCollector(doc).OfClass(typeof(Floor));
                    doc.Delete(col.ToElementIds());
                }
                if (deleteElementsParams.doors)
                {
                    FilteredElementCollector collector = new FilteredElementCollector(doc);
                    ICollection<ElementId> collection = collector.OfClass(typeof(FamilyInstance))
                                                       .OfCategory(BuiltInCategory.OST_Doors)
                                                       .ToElementIds();
                    doc.Delete(collection);
                }
                if (deleteElementsParams.windows)
                {
                    FilteredElementCollector collector = new FilteredElementCollector(doc);
                    ICollection<ElementId> collection = collector.OfClass(typeof(FamilyInstance))
                                                       .OfCategory(BuiltInCategory.OST_Windows)
                                                       .ToElementIds();
                    doc.Delete(collection);
                }
                transaction.Commit();
         }

         ModelPath path = ModelPathUtils.ConvertUserVisiblePathToModelPath("result.rvt");
         doc.SaveAs(path, new SaveAsOptions());

	*/
      }
   }
}
