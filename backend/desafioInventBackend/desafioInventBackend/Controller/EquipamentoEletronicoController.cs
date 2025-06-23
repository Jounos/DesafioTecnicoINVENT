
using Microsoft.AspNetCore.Mvc;
using DesafioInventBackend.Service.Contract;
using DesafioInventBackend.Model.DTO;

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
        public async Task<ActionResult<RetornoEquipamentoEletronicoDto>> cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronicoDto)
        {

            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoDto = await _service.cadastrarEquipamentoEletronico(equipamentoEletronicoDto);
            if (retornoEquipamentoEletronicoDto == null)
            {
                return NotFound();
            }

            return Ok(retornoEquipamentoEletronicoDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RetornoEquipamentoEletronicoDto>> atualizarEquipamentoEletronico(int id, EquipamentoEletronicoDto equipamentoEletronicoDto)
        {
            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoatualizadoDto = await _service.atualizarEquipamentoEletronico(id, equipamentoEletronicoDto);
            if (retornoEquipamentoEletronicoatualizadoDto == null)
            {
                return NotFound();
            }

            return Ok(retornoEquipamentoEletronicoatualizadoDto);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> excluirEquipamentoEletronico(int id)
        {

            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoExcluidoDto = await _service.excluirEquipamentoEletronico(id);

            if (retornoEquipamentoEletronicoExcluidoDto.DataExclusao == null)
            {
                throw new SystemException("Não foi possível excluir este equipamento eletrônico.");
            }

            return Ok();
        }

    }
}
