using System.ComponentModel.DataAnnotations;

namespace PrestadorOnline.Models
{
    public class Prestador
    {
        [Key]
        public int prestadorId { get; set; }

        [Required]
        [StringLength(100)]
        public string nome { get; set; }
        public string email { get; set; }
        public string telefone { get; set; }
        [Required]
        public string especialidade { get; set; }
        [Required]
        public string servico { get; set; }
        public string descricaoServico { get; set; }
        [Required]
        public decimal precoServico { get; set; }

    }
}
