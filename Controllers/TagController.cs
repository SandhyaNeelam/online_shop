using Microsoft.AspNetCore.Mvc;
using Onlineshop.DTOs;
using Onlineshop.Models;
using Onlineshop.Repositories;

namespace Onlineshop.Controllers;

[ApiController]
[Route("api/Tag")]

public class TagController : ControllerBase
{
    private readonly ILogger<TagController> _logger;
    private readonly ITagRepository _tag;

    public TagController(ILogger<TagController> logger, ITagRepository tag)
    {
        _logger = logger;
        _tag = tag;
    }

    [HttpPost]
    public async Task<ActionResult<List<TagDTO>>> CreateTag([FromBody] TagCreateDTO Data)
    {


        var toCreateTag = new Tag
        {

            Name = Data.Name.Trim(),
            ProductId= Data.ProductId

        };
        var createdTag = await _tag.Create(toCreateTag);
        return StatusCode(StatusCodes.Status201Created, createdTag.asDto);
    }

    [HttpGet]
    public async Task<ActionResult<List<TagDTO>>> GetAllTags()
    {
        var TagsList = await _tag.GetList();
        var dtoList = TagsList.Select(x => x.asDto);
        return Ok(dtoList);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<TagDTO>> GetTagById([FromRoute] int id)
    {
        var user = await _tag.GetById(id);
        if (user is null)
            return NotFound("No Tag is found with given id");
        var dto = user.asDto;


        // dto.Posts = await _posts.GetAllForUser(user.Id);

        return Ok(dto);
    }



}