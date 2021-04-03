
using Bookstore.Infrastructure.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Database
{
    public class BaseMongoDb<T> where T : Base
    {
        public IMongoCollection<T> _data;
        private readonly IMongoDatabase _datebase;
        public BaseMongoDb(string mongoDBConnectionString, string dbName, string collectionName)
        {
            var client = new MongoClient(mongoDBConnectionString);
            _datebase = client.GetDatabase(dbName);
            _data = _datebase.GetCollection<T>(collectionName);

        }

        public virtual List<T> get() => _data.Find(m => true).ToList();
        public virtual T Get(string id)
        {
            try
            {
                return _data.Find<T>(m => m.Id == id).FirstOrDefault();
            }
            catch(Exception e)
            {
                throw;
            }
                
        }

        public virtual T Create(T model)
        {
            _data.InsertOne(model);
            return model;
        }

        public virtual bool Update(string id, T model)
        {
            var result = _data.ReplaceOne(m => m.Id == id, model);
            return result.ModifiedCount > 0;
        }
            
        public virtual void Remove(T model) =>
            _data.DeleteOne(m => m.Id == model.Id);

        public virtual bool Remove(string id)
        {
            var result = _data.DeleteOne(m => m.Id == id);
            return result.DeletedCount > 0;
        }
            
    }
}
