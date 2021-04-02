using Bookstore.Infrastructure.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookstore.Infrastructure.Utilities
{
    public static partial class Helper
    {
        public static AppSettings Settings => AppSettingServices.Get;
    }
}
