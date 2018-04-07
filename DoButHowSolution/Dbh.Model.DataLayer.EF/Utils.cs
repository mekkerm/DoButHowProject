using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dbh.Model.DataLayer.EF
{
    public class Utils
    {
        private static Utils _utils;
        private string defaultConnection;

        private Utils()
        {
        }

        public string GetDefaultConnection()
        {
            if (this.defaultConnection == null)
            {
                var rowTemplate = System.IO.File.ReadAllText("appsettings.json");
                var parsed = JObject.Parse(rowTemplate);
                var connStrings = parsed["ConnectionStrings"];
                var connString = connStrings["DefaultConnection"];
                this.defaultConnection = connString.ToString();
            }
            return this.defaultConnection;
        }

        public static Utils GetInstance()
        {
            if(_utils == null)
            {
                _utils = new Utils();
            }
            return _utils;
        }
    }
}
