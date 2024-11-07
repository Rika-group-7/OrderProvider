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

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrdersAsync();
        if (orders != null)
        {
            return Ok(orders);
        }

        return BadRequest("no orders found");
    }

    [HttpGet("ordernumber/{orderNumber}")]
    public async Task<IActionResult> GetOrderByOrderNumber(int orderNumber)
    {
        var order = await _orderService.GetOrderByOrderNumberAsync(orderNumber);
        if (order != null)
        {
            return Ok(order);
        }

        return BadRequest("order not found");
    }

    [HttpGet("customerId/{customerId}")]
    public async Task<IActionResult> GetOrdersByCustomer(string customerId)
    {
        var orders = await _orderService.GetOrdersByCustomerIdAsync(customerId);
        if (orders != null)
        {
            return Ok(orders);
        }

        return BadRequest("no orders found");
    }

    [HttpGet("CustomerEmail/{customerEmail}")]
    public async Task<IActionResult> GetOrdersByCustomerEmail(string customerEmail)
    {
        var orders = await _orderService.GetOrdersByCustomerEmailAsync(customerEmail);
        if (orders != null)
        {
            return Ok(orders);
        }

        return BadRequest("no orders found");
    }
}
