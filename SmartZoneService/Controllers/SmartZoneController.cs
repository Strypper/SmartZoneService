using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZoneService.Controllers
{
    public class SmartZoneController : ControllerBase
    {
        public IMapper _mapper { get; set; }
        public ISmartZoneRepository _smartZoneRepository { get; set; }
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
    }
}
