using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using SmartZone.Entities;
using System.Threading.Tasks;


namespace SmartZoneService.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var customer = await _customerRepository.FindByIdAsync(Id);

            return Ok(_mapper.Map<CustomerDTO>(customer));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDTO dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            _customerRepository.Add(customer);
            await _customerRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { customer.Id }, _mapper.Map<CustomerDTO>(customer));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(CustomerDTO dto)
        {
            var customer = await _customerRepository.FindByIdAsync(dto.Id);
            if (customer == null || customer.IsDeleted == true) return NotFound("Cannot Find Customer With Id "
                                                                                    + dto.Id
                                                                                    + " Or Has Been Deleted");

            customer = _mapper.Map<Customer>(dto);
            _customerRepository.Update(customer);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<CustomerDTO>(customer));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var customer = await _customerRepository.FindByIdAsync(Id);
            if (customer == null || customer.IsDeleted == true) return NotFound("Cannot Find Customer With Id "
                                                                                    + Id
                                                                                    + " Or Has Been Deleted");

            _customerRepository.Delete(customer);

            return NoContent();
        }
    }
}
