namespace DemoAppForRPATesting.Models;

public class Attribute
{
    public string Name { get; set; }
    public string Value { get; set; }

    public Attribute(string name, string value)
    {
        Name = name;
        Value = value;
    }
}