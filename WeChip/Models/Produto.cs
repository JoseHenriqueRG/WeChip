using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WeChip.Models
{
    public class Produto
    {
        [Key]
        public int CodigoProduto { get; set; }
        public string Descricao { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
        public Tipo Tipo { get; set; }

        public virtual ICollection<OfertaProdutos> OfertaProdutos { get; set; }
    }

    public enum Tipo{
        Hardware,
        Software
    }
}
