using ApiAspNetCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAspNetCore.Repository
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            [Guid.Parse("string"), new Jogo(Id = Guid.Parse("string"), Name = "string", Produtora = "string", Preco = 1)],
            [Guid.Parse("string"), new Jogo(Id = Guid.Parse("string"), Name = "string", Produtora = "string", Preco = 1)],
            [Guid.Parse("string"), new Jogo(Id = Guid.Parse("string"), Name = "string", Produtora = "string", Preco = 1)],
            [Guid.Parse("string"), new Jogo(Id = Guid.Parse("string"), Name = "string", Produtora = "string", Preco = 1)]
        };

        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }

        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;

            return Task.FromResult(jogos[id]);
        }

        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => 
                jogo.Name.Equals(nome) && 
                jogo.Produtora.Equals(produtora)).ToList());
        }

        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;
        }
    }
}
