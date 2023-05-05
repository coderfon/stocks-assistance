using System.ComponentModel.DataAnnotations;

namespace StocksAssistance.EF.Models
{
    // TODO: Add Company class with properties: Id, Name, Symbol, Description, Sector, Industry, Website, LogoUrl, Exchange, Currency, Country, Type, CreatedAt, UpdatedAt
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1000)]
        public string? Description { get; set; }
        [Required]
        [MaxLength(100)]
        public string Sector { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Industry { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? Website { get; set; }
        [Required]
        [MaxLength(20)]
        public string Currency { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;


        #region Stats

        public decimal Price { get; set; }
        public decimal Last52WeekHigh { get; set; }
        public decimal Last52WeekLow { get; set; }
        public decimal MarketCap { get; set; }
        public decimal LTM_PE { get; set; }
        public decimal NTM_PE { get; set; }
        public decimal PriceBookRatio { get; set; }
        public decimal DividendYield { get; set; }
        public decimal ROE { get; set; }

        #endregion

        public virtual List<CompanyTag> Tags { get; set; } = new List<CompanyTag>();
    }
}