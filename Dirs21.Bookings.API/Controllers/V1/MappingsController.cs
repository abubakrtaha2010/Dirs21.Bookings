namespace Dirs21.Bookings.API.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
[Produces("application/json")]
public class MappingsController(IMappingService mappingService) : ControllerBase
{
    [HttpGet("{key}")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReservationMappingAsync(string key, [FromQuery] string? sourceType, [FromQuery] string? targetType)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);

        var repoMapping = await mappingService.GetMappingAsync(key, sourceType, targetType).ConfigureAwait(false);

        return Ok(repoMapping);
    }

    [HttpPut("{key}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> SaveReservationMappingAsync(string key, [FromQuery] string? sourceType, [FromQuery] string? targetType, [FromBody] string? inputMapping)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);
        ArgumentException.ThrowIfNullOrEmpty(inputMapping);

        var repoMapping = await mappingService.SaveMappingAsync(key, sourceType, targetType, inputMapping).ConfigureAwait(false);

        return Ok(repoMapping);
    }

    [HttpPost("{key}/MapData")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> MapReservationDataAsync(string key, [FromQuery] string? sourceType, [FromQuery] string? targetType, [FromBody] string? sourceData)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);
        ArgumentException.ThrowIfNullOrEmpty(sourceData);

        var targetData = await mappingService.MapDataAsync(key, sourceType, targetType, sourceData).ConfigureAwait(false);

        return Ok(targetData);
    }
}