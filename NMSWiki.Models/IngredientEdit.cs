using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Models
{
    public class IngredientEdit
    {
        public int IngredientId { get; set; }

        public int CraftableId { get; set; }
        public int ResourceId { get; set; }
    }
}
