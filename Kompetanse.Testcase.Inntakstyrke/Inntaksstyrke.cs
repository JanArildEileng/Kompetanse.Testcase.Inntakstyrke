namespace Kompetanse.Testcase.Inntakstyrke;

public class Inntaksstyrke
{
    private static readonly char[] Digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public string? Teller => _numerator?.Values;
    public string? TellerEnhet => _numerator?.Units;
    public string? Nevner => _denominator?.Values;
    public string? NevnerEnhet => _denominator?.Units;

    private FractionPart? _numerator;
    private FractionPart? _denominator;

    public Inntaksstyrke(string inntaksstyrke)
    {
        Parse(inntaksstyrke);
    }

    private void Parse(string fraction)
    {
        if (string.IsNullOrEmpty(fraction)) return;

        string[] numeratorAndDenominator = fraction.Split('/');

        if (numeratorAndDenominator.Length <= 2)
        {
            _numerator = ParseFractionPart(numeratorAndDenominator[0]);
            if (numeratorAndDenominator.Length == 2)
                _denominator = ParseFractionPart(numeratorAndDenominator[1]);
        }
        else 
            throw new ArgumentException($"For mange '/' ({numeratorAndDenominator.Length - 1}) i inntaksstyrken: {fraction}");
    }

    private static FractionPart ParseFractionPart(string fractionPartString)
    {
        var fractionPart = new FractionPart();
        string[] addends = fractionPartString.Split('+');
        foreach (var addend in addends)
        {
            fractionPart.Add(ParseAddend(addend));
        }

        return fractionPart;
    }

    private static Addend ParseAddend(string valueAndUnit)
    {
        int indexOfLastDigit = valueAndUnit.LastIndexOfAny(Digits);
        string value = valueAndUnit.Substring(0, indexOfLastDigit + 1);
        string unit = valueAndUnit.Substring(indexOfLastDigit + 1, valueAndUnit.Length - (indexOfLastDigit + 1));
        return new Addend(value, unit);
    }

    private class FractionPart
    {
        internal string Values { get; private set; }
        internal string? Units { get; private set; }

        internal FractionPart()
        {
            Values = string.Empty;
        }

        internal void Add(Addend addend)
        {
            if (!string.IsNullOrEmpty(Values)) Values += "|";
            Values += addend.Value;

            if (string.IsNullOrEmpty(addend.Unit)) return;
            if (!string.IsNullOrEmpty(Units)) Units += "|";
            Units += addend.Unit;
        }

    }

    private class Addend
    {
        internal string Value { get; }
        internal string? Unit { get; }

        public Addend(string value, string unit)
        {
            value = value.Trim();
            Value = string.IsNullOrEmpty(value) ? "1" : value;
            unit = unit.Trim();
            Unit = string.IsNullOrEmpty(unit) ? null : unit;
        }
    }
}