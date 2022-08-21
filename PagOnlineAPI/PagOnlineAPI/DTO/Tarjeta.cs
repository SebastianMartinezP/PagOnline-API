namespace PagOnlineAPI.DTO
{
    [Serializable]
    public partial class Tarjeta
    {
        public decimal Id { get; set; }
        public string? Numero { get; set; }
        public int Pin { get; set; }
        public string? FechaValida { get; set; }
    }
}
