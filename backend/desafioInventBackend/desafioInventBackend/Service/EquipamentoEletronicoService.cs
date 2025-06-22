using DesafioInventBackend.Model.Entity;
using DesafioInventBackend.Repository.Contract;
using DesafioInventBackend.Service.Contract;
using NuGet.Repositories;

namespace DesafioInventBackend.Service
{
    public class EquipamentoEletronicoService : IEquipamentoEletronicoService
    {

        private readonly IEquipamentoEletronicoRepository _repository;

        public EquipamentoEletronicoService(IEquipamentoEletronicoRepository equipamentoEletronicoRepositry)
        {
            this._repository = equipamentoEletronicoRepositry;
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

        public EquipamentoEletronico editarEquipamentoEletronico(EquipamentoEletronico equipamentoEletronico)
        {
            throw new NotImplementedException();
        }

        public bool excluirEquipamentoEletronico(int id)
        {
            EquipamentoEletronico equipamentoEletronico = _repository.buscarEquipamentoEletronicoPorId(id);


            throw new NotImplementedException();
        }

    }
}
