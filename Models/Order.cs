using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCGShop.Models
{
    public class Order
    {
        public int ID { get; set; }

        public DateTime CreatedTime { get; set; }

        public Cart Cart { get; set; }

        public int ID_DeliveryType { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }
    }
}
