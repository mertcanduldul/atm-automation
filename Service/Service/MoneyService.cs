
using Automation.Core.Dto;
using Automation.Core.Entities;
using Automation.Core.Enumaration;
using Automation.Core.Repository;
using Automation.Core.Service;
using Automation.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Automation.Service.Service
{
    public class MoneyService : Service<Money>, IMoneyService
    {
        private readonly IMoneyRepository _repository;

        public MoneyService(IGenericRepository<Money> genericRepository,IUnitOfWork unitOfWork, IMoneyRepository repository) : base(repository, unitOfWork)
        {
            _repository = repository;
        }

        public async Task<BaseDto> GetMoney(enumMoneyType moneyType, int moneyValue)
        {
            if (moneyValue % 20 == 0 || moneyValue % 50 == 0 || moneyValue % 100 == 0)
            {
                var getTape = await _repository.AvaiableGetMoneyByMoneyType(moneyType);
                var avaiableGetMoney = await _repository.AvaiableTotalMoneyByTapeId(getTape);

                if (getTape > 0 && avaiableGetMoney >= moneyValue)
                {
                    List<Money> moneyList = await _repository.Where(x => x.ID_TAPE == getTape).ToListAsync();
                    _repository.RemoveRange(moneyList); //Kasetten para çekilir
                    return new BaseDto { IsSuccess = true, Message = "Para çekme işlemi başarılı" };
                }
                else
                    return new BaseDto { IsSuccess = false, Message = "ATM içerisinde istenilen tutarda para bulunmamaktadır." };
            }
            else
            {
                return new BaseDto { IsSuccess = false, Message = "20₺, 50₺, 100₺ ve katlarını çekebilirsiniz." };
            }
        }

        public async Task<int> GetTotalMoney()
        {
            var total = await _repository.GetTotalMoney();
            return total;
        }

    }
}
