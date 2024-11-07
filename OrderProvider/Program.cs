using OrderProvider.Interfaces;
using OrderProvider.Repositories;
using OrderProvider.Services;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IOrderRepo, OrderRepo>();

builder.Services.AddScoped<IOrderItemRepo, OrderItemRepo>();

builder.Services.AddScoped<IOrderService, OrderService>();



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
