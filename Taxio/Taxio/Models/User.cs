using SQLite;

namespace Taxio.Models
{
    public class User
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int User_id { get; set; }

        public string User_name { get; set; }

    }
}
