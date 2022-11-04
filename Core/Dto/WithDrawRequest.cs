using Automation.Core.Enumaration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.Dto
{
    public class WithDrawRequest
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Money { get; set; }
        [Required]
        public enumMoneyType MoneyType { get; set; }

    }
}
