using SQLite;

namespace Taxio
{
    public class Car_vendorV3
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Car_Vendors { get; set; }
    }
}

