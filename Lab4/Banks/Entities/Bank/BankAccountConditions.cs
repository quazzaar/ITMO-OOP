namespace Banks.Entities;

public class BankAccountConditions
{
    public BankAccountConditions(
        string name,
        decimal suspiciousRestriction,
        decimal debitPercent,
        decimal depositPercent,
        decimal depositRateIncreasing,
        decimal creditLimit,
        decimal creditCommission,
        int expirationDate)
    {
        Name = name;
        SuspiciousRestriction = suspiciousRestriction;
        DebitPercent = debitPercent;
        DepositPercent = depositPercent;
        DepositRateIncreasing = depositRateIncreasing;
        CreditLimit = creditLimit;
        CreditCommission = creditCommission;
        ExpirationDate = expirationDate;
    }

    public string Name { get; }
    public decimal SuspiciousRestriction { get; set; }
    public decimal DebitPercent { get; set; }
    public decimal DepositPercent { get; set; }
    public decimal DepositRateIncreasing { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CreditCommission { get; set; }
    public int ExpirationDate { get; set; }
}