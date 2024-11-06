using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderProvider.Interfaces;
using OrderProvider.Models;

namespace OrderProvider.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("createorder")]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequest orderRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _orderService.CreateOrderAsync(orderRequest);
        if (result)
        {
            return Ok();
        }



        return Ok();
    }
}
