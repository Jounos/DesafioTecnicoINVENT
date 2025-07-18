
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
        public ActionResult<HttpStatus<ICollection<RetornoEquipamentoEletronicoDTO>>> ListarTodosEquipamentosEletronicos()
        {
            IEnumerable<EquipamentoEletronico> listaEquipamentosEletronicos = _service.Listar();
            if (listaEquipamentosEletronicos.Count() == 0)
            {
                return NotFound(new HttpStatus<IEnumerable<EquipamentoEletronico>>
                {
                    Body = null,
                    Status = StatusCodes.Status404NotFound,
                    StatusText = "Nenhum equipamento eletrônico foi encontrado."
                });
            }

            IEnumerable<RetornoEquipamentoEletronicoDTO> retorno = _mapper.Map<IEnumerable<RetornoEquipamentoEletronicoDTO>>(listaEquipamentosEletronicos);
            return Ok(new HttpStatus<IEnumerable<RetornoEquipamentoEletronicoDTO>>
            {
                Body = retorno,
                Status = StatusCodes.Status200OK,
            });
        }



        [HttpGet("{id}")]
        public ActionResult<RetornoEquipamentoEletronicoDTO> buscarEquipamentoEletronicoPorId(string id)
        {
            EquipamentoEletronico equipamentoEletronico = _service.BuscarPorId(id);
            if (equipamentoEletronico == null)
            {
                return NotFound();
            }

            RetornoEquipamentoEletronicoDTO retorno = _mapper.Map<RetornoEquipamentoEletronicoDTO>(equipamentoEletronico);

            return Ok(retorno);
        }

        [HttpPost]
        public ActionResult<RetornoEquipamentoEletronicoDTO> cadastrarEquipamentoEletronico(EquipamentoEletronicoDTO equipamentoEletronicoDto)
        {
            EquipamentoEletronico ee = _service.Cadastrar(_mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            if (ee == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RetornoEquipamentoEletronicoDTO>(ee));
        }

        [HttpPut("{id}")]
        public ActionResult<RetornoEquipamentoEletronicoDTO> atualizarEquipamentoEletronico(string id, EquipamentoEletronicoDTO equipamentoEletronicoDto)
        {
            EquipamentoEletronico equipamentoEletronico = _service.Atualizar(id, _mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            if (equipamentoEletronico == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RetornoEquipamentoEletronicoDTO>(equipamentoEletronico));
        }

        [HttpDelete("{id}")]
        public bool excluirEquipamentoEletronico(string id)
        {
            EquipamentoEletronico ee = _service.Excluir(id);

            return ee.DataExclusao != DateTime.MinValue;
        }

    }
}

