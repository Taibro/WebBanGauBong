namespace WebBanGauBong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Discount")]
    public partial class Discount
    {
        [StringLength(10)]
        public string DiscountID { get; set; }

        [StringLength(10)]
        public string ProductID { get; set; }

        [StringLength(50)]
        public string DiscountName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }

        public double? DiscountRate { get; set; }

        public virtual Product Product { get; set; }
    }
}
