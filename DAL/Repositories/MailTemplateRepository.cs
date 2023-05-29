using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class MailTemplateRepository : IMailTemplateRepository
    {
        private readonly AuthServerContext _context;

        public MailTemplateRepository(AuthServerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(MailTemplate entity)
        {
            await _context.MailTemplates.AddAsync(entity);
        }

        public Task<MailTemplate?> ReadAsync(int id)
        {
            return _context.MailTemplates.FirstOrDefaultAsync(mailTemplate => mailTemplate.Id == id);
        }

        public async Task<IEnumerable<MailTemplate>> ReadByConditionAsync(Expression<Func<MailTemplate, bool>> predicate)
        {
            IEnumerable<MailTemplate> mailTemplates = await _context.MailTemplates.Where(predicate).ToListAsync();
            return mailTemplates;
        }

        public async Task UpdateAsync(MailTemplate entity)
        {
            MailTemplate? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<MailTemplate> entityEntry = _context.Entry(oldEntity);
                entityEntry.CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            MailTemplate? mailTemplate = await ReadAsync(id);
            if (mailTemplate != null)
            {
                _context.MailTemplates.Remove(mailTemplate);
            }
        }
    }
}