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
                .ForMember(d => d.Id, o => o.Ignore());
            CreateMap<ESZ.SmartZone, SmartZoneDTO>();


            CreateMap<StoreDTO, ESZ.Store>().ForMember(d => d.Id, o => o.Ignore()).ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
