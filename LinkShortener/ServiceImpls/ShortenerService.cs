using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinkShortener.Services;

namespace LinkShortener.ServiceImpls
{
    public class ShortenerService : IShortenerService
    {
        private const string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyz";
        private static readonly IDictionary<char, int> AlphabetIndex;
        private static readonly int Base = Alphabet.Length;

        static ShortenerService()
        {
            AlphabetIndex = Alphabet
                .Select((c, i) => new { Index = i, Char = c })
                .ToDictionary(c => c.Char, c => c.Index);
        }
        
        public string GenerateShortString(int seed)
        {
            if (seed < Base)
            {
                return Alphabet[0].ToString();
            }
            
            var str = new StringBuilder();
            var i = seed;

            while (i > 0)
            {
                str.Insert(0, Alphabet[i % Base]);
                i /= Base;
            }

            return str.ToString();
        }

        public int RestoreSeedFromString(string str)
        {
            return str.Aggregate(0, (current, c) => current * Base + AlphabetIndex[c]);
        }
    }
}