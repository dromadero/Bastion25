using Microsoft.AspNetCore.Mvc;
using Weather.Api.Mapping;
using Weather.Api.Models;
using Weather.Api.Repositories;
using Weather.Api.Requests;
using Weather.Api.Services;

namespace Weather.Api.Controllers;

[ApiController]
//[Route("api/stacjameteo")]
public class StacjaMeteoController : ControllerBase
{
    private readonly IMeteoService _service;

    public StacjaMeteoController(IMeteoService service)
    {
        _service = service;
    }

    [HttpPost(ApiEndpoints.Meteos.Create)]
    public async Task<IActionResult> Create(
        [FromBody] CreateStacjaMeteoRequest request,
        CancellationToken token
        )
    {
        var item = request.MapToStacjaMeteo();

        var result = await _service.CreateAsync(item);
        return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        //return Created($"/api/stacjameteo/meteos/{item.Id}", item);
    }


    [HttpGet(ApiEndpoints.Meteos.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid id,
         CancellationToken token)
    {
        var item = await _service.GetByIdAsync(id);
        if (item is null)
        {
            return NotFound();
        }
        var response = item.MapToResponse();
        return Ok(response);
    }

    [HttpGet(ApiEndpoints.Meteos.GetAll)]
    public async Task<IActionResult> GetAll(CancellationToken token)
    {
        var items = await _service.GetAllAsync();

        var response = items.MapToResponse();
        return Ok(response);
    }

    [HttpPut(ApiEndpoints.Meteos.Update)]
    public async Task<IActionResult> Update(
    [FromRoute] Guid id,
    [FromBody] UpdateStacjaMeteoRequest request,
     CancellationToken token
        )
    {
        var item = request.MapToStacjaMeteo(id);
        var result = await _service.UpdateAsync(item);

        if (result is null)
        {
            return NotFound();
        }
        var response = result.MapToResponse();
        return Ok(response);
    }

    [HttpDelete(ApiEndpoints.Meteos.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
    {
        var item = await _service.DeleteByIdAsync(id, token);
        if (!item)
        {
            return NotFound();
        }
        return Ok();
    }

}
