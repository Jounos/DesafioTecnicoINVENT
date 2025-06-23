using AutoMapper;
using DesafioInventBackend.Model.DTO;
using DesafioInventBackend.Model.Entity;

namespace DesafioInventBackend.Model.Mapper
{
    public class DtoMapping: Profile
    {
        public DtoMapping()
        {
            CreateMap<EquipamentoEletronico, RetornoEquipamentoEletronicoDto>().ReverseMap();
        }
    }
}
