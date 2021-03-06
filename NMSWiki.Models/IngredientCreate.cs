using NMSWiki.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Models
{
    public class IngredientCreate
    {

        [Required]
        [ForeignKey(nameof(Craftable))]
        public int CraftableId { get; set; }

        [Required]
        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }
    }
}
