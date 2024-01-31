using Gestao.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao.Models
{
    //CRIAÇÃO DAS TABELAS SQL

    public class Morador
    {
        [Key]
        public long? MoradorID { get; set; }

        public string Nome { get; set; }

        public string Endereco { get; set; }

        public string NumeroAP { get; set; }

        public string DataNasc { get; set; }

        public string CPF { get; set; }

        public string Telefone { get; set; }




        public IList<Veiculo>? Veiculos { get; set; }

        public IList<Visitante>? Visitantes { get; set; }

        public IList<Reservas>? Reservas { get; set; }


    }
}
