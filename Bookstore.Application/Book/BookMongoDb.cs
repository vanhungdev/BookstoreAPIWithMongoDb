using Bookstore.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Application.Book
{
    public class BookMongoDb : BaseMongoDb<Infrastructure.Entities.Book>
    {
        public BookMongoDb(string mongoDBConnectionString, string dbName, string collectionName) :
              base(mongoDBConnectionString, dbName, collectionName)
        { }
    }
}
