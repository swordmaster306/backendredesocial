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

namespace RedeSocialApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AddRemoveController : Controller
    {
        private readonly redesocialdbContext _context;
        
        public AddRemoveController(redesocialdbContext context)
        {
            _context = context;
        }

        [HttpPost]
        //[Authorize("Bearer")]
        [Route("adicionaramigo")]
        public IActionResult AdicionarAmigo(int usuario1_id,int usuario2_id){
            try{
                TAmizade amizadeAntiga = _context.TAmizade.Where(a => (a.Usuario1 == usuario1_id && a.Usuario2 == usuario2_id) || (a.Usuario1 == usuario2_id && a.Usuario2 ==usuario1_id)).First();
                if(amizadeAntiga == null){
                    TAmizade amizade = new TAmizade();
                    amizade.Usuario1 = usuario1_id;
                    amizade.Usuario2 = usuario2_id;
                    amizade.Status = "Pendente";
                    _context.TAmizade.Add(amizade);
                    _context.SaveChanges();
                }else{
                    amizadeAntiga.Status = "Pendente";
                    _context.SaveChanges();
                }
                return StatusCode(200);
            }catch(Exception){
                return StatusCode(500);
            }
        }

        [HttpPost]
        //[Authorize("Bearer")]
        [Route("deletaramigo")]
        public IActionResult DeletarAmigo(int usuario1_id,int usuario2_id){
            try{
                TAmizade amizade = _context.TAmizade.Where(a => 
                                                ((a.Usuario1 == usuario1_id && a.Usuario2 == usuario2_id) 
                                                || (a.Usuario1 == usuario2_id && a.Usuario2 ==usuario1_id)) 
                                                && a.Status == "Aprovada" ).First();
                if(amizade !=null){
                    amizade.Status = "Desfeita";
                    _context.SaveChanges();
                    return StatusCode(200);
                }else{
                    return StatusCode(501);
                }
            }catch(Exception){
                return StatusCode(500);
            }
        }


        [HttpPost]
        //[Authorize("Bearer")]
        [Route("aceitaramizade")]
        public IActionResult AceitarAmizade(int usuario1_id,int usuario2_id){
            try{
                TAmizade amizade = _context.TAmizade.Where(a => 
                                                ((a.Usuario1 == usuario1_id && a.Usuario2 == usuario2_id) 
                                                || (a.Usuario1 == usuario2_id && a.Usuario2 ==usuario1_id)) 
                                                && a.Status == "Pendente" ).First();
                if(amizade !=null){
                    amizade.Status = "Aprovada";
                    _context.SaveChanges();
                    return StatusCode(200);
                }else{
                    return StatusCode(501);
                }
            }catch(Exception){
                return StatusCode(500);
            }
        }


        [HttpPost]
        //[Authorize("Bearer")]
        [Route("rejeitaramizade")]
        public IActionResult RejeitarAmizade(int usuario1_id,int usuario2_id){
            try{
                TAmizade amizade = _context.TAmizade.Where(a => 
                                                ((a.Usuario1 == usuario1_id && a.Usuario2 == usuario2_id) 
                                                || (a.Usuario1 == usuario2_id && a.Usuario2 ==usuario1_id)) 
                                                && a.Status == "Pendente" ).First();
                if(amizade !=null){
                    amizade.Status = "Rejeitada";
                    _context.SaveChanges();
                    return StatusCode(200);
                }else{
                    return StatusCode(501);
                }
            }catch(Exception){
                return StatusCode(500);
            }
        }

        [HttpPost]
        //[Authorize("Bearer")]
        [Route("criarhistoria")]
        public IActionResult criarhistoria(THistoria historia){
            try{
                _context.THistoria.Add(historia);
                _context.SaveChanges();
                return StatusCode(200);
            }catch(Exception){
                return StatusCode(500);
            }
        }  


        [HttpPost]
        //[Authorize("Bearer")]
        [Route("deletarhistoria")]
        public IActionResult DeletarHistoria(THistoria historia){
            try{
                _context.THistoria.Remove(historia);
                _context.SaveChanges();
                return StatusCode(200);
            }catch(Exception){
                return StatusCode(500);
            }
        }      

        [HttpPost]
        //[Authorize("Bearer")]
        [Route("criarcomentario")]
        public IActionResult CriarComentario(TComentario comentario){
            try{
                _context.TComentario.Add(comentario);
                _context.SaveChanges();
                return StatusCode(200);
            }catch(Exception){
                return StatusCode(500);
            }
        }      

        [HttpPost]
        //[Authorize("Bearer")]
        [Route("deletarcomentario")]
        public IActionResult DeletarComentario(TComentario comentario){
            try{
                _context.TComentario.Remove(comentario);
                _context.SaveChanges();
                return StatusCode(200);
            }catch(Exception){
                return StatusCode(500);
            }
        }      

        [HttpPost]
        //[Authorize("Bearer")]
        [Route("like")]
        public IActionResult Like(TLikeDislike like_dislike){
            try{
                TLikeDislike existente = _context.TLikeDislike.Where(ld => ld.UserId == like_dislike.UserId && ld.HistoriaId == like_dislike.HistoriaId).First();
                if(existente == null){
                    _context.TLikeDislike.Add(like_dislike);
                    _context.SaveChanges();
                }else{
                    existente.LikeDislike = true;
                    _context.SaveChanges();
                }
                return StatusCode(200);
            }catch(Exception){
                return StatusCode(500);
            }
        }      

        [HttpPost]
        //[Authorize("Bearer")]
        [Route("dislike")]
        public IActionResult Dislike(TLikeDislike like_dislike){
            try{
                TLikeDislike existente = _context.TLikeDislike.Where(ld => ld.UserId == like_dislike.UserId && ld.HistoriaId == like_dislike.HistoriaId).First();
                if(existente == null){
                    _context.TLikeDislike.Add(like_dislike);
                    _context.SaveChanges();
                }else{
                    existente.LikeDislike = false;
                    _context.SaveChanges();
                }
                return StatusCode(200);
            }catch(Exception){
                return StatusCode(500);
            }
        }     
    

        
        
    }
}