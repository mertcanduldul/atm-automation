using Automation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.Dto
{
    public class PreDepositModel : DepositResponse
    {
        public List<Money> Data { get; set; }
    }
}
