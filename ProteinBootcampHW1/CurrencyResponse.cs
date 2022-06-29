using Newtonsoft.Json.Linq;

namespace ProteinBootcampHW1
{
    public class CurrencyResponse
    {
        public bool Success { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Amount { get; set; }
        public double Result { get; set; }
        

        public CurrencyResponse(JObject jo)
        {
            JToken jQuery = jo["query"];

            To = (string) jQuery["to"];

            From = (string) jQuery["from"];

            Amount = (double)jQuery["amount"];

            Success = (bool) jo["success"];

            Result = (double) jo["result"];
        }
    }
}
