using Bookstore.Application.Entities;
using System;

namespace Bookstore.Infrastructure.Database
{
  public class CategoryMongoDb : BaseMongoDb<Category>
    {
        public CategoryMongoDb(string mongoDBConnectionString, string dbName, string collectionName) :base(mongoDBConnectionString, dbName, collectionName)
        {}
    }
}
