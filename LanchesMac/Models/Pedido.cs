using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LanchesMac.Resources;

namespace LanchesMac.Models
{
    public class Pedido
    {
        public int PedidoId { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [MaxLength(50, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [MaxLength(50, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        public string Sobrenome { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [MaxLength(100, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        [Display(Name = "Endereço")]
        public string Endereco1 { get; set; }

        [MaxLength(100, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        [Display(Name = "Complemento")]
        public string? Endereco2 { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [Display(Name = "CEP")]
        [MaxLength(10, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        public string Cep { get; set; }

        [MaxLength(20, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        public string Estado { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [MaxLength(50, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        public string Cidade { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [MaxLength(25, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        [Required(ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.Required))]
        [MaxLength(50, ErrorMessageResourceType = typeof(TextosValidacao), ErrorMessageResourceName = nameof(TextosValidacao.MaxLength))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Total do Pedido")]
        public decimal PedidoTotal { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Itens no Pedido")]
        public int TotalItensPedido { get; set; }

        [Display(Name = "Data do Pedido")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime PedidoEnviado { get; set; }

        [Display(Name = "Data Envio Pedido")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime? PedidoEntregueEm { get; set; }

        public List<PedidoDetalhe>? PedidoItens { get; set; }
    }
}
