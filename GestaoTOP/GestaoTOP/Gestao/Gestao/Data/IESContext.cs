using Gestao.Models;
using Gestao.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data
{
    //CLASSE IESContext HERDA O DBCONTEXT DO EFCORE

    public class IESContext : DbContext
    {
        //METODO CONSTRUTOR PARA GERENCIAMENTO DE DEPENDENCIA; PREPARA O CONTEXTO DO BANCO DE DADOS
        //PARA QUE AS CLASSES ABAIXO SEJAM TRATADAS PARA QUE SE TORNEM UMA TABELA NO BANCO

        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {

        }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Morador> Morador { get; set; }
        public DbSet<Visitante> Visitante { get; set; }

        public DbSet<Reservas> Reservas { get; set; }


    }
}
