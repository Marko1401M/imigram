using ImigramAPI.DTOs;
using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface IPostService
    {
        Task<Post> CreatePost(CreatePostDto dto, string userId);
        Task<List<PostDto>> GetPosts();
        Task<PostDto?> GetPost(string id, string userId);
        Task<List<PostDto>> GetAllPostsForUser(string userId);
        Task DeletePost(string postId);
        Task<List<PostDto>> GetFeed(string userId);
    }
}
