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

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id,
                                                 CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(Id, cancellationToken);

            return Ok(_mapper.Map<StoreDTO>(store));
        }

        [HttpGet("{SmartZoneId}")]
        public async Task<IActionResult> GetBySmartZoneId(int smartZoneId,
                                                 CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindAllBySmartZoneId(smartZoneId)
                                                    .AsNoTracking()
                                                    .ToListAsync(cancellationToken);

            return Ok(_mapper.Map<StoreDTO>(store));
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreDTO dto,
                                                CancellationToken cancellationToken = default)
        {
            var smartZone = _smartZoneRepository.FindByIdAsync(dto.SmartZoneId);
            if (smartZone == null) return NotFound("No SmartZone Found");

            var store = _mapper.Map<Store>(dto);
            _storeRepository.Add(store);
            await _storeRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { store.Id }, _mapper.Map<StoreDTO>(store));
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(StoreDTO dto,
                                                CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (store == null || store.IsDeleted == true) return NotFound("Cannot Find Store With Id "
                                                                                    + dto.Id
                                                                                    + " Or It Has Been Deleted");
            var foods = store.Foods;

            store = _mapper.Map<Store>(dto);
            store.Foods = foods;
            _storeRepository.Update(store);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<StoreDTO>(store));
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var store = await _storeRepository.FindByIdAsync(Id);
            if (store == null || store.IsDeleted == true) return NotFound("Cannot Find Store With Id "
                                                                                    + Id
                                                                                    + " Or It Has Been Deleted");

            _storeRepository.Delete(store);

            return NoContent();
        }
    }
}
