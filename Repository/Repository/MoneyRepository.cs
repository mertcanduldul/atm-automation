using Automation.Core.Entities;
using Automation.Core.Enumaration;
using Automation.Core.Repository;
using Automation.Repository.Context;
using Microsoft.EntityFrameworkCore;

namespace Automation.Repository.Repository
{
    public class MoneyRepository : GenericRepository<Money>, IMoneyRepository
    {
        public MoneyRepository(AppDbContext context) : base(context)
        {
        }

        //Çekilebilecek paraya ait kaseti döner.
        public async Task<int> GetProperTapeIdForWithDrawByMoneyType(enumMoneyType moneyType)
        {
            var getTape = await _context.Tapes
               .Where(x => x.TAPE_MONEY_TYPE_ID == moneyType
                           && (x.TAPE_STATE_TYPE_ID == enumTapeStateType.Outbound
                           || x.TAPE_STATE_TYPE_ID == enumTapeStateType.InboundOutbound))
               .Select(x => x.ID_TAPE).FirstOrDefaultAsync();
            return getTape;
        }

        //Kasete ait para toplamını döner
        public async Task<int> TotalMoneyByProperTapeId(int ID_TAPE)
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

        public async Task<int> GetProperTapeIdForDepositByMoneyType(enumMoneyType moneyType)
        {
            var getTape = await _context.Tapes
                .Where(x => x.TAPE_MONEY_TYPE_ID == moneyType
                            && (x.TAPE_STATE_TYPE_ID == enumTapeStateType.Inbound
                            || x.TAPE_STATE_TYPE_ID == enumTapeStateType.InboundOutbound))
                .Select(x => x.ID_TAPE).FirstOrDefaultAsync();
            return getTape;
        }

        public async Task<int> GetMoneyCountByTapeId(int ID_TAPE)
        {
            var count = await _context.Monies.Where(x => x.ID_TAPE == ID_TAPE).CountAsync();
            return count;
        }
    }
}

