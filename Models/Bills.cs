using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public partial class Bills
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public long Id { get; set; }
        public int? Flag { get; set; }
        public int? CreatorId { get; set; }
        public DateTime? CreateTime { get; set; }
        public long TableId { get; set; }
        public Tables Table { get; set; }
        public long? TotalPayment { get; set; }
    }
}
