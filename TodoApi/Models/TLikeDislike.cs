using System;
using System.Collections.Generic;

namespace RedeSocialApi.Models
{
    public partial class TLikeDislike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HistoriaId { get; set; }
        public bool? LikeDislike { get; set; }

        public THistoria Historia { get; set; }
        public TUsuario User { get; set; }
    }
}
