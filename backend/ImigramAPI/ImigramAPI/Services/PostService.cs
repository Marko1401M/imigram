using ImigramAPI.DTOs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;
using System.Security.Claims;

namespace ImigramAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly INotificationService _notificationService;
        private readonly IFollowService _followService;
        public PostService(IPostRepository postRepository,
            IWebHostEnvironment environment,
            IUserRepository userRepository,
            ILikeRepository likeRepository,
            INotificationService notificationService,
            IFollowService followService
            )
        {
            _postRepository = postRepository;
            _environment = environment;
            _userRepository = userRepository;
            _likeRepository = likeRepository;
            _notificationService = notificationService;
            _followService = followService;
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
        public async Task<PostDto?> GetPost(string id, string userId)
        {
            var post =  await _postRepository.GetById(id);
            var user = await _userRepository.GetById(post.UserId);
 
            bool isLiked = (await _likeRepository.GetLike(post.Id, userId)) != null;
            var postDto = new PostDto
            {
                Id = id,
                UserId = user.Id,
                FullName = user.FirstName + " " + user.LastName,
                Username = user.Username,
                ProfileImage = user.ProfileImage,
                Content = post.Content,
                Media = post.Media,
                CreatedAt = post.CreatedAt,
                LikesCount = post.LikedBy.Count,
                IsLiked = isLiked,
                CommentsCount = post.CommentsCount,
                Location = post.Location

            };
            return postDto;
        }
        public async Task<List<PostDto>> GetAllPostsForUser(string userId)
        {
            List<Post> list = await _postRepository.GetByUserId(userId);
            List<PostDto> list2 = new List<PostDto>();
            foreach (var post in list)
            {
                var user = await _userRepository.GetById(post.UserId);
                bool isLiked = (await _likeRepository.GetLike(post.Id, post.UserId)) != null;
                var postDto = new PostDto
                {
                    Id = post.Id,
                    UserId = user.Id,
                    FullName = user.FirstName + " " + user.LastName,
                    Username = user.Username,
                    ProfileImage = user.ProfileImage,
                    Content = post.Content,
                    Media = post.Media,
                    CreatedAt = post.CreatedAt,
                    LikesCount = post.LikedBy.Count,
                    IsLiked = isLiked,
                    CommentsCount = post.CommentsCount,
                    Location = post.Location

                };
                list2.Add(postDto);
            }
            return list2;
        }
        public async Task<List<PostDto>> GetPosts()
        {
            List<Post> list = await _postRepository.GetAll();
            List<PostDto> list2 = new List<PostDto>();
            foreach(var post in list) {
                var user = await _userRepository.GetById(post.UserId);
                bool isLiked = (await _likeRepository.GetLike(post.Id, post.UserId)) != null;
                var postDto = new PostDto
                {
                    Id = post.Id,
                    UserId = user.Id,
                    FullName = user.FirstName + " " + user.LastName,
                    Username = user.Username,
                    ProfileImage = user.ProfileImage,
                    Content = post.Content,
                    Media = post.Media,
                    CreatedAt = post.CreatedAt,
                    LikesCount = post.LikedBy.Count,
                    IsLiked = isLiked,
                    CommentsCount = post.CommentsCount,
                    Location = post.Location

                };
                list2.Add(postDto);
            }
            return list2;
        }
        public async Task DeletePost(string postId)
        {
            await _postRepository.Delete(postId);
        }
        public async Task<List<PostDto>> GetFeed(string userId)
        {
            
            List<Post> list = await _postRepository.GetAll();
            List<PostDto> list2 = new List<PostDto>();
            foreach (var post in list)
            {
                if (post.IsDeleted) continue;
                var user = await _userRepository.GetById(post.UserId);
                bool isLiked = (await _likeRepository.GetLike(post.Id, userId)) != null;
                var follow = await _followService.IsFollowing(userId, post.UserId);
                
                if (!follow) continue;
                var postDto = new PostDto
                {
                    Id = post.Id,
                    UserId = user.Id,
                    FullName = user.FirstName + " " + user.LastName,
                    Username = user.Username,
                    ProfileImage = user.ProfileImage,
                    Content = post.Content,
                    Media = post.Media,
                    CreatedAt = post.CreatedAt,
                    LikesCount = post.LikedBy.Count,
                    IsLiked = isLiked,
                    CommentsCount = post.CommentsCount,
                    Location = post.Location,
                    IsDeleted = post.IsDeleted

                };
                list2.Add(postDto);
            }
            return list2;
        }
    }
}
