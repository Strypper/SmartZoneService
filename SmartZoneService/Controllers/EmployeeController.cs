using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using SmartZone.Entities;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IStoreRepository _storeRepository;

        public EmployeeController(IMapper mapper, IEmployeeRepository employeeRepository, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _storeRepository = storeRepository;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var employee = await _employeeRepository.FindByIdAsync(Id);

            return Ok(_mapper.Map<EmployeeDTO>(employee));
        }

        [HttpGet("{storeId}")]
        public async Task<IActionResult> GetAllByStore(int storeId)
        {
            var employee = await _employeeRepository.FindAll(emp => emp!.StoreId == storeId).FirstOrDefaultAsync();
            if (employee == null) return NotFound("No Store Or No Employee Found");

            var employees = _employeeRepository.FindAll(storeId).ToListAsync();

            return Ok(_mapper.Map<IEnumerable<Employee>>(employees));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO dto)
        {
            var store = await _storeRepository.FindByIdAsync(dto.StoreId);

            if (store == null) return NotFound("No Store Found");

            var employee = _mapper.Map<Employee>(dto);
            _employeeRepository.Add(employee);
            await _employeeRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { employee.Id }, _mapper.Map<EmployeeDTO>(employee));
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(EmployeeDTO dto)
        {
            var employee = await _employeeRepository.FindByIdAsync(dto.Id);
            if (employee == null || employee.IsDeleted == true) return NotFound("Cannot Find Employee With Id "
                                                                                    + dto.Id
                                                                                    + " Or Has Been Deleted");

            employee = _mapper.Map<Employee>(dto);
            _employeeRepository.Update(employee);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<EmployeeDTO>(employee));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var employee = await _storeRepository.FindByIdAsync(Id);
            if (employee == null || employee.IsDeleted == true) return NotFound("Cannot Find Employee With Id "
                                                                                    + Id
                                                                                    + " Or Has Been Deleted");

            _storeRepository.Delete(employee);

            return NoContent();
        }
    }
}
