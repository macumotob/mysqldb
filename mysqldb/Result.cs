using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysqldb
{
    public class Result
    {
        public bool success { get; set; }
        public object? data { get; set; }
      
        public Result(bool success, object data)
        {
            this.success = success;
            this.data = data;
        }
        public static Result Error(string msg) => new Result(false, msg);
        public static Result Error(Exception ex) => new Result(true, ex.Message);
        public static Result Success(object data) => new Result(true,data);
        public static Result Success() => new Result(true,null);
      
    }
}
