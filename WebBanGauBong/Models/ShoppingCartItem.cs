namespace WebBanGauBong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ShoppingCartItem")]
    public partial class ShoppingCartItem
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string ShoppingCartID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string ProductSizeID { get; set; }

        public int? Quantity { get; set; }

        public virtual ProductSize ProductSize { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
