using StocksAssistance.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.EF.Models
{
    public class CompanyAttribute
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Value { get; set; } = string.Empty;
        [Required]
        public CompanyAttributeType Type { get; set; }
        public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
