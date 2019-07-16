using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CourseApi.Entities;
using CourseApi.Services.DailyChoices;
using CourseApi.Services.Menus;
using CourseApi.Services.Orders;
using CourseApi.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseApi.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly MenuService _menuService;
        private readonly UserService _userService;
        private readonly DailyChoiceService _dailyChoiceService;
        private readonly IMapper _mapper;

        public OrdersController(OrderService orderService, MenuService menuService, UserService userService, DailyChoiceService dailyChoiceService, IMapper mapper)
        {
            _orderService = orderService;
            _menuService = menuService;
            _userService = userService;
            _dailyChoiceService = dailyChoiceService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("getAllOrders")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _orderService.Get();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Order>> Get([FromQuery] string id)
        {
            return await _orderService.Get(id);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            return Ok(await _orderService.Create(order));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] string id, Order orderIn)
        {
            var order = await _orderService.Get(id);
            if(order == null)
                return NotFound();
            await _orderService.Update(id, orderIn);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            var order = await _orderService.Get(id);
            if(order == null)
                return NotFound();
            await _orderService.Delete(id);
            return NoContent();
        }

    }
}