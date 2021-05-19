using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinApp
{
    class Order
    {

        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Magazijn")]
        public int MagazijnId { get; set; }
        public Magazijn Magazijn { get; set; }

        public int Amount { get; set; }


    }
}
