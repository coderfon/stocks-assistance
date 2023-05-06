// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class AssetProfile
{
    public string address1 { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string zip { get; set; }
    public string country { get; set; }
    public string phone { get; set; }
    public string fax { get; set; }
    public string website { get; set; }
    public string industry { get; set; }
    public string sector { get; set; }
    public string longBusinessSummary { get; set; }
    public int fullTimeEmployees { get; set; }
    public List<CompanyOfficer> companyOfficers { get; set; }
    public int auditRisk { get; set; }
    public int boardRisk { get; set; }
    public int compensationRisk { get; set; }
    public int shareHolderRightsRisk { get; set; }
    public int overallRisk { get; set; }
    public int governanceEpochDate { get; set; }
    public int compensationAsOfEpochDate { get; set; }
    public int maxAge { get; set; }
}

public class CompanyOfficer
{
    public int maxAge { get; set; }
    public string name { get; set; }
    public int age { get; set; }
    public string title { get; set; }
    public int yearBorn { get; set; }
    public int fiscalYear { get; set; }
    public TotalPay totalPay { get; set; }
    public ExercisedValue exercisedValue { get; set; }
    public UnexercisedValue unexercisedValue { get; set; }
}

public class CurrentPrice
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class CurrentRatio
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class DebtToEquity
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class EarningsGrowth
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class Ebitda
{
    public long raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class EbitdaMargins
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class ExercisedValue
{
    public int raw { get; set; }
    public object fmt { get; set; }
    public string longFmt { get; set; }
}

public class FinancialData
{
    public int maxAge { get; set; }
    public CurrentPrice currentPrice { get; set; }
    public TargetHighPrice targetHighPrice { get; set; }
    public TargetLowPrice targetLowPrice { get; set; }
    public TargetMeanPrice targetMeanPrice { get; set; }
    public TargetMedianPrice targetMedianPrice { get; set; }
    public RecommendationMean recommendationMean { get; set; }
    public string recommendationKey { get; set; }
    public NumberOfAnalystOpinions numberOfAnalystOpinions { get; set; }
    public TotalCash totalCash { get; set; }
    public TotalCashPerShare totalCashPerShare { get; set; }
    public Ebitda ebitda { get; set; }
    public TotalDebt totalDebt { get; set; }
    public QuickRatio quickRatio { get; set; }
    public CurrentRatio currentRatio { get; set; }
    public TotalRevenue totalRevenue { get; set; }
    public DebtToEquity debtToEquity { get; set; }
    public RevenuePerShare revenuePerShare { get; set; }
    public ReturnOnAssets returnOnAssets { get; set; }
    public ReturnOnEquity returnOnEquity { get; set; }
    public GrossProfits grossProfits { get; set; }
    public FreeCashflow freeCashflow { get; set; }
    public OperatingCashflow operatingCashflow { get; set; }
    public EarningsGrowth earningsGrowth { get; set; }
    public RevenueGrowth revenueGrowth { get; set; }
    public GrossMargins grossMargins { get; set; }
    public EbitdaMargins ebitdaMargins { get; set; }
    public OperatingMargins operatingMargins { get; set; }
    public ProfitMargins profitMargins { get; set; }
    public string financialCurrency { get; set; }
}

public class FreeCashflow
{
    public long raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class GrossMargins
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class GrossProfits
{
    public long raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class NumberOfAnalystOpinions
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class OperatingCashflow
{
    public long raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class OperatingMargins
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class ProfitMargins
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class QuickRatio
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class QuoteSummary
{
    public List<Result> result { get; set; }
    public object error { get; set; }
}

public class RecommendationMean
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class Result
{
    public AssetProfile assetProfile { get; set; }
    public FinancialData financialData { get; set; }
}

public class ReturnOnAssets
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class ReturnOnEquity
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class RevenueGrowth
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class RevenuePerShare
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class QuoteSummaryRoot
{
    public QuoteSummary quoteSummary { get; set; }
}

public class TargetHighPrice
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class TargetLowPrice
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class TargetMeanPrice
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class TargetMedianPrice
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class TotalCash
{
    public long raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class TotalCashPerShare
{
    public double raw { get; set; }
    public string fmt { get; set; }
}

public class TotalDebt
{
    public long raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class TotalPay
{
    public int raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class TotalRevenue
{
    public long raw { get; set; }
    public string fmt { get; set; }
    public string longFmt { get; set; }
}

public class UnexercisedValue
{
    public int raw { get; set; }
    public object fmt { get; set; }
    public string longFmt { get; set; }
}

