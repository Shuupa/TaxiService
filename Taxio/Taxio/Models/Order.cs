using SQLite;

namespace Taxio
{
    public class OrderData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Driver_Name_Car { get; set; }
        public string Price { get; set; }
        public string Distance { get; set; }
        public string OrderClass { get; set; }
        public string Gos_Car { get; set; }
        public string Adress_A_B { get; set; }
    }
}
