using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace myfinance_web_netcore.Models
{
    public class TransacaoModel
    {
        public int? Id { get; set; }
        public string? Historico { get; set; }
        public DateTime Data { get; set; }
        public Decimal Valor { get; set; }

        public int PlanoContaId { get; set; }
        public PlanoContaModel ItemPlanoConta { get; set; }

        public IEnumerable<SelectListItem>? PlanoContas { get; set; }

    }
}