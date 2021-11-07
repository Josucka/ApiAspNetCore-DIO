using ApiAspNetCore.Entity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAspNetCore.Repository
{
    public class JogoSqlServiceRepositoty : IJogoRepository
    {
        private readonly SqlConnection _sqlConection;

        public JogoSqlServiceRepositoty(IConfiguration configuration)
        {
            _sqlConection = new SqlConnection(configuration.GetConnectionString("Defaut"));

        }
        public async Task Atualizar(Jogo jogo)
        {
            var comando = $"update Jogos set Name = '{jogo.Name}', Produtora = '{jogo.Produtora}', Preço = '{jogo.Preco.ToString().Replace(".", ",")}, where Id = '{jogo.Id}'";

            await _sqlConection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConection);
            sqlCommand.ExecuteNonQuery();
            await _sqlConection.CloseAsync();
        }

        public void Dispose()
        {
            _sqlConection?.Close();
            _sqlConection?.Dispose();
        }

        public async Task Inserir(Jogo jogo)
        {
            var comando = $"insert Jogos (Id, Name, Produtora, Preco) values ('{jogo.Id}', '{jogo.Name}', '{jogo.Produtora}', {jogo.Preco}', '{jogo.Preco.ToString().Replace(".", ",")})";

            await _sqlConection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConection);
            sqlCommand.ExecuteNonQuery();
            await _sqlConection.CloseAsync();
        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} ...";

            await _sqlConection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preço"]
                });
            }
            await _sqlConection.CloseAsync();

            return jogos;
        }

        public async Task<Jogo> Obter(Guid id)
        {
            Jogo jogo = null;

            var comando = $"select * from Jogos where Id - '{id}'";

            await _sqlConection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogo = new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preço"]
                };
            }
            await _sqlConection.CloseAsync();

            return jogo;
        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos where Name = '{nome}' andProdutora = '{produtora}'";

            await _sqlConection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = (double)sqlDataReader["Preço"]
                });
            }
            await _sqlConection.CloseAsync();

            return jogos;
        }

        public async Task Remover(Guid id)
        {
            var comando = $"Delete Jogos where Id = `{id}'";

            await _sqlConection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConection);
            sqlCommand.ExecuteNonQuery();
            await _sqlConection.CloseAsync();
        }
    }
}
