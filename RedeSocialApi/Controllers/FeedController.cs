using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RedeSocialApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedeSocialApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : Controller
    {
        private readonly redesocialdbContext _context;

        public FeedController(redesocialdbContext context)
        {
            _context = context;
        }

        [HttpGet]
        //[Authorize("Bearer")]
        [Route("buscaramigos")]
        public ActionResult BuscarAmigos(TUsuario usuario)
        {
            var resultado = from d in _context.TUsuario where d.Nome.ToUpper().Contains(usuario.Nome) select new { d.UserId, d.Nome, d.FotoPerfil };
            return Json(resultado);
        }

        [HttpGet]
        //[Authorize("Bearer")]
        [Route("historias/{id}")]
        public ActionResult GetHistorias(int id)
        {
            //Historias do perfil
            IEnumerable<THistoria> historias = _context.THistoria.Where(h => h.UserId == id);
            return Json(historias);
        }


        [HttpGet]
        //[Authorize("Bearer")]
        [Route("comentarios/{id}")]
        public ActionResult GetComentarios(int id)
        {

            //Comentarios atrelados a uma historia
            IEnumerable<TComentario> comentarios = _context.TComentario.Where(c => c.HistoriaId == id);
            return Json(comentarios);
        }
    }
}
