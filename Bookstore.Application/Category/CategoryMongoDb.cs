
using Bookstore.Infrastructure.Database;
using System;

namespace Bookstore.Application.Category
{
    public class CategoryMongoDb : BaseMongoDb<Infrastructure.Entities.Category>
    {
        public CategoryMongoDb(string mongoDBConnectionString, string dbName, string collectionName) :
            base(mongoDBConnectionString, dbName, collectionName)
        { }
    }
}
