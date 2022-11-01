

using Automation.Core.Entities;
using Automation.Core.Service;

namespace Automation.Service.Service
{
    public interface IMoneyService : IService<Money>
    {
        Task<List<Money>> GetMoneyWichTape();  
    }
}
