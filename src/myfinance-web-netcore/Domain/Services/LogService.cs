using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain.Entities;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain.Services.Interfaces
{
    public class LogService : ILogService
    {
        private readonly MyFinanceDbContext _dbContext;
        public LogService(MyFinanceDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public void Salvar(LogsModel logsModel)
        {
            var dbSet = _dbContext.Logs;

            var entidate = new Logs()
            {
                Id = logsModel.Id,
                Data = logsModel.Data,
                Observacao = logsModel.Observacao,
                Tabela = logsModel.Tabela,
                IdRegistro = logsModel.IdRegistro,
                Operacao = logsModel.Operacao

            };

            dbSet.Add(entidate);

            _dbContext.SaveChanges();
        }

    }
}