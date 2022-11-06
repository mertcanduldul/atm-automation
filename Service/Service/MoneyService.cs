using Automation.Core.Entities;
using Automation.Core.Dto;
using Automation.Core.Repository;
using Automation.Core.Service;
using Automation.Core.UnitOfWork;
using Automation.Core.Enumaration;
using Microsoft.EntityFrameworkCore;


namespace Automation.Service.Service
{
    public class MoneyService : Service<Money>, IMoneyService
    {
        private readonly IMoneyRepository _moneyRepository;

        public MoneyService(IGenericRepository<Money> repository, IUnitOfWork unitOfWork, IMoneyRepository moneyRepository) : base(repository, unitOfWork)
        {
            _moneyRepository = moneyRepository;
        }

        public async Task<WithDrawModel> WithDraw(enumMoneyType moneyType, int moneyValue)
        {
            try
            {
                if (moneyValue % 20 == 0 || moneyValue % 50 == 0 || moneyValue % 100 == 0 || moneyValue % 500 == 0)
                {
                    var getTape = await _moneyRepository.GetProperTapeIdForWithDrawByMoneyType(moneyType);
                    var avaiableGetMoney = await _moneyRepository.TotalMoneyByProperTapeId(getTape);

                    if (getTape > 0 && avaiableGetMoney >= moneyValue)
                    {
                        List<Money> ThirthTapeMoneyCountByTypeList = await _moneyRepository.Where(x => x.ID_TAPE == 4 && x.MONEY_TYPE_ID == moneyType).ToListAsync();
                        List<Money> moneyList = await _moneyRepository.Where(x => x.ID_TAPE == getTape).ToListAsync();
                        List<Money> responseList = new();

                        moneyList.AddRange(ThirthTapeMoneyCountByTypeList);

                        var getMoneyFromTapeByMoneyValue = await SplitMoneyToPaper(moneyValue, moneyType);

                        foreach (var item in getMoneyFromTapeByMoneyValue)
                        {
                            var getMoney = moneyList.Where(x => x.MONEY_TYPE_ID == item.MONEY_TYPE_ID && x.MONEY_VALUE == item.MONEY_VALUE).FirstOrDefault();
                            if (getMoney != null)
                            {
                                responseList.Add(getMoney);
                            }
                        }

                        var totalMoney = responseList.Sum(x => x.MONEY_VALUE);

                        if (moneyValue == totalMoney)
                            return new WithDrawModel { IsSuccess = true, Message = "Para çekme işlemi başarılı", Data = responseList, TotalMoney = totalMoney };
                        else
                            return new WithDrawModel { IsSuccess = false, Message = "Para çekme işlemi için yeterli para adedi bulunmamaktadır." };
                    }
                    else
                        return new WithDrawModel { IsSuccess = false, Message = $"ATM içerisinde istenilen tutarda para bulunmamaktadır. En fazla çekilebilecek tutar {avaiableGetMoney} {moneyType}'dır." };
                }
                else
                {
                    return new WithDrawModel { IsSuccess = false, Message = "20₺, 50₺, 100₺ ve katlarını çekebilirsiniz." };
                }
            }
            catch (Exception ex)
            {
                return new WithDrawModel { IsSuccess = false, Message = $"Para çekme işleminde hata alındı. Hata Detayı : {ex.ToString()}" };
            }
        }
        public async Task<int> GetTotalMoney()
        {
            var total = await _moneyRepository.GetTotalMoney();
            return total;
        }
        public async Task<PreDepositModel> Deposit(DepositRequest request)
        {
            try
            {
                if (request.MONEY_VALUE % 20 == 0 || request.MONEY_VALUE % 50 == 0 || request.MONEY_VALUE % 100 == 0 || request.MONEY_VALUE % 500 == 0)
                {
                    var tapeId = await _moneyRepository.GetProperTapeIdForDepositByMoneyType(request.MONEY_TYPE);

                    if (tapeId > 0)
                    {
                        var tapeCount = await _moneyRepository.GetMoneyCountByTapeId(tapeId);
                        var depositPaper = await SplitMoneyToPaper(request.MONEY_VALUE, request.MONEY_TYPE);

                        if (tapeCount + depositPaper.Count < 100)
                        {
                            return new PreDepositModel { IsSuccess = true, Message = "Para yatırma işlemi başarılı.", Data = depositPaper };
                        }
                        else
                        {
                            return new PreDepositModel { IsSuccess = false, Message = "Kasette yeterli alan bulunamadı." };
                        }
                    }
                    else
                    {
                        return new PreDepositModel { IsSuccess = false, Message = "Paraya uygun kaset bulunamadı." };
                    }
                }
                else
                {
                    return new PreDepositModel { IsSuccess = false, Message = "20₺, 50₺, 100₺ ve katlarını yatırabilirsiniz." };
                }
            }
            catch (Exception ex)
            {
                return new PreDepositModel { IsSuccess = false, Message = $"Para yatırma işleminde hata alındı. Hata Detayı : {ex.ToString()}" };
            }
        }
        public async Task<List<Money>> SplitMoneyToPaper(int moneyValue, enumMoneyType moneyType)
        {
            List<Money> moneyList = new();
            int[] paper = new int[] { 500, 100, 50, 20 };
            int[] paperCounter = new int[4];

            // Greedy approach Algoritması ile parayı parçala
            for (int i = 0; i < paperCounter.Length; i++)
            {
                if (moneyValue >= paper[i])
                {
                    paperCounter[i] = moneyValue / paper[i];
                    moneyValue %= paper[i];
                }
            }

            for (int i = 0; i < paperCounter.Length; i++)
            {
                if (paperCounter[i] != 0)
                {
                    for (int j = 0; j < paperCounter[i]; j++)
                    {
                        Money money = new()
                        {
                            MONEY_VALUE = paper[i],
                            MONEY_TYPE_ID = moneyType,
                            MONEY_NAME = moneyTypeToString(moneyType),
                            ID_TAPE = 100 //Parçalanmış paraların bulunduğu kaset Queue olacak, Hangfire ile sürekli olarak tetiklenecek
                        };
                        moneyList.Add(money);
                    }
                }
            }
            return moneyList;
        }


        public enumMoneyName moneyTypeToString(enumMoneyType type)
        {
            switch (type)
            {
                case enumMoneyType.TRY: return enumMoneyName.TurkishLira;
                case enumMoneyType.USD: return enumMoneyName.USADollar;
                case enumMoneyType.EUR: return enumMoneyName.Euro;
                default: return enumMoneyName.TurkishLira;
            }
        }

        public async Task<List<Money>> WaitingMoneyListToProperTape()
        {
            var result = await _moneyRepository.WaitingMoneyListToProperTape();
            return result;
        }
    }
}