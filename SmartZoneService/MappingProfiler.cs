using AutoMapper;
using SmartZone.DataObjects;
using ESZ = SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartZoneService
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<SmartZoneDTO, ESZ.SmartZone>().ForMember(d => d.Id, o => o.Ignore()).ForMember(d => d.Id, o => o.Ignore());
            CreateMap<AddressInfoDTO, ESZ.AddressInfo>().ForMember(d => d.Id, o => o.Ignore()).ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
