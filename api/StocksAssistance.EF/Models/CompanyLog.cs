using StocksAssistance.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace StocksAssistance.EF.Models
{
    public class CompanyLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public CompanyLogType Type { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
