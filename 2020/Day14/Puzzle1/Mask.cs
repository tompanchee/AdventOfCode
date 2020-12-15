using System;

namespace Day14.Puzzle1
{
    class Mask
    {

        private readonly long setMask;
        private readonly long unsetMask;

        public Mask(string mask) {
            setMask = Convert.ToInt64(mask.Replace('X', '0'), 2);
            unsetMask = ~Convert.ToInt64(mask.Replace('1', 'X').Replace('0', '1').Replace('X', '0'), 2);
        }

        public long GetMaskedValue(long input) {
            var output = input | setMask;
            return output & unsetMask;
        }
    }
}