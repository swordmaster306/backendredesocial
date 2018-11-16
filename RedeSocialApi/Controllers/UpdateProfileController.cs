using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RedeSocialApi.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedeSocialApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : Controller
    {

        private readonly redesocialdbContext _context;

        public ProfileController(redesocialdbContext context)
        {
            _context = context;
        }


        [HttpPut]
        [Authorize("Bearer")]
        [Route("editar")]
        public ActionResult EditarPerfil(TUsuario usuario)
        {
            var user = _context.TUsuario.Find(usuario.UserId);
            if (usuario.Nome != null)
                user.Nome = usuario.Nome;
            if (usuario.Senha != null)
                user.Senha = usuario.Senha;
            if (usuario.FotoPerfil != null)
                user.FotoPerfil = usuario.FotoPerfil;
            if (usuario.Email != null)
                user.Email = usuario.Email;
            _context.SaveChanges();
            return Json(user);
        }
    }
}
