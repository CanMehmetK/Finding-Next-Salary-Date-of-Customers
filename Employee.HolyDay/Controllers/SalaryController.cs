using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Employee.HolyDay.Core;

namespace Employee.HolyDay.Controllers
{
    [Route("api/[controller]")]
    public class SalaryController : Controller
    {
        private IPaymentDateCalculator _paymentDateCalculator;
        public SalaryController(IPaymentDateCalculator paymentDateCalculator)
        {
            _paymentDateCalculator = paymentDateCalculator;
        }


        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]SalaryDateCalculationDTO parameter)
        {
            return Ok(new { PaymentFrequency = parameter.PaymentFrequency.ToString(), NextSalaryDate = _paymentDateCalculator.Calculate(parameter) });
        }


    }
}
