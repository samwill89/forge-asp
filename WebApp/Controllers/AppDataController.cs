using Autodesk.Forge;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Hosting;

namespace forgesample.Controllers
{
    [ApiController]
    public class AppDataController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private List<IDictionary<string, object>> _facadesDb;
        private List<string> _keys;

        public Dictionary<string, string> Values { get; private set; }

        public AppDataController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("api/forge/appdata/all")]
        public List<IDictionary<string, object>> GetAllData()
        {
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "CSVData/BIM Quote Facades.csv");
            using (var reader = new StreamReader(filePath))
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



