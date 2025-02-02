﻿using System.ComponentModel.DataAnnotations;

namespace apiFornecedor.Models
{
    public class UserRegisterModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [EmailAddress(ErrorMessage ="O campo {0} está em formato invalido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres",MinimumLength =6)]
        public string Senha { get; set; }
        [Compare("Senha",ErrorMessage = "As senhas não conferem")]
        public required string ConfirmarSenha { get; set; }
    }
}
