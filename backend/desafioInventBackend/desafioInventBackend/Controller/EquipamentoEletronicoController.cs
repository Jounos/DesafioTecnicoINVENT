
using AutoMapper;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DesafioInventBackend.Controller
{
    [Route("api/equipamento-eletronico")]
    [ApiController]
    [EnableCors("AllowAngularApp")]
    public class EquipamentoEletronicoController : ControllerBase
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RetornoEquipamentoEletronicoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<RetornoEquipamentoEletronicoDto>))]
        public ActionResult<ICollection<RetornoEquipamentoEletronicoDto>> ListarTodosEquipamentosEletronicos()
        {
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Listar();
            if (listaEquipamentosEletronicos.Count() == 0)
            {
                return NotFound(new HttpStatus<ICollection<RetornoEquipamentoEletronicoDto>>
                {
                    Body = null,
                    Status = 404,
                    Message = "Not Found"
                });
            }

            ICollection<RetornoEquipamentoEletronicoDto> retorno = _mapper.Map<ICollection<RetornoEquipamentoEletronicoDto>>(listaEquipamentosEletronicos);

            return Ok(new HttpStatus<ICollection<RetornoEquipamentoEletronicoDto>>
            {
                Body = retorno,
                Status = 201,
                Message = "Success"
            });
        }



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RetornoEquipamentoEletronicoDto>))]
        public ActionResult<RetornoEquipamentoEletronicoDto> buscarEquipamentoEletronicoPorId(int id)
        {
            EquipamentoEletronico equipamentoEletronico = _service.BuscarPorId(id);
            if (equipamentoEletronico == null)
            {
                return NotFound();
            }

            RetornoEquipamentoEletronicoDto retorno = _mapper.Map<RetornoEquipamentoEletronicoDto>(equipamentoEletronico);

            return Ok(new HttpStatus<RetornoEquipamentoEletronicoDto>
            {
                Body = retorno,
                Status = 201,
                Message = "Success"
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RetornoEquipamentoEletronicoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RetornoEquipamentoEletronicoDto))]
        public void cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronicoDto)
        {

            _service.Cadastrar(_mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
        }

        [HttpPut("{id}")]
        public void atualizarEquipamentoEletronico(int id, AtualizarEquipamentoEletronicoDto equipamentoEletronicoDto)
        {

            _service.Atualizar(id, _mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public void excluirEquipamentoEletronico(int id)
        {

            _service.Excluir(id);
        }

    }
}
