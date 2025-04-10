using Microsoft.AspNetCore.Mvc;
using DHCPManagerAPI.Services;
using System.Collections.Generic;

namespace DHCPManagerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AuthService _authService;

        public UserController(AuthService authService)
        {
            _authService = authService;
        }

        // Exemplo de endpoint para cadastrar usuário
        [HttpPost("register")]
        public IActionResult Register(string username, string password)
        {
            var hashedPassword = AuthService.HashPassword(password);
            // Aqui você pode salvar o usuário no banco de dados
            return Ok(new { message = "Usuário registrado com sucesso!", hashedPassword });
        }

        // Exemplo de endpoint para login
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // Aqui você buscaria o usuário do banco e verificaria a senha
            string storedHashedPassword = "senha_hash_ficticia"; // Simulação

            if (!AuthService.VerifyPassword(password, storedHashedPassword))
                return Unauthorized(new { message = "Credenciais inválidas!" });

            var token = _authService.GenerateJwtToken(username);
            return Ok(new { token });
        }
    }
}
