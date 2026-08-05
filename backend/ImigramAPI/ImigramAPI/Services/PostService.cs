using ImigramAPI.DTOs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;

namespace ImigramAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IWebHostEnvironment _environment;
        public PostService(IPostRepository postRepository, IWebHostEnvironment environment)
        {
            _postRepository = postRepository;
            _environment = environment;
        }

        public async Task<Post> CreatePost(CreatePostDto dto, string userId)
        {
            var post = new Post
            {
                UserId = userId,
                Content = dto.Content,
                CreatedAt = DateTime.UtcNow,
                CommentsCount = 0,
                Location = new PostLocation
                {
                    Latitude = 0,
                    Longitude = 0,
                    Name = dto.Location
                }
            };

            foreach(var file in dto.Media)
            {
                var mediaUrl = SaveFile(file);

                post.Media.Add(new PostMedia
                {
                    Url = mediaUrl.Result,
                    Type = file.ContentType.StartsWith("video") ? MediaType.Video : MediaType.Image
                });
            }

            await _postRepository.Create(post);

            return post;
        }
        private async Task<string> SaveFile(IFormFile file)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath, "uploads"
            );

            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/" + fileName;
        }
        public async Task<Post?> GetPost(string id)
        {
            return await _postRepository.GetById(id);
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _postRepository.GetAll();
        }
    }
}
