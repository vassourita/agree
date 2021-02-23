using System;

namespace Agree.Athens.Domain.Aggregates.Account.Factories
{
    public static class UserTagFactory
    {
        public static UserTag CreateRandomUserTag()
        {
            var random = new Random().Next(1, 9999);
            var formatted = random.ToString().PadLeft(4, '0');
            return new UserTag(formatted);
        }

        public static UserTag FromInteger(int tag)
        {
            var formatted = tag.ToString().PadLeft(4, '0');
            return new UserTag(formatted);
        }

        public static UserTag FromString(string tag)
        {
            return new UserTag(tag);
        }
    }
}