using Microsoft.AspNetCore.Mvc;
using Onlineshop.DTOs;
using Onlineshop.Models;
using Onlineshop.Repositories;

namespace Onlineshop.Controllers;

[ApiController]
[Route("api/order")]

public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderRepository _order;
    private readonly IProductRepository _product;

    public OrderController(ILogger<OrderController> logger, IOrderRepository order, IProductRepository product)
    {
        _logger = logger;
        _order = order;
        _product = product;
    }

    [HttpPost]
    public async Task<ActionResult<List<OrderDTO>>> Createorder([FromBody] OrderCreateDTO Data)
    {


        var toCreateOrder = new Order
        {
            //    OrderDate= Data.OrderDate,
            DeliveryDate = Data.DeliveryDate.UtcDateTime,
            CustomerId = Data.CustomerId

        };
        var createdOrder = await _order.Create(toCreateOrder);
        return StatusCode(StatusCodes.Status201Created, createdOrder.asDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
    {
        var ordersList = await _order.GetList();
        var dtoList = ordersList.Select(x => x.asDto);
        return Ok(dtoList);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrderById([FromRoute] int id)
    {
        var orders = await _order.GetById(id);
        if (orders is null)
            return NotFound("No order is found with given id");
        var dto = orders.asDto;


        dto.Product = await _product.GetAllForOrder(id);
        // dto.Product = (await _product.GetAllForOrder(id)).Select(x => x.asDto).ToList();


        return Ok(dto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> Updateorder([FromRoute] int id, [FromBody] OrderUpdateDTO Data)
    {
        var existing = await _order.GetById(id);
        if (existing is null)
            return NotFound("No order found with given order id");

        var toUpdateorder = existing with
        {
            DeliveryDate = Data.DeliveryDate
        };

        var didUpdate = await _order.Update(toUpdateorder);
        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not Update order");

        return NoContent();
    }





}