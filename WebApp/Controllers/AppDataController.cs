using Autodesk.Forge;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace forgesample.Controllers
{
    [ApiController]
    public class AppDataController : ControllerBase
    {
        private List<IDictionary<string, object>> _facadesDb;
        private List<string> _keys;

        public Dictionary<string, string> Values { get; private set; }


        [HttpGet]
        [Route("api/forge/appdata/all")]
        public List<IDictionary<string, object>> GetAllData()
        {
            using (var reader = new StreamReader("./CSVData/BIM Quote Facades.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                _facadesDb = csv.GetRecords<dynamic>().Select(row => (IDictionary<string, object>)row).ToList();
                //_keys = _facadesDb[0].Keys.ToList();

                //int styleIndex = _keys.IndexOf(style);
                //if (styleIndex == -1)
                //{
                //    throw new ArgumentException($"Style '{style}' cannot be find.");
                //}
                //IEnumerable<List<string>> table = _facadesDb.Select(row => row.Values.Select(value => value.ToString()).ToList());

                //Values = table.ToDictionary(row => row[0], row => row[styleIndex]);


                return _facadesDb;
                
            }
        }

    }
}



