using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MarcasDeAutosDATA;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarcasDeAutosAPI.Controllers
{

    [Route("api/[controller]")] // Define la ruta base del controlador como '/api/MarcasAutos'
    [ApiController]
    public class MarcasAutosController : ControllerBase
    {
        private readonly MarcasDeAutosContext _context;

        // Constructor que inyecta el DbContext en el controlador
        public MarcasAutosController(MarcasDeAutosContext context)
        {
            _context = context;
        }
        
        // Acción para obtener todas las marcas de autos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaAuto>>> GetMarcasAutos()
        {
            // Asíncronamente obtenemos la lista de marcas de autos y las devolvemos
            return await _context.MarcasAutos.ToListAsync();
        }
    }
}
