using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace DeleteElements
{
    public static class DocumentExtensions
    {
        public static List<T> CollectTypes<T>(this Document document, BuiltInCategory category)
            where T : Element
        {
            FilteredElementCollector typeCollector = new FilteredElementCollector(document).OfCategory(category).WhereElementIsElementType();

            return typeCollector.Select(type => (T)type).ToList();
        }

        public static List<T> CollectElements<T>(this Document document, BuiltInCategory category)
            where T : Element
        {
            FilteredElementCollector elementCollector = new FilteredElementCollector(document).OfCategory(category).WhereElementIsNotElementType();

            return elementCollector.Select(type => (T)type).ToList();
        }
    }
}
