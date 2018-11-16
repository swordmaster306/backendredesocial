using System;
using System.Collections.Generic;

namespace RedeSocialApi.Models
{
    public partial class TUsuario
    {
        public TUsuario()
        {
            TAmizadeUsuario1Navigation = new HashSet<TAmizade>();
            TAmizadeUsuario2Navigation = new HashSet<TAmizade>();
            TComentario = new HashSet<TComentario>();
            THistoria = new HashSet<THistoria>();
            TLikeDislike = new HashSet<TLikeDislike>();
        }

        public int UserId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? QtdAmigos { get; set; }
        public int? QtdHistorias { get; set; }
        public string FotoPerfil { get; set; }

        public ICollection<TAmizade> TAmizadeUsuario1Navigation { get; set; }
        public ICollection<TAmizade> TAmizadeUsuario2Navigation { get; set; }
        public ICollection<TComentario> TComentario { get; set; }
        public ICollection<THistoria> THistoria { get; set; }
        public ICollection<TLikeDislike> TLikeDislike { get; set; }
    }
}
