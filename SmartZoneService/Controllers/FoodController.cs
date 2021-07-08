using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using SmartZone.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZoneService.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class FoodController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository _foodRepository;
        private readonly IStoreRepository _storeRepository;            

        public FoodController(IMapper mapper, IFoodRepository foodRepository, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
            _storeRepository = storeRepository;
        }


        [HttpGet("{storeId}")]
        public async Task<IActionResult> GetAllByStore(int storeId,
                                                       CancellationToken cancellationToken = default)
        {
            var aFood = await _foodRepository.FindAll(food => food.StoreId == storeId).FirstOrDefaultAsync(cancellationToken);
            if (aFood == null) return NotFound("No Store Or No Food Found");

            return Ok(_mapper.Map<IEnumerable<FoodDTO>>(await _foodRepository.FindAll(storeId).ToListAsync()));
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id,
                                                 CancellationToken cancellationToken = default)
        {
            var food = await _foodRepository.FindByIdAsync(id, cancellationToken);

            if (food == null) return BadRequest("No food found");
            return Ok(_mapper.Map<FoodDTO>(food));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FoodDTO dto, CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(dto.StoreId);
            if (store == null) return NotFound("No Store Found");

            var food = _mapper.Map<Food>(dto);
            _foodRepository.Add(food);
            await _foodRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { food.Id }, _mapper.Map<FoodDTO>(food));
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody] FoodDTO dto,
                                                CancellationToken cancellationToken = default)
        {
            var food = await _foodRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (food == null) return NotFound("No Food Found");

            food = _mapper.Map<Food>(dto);
            _foodRepository.Update(food);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<SmartZoneDTO>(food));
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteById(int Id,
                                                CancellationToken cancellationToken = default)
        {
            var food = await _foodRepository.FindByIdAsync(Id, cancellationToken);
            if (food == null) return NotFound("No Food Found");

            _foodRepository.Delete(food);
            await _foodRepository.SaveChangesAsync(cancellationToken);

            return Ok(new { message = "done" });
        }


        [HttpDelete("{StoreId}")]
        public async Task<IActionResult> DeleteAllByStore(int StoreId,
                                                CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(StoreId, cancellationToken);
            if (store == null) return NotFound("No Store Found");

            var food = store.Foods;
            if (food == null) return NotFound("No Food Found");

            _foodRepository.DeleteRange(food);

            return NoContent();
        }
    }
}
