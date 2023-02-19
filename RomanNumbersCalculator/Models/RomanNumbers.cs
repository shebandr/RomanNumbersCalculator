using System;

namespace RomanNumbersCalculator.Models
{
    internal class RomanNumber : ICloneable, IComparable<RomanNumber>
    {
        private ushort number = 1;
        public RomanNumber(ushort num)
        {   if(num > 3999 || num < 1) {
                throw new RomanNumberException();
            } else
            {
                number = num;

            }
        }
        public RomanNumber(string num)
        {
            number = (ushort)FromRoman(num);
        }
        private static int romanValue(int index)
        {
            int basefactor = ((index % 2) * 4 + 1);
            return index > 1 ? (int)(basefactor * System.Math.Pow(10.0, index / 2)) : basefactor;
        }

        public static int FromRoman(string roman)
        {
            roman = roman.ToLower();
            string literals = "mdclxvi";
            int value = 0, index = 0;
            foreach (char literal in literals)
            {
                value = romanValue(literals.Length - literals.IndexOf(literal) - 1);
                index = roman.IndexOf(literal);
                if (index > -1) {
                    int a = FromRoman(roman.Substring(index + 1)) + (index > 0 ? value - FromRoman(roman.Substring(0, index)) : value);
                    if (a > 3999 || a < 1 )
                    {
                        throw new RomanNumberException();
                    } else
                    {
                        return a;
                    }
                }
            }
            return 0;
        }
        public static RomanNumber Add(RomanNumber romanNumber1, RomanNumber romanNumber2)
        {
            return new RomanNumber((ushort)(romanNumber1.ToUInt16() + romanNumber2.ToUInt16()));
        }
        public static RomanNumber Sub(RomanNumber romanNumber1, RomanNumber romanNumber2)
        {
            return new RomanNumber((ushort)(romanNumber1.ToUInt16() - romanNumber2.ToUInt16()));
        }
        public static RomanNumber Mul(RomanNumber romanNumber1, RomanNumber romanNumber2)
        {
            return new RomanNumber((ushort)(romanNumber1.ToUInt16() * romanNumber2.ToUInt16()));
        }
        public static RomanNumber Div(RomanNumber romanNumber1, RomanNumber romanNumber2)
        {
            return new RomanNumber((ushort)(romanNumber1.ToUInt16() / romanNumber2.ToUInt16()));
        }
        public object Clone()
        {
            return new RomanNumber(number);
        }

        public int CompareTo(RomanNumber? other)
        {
            if (other.ToUInt16() > number)
            {
                if (other == null) return 1;
                RomanNumber othernum = other as RomanNumber;
                if (othernum != null)
                    return this.number.CompareTo(othernum.ToUInt16());
                else
                    throw new ArgumentException("invalid object");
            }
            throw new NotImplementedException();
        }
        public string ToString()
        {
            return ToRoman(number);
        }
        public ushort ToUInt16()
        {
            return number;
        }
        private string ToRoman(int num)
        {
            if ((num < 0) || (num > 3999)) throw new RomanNumberException();
            if (num < 1) return string.Empty;
            if (num >= 1000) return "M" + ToRoman(num - 1000);
            if (num >= 900) return "CM" + ToRoman(num - 900);
            if (num >= 500) return "D" + ToRoman(num - 500);
            if (num >= 400) return "CD" + ToRoman(num - 400);
            if (num >= 100) return "C" + ToRoman(num - 100);
            if (num >= 90) return "XC" + ToRoman(num - 90);
            if (num >= 50) return "L" + ToRoman(num - 50);
            if (num >= 40) return "XL" + ToRoman(num - 40);
            if (num >= 10) return "X" + ToRoman(num - 10);
            if (num >= 9) return "IX" + ToRoman(num - 9);
            if (num >= 5) return "V" + ToRoman(num - 5);
            if (num >= 4) return "IV" + ToRoman(num - 4);
            if (num >= 1) return "I" + ToRoman(num - 1);
            return "";
        }
    }
}
