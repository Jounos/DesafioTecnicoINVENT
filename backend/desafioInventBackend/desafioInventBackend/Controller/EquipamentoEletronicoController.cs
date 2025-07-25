using AutoMapper;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DesafioInventBackend.Controller
{
    [ApiController]
    [Route("api/equipamento-eletronico")]
    [EnableCors("AllowAngularApp")]
    public class EquipamentoEletronicoController: ControllerBase
    {

        private readonly EquipamentoEletronicoService _service;
        private readonly IMapper _mapper;

        public EquipamentoEletronicoController(
            EquipamentoEletronicoService equipamentoEletronicoService,
            IMapper mapper
            )
        {
            this._service = equipamentoEletronicoService;
            this._mapper = mapper;
        }

        [HttpGet]
        public OkObjectResult ListarTodosEquipamentosEletronicos()
        {
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.ListarTodos();
            return Ok(_mapper.Map<IEnumerable<EquipamentoEletronicoDTO>>(listaEquipamentosEletronicos));
        }

        [HttpGet("{id}")]
        public ObjectResult buscarEquipamentoEletronicoPorId([FromRoute] string id)
        {
            return Ok(_service.BuscarPorId(id));
        }

        [HttpPost]
        public CreatedResult cadastrarEquipamentoEletronico([FromBody] EquipamentoEletronicoDTO equipamentoEletronicoDto)
        {
            _service.Cadastrar(_mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            return Created();
        }

        [HttpPut("{id}")]
        public NoContentResult atualizarEquipamentoEletronico([FromRoute] string id, [FromBody] EquipamentoEletronicoDTO equipamentoEletronicoDto)
        {
            _service.Atualizar(id, _mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public NoContentResult excluirEquipamentoEletronico([FromRoute] string id)
        {
            _service.Excluir(id);
            return NoContent();
        }

    }
}

