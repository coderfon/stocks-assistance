using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Common.DTOs
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; } // TODO: Add Symbol property
        public string Description { get; set; } // TODO: Add Description property
        public SectorDto Sector { get; set; } // TODO: Add Sector property
        public IndustryDto Industry { get; set; } // TODO: Add Industry property
        public string Website { get; set; } // TODO: Add Website property
        public string LogoUrl { get; set; } // TODO: Add LogoUrl property
        public string Exchange { get; set; } // TODO: Add Exchange property
        public string Currency { get; set; } // TODO: Add Currency property
        public string Country { get; set; } // TODO: Add Country property
        public string Type { get; set; } // TODO: Add Type property
        public DateTime CreatedAt { get; set; } // TODO: Add CreatedAt property
        public DateTime UpdatedAt { get; set; } // TODO: Add UpdatedAt property
    }
}
