namespace ExpressionEvaluator.Core;

public static class Evaluator
{
    public static double Evaluate(string expression)
    {
        var tokens = Tokenize(expression);
        var postfix = InfixToPostfix(tokens);
        return EvaluatePostfix(postfix);
    }

    private static List<string> Tokenize(string expression)
    {
        var tokens = new List<string>();
        int i = 0;

        while (i < expression.Length)
        {
            char c = expression[i];

            if (char.IsWhiteSpace(c))
            {
                i++;
                continue;
            }

            if (char.IsDigit(c) || c == '.')
            {
                string number = "";
                while (i < expression.Length && (char.IsDigit(expression[i]) || expression[i] == '.'))
                {
                    number += expression[i];
                    i++;
                }
                tokens.Add(number);
            }
            else
            {
                tokens.Add(c.ToString());
                i++;
            }
        }

        return tokens;
    }

    private static List<string> InfixToPostfix(List<string> tokens)
    {
        var output = new List<string>();
        var stack = new Stack<string>();

        foreach (var token in tokens)
        {
            if (double.TryParse(token, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                output.Add(token);
            }
            else if (token == "(")
            {
                stack.Push(token);
            }
            else if (token == ")")
            {
                while (stack.Count > 0 && stack.Peek() != "(")
                    output.Add(stack.Pop());
                if (stack.Count > 0)
                    stack.Pop();
            }
            else if (IsOperator(token))
            {
                while (stack.Count > 0 && IsOperator(stack.Peek()) &&
                       ShouldPopOperator(stack.Peek(), token))
                {
                    output.Add(stack.Pop());
                }
                stack.Push(token);
            }
        }

        while (stack.Count > 0)
            output.Add(stack.Pop());

        return output;
    }

    private static double EvaluatePostfix(List<string> postfix)
    {
        var stack = new Stack<double>();

        foreach (var token in postfix)
        {
            if (double.TryParse(token, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double number))
            {
                stack.Push(number);
            }
            else
            {
                double b = stack.Pop();
                double a = stack.Pop();
                double result = token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    "^" => Math.Pow(a, b),
                    _ => throw new InvalidOperationException($"Operador desconocido: {token}")
                };
                stack.Push(result);
            }
        }

        return stack.Pop();
    }

    private static bool IsOperator(string token) =>
        token == "+" || token == "-" || token == "*" || token == "/" || token == "^";

    private static int Precedence(string op) => op switch
    {
        "+" or "-" => 1,
        "*" or "/" => 2,
        "^" => 3,
        _ => 0
    };

    private static bool IsRightAssociative(string op) => op == "^";

    private static bool ShouldPopOperator(string top, string current)
    {
        if (IsRightAssociative(current))
            return Precedence(top) > Precedence(current);
        return Precedence(top) >= Precedence(current);
    }
}