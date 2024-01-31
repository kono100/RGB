using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gestao.Models

//Criação das tabelas SQL 

{
    public class Reservas
    {
        [Key]
        public long? ReservasID { get; set; }        

        public string Nome { get; set; }  //AreaReservada

        public DateTime DataHoraRes { get; set; }

        public string DuracaoReserva { get; set; }
        public string Status { get; set; }

        [ForeignKey("Morador")]
        public long? fk_MoradorID { get; set; }
        public Morador? Morador { get; set; }
    }
}
