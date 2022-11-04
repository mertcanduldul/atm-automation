using Automation.Core.Dto;
using Automation.Core.Entities;
using Automation.Core.Enumaration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.Service
{
    public interface IMoneyService : IService<Money>
    {
        Task<int> GetTotalMoney();
        Task<WithDrawModel> WithDraw(enumMoneyType moneyType, int moneyValue);
        Task<PreDepositModel> Deposit(DepositRequest request);
        Task<List<Money>> SplitMoneyToPaper(int moneyValue, enumMoneyType moneyType);
        enumMoneyName moneyTypeToString(enumMoneyType type);
    }
}
