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
        Task<BaseDto> GetMoney(enumMoneyType moneyType, int moneyValue);
        
    }
}
