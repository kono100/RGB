using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao.Models

//Criação das tabelas SQL 

{
    public class Visitante
    {
        [Key]
        public long? VisitanteID { get; set; }

        public string Nome { get; set; }

        public string DataNasc { get; set; }

        public string EndDate { get; set; }

        public string CPF { get; set; }

        public string? Observacao { get; set; }



        [ForeignKey("Morador")]
        public long? fk_MoradorID { get; set; }
        public Morador? Morador { get; set; }
    }
}
    