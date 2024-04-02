using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Taxio
{
    public class Car_vendor
    {
        [PrimaryKey, AutoIncrement]
        public int ID {get; set;}
        public string Car_Vendors { get; set;}
    }
}

