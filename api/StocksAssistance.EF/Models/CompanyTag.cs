using StocksAssistance.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.EF.Models
{
    public class CompanyTag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public CompanyTagType Type { get; set; }
        [Required]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = null!;
    }
}
