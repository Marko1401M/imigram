namespace ImigramAPI.Services.Interfaces
{
    public interface IAdminService
    {
        Task BanUser(string userId);
        Task UnbanUser(string userId);
        Task DeletePost(string postId);
    }
}
