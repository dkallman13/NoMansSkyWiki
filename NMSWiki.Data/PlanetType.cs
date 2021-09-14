using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Data
{
    public class PlanetType
    {
        [Key]
        public int PlanetTypeId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
