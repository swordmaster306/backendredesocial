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
        [Route("meusamigos/{id}")]
        public IActionResult MeusAmigos(int id){
            var amizades = _context.TAmizade.Where(a => (a.Usuario1 == id || a.Usuario2 == id) && a.Status == "Aprovada");
            List<int> id_amigos = new List<int>();
            foreach(TAmizade t in amizades){
                if(t.Usuario1 == id){
                    id_amigos.Add(t.Usuario2);
                }else{
                    id_amigos.Add(t.Usuario1);
                }
            }
            var amigos = from a in _context.TUsuario where id_amigos.Contains(a.UserId) orderby a.Nome ascending select a;
            return Json(amigos);
        }


        [HttpGet]
        //[Authorize("Bearer")]
        [Route("amizadespendentes/{id}")]
        public IActionResult AmizadesPendentes(int id){
            var amizadesPendentes = _context.TAmizade.Where(a => a.Usuario2 == id && a.Status == "Pendente" );
            return Json(amizadesPendentes);
        }



        [HttpGet]
        //[Authorize("Bearer")]
        [Route("historias/{id}")]
        public ActionResult GetHistorias(int id)
        {
            //Historias do perfil
            IEnumerable<THistoria> historias = _context.THistoria.Where(h => h.UserId == id);
            foreach(THistoria th in historias){
                bool verificador = _context.TLikeDislike.Any(ld => ld.UserId == id && ld.HistoriaId == th.Id);
                if(verificador){
                    TLikeDislike temp = _context.TLikeDislike.Where(ld => ld.UserId == id && ld.HistoriaId == th.Id).First();
                    if(temp.LikeDislike == true){
                        th.deulike = 1;
                    }else{
                        th.deulike = 2;
                    }
                }else{
                    th.deulike = 0;
                }
            }
            return Json(historias);
        }



        [HttpGet]
        //[Authorize("Bearer")]
        [Route("feedprincipal/{id}")]
        public ActionResult FeedPrincipal(int id)
        {
            try{
                var amizades = _context.TAmizade.Where(a => (a.Usuario1 == id || a.Usuario2 == id) && a.Status == "Aprovada");
                List<int> id_amigos = new List<int>();
                id_amigos.Add(id);
                foreach(TAmizade t in amizades){
                    if(t.Usuario1 == id){
                        id_amigos.Add(t.Usuario2);
                    }else{
                        id_amigos.Add(t.Usuario1);
                    }
                }

                var historias_filtradas = from h in _context.THistoria where id_amigos.Contains(h.UserId) orderby h.Data descending select h;            
                return Json(historias_filtradas);
            }catch(Exception){
                return StatusCode(500);
            }
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
