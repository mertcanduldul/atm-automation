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

        public async Task<BaseDto> WithDraw(enumMoneyType moneyType, int moneyValue)
        {
            try
            {
                if (moneyValue % 20 == 0 || moneyValue % 50 == 0 || moneyValue % 100 == 0 || moneyValue % 500 == 0)
                {
                    var getTape = await _moneyRepository.GetProperTapeIdForWithDrawByMoneyType(moneyType);
                    var avaiableGetMoney = await _moneyRepository.TotalMoneyByProperTapeId(getTape);

                    if (getTape > 0 && avaiableGetMoney >= moneyValue)
                    {
                        List<Money> moneyList = await _moneyRepository.Where(x => x.ID_TAPE == getTape).ToListAsync();
                        _moneyRepository.RemoveRange(moneyList); //Kasetten para çekilir
                        return new BaseDto { IsSuccess = true, Message = "Para çekme işlemi başarılı" };
                    }
                    else
                        return new BaseDto { IsSuccess = false, Message = $"ATM içerisinde istenilen tutarda para bulunmamaktadır. En fazla çekilebilecek tutar {avaiableGetMoney}'dır." };
                }
                else
                {
                    return new BaseDto { IsSuccess = false, Message = "20₺, 50₺, 100₺ ve katlarını çekebilirsiniz." };
                }
            }
            catch (Exception ex)
            {
                return new BaseDto { IsSuccess = false, Message = $"Para çekme işleminde hata alındı. Hata Detayı : {ex.ToString()}" };
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
            catch (Exception ex)
            {
                return new PreDepositModel { IsSuccess = false, Message = $"Para yatırma işleminde hata alındı. Hata Detayı : { ex.ToString() }" };
            }
        }
        public async Task<List<Money>> SplitMoneyToPaper(int moneyValue, enumMoneyType moneyType)
        {
            List<Money> moneyList = new List<Money>();
            int[] paper = new int[] { 500, 100, 50, 20 };
            int[] paperCounter = new int[4];

            // Greedy approach Algoritması ile parayı parçala
            for (int i = 0; i < paperCounter.Length; i++)
            {
                if (moneyValue >= paper[i])
                {
                    paperCounter[i] = moneyValue / paper[i];
                    moneyValue = moneyValue % paper[i];
                }
            }

            for (int i = 0; i < paperCounter.Length; i++)
            {
                if (paperCounter[i] != 0)
                {
                    for (int j = 0; j < paperCounter[i]; j++)
                    {
                        Money money = new Money();
                        money.MONEY_VALUE = paper[i];
                        money.MONEY_TYPE_ID = moneyType;
                        money.MONEY_NAME = moneyTypeToString(moneyType);
                        money.ID_TAPE = 100; //Parçalanmış paraların bulunduğu kaset Queue olacak
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
    }
}