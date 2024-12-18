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

        [InlineData("2 * (6 - (5 - 2) / 3) / 4", "2,5")]
        [InlineData("-5 + 3", "-2")]
        [InlineData("2 ^ 3", "8")]
        [InlineData("10 / 3", "3,3333333333333335")]
        [InlineData("0 * 5", "0")]
        [InlineData("1 + 2 + 3 + 4", "10")]
        [InlineData("2 * 3 * 4 * 5", "120")]
        [InlineData("10 - 5 + 3 - 2", "6")]
        [InlineData("2 * 3 + 4 * 5", "26")]
        [InlineData("(1 + 2) * (3 + 4)", "21")]
        [InlineData(" ( 2 + 2 ) ", "4")]
        [InlineData("10,5 / 2", "5,25")]
        public void CalculatorTest(string expression, string expected)
        {
            var calculator = new ParseStringCalculatorController(expression);
            var result = calculator.Calculate();
            Assert.Equal(expected, result);

        }
    }
}