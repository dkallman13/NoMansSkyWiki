using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMSWiki.Data
{
    public class PlanetResource
    {
        [Key]
        public int PlanetResourceId { get; set; }

        [Required]
        [ForeignKey(nameof(PlanetType))]
        public int PlanetTypeId { get; set; }

        [Required]
        [ForeignKey(nameof(Resource))]
        public int ResourceId { get; set; }
    }
}
