using AutoMapper;
using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Repository.Contract;
using DesafioInventBackend.Service.Contract;
using NuGet.Repositories;

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

        ICollection<EquipamentoEletronico> IEquipamentoEletronicoService.listarEquipamentosEletronicos()
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

        public EquipamentoEletronico cadastrarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            throw new NotImplementedException();
        }

        public EquipamentoEletronico editarEquipamentoEletronico(int id, EquipamentoEletronico equipamentoEletronico)
        {
            
            EquipamentoEletronico equipamentoEletronicoHaAlterar = this.buscarEquipamentoEletronicoPorId(id);
            if (equipamentoEletronicoHaAlterar == null)
            {
                throw new FormatException("Equipamento Eletrônico não encontrado.");
            }
            
            if (equipamentoEletronicoHaAlterar.DataExclusao != null)
            {
                throw new FormatException("Este equipamento não pode ser editado, pois já foi excluído.");
            }

            _mapper.


            throw new NotImplementedException();
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
