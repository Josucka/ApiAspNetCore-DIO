using ApiAspNetCore.Entity;
using ApiAspNetCore.Exception;
using ApiAspNetCore.InputModel;
using ApiAspNetCore.Repository;
using ApiAspNetCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAspNetCore.Service
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task AtualizarJogo(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if(entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Name = jogo.Name;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(entidadeJogo);

        }

        public async Task AtualizarJogo(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task DeleteJogo(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            
            if(jogo == null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public async Task<JogoViewModel> InsertJogo(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Name, jogo.Produtora);
            if(entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Name = jogo.Name,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoRepository.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Name = jogo.Name,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Name = jogo.Name,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Name = jogo.Name,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
