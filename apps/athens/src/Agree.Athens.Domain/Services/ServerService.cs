using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Agree.Athens.Domain.Aggregates.Account;
using Agree.Athens.Domain.Aggregates.Servers;
using Agree.Athens.Domain.Aggregates.Servers.Builders;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Dtos;
using Agree.Athens.Domain.Dtos.Validators;

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

        public Task<IEnumerable<Server>> Search(UserAccount searchedBy, SearchServerDto searchServerDto)
        {
            searchServerDto.Validate(new SearchServerDtoValidator());
            if (searchServerDto.IsInvalid)
            {
                throw new DomainValidationException(searchServerDto);
            }

            searchServerDto.Query = searchServerDto.Query.Trim();
            if (string.IsNullOrEmpty(searchServerDto.Query))
            {
                throw DomainInvalidSearchException.EmptyQuery();
            }
            return _serverRepository.Search(searchServerDto);
        }

        public async Task<Server> CreateServer(UserAccount account, CreateServerDto createServerDto)
        {
            createServerDto.Validate(new CreateAccountDtoValidator());
            if (createServerDto.IsInvalid)
            {
                throw new DomainValidationException(createServerDto);
            }

            try
            {
                var server = new Server(createServerDto.Name,
                                        createServerDto.Description,
                                        (ServerPrivacy)Enum.Parse(typeof(ServerPrivacy), createServerDto.Privacy));

                var roleBuilder = new RoleBuilder()
                    .HasName("Admin")
                    .HasRandomColorHex()
                    .BelongsToServer(server)
                    .HasDefaultOwnerPermissions();

                var adminRole = roleBuilder.Build();
                server.AddRole(adminRole);

                var userBuilder = new UserBuilder()
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