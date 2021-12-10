using System.ComponentModel.DataAnnotations;

namespace WeChip.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public decimal Credito { get; set; }
        [Required]
        public string Telefone { get; set; }
        public Status Status { get; set; }
    }
}
