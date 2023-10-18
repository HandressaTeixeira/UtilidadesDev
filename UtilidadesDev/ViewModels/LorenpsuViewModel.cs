using System.ComponentModel.DataAnnotations;

namespace UtilidadesDev.ViewModels
{
    public class LorenpsuViewModel
    {

        public int MinPalavras = 1;

        [Display(Name = "Quantidade de palavras")]
        [Required(ErrorMessage = "O Quantidade de palavras campo é obrigatório")]
        public int MaxPalavras { get; set; }

        public int MinFrases = 1;

        [Display(Name = "Quantidade de frases")]
        [Required(ErrorMessage = "O campo Quantidade máxima de frases é obrigatório")]
        public int MaxFrases { get; set; }

        [Display(Name = "Quantidade de paragrafos")]
        [Required(ErrorMessage = "O campo Quantidade de paragrafos é obrigatório")]
        public int NumParagrafos { get; set; }

        public string Resultado { get; set; }
    }
}
