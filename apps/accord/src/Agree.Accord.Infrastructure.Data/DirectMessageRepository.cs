namespace Agree.Accord.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agree.Accord.Domain.Social;
using Agree.Accord.Domain.Social.Requests;
using Agree.Accord.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// A custom <see cref="IRepository{T}"/> implementation for <see cref="DirectMessage"/> using Entity Framework.
/// </summary>
public class DirectMessageRepository : GenericRepository<DirectMessage, Guid>, IDirectMessageRepository
{
    public DirectMessageRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<IEnumerable<DirectMessage>> SearchAsync(GetFriendChatRequest request)
    {

        var query = _dbContext
            .Set<DirectMessage>()
            .Where(dm => (dm.From.Id == request.UserId && dm.To.Id == request.FriendId) || (dm.From.Id == request.FriendId && dm.To.Id == request.UserId));

        if (request.StartAtId != Guid.Empty)
        {
            var startAt = await _dbContext.Set<DirectMessage>()
                .Where(dm => dm.Id == request.StartAtId)
                .FirstOrDefaultAsync();

            if (startAt != null)
                query = query.Where(dm => dm.CreatedAt < startAt.CreatedAt);
        }

        query = query.OrderByDescending(dm => dm.CreatedAt)
            .Take(request.PageSize)
            .Include(dm => dm.From)
            .Include(dm => dm.To)
            .Include(dm => dm.InReplyTo);

        return await query.ToListAsync();
    }

    public new async Task<DirectMessage> GetFirstAsync(Specification<DirectMessage> specification)
    {
        var query = _dbContext.Set<DirectMessage>()
            .Where(specification.Expression)
            .Include(dm => dm.From)
            .Include(dm => dm.To)
            .Include(dm => dm.InReplyTo);

        return await query.FirstOrDefaultAsync();
    }

    public new async Task<IEnumerable<DirectMessage>> GetAllAsync(Specification<DirectMessage> specification)
    {
        var query = _dbContext.Set<DirectMessage>()
            .Where(specification.Expression)
            .Include(dm => dm.From)
            .Include(dm => dm.To)
            .Include(dm => dm.InReplyTo);

        return await query.ToListAsync();
    }
}