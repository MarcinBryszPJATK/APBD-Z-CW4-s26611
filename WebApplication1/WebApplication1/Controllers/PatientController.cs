using Microsoft.AspNetCore.Mvc;
using WebApplication1.Sevices;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientController(IDbService service) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        try
        {
            var result = await service.GetPatientDetailsAsync(id);
            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "Wystąpił błąd serwera." });
        }
    }
}