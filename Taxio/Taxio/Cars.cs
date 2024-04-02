using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Taxio
{
    public class Cars
    {
        [PrimaryKey, AutoIncrement]
        public int ID {  get; set; }
        public string Car_Stat { get; set; }

    }
}
