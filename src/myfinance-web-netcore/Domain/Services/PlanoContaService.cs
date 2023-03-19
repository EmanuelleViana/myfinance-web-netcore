using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain.Entities;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Domain.Services.Interfaces
{
    public class PlanoContaService : IPlanoContaService
    {
        private readonly MyFinanceDbContext _dbContext;
        public PlanoContaService(MyFinanceDbContext dbContext)
        {

            _dbContext = dbContext;
        }

        public List<PlanoContaModel> ListarRegistros()
        {
            var dbSet = _dbContext.PlanoConta;
            var result = new List<PlanoContaModel>();

            foreach (var item in dbSet)
            {

                result.Add(
                    new PlanoContaModel()
                    {
                        Id = item.Id,
                        Descricao = item.Descricao,
                        Tipo = item.Tipo
                    });

            }

            return result;
        }

        public void Salvar(PlanoContaModel planoContaModel)
        {
            var dbSet = _dbContext.PlanoConta;

            var entidate = new PlanoConta()
            {
                Id = planoContaModel.Id,
                Descricao = planoContaModel.Descricao,
                Tipo = planoContaModel.Tipo
            };


            if (entidate.Id == null)
            {
                dbSet.Add(entidate);//salvar
            }
            else
            {
                dbSet.Attach(entidate); //editar 
                _dbContext.Entry(entidate).State = EntityState.Modified;

            }

            _dbContext.SaveChanges(); //commitar alterações no banco
        }

        public PlanoContaModel RetornaRegistro(int id)
        {
            var item = _dbContext.PlanoConta.Where(x => x.Id == id).First();

            return new PlanoContaModel()
            {
                Id = item.Id,
                Descricao = item.Descricao,
                Tipo = item.Tipo
            };

        }

        public void Excluir(int id)
        {
            var dbSet = _dbContext.PlanoConta;

            var item = _dbContext.PlanoConta.Where(x => x.Id == id).First();
            _dbContext.Attach(item);
            _dbContext.Remove(item);
            _dbContext.SaveChanges();

        }
    }
}