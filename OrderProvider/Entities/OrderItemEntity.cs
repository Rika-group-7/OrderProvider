﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderProvider.Entities;

public class OrderItemEntity
{
    [Key]
    public string Id { get; set; } = null!;

    [ForeignKey("Order")]
    public string OrderEntityId { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;

    // different options for the product
    public string? Size { get; set; }
    public string? Color { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }

    public OrderEntity Order { get; set; } = null!;
}
