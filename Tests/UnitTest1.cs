using ParseStringCalculator;


namespace ParseCalculatorTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("2 + 2", "4")]
        [InlineData("3 * 3", "9")]
        [InlineData("10 - 5", "5")]
        [InlineData("15 / 3", "5")]
        [InlineData("2 + 2 * 2", "6")]
        [InlineData("(2 + 2) * 2", "8")]
        public void CalculatorTest(string expression, string expected)
        {
            var calculator = new ParseStringCalculatorController(expression);
            var result = calculator.Calculate();
            Assert.Equal(expected, result);

        }
    }
}