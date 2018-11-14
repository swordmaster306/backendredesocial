using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedeSocialApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;
using RedeSocialApi.Token;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedeSocialApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly redesocialdbContext _context;

        public AuthController(redesocialdbContext context)
        {
            _context = context;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("cadastro")]
        public ActionResult Registrar(TUsuario usuario)
        {
            bool usuarioExistente = _context.TUsuario.Any(o => o.Email == usuario.Email);

            if (!usuarioExistente)
            {
                _context.TUsuario.Add(usuario);
                _context.SaveChanges();
                return Ok();
            }
            return StatusCode(409);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("autenticar")]
        public Object Autenticar(TUsuario usuario,
            [FromServices] SigningConfigurations SigningConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {

            TUsuario user;

            try
            {
                user = _context.TUsuario.Single(u => u.Email == usuario.Email);
            }
            catch (InvalidOperationException)
            {
                user = null;
            }
            bool credenciaisValidas = false;

            if(usuario != null && !String.IsNullOrWhiteSpace(usuario.Email))
            {
                credenciaisValidas = (user != null &&
                    user.Senha == usuario.Senha);
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Email, "Login"),
                    new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Email)
                    }
                );


                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = SigningConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });

                var token = handler.WriteToken(securityToken);

                return new
                {
                    authenticated = true,
                    created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                    expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                    accessToken = token,
                    message = "Ok"
                };
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha na autenticação"
                };
            }
        }


    }
}
