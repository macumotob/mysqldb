﻿using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mysqldb
{
    public static class mdb
    {
        private static string _host = string.Empty;
        private static int _port= 3306;
        private static string _username = string.Empty;
        private static string _password = string.Empty;
        private static string _database = string.Empty;

        public static void use(string host,string user,string password,string databse)
        {
            _host = host;
            _database = databse;
            _username = user;
            _password = password;
        }
        private static MySqlConnection _GetDBConnection()
        {
            String connString = "Server=" + _host + ";Database=" + _database
                      + ";port=" + _port + ";User Id=" + _username + ";password=" + _password
                       //+ ";TLS Version=TLS 1.3";
                       + ";SslMode=none;"
                       ;
            var conn = new MySqlConnection(connString);
            return conn;
        }
        private static MySqlConnection Connect()
        {
            //return ConnectToVasylDb();
            var conn = _GetDBConnection();
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
                Console.WriteLine(ex.Message);
            }
        }

        public static Result exec<T>(T data, Func<MySqlCommand, T, Result> action)
        {
            MySqlConnection conn = null; 
            try
            {
                var result = Result.Error("som error");

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
                return Result.Error(ex.Message);
            }
            finally
            {
                mdb.Close(conn);
            }
        }
        public static Result exec<T>(T data, Func<MySqlConnection, MySqlCommand, T, Result> action)
        {
            MySqlConnection conn = null;
            try
            {
                var result = Result.Error("som error");

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
                return Result.Error(ex.Message);
            }
            finally
            {
                mdb.Close(conn);
            }

        }
        public static Result exec(Func<MySqlConnection, MySqlCommand, Result> action)
        {
            MySqlConnection conn = null;
            try
            {
                var result = Result.Error("som error");
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
                return Result.Error(ex.Message);
            }
            finally
            {
                mdb.Close(conn);
            }

        }

    }
}
