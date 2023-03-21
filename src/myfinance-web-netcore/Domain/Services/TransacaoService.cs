using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain.Entities;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain.Services.Interfaces
{
    public class TransacaoService : ITransacaoService
    {
        private readonly MyFinanceDbContext _dbContext;
        public TransacaoService(MyFinanceDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public List<TransacaoModel> ListarRegistros()
        {
            var dbSet = _dbContext.Transacao.Include(x => x.PlanoConta);
            var result = new List<TransacaoModel>();

            foreach (var item in dbSet)
            {

                result.Add(
                    new TransacaoModel()
                    {
                        Id = item.Id,
                        Data = item.Data,
                        Historico = item.Historico,
                        Valor = item.Valor,
                        ItemPlanoConta = new PlanoContaModel()
                        {
                            Id = item.PlanoConta.Id,
                            Descricao = item.PlanoConta.Descricao,
                            Tipo = item.PlanoConta.Tipo
                        },
                        PlanoContaId = item.PlanoContaId
                    });

            }

            return result;
        }


        public TransacaoModel RetornaRegistro(int id)
        {
            var item = _dbContext.Transacao.Where(x => x.Id == id).First();

            return new TransacaoModel()
            {
                Id = item.Id,
                Data = item.Data,
                Historico = item.Historico,
                Valor = item.Valor,
                PlanoContaId = item.PlanoContaId,
            };

        }

        public void Salvar(TransacaoModel transacaoModel)
        {
            var dbSet = _dbContext.Transacao;

            var entidate = new Transacao()
            {
                Id = transacaoModel.Id,
                Data = transacaoModel.Data,
                Historico = transacaoModel.Historico,
                Valor = transacaoModel.Valor,
                PlanoContaId = transacaoModel.PlanoContaId
            };


            if (entidate.Id == null)
            {
                dbSet.Add(entidate);
            }
            else
            {
                dbSet.Attach(entidate);
                _dbContext.Entry(entidate).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();
        }

        public void Excluir(int id)
        {
            var dbSet = _dbContext.Transacao;

            var item = _dbContext.Transacao.Where(x => x.Id == id).First();
            _dbContext.Attach(item);
            _dbContext.Remove(item);
            _dbContext.SaveChanges();
        }
    }
}