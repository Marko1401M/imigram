using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;
namespace ImigramAPI.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly MongoDbContext _context;
        public ReportRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task<Report> Create(Report report)
        {
            await _context.Reports.InsertOneAsync(report);

            return report;
        }

        public async Task<List<Report>> GetAllReports()
        {
            return await _context.Reports.Find(_ => true).ToListAsync();
        }

        public async Task<Report> GetReport(string id)
        {
            return await _context.Reports.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Report> Update(Report report)
        {
            await _context.Reports.ReplaceOneAsync(r => r.Id == report.Id, report);

            return report;
        }
        public async Task<List<Report>> GetByStatus(string status)
        {
            var res = await _context.Reports.Find(r => r.Status == status).ToListAsync();

            return res;
        }
    }
}
