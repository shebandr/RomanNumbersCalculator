namespace RomanNumbersCalculator.Models
{
    internal class RomanNumberExtend : RomanNumber
    {
        public RomanNumberExtend(ushort num) : base(num)
        {
        }

        public RomanNumberExtend(string num) : base(num)
        {
        }
        public static RomanNumberExtend operator +(RomanNumberExtend romanNumber1, RomanNumberExtend romanNumber2)
        {
            return new RomanNumberExtend((ushort)(romanNumber1.ToUInt16() + romanNumber2.ToUInt16()));
        }
        public static RomanNumberExtend operator -(RomanNumberExtend romanNumber1, RomanNumberExtend romanNumber2)
        {
            return new RomanNumberExtend((ushort)(romanNumber1.ToUInt16() - romanNumber2.ToUInt16()));
        }
        public static RomanNumberExtend operator *(RomanNumberExtend romanNumber1, RomanNumberExtend romanNumber2)
        {
            return new RomanNumberExtend((ushort)(romanNumber1.ToUInt16() * romanNumber2.ToUInt16()));
        }
        public static RomanNumberExtend operator /(RomanNumberExtend romanNumber1, RomanNumberExtend romanNumber2)
        {
            return new RomanNumberExtend((ushort)(romanNumber1.ToUInt16() / romanNumber2.ToUInt16()));
        }
    }
}
