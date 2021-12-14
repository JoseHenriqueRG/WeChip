using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeChip.Models
{
    public class OfertaProdutos
    {
        public int OfertaID { get; set; }
        public int ProdutoID { get; set; }

        public Oferta Oferta { get; set; }
        public Produto Produto { get; set; }
    }
}
