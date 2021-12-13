using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeChip.Models
{
    public class UsuarioApi
    {
        public int ID { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public static UsuarioApi Get(string username, string password)
        {
            var users = new List<UsuarioApi>
            {
                new UsuarioApi { ID = 1, Usuario = "Test", Senha = "#Test!" }
            };

            return users.Where(x => x.Usuario.ToLower() == username.ToLower() && x.Senha == x.Senha).FirstOrDefault();
        }
    }
}
