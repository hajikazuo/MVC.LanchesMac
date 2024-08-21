using LanchesMac.Resources;
using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models;

public class Categoria
{
    public int CategoriaId { get; set; }

    [MaxLength(100, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
    [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
    [Display(Name = "Nome")]
    public string CategoriaNome { get; set; }

    [MaxLength(200, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
    [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; }

    public List<Lanche>? Lanches { get; set; }
}
