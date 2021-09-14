using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Data
{
    public class Craftable
    {
        [Key]
        public int CraftableId { get; set; }
        public string Name { get; set; }
        public virtual int[] IngredientId { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }

    }
}
