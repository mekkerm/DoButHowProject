using Dbh.Elasticsearch.BL.DataIndexing;
using Dbh.Elasticsearch.BL.DataQueries;
using Dbh.Model.EF.Context;
using Dbh.Model.EF.Entities;
using Dbh.Model.EF.Repositories;
using Dbh.Model.EF.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ElasticsearchTests
{
    public class ElasticTests
    {
        [Fact]
        public void IndexBulkDataTest()
        {
            var indexer = new QuestionIndexer();
            var q1 = new Question();
            var q2 = new Question();

            q1.Id = 3;
            q2.Id = 4;
            q1.Title = "how to register to vote";
            q2.Title = "how to lose weight";
            q1.Description = "I realy would like to go to vote this summer, can someone explain to me how/where to register, I have no idea.";
            q2.Description = "I'm 150 kg, i would like to get into shape as fast as possible, what is the best way?";
            var list = new List<Question>();
            list.Add(q1);
            list.Add(q2);
            var res = indexer.IndexData(list);
            Assert.True(res);
        }

        [Fact]
        void IndexAllQuestions()
        {

            var ctx = new ApplicationDbContext();
            var repo = new QuestionRepository(ctx);
            var catRepo = new QuestionCategoryRepository(ctx);
            var all = repo.FindAllOrDefault(x => x.IsApproved == true).ToList();
            var catDesc = catRepo.GetAll().ToDictionary(x => x.Id, x => x.Name);
            
            all.ForEach(x => x.CategoryDescription = catDesc.GetValueOrDefault(x.CategoryId, "General"));

            var indexer = new QuestionIndexer();
            indexer.IndexData(all);
        }

        [Fact]
        public void IndexDataTest()
        {
            var indexer = new QuestionIndexer();
            var q1 = new Question();
            
            q1.Id = 23;
            q1.CategoryDescription = "General";
            q1.Title = "how to register to vote";
            q1.Description = "I really would like to go to vote this summer, can someone explain to me how/where to register, I have no idea.";
            
            var res = indexer.IndexData(q1);
            Assert.True(res);
        }

        [Fact]
        public void CreateIndexTest()
        {
            var indexer = new QuestionIndexer();
            var res = indexer.CreateIndex();
            Assert.True(res);
        }

        [Fact]
        public void Bela()
        {
            var indexer = new QuestionIndexer();
            indexer.bela();
        }

        [Fact]
        public void SearchTest()
        {
            var finder = new ElasticSearch();
            var results = finder.SearchForQuestion("I would like");
            Assert.True(results.Any());
            
        }
    }
}
