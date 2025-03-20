using Microsoft.AspNetCore.Mvc;
using Weather.Api.Mapping;
using Weather.Api.Models;
using Weather.Api.Repositories;
using Weather.Api.Requests;

namespace Weather.Api.Controllers;

[ApiController]
//[Route("api/stacjameteo")]
public class StacjaMeteoController : ControllerBase
{
    private readonly IMeteoRepository _repository;

    public StacjaMeteoController(IMeteoRepository repository)
    {

        _repository = repository;

    }

    [HttpPost(ApiEndpoints.Meteos.Create)]
    public async Task<IActionResult> Create(
        [FromBody] CreateStacjaMeteoRequest request
        )
    {
        var item = request.MapToStacjaMeteo();

        var result = await _repository.CreateAsync(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        //return Created($"/api/stacjameteo/meteos/{item.Id}", item);
    }


    [HttpGet(ApiEndpoints.Meteos.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item is null)
        {
            return NotFound();
        }
        var response = item.MapToResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Meteos.GetAll)]
    public async Task<IActionResult> GetAll()
    {
        var items = await _repository.GetAllAsync();

        var response = items.MapToResponse();
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Meteos.Update)]
    public async Task<IActionResult> Update(
    [FromRoute] Guid id,
    [FromBody] UpdateStacjaMeteoRequest request
        )
    {
        var item = request.MapToStacjaMeteo(id);
        var result = await _repository.UpdateAsync(item);

        if (!result)
        {
            return NotFound();
        }
        var response = item.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Meteos.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var item = await _repository.DeleteByIdAsync(id);
        if (!item)
        {
            return NotFound();
        }
        return Ok();
    }

}
