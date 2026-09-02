using ImigramAPI.DTOs;
using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> AddComment(AddCommentDto dto);
        Task<List<CommentResponseDto>> GetComments(string postId);
        Task<CommentResponseDto> GetComment(string commentId);
        Task<bool> DeleteComment(string commentId);
    }
}
