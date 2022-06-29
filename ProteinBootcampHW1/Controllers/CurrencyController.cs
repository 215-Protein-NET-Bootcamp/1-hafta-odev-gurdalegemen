using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace ProteinBootcampHW1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly IConfiguration config;

        public CurrencyController(IConfiguration iconfiguration)
        {
            config = iconfiguration;
        }

        [HttpGet]
        [Route("Get_Currencies")]
        public string GetCurrencies()
        {
            string _request = config.GetValue<string>("CurrencyListRequest");
            var your_api_key = config.GetValue<string>("YourApiKey");


            var client = new RestClient(_request);
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", your_api_key);

            IRestResponse response = client.Execute(request);

            return response.Content;
        }

        [HttpGet]
        [Route("Currency_Compare")]
        public CurrencyRates Currency_Value([FromQuery] string Base, string Symbols)
        {
            var _request = config.GetValue<string>("CurrencyValue");
            _request = _request.Replace("{base}", Base);

            Symbols = Symbols.Replace(",","%2C");
            _request = _request.Replace("{symbols}", Symbols);

            var your_api_key = config.GetValue<string>("YourApiKey");

            var client = new RestClient(_request);
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", your_api_key);

            IRestResponse response = client.Execute(request);

            JObject jObject = JObject.Parse(response.Content);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jObject["rates"].ToString());

            List<string> list = new List<string>();
            foreach (var item in dict)
            {
                list.Add(item.Key + ": " + item.Value);
            }

            CurrencyRates currencyRates = new CurrencyRates(jObject,list);

            return currencyRates;
        }

        [HttpPost]
        [Route("Convert_Currencies")]
        public CurrencyResponse Convert([FromQuery] Currency cur)
        {

            var your_api_key = config.GetValue<string>("YourApiKey");

            string _request = config.GetValue<string>("ConvertRequest");
            _request = _request.Replace("{to}", cur.To);
            _request = _request.Replace("{from}", cur.From);
            _request = _request.Replace("{amount}", cur.Amount.ToString());

            var client = new RestClient(_request);
            client.Timeout = -1;

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", your_api_key);

            IRestResponse response = client.Execute(request);

            JObject jObject = JObject.Parse(response.Content);

            CurrencyResponse currencyResponse = new CurrencyResponse(jObject);

            return currencyResponse;
        }


    }
}