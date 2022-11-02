using Automation.Core.Dto;
using Automation.Core.Entities;
using Automation.Core.Enumaration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.Repository
{
    public interface IMoneyRepository : IGenericRepository<Money>
    {
        Task<int> GetTotalMoney();
        Task<int> AvaiableGetMoneyByMoneyType(enumMoneyType moneyType);
        Task<int> AvaiableTotalMoneyByTapeId(int ID_TAPE);
    }
}
