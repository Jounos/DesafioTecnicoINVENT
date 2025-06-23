using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioInventBackend.Context;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service.Contract;
using DesafioInventBackend.Model.DTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DesafioInventBackend.Controller
{
    [Route("api/equipamento-eletronico")]
    [ApiController]
    public class EquipamentoEletronicoController : ControllerBase
    {

        private readonly IEquipamentoEletronicoService _service;

        public EquipamentoEletronicoController(IEquipamentoEletronicoService equipamentoEletronicoService)
        {
            this._service = equipamentoEletronicoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RetornoEquipamentoEletronicoDto>))]
        public async Task<ActionResult<ICollection<RetornoEquipamentoEletronicoDto>>> listarTodosEquipamentosEletronicos()
        {
            ICollection<RetornoEquipamentoEletronicoDto> listaEquipamentosEletronicos = await _service.listarEquipamentosEletronicos();
            if (listaEquipamentosEletronicos.Count == 0)
            {
                return NotFound();
            }

            return Ok(listaEquipamentosEletronicos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RetornoEquipamentoEletronicoDto>))]
        public async Task<ActionResult<RetornoEquipamentoEletronicoDto>> buscarEquipamentoEletronicoPorId(int id)
        {
            RetornoEquipamentoEletronicoDto equipamentoEletronicoDto = await _service.buscarEquipamentoEletronicoPorId(id);
            if (equipamentoEletronicoDto == null)
            {
                return NotFound();
            }

            return Ok(equipamentoEletronicoDto);
        }

        [HttpPost]
        public async Task<IActionResult> cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronicoDto)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> atualizarEquipamentoEletronico(int id, EquipamentoEletronico equipamentoEletronico)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> excluirEquipamentoEletronico(int id)
        {
            return Ok();
        }

    }
}
