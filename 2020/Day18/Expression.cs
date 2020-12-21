using System;
using System.Linq;

namespace Day18
{
    // Sorry, no regex
    class Expression
    {
        readonly char[] operators = {'*', '+'};

        private string exp;
        private readonly char[] operatorPrecedence;

        public Expression(string exp, char[] operatorPrecedence = null) {
            this.exp = exp;
            this.operatorPrecedence = operatorPrecedence ?? new[] {'\0'};
        }

        public long Evaluate() {
            Expression inner;
            while ((inner = GetInnerExpression()) != null) {
                exp = exp.Replace($"({inner.exp})", inner.Evaluate().ToString());
            }

            foreach (var @operator in operatorPrecedence) {
                (int, int)? operation;
                while ((operation = GetNextOperation(exp, @operator)) != null) {
                    var o = operation.Value.Item2 > 0 ? exp[operation.Value.Item1..operation.Value.Item2] : exp[operation.Value.Item1..];
                    ReplaceOperation(operation.Value, EvaluateOperation(o));
                }
            }

            return long.Parse(exp);
        }

        void ReplaceOperation((int, int) position, string replacement) {
            var @new = exp[..position.Item1] + replacement;
            if (position.Item2 > 0) @new += exp[position.Item2..];
            exp = @new;
        }

        private Expression GetInnerExpression() {
            var start = exp.IndexOf('(');
            if (start < 0) return null;

            var inner = "";
            var count = 1;
            var i = 1;
            while (count > 0) {
                if (exp[start + i] == '(') count++;
                if (exp[start + i] == ')') count--;
                inner += exp[start + i++];
            }

            return new Expression(inner[..^1], operatorPrecedence);
        }

        private (int, int)? GetNextOperation(string e, char @operator) {
            var indexOfOperator = GetIndexOfNextOperator(e, @operator);
            if (indexOfOperator < 0) return null;
            var indexOfPreviousOperator = indexOfOperator - 1;
            while (indexOfPreviousOperator > 0) {
                if (operators.Contains(exp[indexOfPreviousOperator])) {
                    indexOfPreviousOperator++;
                    break;
                }

                indexOfPreviousOperator--;
            }

            var indexOfNextOperator = GetIndexOfNextOperator(e[(indexOfOperator + 1)..]);
            if (indexOfNextOperator < 0) return (indexOfPreviousOperator, -1);
            return (indexOfPreviousOperator, indexOfNextOperator + indexOfOperator);
        }

        private string EvaluateOperation(string e) {
            var indexOfNextOperator = GetIndexOfNextOperator(e);
            var @operator = e[indexOfNextOperator];
            var left = e[..indexOfNextOperator];
            var right = e[(indexOfNextOperator + 1)..];
            return @operator switch {
                '+' => (long.Parse(left) + long.Parse(right)).ToString(),
                '*' => (long.Parse(left) * long.Parse(right)).ToString(),
                _ => throw new InvalidOperationException()
            };
        }

        int GetIndexOfNextOperator(string e, char @operator = '\0') {
            return @operator == '\0' ? e.IndexOfAny(operators) : e.IndexOf(@operator);
        }
    }
}