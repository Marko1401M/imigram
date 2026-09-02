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
        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
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
