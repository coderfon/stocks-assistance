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
        [MaxLength(5000)]
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

        public double Price { get; set; }
        public double Last52WeekHigh { get; set; }
        public double Last52WeekLow { get; set; }
        public double MarketCap { get; set; }
        public double LTM_PE { get; set; }
        public double NTM_PE { get; set; }
        public double PriceBookRatio { get; set; }
        public double DividendYield { get; set; }
        public double ROE { get; set; }

        #endregion

        public virtual ICollection<CompanyAttribute> Attributes { get; set; } = new HashSet<CompanyAttribute>();
        public virtual ICollection<CompanyTag> Tags { get; set; } = new HashSet<CompanyTag>();
        public virtual ICollection<CompanyLog> Logs { get; set; } = new HashSet<CompanyLog>();
    }
}