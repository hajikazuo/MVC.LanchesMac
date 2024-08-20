using LanchesMac.Resources;
using System.ComponentModel.DataAnnotations;

namespace LanchesMac.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [Display(Name = "Login")]
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação devem ser iguais.")]
        public string ConfirmPassword { get; set; }
    }
}
