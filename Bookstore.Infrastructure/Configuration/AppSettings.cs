using System;
using System.Collections.Generic;
using System.Text;
using static Bookstore.Infrastructure.Configuration.AppSettingDetail;

namespace Bookstore.Infrastructure.Configuration
{
    public class AppSettings
    {
        public ConnectionStringSettings ConnectionStringSettings { get; set; }
    }
    public class AppSettingDetail
    {
        public class ConnectionStringSettings
        {
            public string mongodbConnectString { get; set; }
            public string MongoDatabaseName { get; set; }
            public string CategoryCollectionName { get; set; }
        }
       
    }
}
