using AutoMapper;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Repository.Contract;
using DesafioInventBackend.Service.Contract;

namespace DesafioInventBackend.Service
{
    public class EquipamentoEletronicoService : IEquipamentoEletronicoService
    {

        private readonly IEquipamentoEletronicoRepository _repository;
        private readonly IMapper _mapper;

        public EquipamentoEletronicoService(
            IEquipamentoEletronicoRepository equipamentoEletronicoRepositry,
            IMapper mapper
            )
        {
            this._repository = equipamentoEletronicoRepositry;
            this._mapper = mapper;
        }

        public async Task<ICollection<RetornoEquipamentoEletronicoDto>> listarEquipamentosEletronicos()
        {
            ICollection<EquipamentoEletronico> listaEquipamentosEletronicos = await _repository.listarEquipamentosEletronicos();
            if (listaEquipamentosEletronicos.Count == 0)
            {
                throw new FormatException("Nenhum equipamento encontrado.");
            }

            ICollection<RetornoEquipamentoEletronicoDto> listaEquipamentosEletronicosDto = _mapper.Map<ICollection<RetornoEquipamentoEletronicoDto>>(listaEquipamentosEletronicos);

            return listaEquipamentosEletronicosDto;
        }

        public async Task<RetornoEquipamentoEletronicoDto> buscarEquipamentoEletronicoPorId(int id)
        {
            EquipamentoEletronico equipamentoEletronico = await _repository.buscarEquipamentoEletronicoPorId(id);
            if (equipamentoEletronico == null)
            {
                throw new FormatException("Equipamento eletrônico não encontrado.");
            }

            RetornoEquipamentoEletronicoDto equipamentoEletronicoDto = _mapper.Map<RetornoEquipamentoEletronicoDto>(equipamentoEletronico);

            return equipamentoEletronicoDto;
        }

        public async Task<RetornoEquipamentoEletronicoDto> cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronicoDto)
        {

            EquipamentoEletronico equipamentoEletronico = await _repository.cadastrarEquipamentoEletronico(new EquipamentoEletronico(equipamentoEletronicoDto));

            return _mapper.Map<RetornoEquipamentoEletronicoDto>(equipamentoEletronico);
        }

        public async Task<RetornoEquipamentoEletronicoDto> atualizarEquipamentoEletronico(int id, AtualizarEquipamentoEletronicoDto atualizarEquipamentoEletronicoDto)
        {

            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoDto = await this.buscarEquipamentoEletronicoPorId(id);
            if (retornoEquipamentoEletronicoDto == null)
            {
                throw new FormatException("Equipamento Eletrônico não encontrado.");
            }
            
            if (retornoEquipamentoEletronicoDto.DataExclusao != null)
            {
                throw new FormatException("Este equipamento não pode ser editado, pois já foi excluído.");
            }


            atualizarEquipamentoEletronicoDto.Id = id;
            atualizarEquipamentoEletronicoDto.DataInclusao = retornoEquipamentoEletronicoDto.DataInclusao;
            EquipamentoEletronico equipamentoEletronico = new EquipamentoEletronico(atualizarEquipamentoEletronicoDto);
            equipamentoEletronico = await _repository.atualizarEquipamentoEletronico(equipamentoEletronico);

            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoAtualizadoDto = await this.buscarEquipamentoEletronicoPorId(id);

            return retornoEquipamentoEletronicoAtualizadoDto;
        }

        public async Task<RetornoEquipamentoEletronicoDto> excluirEquipamentoEletronico(int id)
        {
            RetornoEquipamentoEletronicoDto retornoEquipamentoEletronicoDto = await this.buscarEquipamentoEletronicoPorId(id);
            if (retornoEquipamentoEletronicoDto.DataExclusao != null)
            {
                throw new FormatException("Equipamento já excluído.");
            }

            if (retornoEquipamentoEletronicoDto.TemEstoque)
            {
                throw new FormatException("Este equipamento não pode ser excluído, pois ainda há em estoque");
            }

            retornoEquipamentoEletronicoDto.DataExclusao = DateTime.Now;
            await _repository.atualizarEquipamentoEletronico(new EquipamentoEletronico(retornoEquipamentoEletronicoDto));

            return await this.buscarEquipamentoEletronicoPorId(id);
        }

    }
}
