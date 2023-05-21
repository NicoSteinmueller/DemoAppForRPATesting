using DemoAppForRPATesting.Models.Enum;

namespace DemoAppForRPATesting.Models;

public class Address
{
    public Address(string street, string houseNumber, string zipCode, string city, Country country)
    {
        Street = street;
        HouseNumber = houseNumber;
        ZipCode = zipCode;
        City = city;
        Country = country;
    }

    public string Street { get; set; }
    public string HouseNumber { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public Country Country { get; set; }
}
