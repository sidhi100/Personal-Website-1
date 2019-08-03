using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final4.Models
{
    
    public class ResearchPublication
    {
        [Key]
        public int ResearchPublicationId { get; set; }
        public string researchName { get; set; }
        public string Conference { get; set; }
        public string researchYear { get; set; }
        public string link { get; set; }
        public string researchImage { get; set; }
    }
}
