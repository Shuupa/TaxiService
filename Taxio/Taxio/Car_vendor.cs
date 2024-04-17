using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Taxio
{
    public class Car_vendorV2
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Car_Vendors { get; set; }
    }
}

