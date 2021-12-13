using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WeChip.Models
{
    public class Oferta
    {
        public int ID { get; set; }
        public Cliente Cliente { get; set; }
        public List<Produto> Produtos { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
