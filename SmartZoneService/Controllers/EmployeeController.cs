using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using SmartZone.Entities;
using SmartZone.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZoneService.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EmployeeManager _employeeManager;
        private readonly IStoreRepository _storeRepository;

        public EmployeeController(IMapper mapper, EmployeeManager employeeManager, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _employeeManager = employeeManager;
            _storeRepository = storeRepository;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeManager.FindByIdAsync(Id);

            if (employee == null) return BadRequest("No employee with id " + Id);

            return Ok(_mapper.Map<EmployeeDTO>(employee));
        }


        [HttpGet("{storeId}")]
        public async Task<IActionResult> GetAllByStore(int storeId, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeManager.FindAll(storeId).ToListAsync(cancellationToken);
            if (employee == null) return NotFound("No Store Or No Employee Found");

            var employees = await _employeeManager.FindAll(storeId).ToListAsync(cancellationToken);

            return Ok(_mapper.Map<IEnumerable<Employee>>(employees));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO dto, CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(dto.StoreId, cancellationToken);

            if (store == null) return NotFound("No Store Found");

            var employee = _mapper.Map<Employee>(dto);
            employee.IsDeleted = false; // Just to make sure that the field is false at the point of creating

            var result = await _employeeManager.CreateAsync(employee, dto.PasswordHash);
            if (!result.Succeeded)
            {
                return StatusCode(500);
            }

            var addtoRoleResullt = await _employeeManager.AddToRoleAsync(employee, "employee");
            if (!addtoRoleResullt.Succeeded)
            {
                return BadRequest("Fail to add role");
            }

            return CreatedAtAction(nameof(GetById), new { employee.Id }, _mapper.Map<EmployeeDTO>(employee));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(EmployeeDTO dto, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeManager.FindByIdAsync(dto.Id);
            if (employee == null || employee.IsDeleted == true) return NotFound("Cannot Find Employee With Id "
                                                                                    + dto.Id
                                                                                    + " Or Has Been Deleted");

            employee = _mapper.Map<Employee>(dto);
            var result = await _employeeManager.UpdateAsync(employee);
            if (!result.Succeeded) return StatusCode(500);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<EmployeeDTO>(employee));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id, CancellationToken cancellationToken = default)
        {
            var employee = await _employeeManager.FindByIdAsync(Id);
            if (employee == null || employee.IsDeleted == true) return NotFound("Cannot Find Employee With Id "
                                                                                    + Id
                                                                                    + " Or Has Been Deleted");

            var result = await _employeeManager.Delete(employee);
            if (!result.Succeeded) return StatusCode(500);

            return NoContent();
        }
    }
}
