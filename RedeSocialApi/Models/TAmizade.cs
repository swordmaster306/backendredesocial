using System;
using System.Collections.Generic;

namespace RedeSocialApi.Models
{
    public partial class TAmizade
    {
        public int Id { get; set; }
        public int Usuario1 { get; set; }
        public int Usuario2 { get; set; }
        public string Status { get; set; }

        public TUsuario Usuario1Navigation { get; set; }
        public TUsuario Usuario2Navigation { get; set; }
    }
}
