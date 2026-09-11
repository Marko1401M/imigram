using ImigramAPI.DTOs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;
using System.ComponentModel.Design;

namespace ImigramAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly INotificationService _notificationService;
        private readonly IPostService _postService;
        private readonly IPostRepository _postRepository;
        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, INotificationService notificationService, IPostService postService, IPostRepository postRepository )
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _notificationService = notificationService;
            _postService = postService;
            _postRepository = postRepository;
        }
        public async Task<Comment> AddComment(AddCommentDto dto)
        {
            var comment = new Comment
            {
                Content = dto.Content,
                UserId = dto.UserId,
                PostId = dto.PostId,
                CreatedAt = DateTime.UtcNow
            };
            await _commentRepository.Create(comment);

            var post = await _postRepository.GetById(dto.PostId);
            var user = await _userRepository.GetById(dto.UserId);
            await _notificationService.Create(post.UserId, dto.UserId, "Comment", $"{user.FirstName} je komentarisao Vašu objavu!", post.Id, comment.Id);
            post.CommentsCount += 1;
            await _postRepository.Update(post);
            return comment;
        }

        public async Task<bool> DeleteComment(string commentId)
        {
            await _commentRepository.Delete(commentId);

            return true;
        }

        public async Task<CommentResponseDto> GetComment(string commentId)
        {
            var comment = await _commentRepository.GetById(commentId);

            var user = await _userRepository.GetById(comment.UserId);

            var response = new CommentResponseDto
            {
                UserId = comment.UserId,
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                PostId = comment.PostId,
                FullName = user.FirstName + " " + user.LastName,
                ProfileImage = user.ProfileImage,
            };
            return response;
        }

        public async Task<List<CommentResponseDto>> GetComments(string postId)
        {
            var comments = await _commentRepository.GetByPostId(postId);
            List<CommentResponseDto> response = new List<CommentResponseDto>();
            foreach (var comment in comments)
            {

                var user = await _userRepository.GetById(comment.UserId);

                var res = new CommentResponseDto
                {
                    UserId = comment.UserId,
                    Id = comment.Id,
                    Content = comment.Content,
                    CreatedAt = comment.CreatedAt,
                    PostId = comment.PostId,
                    FullName = user.FirstName + " " + user.LastName,
                    ProfileImage = user.ProfileImage,
                };

                response.Add(res);
            }
            return response;
        }
    }
}
