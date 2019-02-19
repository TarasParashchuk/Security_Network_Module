using Security_network_module.Model;
using System;
using System.IO;

namespace Security_network_module.DataBase
{
    class DataBaseConnection
    {
        SQLite.SQLiteConnection database;
        public const string DBFileName = "DataBaseSecuritnetwormodule.db";

        public DataBaseConnection()
        {
            var path = GetDatabasePath(DBFileName);
            if (!File.Exists(path))
            {
                database = new SQLite.SQLiteConnection(path);
                database.CreateTable<ModelTabelNetworkParameter>();
                database.CreateTable<ModelTabelParameter>();
                database.CreateTable<ModelGaps>();
            }
            else database = new SQLite.SQLiteConnection(path);
        }

        public SQLite.SQLiteConnection SQLiteConnection()
        {
            return database;
        }

        public string GetDatabasePath(string sqliteFilename)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(documentsPath, sqliteFilename);
            return path;
        }
    }
}