using Dbh.BusinessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace Dbh.Elasticsearch.BL.DataIndexing
{
    internal class QuestionIndexCreator
    {
        private readonly string INDEX_HEADER = "index";
        private readonly string SEPARATOR = " ";
        private readonly string INDEX_NAME = "questions";

        private Uri url;

        public QuestionIndexCreator(Uri url)
        {
            this.url = url;
        }
        public bool CreateIndex()
        {
            if (IndexExists())
            {
                return true;//TODO maybe force recreation?
            }
            else
            {
                return Create();
            }
        }

        private bool Create()
        {
            var requestUrl = url + "/" + INDEX_NAME;

            using (var client = new HttpClient())
            {
                var body = System.IO.File.ReadAllText("JSONS/question_mapping.json");
                HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");
                var res = client.PutAsync(requestUrl, content);
                var returned = res.Result.Content.ReadAsStringAsync().Result;
                var error =  returned.IndexOf("\"acknowledged\":true") != -1;
                if (error)
                {
                    throw new ElasticsearchException(returned);
                }
                return true;
            }
            
        }

        public bool IndexExists()
        {
            using (var client = new HttpClient())
            {
                var GETResult = client.GetAsync(url + "/_cat/indices?v");
                var GETResultString = GETResult.Result.Content.ReadAsStringAsync().Result;

                Regex trimmer = new Regex(@"\s\s+");

                var formattedResultString = trimmer.Replace(GETResultString, " ");

                var lines = formattedResultString.Split("\n");

                if (lines.Length > 0)
                {
                    var headers = lines[0].Split(SEPARATOR);
                    var indexAt = indexOf(headers, INDEX_HEADER);

                    for (int i = 1; i < lines.Length; i++)
                    {
                        var parts = lines[i].Split(SEPARATOR);
                        if (parts.Length > indexAt)
                        {
                            if (parts[indexAt] == "questions")
                            {
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
        }

        private int indexOf(string[] collection, string term)
        {
            for(int i = 0; i < collection.Length; i++)
            {
                if(collection[i] == term)
                {
                    return i;
                }
            }
            return -1;
        }

        
    }
}
