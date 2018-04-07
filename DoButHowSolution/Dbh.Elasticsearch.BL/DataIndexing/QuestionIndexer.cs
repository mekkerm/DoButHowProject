using Dbh.BusinessLayer.Contracts;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Dbh.Elasticsearch.BL.DataIndexing
{
    public class QuestionIndexer : IIndexer<Question>
    {
        private readonly string INDEX_NAME = "questions";
        private readonly string INSERT_ROW = "{ \"index\":{} }";
        private readonly string BULK_ERRORS = "\"errors\":false,";
        private readonly string INDEX_SUCCESS = "\"result\":\"created\"";
        private Utils _utils;
        private Utils Utils {
            get {
                if(_utils == null)
                {
                    _utils = new Utils();
                }
                return _utils;
            }
        }
        
        public QuestionIndexer()
        {

        }
        public bool IndexData(IEnumerable<Question> questions)
        {
            var requestUrl = Utils.GetElasticServerUrl() + INDEX_NAME + "/_doc/_bulk";

            using (var client = new HttpClient())
            {
                var body = new StringBuilder();

                string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var rowTemplate = System.IO.File.ReadAllText(directoryName + "/JSONS/question_insert.json");
                foreach (var item in questions)
                {
                    var row = rowTemplate.Replace("{0}", item.Id.ToString()).Replace("{1}", item.Title).Replace("{2}", item.Description).Replace("{3}", item.CategoryDescription).Replace("{4}", Utils.tokenize(item)).Replace("\n", "");
                    body.AppendLine(INSERT_ROW).AppendLine(row);
                }


                HttpContent content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

                var res = client.PostAsync(requestUrl, content);

                var returned = res.Result.Content.ReadAsStringAsync().Result;
                var error = returned.IndexOf(BULK_ERRORS) == -1;
                if (error)
                {
                    throw new ElasticsearchException(returned);
                }
                return true;
            }
        }

        public bool IndexData(Question question)
        {
            var requestUrl = Utils.GetElasticServerUrl() + INDEX_NAME + "/_doc";

            using (var client = new HttpClient())
            {
                var body = new StringBuilder();
                string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                var rowTemplate = System.IO.File.ReadAllText(directoryName+"/JSONS/question_insert.json");

                var row = rowTemplate.Replace("{0}", question.Id.ToString()).Replace("{1}", question.Title).Replace("{2}", question.Description).Replace("{3}", question.CategoryDescription).Replace("{4}", Utils.tokenize(question)).Replace("\n", "");
                body.AppendLine(row);


                HttpContent content = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

                var res = client.PostAsync(requestUrl, content);

                var returned = res.Result.Content.ReadAsStringAsync().Result;
                var error = returned.IndexOf(INDEX_SUCCESS) == -1;
                if (error)
                {
                    throw new ElasticsearchException(returned);
                }
                return true;
            }
        }

        public bool CreateIndex()
        {
            var cr = new QuestionIndexCreator(Utils.GetElasticServerUrl());
            return cr.CreateIndex();

            return true;
        }

        public void bela()
        {

            var rowTemplate = System.IO.File.ReadAllText("appsettings.json");
            var parsed = JObject.Parse(rowTemplate);
            var url = parsed["elasticPath"];
        }
    }
}
