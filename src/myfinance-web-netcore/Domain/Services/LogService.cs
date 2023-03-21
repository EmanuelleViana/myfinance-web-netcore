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

        public void Salvar(LogExclusaoModel logExclusaoModel)
        {
            var dbSet = _dbContext.LogExclusao;

            var entidate = new LogExclusao()
            {
                Id = logExclusaoModel.Id,
                Data = logExclusaoModel.Data,
                Observacao = logExclusaoModel.Observacao,
                Tabela = logExclusaoModel.Tabela,
                IdRegistro = logExclusaoModel.IdRegistro,
                Operacao = logExclusaoModel.Operacao

            };

            dbSet.Add(entidate);

            _dbContext.SaveChanges();
        }

    }
}