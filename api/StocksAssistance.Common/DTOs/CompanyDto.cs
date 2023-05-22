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
        public string Symbol { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public SectorDto Sector { get; set; }
        public IndustryDto Industry { get; set; }
        public string Country { get; set; }

        // Financials
        public double Price { get; set; }
        public double Last52WeekHigh { get; set; }
        public double Last52WeekLow { get; set; }
        public double MarketCap { get; set; }
        public double LTM_PE { get; set; }
        public double NTM_PE { get; set; }
        public double PriceBookRatio { get; set; }
        public double DividendYield { get; set; }
        public double ROE { get; set; }
    }
}
