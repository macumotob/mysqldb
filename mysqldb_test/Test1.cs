using Google.Protobuf;
using mysqldb;

namespace mysqldb_test
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            mdb.use("localhost", "user_for_test", "maXimka!@#$ivAn", "hour_prod");
            var result = mdb.exec((cnn, cmd) =>
            {
                return Result.Success();
            });

        }
        [TestMethod]
        public void TestMethod2()
        {
            mdb.use("localhost", "user_for_test", "maXimka!@#$ivAn", "hour_prod");
            var result = mdb.exec((cnn, cmd) =>
            {
                var count = 0;
                cmd.CommandText = "select * from note";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                    reader.Close();
                }
                return Result.Success(count);
            });

        }
        [TestMethod]
        public void TestMethod3()
        {
            mdb.use("localhost", "user_for_test", "maXimka!@#$ivAn", "hour_prod");
            var result = mdb.exec((cnn, cmd) =>
            {
                var count = 0;
                cmd.CommandText = "select * from task";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        count++;
                    }
                    reader.Close();
                }
                return Result.Success(count);
            });

        }
    }
}
