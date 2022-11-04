using Hangfire;
using Automation.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using Automation.Core.Repository;

namespace AutomationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class JobManagementController : ControllerBase
    {
        private readonly IMoneyRepository _repository;
        public JobManagementController(IMoneyRepository repository)
        {
            _repository = repository;
            RecurringJob.AddOrUpdate(() => _repository.WaitingMoneyListToProperTape(), Cron.Minutely);
        }

        [HttpGet]
        public string Get()
        {
            return "AutomationAPI - JobManagement Successfull Reached.";
        }

    }
}
