using HealthcareApi.Data;
using HealthcareApi.DataTransferObjects;
using HealthcareApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HealthcareApi.Interface;
using Microsoft.AspNetCore.Authorization;


namespace HealthcareApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentInfoController : ControllerBase
    {
        private readonly IPayment _paymentRepo;


        public PaymentInfoController(IPayment paymentRepo)
        {
            _paymentRepo = paymentRepo;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Order), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetPaymentInfoById(int id)
        {
            try
            {                
                var payment = await Task.FromResult(_paymentRepo.GetPaymentById(id));
                if (payment == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(payment);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult CreatePayment(AddPaymentInfoDTO paymentdto)
        {
            try
            {
                var newPay = _paymentRepo.AddPayment(paymentdto);
                return Ok(newPay);
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + Environment.NewLine + ex.InnerException);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult UpdatePaymentAmount(int paymentid, double amount)
        {
            var payment = _paymentRepo.GetPaymentById(paymentid);
            if(payment == null)
                return NotFound();
            else
            {
                payment = _paymentRepo.UpdatePayment(paymentid, amount);
                return Ok(payment);

            }
        }
    }
}
