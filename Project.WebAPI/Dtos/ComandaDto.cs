using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.WebAPI.Dtos
{
    public class ComandaDto
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int idUsuario { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome deve possuir no mínimo 3 caracteres")]
        public string nomeUsuario { get; set; }

        [Phone]
        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(11, MinimumLength = 10, ErrorMessage = "O Telefone está incorreto")]
        public string telefoneUsuario { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public List<ProdutoDto> Produtos { get; set; }
    }
}
