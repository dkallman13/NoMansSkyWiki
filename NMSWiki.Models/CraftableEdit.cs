using NMSWiki.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Models
{
    public class CraftableEdit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Ingredient> Ingredients { get; set; }
    }
}
