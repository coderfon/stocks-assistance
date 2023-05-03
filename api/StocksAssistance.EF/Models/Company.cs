namespace StocksAssistance.EF.Models
{
    // TODO: Add Company class with properties: Id, Name, Symbol, Description, Sector, Industry, Website, LogoUrl, Exchange, Currency, Country, Type, CreatedAt, UpdatedAt
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Description { get; set; }
        public string Sector { get; set; }
        public string Industry { get; set; }
        public string Website { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
    }
}