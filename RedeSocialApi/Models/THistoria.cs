using System;
using System.Collections.Generic;

namespace RedeSocialApi.Models
{
    public partial class THistoria
    {
        public THistoria()
        {
            TComentario = new HashSet<TComentario>();
            TLikeDislike = new HashSet<TLikeDislike>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Mensagem { get; set; }
        public string Foto { get; set; }
        public int? Likes { get; set; }
        public int? Dislikes { get; set; }
        public int? QtdComentarios { get; set; }
        public DateTime? Data { get; set; }

        public TUsuario User { get; set; }
        public ICollection<TComentario> TComentario { get; set; }
        public ICollection<TLikeDislike> TLikeDislike { get; set; }
    }
}
