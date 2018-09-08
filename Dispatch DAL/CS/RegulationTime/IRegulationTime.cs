using System.Collections.Generic;

namespace Dispatch
{
    public interface IRegulationTime
    {
        List<RegulationTimeEntity> ListAllPaging(string searchString, int pageIndex, int pageSize, ref int totalRow);
        bool ChangeAllowedLate(int Id, int allowedLate);
        bool ChangeAllowEarly(int Id, int allowEarly);
        bool ChangeTimesLate(int Id, int timesLate);
        bool Delete(int Id);
        bool Insert(RegulationTimeEntity regulationTime);
        List<RegulationTimeEntity> ListAll();
        }
}
