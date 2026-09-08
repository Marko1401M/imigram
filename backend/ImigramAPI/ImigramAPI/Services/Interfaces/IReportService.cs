using ImigramAPI.DTOs;
using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface IReportService
    {
        Task<Report> Create(CreateReportDto dto);
        Task<List<ReportResponseDto>> GetAll();
        Task<ReportResponseDto> GetById(string id);
        Task<List<ReportResponseDto>> GetByStatus(string status);
        Task Resolve(string id, string status, string adminId);
    }
}
