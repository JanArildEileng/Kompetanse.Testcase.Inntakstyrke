using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompetanse.Testcase.Inntakstyrke
{
    public class InntaksStyrke
    {
        private const string _allowedCharsInNumber = "01234567890.,";
        private const string _allowedCharsInUnit = "abcdefghijklmnopqrstuvwxyzæøå%";

        public string? Teller;
        public string? TellerEnhet;
        public string? Nevner;
        public string? NevnerEnhet;

        public InntaksStyrke(string str)
        {
            if(str == null) throw new ArgumentNullException("str");

            this.Teller = GetNumerator(str, _allowedCharsInNumber);
            this.TellerEnhet = GetNumerator(str, _allowedCharsInUnit);
            this.Nevner = GetDenominator(str, _allowedCharsInNumber);
            this.NevnerEnhet = GetDenominator(str, _allowedCharsInUnit);
        }
                
        private string? GetNumerator(string str, string allowedChars)
        {
            if (HasDenominator(str))
            {                
                str = str.Substring(0, str.IndexOf("/"));
            }
           
            var numerator = SplitAndRejoin(str, allowedChars);

            return string.IsNullOrEmpty(numerator) ? null : numerator; 
        }

        private string? GetDenominator(string str, string allowedChars)
        {
            if (HasDenominator(str))
            {
                str = str.Substring(str.IndexOf("/"));

                var denominator = SplitAndRejoin(str, allowedChars);

                return string.IsNullOrEmpty(denominator) ? "1" : denominator;
            }

            return null; 
        }

        private string SplitAndRejoin(string str, string allowedChars)
        {
            var strArray = str.Split("+");
            var strList = new List<string>();

            for (int i = 0; i < strArray.Length; i++)
            {
                var result = new String(strArray[i].Where(c => allowedChars.Contains(c)).ToArray());

                if (!string.IsNullOrWhiteSpace(result))
                {
                    strList.Add(result);
                }
            }

            return String.Join("|", strList.ToArray());
        }       

        private bool HasDenominator(string str)
        {
            return str.Contains("/");
        }
    }
}
