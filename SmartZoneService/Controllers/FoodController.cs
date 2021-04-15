﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using SmartZone.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESZ = SmartZone.Entities;

namespace SmartZoneService.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class FoodController : ControllerBase
    {
        private IMapper _mapper { get; set; }
        private IFoodRepository _foodRepository { get; set; }
        private IStoreRepository _storeRepository { get; set; }

        public FoodController(IMapper mapper, IFoodRepository foodRepository, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _foodRepository = foodRepository;
            _storeRepository = storeRepository;
        }


        [HttpGet("{StoreId}")]
        public async Task<IActionResult> GetAllByStore(string StoreId,
                                                       CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(StoreId, cancellationToken);
            if (store == null) return NotFound("No Store Found");

            var foods = await _foodRepository.FindAll(f => f!.StoreId == StoreId).ToListAsync();
            if (foods.Count == 0) return NotFound("No Food Found");
            return Ok(_mapper.Map<IEnumerable<FoodDTO>>(foods));
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(string Id,
                                                 CancellationToken cancellationToken = default)
        {
            var food = await _foodRepository.FindByIdAsync(Id, cancellationToken);
            return Ok(_mapper.Map<FoodDTO>(food));
        }
           


        [HttpPost]
        public async Task<IActionResult> Create(FoodDTO dto, CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(dto.StoreId);
            if (store == null) return NotFound("No store found");

            var food = _mapper.Map<Food>(dto);
            _foodRepository.Add(food);
            await _foodRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { food.Id }, _mapper.Map<FoodDTO>(food));
        }
    }
}