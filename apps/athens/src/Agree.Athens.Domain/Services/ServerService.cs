using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Aggregates.Servers.Builders;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.SharedKernel.Data;
using Agree.Athens.Domain.Exceptions;

namespace Agree.Athens.Domain.Services
{
    public class ServerService
    {
        private readonly IServerRepository _serverRepository;
        private readonly IAccountRepository _accountRepository;

        public ServerService(IServerRepository serverRepository, IAccountRepository accountRepository)
        {
            _serverRepository = serverRepository;
            _accountRepository = accountRepository;
        }

        public Task<IEnumerable<Server>> Search(UserAccount searchedBy, string query, string orderBy, Paginated paginated)
        {
            query = query.Trim();
            if (string.IsNullOrEmpty(query))
            {
                throw DomainInvalidSearchException.EmptyQuery(query);
            }
            return _serverRepository.Search(query, orderBy, paginated);
        }

        public async Task<Server> CreateServer(UserAccount account, string serverName, string serverDescription, ServerPrivacy privacy = ServerPrivacy.Public)
        {
            try
            {
                var server = new Server(serverName, serverDescription, privacy);

                var roleBuilder = new RoleBuilder();
                roleBuilder
                    .HasName("Admin")
                    .HasRandomColorHex()
                    .BelongsToServer(server)
                    .HasDefaultOwnerPermissions();

                var adminRole = roleBuilder.Build();
                server.AddRole(adminRole);

                var userBuilder = new UserBuilder();
                userBuilder
                    .FromUserAccount(account)
                    .IsMemberOfServer(server)
                    .HasRole(adminRole);

                var adminUser = userBuilder.Build();
                server.AddUser(adminUser);

                var defaultCategory = new Category("Geral", server);
                var defaultChannel = new TextChannel("Boas Vindas!", defaultCategory);
                defaultCategory.AddTextChannel(defaultChannel);
                server.AddCategory(defaultCategory);

                await _serverRepository.AddAsync(server);
                await _serverRepository.UnitOfWork.Commit();

                return server;
            }
            catch (Exception ex)
            {
                await _serverRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }
    }
}