using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinApp
{
    class OwnersMagazijn
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Magazijn")]
        public int MagazijnId { get; set; }
        public Magazijn Magazijn { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
