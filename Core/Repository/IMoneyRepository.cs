using Automation.Core.Entities;
using Automation.Core.Enumaration;

namespace Automation.Core.Repository
{
    public interface IMoneyRepository : IGenericRepository<Money>
    {
        Task<int> GetTotalMoney();
        Task<int> GetProperTapeIdForWithDrawByMoneyType(enumMoneyType moneyType);
        Task<int> TotalMoneyByProperTapeId(int ID_TAPE);
        Task<int> GetMoneyCountByTapeId(int ID_TAPE);
        Task<int> GetProperTapeIdForDepositByMoneyType(enumMoneyType moneyType);

    }
}
