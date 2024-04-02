using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Taxio
{
    public class User
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int User_id { get; set; }

        public string User_name { get; set; }

    }
}
