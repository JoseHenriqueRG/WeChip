using System.ComponentModel.DataAnnotations;

namespace WeChip.Models
{
    public class Status
    {
        [Key]
        public int CodigoStatus { get; set; }
        public string Descricao { get; set; }
        public bool FinalizaCliente { get; set; }
        public bool ContabilizaVenda { get; set; }
    }
}
