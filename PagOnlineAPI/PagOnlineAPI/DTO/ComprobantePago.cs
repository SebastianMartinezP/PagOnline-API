namespace PagOnlineAPI.DTO
{
    [Serializable]
    public partial class ComprobantePagoRequest
    {
        public string Numerotarjeta { get; set; } = null!;
        public string Nombretitular { get; set; } = null!;
        public string Fechavalida { get; set; } = null!;
        public decimal Monto { get; set; }
        public string Tipomoneda { get; set; } = null!;
        public string? Cvv { get; set; }
    }


    [Serializable]
    public partial class ComprobantePago
    {
        public decimal Idcomprobante { get; set; }
        public string Numerotarjeta { get; set; } = null!;
        public string Nombretitular { get; set; } = null!;
        public string Fechavalida { get; set; } = null!;
        public DateTime Fecharegistro { get; set; }
        public decimal Monto { get; set; }
        public string Tipomoneda { get; set; } = null!;
        public decimal Valorusd { get; set; }
        public decimal Valoruf { get; set; }
        public decimal Valorutm { get; set; }
        public string? Cvv { get; set; }
    }




    [Serializable]
    public class ComprobantePagoResponse
    {
        public decimal? Idcomprobante { get; set; }
        public decimal? MontoPago { get; set; }
        public string? Message { get; set; }
        public DateTime? Fecharegistro { get; set; }
    }


}
