using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Common.DTOs
{
    public class CompanyOptionsDto
    {
        public IEnumerable<SectorDto> Sectors { get; set; }
        public IEnumerable<IndustryDto> Industries { get; set; }
        public IEnumerable<TagDto> Tags { get; set; }
    }
}
