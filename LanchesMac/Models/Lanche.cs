using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LanchesMac.Resources;

namespace LanchesMac.Models;

public class Lanche
{
    public int LancheId { get; set; }

    [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
    [Display(Name = "Nome do Lanche")]
    [MaxLength(80, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
    public string Nome { get; set; }

    [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
    [Display(Name = "Descrição do Lanche")]
    [MaxLength(200, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
    public string DescricaoCurta { get; set; }

    [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
    [Display(Name = "Descrição detalhada do Lanche")]
    [MaxLength(200, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
    public string DescricaoDetalhada { get; set; }

    [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
    [Display(Name = "Preço")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Display(Name = "Imagem Normal")]
    [MaxLength(200, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
    public string? ImagemUrl { get; set; }

    [Display(Name = "Imagem Miniatura")]
    [MaxLength(200, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
    public string? ImagemThumbnailUrl { get; set; }

    [Display(Name = "Preferido?")]
    public bool IsLanchePreferido { get; set; }

    [Display(Name = "Estoque")]
    public bool EmEstoque { get; set; }

    public int CategoriaId { get; set; }
    public virtual Categoria? Categoria { get; set; }

}
