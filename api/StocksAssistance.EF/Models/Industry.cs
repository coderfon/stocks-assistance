using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.EF.Models
{
    public class Industry
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int SectorId { get; set; }
        public virtual Sector Sector { get; set; } = null!;
        public virtual ICollection<Company> Companies { get; set; } = new HashSet<Company>();
    }
}
