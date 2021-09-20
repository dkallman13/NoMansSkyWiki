using NMSWiki.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Models
{
    public class CraftableIngredientLookup
    {
        public Ingredient ingredient { get; set; }
        public Craftable craftable { get; set; }
    }
}
