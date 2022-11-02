using Automation.Core.Dto;
using Automation.Core.Entities;
using Automation.Core.Enumaration;
using Automation.Core.Repository;
using Automation.Repository.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Repository.Repository
{
    public class MoneyRepository : GenericRepository<Money>, IMoneyRepository
    {
        public MoneyRepository(AppDbContext context) : base(context)
        {
        }

        //Çekilebilecek paraya ait kaseti döner.
        public async Task<int> AvaiableGetMoneyByMoneyType(enumMoneyType moneyType)
        {
            var getTape = await _context.Tapes
               .Where(x => x.TAPE_MONEY_TYPE_ID == moneyType
                           && (x.TAPE_STATE_TYPE_ID == enumTapeStateType.Outbound
                           || x.TAPE_STATE_TYPE_ID == enumTapeStateType.InboundOutbound))
               .Select(x => x.ID_TAPE).FirstOrDefaultAsync();
            return getTape;
        }

        //Kasete ait para toplamını döner
        public async Task<int> AvaiableTotalMoneyByTapeId(int ID_TAPE)
        {
            var avaiableGetMoney = await _context.Monies.Where(x => x.ID_TAPE == ID_TAPE).Select(x => x.MONEY_VALUE).SumAsync();
            return avaiableGetMoney;
        }

        //Para tipine göre toplam parayı döner.
        public async Task<int> GetTotalMoney()
        {
            var result = await _context.Monies.SumAsync(x => x.MONEY_VALUE);
            return result;
        }

    }
}

