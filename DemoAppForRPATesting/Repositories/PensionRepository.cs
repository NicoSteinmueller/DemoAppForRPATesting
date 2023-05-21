using DemoAppForRPATesting.Models;
using DemoAppForRPATesting.Models.Enum;

namespace DemoAppForRPATesting.Repositories;


public class PensionRepository : IPensionRepository
{
    private List<Pension> _pensions = new ();

    public void AddPension(Pension pension)
    {
        _pensions.Add(pension);
    }

    public Pension FindByPanrAndPrnr(string panr, string prnrPart)
    {
        return _pensions.Find(p => p.PANR == panr && p.PRNR.Contains(prnrPart))!;
    }

    public Pension FindById(string id)
    {
        return _pensions.Find(p => p.Id == id)!;
    }

   
    string CalculatePrnr(Person person)
    {
        string birthDate = person.Birthday.ToString("ddMMyy");
        string lastName = person.LastName.ToUpper();
        bool isFemale = person.Salutation != Salutation.Herr;

        string prnr = "";
        Random random = new Random();

        // Zwei zufällige Zahlen (0-9)
        prnr += random.Next(0, 10);
        prnr += random.Next(0, 10);

        // Geburtsdatum "ddMMyy"
        prnr += birthDate;

        // Anfangsbuchstabe des Nachnamens
        prnr += lastName[0];

        // Zahl zwischen 0-49 für Männer oder Zahl zwischen 50-99 für Frauen und Divers
        int minNumber = isFemale ? 50 : 0;
        int maxNumber = isFemale ? 99 : 49;
        prnr += random.Next(minNumber, maxNumber + 1).ToString("D2");

        // Prüfzifferberechnung
        int[] factors = { 2, 1, 2, 5, 7, 1, 2, 1, 2, 1, 2, 1 };
        int sum = 0;
        for (int i = 0; i < prnr.Length; i++)
        {
            int digit = ConvertLetterToNumber(prnr.Substring(i, 1).ToCharArray()[0]);
            sum += digit * factors[i];
        }
        int checkDigit = sum % 10;
        prnr += checkDigit;

        // Zwei zufällige Zahlen (0-9)
        prnr += random.Next(0, 10);
        prnr += random.Next(0, 10);
        
        return prnr;
    }

    
    int ConvertLetterToNumber(char letter)
    {
        if (char.IsLetter(letter))
        {
            // Wandelt den Buchstaben in Großbuchstaben um, um eine einheitliche Repräsentation zu gewährleisten
            letter = char.ToUpper(letter);

            // Berechnet den Offset vom ASCII-Wert des Buchstabens 'A' (65)
            int offset = letter - 'A';

            // Fügt 1 zum Offset hinzu, um von 0-basierter Indexierung zu 1-basierter Indexierung zu wechseln
            int number = offset + 1;

            return number;
        }
        else
        {
            return letter;
        }
    }
    
    
    
    
    
    public PensionRepository()
    { 
        // Beispiel 1
        Address address1 = new Address("Hauptstraße", "123", "12345", "Musterstadt", Country.Deutschland);
        Person person1 = new Person(Salutation.Herr, "Dr.", "Max", "Mustermann", new DateTime(1958, 5, 12), address1);
        BankDetails bankDetails1 = new BankDetails("Commerzbank", "DE12345678901234567");
        Pension pension1 = new Pension(
            "c0f54c7d-1093-4488-805e-418f16a905a8",
            person1,
            "002",
            "01120558M07815",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails1
        );
        _pensions.Add(pension1);
        
        // Beispiel 2
        Address address2 = new Address("Musterweg", "456", "54321", "Teststadt", Country.Deutschland);
        Person person2 = new Person(Salutation.Frau, "", "Anna", "Schmidt", new DateTime(1935, 9, 28), address2);
        BankDetails bankDetails2 = new BankDetails("Deutsche Bank", "DE98765432109876543");
        Pension pension2 = new Pension(
            "14eea4d6-4c3e-41e2-8d7e-ebee90b2737d",
            person2,
            "026",
            "42280935S57309",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails2
        );
        _pensions.Add(pension2);
        
        // Beispiel 3
        Address address3 = new Address("Bahnhofstraße", "789", "98765", "Musterstadt", Country.Deutschland);
        Person person3 = new Person(Salutation.Herr, "", "Peter", "Schneider", new DateTime(1950, 12, 8), address3);
        BankDetails bankDetails3 = new BankDetails("Sparkasse", "DE87654321098765432");
        Pension pension3 = new Pension(
            "cf462451-a947-4b24-b6ed-436274f5d656",
            person3,
            "010",
            "17081250S06047",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Barauszahlung,
            PensionStatus.Aktiv,
            bankDetails3
        );
        _pensions.Add(pension3);
        
        // Beispiel 4
        Address address4 = new Address("Am Markt", "456", "54321", "Teststadt", Country.Deutschland);
        Person person4 = new Person(Salutation.Frau, "", "Sabine", "Müller", new DateTime(1932, 3, 15), address4);
        BankDetails bankDetails4 = new BankDetails("Volksbank", "DE54321098765432109");
        Pension pension4 = new Pension(
            "33aec466-301f-4e24-8a03-3be6ced44a4c",
            person4,
            "013",
            "43150332M92054",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails4
        );
        _pensions.Add(pension4);
        
        // Beispiel 5
        Address address5 = new Address("Rathausplatz", "789", "98765", "Musterstadt", Country.Deutschland);
        Person person5 = new Person(Salutation.Herr, "Prof.", "Thomas", "Schmidt", new DateTime(1925, 7, 22), address5);
        BankDetails bankDetails5 = new BankDetails("Postbank", "DE01234567890123456");
        Pension pension5 = new Pension(
            "1bcd64b7-cc32-44a7-84e0-d524e73da860",
            person5,
            "021",
            "86220725S41866",
            PensionType.Hinterbliebenenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails5
        );
        _pensions.Add(pension5);
        
        
        // Beispiel 6
        Address address6 = new Address("Lindenstraße", "111", "12345", "Musterstadt", Country.Deutschland);
        Person person6 = new Person(Salutation.Herr, "", "Markus", "Keller", new DateTime(1920, 9, 10), address6);
        BankDetails bankDetails6 = new BankDetails("Commerzbank", "DE12345678901234567");
        Pension pension6 = new Pension(
            "0c391a0d-4173-4dcb-97b2-d1f6f32f3da7",
            person6,
            "002",
            "32100920K32412",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails6
        );
        _pensions.Add(pension6);
        
        // Beispiel 7
        Address address7 = new Address("Am See", "789", "98765", "Teststadt", Country.Deutschland);
        Person person7 = new Person(Salutation.Frau, "", "Laura", "Vogel", new DateTime(1947, 6, 28), address7);
        BankDetails bankDetails7 = new BankDetails("Sparkasse", "DE98765432109876543");
        Pension pension7 = new Pension(
            "326d6678-bd53-4291-8112-16b27da9a9a0",
            person7,
            "026",
            "97280647V66466",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Beendet,
            bankDetails7
        );
        _pensions.Add(pension7);
        
        // Beispiel 8
        Address address8 = new Address("Hauptstraße", "55", "54321", "Musterstadt", Country.Deutschland);
        Person person8 = new Person(Salutation.Frau, "", "Sabine", "Müller", new DateTime(1955, 4, 15), address8);
        BankDetails bankDetails8 = new BankDetails("Volksbank", "DE87654321098765432");
        Pension pension8 = new Pension(
            "8667a2a1-f5ae-4717-8c6f-9fff7641ab3e",
            person8,
            "009",
            "03150455M55235",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Barauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails8
        );
        _pensions.Add(pension8);
        
        // Beispiel 9
        Address address9 = new Address("Rosenweg", "7", "34567", "Musterstadt", Country.Deutschland);
        Person person9 = new Person(Salutation.Herr, "", "Stefan", "Schmidt", new DateTime(1955, 9, 20), address9);
        BankDetails bankDetails9 = new BankDetails("Postbank", "DE34567890123456789");
        Pension pension9 = new Pension(
            "b50f2c80-51ba-433c-a61d-265f6d1b07f9",
            person9,
            "018",
            "53200955S28924",
            PensionType.Betriebsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Beendet,
            bankDetails9
        );
        _pensions.Add(pension9);
        
        // Beispiel 10
        Address address10 = new Address("Ahornstraße", "15", "67890", "Musterstadt", Country.Deutschland);
        Person person10 = new Person(Salutation.Herr, "Dr.", "Maximilian", "Lehmann", new DateTime(1965, 9, 12), address10);
        BankDetails bankDetails10 = new BankDetails("Volksbank", "DE98765432101234567");
        Pension pension10 = new Pension(
            "9c685a9a-bc55-4a0f-9716-0920235adc7f",
            person10,
            "024",
            "84120965L12906",
            PensionType.Altersrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Aktiv,
            bankDetails10
        );
        _pensions.Add(pension10);
        
        // Beispiel 11
        Address address11 = new Address("Lindenallee", "8", "45678", "Testort", Country.Deutschland);
        Person person11 = new Person(Salutation.Frau, "", "Anna", "Müller", new DateTime(1952, 3, 25), address11);
        BankDetails bankDetails11 = new BankDetails("Deutsche Bank", "DE12345678901234567");
        Pension pension11 = new Pension(
            "6161f496-f1dc-4360-b04b-e20c477abc61",
            person11,
            "025",
            "35250352M59601",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails11
        );
        _pensions.Add(pension11);
        
        // Beispiel 12
        Address address12 = new Address("Birkenweg", "3", "12345", "Musterstadt", Country.Deutschland);
        Person person12 = new Person(Salutation.Herr, "", "Thomas", "Schmidt", new DateTime(1965, 8, 21), address12);
        BankDetails bankDetails12 = new BankDetails("Sparkasse", "DE87654321098765432");
        Pension pension12 = new Pension(
            "854b7bad-7eac-4ffd-8b9a-71bd1c07764b",
            person12,
            "012",
            "12210865S18515",
            PensionType.Hinterbliebenenrente,
            true,
            PayoutMethod.Barauszahlung,
            PensionStatus.Beendet,
            bankDetails12
        );
        _pensions.Add(pension12);
        
        // Beispiel 13
        Address address13 = new Address("Rosenstraße", "7", "56789", "Testort", Country.Deutschland);
        Person person13 = new Person(Salutation.Frau, "Prof.", "Sophie", "Huber", new DateTime(1968, 12, 7), address13);
        BankDetails bankDetails13 = new BankDetails("Commerzbank", "DE56789012345678901");
        Pension pension13 = new Pension(
            "30a8a455-b8d4-4ebf-ae8f-8d1505f9f0bf",
            person13,
            "026",
            "80071268H89403",
            PensionType.Betriebsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails13
        );
        _pensions.Add(pension13);
        
        // Beispiel 14
        Address address14 = new Address("Ahornweg", "12", "98765", "Musterstadt", Country.Deutschland);
        Person person14 = new Person(Salutation.Frau, "Dr.", "Laura", "Müller", new DateTime(1980, 5, 15), address14);
        BankDetails bankDetails14 = new BankDetails("Volksbank", "DE12345678901234567");
        Pension pension14 = new Pension(
            "4e9b5329-5624-4751-98e9-5a8cfd706bbf",
            person14,
            "010",
            "83150580M81572",
            PensionType.Witwenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails14
        );
        _pensions.Add(pension14);
        
        // Beispiel 15
        Address address15 = new Address("Lindenstraße", "9", "54321", "Testort", Country.Deutschland);
        Person person15 = new Person(Salutation.Herr, "", "Markus", "Wagner", new DateTime(1972, 10, 2), address15);
        BankDetails bankDetails15 = new BankDetails("Deutsche Bank", "DE23456789012345678");
        Pension pension15 = new Pension(
            "5d29f5ba-a7cc-4186-9f5c-79dd41ee501a",
            person15,
            "013",
            "45021072W22036",
            PensionType.Waisenrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Aktiv,
            bankDetails15
        );
        _pensions.Add(pension15);
        
        // Beispiel 16
        Address address16 = new Address("Rosenweg", "7", "12345", "Musterstadt", Country.Deutschland);
        Person person16 = new Person(Salutation.Herr, "", "Thomas", "Schmidt", new DateTime(1965, 8, 22), address16);
        BankDetails bankDetails16 = new BankDetails("Commerzbank", "DE34567890123456789");
        Pension pension16 = new Pension(
            "b2c444d9-741d-49db-a749-d2397ae39ed9",
            person16,
            "025",
            "12220865S48377",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Barauszahlung,
            PensionStatus.Beendet,
            bankDetails16
        );
        _pensions.Add(pension16);
        
        // Beispiel 17
        Address address17 = new Address("Birkenallee", "3", "98765", "Teststadt", Country.Deutschland);
        Person person17 = new Person(Salutation.Frau, "", "Sandra", "Hofmann", new DateTime(1978, 11, 10), address17);
        BankDetails bankDetails17 = new BankDetails("Sparkasse", "DE45678901234567890");
        Pension pension17 = new Pension(
            "511bd27b-bc4a-4e03-89ca-18db600324af",
            person17,
            "018",
            "31101178H97027",
            PensionType.Betriebsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails17
        );
        _pensions.Add(pension17);
        
        // Beispiel 18
        Address address18 = new Address("Ahornstraße", "15", "54321", "Musterstadt", Country.Deutschland);
        Person person18 = new Person(Salutation.Frau, "", "Melanie", "Koch", new DateTime(1920, 5, 12), address18);
        BankDetails bankDetails18 = new BankDetails("Volksbank", "DE56789012345678901");
        Pension pension18 = new Pension(
            "8286804a-15d9-4baa-acea-681530f054e4",
            person18,
            "024",
            "73120520K76144",
            PensionType.Altersrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails18
        );
        _pensions.Add(pension18);
        
        // Beispiel 19
        Address address19 = new Address("Lindenstraße", "10", "67890", "Teststadt", Country.Deutschland);
        Person person19 = new Person(Salutation.Herr, "", "Michael", "Wagner", new DateTime(1972, 9, 7), address19);
        BankDetails bankDetails19 = new BankDetails("Deutsche Bank", "DE67890123456789012");
        Pension pension19 = new Pension(
            "d7050a06-a98b-4d0b-af57-81ebbd706e07",
            person19,
            "010",
            "47070972W05371",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails19
        );
        _pensions.Add(pension19);
        
        // Beispiel 20
        Address address20 = new Address("Birkenweg", "7", "12345", "Musterstadt", Country.Deutschland);
        Person person20 = new Person(Salutation.Frau, "Dr.", "Laura", "Müller", new DateTime(1990, 3, 25), address20);
        BankDetails bankDetails20 = new BankDetails("Commerzbank", "DE12345678901234567");
        Pension pension20 = new Pension(
            "46773d86-357e-44f6-92fd-1bd2cb84680a",
            person20,
            "025",
            "07250390M71418",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Barauszahlung,
            PensionStatus.Beendet,
            bankDetails20
        );
        _pensions.Add(pension20);
        
        // Beispiel 21
        Address address21 = new Address("Rosenweg", "12", "98765", "Testort", Country.Deutschland);
        Person person21 = new Person(Salutation.Herr, "", "Markus", "Schneider", new DateTime(1985, 8, 15), address21);
        BankDetails bankDetails21 = new BankDetails("Sparkasse", "DE87654321098765432");
        Pension pension21 = new Pension(
            "0754968b-89a5-41a3-878d-5f54f8aeadcc",
            person21,
            "037",
            "65150885S34489",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails21
        );
        _pensions.Add(pension21);
        
        // Beispiel 22
        Address address22 = new Address("Eichenstraße", "5", "54321", "Musterstadt", Country.Deutschland);
        Person person22 = new Person(Salutation.Frau, "", "Julia", "Fischer", new DateTime(1978, 11, 10), address22);
        BankDetails bankDetails22 = new BankDetails("Raiffeisenbank", "DE54321098765432109");
        Pension pension22 = new Pension(
            "58dd6935-1dd6-4440-bddc-385fc592b779",
            person22,
            "028",
            "25101178F97861",
            PensionType.Waisenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails22
        );
        _pensions.Add(pension22);
        
        // Beispiel 23
        Address address23 = new Address("Ahornallee", "3", "67890", "Musterstadt", Country.Deutschland);
        Person person23 = new Person(Salutation.Frau, "", "Sabine", "Meier", new DateTime(1932, 6, 8), address23);
        BankDetails bankDetails23 = new BankDetails("Volksbank", "DE98765432109876543");
        Pension pension23 = new Pension(
            "73bc68cf-0bc9-409d-9fc6-9985ae6f4281",
            person23,
            "012",
            "32080632M63264",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails23
        );
        _pensions.Add(pension23);
        
        // Beispiel 24
        Address address24 = new Address("Lindenstraße", "10", "13579", "Testort", Country.Deutschland);
        Person person24 = new Person(Salutation.Herr, "", "Thomas", "Schulz", new DateTime(1975, 4, 12), address24);
        BankDetails bankDetails24 = new BankDetails("Deutsche Bank", "DE76543210987654321");
        Pension pension24 = new Pension(
            "e8cb6b9d-d820-400c-97f8-2ad66092a39f",
            person24,
            "015",
            "26120475S39667",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Barauszahlung,
            PensionStatus.Beendet,
            bankDetails24
        );
        _pensions.Add(pension24);
        
        // Beispiel 25
        Address address25 = new Address("Kastanienweg", "8", "54321", "Musterstadt", Country.Deutschland);
        Person person25 = new Person(Salutation.Herr, "", "Michael", "Hoffmann", new DateTime(1962, 9, 20), address25);
        BankDetails bankDetails25 = new BankDetails("Postbank", "DE01234567890123456");
        Pension pension25 = new Pension(
            "cf364fc0-1af9-4f1c-97c2-99b7abd8f052",
            person25,
            "021",
            "51200962H42457",
            PensionType.Betriebsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails25
        );
        _pensions.Add(pension25);
        
        // Beispiel 26
        Address address26 = new Address("Birkenweg", "5", "12345", "Musterstadt", Country.Deutschland);
        Person person26 = new Person(Salutation.Frau, "Dr.", "Anna", "Schneider", new DateTime(1978, 11, 15), address26);
        BankDetails bankDetails26 = new BankDetails("Commerzbank", "DE87654321098765432");
        Pension pension26 = new Pension(
            "1fee82ff-3e2e-4d94-bb76-4804ebfb5347",
            person26,
            "025",
            "31151178S60042",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails26
        );
        _pensions.Add(pension26);
        
        // Beispiel 27
        Address address27 = new Address("Eichenweg", "12", "98765", "Testort", Country.Deutschland);
        Person person27 = new Person(Salutation.Herr, "", "Markus", "Becker", new DateTime(1985, 7, 3), address27);
        BankDetails bankDetails27 = new BankDetails("Sparkasse", "DE23456789012345678");
        Pension pension27 = new Pension(
            "56b6dbf5-9f98-42b5-b949-26094a61ee83",
            person27,
            "037",
            "26030785B29921",
            PensionType.Hinterbliebenenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails27
        );
        _pensions.Add(pension27);
        
        // Beispiel 28
        Address address28 = new Address("Lindenstraße", "7", "54321", "Musterstadt", Country.Deutschland);
        Person person28 = new Person(Salutation.Herr, "Prof. Dr.", "Max", "Mustermann", new DateTime(1950, 5, 20), address28);
        BankDetails bankDetails28 = new BankDetails("Volksbank", "DE12345678901234567");
        Pension pension28 = new Pension(
            "fd8d78d6-e0b6-4122-adae-14f31cfeecf7",
            person28,
            "018",
            "48200550M09175",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails28
        );
        _pensions.Add(pension28);
        
        // Beispiel 29
        Address address29 = new Address("Feldstraße", "20", "87654", "Testort", Country.Deutschland);
        Person person29 = new Person(Salutation.Frau, "Dr.", "Sophie", "Müller", new DateTime(1982, 9, 10), address29);
        BankDetails bankDetails29 = new BankDetails("Deutsche Bank", "DE34567890123456789");
        Pension pension29 = new Pension(
            "119576da-f292-4f1f-b9df-694306fc3167",
            person29,
            "026",
            "82100982M92807",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails29
        );
        _pensions.Add(pension29);
        
        // Beispiel 30
        Address address30 = new Address("Ahornweg", "12", "98765", "Musterstadt", Country.Deutschland);
        Person person30 = new Person(Salutation.Divers, "", "Alex", "Schulz", new DateTime(1995, 8, 15), address30);
        BankDetails bankDetails30 = new BankDetails("Sparkasse", "DE56789012345678901");
        Pension pension30 = new Pension(
            "0a129d88-936d-41b7-a9b1-d83703abc190",
            person30,
            "012",
            "51150895S68199",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails30
        );
        _pensions.Add(pension30);
        
        // Beispiel 31
        Address address31 = new Address("Rosenweg", "5", "76543", "Testort", Country.Deutschland);
        Person person31 = new Person(Salutation.Frau, "", "Laura", "Hoffmann", new DateTime(1988, 4, 25), address31);
        BankDetails bankDetails31 = new BankDetails("Commerzbank", "DE45678901234567890");
        Pension pension31 = new Pension(
            "365f9b15-9dd4-418c-9a2b-a86726aa665f",
            person31,
            "021",
            "97250488H79546",
            PensionType.Waisenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Beendet,
            bankDetails31
        );
        _pensions.Add(pension31);
        
        // Beispiel 32
        Address address32 = new Address("Buchenweg", "9", "54321", "Musterstadt", Country.Deutschland);
        Person person32 = new Person(Salutation.Herr, "Dr.", "Maximilian", "Schmidt", new DateTime(1977, 12, 10), address32);
        BankDetails bankDetails32 = new BankDetails("Volksbank", "DE09876543210987654");
        Pension pension32 = new Pension(
            "62f37281-cc22-46be-9102-463e09d814da",
            person32,
            "025",
            "55101277S45122",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails32
        );
        _pensions.Add(pension32);
        
        // Beispiel 33
        Address address33 = new Address("Sonnenallee", "21", "12345", "Teststadt", Country.Deutschland);
        Person person33 = new Person(Salutation.Frau, "Prof.", "Isabella", "Müller", new DateTime(1985, 6, 28), address33);
        BankDetails bankDetails33 = new BankDetails("Deutsche Bank", "DE98765432109876543");
        Pension pension33 = new Pension(
            "e69a510c-9d4e-45e9-955b-3cadcc84b9b3",
            person33,
            "012",
            "32280685M93281",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails33
        );
        _pensions.Add(pension33);
        
        // Beispiel 34
        Address address34 = new Address("Hauptstraße", "7", "12345", "Musterstadt", Country.Deutschland);
        Person person34 = new Person(Salutation.Herr, "Prof.", "Julian", "Wagner", new DateTime(1972, 9, 15), address34);
        BankDetails bankDetails34 = new BankDetails("Commerzbank", "DE87654321098765432");
        Pension pension34 = new Pension(
            "433e7a87-42d8-4d35-814b-a2f5f982f913",
            person34,
            "025",
            "48150972W34742",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails34
        );
        _pensions.Add(pension34);
        
        // Beispiel 35
        Address address35 = new Address("Rosenstraße", "12", "54321", "Teststadt", Country.Deutschland);
        Person person35 = new Person(Salutation.Divers, "", "Alex", "Müller", new DateTime(1990, 4, 3), address35);
        BankDetails bankDetails35 = new BankDetails("Sparkasse", "DE76543210987654321");
        Pension pension35 = new Pension(
            "1366ddfb-fd38-4630-8dfe-bf469884b021",
            person35,
            "015",
            "10030490M86783",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails35
        );
        _pensions.Add(pension35);
        
        // Beispiel 36
        Address address36 = new Address("Am Seeufer", "42", "98765", "Musterstadt", Country.Deutschland);
        Person person36 = new Person(Salutation.Frau, "Dr.", "Sophie", "Schneider", new DateTime(1945, 6, 28), address36);
        BankDetails bankDetails36 = new BankDetails("Deutsche Bank", "DE12345678901234567");
        Pension pension36 = new Pension(
            "11327b71-2d5d-41a3-846c-9e2661a07ca8",
            person36,
            "018",
            "59280645S60890",
            PensionType.Altersrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails36
        );
        _pensions.Add(pension36);
        
        // Beispiel 37
        Address address37 = new Address("Lindenallee", "30", "54321", "Teststadt", Country.Deutschland);
        Person person37 = new Person(Salutation.Divers, "", "Max", "Müller", new DateTime(1992, 9, 10), address37);
        BankDetails bankDetails37 = new BankDetails("Volksbank", "DE98765432109876543");
        Pension pension37 = new Pension(
            "8ef7e4bb-403c-4e1a-83ed-5a2126e1a56f",
            person37,
            "024",
            "32100992M67756",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Barauszahlung,
            PensionStatus.Aktiv,
            bankDetails37
        );
        _pensions.Add(pension37);
        
        // Beispiel 38
        Address address38 = new Address("Hauptstraße", "15", "12345", "Musterstadt", Country.Deutschland);
        Person person38 = new Person(Salutation.Frau, "Prof. Dr.", "Anna", "Schmidt", new DateTime(1978, 3, 15), address38);
        BankDetails bankDetails38 = new BankDetails("Commerzbank", "DE87654321098765432");
        Pension pension38 = new Pension(
            "c47cf28a-37be-4f90-ac3c-2be4cba728bf",
            person38,
            "016",
            "34150378S97550",
            PensionType.Witwenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Beendet,
            bankDetails38
        );
        _pensions.Add(pension38);
        
        // Beispiel 39
        Address address39 = new Address("Rosenweg", "8", "56789", "Teststadt", Country.Deutschland);
        Person person39 = new Person(Salutation.Herr, "Dr.", "Markus", "Lehmann", new DateTime(1982, 11, 20), address39);
        BankDetails bankDetails39 = new BankDetails("Sparkasse", "DE76543210987654321");
        Pension pension39 = new Pension(
            "7591b3d4-941a-4c3d-a18f-aea465a5c9f8",
            person39,
            "025",
            "05201182L35411",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails39
        );
        _pensions.Add(pension39);
        
        // Beispiel 40
        Address address40 = new Address("Am Seeufer", "7", "98765", "Musterstadt", Country.Deutschland);
        Person person40 = new Person(Salutation.Frau, "Prof.", "Julia", "Müller", new DateTime(1975, 8, 10), address40);
        BankDetails bankDetails40 = new BankDetails("Volksbank", "DE12345678901234567");
        Pension pension40 = new Pension(
            "8fe93bc9-89ff-44b0-a9b3-c8cd60b13b68",
            person40,
            "037",
            "75100875M84254",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails40
        );
        _pensions.Add(pension40);
        
        // Beispiel 41
        Address address41 = new Address("Schlossallee", "12", "54321", "Teststadt", Country.Deutschland);
        Person person41 = new Person(Salutation.Herr, "Dr.", "Sebastian", "Kaiser", new DateTime(1934, 5, 22), address41);
        BankDetails bankDetails41 = new BankDetails("Deutsche Bank", "DE98765432109876543");
        Pension pension41 = new Pension(
            "c98ece56-a1f1-4f1f-9920-0ae595eda7ac",
            person41,
            "050",
            "85220534K28247",
            PensionType.Altersrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails41
        );
        _pensions.Add(pension41);
        
        // Beispiel 42
        Address address42 = new Address("Hauptstraße", "15", "12345", "Musterstadt", Country.Deutschland);
        Person person42 = new Person(Salutation.Divers, "Dr.", "Alex", "Schmidt", new DateTime(1990, 3, 17), address42);
        BankDetails bankDetails42 = new BankDetails("Commerzbank", "DE87654321098765432");
        Pension pension42 = new Pension(
            "acc40ac8-a69d-4db1-89c1-66ff353f7227",
            person42,
            "702",
            "15170390S51235",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails42
        );
        _pensions.Add(pension42);
        
        // Beispiel 43
        Address address43 = new Address("Rosenweg", "3", "56789", "Teststadt", Country.Deutschland);
        Person person43 = new Person(Salutation.Herr, "Prof.", "Maximilian", "Hofmann", new DateTime(1982, 12, 5), address43);
        BankDetails bankDetails43 = new BankDetails("Sparkasse", "DE65432109876543210");
        Pension pension43 = new Pension(
            "e96239c4-41ac-4b59-9e2a-ad61d028e2fa",
            person43,
            "024",
            "14051282H49877",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Beendet,
            bankDetails43
        );
        _pensions.Add(pension43);
        
        // Beispiel 44
        Address address44 = new Address("Birkenallee", "7", "98765", "Musterstadt", Country.Deutschland);
        Person person44 = new Person(Salutation.Frau, "Prof.", "Sophie", "Müller", new DateTime(1955, 9, 21), address44);
        BankDetails bankDetails44 = new BankDetails("Volksbank", "DE12345678901234567");
        Pension pension44 = new Pension(
            "3fd3b97f-618d-46d1-98f8-033911920678",
            person44,
            "025",
            "48210955M50261",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails44
        );
        _pensions.Add(pension44);
        
        // Beispiel 45
        Address address45 = new Address("Ahornstraße", "10", "54321", "Teststadt", Country.Deutschland);
        Person person45 = new Person(Salutation.Divers, "Dr.", "Sam", "Wagner", new DateTime(1992, 6, 12), address45);
        BankDetails bankDetails45 = new BankDetails("Deutsche Bank", "DE98765432109876543");
        Pension pension45 = new Pension(
            "10c0ebdd-243c-4be6-b4d9-4f3e78dccda7",
            person45,
            "026",
            "24120692W69843",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails45
        );
        _pensions.Add(pension45);
        
        // Beispiel 46
        Address address46 = new Address("Lindenstraße", "12", "12345", "Musterort", Country.Deutschland);
        Person person46 = new Person(Salutation.Herr, "Dr.", "Max", "Schmidt", new DateTime(1980, 4, 3), address46);
        BankDetails bankDetails46 = new BankDetails("Sparkasse", "DE87654321098765432");
        Pension pension46 = new Pension(
            "f78fdfbd-379b-4b45-b36c-340d150652be",
            person46,
            "011",
            "53030480S11167",
            PensionType.Witwenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails46
        );
        _pensions.Add(pension46);
        
        // Beispiel 47
        Address address47 = new Address("Rosenweg", "5", "56789", "Testort", Country.Deutschland);
        Person person47 = new Person(Salutation.Divers, "Prof.", "Alex", "Schneider", new DateTime(1995, 11, 15), address47);
        BankDetails bankDetails47 = new BankDetails("Commerzbank", "DE65432109876543210");
        Pension pension47 = new Pension(
            "db248f34-96a5-4e33-a129-2f743bbdd204",
            person47,
            "018",
            "18151195S95713",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Beendet,
            bankDetails47
        );
        _pensions.Add(pension47);
        
        // Beispiel 48
        Address address48 = new Address("Birkenweg", "8", "98765", "Musterstadt", Country.Deutschland);
        Person person48 = new Person(Salutation.Frau, "Prof.", "Anna", "Müller", new DateTime(1975, 9, 12), address48);
        BankDetails bankDetails48 = new BankDetails("Volksbank", "DE12345678901234567");
        Pension pension48 = new Pension(
            "9748be8c-b096-409b-8996-52e2fe2fbfc5",
            person48,
            "025",
            "90120975M69005",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails48
        );
        _pensions.Add(pension48);
        
        // Beispiel 49
        Address address49 = new Address("Ahornstraße", "20", "54321", "Teststadt", Country.Deutschland);
        Person person49 = new Person(Salutation.Divers, "Dr.", "Max", "Schulz", new DateTime(1948, 6, 25), address49);
        BankDetails bankDetails49 = new BankDetails("Deutsche Bank", "DE98765432101234567");
        Pension pension49 = new Pension(
            "ca3d34d2-a740-41be-8774-93bfd51f22b1",
            person49,
            "037",
            "49250648S56504",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails49
        );
        _pensions.Add(pension49);
        
        // Beispiel 50
        Address address50 = new Address("Rosenstraße", "15", "12345", "Musterstadt", Country.Deutschland);
        Person person50 = new Person(Salutation.Herr, "Prof.", "Hans", "Schmidt", new DateTime(1965, 3, 18), address50);
        BankDetails bankDetails50 = new BankDetails("Sparkasse", "DE87654321098765432");
        Pension pension50 = new Pension(
            "a7e830ec-0a62-4d8b-bf0f-79d062413b17",
            person50,
            "015",
            "36180365S02856",
            PensionType.Witwenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails50
        );
        _pensions.Add(pension50);
        
        // Beispiel 51
        Address address51 = new Address("Lindenweg", "7", "56789", "Teststadt", Country.Deutschland);
        Person person51 = new Person(Salutation.Frau, "Dr.", "Julia", "Weber", new DateTime(1990, 10, 5), address51);
        BankDetails bankDetails51 = new BankDetails("Commerzbank", "DE54321098765432109");
        Pension pension51 = new Pension(
            "4dc89a8c-9d39-494d-a5aa-31bc977c98c9",
            person51,
            "018",
            "62051090W85096",
            PensionType.Waisenrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Beendet,
            bankDetails51
        );
        _pensions.Add(pension51);
        
        // Beispiel 52
        Address address52 = new Address("Bergstraße", "3", "98765", "Musterdorf", Country.Deutschland);
        Person person52 = new Person(Salutation.Frau, "Prof.", "Anna", "Müller", new DateTime(1938, 7, 12), address52);
        BankDetails bankDetails52 = new BankDetails("Volksbank", "DE12345678901234567");
        Pension pension52 = new Pension(
            "ab0bcbc8-dbe4-4b7b-84ee-313e4d6d2a7f",
            person52,
            "021",
            "79120738M74973",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails52
        );
        _pensions.Add(pension52);
        
        // Beispiel 53
        Address address53 = new Address("Am Markt", "10", "54321", "Testdorf", Country.Deutschland);
        Person person53 = new Person(Salutation.Divers, "Prof.", "Alex", "Schneider", new DateTime(1995, 9, 25), address53);
        BankDetails bankDetails53 = new BankDetails("Deutsche Bank", "DE98765432109876543");
        Pension pension53 = new Pension(
            "883b5a39-7fdf-40cd-bb94-611cd49c8e96",
            person53,
            "025",
            "75250995S67047",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails53
        );
        _pensions.Add(pension53);
        
        // Beispiel 54
        Address address54 = new Address("Hauptstraße", "7", "12345", "Musterstadt", Country.Deutschland);
        Person person54 = new Person(Salutation.Frau, "Dr.", "Sophie", "Schulz", new DateTime(1985, 4, 28), address54);
        BankDetails bankDetails54 = new BankDetails("Sparkasse", "DE87654321098765432");
        Pension pension54 = new Pension(
            "93d36367-940a-46b1-a0e3-9d1de6e44f8e",
            person54,
            "037",
            "07280485S57513",
            PensionType.Witwenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails54
        );
        _pensions.Add(pension54);
        
        // Beispiel 55
        Address address55 = new Address("Parkweg", "15", "56789", "Teststadt", Country.Deutschland);
        Person person55 = new Person(Salutation.Divers, "Prof.", "Max", "Hoffmann", new DateTime(1972, 12, 10), address55);
        BankDetails bankDetails55 = new BankDetails("Commerzbank", "DE54321098765432109");
        Pension pension55 = new Pension(
            "cc7f0367-953a-4bb4-8bca-b8ad9c143eba",
            person55,
            "024",
            "72101272H57038",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails55
        );
        _pensions.Add(pension55);
        
        // Beispiel 56
        Address address56 = new Address("Am Markt", "3", "98765", "Musterstadt", Country.Deutschland);
        Person person56 = new Person(Salutation.Frau, "Dr.", "Laura", "Müller", new DateTime(1990, 8, 15), address56);
        BankDetails bankDetails56 = new BankDetails("Volksbank", "DE12345678901234567");
        Pension pension56 = new Pension(
            "4b11ff0d-f4a2-48a4-8694-d4ffe659c5ac",
            person56,
            "025",
            "15150890M88245",
            PensionType.Hinterbliebenenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails56
        );
        _pensions.Add(pension56);
        
        // Beispiel 57
        Address address57 = new Address("Kirchstraße", "10", "54321", "Teststadt", Country.Deutschland);
        Person person57 = new Person(Salutation.Herr, "Prof.", "Alexander", "Schmidt", new DateTime(1983, 5, 20), address57);
        BankDetails bankDetails57 = new BankDetails("Deutsche Bank", "DE98765432109876543");
        Pension pension57 = new Pension(
            "60676937-0970-4759-8c45-0197a7c46177",
            person57,
            "015",
            "46200583S35592",
            PensionType.Witwenrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails57
        );
        _pensions.Add(pension57);
        
        // Beispiel 58
        Address address58 = new Address("Hauptstraße", "5", "12345", "Musterstadt", Country.Deutschland);
        Person person58 = new Person(Salutation.Frau, "Prof.", "Sophie", "Wagner", new DateTime(1975, 10, 25), address58);
        BankDetails bankDetails58 = new BankDetails("Sparkasse", "DE87654321098765432");
        Pension pension58 = new Pension(
            "8d7b6396-de57-4193-8400-cc8568fbca2e",
            person58,
            "024",
            "97251075W52733",
            PensionType.Betriebsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Beendet,
            bankDetails58
        );
        _pensions.Add(pension58);
        
        // Beispiel 59
        Address address59 = new Address("Schlossallee", "1", "54321", "Teststadt", Country.Deutschland);
        Person person59 = new Person(Salutation.Divers, "", "Alex", "Müller", new DateTime(1992, 4, 12), address59);
        BankDetails bankDetails59 = new BankDetails("Commerzbank", "DE56789012345678901");
        Pension pension59 = new Pension(
            "1a9bd379-7d5e-4c95-96e5-7218d9923f5a",
            person59,
            "010",
            "94120492M83097",
            PensionType.Waisenrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails59
        );
        _pensions.Add(pension59);
        
        // Beispiel 60
        Address address60 = new Address("Musterweg", "10", "98765", "Musterstadt", Country.Deutschland);
        Person person60 = new Person(Salutation.Frau, "Dr.", "Laura", "Schulz", new DateTime(1980, 6, 15), address60);
        BankDetails bankDetails60 = new BankDetails("Volksbank", "DE10987654321098765");
        Pension pension60 = new Pension(
            "83cfa99a-663c-42a9-a48d-e508d0a0175e",
            person60,
            "021",
            "78150680S74652",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails60
        );
        _pensions.Add(pension60);
        
        // Beispiel 61
        Address address61 = new Address("Am Markt", "3", "54321", "Stadtberg", Country.Deutschland);
        Person person61 = new Person(Salutation.Herr, "", "Markus", "Hoffmann", new DateTime(1978, 9, 8), address61);
        BankDetails bankDetails61 = new BankDetails("Deutsche Bank", "DE01234567890123456");
        Pension pension61 = new Pension(
            "e2985979-90fb-428b-ad3e-f06abdfcd559",
            person61,
            "025",
            "88080978H36849",
            PensionType.Witwenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails61
        );
        _pensions.Add(pension61);
        
        // Beispiel 62
        Address address62 = new Address("Hauptstraße", "7", "12345", "Musterhausen", Country.Deutschland);
        Person person62 = new Person(Salutation.Divers, "", "Max", "Müller", new DateTime(1995, 4, 20), address62);
        BankDetails bankDetails62 = new BankDetails("Sparkasse", "DE98765432109876543");
        Pension pension62 = new Pension(
            "ea44bbbf-b93e-40d3-b42e-d404559a7cd1",
            person62,
            "011",
            "75200495M73130",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails62
        );
        _pensions.Add(pension62);
        
        // Beispiel 63
        Address address63 = new Address("Schillerstraße", "15", "87654", "Musterstadt", Country.Deutschland);
        Person person63 = new Person(Salutation.Frau, "", "Sophie", "Fischer", new DateTime(1988, 12, 10), address63);
        BankDetails bankDetails63 = new BankDetails("Commerzbank", "DE87654321098765432");
        Pension pension63 = new Pension(
            "3010345e-57be-4f9d-95a2-fceeec24389b",
            person63,
            "018",
            "44101288F61926",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails63
        );
        _pensions.Add(pension63);
        
        // Beispiel 64
        Address address64 = new Address("Rosenweg", "3", "54321", "Blumenstadt", Country.Deutschland);
        Person person64 = new Person(Salutation.Frau, "", "Anna", "Schmidt", new DateTime(1976, 9, 15), address64);
        BankDetails bankDetails64 = new BankDetails("Volksbank", "DE54321098765432109");
        Pension pension64 = new Pension(
            "19928d35-5d19-4ec1-8923-2bd0d6d72bee",
            person64,
            "025",
            "73150976S98837",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Beendet,
            bankDetails64
        );
        _pensions.Add(pension64);
        
        // Beispiel 65
        Address address65 = new Address("Bergstraße", "10", "98765", "Gipfelstadt", Country.Deutschland);
        Person person65 = new Person(Salutation.Divers, "", "Alex", "Schulz", new DateTime(1990, 6, 8), address65);
        BankDetails bankDetails65 = new BankDetails("Deutsche Bank", "DE10987654321098765");
        Pension pension65 = new Pension(
            "fcc2eded-7841-4cac-99a8-519faf6f5507",
            person65,
            "021",
            "48080690S88441",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails65
        );
        _pensions.Add(pension65);
        
        // Beispiel 66
        Address address66 = new Address("Sonnenallee", "45", "12345", "Sonnendorf", Country.Deutschland);
        Person person66 = new Person(Salutation.Herr, "Dr.", "Max", "Müller", new DateTime(1925, 3, 22), address66);
        BankDetails bankDetails66 = new BankDetails("Sparkasse", "DE12345678901234567");
        Pension pension66 = new Pension(
            "2c1122a5-dc0c-41f2-a256-eb5695682db1",
            person66,
            "018",
            "99220325M41744",
            PensionType.Altersrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Aktiv,
            bankDetails66
        );
        _pensions.Add(pension66);
        
        // Beispiel 67
        Address address67 = new Address("Hauptstraße", "7", "54321", "Stadtmitte", Country.Deutschland);
        Person person67 = new Person(Salutation.Frau, "", "Laura", "Lehmann", new DateTime(1978, 11, 10), address67);
        BankDetails bankDetails67 = new BankDetails("Commerzbank", "DE54321012345678901");
        Pension pension67 = new Pension(
            "6db7e8b1-a319-4476-9846-4684c0382a4d",
            person67,
            "011",
            "59101178L98216",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails67
        );
        _pensions.Add(pension67);
        
        // Beispiel 68
        Address address68 = new Address("Am Markt", "12", "98765", "Stadtstadt", Country.Deutschland);
        Person person68 = new Person(Salutation.Divers, "Prof.", "Alex", "Schmidt", new DateTime(1990, 7, 15), address68);
        BankDetails bankDetails68 = new BankDetails("Volksbank", "DE98765432101234567");
        Pension pension68 = new Pension(
            "4611220a-4484-4aed-bd82-ca402b3b6c58",
            person68,
            "013",
            "46150790S57555",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Beendet,
            bankDetails68
        );
        _pensions.Add(pension68);
        
        // Beispiel 69
        Address address69 = new Address("Bergstraße", "9", "87654", "Bergdorf", Country.Deutschland);
        Person person69 = new Person(Salutation.Herr, "", "Finn", "Wagner", new DateTime(1982, 5, 8), address69);
        BankDetails bankDetails69 = new BankDetails("Deutsche Bank", "DE87654321098765432");
        Pension pension69 = new Pension(
            "b4c9cbdd-e53a-41b3-af53-dab35a07f339",
            person69,
            "024",
            "03080582W20679",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails69
        );
        _pensions.Add(pension69);
        
        // Beispiel 70
        Address address70 = new Address("Hauptstraße", "7", "12345", "Stadtstadt", Country.Deutschland);
        Person person70 = new Person(Salutation.Frau, "", "Laura", "Müller", new DateTime(1935, 9, 22), address70);
        BankDetails bankDetails70 = new BankDetails("Sparkasse", "DE12345678901234567");
        Pension pension70 = new Pension(
            "cff32674-3477-499f-9580-d069ab6f17a9",
            person70,
            "037",
            "21220935M82921",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails70
        );
        _pensions.Add(pension70);
        
        // Beispiel 71
        Address address71 = new Address("Bahnhofstraße", "15", "54321", "Stadtstadt", Country.Deutschland);
        Person person71 = new Person(Salutation.Herr, "", "Max", "Schulz", new DateTime(1988, 4, 10), address71);
        BankDetails bankDetails71 = new BankDetails("Commerzbank", "DE54321098765432109");
        Pension pension71 = new Pension(
            "237d0fff-ab0b-4d8f-a853-d2cadec2a10e",
            person71,
            "028",
            "23100488S05792",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails71
        );
        _pensions.Add(pension71);
        
        // Beispiel 72
        Address address72 = new Address("Kirchstraße", "10", "98765", "Stadtstadt", Country.Deutschland);
        Person person72 = new Person(Salutation.Frau, "", "Sophie", "Wagner", new DateTime(1963, 11, 17), address72);
        BankDetails bankDetails72 = new BankDetails("Volksbank", "DE98765432109876543");
        Pension pension72 = new Pension(
            "0d68885c-a50b-44ac-97fb-b6137482f352",
            person72,
            "025",
            "83171163W50207",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails72
        );
        _pensions.Add(pension72);
        
        // Beispiel 73
        Address address73 = new Address("Rosenweg", "3", "23456", "Stadtstadt", Country.Deutschland);
        Person person73 = new Person(Salutation.Divers, "", "Robin", "Meier", new DateTime(1990, 8, 5), address73);
        BankDetails bankDetails73 = new BankDetails("Deutsche Bank", "DE23456789012345678");
        Pension pension73 = new Pension(
            "263edfde-9672-4256-be83-42c0584c26bf",
            person73,
            "018",
            "23050890M97990",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails73
        );
        _pensions.Add(pension73);
        
        // Beispiel 74
        Address address74 = new Address("Hauptstraße", "7", "34567", "Stadtstadt", Country.Deutschland);
        Person person74 = new Person(Salutation.Frau, "", "Hannah", "Müller", new DateTime(1985, 6, 12), address74);
        BankDetails bankDetails74 = new BankDetails("Sparkasse", "DE34567890123456789");
        Pension pension74 = new Pension(
            "bfef21ff-0cb6-494b-8abd-6717d6b017e0",
            person74,
            "030",
            "63120685M59584",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails74
        );
        _pensions.Add(pension74);
        
        // Beispiel 75
        Address address75 = new Address("Am Markt", "15", "45678", "Stadtstadt", Country.Deutschland);
        Person person75 = new Person(Salutation.Herr, "", "Max", "Schulz", new DateTime(1978, 3, 25), address75);
        BankDetails bankDetails75 = new BankDetails("Commerzbank", "DE45678901234567890");
        Pension pension75 = new Pension(
            "d11d5f28-7e4f-468b-9961-681e695ea8c8",
            person75,
            "040",
            "61250378S27380",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails75
        );
        _pensions.Add(pension75);
        
        // Beispiel 76
        Address address76 = new Address("Goethestraße", "12", "98765", "Berlin", Country.Deutschland);
        Person person76 = new Person(Salutation.Frau, "Marie", "Sophie", "Müller", new DateTime(1940, 7, 15), address76);
        BankDetails bankDetails76 = new BankDetails("Deutsche Bank", "DE98765432109876543");
        Pension pension76 = new Pension(
            "24b1b170-27f1-4b21-9452-1599b7b7474e",
            person76,
            "051",
            "75150740M66781",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails76
        );
        _pensions.Add(pension76);
        
        // Beispiel 77
        Address address77 = new Address("Rathausplatz", "5", "87654", "München", Country.Deutschland);
        Person person77 = new Person(Salutation.Herr, "", "Thomas", "Schneider", new DateTime(1982, 10, 5), address77);
        BankDetails bankDetails77 = new BankDetails("Postbank", "DE87654321098765432");
        Pension pension77 = new Pension(
            "163f012e-b21f-43c0-adc8-526df75d509b",
            person77,
            "040",
            "65051082S37442",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails77
        );
        _pensions.Add(pension77);
        
        // Beispiel 78
        Address address78 = new Address("Schlossallee", "1", "12345", "Musterstadt", Country.Deutschland);
        Person person78 = new Person(Salutation.Herr, "Dr.", "Alexander", "Schmidt", new DateTime(1975, 3, 21), address78);
        BankDetails bankDetails78 = new BankDetails("Commerzbank", "DE12345678901234567");
        Pension pension78 = new Pension(
            "3c5925bd-353d-44eb-b647-9a2dc6e17068",
            person78,
            "030",
            "86210375S39424",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails78
        );
        _pensions.Add(pension78);
        
        // Beispiel 79
        Address address79 = new Address("Am Markt", "7", "54321", "Stadtstadt", Country.Deutschland);
        Person person79 = new Person(Salutation.Frau, "Prof. Dr.", "Christine", "Wagner", new DateTime(1958, 6, 12), address79);
        BankDetails bankDetails79 = new BankDetails("Sparkasse", "DE76543210987654321");
        Pension pension79 = new Pension(
            "818efab2-6e5f-4feb-8056-9bdfbc6b6ea8",
            person79,
            "055",
            "71120658W71889",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails79
        );
        _pensions.Add(pension79);
        
        // Beispiel 80
        Address address80 = new Address("Hauptstraße", "10", "98765", "Musterstadt", Country.Deutschland);
        Person person80 = new Person(Salutation.Frau, "Mag.", "Julia", "Müller", new DateTime(1926, 9, 8), address80);
        BankDetails bankDetails80 = new BankDetails("Volksbank", "DE87654321098765432");
        Pension pension80 = new Pension(
            "2321e4c1-cf21-49ba-95f1-d8f4f437160f",
            person80,
            "040",
            "74080926M79013",
            PensionType.Altersrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails80
        );
        _pensions.Add(pension80);
        
        // Beispiel 81
        Address address81 = new Address("Marktplatz", "5", "87654", "Stadtstadt", Country.Deutschland);
        Person person81 = new Person(Salutation.Herr, "Prof.", "Maximilian", "Schulz", new DateTime(1978, 12, 15), address81);
        BankDetails bankDetails81 = new BankDetails("Deutsche Bank", "DE09876543210987654");
        Pension pension81 = new Pension(
            "df609f2c-95e0-4240-b04b-fb2724aeef24",
            person81,
            "050",
            "58151278S33561",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails81
        );
        _pensions.Add(pension81);
        
        // Beispiel 82
        Address address82 = new Address("Musterstraße", "15", "12345", "Musterstadt", Country.Deutschland);
        Person person82 = new Person(Salutation.Herr, "Dr.", "Karl", "Schneider", new DateTime(1950, 3, 12), address82);
        BankDetails bankDetails82 = new BankDetails("Commerzbank", "DE12345678901234567");
        Pension pension82 = new Pension(
            "80207681-f21e-4583-ac73-6b16f2629bb2",
            person82,
            "060",
            "75120350S33368",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails82
        );
        _pensions.Add(pension82);
        
        // Beispiel 83
        Address address83 = new Address("Am Park", "7", "54321", "Stadtstadt", Country.Deutschland);
        Person person83 = new Person(Salutation.Frau, "Mag.", "Elisabeth", "Weber", new DateTime(1942, 8, 25), address83);
        BankDetails bankDetails83 = new BankDetails("Sparkasse", "DE98765432109876543");
        Pension pension83 = new Pension(
            "12a2ad24-6587-4e62-b06f-312997109eca",
            person83,
            "070",
            "63250842W89694",
            PensionType.Hinterbliebenenrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Beendet,
            bankDetails83
        );
        _pensions.Add(pension83);
        
        // Beispiel 84
        Address address84 = new Address("Hauptstraße", "10", "98765", "Stadtberg", Country.Deutschland);
        Person person84 = new Person(Salutation.Frau, "Prof. Dr.", "Helga", "Müller", new DateTime(1956, 9, 8), address84);
        BankDetails bankDetails84 = new BankDetails("Deutsche Bank", "DE87654321098765432");
        Pension pension84 = new Pension(
            "5a4df095-8ae7-498b-b8df-28ee2d1b95fe",
            person84,
            "080",
            "50080956M56000",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails84
        );
        _pensions.Add(pension84);
        
        // Beispiel 85
        Address address85 = new Address("Marktplatz", "3", "65432", "Stadtstadt", Country.Deutschland);
        Person person85 = new Person(Salutation.Herr, "Prof.", "Johannes", "Wagner", new DateTime(1948, 12, 15), address85);
        BankDetails bankDetails85 = new BankDetails("Volksbank", "DE76543210987654321");
        Pension pension85 = new Pension(
            "bf290817-54be-4179-b952-18e3332d8ed6",
            person85,
            "090",
            "19151248W38094",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails85
        );
        _pensions.Add(pension85);
        
        // Beispiel 86
        Address address86 = new Address("Am Kirchplatz", "7", "30159", "Hannover", Country.Deutschland);
        Person person86 = new Person(Salutation.Frau, "Dr.", "Sophie", "Schneider", new DateTime(1975, 5, 20), address86);
        BankDetails bankDetails86 = new BankDetails("Commerzbank", "DE12345678901234567");
        Pension pension86 = new Pension(
            "bef82d68-c01c-49a4-980b-05a80963524b",
            person86,
            "091",
            "13200575S66181",
            PensionType.Erwerbsminderungsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails86
        );
        _pensions.Add(pension86);
        
        // Beispiel 87
        Address address87 = new Address("Rathausplatz", "2", "80331", "München", Country.Deutschland);
        Person person87 = new Person(Salutation.Herr, "Prof. Dr.", "Maximilian", "Mayer", new DateTime(1968, 8, 10), address87);
        BankDetails bankDetails87 = new BankDetails("Sparkasse", "DE98765432109876543");
        Pension pension87 = new Pension(
            "aff351d8-f7a0-4b66-b4af-ace5403ca72d",
            person87,
            "092",
            "80100868M35752",
            PensionType.Hinterbliebenenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails87
        );
        _pensions.Add(pension87);
        
        // Beispiel 88
        Address address88 = new Address("Hauptstraße", "15", "10178", "Berlin", Country.Deutschland);
        Person person88 = new Person(Salutation.Frau, "", "Martha", "Schmidt", new DateTime(1940, 3, 12), address88);
        BankDetails bankDetails88 = new BankDetails("Deutsche Bank", "DE87654321098765432");
        Pension pension88 = new Pension(
            "3b7f64b9-a48d-42f1-bc30-bed9b72039d5",
            person88,
            "093",
            "46120340S50265",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails88
        );
        _pensions.Add(pension88);
        
        // Beispiel 89
        Address address89 = new Address("Schlossallee", "10", "70173", "Stuttgart", Country.Deutschland);
        Person person89 = new Person(Salutation.Herr, "", "Karl", "Bauer", new DateTime(1955, 7, 8), address89);
        BankDetails bankDetails89 = new BankDetails("Kreissparkasse", "DE34567890123456789");
        Pension pension89 = new Pension(
            "df2b5318-43cb-4247-b7c7-bcfafb545c5b",
            person89,
            "094",
            "86080755B23821",
            PensionType.Erwerbsminderungsrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Beendet,
            bankDetails89
        );
        _pensions.Add(pension89);
        
        // Beispiel 90
        Address address90 = new Address("Baker Street", "221B", "NW1 6XE", "London", Country.Großbritannien);
        Person person90 = new Person(Salutation.Herr, "", "Arthur", "Conan Doyle", new DateTime(1859, 5, 22), address90);
        BankDetails bankDetails90 = new BankDetails("Barclays Bank", "GB12BARC34567890123456");
        Pension pension90 = new Pension(
            "47396c1a-ae70-4c66-a6c6-17319f37eb52",
            person90,
            "095",
            "85220559C03301",
            PensionType.Altersrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Aktiv,
            bankDetails90
        );
        _pensions.Add(pension90);
        
        // Beispiel 91
        Address address91 = new Address("Rue de Rivoli", "12", "75001", "Paris", Country.Frankreich);
        Person person91 = new Person(Salutation.Frau, "Madame", "Marie", "Curie", new DateTime(1927, 11, 7), address91);
        BankDetails bankDetails91 = new BankDetails("BNP Paribas", "FR1234567890123456789012345");
        Pension pension91 = new Pension(
            "88b7c54d-4132-40f3-bc33-44e6d02f6e4f",
            person91,
            "096",
            "32071127C76910",
            PensionType.Witwenrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails91
        );
        _pensions.Add(pension91);
        
        // Beispiel 92
        Address address92 = new Address("Broadway", "123", "10001", "New York", Country.Vereinigte_Staaten_von_Amerika);
        Person person92 = new Person(Salutation.Frau, "Ms.", "Audrey", "Hepburn", new DateTime(1929, 5, 4), address92);
        BankDetails bankDetails92 = new BankDetails("Bank of America", "US12345678901234567890");
        Pension pension92 = new Pension(
            "7969a878-e5ba-434c-b462-a46b02289bb4",
            person92,
            "097",
            "42040529H64052",
            PensionType.Altersrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails92
        );
        _pensions.Add(pension92);
        
        // Beispiel 93
        Address address93 = new Address("Piazza del Duomo", "1", "20121", "Mailand", Country.Italien);
        Person person93 = new Person(Salutation.Herr, "Signore", "Leonardo", "da Vinci", new DateTime(1952, 4, 15), address93);
        BankDetails bankDetails93 = new BankDetails("Banca Intesa Sanpaolo", "IT1234567890123456789012345");
        Pension pension93 = new Pension(
            "c3b5c62f-1e4f-4309-b6d7-2517123e504f",
            person93,
            "098",
            "50150452D31841",
            PensionType.Waisenrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Aktiv,
            bankDetails93
        );
        _pensions.Add(pension93);
        
        // Beispiel 94
        Address address94 = new Address("Champs-Élysées", "123", "75008", "Paris", Country.Frankreich);
        Person person94 = new Person(Salutation.Frau, "Madame", "Coco", "Chanel", new DateTime(1943, 8, 19), address94);
        BankDetails bankDetails94 = new BankDetails("Banque de France", "FR1234567890123456789012345");
        Pension pension94 = new Pension(
            "8c787406-d79a-459a-8c80-a26e0c29dcee",
            person94,
            "099",
            "90190843C68408",
            PensionType.Altersrente,
            false,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails94
        );
        _pensions.Add(pension94);
        
        // Beispiel 95
        Address address95 = new Address("Piazza San Marco", "1", "30124", "Venedig", Country.Italien);
        Person person95 = new Person(Salutation.Herr, "Signore", "Marco", "Polo", new DateTime(1954, 9, 15), address95);
        BankDetails bankDetails95 = new BankDetails("UniCredit Bank", "IT1234567890123456789012345");
        Pension pension95 = new Pension(
            "b7072fc9-f787-4e2a-9674-0e5b0b2f2e1e",
            person95,
            "100",
            "08150954P25495",
            PensionType.Altersrente,
            true,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Aktiv,
            bankDetails95
        );
        _pensions.Add(pension95);
        
        // Beispiel 96
        Address address96 = new Address("Times Square", "1", "10036", "New York", Country.Vereinigte_Staaten_von_Amerika);
        Person person96 = new Person(Salutation.Herr, "Mr.", "John", "Doe", new DateTime(1950, 3, 12), address96);
        BankDetails bankDetails96 = new BankDetails("Bank of America", "US1234567890123456789012345");
        Pension pension96 = new Pension(
            "2ca51db6-d351-4789-bca3-0c6cea12e70a",
            person96,
            "101",
            "36120350D13478",
            PensionType.Betriebsrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails96
        );
        _pensions.Add(pension96);
        
        // Beispiel 97
        Address address97 = new Address("Red Square", "1", "109012", "Moskau", Country.Russland);
        Person person97 = new Person(Salutation.Frau, "Frau", "Maria", "Iwanowa", new DateTime(1945, 7, 8), address97);
        BankDetails bankDetails97 = new BankDetails("Sberbank", "RU1234567890123456789012345");
        Pension pension97 = new Pension(
            "c3301b28-832d-41f9-89e7-e1677282aca2",
            person97,
            "102",
            "03080745I88750",
            PensionType.Altersrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Beendet,
            bankDetails97
        );
        _pensions.Add(pension97);
        
        // Beispiel 98
        Address address98 = new Address("Champs-Élysées", "10", "75008", "Paris", Country.Frankreich);
        Person person98 = new Person(Salutation.Herr, "Monsieur", "Jean", "Dupont", new DateTime(1940, 12, 25), address98);
        BankDetails bankDetails98 = new BankDetails("Banque de France", "FR1234567890123456789012345");
        Pension pension98 = new Pension(
            "269f1906-5c4a-4c4e-873d-33fc4bc8359c",
            person98,
            "103",
            "32251240D35788",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Ausgesetzt,
            bankDetails98
        );
        _pensions.Add(pension98);
        
        // Beispiel 99
        Address address99 = new Address("Piazza San Marco", "1", "30124", "Venedig", Country.Italien);
        Person person99 = new Person(Salutation.Frau, "Signora", "Anna", "Rossi", new DateTime(1935, 6, 18), address99);
        BankDetails bankDetails99 = new BankDetails("Banca d'Italia", "IT1234567890123456789012345");
        Pension pension99 = new Pension(
            "e8d9b95d-e83f-4e6f-9854-35bfc1e632fc",
            person99,
            "104",
            "32180635R53621",
            PensionType.Altersrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Aktiv,
            bankDetails99
        );
        _pensions.Add(pension99);
        
        // Beispiel 100
        Address address100 = new Address("Broadway", "123", "10001", "New York", Country.Vereinigte_Staaten_von_Amerika);
        Person person100 = new Person(Salutation.Herr, "Mr.", "John", "Smith", new DateTime(1955, 3, 12), address100);
        BankDetails bankDetails100 = new BankDetails("Bank of America", "US1234567890123456789012345");
        Pension pension100 = new Pension(
            "cd654b66-2b1e-4b66-856e-36d8bfe84c1e",
            person100,
            "105",
            "26120355S24077",
            PensionType.Altersrente,
            true,
            PayoutMethod.Banküberweisung,
            PensionStatus.Aktiv,
            bankDetails100
        );
        _pensions.Add(pension100);
        
        // Beispiel 101
        Address address101 = new Address("Kurfürstendamm", "42", "10719", "Berlin", Country.Deutschland);
        Person person101 = new Person(Salutation.Frau, "Frau", "Maria", "Schmidt", new DateTime(1952, 8, 28), address101);
        BankDetails bankDetails101 = new BankDetails("Deutsche Bank", "DE1234567890123456789012345");
        Pension pension101 = new Pension(
            "96bf3864-56ab-4d79-aa38-2eb78af17572",
            person101,
            "106",
            "31280852S84733",
            PensionType.Altersrente,
            false,
            PayoutMethod.Scheckauszahlung,
            PensionStatus.Ausgesetzt,
            bankDetails101
        );
        _pensions.Add(pension101);


    }
}