using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysqldb
{
    public class DbResult
    {
        public bool success { get; set; }
        public object? data { get; set; }
      
        public DbResult(bool success, object data)
        {
            this.success = success;
            this.data = data;
        }
        public static DbResult Error(string msg) => new DbResult(false, msg);
        public static DbResult Error(Exception ex) => new DbResult(true, ex.Message);
        public static DbResult Success(object data) => new DbResult(true,data);
        public static DbResult Success() => new DbResult(true,null);
      
    }
}
