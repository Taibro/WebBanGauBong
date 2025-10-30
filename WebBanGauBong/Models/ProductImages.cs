namespace WebBanGauBong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductImages
    {
        [Key]
        [StringLength(10)]
        public string ProductImageID { get; set; }

        [StringLength(10)]
        public string ProductID { get; set; }

        [StringLength(500)]
        public string ImageURL { get; set; }

        public virtual Product Product { get; set; }
    }
}
