using Automation.Core.Enumaration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.Dto
{
    public class DepositRequest
    {
        public int MONEY_VALUE { get; set; }
        public enumMoneyType MONEY_TYPE { get; set; }
    }
}
