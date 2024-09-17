using Microsoft.AspNetCore.Mvc;
using ProduccionBack.Entities;
using ProduccionBack.Services;
using System.ComponentModel;

namespace ProduccionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenProduccionController : ControllerBase
    {
        private IProduccionService service;

        public OrdenProduccionController()
        {
            service = new ProduccionService();
        }

        // GET: api/<OrdenProduccionController>
        [HttpGet("componentes")]
        public IActionResult Get()
        {
            return Ok(service.ConsultarComponentes());
        }
       
        // POST api/<OrdenProduccionController>
        [HttpPost]
        public IActionResult Post([FromBody] OrdenProduccion orden)
        {
            try { 
                if(orden == null)
                {
                    return BadRequest("Se esperaba una orden de producción completa");
                }
                if (service.RegistrarProduccion(orden))
                    return Ok("Orden registrada con éxito!");
                else
                    return StatusCode(500, "No se pudo registrar la orden!");
            }
            catch (Exception )
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }

        // GET: api/ordenproduccion/ordenes
        [HttpGet("Ordenes")]
        public IActionResult GetOrdenes([FromQuery] DateTime? fecha, [FromQuery] string? estado)
        {
            try
            {
                var ordenes = service.ConsultarOrdenes(fecha, estado);
                if (ordenes.Count == 0)
                    return NotFound("No se encontraron ordenes con esos criterios!");
                return Ok(ordenes);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno, intente nuevamente!");
            }
        }
    }
}
