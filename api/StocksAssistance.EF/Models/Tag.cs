using StocksAssistance.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StocksAssistance.EF.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public TagType Type { get; set; }
        public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
