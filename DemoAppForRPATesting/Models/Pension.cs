using DemoAppForRPATesting.Models.Enum;

namespace DemoAppForRPATesting.Models;

public class Pension
{
    public Pension(string id, Person person, string panr, string prnr, PensionType type, bool pensionCard, PayoutMethod payoutMethod, PensionStatus pensionStatus, BankDetails bankDetails)
    {
        Id = id;
        Person = person;
        PANR = panr;
        PRNR = prnr;
        Type = type;
        PensionCard = pensionCard;
        PayoutMethod = payoutMethod;
        PensionStatus = pensionStatus;
        BankDetails = bankDetails;
    }

    public string Id { get; set; }
    public Person Person { get; set; } 
    public string PANR { get; set; } //Postabrechnungsnummer
    public string PRNR { get; set; } //Postrentennummer
    public PensionType Type { get; set; } //Retenart
    public bool PensionCard { get; set; } //Rentenausweis
    public PayoutMethod PayoutMethod { get; set; } //Auszahlungsart
    public PensionStatus PensionStatus { get; set; } //Rentenstatus
    public BankDetails BankDetails { get; set; }
}