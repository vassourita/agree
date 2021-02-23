using System;

namespace Agree.Athens.Domain.Aggregates.Servers.Factories
{
    public class ColorHexFactory
    {
        public static ColorHex CreateRandomColorHex()
        {
            var random = new Random();
            var hex1 = random.Next(1, 255);
            var hex2 = random.Next(1, 255);
            var hex3 = random.Next(1, 255);
            return new ColorHex($"{hex1}{hex2}{hex3}");
        }

        public static ColorHex FromInteger(int hex1, int hex2, int hex3)
        {
            return new ColorHex($"{hex1}{hex2}{hex3}");
        }

        public static ColorHex FromString(string str)
        {
            return new ColorHex(str);
        }
    }
}