using Microsoft.AspNetCore.Mvc;
using PaymentsService.Application.Interfaces;
using PaymentsService.Controllers.Models;

namespace PaymentsService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PaymentController : ControllerBase
  {
    private readonly IPaymentsService _paymentsService;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(IPaymentsService paymentsService, ILogger<PaymentController> logger)
    {
      _paymentsService = paymentsService;
      _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaymentResponce),StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreatePay([FromQuery] int amount)
    {
      if (amount < 0)
      {
        return BadRequest("Balance should not be negative");
      }
      var id = Guid.NewGuid();
      await _paymentsService.CreatePayment(id, amount);
      return Ok(new PaymentResponce(
        userId: id,
        balance: amount));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdatePay([FromQuery] PaymentsQueryApi request)
    {
      try
      {
        var amount = await _paymentsService.UpdateBalance(request.userId, request.amount);
        return Ok(amount);
      }
      catch (Exception ex)
      {
        return NotFound(ex.Message);
      }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetBalance([FromQuery] Guid id)
    {
      try
      {
        var result = await _paymentsService.PrintBalance(id);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return NotFound(ex.Message);
      }
    }
  }
}
