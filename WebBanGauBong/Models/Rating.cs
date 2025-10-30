namespace WebBanGauBong.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        [StringLength(10)]
        public string RatingID { get; set; }

        [StringLength(10)]
        public string ProductID { get; set; }

        [StringLength(10)]
        public string UserID { get; set; }

        public double? Star { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        public virtual Product Product { get; set; }
    }
}
