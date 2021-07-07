using AutoMapper;
using SmartZone.DataObjects;
using System.Linq;
using ESZ = SmartZone.Entities;

namespace SmartZoneService
{
    public class MappingProfiler : Profile
    {
        public MappingProfiler()
        {
            CreateMap<SmartZoneDTO, ESZ.SmartZone>()
                .ForMember(smz => smz.Id, opt => opt.Ignore())
                .ForMember(smz => smz.Guid, opt => opt.Ignore())
                .ForMember(smz => smz.IsDeleted, opt => opt.Ignore());
            CreateMap<ESZ.SmartZone, SmartZoneDTO>();


            CreateMap<StoreDTO, ESZ.Store>()
                .ForMember(sto => sto.Id, opt => opt.Ignore());
            CreateMap<ESZ.Store, StoreDTO>();


            CreateMap<FoodDTO, ESZ.Food>()
                .ForMember(foo => foo.Id, opt => opt.Ignore());
            CreateMap<ESZ.Food, FoodDTO>();


            CreateMap<ESZ.Role, RoleDTO>();
            CreateMap<RoleDTO, ESZ.Role>()
                .ForMember(rol => rol.Id, opt => opt.Ignore());


            CreateMap<ESZ.User, IdentityUserDTO>()
                .ForMember(usr => usr.Roles, opt => opt.MapFrom(usr => usr.UserRoles.Select(u_r => u_r.Role!.Name)));
            CreateMap<IdentityUserDTO, ESZ.User>();


            CreateMap<EmployeeDTO, ESZ.Employee>()
                .ForMember(emp => emp.Guid, opt => opt.Ignore())
                .ForMember(emp => emp.Id, opt => opt.Ignore());
            CreateMap<ESZ.Employee, EmployeeDTO>();


            CreateMap<CustomerDTO, ESZ.Customer>()
                .ForMember(cus => cus.Guid, opt => opt.Ignore())
                .ForMember(cus => cus.Id, opt => opt.Ignore());
            CreateMap<ESZ.Customer, CustomerDTO>();


            CreateMap<OrderDTO, ESZ.Order>()
                .ForMember(cus => cus.Id, opt => opt.Ignore());
            CreateMap<ESZ.Order, OrderDTO>();
        }
    }
}
