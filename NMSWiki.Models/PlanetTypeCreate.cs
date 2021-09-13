using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Models
{
    public class PlanetTypeCreate
    {
        [Required]
        public string Name { get; set; }
    }
}
