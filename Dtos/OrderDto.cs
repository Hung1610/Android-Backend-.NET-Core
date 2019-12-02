using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Dtos
{
    public class OrderDto
    {
        public long FoodId { get; set; }
        public int? Quantity { get; set; }
    }
}
