using System.ComponentModel.DataAnnotations;

namespace Project.WebAPI.Dtos
{
    public class ProdutoDto
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        public int id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O Nome do produto deve possuir no mínimo 3 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        public int preco { get; set; }
    }
}