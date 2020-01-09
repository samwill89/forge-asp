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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using DesignAutomationFramework;
using Newtonsoft.Json;


namespace CountIt
{
	[Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
	[Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
	public class CountIt : IExternalDBApplication
	{
		public ExternalDBApplicationResult OnStartup(ControlledApplication app)
		{
			DesignAutomationBridge.DesignAutomationReadyEvent += HandleDesignAutomationReadyEvent;
			return ExternalDBApplicationResult.Succeeded;
		}

		public ExternalDBApplicationResult OnShutdown(ControlledApplication app)
		{
			return ExternalDBApplicationResult.Succeeded;
		}

		public void HandleDesignAutomationReadyEvent(object sender, DesignAutomationReadyEventArgs e)
		{
			e.Succeeded = CountElementsInModel(e.DesignAutomationData.RevitApp, e.DesignAutomationData.FilePath, e.DesignAutomationData.RevitDoc);
		}

		internal static List<Document> GetHostAndLinkDocuments(Document revitDoc)
		{
			List<Document> docList = new List<Document>();
			docList.Add(revitDoc);

			// Find RevitLinkInstance documents
			FilteredElementCollector elemCollector = new FilteredElementCollector(revitDoc);
			elemCollector.OfClass(typeof(RevitLinkInstance));
			foreach (Element curElem in elemCollector)
			{
				RevitLinkInstance revitLinkInstance = curElem as RevitLinkInstance;
				if (null == revitLinkInstance)
					continue;

				Document curDoc = revitLinkInstance.GetLinkDocument();
				if (null == curDoc) // Link is unloaded.
					continue;

				// When one linked document has more than one RevitLinkInstance in the
				// host document, then 'docList' will contain the linked document multiple times.

				docList.Add(curDoc);
			}

			return docList;
		}

		/// <summary>
		/// Count the element in each file
		/// </summary>
		/// <param name="revitDoc"></param>
		/// <param name="countItParams"></param>
		/// <param name="results"></param>
		internal static void CountElements(Document revitDoc, CountItParams countItParams)
		{

			//logic goes in here!

			FilteredElementCollector elemCollector = new FilteredElementCollector(revitDoc);
			elemCollector.OfClass(typeof(Wall));
			List<Wall> wallElements = new List<Wall>();
			List<RoofBase> roofElements = new List<RoofBase>();
			ICollection<ElementId> userSelection = elemCollector.ToElementIds();
			Element elmnt;
			foreach (var i in userSelection)
			{

				elmnt = revitDoc.GetElement(i);
				if (elmnt.Category.Name == "Walls")
				{
					wallElements.Add(elmnt as Wall);
				}
				else if (elmnt.Category.Name == "Roofs")
				{
					roofElements.Add(elmnt as RoofBase);
				}
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

			//TaskDialog.Show("End", "job done");
			//return Result.Succeeded;
		}


		public static double segmentdistance(double lsX, double lsY, double leX, double leY)
		{
			return Math.Sqrt(Math.Pow(leX - lsX, 2) + Math.Pow(leY - lsY, 2));
		}
		public static double inft(double inches)
		{
			return inches / 12;
		}
		public static double GetBasisAngle(double x1, double y1, double x2, double y2)
		{
			return 0;
		}
		public static double GetAngle(float x1, float y1, float x2, float y2)
		{
			double a;
			double b;
			double angle;
			int quad;
			if (Math.Round(x1) == Math.Round(x2))
			{
				b = 1;
			}
			else
			{
				if (x1 < x2)
				{
					b = x2 - x1;
				}
				else
				{
					b = x1 - x2;
				}
			}
			if (Math.Round(y1) == Math.Round(y2))
			{
				a = 1;
			}
			else
			{
				if (y1 < y2)
				{
					a = y2 - y1;
				}
				else
				{
					a = y1 - y2;
				}
			}
			//retrieve quadrants

			if (x1 <= x2 & y1 <= y2)
			{
				quad = 1;
			}
			else if (x1 <= x2 & y1 >= y2)
			{
				quad = 4;
			}
			else if (x1 >= x2 & y1 <= y2)
			{
				quad = 2;
			}
			else if (x1 >= x2 & y1 >= y2)
			{
				quad = 3;
			}
			else
			{
				//!! there has to be else statement or quad won't be assigned!!
				quad = 5;
			}
			//set angle	
			if (quad == 1)
			{
				angle = RadianToDegree(Math.Atan(a / b)) - 180;
			}
			else if (quad == 2)
			{
				angle = (RadianToDegree(Math.Atan(a / b)));
			}
			else if (quad == 3)
			{
				angle = (RadianToDegree(Math.Atan(a / b)));
			}
			else
			{
				angle = 180 - (RadianToDegree(Math.Atan(a / b)));
			}
			return (angle);
		}
		public static double RadianToDegree(double angle)
		{
			return angle * (180.0 / Math.PI);
		}
		public static double DegreeToRadian(double angle)
		{
			return Math.PI * angle / 180.0;
		}
		public static List<double> fromStart(double x1, double y1, double x2, double y2, double angle, double distance)
		{
			double sxDist = distance * Math.Cos(DegreeToRadian(angle));
			double syDist = distance * Math.Sin(DegreeToRadian(angle));
			int quad;
			double newsX, newsY;
			//retrieve quadrants
			if (x1 <= x2 & y1 <= y2)
			{
				quad = 1;
			}
			else if (x1 <= x2 & y1 >= y2)
			{
				quad = 4;
			}
			else if (x1 >= x2 & y1 <= y2)
			{
				quad = 2;
			}
			else if (x1 >= x2 & y1 >= y2)
			{
				quad = 3;
			}
			else
			{
				quad = 4;
			}
			//set angle	
			if (quad == 1)
			{
				newsX = x1 + sxDist;
				newsY = y1 + syDist;
			}
			else if (quad == 2)
			{
				newsX = x1 - sxDist;
				newsY = y1 + syDist;
			}
			else if (quad == 3)
			{
				newsX = x1 - sxDist;
				newsY = y1 - syDist;
			}
			else
			{
				newsX = x1 + sxDist;
				newsY = y1 - syDist;
			}
			List<double> retval = new List<double>() { newsX, newsY };
			return retval;
		}
		public static double GetAngleFrom3Pts(double x1, double y1, double x2, double y2, double x3, double y3)
		{
			double ang = RadianToDegree(Math.Atan2(y3 - y2, x3 - x2) - Math.Atan2(y1 - y2, x1 - x2));
			if (ang < 0)
			{
				ang = ang + 360;
			}
			return ang;
		}


		/// <summary>
		/// count the elements depends on the input parameter in params.json
		/// </summary>
		/// <param name="rvtApp"></param>
		/// <param name="inputModelPath"></param>
		/// <param name="doc"></param>
		/// <returns></returns>
		public static bool CountElementsInModel(Application rvtApp, string inputModelPath, Document doc)
		{
			if (rvtApp == null)
				return false;

			if (!File.Exists(inputModelPath))
				return false;

			if (doc == null)
				return false;

			// For CountIt workItem: If RvtParameters is null, count all types
			CountItParams countItParams = CountItParams.Parse("params.json");
			//CountItResults results = new CountItResults();

			List<Document> allDocs = GetHostAndLinkDocuments(doc);
			foreach (Document curDoc in allDocs)
			{
				CountElements(curDoc, countItParams);
			}

			//using (StreamWriter sw = File.CreateText("result.txt"))
			//{
			//	sw.WriteLine(JsonConvert.SerializeObject(results));
			//	sw.Close();
			//}

			return true;
		}

	}
}

		//results.abdo = 50;
		/*if (countItParams.walls)
		{
			FilteredElementCollector elemCollector = new FilteredElementCollector(revitDoc);
			elemCollector.OfClass(typeof(Wall));
			int count = elemCollector.ToElementIds().Count;
			results.walls += count;
			results.total += count;
		}

		if (countItParams.floors)
		{
			FilteredElementCollector elemCollector = new FilteredElementCollector(revitDoc);
			elemCollector.OfClass(typeof(Floor));
			int count = elemCollector.ToElementIds().Count;
			results.floors += count;
			results.total += count;
		}

		if (countItParams.doors)
		{
			FilteredElementCollector collector = new FilteredElementCollector(revitDoc);
			ICollection<Element> collection = collector.OfClass(typeof(FamilyInstance))
												.OfCategory(BuiltInCategory.OST_Doors)
												.ToElements();

			int count = collection.Count;
			results.doors += count;
			results.total += count;
		}

		if (countItParams.windows)
		{
			FilteredElementCollector collector = new FilteredElementCollector(revitDoc);
			ICollection<Element> collection = collector.OfClass(typeof(FamilyInstance))
												.OfCategory(BuiltInCategory.OST_Windows)
												.ToElements();

			int count = collection.Count;
			results.windows += count;
			results.total += count;
		}*/
	//}