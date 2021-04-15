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
    public class StoreController : ControllerBase
    {
        private IMapper _mapper { get; set; }
        private IStoreRepository _storeRepository { get; set; }

        public StoreController(IMapper mapper, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var stores = await _storeRepository.FindAll()
                                                  .AsNoTracking()
                                                  .ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<StoreDTO>>(stores));
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id,
                                                 CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindAll(s => s!.Id == Id)
                                                    .AsNoTracking()
                                                    .ToListAsync(cancellationToken);
            return Ok(_mapper.Map<StoreDTO>(store));
        }

        [HttpPost("{Id}")]
        public async Task<IActionResult> Create(StoreDTO dto,
                                                CancellationToken cancellationToken = default)
        {
            var store = _mapper.Map<ESZ.Store>(dto);
            _storeRepository.Add(store);
            await _storeRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { store.Id }, _mapper.Map<StoreDTO>(store));
        }
    }
}
