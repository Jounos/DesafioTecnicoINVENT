
using AutoMapper;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Service.Contract;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace DesafioInventBackend.Controller
{
    [Route("api/equipamento-eletronico")]
    [ApiController]
    [EnableCors("AllowAngularApp")]
    public class EquipamentoEletronicoController : ControllerBase
    {

        private readonly IEquipamentoEletronicoService _service;
        private readonly IMapper _mapper;

        public EquipamentoEletronicoController(
            IEquipamentoEletronicoService equipamentoEletronicoService,
            IMapper mapper
            )
        {
            this._service = equipamentoEletronicoService;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RetornoEquipamentoEletronicoDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(List<RetornoEquipamentoEletronicoDto>))]
        public async Task<ActionResult<ICollection<RetornoEquipamentoEletronicoDto>>> listarTodosEquipamentosEletronicos()
        {
            ICollection<EquipamentoEletronico> listaEquipamentosEletronicos = await _service.listarEquipamentosEletronicos();
            if (listaEquipamentosEletronicos.Count == 0)
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
        public async Task<ActionResult<RetornoEquipamentoEletronicoDto>> buscarEquipamentoEletronicoPorId(int id)
        {
            EquipamentoEletronico equipamentoEletronico = await _service.buscarEquipamentoEletronicoPorId(id);
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
        public async Task<ActionResult<RetornoEquipamentoEletronicoDto>> cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronicoDto)
        {

            EquipamentoEletronico equipamentoEletronico = await _service.cadastrarEquipamentoEletronico(_mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            if (equipamentoEletronico == null)
            {
                return NotFound(new HttpStatus<RetornoEquipamentoEletronicoDto>
                {
                    Body = null,
                    Status = 400,
                    Message = "Error"
                });
            }

            RetornoEquipamentoEletronicoDto retorno = _mapper.Map<RetornoEquipamentoEletronicoDto>(equipamentoEletronico);

            return Ok(new HttpStatus<RetornoEquipamentoEletronicoDto>
            {
                Body = retorno,
                Status = 201,
                Message = "Success"
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RetornoEquipamentoEletronicoDto>> atualizarEquipamentoEletronico(int id, AtualizarEquipamentoEletronicoDto equipamentoEletronicoDto)
        {

            EquipamentoEletronico equipamentoEletronico = await _service.atualizarEquipamentoEletronico(id, _mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            if (equipamentoEletronico == null)
            {
                return NotFound(new HttpStatus<RetornoEquipamentoEletronicoDto>
                {
                    Body = null,
                    Status = 400,
                    Message = "Error"
                });
            }

            RetornoEquipamentoEletronicoDto retorno = _mapper.Map<RetornoEquipamentoEletronicoDto>(equipamentoEletronico);

            return Ok(new HttpStatus<RetornoEquipamentoEletronicoDto>
            {
                Body = retorno,
                Status = 201,
                Message = "Success",
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> excluirEquipamentoEletronico(int id)
        {

            EquipamentoEletronico equipamentoEletronico = await _service.excluirEquipamentoEletronico(id);
            RetornoEquipamentoEletronicoDto retorno = _mapper.Map<RetornoEquipamentoEletronicoDto>(equipamentoEletronico);

            if (equipamentoEletronico != null)
            {
                return NotFound(new HttpStatus<RetornoEquipamentoEletronicoDto>
                {
                    Body = null,
                    Status = 400,
                    Message = "Error"
                });
            }

            return Ok(new HttpStatus<RetornoEquipamentoEletronicoDto>
            {
                Body = retorno,
                Status = 400,
                Message = "Error"
            });
        }

    }
}
