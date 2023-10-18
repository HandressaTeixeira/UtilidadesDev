using System.ComponentModel.DataAnnotations;

namespace UtilidadesDev.ViewModels
{
    public class QRCodeViewModel
    {
        [Display(Name = "Tamanho em pixels")]
        [Required(ErrorMessage = "O campo Tamanho em pixels é obrigatório")]
        public int? Tamanho { get; set; }

        [Display(Name = "Texto")]
        [Required(ErrorMessage = "O campo Texto é obrigatório")]
        public string Texto { get; set; }

        public byte[] Imagem { get; set; }
    }
}
