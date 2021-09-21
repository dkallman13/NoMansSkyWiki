using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Data
{
    public class Ingredient
    {
        [Key]
        public int PlanetResourceId { get; set; }

        [Required]
        public virtual int CraftableId { get; set; }

        [Required]
        public virtual int ResourceId { get; set; }
    }
}
