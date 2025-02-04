using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PaisesApiPrueba5.Models;
using ServiceReferenceCountry;


namespace PaisesApiPrueba5.Services
{
    public class PaisesServices
    {

        private string[] codeCountrys = ["USA","ECU","BRA"];



        public async Task<List<tCountryCodeAndName>> listaPaises()
        {
            

            CountryInfoServiceSoapTypeClient countryClient = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);



            ListOfCountryNamesByNameRequestBody listofCodeBody = new ListOfCountryNamesByNameRequestBody();

            ListOfCountryNamesByNameRequest countryNameRequest = new ListOfCountryNamesByNameRequest(listofCodeBody);

            ListOfCountryNamesByNameResponse response = await countryClient.ListOfCountryNamesByNameAsync(countryNameRequest);

            List<tCountryCodeAndName> listaPais = response.Body.ListOfCountryNamesByNameResult.ToList();
            
            return listaPais;
        }


        public async Task<Paises> codigoPaises(string codigoPaises)
        {
            foreach (char charPais in codigoPaises)
            {
                if (!Char.IsLetter(charPais))
                {
                    Console.WriteLine(charPais);
                    Console.WriteLine(codigoPaises);
                    return null;
                }
            }
            
            CountryInfoServiceSoapTypeClient countryClient = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

            CountryNameRequestBody countryNameRequestBody = new CountryNameRequestBody(codigoPaises);

            CountryNameRequest countryNameRequest = new CountryNameRequest(countryNameRequestBody);

            CountryNameResponse response = await countryClient.CountryNameAsync(countryNameRequest);

            //, su nombre, su capital, moneda, bandera y código de teléfono

            CapitalCityRequestBody capitalCityRequestBody = new CapitalCityRequestBody(codigoPaises);

            CapitalCityRequest capitalCityRequest = new CapitalCityRequest(capitalCityRequestBody);

            CapitalCityResponse responseCapital = await countryClient.CapitalCityAsync(capitalCityRequest);

            //Moneda

            CurrencyNameRequestBody currencyNameRequestBody = new CurrencyNameRequestBody(codigoPaises);

            CurrencyNameRequest currencyNameRequest = new CurrencyNameRequest(currencyNameRequestBody);

            CurrencyNameResponse currencyNameResponse = await countryClient.CurrencyNameAsync(currencyNameRequest);
            
            // Bandera

            CountryFlagRequestBody countryFlagRequestBody = new CountryFlagRequestBody(codigoPaises);

            CountryFlagRequest countryFlagRequest = new CountryFlagRequest(countryFlagRequestBody);

            CountryFlagResponse countryFlagResponse = await countryClient.CountryFlagAsync(countryFlagRequest);

            //Codigo de teléfono

            CountryIntPhoneCodeRequestBody country = new CountryIntPhoneCodeRequestBody(codigoPaises);

            CountryIntPhoneCodeRequest countryIntPhoneCodeRequest = new CountryIntPhoneCodeRequest(country);

            CountryIntPhoneCodeResponse countryIntPhoneCodeResponse = await countryClient.CountryIntPhoneCodeAsync(countryIntPhoneCodeRequest);

            //Generacion del objeto

            Paises paises = new Paises()
            {
                nombre= response.Body.CountryNameResult,
                bandera = countryFlagResponse.Body.CountryFlagResult,
                capital = responseCapital.Body.CapitalCityResult,
                codigotelefono = countryIntPhoneCodeResponse.Body.CountryIntPhoneCodeResult,
                moneda = currencyNameResponse.Body.CurrencyNameResult
            };
            
            return paises;
        }
    }
}
