
using Bookstore.Infrastructure.Entities;
using Bookstore.Infrastructure.Exceptions;
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

        public virtual List<T> get() 
        {
            try
            {
                return _data.Find(m => true).ToList();
            }
            catch(Exception e)
            {
                throw new CustomException(e.Message);
            }        
        } 
        public virtual T Get(string id)
        {
            try
            {
                return _data.Find<T>(m => m.Id == id).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new CustomException(e.Message);
            }                
        }

        public virtual T Create(T model)
        {
            try
            {
                _data.InsertOne(model);
                return model;
            }
            catch (Exception e)
            {
                throw new CustomException(e.Message);
            }          
        }

        public virtual bool Update(string id, T model)
        {
            try
            {
                var result = _data.ReplaceOne(m => m.Id == id, model);
                return result.ModifiedCount > 0;
            }
            catch (Exception e)
            {
                throw new CustomException(e.Message);
            }      
        }

        public virtual void Remove(T model) {
            try
            {
                _data.DeleteOne(m => m.Id == model.Id);
            }
            catch (Exception e)
            {
                throw new CustomException(e.Message);
            }
        }
       

        public virtual bool Remove(string id)
        {
            try
            {
                var result = _data.DeleteOne(m => m.Id == id);
                return result.DeletedCount > 0;
            }
            catch (Exception e)
            {
                throw new CustomException(e.Message);
            }
            
        }
            
    }
}
