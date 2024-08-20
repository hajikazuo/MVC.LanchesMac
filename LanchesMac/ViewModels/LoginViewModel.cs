using LanchesMac.Resources;
using System.ComponentModel.DataAnnotations;

namespace LanchesMac.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [Display(Name = "Usuário")]
        public string UserName { get; set; }
        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
