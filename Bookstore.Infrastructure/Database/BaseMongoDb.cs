using Bookstore.Application.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Database
{
    public class BaseMongoDb<Tn> where Tn : Base
    {
        public IMongoCollection<Tn> _data;
        private readonly IMongoDatabase _datebase;
        public BaseMongoDb(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            _datebase = client.GetDatabase(dbName);
            _data = _datebase.GetCollection<Tn>(collectionName);
        }

        public virtual List<Tn> get() => _data.Find(m => true).ToList();
        public virtual Tn Get(string id) =>
            _data.Find<Tn>(m => m.Id == id).FirstOrDefault();

        public virtual Tn Create(Tn model)
        {
            _data.InsertOne(model);
            return model;
        }

        public virtual bool Update(string id, Tn model)
        {
            var result = _data.ReplaceOne(m => m.Id == id, model);
            return result.ModifiedCount > 0;
        }
            
        public virtual void Remove(Tn model) =>
            _data.DeleteOne(m => m.Id == model.Id);

        public virtual bool Remove(string id)
        {
            var result = _data.DeleteOne(m => m.Id == id);
            return result.DeletedCount > 0;
        }
            
    }
}
