using System;
using System.Threading;
using System.Threading.Tasks;
using Agree.Athens.Domain.Entities;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.Interfaces;
using Agree.Athens.Domain.Interfaces.Providers;
using Agree.Athens.Domain.Interfaces.Repositories;
using Agree.Athens.Domain.Specifications;
using Agree.Athens.Domain.ValueObjects;
using MediatR;

namespace Agree.Athens.Domain.Commands.Auth.CreateAccount
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHashProvider _hashProvider;

        public CreateAccountHandler(IBaseRepository<User> userRepository, IUnitOfWork unitOfWork, IHashProvider hashProvider)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _hashProvider = hashProvider;
        }

        public async Task<Guid> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var emailIsInUse = (await _userRepository.ListAsync(new UserEmailSpecification(command.Email))).Count > 0;
                if (emailIsInUse)
                {
                    throw AccountException.EmailAlreadyInUse(command.Email);
                }

                var tag = UserTag.Generate();
                var password = _hashProvider.Hash(command.Password);
                var user = new User(command.Username, command.Email, tag, password);
                await _userRepository.AddAsync(user);
                await _unitOfWork.Commit();
                return user.Id;
            }
            catch (Exception e)
            {
                await _unitOfWork.Rollback();
                throw e;
            }
        }
    }
}