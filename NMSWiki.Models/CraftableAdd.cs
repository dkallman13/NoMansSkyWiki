using NMSWiki.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Models
{
    public class CraftableAdd
    {
        [Required]
        public string Name { get; set; }
        public string IngredientId { get; set; }

    }
}
