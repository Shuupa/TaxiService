using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace Taxio
{
    public class DB
    {
        private readonly SQLiteConnection conn;

        public DB(string path)
        {
            conn = new SQLiteConnection(path);
            conn.CreateTable<User>();
        }
        public List<User> GetUsers()
        {
            return conn.Table<User>().ToList();
        }

        public int SaveUser(User user)
        {
            return conn.Insert(user);
        }
    }
}
