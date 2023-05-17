using DemoAppForRPATesting.Models;
namespace DemoAppForRPATesting.Repositories;


public class PensionRepository
{
    private List<Pension> _pensions;

    public PensionRepository()
    {
        _pensions = new List<Pension>();
    }

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
}