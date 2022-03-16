using Microsoft.AspNetCore.Mvc;
using Onlineshop.DTOs;
using Onlineshop.Models;
using Onlineshop.Repositories;

namespace Onlineshop.Controllers;

[ApiController]
[Route("api/customer")]

public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> _logger;
    private readonly ICustomerRepository _customer;
    private readonly IOrderRepository _order;

    public CustomerController(ILogger<CustomerController> logger, ICustomerRepository customer, IOrderRepository order)
    {
        _logger = logger;
        _customer = customer;
        _order = order;
    }

    [HttpPost]
    public async Task<ActionResult<List<CustomerDTO>>> CreateCustomer([FromBody] CustomerCreateDTO Data)
    {


        var toCreateCustomer = new Customer
        {

            Name = Data.Name.Trim(),
            Mobile = Data.Mobile,
            Email = Data.Email.ToLower().Trim(),
            Address = Data.Address.Trim()
        };
        var createdCustomer = await _customer.Create(toCreateCustomer);
        return StatusCode(StatusCodes.Status201Created, createdCustomer.asDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers()
    {
        var customersList = await _customer.GetList();
        var dtoList = customersList.Select(x => x.asDto);
        return Ok(dtoList);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerDTO>> GetCustomerById([FromRoute] int id)
    {
        var user = await _customer.GetById(id);
        if (user is null)
            return NotFound("No customer is found with given id");
        var dto = user.asDto;

        dto.Order = (await _order.GetAllForCustomer(id)).Select(x =>x.asDto).ToList();
    
        return Ok(dto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCustomer([FromRoute] int id, [FromBody] CustomerUpdateDTO Data)
    {
        var existing = await _customer.GetById(id);
        if (existing is null)
            return NotFound("No customer found with given id");

        var toUpdateCustomer = existing with
        {
            Mobile = Data.Mobile,
            // Mobile = Data.Mobile ?? existing.Mobile,
            Email = Data.Email?.ToLower().Trim() ?? existing.Email,
            Address = Data.Address?.Trim() ?? existing.Address

        };

        var didUpdate = await _customer.Update(toUpdateCustomer);
        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not Update Customer");

        return NoContent();
    }





}