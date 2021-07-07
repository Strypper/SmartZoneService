using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SmartZone.Entities;


namespace SmartZoneService.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class StoreController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStoreRepository _storeRepository;
        private readonly ISmartZoneRepository _smartZoneRepository;

        public StoreController(IMapper mapper, IStoreRepository storeRepository, ISmartZoneRepository smartZoneRepository)
        {
            _mapper = mapper;
            _storeRepository = storeRepository;
            _smartZoneRepository = smartZoneRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var stores = await _storeRepository.FindAll()
                                                  .AsNoTracking()
                                                  .ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<StoreDTO>>(stores));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id,
                                                 CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(id, cancellationToken);

            if (store == null) return BadRequest("No store with id " + id);

            return Ok(_mapper.Map<StoreDTO>(store));
        }


        [HttpGet("{smartZoneId}")]
        public async Task<IActionResult> GetBySmartZoneId(int smartZoneId,
                                                 CancellationToken cancellationToken = default)
        {
            var smz = await _smartZoneRepository.FindByIdAsync(smartZoneId);

            if (smz == null) return BadRequest("No SmartZone with id " + smartZoneId);
            else {
                var stores = await _storeRepository.FindAllBySmartZoneId(smartZoneId)
                                                        .AsNoTracking()
                                                        .ToListAsync(cancellationToken);

                return Ok(_mapper.Map<IEnumerable<StoreDTO>>(stores));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StoreDTO dto,
                                                CancellationToken cancellationToken = default)
        {
            var smartZone = await _smartZoneRepository.FindByIdAsync(dto.SmartZoneId);
            if (smartZone == null) return NotFound("No SmartZone Found");

            var store = _mapper.Map<Store>(dto);

            store.IsDeleted = false;
            store.OneStarRating = 0;
            store.TwoStarRating = 0;
            store.ThreeStarRating = 0;
            store.FourStarRating = 0;
            store.FiveStarRating = 0;

            _storeRepository.Add(store);
            await _storeRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { store.Id }, _mapper.Map<StoreDTO>(store));
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(StoreDTO dto,
                                                CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (store == null) return NotFound("Cannot Find Store With Id ");

            var foods = store.Foods;

            store = _mapper.Map<Store>(dto);
            store.Foods = foods;
            _storeRepository.Update(store);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<StoreDTO>(store));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(id, cancellationToken);
            if (store == null || store.IsDeleted == true) return NotFound("Cannot Find Store With Id "
                                                                                    + id
                                                                                    + " Or It Has Been Deleted");

            _storeRepository.Delete(store);

            return NoContent();
        }
    }
}
