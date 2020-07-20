using System.ComponentModel.DataAnnotations.Schema;

namespace ChatBoot.Models {
    [Table("RespostasChat")]
    public class RespostaChat {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public string Resposta { get; set; }
    }
}
