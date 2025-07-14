
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
                return NotFound(new HttpStatus<ICollection<RetornoEquipamentoEletronicoDto>>
                {
                    Body = null,
                    Status = 404,
                    Message = "Not Found"
                });
            }

            return Ok(new HttpStatus<ICollection<RetornoEquipamentoEletronicoDto>>
            {
                Body = listaEquipamentosEletronicos,
                Status = 201,
                Message = "Success"
            });
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

            return Ok(new HttpStatus<RetornoEquipamentoEletronicoDto>
            {
                Body = equipamentoEletronicoDto,
                Status = 201,
                Message = "Success"
            });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RetornoEquipamentoEletronicoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(RetornoEquipamentoEletronicoDto))]
        public async Task<ActionResult<RetornoEquipamentoEletronicoDto>> cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronicoDto)
        {

            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoDto = await _service.cadastrarEquipamentoEletronico(equipamentoEletronicoDto);
            if (retornoEquipamentoEletronicoDto == null)
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
                Body = retornoEquipamentoEletronicoDto,
                Status = 201,
                Message = "Success"
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RetornoEquipamentoEletronicoDto>> atualizarEquipamentoEletronico(int id, AtualizarEquipamentoEletronicoDto equipamentoEletronicoDto)
        {
            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoAtualizadoDto = await _service.atualizarEquipamentoEletronico(id, equipamentoEletronicoDto);
            if (retornoEquipamentoEletronicoAtualizadoDto == null)
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
                Body = retornoEquipamentoEletronicoAtualizadoDto,
                Status = 201,
                Message = "Success"
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> excluirEquipamentoEletronico(int id)
        {

            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoExcluidoDto = await _service.excluirEquipamentoEletronico(id);

            if (retornoEquipamentoEletronicoExcluidoDto != null)
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
                Body = null,
                Status = 400,
                Message = "Error"
            });
        }

    }
}
