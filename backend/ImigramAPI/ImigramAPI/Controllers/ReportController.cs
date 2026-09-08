using ImigramAPI.DTOs;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;
        public ReportController(IReportService reportService) { 
            _reportService = reportService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromForm] CreateReportDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            dto.ReporterId = userId;
            var res = await _reportService.Create(dto);

            return Ok(res);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _reportService.GetAll();

            return Ok(res);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("get/all/{status}")]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var res = await _reportService.GetByStatus(status);

            return Ok(res);
        }
        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var res = await _reportService.GetById(id);

            return Ok(res);
        }
        [Authorize(Roles = "admin")]
        [HttpPut("{reportId}/{status}")]
        public async Task<IActionResult> SetStatus(string reportId, string status)
        {
            var adminId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"AdminId = {adminId}");
            await _reportService.Resolve(reportId, status, adminId);

            return Ok();
        }
    }
}
