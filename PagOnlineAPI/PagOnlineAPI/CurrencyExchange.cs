using Microsoft.AspNetCore.Authentication;
using RestSharp;
using System.Globalization;
using System.Text.Json;


namespace PagOnlineAPI
{
    public class CurrencyExchange
    {
        public string sourceUrl = "https://mindicador.cl/api/{0}/{1}";
        public string dateTime { get; set; }



        public CurrencyExchange()
        {
            this.dateTime = DateTime.Now.ToString("dd-MM-yyyy");
        }

        public DTO.CurrencyData? GetCurrency(string indicator)
        {
            try
            {
                setRequestDateTime(indicator);
                RestClient client = 
                    new RestClient(string.Format(this.sourceUrl, indicator, this.dateTime));
                RestRequest request = new RestRequest();
                RestResponse response = client.Execute(request);

                if (response.IsSuccessStatusCode && response.Content != null)
                {
                    DTO.CurrencyData? currencyDataResponse = 
                        mapToCurrencyData(response.Content.ToString());

                    if (currencyDataResponse != null)
                    {
                        return currencyDataResponse;
                    }
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void setRequestDateTime(string indicator)
        {
            List<string> indicators = new List<string>() {"dolar", "euro"};

            if (indicators.Contains(indicator))
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday)
                {
                    this.dateTime = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
                }
                else if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    this.dateTime = DateTime.Now.AddDays(-2).ToString("dd-MM-yyyy");
                }
                else
                {
                    this.dateTime = DateTime.Now.ToString("dd-MM-yyyy");
                }
            }
        }


        public DTO.CurrencyData? mapToCurrencyData(string jsonString)
        {
            try
            {
                var data = JsonDocument.Parse(jsonString);
                DTO.CurrencyData currencyData = new DTO.CurrencyData()
                {
                    version = data.RootElement.GetProperty("version").ToString(),
                    author  = data.RootElement.GetProperty("autor").ToString(),
                    code    = data.RootElement.GetProperty("codigo").ToString(),
                    name    = data.RootElement.GetProperty("nombre").ToString(),
                    unit    = data.RootElement.GetProperty("unidad_medida").ToString(),
                    value   = float.Parse(
                        data.RootElement.
                            GetProperty("serie")[0].GetProperty("valor").ToString(), 
                        CultureInfo.InvariantCulture.NumberFormat)
                };
                return currencyData;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
