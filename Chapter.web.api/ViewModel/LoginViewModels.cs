using System.ComponentModel.DataAnnotations;

namespace Chapter.web.api.ViewModel
{
    public class LoginViewModels
    {
        [Required(ErrorMessage = "Informe o Email do Usuário")]
        public string email { get; set; }
        [Required(ErrorMessage = "Iforme a Senha do Usuário")]
        public string senha { get; set; }

    }
}
