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

        public ICollection<EquipamentoEletronico> listarEquipamentosEletronicos()
        {
            List<EquipamentoEletronico> listaEquipamentoEletronico = _repository.listarEquipamentosEletronicos().ToList();
            if (listaEquipamentoEletronico.Count == 0)
            {
                throw new FormatException("Nenhum equipamento encontrado.");
            }

            return listaEquipamentoEletronico;
        }

        public EquipamentoEletronico buscarEquipamentoEletronicoPorId(int id)
        {
            EquipamentoEletronico equipamentoEletronico = _repository.buscarEquipamentoEletronicoPorId(id);
            if (equipamentoEletronico == null)
            {
                throw new FormatException("Equipamento eletrônico não encontrado.");
            }

            return equipamentoEletronico;
        }

        public EquipamentoEletronicoDto cadastrarEquipamentoEletronico(EquipamentoEletronicoDto equipamentoEletronicoDto)
        {

            EquipamentoEletronico equipamentoEletronico = new EquipamentoEletronico(equipamentoEletronicoDto);
            if (_repository.cadastrarEquipamentoEletronico(equipamentoEletronico)) {
                throw new SystemException("Não foi possível realizar o cadastro do equipamento eletrônico.");
            }

            return _mapper.Map<EquipamentoEletronicoDto>(equipamentoEletronico);
        }

        public EquipamentoEletronicoDto editarEquipamentoEletronico(int id, EquipamentoEletronicoDto equipamentoEletronicoDto)
        {
            
            EquipamentoEletronico equipamentoEletronico = this.buscarEquipamentoEletronicoPorId(id);
            if (equipamentoEletronico == null)
            {
                throw new FormatException("Equipamento Eletrônico não encontrado.");
            }
            
            if (equipamentoEletronico.DataExclusao != null)
            {
                throw new FormatException("Este equipamento não pode ser editado, pois já foi excluído.");
            }

            equipamentoEletronico.Nome = equipamentoEletronicoDto.Nome;
            equipamentoEletronico.TipoEquipamento = (TipoEquipamento)equipamentoEletronicoDto.TipoEquipamentoId;
            equipamentoEletronico.QuantidadeEstoque = equipamentoEletronicoDto.QuantidadeEstoque;
            //equipamentoEletronico.TemEstoque = (equipamentoEletronicoDto.QuantidadeEstoque > 0);

            if (_repository.atualizarEquipamentoEletronico(equipamentoEletronico))
            {
                throw new SystemException("Não foi possível atualizar o equipamento eletrõnico.");
            }

            equipamentoEletronicoDto = _mapper.Map<EquipamentoEletronicoDto>(equipamentoEletronico);

            return equipamentoEletronicoDto;
        }

        public bool excluirEquipamentoEletronico(int id)
        {
            EquipamentoEletronico equipamentoEletronico = this.buscarEquipamentoEletronicoPorId(id);
            if (equipamentoEletronico.DataExclusao != null)
            {
                throw new FormatException("Equipamento já excluído.");
            }

            equipamentoEletronico.DataExclusao = DateTime.Now;

            return _repository.atualizarEquipamentoEletronico(equipamentoEletronico);
        }

    }
}
