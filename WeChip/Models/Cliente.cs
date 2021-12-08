namespace WeChip.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public decimal Credito { get; set; }
        public string Telefone { get; set; }
        public Status Status { get; set; }
    }
}
