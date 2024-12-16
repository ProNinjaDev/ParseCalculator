using ParseStringCalculator;


namespace ParseCalculatorTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var calculator = new ParseStringCalculatorController("2 + 2");
            var result = calculator.Calculate();
            Assert.Equal("4", result);

        }
    }
}