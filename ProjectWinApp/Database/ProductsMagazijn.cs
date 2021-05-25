using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinApp
{
    class ProductsMagazijn
    {

        [Key]
        [Column(Order = 1)]

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Magazijn")]
        public int MagazijnId { get; set; }
        public Magazijn Magazijn { get; set; }

        public int Amount { get; set; }


    }
}
