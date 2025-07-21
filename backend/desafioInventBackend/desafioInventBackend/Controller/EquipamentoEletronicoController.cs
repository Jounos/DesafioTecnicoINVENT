
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
                    Status = StatusCodes.Status500InternalServerError,
                    StatusText = "Nenhum equipamento eletrônico foi encontrado.",
                });
            }

            IEnumerable<RetornoEquipamentoEletronicoDTO> eeDTO = _mapper.Map<IEnumerable<RetornoEquipamentoEletronicoDTO>>(listaEquipamentosEletronicos);
            return Ok(new HttpStatus<IEnumerable<RetornoEquipamentoEletronicoDTO>>
            {
                Body = eeDTO,
                Status = StatusCodes.Status200OK,
            });
        }



        [HttpGet("{id}")]
        public ActionResult<RetornoEquipamentoEletronicoDTO> buscarEquipamentoEletronicoPorId(string id)
        {
            EquipamentoEletronico equipamentoEletronico = _service.BuscarPorId(id);
            if (equipamentoEletronico == null)
            {
                return NotFound(new HttpStatus<EquipamentoEletronico>
                {
                    Body = null,
                    Status = StatusCodes.Status500InternalServerError,
                    StatusText = "Equipamento Eletrônico selecionando não encontrado.",
                });
            }

            RetornoEquipamentoEletronicoDTO equipamentoEletronicoDTO= _mapper.Map<RetornoEquipamentoEletronicoDTO>(equipamentoEletronico);

            return Ok(new HttpStatus<RetornoEquipamentoEletronicoDTO>
            {
                Body = equipamentoEletronicoDTO,
                Status = StatusCodes.Status200OK,
            });
        }

        [HttpPost]
        public ActionResult<RetornoEquipamentoEletronicoDTO> cadastrarEquipamentoEletronico(EquipamentoEletronicoDTO equipamentoEletronicoDto)
        {
            EquipamentoEletronico equipamentoEletronico = _service.Cadastrar(_mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            if (equipamentoEletronico == null)
            {
                return NotFound(new HttpStatus<RetornoEquipamentoEletronicoDTO>
                {
                    Body = null,
                    Status = StatusCodes.Status200OK,
                    StatusText = "Erro ao cadastrar este equipamento eletrônico",
                });
            }

            return Ok(new HttpStatus<RetornoEquipamentoEletronicoDTO> {
                Body = _mapper.Map<RetornoEquipamentoEletronicoDTO>(equipamentoEletronico),
                Status = StatusCodes.Status200OK
            });
        }

        [HttpPut("{id}")]
        public ActionResult<RetornoEquipamentoEletronicoDTO> atualizarEquipamentoEletronico(string id, EquipamentoEletronicoDTO equipamentoEletronicoDto)
        {
            EquipamentoEletronico equipamentoEletronico = _service.Atualizar(id, _mapper.Map<EquipamentoEletronico>(equipamentoEletronicoDto));
            if (equipamentoEletronico == null)
            {
                return NotFound(new HttpStatus<EquipamentoEletronico>
                {
                    Body = null,
                    Status = StatusCodes.Status500InternalServerError,
                    StatusText = "Erro ao tentar alterar equipamento eletrônico.",
                });
            }

            return Ok(new HttpStatus<RetornoEquipamentoEletronicoDTO>
            {
                Body = _mapper.Map<RetornoEquipamentoEletronicoDTO>(equipamentoEletronico),
                Status = StatusCodes.Status200OK,
            });
        }

        [HttpDelete("{id}")]
        public ActionResult<HttpStatus<bool>> excluirEquipamentoEletronico(string id)
        {   
            EquipamentoEletronico equipamentoEletronico = _service.Excluir(id);

            if  (equipamentoEletronico.DataExclusao == DateTime.MinValue)
            {
                return NotFound(new HttpStatus<bool>
                {
                    Body = false,
                    Status = StatusCodes.Status500InternalServerError,
                    StatusText = "Error ao tentar excluir o equipamento eletrônico"
                });
            }

            return Ok(new HttpStatus<bool>
            {
                Body = true,
                Status = StatusCodes.Status200OK,
            });
        }

    }
}

