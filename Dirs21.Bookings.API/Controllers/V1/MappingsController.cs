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
    [HttpGet("Reservations")]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReservationMappingAsync([FromQuery] string? sourceType, [FromQuery] string? targetType)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);

        var repoMapping = await mappingService.GetMappingAsync("Reservation", sourceType, targetType).ConfigureAwait(false);

        return Ok(repoMapping);
    }

    [HttpPut("Reservations")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> SaveReservationMappingAsync([FromQuery] string? sourceType, [FromQuery] string? targetType, [FromBody] string? inputMapping)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);
        ArgumentException.ThrowIfNullOrEmpty(inputMapping);

        var repoMapping = await mappingService.SaveMappingAsync("Reservation", sourceType, targetType, inputMapping).ConfigureAwait(false);

        return Ok(repoMapping);
    }

    [HttpPost("Reservations/MapData")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> MapReservationDataAsync([FromQuery] string? sourceType, [FromQuery] string? targetType, [FromBody] string? sourceData)
    {
        ArgumentException.ThrowIfNullOrEmpty(sourceType);
        ArgumentException.ThrowIfNullOrEmpty(targetType);
        ArgumentException.ThrowIfNullOrEmpty(sourceData);
        
        var targetData = await mappingService.MapDataAsync("Reservation", sourceType, targetType, sourceData).ConfigureAwait(false);

        return Ok(targetData);
    }
}