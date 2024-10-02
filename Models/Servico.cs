using System.ComponentModel.DataAnnotations;
namespace PrestadorOnline.Models
{
    public class Servico
    {
        [Key]
        public int servicoId { get; set; }
        [StringLength(100)]
        public string nome { get; set; }
        
    }
}
