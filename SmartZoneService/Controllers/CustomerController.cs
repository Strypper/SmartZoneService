using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using SmartZone.Entities;
using SmartZone.Repositories;
using System.Threading;
using System.Threading.Tasks;


namespace SmartZoneService.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CustomerManager _customertManager;

        public CustomerController(IMapper mapper, CustomerManager customertManager)
        {
            _mapper = mapper;
            _customertManager = customertManager;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id, CancellationToken cancellationToken = default)
        {
            var customer = await _customertManager.FindByIdAsync(Id);

            if (customer == null) return BadRequest("No customer with id " + Id);

            return Ok(_mapper.Map<CustomerDTO>(customer));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerDTO dto, CancellationToken cancellationToken = default)
        {
            var customer = _mapper.Map<Customer>(dto);
            customer.IsDeleted = false;
            var result = await _customertManager.CreateAsync(customer);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            var addtoRoleResullt = await _customertManager.AddToRoleAsync(customer, "customer");
            if (!addtoRoleResullt.Succeeded)
            {
                return BadRequest("Fail to add role");
            }

            return CreatedAtAction(nameof(GetById), new { customer.Id }, _mapper.Map<CustomerDTO>(customer));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(CustomerDTO dto, CancellationToken cancellationToken = default)
        {
            var customer = await _customertManager.FindByIdAsync(dto.Id);
            if (customer == null || customer.IsDeleted == true) return NotFound("Cannot Find Customer With Id "
                                                                                    + dto.Id
                                                                                    + " Or Has Been Deleted");

            customer = _mapper.Map<Customer>(dto);
            var result = await _customertManager.UpdateAsync(customer);

            if (!result.Succeeded) return BadRequest("Failed to update!");
                
            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<CustomerDTO>(customer));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id, CancellationToken cancellationToken = default)
        {
            var customer = await _customertManager.FindByIdAsync(Id);
            if (customer == null || customer.IsDeleted == true) return NotFound("Cannot Find Customer With Id "
                                                                                    + Id
                                                                                    + " Or Has Been Deleted");
            var result = await _customertManager.Delete(customer);
            if (!result.Succeeded) return StatusCode(500);

            return NoContent();
        }
    }
}
