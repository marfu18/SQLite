using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SQLite.controlador
{
    public class conexion
    {
        private string pthdb;

        public conexion() { }

        public string Conectar()
        {
            string dbname = "db.sqlite";
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            pthdb = Path.Combine(path, dbname);
            return pthdb;

        }

        public SQLiteConnection Conn()
        {
            SQLiteConnection conn = new SQLiteConnection(App.UBICACIONDB);
            return conn;
        }


        public SQLiteAsyncConnection GetConnectionAsync()
        {
            return new SQLiteAsyncConnection(App.UBICACIONDB);
        }

    }
}

