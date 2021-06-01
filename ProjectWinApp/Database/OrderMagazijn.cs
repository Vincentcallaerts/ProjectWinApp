using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinApp
{
    class OrderMagazijn
    {
        [Key]
        public int OrderMagazijnId { get; set; }
        
        public int ProductId { get; set; }
       
        [ForeignKey("Magazijn")]
        public int MagazijnId { get; set; }
        public Magazijn Magazijn { get; set; }

        public int Amount { get; set; }
        public double UnitPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
