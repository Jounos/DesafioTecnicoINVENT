
using AutoMapper;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

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
            return new OkObjectResult(_service.ListarTodos());
        }

        [HttpGet("{id}")]
        public ObjectResult buscarEquipamentoEletronicoPorId(string id)
        {
            return new OkObjectResult(_service.BuscarPorId(id));
        }

        [HttpPost]
        public CreatedResult cadastrarEquipamentoEletronico(EquipamentoEletronicoDTO equipamentoEletronicoDto)
        {
            return new CreatedResult("/api/equipamento-eletronico", _service.Cadastrar(_mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto)));
        }

        [HttpPut("{id}")]
        public NoContentResult atualizarEquipamentoEletronico(string id, EquipamentoEletronicoDTO equipamentoEletronicoDto)
        {
            _service.Atualizar(id, _mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public NoContentResult excluirEquipamentoEletronico(string id)
        {
            _service.Excluir(id);
            return new NoContentResult();
        }

    }
}

