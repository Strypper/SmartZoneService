using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.DataObjects;
using SmartZone.Entities;
using SmartZone.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZoneService.Controllers
{
    [Route("/api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly CustomerManager _customerManager;
        private readonly IStoreRepository _storeRepository;

        public OrderController(IMapper mapper, IOrderRepository orderRepository, CustomerManager customerManager, IStoreRepository storeRepository)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
            _customerManager = customerManager;
            _storeRepository = storeRepository;
        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.FindByIdAsync(Id, cancellationToken);

            return Ok(_mapper.Map<OrderDTO>(order));
        }


        [HttpGet("{cusomerId}")]
        public async Task<IActionResult> GetByCustomerId(string customerId, CancellationToken cancellationToken = default)
        {
            var customer = await _customerManager.FindByIdAsync(customerId);
            if (customer is null) return NotFound("No Customer Found");

            var orders = await _orderRepository.FindAllByCustomerId(customerId).ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(orders));
        }


        [HttpGet("{storeId}")]
        public async Task<IActionResult> GetByStoreId(int storeId, CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(storeId, cancellationToken);
            if (store is null) return NotFound("No Store Found");

            var orders = await _orderRepository.FindAllByStoreId(storeId).ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(orders));
        }


        [HttpGet("{storeId}")]
        public async Task<IActionResult> GetByStoreIdToday(int storeId, CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(storeId, cancellationToken);
            if (store is null) return NotFound("No Store Found");

            var orders = await _orderRepository.FindByStoreIdToday(storeId).ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(orders));
        }


        [HttpGet("{storeId}/{status}")]
        public async Task<IActionResult> GetStatusByStoreId(int storeId, OrderStatus status, CancellationToken cancellationToken = default)
        {
            var store = await _storeRepository.FindByIdAsync(storeId, cancellationToken);
            if (store is null) return NotFound("No Store Found");

            var orders = await _orderRepository.FindStatusByStoreId(storeId, status).ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(orders));
        }


        [HttpGet("{customerId}/{status}")]
        public async Task<IActionResult> GetStatusByCustomerId(string customerId, OrderStatus status, CancellationToken cancellationToken = default)
        {
            var customer = await _customerManager.FindByIdAsync(customerId);
            if (customer is null) return NotFound("No Customer Found");

            var orders = await _orderRepository.FindStatusByCustomerId(customerId, status).ToListAsync(cancellationToken);
            return Ok(_mapper.Map<IEnumerable<OrderDTO>>(orders));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDTO dto, CancellationToken cancellationToken = default)
        {
            var order = _mapper.Map<Order>(dto);
            _orderRepository.Add(order);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            return CreatedAtAction(nameof(GetById), new { order.Id }, _mapper.Map<OrderDTO>(order));
        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update([FromBody] OrderDTO dto, CancellationToken cancellationToken = default)
        {

            var order = await _orderRepository.FindByIdAsync(dto.Id, cancellationToken);
            if (order == null) return NotFound("Cannot Find Order With Id "
                                                                    + dto.Id
                                                                    + " Or It Has Been Deleted");

            var foods = order.Foods;

            order = _mapper.Map<Order>(dto);
            order.Id = dto.Id;
            order.Foods = foods;
            _orderRepository.Update(order);

            return CreatedAtAction(nameof(GetById), new { dto.Id }, _mapper.Map<OrderDTO>(order));
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id, CancellationToken cancellationToken = default)
        {
            var order = await _orderRepository.FindByIdAsync(Id, cancellationToken);
            if (order == null) return NotFound("Cannot Find Customer With Id "
                                                                        + Id
                                                                        + " Or It Has Been Deleted");

            _orderRepository.Delete(order);

            return NoContent();
        }
    }
}
