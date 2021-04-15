using AutoMapper;
using SmartZone.DataObjects;
using ESZ = SmartZone.Entities;

namespace SmartZoneService
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<SmartZoneDTO, ESZ.SmartZone>()
                .ForMember(sz => sz.Id, o => o.Ignore());
            CreateMap<ESZ.SmartZone, SmartZoneDTO>();


            CreateMap<FoodDTO, ESZ.Food>()
                .ForMember(f => f.Id, o => o.Ignore());
            CreateMap<ESZ.Food, FoodDTO>();


            CreateMap<StoreDTO, ESZ.Store>()
                .ForMember(s => s.Id, o => o.Ignore());
            CreateMap<ESZ.Store, StoreDTO>();
        }
    }
}
