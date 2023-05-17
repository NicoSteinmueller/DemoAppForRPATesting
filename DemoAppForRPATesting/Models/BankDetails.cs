namespace DemoAppForRPATesting.Models;

public class BankDetails
{
    public BankDetails(string bank, string iban)
    {
        Bank = bank;
        IBAN = iban;
    }

    public string Bank { get; set; }
    public string IBAN { get; set; }
}