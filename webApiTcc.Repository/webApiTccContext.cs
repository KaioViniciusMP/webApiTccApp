using CriandoApi8ParaTestar.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApiTcc.Repository.Models;

namespace webApiTcc.Repository
{
    public class webApiTccContext : DbContext
    {
        public webApiTccContext(DbContextOptions<webApiTccContext> options) : base(options) { }

        public DbSet<TabCartao> tabCartao { get; set; }
        public DbSet<TabContaCorrente> tabContaCorrente { get; set; }
        public DbSet<TabHistoricoTransacao> tabHistoricoTransacao { get; set; }
        public DbSet<TabModalidade> tabModalidade { get; set; }
        public DbSet<TabUsuario> tabUsuario { get; set; }
        public DbSet<tabLogExcecao> tabLogExcecao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TabCartao>().ToTable("TabCartao");
            modelBuilder.Entity<TabContaCorrente>().ToTable("TabContaCorrente");
            modelBuilder.Entity<TabHistoricoTransacao>().ToTable("TabHistoricoTransacao");
            modelBuilder.Entity<TabModalidade>().ToTable("TabModalidade");
            modelBuilder.Entity<TabUsuario>().ToTable("TabUsuario");
            modelBuilder.Entity<tabLogExcecao>().ToTable("tabLogExcecao");
        }
    }
}
