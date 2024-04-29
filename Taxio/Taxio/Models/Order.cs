using SQLite;

namespace Taxio
{
    public class OrderDataV4
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Driver_Name_Car { get; set; }
        public string Price { get; set; }
        public string OrderClass { get; set; }
        public string Adress_A_B { get; set; }
        public int PaymentOption { get; set; }
    }
}
