using System.Collections.Generic;
using Agree.Athens.Domain.Exceptions;
using Agree.Athens.Domain.ValueObjects;
using Xunit;

namespace Agree.Athens.Domain.Test.ValueObjects
{
    public class UserTagTest
    {
        public static IEnumerable<object[]> StringTestData =>
            new List<object[]>
            {
                new object[] {null},
                new object[] {""},
                new object[] {"0"},
                new object[] {"00"},
                new object[] {"000"},
                new object[] {"0000"},
                new object[] {"00000"},
                new object[] {"1", false},
                new object[] {"11", false},
                new object[] {"111", false},
                new object[] {"1111", false},
                new object[] {"11111"},
                new object[] {"1a"},
                new object[] {"1a1"},
                new object[] {"1a1a"},
                new object[] {"1a1a1"},
                new object[] {" a "},
                new object[] {" aa "},
                new object[] {" aaa "},
                new object[] {" aaaa "},
                new object[] {" aaaaa "},
                new object[] {"  "},
                new object[] {" 0 "},
                new object[] {" 00 "},
                new object[] {" 000 "},
                new object[] {" 0000 "},
                new object[] {" 00000 "},
                new object[] {" 1 ", false},
                new object[] {" 11 ", false},
                new object[] {" 111 ", false},
                new object[] {" 1111 ", false},
                new object[] {" 11111 "},
                new object[] {" 1a "},
                new object[] {" 1a1 "},
                new object[] {" 1a1a "},
                new object[] {" 1a1a1 "},
                new object[] {" a "},
                new object[] {" aa "},
                new object[] {" aaa "},
                new object[] {" aaaa "},
                new object[] {" aaaaa "}
            };
        public static IEnumerable<object[]> IntTestData =>
            new List<object[]>
            {
                new object[] {0},
                new object[] {00},
                new object[] {000},
                new object[] {0000},
                new object[] {00000},
                new object[] {1, false},
                new object[] {11, false},
                new object[] {111, false},
                new object[] {1111, false},
                new object[] {11111}
            };

        [Theory]
        [MemberData(nameof(StringTestData))]
        public void UserTagConstructorShouldValidateStringInputAndReturnValidUserTag(string tag, bool shouldThrow = true)
        {
            if (shouldThrow)
            {
                Assert.Throws<InvalidUserTagException>(() =>
                {
                    var userTag = new UserTag(tag);
                });
            }
            else
            {
                var userTag = new UserTag(tag);
                Assert.Equal(4, userTag.ToString().Length);
            }
        }

        [Theory]
        [MemberData(nameof(IntTestData))]
        public void UserTagConstructorShouldValidateIntInputAndReturnValidUserTag(int tag, bool shouldThrow = true)
        {
            if (shouldThrow)
            {
                Assert.Throws<InvalidUserTagException>(() =>
                {
                    var userTag = new UserTag(tag);
                });
            }
            else
            {
                var userTag = new UserTag(tag);
                Assert.Equal(4, userTag.ToString().Length);
            }
        }
    }
}