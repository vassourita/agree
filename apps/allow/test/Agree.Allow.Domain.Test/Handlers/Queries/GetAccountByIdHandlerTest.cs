namespace Agree.Allow.Domain.Test.Handlers.Queries;

using Agree.Allow.Domain.Handlers.Queries;
using Agree.Allow.Domain.Requests;
using Agree.Allow.Test;
using Agree.SharedKernel.Data;

public class GetAccountByIdHandlerTest : TestBase
{
    [Fact]
    public async Task Handle_WithExistingId_ShouldReturnAccount()
    {
        // Arrange
        var account = await CreateTestUserAccount();
        var repository = Resolve<IRepository<UserAccount, Guid>>();
        var sut = new GetAccountByIdHandler(repository);

        // Act
        var result = await sut.Handle(new GetAccountByIdRequest(account.Id), CancellationToken.None);

        // Assert
        Assert.Equal(account.Id, result.Id);
    }
}