using Chapter.web.api.models;
using Chapter.web.api.Repository;
using Chapter.web.api.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Chapter.web.api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public LoginController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModels Login)
        {
            try
            {
                Usuario usuariobuscado = _usuarioRepository.Login(Login.email, Login.senha);
                if(usuariobuscado == null)
                {
                    return NotFound("Email ou Senha invalidos");
                }

                var MinhasClains = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuariobuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuariobuscado.Id.ToString()),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("Charpter-chave-autentication"));

                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var meuToken = new JwtSecurityToken(
                    
                    issuer: "Chapter.web.api",
                    
                    audience: "Chapter.web.api",
                    
                    claims: MinhasClains,
                    
                    expires: DateTime.Now.AddMinutes(60),
                    
                    signingCredentials: cred);

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken),
                    }
                    );

            }
            catch (System.Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
