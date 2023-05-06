using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Common.DTOs
{
    public class CompanySetupDto
    {
        public List<CompanyTagDto> Tags { get; set; }
        public List<CompanyAttributeDto> Attributes { get; set; }
    }
}
