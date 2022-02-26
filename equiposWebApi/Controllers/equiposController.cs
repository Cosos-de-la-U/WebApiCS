using equiposWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace equiposWebApi.Controllers;

[ApiController] 
//[Route("api/[controller]")]
public class equiposController : ControllerBase
{
    //Configurar mi variable de conexional dbcontext
    private readonly prestamosContext _context;

    public equiposController(prestamosContext context)
    {
        _context = context;
    }
    [HttpGet]
    [Route("api/equipos")]
    public IActionResult Get()
    {
        IEnumerable<equipos> equiposList = (from e in _context.equipos select e);
        if (equiposList.Count() > 0)
        {
            return Ok(equiposList);
        }

        return NotFound();
    }
}