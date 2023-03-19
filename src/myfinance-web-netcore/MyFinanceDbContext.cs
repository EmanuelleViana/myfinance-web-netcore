using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain.Entities;

namespace myfinance_web_netcore
{
    public class MyFinanceDbContext : DbContext
    {
        // mapea entidades como tabelas do banco
        public DbSet<PlanoConta> PlanoConta { get; set; }
        public DbSet<Transacao> Transacao { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // configurar banco de dados
            // var connectionString = @"Server=127.0.0.1,1433;Database=myfinance;User Id=sa;Password=Sql.123456;TrustServerCertificate=True";
            var connectionString = @"Server=.\SQLEXPRESS01;Database=myfinance;Trusted_Connection=True;TrustServerCertificate=True";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

        }
    }
}