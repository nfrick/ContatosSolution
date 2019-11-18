using System.ComponentModel.DataAnnotations;

namespace ContatosAPI.Models {
    public class ContatoModel : IEntity {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [MinLength(5, ErrorMessage = "Nome deve ter entre 5 e 20 caracteres")]
        [MaxLength(20, ErrorMessage = "Nome deve ter entre 5 e 20 caracteres")]
        public string Nome { get; set; }

        [MinLength(8, ErrorMessage = "Telefone deve ter entre 8 e 15 caracteres")]
        [MaxLength(15, ErrorMessage = "Telefone deve ter entre 8 e 15 caracteres")]
        [RegularExpression(@"^[(]{0,1}[0-9]{1,2}[)]{0,1}[-s./0-9]*$", ErrorMessage = "Telefone com formato inválido")]
        public string Telefone { get; set; }

        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(/)(((0)[0-9])|((1)[0-2]))$", ErrorMessage = "Aniversário fora do formato dd/mm")]
        public string Aniversario { get; set; }

        public bool Ativo { get; set; }
    }
}