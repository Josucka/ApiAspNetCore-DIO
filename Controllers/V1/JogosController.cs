using ApiAspNetCore.InputModel;
using ApiAspNetCore.Service;
using ApiAspNetCore.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAspNetCore.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> Obter([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoService.Obter(pagina, quantidade);
            
            if (jogos.Count() == 0)
                return NoContent();
            
            return Ok(jogos);
        }

       [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Obter([FromRoute]Guid idJogo)
        {
            var jogo = await _jogoService.Obter(idJogo);

            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InsertJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoService.InsertJogo(jogoInputModel);

                return Ok(jogo);
            }
            catch(Exception ex)
            {
                return UnprocessableEntity("ja existe um jogo com este nome com a mesma produtora");

            }
        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogo)
        {
            try
            {
                await _jogoService.AtualizarJogo(idJogo, jogo);

                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound("Jogo inexistente");
            }
        }

        [HttpPatch("{id:guid}/preco/{preco:double}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.AtualizarJogo(idJogo, preco);

                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound("Jogo inexistente");
            }
        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> DeleteJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.DeleteJogo(idJogo);

                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound("Jogo nao existe");
            }
        }
    }
}
