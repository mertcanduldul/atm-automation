﻿using Automation.Core.Dto;
using Automation.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AutomationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        public readonly IMoneyService _moneyService;

        public MainController(IMoneyService moneyService)
        {
            _moneyService = moneyService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Servis kontrol metodu")]
        public string Get()
        {
            return "AutomationAPI Successfull Reached.";
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Para çekme işlemi")]
        public async Task<WithDrawResponse> WithDraw(WithDrawRequest request)
        {
            var list = await _moneyService.WithDraw(request.MoneyType, request.Money);
            if (list.IsSuccess)
            {
                await _moneyService.RemoveRangeAsync(list.Data);
            }
            return new WithDrawResponse { IsSuccess = list.IsSuccess, Message = list.Message, PaidMoney = list.TotalMoney };
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Para yatırma işlemi")]
        public async Task<DepositResponse> Deposit(DepositRequest request)
        {
            var list = await _moneyService.Deposit(request);
            if (list.IsSuccess)
                await _moneyService.AddRangeAsync(list.Data);
            return new DepositResponse { IsSuccess = list.IsSuccess, Message = list.Message };
        }

        [HttpGet]
        [SwaggerOperation(Summary = "ATM toplam parayı döner")]
        public async Task<IActionResult> GetTotalMoney()
        {
            var result = await _moneyService.GetTotalMoney();
            return Ok(result);
        }

    }
}
