using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myfinance_web_netcore.Models
{
    public class LogsModel
    {
        public int? Id { get; set; }
        public string? Observacao { get; set; }
        public string Tabela { get; set; }
        public int IdRegistro { get; set; }
        public string Operacao { get; set; }
        public DateTime Data { get; set; }
    }
}