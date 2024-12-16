using ParseStringCalculator;

string expression = "2 * (6 - (5 - 2) / 3) / 4";
ParseStringCalculatorController calculator = new(expression);
string result = calculator.Calculate();
Console.WriteLine($"Результат: {result}");
