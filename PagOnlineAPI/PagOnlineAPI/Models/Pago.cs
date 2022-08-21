using System;
using System.Collections.Generic;

namespace PagOnlineAPI.Models
{
    public partial class Pago
    {
        public decimal Idpago { get; set; }
        public decimal Idtarjeta { get; set; }
        public string? Detallepago { get; set; }
        public DateTime Fechapago { get; set; }

        public virtual Models.Tarjeta IdtarjetaNavigation { get; set; } = null!;
    }
}
