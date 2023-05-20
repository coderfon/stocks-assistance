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
        [MaxLength(100)]
        public string? Website { get; set; }
        [Required]
        [MaxLength(20)]
        public string Currency { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Country { get; set; } = string.Empty;
        [Required]
        public int SectorId { get; set; }
        public virtual Sector Sector { get; set; } = null!;
        public int IndustryId { get; set; }
        public virtual Industry Industry { get; set; } = null!;


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

        public virtual ICollection<Attribute> Attributes { get; set; } = new HashSet<Attribute>();
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public virtual ICollection<Log> Logs { get; set; } = new HashSet<Log>();
    }
}