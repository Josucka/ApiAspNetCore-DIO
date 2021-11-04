using System;
using System.ComponentModel.DataAnnotations;

namespace ApiAspNetCore.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome do jogo deve conter entre 3 e 100 caracters")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora do jogo deve conter entre 3 e 100 caracters")]
        public string Produtora { get; set; }
        
        [Required]
        [Range(1, 1000, ErrorMessage = "O Preço do jogo deve ser entre 1 e 1000 R$")]
        public double Preco { get; set; }
    }
}
