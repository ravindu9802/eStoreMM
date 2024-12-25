using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Products.Add;
using Products.Application.Products.AdjustInventry;
using Products.Application.Products.Delete;
using Products.Application.Products.GetAll;

namespace eStore.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetAllProductsQuery();
        var res = await _sender.Send(query);
        return Ok(res);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRequest productRequest)
    {
        var command = new AddProductCommand(productRequest);
        var res = await _sender.Send(command);
        return Ok(res);
    }

    [Authorize]
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var command = new DeleteProductCommand(id);
        var res = await _sender.Send(command);
        return Ok(res);
    }

    [HttpPost]
    [Route("adjustInventory")]
    public async Task<IActionResult> UpdateInventory([FromBody] AdjustProductInventoryRequest request)
    {
        var command = new AdjustProductInventoryCommand(request.ProductId, request.Quantity, request.Option);
        var res = await _sender.Send(command);
        if (!res) return BadRequest();
        return Ok(res);
    }
}