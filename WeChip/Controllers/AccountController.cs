using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChip.Data;
using WeChip.Models;
using WeChip.Services;

namespace WeChip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly WeChipDataBaseContext _context;

        public AccountController(WeChipDataBaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<dynamic> Login([FromBody] Usuario usuario)
        {
            // Recuperar o usuário
            var user = _context.Usuario.FirstOrDefault(u => u.Login == usuario.Login && u.Senha == usuario.Senha);

            // Verificar se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gerar o Token
            var token = TokenService.GenerateToken(user);

            // Retornar o token
            return new
            {
                token = token
            };
        }
    }
}
