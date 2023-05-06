using StocksAssistance.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Common.DTOs
{
    public class CompanyAttributeDto
    {
        [Required]
        [MaxLength(20)]
        public string Value { get; set; } = string.Empty;
        [Required]
        public CompanyAttributeType Type { get; set; }
    }
}
