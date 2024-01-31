using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao.Models

//Criação das tabelas SQL 

{
    public class Veiculo
    {
        [Key]
        public long? VeiculoID { get; set; }

        public string Placa { get; set; }

        public string Marca { get; set; }

        public string Modelo { get; set; }

        public string Cor { get; set; }




        [ForeignKey("Morador")]
        public long? fk_MoradorID { get; set; }
        public Morador? Morador { get; set; }
        public long? VisitanteID { get; set; }
    }
}
