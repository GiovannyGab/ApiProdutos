using System.ComponentModel.DataAnnotations;

namespace apiFornecedor.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatorio")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O Campo{0} é obrigatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser mair que zero")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O Campo{0} é obrigatorio")]
        public int QuantidadeEmEstoque { get; set; }

        [Required(ErrorMessage = "O Campo{0} é obrigatorio")]
        [MaxLength(500,ErrorMessage ="O campo {0} não pode ultrapassar 500 caracteres!")]
        public string? Descricao { get; set; }
    }
}
