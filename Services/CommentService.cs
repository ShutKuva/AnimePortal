using AutoMapper;
using Core.DB;
using Core.DTOs.Others;
using Core.Exceptions;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Comment> CreateCommentAsync(CommentDto commentDto)
        {
            var comment = _mapper.Map<Comment>(commentDto);
            await _uow.CommentRepository.CreateAsync(comment);

            await _uow.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> UpdateCommentAsync(int commentId, string text)
        {
            var comment = await GetCommentAsync(commentId);
            comment.Text = text;
            await _uow.CommentRepository.UpdateAsync(comment);

            await _uow.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            await _uow.CommentRepository.DeleteAsync(commentId);
            await _uow.SaveChangesAsync();
        }

        public async Task<Comment> GetCommentAsync(int commentId)
        {
            Comment comment = await _uow.CommentRepository.ReadAsync(commentId) ??
                              throw new NotFoundException($"Resource with id {commentId} was not found.");
            return comment;
        }
    }
}
