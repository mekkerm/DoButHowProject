using Dbh.Model.EF.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Dbh.Elasticsearch.BL
{
    public class Utils
    {
        private Uri elasticUri;
        public Uri GetElasticServerUrl()
        {
            if(elasticUri == null)
            {
                var rowTemplate = System.IO.File.ReadAllText("appsettings.json");
                var parsed = JObject.Parse(rowTemplate);
                var url = parsed["elasticPath"];
                elasticUri = new Uri(url.ToString());
            }
            return elasticUri;
        }

        public string tokenize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            var cleanText = stripHTML(input).Replace(".", "").Replace(",", "").Replace(":", "").Replace(";", "");
            var cleanerText = Regex.Replace(cleanText, @"\s+", " ");
            var spl = cleanerText.Split(" ");
            var splitted = new List<string>();

            foreach (var item in spl)
            {
                var processed = Regex.Replace(item, @"([^\w]|_)", "");
                if (processed != "")
                {
                    splitted.Add(processed);
                }
            }
            
            return "\"" + string.Join("\", \"", splitted) + "\"";
        }
        public string stripHTML(string input)
        {
            return Regex.Replace(String.Copy(input), "<.*?>", String.Empty);
        }

        public string tokenize(Question question)
        {
            var title = tokenize(question.Title);
            var descr = tokenize(question.Description);
            var category = tokenize(question.CategoryDescription);
            var full = String.Join(", ",title, descr, category);
            return full;
        }
    }
}
