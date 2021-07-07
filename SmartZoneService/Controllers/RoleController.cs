using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.DataObjects;
using SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartZoneService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var roles = await _roleManager.Roles.OrderBy(rol => rol.NormalizedName).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<RoleDTO>>(roles));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleDTO dto)
        {
            var role = _mapper.Map<Role>(dto);
            await _roleManager.CreateAsync(role);

            return Ok(_mapper.Map<RoleDTO>(role));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] RoleDTO dto)
        {
            var role = await _roleManager.FindByIdAsync(dto.Id.ToString());
            if (role is null)
                return NotFound();

            _mapper.Map(dto, role);
            await _roleManager.UpdateAsync(role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();

            await _roleManager.DeleteAsync(role);
            return NoContent();
        }
    }
}
