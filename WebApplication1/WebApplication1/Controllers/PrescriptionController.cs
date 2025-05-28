using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Sevices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionController(IDbService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PrescriptionCreateDto dto)
    {
        try
        {
            ICollection<PrescriptionCreateDto> prescriptions = await service.CreatePrescriptionAsync(dto);
            return Created(string.Empty, prescriptions);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Wystąpił nieoczekiwany błąd serwera." });
        }
    }
}