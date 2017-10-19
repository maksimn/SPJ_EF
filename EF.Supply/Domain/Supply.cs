using System.ComponentModel.DataAnnotations;

namespace EF.SupplyData.Domain {
    public class Supply {
        public int Id { get; set; }
        [Required]
        public Shipper Shipper { get; set; }
        [Required]
        public Part Part { get; set; }
        [Required]
        public Project Project { get; set; }
        public int Quantity { get; set; }
    }
}
