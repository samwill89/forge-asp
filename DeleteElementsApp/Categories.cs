using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteElements
{
    class Categories
    {
        public IList<string> Materials { get; set; }

        public IList<string> Columns { get; set; }

        public IList<string> Porch { get; set; }

        public IList<string> Windows { get; set; }

        public IList<string> Doors { get; set; }

        public IList<string> Roofs { get; set; }

        public IList<string> Misc { get; set; }

        public Categories()
        {
            Materials = new List<string>();
            Columns = new List<string>();
            Porch = new List<string>();
            Windows = new List<string>();
            Doors = new List<string>();
            Roofs = new List<string>();
            Misc = new List<string>();
        }
    }
}
