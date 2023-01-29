namespace Agree.Allow.Domain.Test;

using Agree.Allow.Test;

public class DiscriminatorTagFactoryTest : TestBase
{
    [Fact]
    public async Task GenerateDiscriminatorTagAsync_WithExistingTag_ReturnsNewTag()
    {
        var sut = Resolve<DiscriminatorTagFactory>();

        var tag1 = await sut.GenerateDiscriminatorTagAsync("testuser");
        var user1 = await CreateTestUserAccount(tag1);
        for (var i = 0; i < 9999; i++)
        {
            var tag2 = await sut.GenerateDiscriminatorTagAsync("testuser");
            var user2 = await CreateTestUserAccount(tag2);

            Assert.NotEqual(tag1, tag2);
        }
    }

    [Fact]
    public async Task GenerateDiscriminatorTagAsync_WithMoreThan10000ExistingTags_ReturnsNull()
    {
        var sut = Resolve<DiscriminatorTagFactory>();

        var tag1 = await sut.GenerateDiscriminatorTagAsync("testuser");
        var user1 = await CreateTestUserAccount(tag1);
        for (var i = 0; i < 9999; i++)
        {
            var tag2 = await sut.GenerateDiscriminatorTagAsync("testuser");
            var user2 = await CreateTestUserAccount(tag2);
        }

        var tag3 = await sut.GenerateDiscriminatorTagAsync("testuser");
        Assert.Null(tag3);
    }

    [Fact]
    public async Task GenerateDiscriminatorTagAsync_WithNoExistingTags_ReturnsTag0()
    {
        var sut = Resolve<DiscriminatorTagFactory>();

        var tag = await sut.GenerateDiscriminatorTagAsync("testuser");
        Assert.Equal(0, tag.Value);
    }
}
