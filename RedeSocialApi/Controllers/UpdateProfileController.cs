using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RedeSocialApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedeSocialApi.Controllers
{
    [Route("api/[controller]")]
    public class UpdateProfileController : Controller
    {
        [HttpGet]
        [Authorize("Bearer")]
        public IActionResult CreateImage(TUsuario usuario)
        {

            return Json("Entrou autenticado!");
            //Console.WriteLine("Creating file....");
            //byte[] imageBytes = Convert.FromBase64String(item.Image);
            //System.IO.File.WriteAllBytes("./Images/imagem.jpg", imageBytes);
            //return Ok();
        }
    }
}
