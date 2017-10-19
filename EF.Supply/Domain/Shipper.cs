namespace EF.SupplyData.Domain {
    public class Shipper {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; } // range  0 ... 100
        public string City { get; set; }
    }
}
