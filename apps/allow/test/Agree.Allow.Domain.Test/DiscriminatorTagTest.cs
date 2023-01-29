namespace Agree.Allow.Domain.Test;

public class DiscriminatorTagTest
{
    [Theory]
    [InlineData(0, true)]
    [InlineData("0000", true)]
    [InlineData(1, true)]
    [InlineData("1", true)]
    [InlineData("01", true)]
    [InlineData("001", true)]
    [InlineData("0001", true)]
    [InlineData("00001", false)]
    [InlineData(11, true)]
    [InlineData(111, true)]
    [InlineData(1111, true)]
    [InlineData("0111", true)]
    [InlineData(11111, false)]
    [InlineData("01111", false)]
    [InlineData("", false)]
    [InlineData(" ", false)]
    [InlineData("a", false)]
    [InlineData("a1", false)]
    public void TryParse_ShouldParseValuesCorrectly(object value, bool isValidTag)
    {
        if (DiscriminatorTag.TryParse(value, out var tag))
        {
            if (isValidTag)
            {
                Assert.Equal(value.ToString().PadLeft(4, '0'), tag.ToString());
                Assert.Equal(ushort.Parse(value.ToString()), tag.Value);
            }
            else
            {
                Assert.Equal("0000", tag.ToString());
                Assert.Equal(0, tag.Value);
            }
        }
    }

    [Fact]
    public void NewTag_ShouldReturnTagBetween1And9999()
    {
        for (var i = 0; i < 1000000; i++)
        {
            var tag = DiscriminatorTag.NewTag();
            Assert.True(tag.Value >= 1);
            Assert.True(tag.Value <= 9999);
        }
    }
}
