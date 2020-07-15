using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kafka.Producer.API.Domain.DTO
{
    public class AtendimentoDto
    {
        public string NomeAtendente { get; set; }
        public DateTime DataHora { get; set; }
        public string NumeroCompra { get; set; }
        public string ContextoAtendimento { get; set; }
        public string ProtocoloAtendimento { get; set; }

    }
}
