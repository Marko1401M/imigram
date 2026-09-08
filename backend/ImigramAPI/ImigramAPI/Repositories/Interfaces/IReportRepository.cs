using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<Report> Create(Report report);
        Task<Report> Update(Report report);
        Task<List<Report>> GetAllReports();
        Task<List<Report>> GetByStatus(string status);
        Task<Report> GetReport(string id);
    }
}
