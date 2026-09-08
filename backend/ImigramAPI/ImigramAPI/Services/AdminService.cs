using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;

namespace ImigramAPI.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepostitory;
        private readonly IUserRepository _userRepository;
        public AdminService(IUserService userService, IPostService postService, IUserRepository userRepository, IPostRepository postRepository)
        {
            _userService = userService;
            _postService = postService;
            _userRepository = userRepository;
            _postRepostitory = postRepository;
        }
        public async Task BanUser(string userId)
        {
            var user = await _userRepository.GetById(userId);

            user.IsBanned = true;

            await _userRepository.Update(user);
        }
        public async Task DeletePost(string postId)
        {
            var post = await _postRepostitory.GetById(postId);
            post.IsDeleted = true;
            await _postRepostitory.Update(post);
        }
        public async Task UnbanUser(string userId)
        {
            var user = await _userRepository.GetById(userId);
            Console.WriteLine($"User {userId} has been unbanned");
            user.IsBanned = false;

            await _userRepository.Update(user);
        }
    }
}
