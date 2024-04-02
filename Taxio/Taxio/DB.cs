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
            conn.CreateTable<Car_vendor>();
        }
        public List<Car_vendor> GetCar_Vendors() 
        {
            return conn.Table<Car_vendor>().ToList();
        }
        public int SaveVendor(Car_vendor vendor)
        {
            return conn.Insert(vendor);
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
