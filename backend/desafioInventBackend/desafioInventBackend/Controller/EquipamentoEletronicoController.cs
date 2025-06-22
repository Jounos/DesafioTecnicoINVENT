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

namespace DesafioInventBackend.Controller
{
    [Route("api/equipamento-eletronico")]
    [ApiController]
    public class EquipamentoEletronicoController : ControllerBase
    {

        private readonly IEquipamentoEletronicoService _equipamentoEletronicoService;

        public EquipamentoEletronicoController(IEquipamentoEletronicoService equipamentoEletronicoService)
        {
            this._equipamentoEletronicoService = equipamentoEletronicoService;
        }

        [HttpGet]
        public async Task<IActionResult> listarTodosEquipamentosEletronicos()
        {
            return OK();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> buscarEquipamentoEletronicoPorId(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
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
