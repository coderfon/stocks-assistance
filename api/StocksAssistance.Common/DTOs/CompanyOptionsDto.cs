using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Common.DTOs
{
    public class CompanyOptionsDto
    {
        public List<SectorDto> Sectors { get; set; }
        public List<IndustryDto> Industries { get; set; }
        public List<TagDto> Tags { get; set; }
    }
}
