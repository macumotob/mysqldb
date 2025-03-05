using MySql.Data.MySqlClient;
namespace mysqldb
{
    public static class mdb
    {
        private static string _host = string.Empty;
        private static int _port= 3306;
        private static string _username = string.Empty;
        private static string _password = string.Empty;
        private static string _database = string.Empty;

        public static string Database => _database;
        public static void use(string host,string user,string password,string databse)
        {
            _host = host;
            _database = databse;
            _username = user;
            _password = password;
        }
        private static MySqlConnection _connect()
        {
     
            string connString = $"Server={_host};Database={_database};Port={_port};User Id={_username};Password={_password};SslMode=none;";

            var conn = new MySqlConnection(connString);
            return conn;
        }
        private static MySqlConnection Connect()
        {
            var conn = _connect();
            conn.Open();
            return conn;
        }

        private static void Close(MySqlConnection conn)
        {
            try
            {
                if (conn == null)
                {
                    return;
                }
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            catch (Exception ex)
            {
               throw new Exception(_database, ex);
            }
        }

        public static DbResult exec<T>(T data, Func<MySqlCommand, T, DbResult> action)
        {
            MySqlConnection conn = null; 
            try
            {
                var result = DbResult.Error("som error");

                using (conn = mdb.Connect())
                {
                    using (var command = conn.CreateCommand())
                    {
                        result = action(command, data);
                    }
                    mdb.Close(conn);
                    return result;
                }

            }
            catch (Exception ex)
            {
                return DbResult.Error(ex.Message);
            }
            finally
            {
                mdb.Close(conn);
            }
        }
        public static DbResult exec<T>(T data, Func<MySqlConnection, MySqlCommand, T, DbResult> action)
        {
            MySqlConnection conn = null;
            try
            {
                var result = DbResult.Error("som error");

                using (conn = mdb.Connect())
                {
                    using (var command = conn.CreateCommand())
                    {
                        result = action(conn, command, data);
                    }
                    mdb.Close(conn);
                    return result;
                }

            }
            catch (Exception ex)
            {
                return DbResult.Error(ex.Message);
            }
            finally
            {
                mdb.Close(conn);
            }

        }
        public static DbResult exec(Func<MySqlConnection, MySqlCommand, DbResult> action)
        {
            MySqlConnection conn = null;
            try
            {
                var result = DbResult.Error("som error");
                using (conn = mdb.Connect())
                {
                    using (var command = conn.CreateCommand())
                    {
                        result = action(conn, command);
                    }
                    mdb.Close(conn);
                    return result;
                }

            }
            catch (Exception ex)
            {
                return DbResult.Error(ex.Message);
            }
            finally
            {
                mdb.Close(conn);
            }

        }
        public static DbResult exec(Func<MySqlCommand, DbResult> action)
        {
            MySqlConnection conn = null;
            try
            {
                var result = DbResult.Error("som error");
                using (conn = mdb.Connect())
                {
                    using (var command = conn.CreateCommand())
                    {
                        result = action(command);
                    }
                    mdb.Close(conn);
                    return result;
                }

            }
            catch (Exception ex)
            {
                return DbResult.Error(ex.Message);
            }
            finally
            {
                mdb.Close(conn);
            }

        }
    }
}
