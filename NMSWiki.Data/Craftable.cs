using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Data
{
    public class Craftable
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Ingredient> Ingredients { get; set; }

    }
}
