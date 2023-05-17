using DemoAppForRPATesting.Models.Enum;

namespace DemoAppForRPATesting.Models;

public class Person
{
    public Person(Salutation salutation, string title, string firstName, string lastName, DateTime birthday, Address address)
    {
        Salutation = salutation;
        Title = title;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
        Address = address;
    }

    public Salutation Salutation { get; set; }
    public string Title { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthday { get; set; }
    public Address Address { get; set; }

}