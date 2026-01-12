using System;
using System.Globalization;

namespace Classes
{
    public class ExpressionParser
    {
        private readonly string _text;
        private int _pos;

        public ExpressionParser(string text)
        {
            _text = text ?? string.Empty;
            _pos = 0;
        }

        public static BaseExpression Parse(string text)
        {
            var parser = new ExpressionParser(text);
            var expr = parser.ParseExpression();
            parser.SkipWhitespace();
            if (!parser.End)
            {
                throw new FormatException($"Unexpected character at position {parser._pos}: '{parser.Current}'");
            }
            return expr;
        }

        private BaseExpression ParseExpression()
        {
            var left = ParseTerm();
            while (true)
            {
                SkipWhitespace();
                if (Match('+'))
                {
                    var right = ParseTerm();
                    left = new CompositeExpression { e1 = left, e2 = right, op = Operand.ADD };
                }
                else if (Match('-'))
                {
                    var right = ParseTerm();
                    left = new CompositeExpression { e1 = left, e2 = right, op = Operand.SUB };
                }
                else
                {
                    break;
                }
            }
            return left;
        }

        private BaseExpression ParseTerm()
        {
            var left = ParseFactor();
            while (true)
            {
                SkipWhitespace();
                if (Match('*'))
                {
                    var right = ParseFactor();
                    left = new CompositeExpression { e1 = left, e2 = right, op = Operand.MUL };
                }
                else if (Match('/'))
                {
                    var right = ParseFactor();
                    left = new CompositeExpression { e1 = left, e2 = right, op = Operand.DIV };
                }
                else
                {
                    break;
                }
            }
            return left;
        }

        private BaseExpression ParseFactor()
        {
            SkipWhitespace();

            // Unary plus
            if (Match('+'))
            {
                return ParseFactor();
            }

            // Unary minus
            if (Match('-'))
            {
                var operand = ParseFactor();
                return new CompositeExpression { e1 = new SimpleExpression(0), e2 = operand, op = Operand.SUB };
            }

            if (Match('('))
            {
                var expr = ParseExpression();
                SkipWhitespace();
                if (!Match(')'))
                {
                    throw new FormatException($"Missing closing parenthesis at position {_pos}");
                }
                return expr;
            }

            return ParseNumber();
        }

        private BaseExpression ParseNumber()
        {
            SkipWhitespace();
            int start = _pos;
            if (Match('+') || Match('-'))
            {
                // If there was a sign attached to a number (not unary), backtrack so we can handle it in the number parsing loop
                _pos = start;
            }

            // Build number string: digits, optional decimal point, optional exponent
            var sb = new System.Text.StringBuilder();
            bool seenDigit = false;

            while (!End && char.IsDigit(Current))
            {
                seenDigit = true;
                sb.Append(Current);
                _pos++;
            }

            if (!End && Current == '.')
            {
                sb.Append(Current);
                _pos++;
                while (!End && char.IsDigit(Current))
                {
                    seenDigit = true;
                    sb.Append(Current);
                    _pos++;
                }
            }

            if (!End && (Current == 'e' || Current == 'E'))
            {
                sb.Append(Current);
                _pos++;
                if (!End && (Current == '+' || Current == '-'))
                {
                    sb.Append(Current);
                    _pos++;
                }

                bool expDigits = false;
                while (!End && char.IsDigit(Current))
                {
                    expDigits = true;
                    sb.Append(Current);
                    _pos++;
                }

                if (!expDigits)
                {
                    throw new FormatException($"Invalid exponent at position {_pos}");
                }
            }

            if (!seenDigit)
            {
                throw new FormatException($"Number expected at position {start}");
            }

            var s = sb.ToString();
            if (!double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double value))
            {
                throw new FormatException($"Invalid number '{s}' at position {start}");
            }

            return new SimpleExpression(value);
        }

        private void SkipWhitespace()
        {
            while (!End && char.IsWhiteSpace(Current)) _pos++;
        }

        private bool Match(char c)
        {
            if (!End && Current == c)
            {
                _pos++;
                return true;
            }
            return false;
        }

        private char Current => End ? '\0' : _text[_pos];
        private bool End => _pos >= _text.Length;
    }
}
