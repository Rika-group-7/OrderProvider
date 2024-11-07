# OrderProvider
Hanterar sparandet av ordrar, tilldelas ordernummer.

## ENDPOINTS:

### POST:
#### `https://orderprovider-e8cvakccb9adgpep.northeurope-01.azurewebsites.net/api/order/createorder`


### GET:
#### `https://orderprovider-e8cvakccb9adgpep.northeurope-01.azurewebsites.net/api/order/getall`
#### `https://orderprovider-e8cvakccb9adgpep.northeurope-01.azurewebsites.net/api/order/ordernumber/"your ordernumber"`
#### `https://orderprovider-e8cvakccb9adgpep.northeurope-01.azurewebsites.net/api/order/customerId/"your customerId (same as userId from authenticationProvider)"`
#### `https://orderprovider-e8cvakccb9adgpep.northeurope-01.azurewebsites.net/api/order/CustomerEmail/"the customers email"`


## Models:

OrderRequest:
```
public class OrderRequest
{
    [Required]
    public string CustomerId { get; set; } = null!;
    [Required]
    public string CustomerEmail { get; set; } = null!;
    [Required]
    public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
}
```
OrderItemDto:
```
public class OrderItemDto
{
    [Required]
    public string ProductId { get; set; } = null!;
    [Required]
    public string ProductName { get; set; } = null!;
    public string? Size { get; set; }
    public string? Color { get; set; }
    [Required]
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
```

## JSON:

OrderRequest will look like this:
```
{
  "CustomerId": "test123",
  "CustomerEmail": "customer@example.com",
  "OrderItems": [
    {
      "ProductId": "prod-001",
      "ProductName": "Product Name 1",
      "Size": "M",
      "Color": "Red",
      "Quantity": 2,
      "Price": 19.99
    },
    {
      "ProductId": "prod-002",
      "ProductName": "Product Name 2",
      "Size": "L",
      "Color": "Blue",
      "Quantity": 1,
      "Price": 29.99
    }
  ]
}
```
The OrderItems is a list of the class OrderItemDto, you can add as many of these as you want, just remember the `,` after each OrderItemDto.

## Example: 
```
// Example list of items purchased by the customer
var purchasedProducts = new List<(string ProductId, string ProductName, string? Size, string? Color, int Quantity, decimal Price)>
{
    ("prod-001", "Product Name 1", "M", "Red", 2, 19.99m),
    ("prod-002", "Product Name 2", "L", "Blue", 1, 29.99m)
};

var orderItems = new List<OrderItemDto>();

foreach (var item in purchasedProducts)
{
    var orderItemDto = new OrderItemDto
    {
        ProductId = item.ProductId,
        ProductName = item.ProductName,
        Size = item.Size,
        Color = item.Color,
        Quantity = item.Quantity,
        Price = item.Price
    };

    orderItems.Add(orderItemDto);
}

var orderRequest = new OrderRequest
{
    CustomerId = "example123",
    CustomerEmail = "customer@example.com",
    OrderItems = orderItems
};
```

