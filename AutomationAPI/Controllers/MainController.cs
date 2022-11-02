using Automation.Core.Dto;
using Automation.Core.Entities;
using Automation.Core.Enumaration;
using Automation.Core.Service;
using Automation.Service.Service;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public string Get()
        {
            return "AutomationAPI Successfull Reached.";
        }


        [HttpGet]
        public async Task<IActionResult> GetTotalMoney()
        {
            var result = await _moneyService.GetTotalMoney();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> GetMoney(GetMoneyRequest request)
        {
            var result = await _moneyService.GetMoney(request.MoneyType, request.Money);
            return Ok(result);
        }

    }
}
