using StocksAssistance.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace StocksAssistance.EF.Models
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public LogType Type { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        [Required]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = null!;
    }
}
