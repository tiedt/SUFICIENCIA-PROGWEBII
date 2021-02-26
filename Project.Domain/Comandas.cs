using System.Collections.Generic;

namespace Project.Domain
{
    public class Comandas
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int idUsuario { get; set; }
        public string nomeUsuario { get; set; }
        public string telefoneUsuario { get; set; }
        public List<Produto> Produtos { get; set; }
    }
}
