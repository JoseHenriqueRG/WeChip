using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeChip.Models;
using WeChip.Services;

namespace WeChip.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<dynamic>> Login([FromBody] UsuarioApi usuarioApi)
        {
            // Recupera o usuário
            var usuario = UsuarioApi.Get(usuarioApi.Usuario, usuarioApi.Senha);

            // Verifica se o usuário existe
            if (usuario == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(usuario);

            // Oculta a senha
            usuario.Senha = "";

            // Retorna os dados
            return new
            {
                usuario = usuario,
                token = token
            };
        }
    }
}
