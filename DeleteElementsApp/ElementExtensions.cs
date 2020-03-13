using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace DeleteElements
{
    public static class ElementExtensions
    {
        public static void SetParameter(this Element element, string name, ElementId value)
        {
            IList<Parameter> parameters = element.GetParameters(name);
            foreach (Parameter parameter in parameters)
            {
                parameter.Set(value);
            }
        }

        public static void SetParameter(this Element element, string name, double value)
        {
            IList<Parameter> parameters = element.GetParameters(name);
            foreach (Parameter parameter in parameters)
            {
                parameter.Set(value);
            }
        }

        public static void SetParameter(this Element element, string name, int value)
        {
            IList<Parameter> parameters = element.GetParameters(name);
            foreach (Parameter parameter in parameters)
            {
                parameter.Set(value);
            }
        }

        public static void SetParameter(this Element element, string name, string value)
        {
            IList<Parameter> parameters = element.GetParameters(name);
            foreach (Parameter parameter in parameters)
            {
                parameter.Set(value);
            }
        }

        public static void SetParameterAsString(this Element element, string name, string value)
        {
            IList<Parameter> parameters = element.GetParameters(name);
            foreach (Parameter parameter in parameters)
            {
                parameter.SetValueString(value);
            }
        }
    }
}
