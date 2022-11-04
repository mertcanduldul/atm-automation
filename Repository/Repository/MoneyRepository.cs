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

        //Kasete ait para toplamını döner.
        public async Task<int> TotalMoneyByProperTapeId(int ID_TAPE)
        {
            var avaiableGetMoney = await _context.Monies.Where(x => x.ID_TAPE == ID_TAPE).Select(x => x.MONEY_VALUE).SumAsync();
            return avaiableGetMoney;
        }

        //Toplam parayı döner.
        public async Task<int> GetTotalMoney()
        {
            var result = await _context.Monies.SumAsync(x => x.MONEY_VALUE);
            return result;
        }

        //Para tipine göre yatırmaya uygun olan kasetleri döner.
        public async Task<int> GetProperTapeIdForDepositByMoneyType(enumMoneyType moneyType)
        {
            var getTape = await _context.Tapes
                .Where(x => x.TAPE_MONEY_TYPE_ID == moneyType
                            && (x.TAPE_STATE_TYPE_ID == enumTapeStateType.Inbound
                            || x.TAPE_STATE_TYPE_ID == enumTapeStateType.InboundOutbound))
                .Select(x => x.ID_TAPE).FirstOrDefaultAsync();
            return getTape;
        }

        //Kasete ait kağıt sayısını döner
        public async Task<int> GetMoneyCountByTapeId(int ID_TAPE)
        {
            var count = await _context.Monies.Where(x => x.ID_TAPE == ID_TAPE).CountAsync();
            return count;
        }

        //Uygun kasete gönderilmeyi bekleyen kuyruktaki paraları döner.
        public async Task<List<Money>> GetQueueMoney()
        {
            var list = await _context.Monies.Where(x => x.ID_TAPE == 100).ToListAsync();
            return list;
        }

        //Kuyruktaki paraların uygun kasetini bulur.
        public async Task<Money> GetProperTapeForQueueMoney(Money money)
        {
            if (money.ID_TAPE == 100)
            {
                switch (money.MONEY_TYPE_ID)
                {
                    case enumMoneyType.TRY:
                        {
                            var firstTapePaperCount = await GetMoneyCountByTapeId(1);
                            var secondTapePaperCount = await GetMoneyCountByTapeId(2);
                            var lastTapePaperCount = await GetMoneyCountByTapeId(5);
                            Dictionary<int, int> tapeList = new()
                            {
                                { 1, firstTapePaperCount },
                                { 2, secondTapePaperCount },
                                { 5, lastTapePaperCount }
                            };

                            int minValueIndex = 0;
                            if (firstTapePaperCount >= secondTapePaperCount && firstTapePaperCount >= lastTapePaperCount)
                            {
                                //Ilk Kasetten para çekme işlemi yapılmadığından önce diğer kasetler doldurulsun.
                                tapeList.Remove(1);
                                minValueIndex = tapeList.MinBy(x => x.Value).Key;
                            }
                            else
                            {
                                minValueIndex = tapeList.MinBy(x => x.Value).Key;
                            }


                            money.ID_TAPE = minValueIndex;
                            return money;
                        }
                    case enumMoneyType.USD:
                        {
                            var firstTapePaperCount = await GetMoneyCountByTapeId(1);
                            var thirthTapePaperCount = await GetMoneyCountByTapeId(3);
                            var lastTapePaperCount = await GetMoneyCountByTapeId(5);
                            Dictionary<int, int> tapeList = new()
                            {
                                { 1, firstTapePaperCount },
                                { 3, thirthTapePaperCount },
                                { 5, lastTapePaperCount }
                            };

                            int minValueIndex = 0;
                            if (firstTapePaperCount >= thirthTapePaperCount && firstTapePaperCount >= lastTapePaperCount)
                            {
                                //Ilk Kasetten para çekme işlemi yapılmadığından önce diğer kasetler doldurulsun.
                                tapeList.Remove(1);
                                minValueIndex = tapeList.MinBy(x => x.Value).Key;
                            }
                            else
                            {
                                minValueIndex = tapeList.MinBy(x => x.Value).Key;
                            }
                            money.ID_TAPE = minValueIndex;
                            return money;
                        }
                    case enumMoneyType.EUR:
                        {
                            var firstTapePaperCount = await GetMoneyCountByTapeId(1);
                            var fourthTapePaperCount = await GetMoneyCountByTapeId(4);
                            var lastTapePaperCount = await GetMoneyCountByTapeId(5);
                            Dictionary<int, int> tapeList = new()
                            {
                                { 1, firstTapePaperCount },
                                { 4, fourthTapePaperCount },
                                { 5, lastTapePaperCount }
                            };

                            int minValueIndex = 0;
                            if (firstTapePaperCount >= fourthTapePaperCount && firstTapePaperCount >= lastTapePaperCount)
                            {
                                //Ilk Kasetten para çekme işlemi yapılmadığından önce diğer kasetler doldurulsun.
                                tapeList.Remove(1);
                                minValueIndex = tapeList.MinBy(x => x.Value).Key;
                            }
                            else
                            {
                                minValueIndex = tapeList.MinBy(x => x.Value).Key;
                            }
                            money.ID_TAPE = minValueIndex;
                            return money;
                        }
                    case enumMoneyType.ALL:
                        {
                            var firstTapePaperCount = await GetMoneyCountByTapeId(1);
                            var lastTapePaperCount = await GetMoneyCountByTapeId(5);

                            int minValueIndex = 0;
                            if (firstTapePaperCount >= lastTapePaperCount)
                                minValueIndex = 5;
                            else
                                minValueIndex = 1;

                            money.ID_TAPE = minValueIndex;
                            return money;
                        }
                    default:
                        return money;
                }
            }
            return money;
        }

        public async Task<List<Money>> WaitingMoneyListToProperTape()
        {
            var waitingMoneyList = await GetQueueMoney();
            for (int i = 0; i < waitingMoneyList.Count; i++)
            {
                var properTape = await GetProperTapeForQueueMoney(waitingMoneyList[i]);
                waitingMoneyList[i].ID_TAPE = properTape.ID_TAPE;
            }
            _context.Monies.UpdateRange(waitingMoneyList);
            await _context.SaveChangesAsync();
            return waitingMoneyList;
        }
    }
}

