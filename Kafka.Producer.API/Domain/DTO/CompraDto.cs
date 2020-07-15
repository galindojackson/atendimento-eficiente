using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kafka.Producer.API.Domain.DTO
{
    public class CompraDto
    {

        public string NomeCliente { get; set; }

        public string IdentificacaoCliente { get; set; }

        public string NumeroCompra { get; set; }

        public DateTime DataCompra { get; set; }

        public string StatusCompra { get; set; }

    }
}
