using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Taxio
{
    public class CarsV3
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Car_Stat { get; set; }
        public string Driver_name { get; set; }
        public string car_vendors { get; set; }
        public string Car_Gos { get; set; }
    }
}
