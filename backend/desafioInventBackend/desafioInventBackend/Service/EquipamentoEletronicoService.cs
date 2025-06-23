using AutoMapper;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Repository.Contract;
using DesafioInventBackend.Service.Contract;
using NuGet.Repositories;
using System.Security.Cryptography.Xml;

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
            /*if (equipamentoEletronico.Id != null) {
                throw new SystemException("Não foi possível realizar o cadastro do equipamento eletrônico.");
            }*/

            return _mapper.Map<RetornoEquipamentoEletronicoDto>(equipamentoEletronico);
        }

        public async Task<RetornoEquipamentoEletronicoDto> atualizarEquipamentoEletronico(int id, RetornoEquipamentoEletronicoDto equipamentoEletronicoDto)
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

            EquipamentoEletronico equipamentoEletronico = await _repository.atualizarEquipamentoEletronico(new EquipamentoEletronico(retornoEquipamentoEletronicoDto));
            /*if ()
            {
                throw new SystemException("Não foi possível atualizar o equipamento eletrõnico.");
            }*/

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

            await _repository.atualizarEquipamentoEletronico(new EquipamentoEletronico(retornoEquipamentoEletronicoDto));

            return await this.buscarEquipamentoEletronicoPorId(id);
        }

    }
}
