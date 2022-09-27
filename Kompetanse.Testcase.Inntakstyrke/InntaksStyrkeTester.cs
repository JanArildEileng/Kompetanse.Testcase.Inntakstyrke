namespace Kompetanse.Testcase.Inntakstyrke;

/*
 * En streng ' inntaksStyrke' , skal deles opp i opptil 4 deler :
 *   - teller , tellerEnhet , nevner , nevnerEnhet    
 *  
 *    f.eks    '1g/2ml'    =>  teller=1 ,tellerEnhet=g , nevner=2 ,  nevnerEnhet=ml
 *    streng kan også inneholde flere verdier, disse skal da splittes med '|' 
 *    -  '2+1'  =>   teller= 2|1    
 */

public class InntaksStyrkeTester
{
    public InntaksStyrkeTester()
    {

    }

    /* alle testcase-data er hentet fra Reseptregisterets produksjonssystem */
    [Theory]
    [InlineData("", null, null, null, null)]
    [InlineData("100/25mg", "100", null, "25", "mg")]
    [InlineData("20mcg/ml", "20", "mcg", "1", "ml")]
    [InlineData("25 + 100 + 200", "25|100|200", null, null, null)]
    [InlineData("150mg+1%", "150|1", "mg|%", null, null)]
    [InlineData("0,2mg/ml", "0,2", "mg", "1", "ml")]
    [InlineData("16/12,5mg", "16", null, "12,5", "mg")]

    public void SplitInntaksStyrkeTest(string inntaksStyrke, string expectedTeller, string expectedTellerEnhet, string expectedNevner, string expectedNevnerEnhet)
    {
        (string teller, string tellerEnhet, string nevner, string nevnerEnhet) = InntaksStyrkeSplitter(inntaksStyrke);

        Assert.Equal(expectedTeller, teller);
        Assert.Equal(expectedTellerEnhet, tellerEnhet);
        Assert.Equal(expectedNevner, nevner);
        Assert.Equal(expectedNevnerEnhet, nevnerEnhet);
    }
  
    private (string teller, string tellerEnhet, string nevner, string nevnerEnhet) InntaksStyrkeSplitter(string inntaksStyrke)
    {
        //TODO implementer denne !!!
        //Denne koden kan flyttes i en egen klasse eller hva som helst....

        throw new NotImplementedException("Implementer InntaksStyrkeSplitter() !!!");
      //  return (null, null, null, null);
    }

}