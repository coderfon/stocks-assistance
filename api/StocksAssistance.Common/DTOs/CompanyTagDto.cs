using StocksAssistance.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Common.DTOs
{
    public class CompanyTagDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public CompanyTagType Type { get; set; }
    }
}
