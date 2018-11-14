using System;
using System.Collections.Generic;

namespace RedeSocialApi.Models
{
    public partial class TComentario
    {
        public int Id { get; set; }
        public int HistoriaId { get; set; }
        public int UserId { get; set; }
        public string Mensagem { get; set; }
        public DateTime? Data { get; set; }

        public THistoria Historia { get; set; }
        public TUsuario User { get; set; }
    }
}
