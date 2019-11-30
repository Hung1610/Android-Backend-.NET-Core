using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models
{
    public partial class Orders
    {
        public long BillId {get;set;}
        public Bills Bill { get; set; }
        public long FoodId { get; set; }
        public Foods Food { get; set; }
        public int? Quantity { get; set; }
        public long? PaymentAmount { get; set; }
        public int? Flag { get; set; }
    }
}
