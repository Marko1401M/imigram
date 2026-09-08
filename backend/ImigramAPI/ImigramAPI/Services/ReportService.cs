using ImigramAPI.DTOs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;

namespace ImigramAPI.Services
{
    public class ReportService : IReportService
    {
        IReportRepository _reportRepository;
        IUserService _userService;
        IPostService _postService;
        public ReportService(IReportRepository reportRepository, IUserService userService, IPostService postService)
        {
            _reportRepository = reportRepository;
            _userService = userService;
            _postService = postService;
        }
        public async Task<Report> Create(CreateReportDto dto)
        {
            var report = new Report
            {
                ReporterId = dto.ReporterId,
                ReportedUserId = dto.ReportedUserId,
                Reason = dto.Reason,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
                PostId = dto.PostId,
                Status = "Pending",
                ResolvedAt = null,
                ResolvedBy = null,
            };
            await _reportRepository.Create(report);

            return report;
        }

        public async Task<List<ReportResponseDto>> GetAll()
        {
            var res = await _reportRepository.GetAllReports();

            List<ReportResponseDto> result = new List<ReportResponseDto>();

            foreach (var report in res) {
                var reporterUser = await _userService.GetUserById(report.ReporterId);
                if (reporterUser == null) continue;
                var post = await _postService.GetPost(report.PostId, report.ReporterId);
                if (post == null) continue;
                var resolvedBy = report.Status != "Pending" ? await _userService.GetUserById(report.ResolvedBy) : null;

                var temp = new ReportResponseDto
                {
                    Id = report.Id,
                    Reporter = reporterUser,
                    Post = post,
                    CreatedAt = report.CreatedAt,
                    Description = report.Description,
                    Reason = report.Reason,
                    ResolvedBy = resolvedBy,
                    Status = report.Status,
                    ResolvedAt = report.ResolvedAt,
                };
                result.Add(temp);
            }

            return result;
        }
        public async Task<List<ReportResponseDto>> GetByStatus(string status)
        {
            var res = await _reportRepository.GetByStatus(status);

            List<ReportResponseDto> result = new List<ReportResponseDto>();

            foreach (var report in res)
            {
                var reporterUser = await _userService.GetUserById(report.ReporterId);
                var post = await _postService.GetPost(report.PostId, report.ReporterId);
                var resolvedBy = report.Status != "Pending" ? await _userService.GetUserById(report.ResolvedBy) : null;

                var temp = new ReportResponseDto
                {
                    Id = report.Id,
                    Reporter = reporterUser,
                    Post = post,
                    CreatedAt = report.CreatedAt,
                    Description = report.Description,
                    Reason = report.Reason,
                    ResolvedBy = resolvedBy,
                    Status = report.Status,
                    ResolvedAt = report.ResolvedAt,
                };
                result.Add(temp);
            }

            return result;
        }

        public async Task<ReportResponseDto> GetById(string id)
        {
            var report = await _reportRepository.GetReport(id);

            var reporterUser = await _userService.GetUserById(report.ReporterId);
            var post = await _postService.GetPost(report.PostId, report.ReporterId);
            var resolvedBy = report.Status != "Pending" ? await _userService.GetUserById(report.ResolvedBy) : null;

            var result = new ReportResponseDto
            {
                Id = report.Id,
                Reporter = reporterUser,
                Post = post,
                CreatedAt = report.CreatedAt,
                Description = report.Description,
                Reason = report.Reason,
                ResolvedBy = resolvedBy,
                Status = report.Status,
                ResolvedAt = report.ResolvedAt,
            };
            return result;
        }

        public async Task Resolve(string id, string status, string adminId)
        {
            var res = await _reportRepository.GetReport(id);

            res.Status = status;

            res.ResolvedAt = DateTime.UtcNow;

            res.ResolvedBy = adminId;
            await _reportRepository.Update(res);
        }
    }
}
