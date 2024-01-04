using Examenp2FO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Examenp2FO.Models.Class;

namespace Examenp2FO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly apllicationdbcontext_ context;

        public ValuesController(applicationDbContext context)
        {
            _context = context;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }


    }
}


using Examenp2FO.Models;
using Examenp2FO.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace IntegracionP2ES.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Geocode _geocodeService;

        public ClientesC(ApplicationDbContext context, Geocode geocodeService)
        {
            _context = context;
            _geocodeService = geocodeService;
        }

        // GET: api/clientes/usuario/jalmeida
        [HttpGet("usuario/{username}")]
        public async Task<IActionResult> GetGeocodeDataByUsuario(string username)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Usuario == username);
            if (cliente == null)
            {
                return NotFound("Usuario no encontrado");
            }

            var geocodeData = await _geocodeService.GetGeocodeDataAsync(cliente.Ciudad);
            var geoData = JsonConvert.DeserializeObject<GeocodeData>(geocodeData);

            var city = geoData.Standard.City;
            var province = geoData.Standard.Prov;
            var countryName = geoData.Standard.Countryname;

            var existingData = await _context.CiudadesGeoreferenciadas
                .FirstOrDefaultAsync(g => g.City == city && g.Province == province && g.CountryName == countryName);

            if (existingData == null && city != null && province != null && countryName != null)
            {
                var newGeoData = new CiudadesGeoreferenciadas
                {
                    City = city,
                    Province = province,
                    CountryName = countryName
                };
                _context.CiudadesGeoreferenciadas.Add(newGeoData);
                await _context.SaveChangesAsync();
            }

            return Ok(geocodeData);
        }
    }
}