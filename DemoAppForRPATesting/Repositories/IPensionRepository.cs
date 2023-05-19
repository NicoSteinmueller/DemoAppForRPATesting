using DemoAppForRPATesting.Models;

namespace DemoAppForRPATesting.Repositories;

public interface IPensionRepository
{
    public void AddPension(Pension pension);
    public Pension FindByPanrAndPrnr(string panr, string prnrPart);
    public Pension FindById(string id);
}