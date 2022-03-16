using Microsoft.AspNetCore.Mvc;
using Onlineshop.DTOs;
using Onlineshop.Models;
using Onlineshop.Repositories;

namespace Onlineshop.Controllers;

[ApiController]
[Route("api/product")]

public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductRepository _product;
    private readonly ITagRepository _tag;

    public ProductController(ILogger<ProductController> logger, IProductRepository product, ITagRepository tag)
    {
        _logger = logger;
        _product = product;
        _tag = tag;
    }

    [HttpPost]
    public async Task<ActionResult<List<ProductDTO>>> Createproduct([FromBody] ProductCreateDTO Data)
    {


        var toCreateProduct = new Product
        {

            Name = Data.Name.Trim(),
            Price = Data.Price,
            OrderId = Data.OrderId,
            Discount = Data.Discount

        };
        var createdProduct = await _product.Create(toCreateProduct);
        return StatusCode(StatusCodes.Status201Created, createdProduct.asDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDTO>>> GetAllProducts()
    {
        var ProductsList = await _product.GetList();
        var dtoList = ProductsList.Select(x => x.asDto);
        return Ok(dtoList);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetproductById([FromRoute] int id)
    {
        var user = await _product.GetById(id);
        if (user is null)
            return NotFound("No product is found with given id");
        var dto = user.asDto;


        dto.Tag = await _tag.GetAllForProduct(user.Id);

        return Ok(dto);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductUpdateDTO Data)
    {
        var existing = await _product.GetById(id);
        if (existing is null)
            return NotFound("No product found with given id");

        var toUpdateProduct = existing with
        {
            Price = Data.Price,
            Discount = Data.Discount

        };

         var didUpdate = await _product.Update(toUpdateProduct);
        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "Could not Update product");

        return NoContent();
    }





}