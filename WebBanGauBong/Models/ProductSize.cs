namespace WebBanGauBong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductSize")]
    public partial class ProductSize
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductSize()
        {
            OrderDetail = new HashSet<OrderDetail>();
            ShoppingCartItem = new HashSet<ShoppingCartItem>();
        }

        public int ProductSizeID { get; set; }

        public int? ProductID { get; set; }

        public int? SizeName { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ShoppingCartItem> ShoppingCartItem { get; set; }
    }
}
