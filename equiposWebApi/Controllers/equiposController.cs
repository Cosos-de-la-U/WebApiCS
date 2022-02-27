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
        IEnumerable<equipos> equiposList = (from e in _context.equipos select e).ToList();
        if (equiposList.Count() > 0)
        {
            return Ok(equiposList);
        }

        return NotFound();
    }

    [HttpGet]
    [Route("api/equipos/{idUsuario}")]
    public IActionResult Get(int idUsuario)
    {
        //Le quitamos Enum porque no va a tener varios registros
         equipos? unEquipo = (from e in _context.equipos where 
                                                                e.id_equipos == idUsuario 
                                                                select e).FirstOrDefault();
        return (unEquipo != null) ? Ok(unEquipo) : NotFound();
    }

    [HttpPost]
    [Route("api/equipos/")]
    public IActionResult Post([FromBody]equipos nuevoEquipo)
    {
        try
        {
            _context.equipos.Add(nuevoEquipo);
            _context.SaveChanges();
            return Ok(nuevoEquipo);
        }
        catch (Exception e)
        {
            return BadRequest();
        }

    }

    [HttpPut]
    [Route("api/equipos")]
    public IActionResult Update([FromBody] equipos updateEquipo)
    {
        equipos? equipoExistente = (from e in _context.equipos
            where e.id_equipos == updateEquipo.id_equipos
            select e).FirstOrDefault();
        
        if (equipoExistente is null) return NotFound();
        
        equipoExistente.nombre = updateEquipo.nombre;
        equipoExistente.descripcion = updateEquipo.descripcion;

        _context.Entry(equipoExistente).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(equipoExistente);
    }
}