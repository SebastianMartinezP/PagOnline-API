namespace PagOnlineAPI.DTO
{
    [Serializable]
    public class CurrencyData
    {
        public CurrencyData()
        {

        }
        public string? version { get; set; }
        public string? author { get; set; }
        public string? code { get; set; }
        public string? name { get; set; }
        public string? unit { get; set; }
        public float? value { get; set; }
    }
}
