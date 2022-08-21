using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;

namespace PagOnlineAPI.Models
{
    public partial class Tarjeta
    {
        public Tarjeta()
        {
            Pagos = new HashSet<Pago>();
        }

        public decimal Id { get; set; }
        public string Numero { get; set; } = null!;
        public decimal Pin { get; set; }
        public string FechaValida { get; set; } = null!;

        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
