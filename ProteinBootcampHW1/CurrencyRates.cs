using Newtonsoft.Json.Linq;

namespace ProteinBootcampHW1
{
    public class CurrencyRates
    {
        public bool Success { get; set; }
        public string Base { get; set; }
        public List<string> Rates { get; set; }

        public CurrencyRates(JObject jo, List<string> list)
        {
            Rates = list;
            Success = (bool) jo["success"];
            Base = (string) jo["base"];
        }

    }
}
