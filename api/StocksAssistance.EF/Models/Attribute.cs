using StocksAssistance.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.EF.Models
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Value { get; set; } = string.Empty;
        [Required]
        public AttributeType Type { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = null!;
    }
}
