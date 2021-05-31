using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinApp
{
    class ProductsOrder
    {
        [Key]
        public int ProductsOrderId { get; set; }

        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Magazijn")]
        public int MagazijnId { get; set; }
        public Magazijn Magazijn { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int Amount { get; set; }
        public int OrderUnitPrice { get; set; }

    }
}
