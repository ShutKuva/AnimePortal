using Core.DB;
using Core.DTOs.Others;

namespace Services.Abstraction.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(CommentDto commentDto); 
        Task<Comment> UpdateCommentAsync(int commentId, string text);
        Task DeleteCommentAsync(int commentId);
        Task<Comment> GetCommentAsync(int commentId);
    }
}
