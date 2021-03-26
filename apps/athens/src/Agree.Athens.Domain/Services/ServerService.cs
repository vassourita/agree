using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Aggregates.Servers.Builders;
using Agree.Athens.Domain.Interfaces.Repositories;

namespace Agree.Athens.Domain.Services
{
    public class ServerService
    {
        private readonly IServerRepository _serverRepository;

        public ServerService(IServerRepository serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<Server> CreateServer(UserAccount account, string serverName, string serverDescription)
        {
            try
            {
                var server = new Server(serverName, serverDescription);

                var roleBuilder = new RoleBuilder();
                roleBuilder
                    .HasName("Admin")
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