using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESZ = SmartZone.Entities;

namespace SmartZoneService.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class SmartZoneController : ControllerBase
    {
        private IMapper _mapper { get; set; }
        private ISmartZoneRepository _smartZoneRepository { get; set; }


        //private readonly JwTokenConfig _jwTokenConfig;

        public SmartZoneController(IMapper mapper, ISmartZoneRepository smartZoneRepository)
        {
            _mapper = mapper;
            _smartZoneRepository = smartZoneRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindAll().AsNoTracking().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<SmartZoneDTO>>(smartZone));
        }

        [HttpGet("{amount}")]
        public async Task<IActionResult> GetTop(int amount, CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindTop(amount).AsNoTracking().ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<SmartZoneDTO>>(smartZone));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id, CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindByIdAsync(id);
            return Ok(_mapper.Map<IEnumerable<SmartZoneDTO>>(smartZone));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SmartZoneDTO dto, CancellationToken cancellationToken = default)
        {
            var smartZone = _mapper.Map<ESZ.SmartZone>(dto);
            _smartZoneRepository.Add(smartZone);
            await _smartZoneRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetByID), new { smartZone.Id }, _mapper.Map<SmartZoneDTO>(smartZone));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id, CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindByIdAsync(id, cancellationToken);
            if (smartZone == null) return NotFound();

            smartZone.IsDeleted = true;
            _smartZoneRepository.Update(smartZone);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(SmartZoneDTO dto, CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (smartZone == null || smartZone.IsDeleted == true) return NotFound();

            smartZone = _mapper.Map<ESZ.SmartZone>(dto);
            _smartZoneRepository.Update(smartZone);
            return CreatedAtAction(nameof(GetByID), new { dto.Id }, _mapper.Map<SmartZoneDTO>(smartZone));
        }
    }
}
