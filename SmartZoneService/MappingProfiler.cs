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
                .ForMember(smz => smz.Id, o => o.Ignore())
                .ForMember(smz => smz.Guid, o => o.Ignore())
                .ForMember(smz => smz.IsDeleted, o => o.Ignore());
            CreateMap<ESZ.SmartZone, SmartZoneDTO>();


            CreateMap<StoreDTO, ESZ.Store>()
                .ForMember(sto => sto.Id, o => o.Ignore());
            CreateMap<ESZ.Store, StoreDTO>();


            CreateMap<FoodDTO, ESZ.Food>()
                .ForMember(foo => foo.Id, o => o.Ignore());
            CreateMap<ESZ.Food, FoodDTO>();


            CreateMap<EmployeeDTO, ESZ.Employee>()
                .ForMember(emp => emp.Id, o => o.Ignore());
            CreateMap<ESZ.Employee, EmployeeDTO>();


            CreateMap<CustomerDTO, ESZ.Customer>()
                .ForMember(cus => cus.Id, o => o.Ignore());
            CreateMap<ESZ.Customer, CustomerDTO>();


            CreateMap<OrderDTO, ESZ.Order>()
                .ForMember(cus => cus.Id, o => o.Ignore());
            CreateMap<ESZ.Order, OrderDTO>();
        }
    }
}
