using System;

namespace RomanNumbersCalculator.Models
{
    internal class RomanNumberException : Exception
    {
        public RomanNumberException(string message = "#ERROR") : base(message) { }
    }
}

