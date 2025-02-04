using Microsoft.AspNetCore.Mvc;
using PaisesApiPrueba5.Services;
using PaisesApiPrueba5.Models;
using ServiceReferenceCountry;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaisesApiPrueba5.Controllers
{
    [Route("api/países")]
    [ApiController]
    public class PaisesController : ControllerBase
    {
        public PaisesServices _paisesService;
        public PaisesController(PaisesServices paisesServices) {
            _paisesService = paisesServices;
        }

        // GET: api/<PaisesController>
        [HttpGet]
        public async Task<List<tCountryCodeAndName>> GetPaisesLista()
        {
            return await _paisesService.listaPaises();
        }

        // GET api/<PaisesController>/5
        [HttpGet("/{cod}")]
        public async Task<Paises> GetPais(string cod)
        {
            Paises pais = await _paisesService.codigoPaises(cod);
            
            return pais;
        }

    }
}
