using ImigramAPI.DTOs;
using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface IPostService
    {
        Task<Post> CreatePost(CreatePostDto dto, string userId);
        Task<List<Post>> GetPosts();
        Task<Post?> GetPost(string id);
    }
}
