using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompetanse.Testcase.Inntakstyrke
{
    public class InntaksStyrkeSplitter
    {
        public (string? numerator, string? numeratorUnit, string? denominator, string? denominatorUnit) Split(string str)
        {
            var numerator = GetNumerator(str, "01234567890.,");
            var numeratorUnit = GetNumerator(str, "abcdefghijklmnopqrstuvwxyzæøå%");
            var denominator = GetDenominator(str, "01234567890.,");
            var denominatorUnit = GetDenominator(str, "abcdefghijklmnopqrstuvwxyzæøå%");

            return (numerator, numeratorUnit, denominator, denominatorUnit);            
        }        
                
        private string? GetNumerator(string str, string allowedChars)
        {
            var backslashIndex = str.IndexOf("/");

            var numerator = SplitAndRejoin(backslashIndex > -1 ? str.Substring(0, backslashIndex) : str, allowedChars);

            return string.IsNullOrEmpty(numerator) ? null : numerator; 
        }

        private string? GetDenominator(string str, string allowedChars)
        {
            if (!HasDenominator(str))
            {
                return null; 
            }

            var backslashIndex = str.IndexOf("/");
            
            var denominator = SplitAndRejoin(backslashIndex > -1 ? str.Substring(backslashIndex) : str, allowedChars);            

            return string.IsNullOrEmpty(denominator) ? "1" : denominator;
        }

        private string SplitAndRejoin(string str, string allowedChars)
        {
            var originalListOfSplitStrings = str.Split("+");
            var newListOfSplitStrings = new List<string>();

            for (int i = 0; i < originalListOfSplitStrings.Length; i++)
            {
                var splitStr = new String(originalListOfSplitStrings[i].Where(c => allowedChars.Contains(c)).ToArray());
                if (!string.IsNullOrWhiteSpace(splitStr))
                {
                    newListOfSplitStrings.Add(splitStr);
                }
            }

            return String.Join("|", newListOfSplitStrings.ToArray());
        }       

        private bool HasDenominator(string str)
        {
            return str.Contains("/");
        }
    }
}
