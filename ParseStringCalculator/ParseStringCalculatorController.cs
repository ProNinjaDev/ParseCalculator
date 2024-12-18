using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParseStringCalculator
{
    public class ParseStringCalculatorController
    {
        private readonly string _infixExpression;

        private readonly Dictionary<char, int> OperationPriority = new() 
        {
            { '(', 0 },
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '^', 3 },
            { '~', 4 }
        };

        public ParseStringCalculatorController(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("Выражение не может быть пустым!", nameof(expression));

            _infixExpression = expression;
        }

        public string Calculate()
        {
            string postfixExpression = ConvertToPostfix(_infixExpression + "\r");
            Stack<double> stack = new();

            string[] operators = postfixExpression.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string oper in operators)
            {
                if (double.TryParse(oper, out double number))
                {
                    stack.Push(number);
                }
                else
                {
                    double operand2 = stack.Pop();
                    double operand1 = stack.Count > 0 ? stack.Pop() : 0;

                    switch (oper)
                    {
                        case "+":
                            stack.Push(operand1 + operand2);
                            break;
                        case "-":
                            stack.Push(operand1 - operand2);
                            break;
                        case "*":
                            stack.Push(operand1 * operand2);
                            break;
                        case "/":
                            stack.Push(operand1 / operand2);
                            break;
                        case "^":
                            stack.Push(Math.Pow(operand1, operand2));
                            break;
                        case "~":
                            stack.Push(-operand2);
                            break;
                        default:
                            throw new ArgumentException($"Unknown operator: {oper}");
                    }
                }
            }
            return stack.Pop().ToString();
        }

        private string ConvertToPostfix(string infixExpression)
        {
            StringBuilder postfixExpression = new();
            Stack<char> stack = new();

            for (int i = 0; i < infixExpression.Length; i++)
            {
                char c = infixExpression[i];

                switch (c)
                {
                    case char digit when (char.IsDigit(digit) || digit == ','):
                        ProcessDigitOrComma(infixExpression, postfixExpression, ref i);
                        break;
                    case '(':
                        ProcessOpeningParenthesis(stack);
                        break;
                    case ')':
                        ProcessClosingParenthesis(postfixExpression, stack);
                        break;
                    case char op when OperationPriority.ContainsKey(op):
                        ProcessOperator(infixExpression, postfixExpression, stack, ref i, c);
                        break;
                    case char whiteSpace when char.IsWhiteSpace(whiteSpace):
                        break;
                    default:
                        throw new ArgumentException($"Unexpected character: {c}");
                }
            }

            while (stack.Count > 0)
                postfixExpression.Append(stack.Pop()).Append(" ");

            return postfixExpression.ToString().Trim();
        }

        private void ProcessOperator(string infixExpression, StringBuilder postfixExpression, Stack<char> stack, ref int i, char c)
        {
            char operatorChar = c;

            if (operatorChar == '-' && (i == 0 || (i > 1 && OperationPriority.ContainsKey(infixExpression[i - 1]))))
                operatorChar = '~';

            while (stack.Count > 0 && OperationPriority[stack.Peek()] >= OperationPriority[operatorChar])
                postfixExpression.Append(stack.Pop()).Append(" ");
            stack.Push(operatorChar);
        }

        private void ProcessClosingParenthesis(StringBuilder postfixExpression, Stack<char> stack)
        {
            while (stack.Count > 0 && stack.Peek() != '(')
                postfixExpression.Append(stack.Pop()).Append(" ");
            stack.Pop();
        }

        private static void ProcessOpeningParenthesis(Stack<char> stack)
        {
            stack.Push('(');
        }

        private void ProcessDigitOrComma(string infixExpression, StringBuilder postfixExpression, ref int i)
        {
            postfixExpression.Append(GetStringNumber(infixExpression, ref i)).Append(" ");
        }

        private string GetStringNumber(string expression, ref int index)
        {
            StringBuilder strNumber = new();

            for (; index < expression.Length; index++)
            {
                char currentChar = expression[index];

                if (char.IsDigit(currentChar) || currentChar == ',')
                    strNumber.Append(currentChar);
                else
                {
                    index--;
                    break;
                }
            }

            return strNumber.ToString();
        }
    }
}


