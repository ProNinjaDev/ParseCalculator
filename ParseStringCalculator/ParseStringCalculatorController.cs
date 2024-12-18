using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ParseStringCalculator
{
    public class ParseStringCalculatorController (string expression)
    {
        private string[] _expression = expression.Split();
        private string InfixExpression = expression;
        private string PostfixExpression;

        private readonly Dictionary<char, int> operationPriority = new() 
        {
            { '(', 0 },
            { '+', 1 },
            { '-', 1 },
            { '*', 2 },
            { '/', 2 },
            { '^', 3 },
            { '~', 4 }
        };

        public string Calculate()
        {
            string postfixExpression = ToPostfix(expression + "\r");
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

        private string ToPostfix(string infixExpression)
        {
            string postfixExpression = "";
            Stack<char> stack = new();

            for (int i = 0; i < infixExpression.Length; i++)
            {
                char c = infixExpression[i];

                if (char.IsDigit(c) || c == ',')
                {
                    postfixExpression += GetStringNumber(infixExpression, ref i) + " ";
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                        postfixExpression += stack.Pop() + " ";
                    stack.Pop();
                }
                else if (operationPriority.ContainsKey(c))
                {
                    char op = c;

                    if (op == '-' && (i == 0 || (i > 1 && operationPriority.ContainsKey(infixExpression[i - 1]))))
                        op = '~';

                    while (stack.Count > 0 && operationPriority[stack.Peek()] >= operationPriority[op])
                        postfixExpression += stack.Pop() + " ";
                    stack.Push(op);
                }
            }

            foreach (char op in stack)
                postfixExpression += op + " ";

            Console.WriteLine($"Postfix Expression: {postfixExpression.Trim()}");
            return postfixExpression.Trim();
        }

        private string GetStringNumber(string infixExpression, ref int i)
        {
            StringBuilder strNumber = new();

            for (; i < infixExpression.Length; i++)
            {
                char num = infixExpression[i];

                if (char.IsDigit(num) || num == ',')
                    strNumber.Append(num);
                else
                {
                    i--;
                    break;
                }
            }

            return strNumber.ToString();
        }
    }
}


