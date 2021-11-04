using ApiAspNetCore.InputModel;
using ApiAspNetCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAspNetCore.Service
{
    public interface IJogoService
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);

        Task<JogoViewModel> Obter(Guid id);
        
        Task<JogoViewModel> InsertJogo(JogoInputModel jogo);
        
        Task AtualizarJogo(Guid id, JogoInputModel jogo);
        
        Task AtualizarJogo(Guid id, double preco);
        
        Task DeleteJogo(Guid id);
    }
}
