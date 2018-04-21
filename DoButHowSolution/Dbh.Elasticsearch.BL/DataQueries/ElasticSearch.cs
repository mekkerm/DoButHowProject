using Dbh.BusinessLayer.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Dbh.Elasticsearch.BL.DataQueries
{
    public class ElasticSearch : IElasticSearch
    {
        private readonly string INDEX_NAME = "questions";
        private Utils _utils;
        private Utils Utils {
            get {
                if (_utils == null)
                {
                    _utils = new Utils();
                }
                return _utils;
            }
        }
        public List<QuestionHeaderDTO> SearchForQuestion(string text)
        {
            var results = new List<QuestionHeaderDTO>();
            var requestUrl = Utils.GetElasticServerUrl() + INDEX_NAME + "/_doc/_search";
            using (var client = new HttpClient())
            {
                var body = new StringBuilder();

                string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var rowTemplate = System.IO.File.ReadAllText(directoryName + "/JSONS/question_search.json");
                var row = rowTemplate.Replace("{0}", text);
                body.AppendLine(row);

                HttpContent content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

                var res = client.PostAsync(requestUrl, content);

                var returned = res.Result.Content.ReadAsStringAsync().Result;
                dynamic stuff = JsonConvert.DeserializeObject(returned);
                foreach (var item in stuff.hits.hits)
                {
                    var qid = Int32.Parse(item._source.question_id.Value.ToString());
                    var qTitle = item._source.title.Value;
                    var qDescription = item._source.description.Value;
                    results.Add(new QuestionHeaderDTO
                    {
                        Id = qid,
                        Description = qDescription,
                        Title = qTitle
                    });
                }
            }

            return results;
        }
    }
}
