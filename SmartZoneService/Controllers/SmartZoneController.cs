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
            var smartZone = await _smartZoneRepository.FindAll()
                                                      .AsNoTracking()
                                                      .ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<SmartZoneDTO>>(smartZone));
        }

        [HttpGet("{amount}")]
        public async Task<IActionResult> GetTop(int amount, 
                                                CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindTop(amount)
                                                      .AsNoTracking()
                                                      .ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<SmartZoneDTO>>(smartZone));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id , 
                                                 CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindByIdAsync(Id, cancellationToken);
            return Ok(_mapper.Map<SmartZoneDTO>(smartZone));
        }

        [HttpPost]
        public async Task<IActionResult> Create(SmartZoneDTO dto , 
                                                CancellationToken cancellationToken = default)
        {
            var smartZone = _mapper.Map<ESZ.SmartZone>(dto);
            smartZone.IsDeleted = smartZone.IsExpired = false;
            _smartZoneRepository.Add(smartZone);
            await _smartZoneRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { smartZone.Id }, _mapper.Map<SmartZoneDTO>(smartZone));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id , 
                                                CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindByIdAsync(id, cancellationToken);
            if (smartZone == null) return NotFound();

            smartZone.IsDeleted = true;
            _smartZoneRepository.Update(smartZone);
            return NoContent();
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(SmartZoneDTO dto , 
                                                CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (smartZone == null || smartZone.IsDeleted == true) return NotFound("Cannot find your SmartZone with id {0} " 
                                                                                    + dto.Id 
                                                                                    + " or it has been deleted");

            smartZone = _mapper.Map<ESZ.SmartZone>(dto);
            _smartZoneRepository.Update(smartZone);
            await _smartZoneRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<SmartZoneDTO>(smartZone));
        }
    }
}
