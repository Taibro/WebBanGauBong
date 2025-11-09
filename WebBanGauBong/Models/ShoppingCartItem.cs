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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ShoppingCartID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductSizeID { get; set; }

        public int? Quantity { get; set; }

        public virtual ProductSize ProductSize { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
