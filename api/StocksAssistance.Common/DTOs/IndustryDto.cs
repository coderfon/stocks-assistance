using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Common.DTOs
{
    public class IndustryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SectorId { get; set; }
    }
}
