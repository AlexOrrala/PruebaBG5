using PaisesApiPrueba5.Models;
using PaisesApiPrueba5.Services;
using ServiceReferenceCountry;

namespace PruebaPais
{
    public class PruebaPais
    {
        public PaisesServices _paises = new PaisesServices(); 
        public PruebaPais()
        {
            ;
        }

        [Theory]
        [InlineData("1")]
        [InlineData("")]
        public async void codigoPais(string cod)
        {
             Paises paises = await _paises.codigoPaises(cod);

             Assert.NotNull(paises);

        }

        [Fact]
        public async void validarLista()
        {
            List<tCountryCodeAndName> tCountryCodeAndName = await _paises.listaPaises();

            Assert.NotEmpty(tCountryCodeAndName);
        }


    }
}

