using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final4.Models
{
    public class FilesMetadata
    {
        [Key]
        public int FilesMetadataId { get; set; }
        public string FileName { get; set; }
        public string FileImage { get; set; }
        
    }

        

}
