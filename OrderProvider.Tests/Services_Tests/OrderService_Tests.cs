using Moq;
using OrderProvider.Interfaces;
using OrderProvider.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProvider.Tests.Services_Tests;

internal class OrderService_Tests
{
    private readonly Mock<IOrderRepo> _repo;
    private readonly IOrderService _service;

    public OrderService_Tests()
    {
        _repo = new Mock<IOrderRepo>();
        _service = new OrderService(_repo.Object, new Mock<IOrderItemRepo>().Object);
    }

    [Fact]
    public async Task Should_Create_order_AndReturnTrue()
    {

    }
}
