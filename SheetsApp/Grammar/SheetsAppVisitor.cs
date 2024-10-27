using Antlr4.Runtime;

namespace SheetsApp
{
    public class SheetsAppVisitor : SheetsBaseVisitor<double>
    {
        private List<Cell> cells;
        public SheetsAppVisitor(Dictionary<(int, int), Cell> cells)
        {
            this.cells = new List<Cell>(cells.Values);
        }
        public double Eval(string input)
        {
            var inputStream = new AntlrInputStream(input);
            var lexer = new SheetsLexer(inputStream);
            var tokens = new CommonTokenStream(lexer);
            var parser = new SheetsParser(tokens);
            var tree = parser.expression();
            return Visit(tree);
        }
        public override double VisitPowerExpr(SheetsParser.PowerExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            return Math.Pow(left, right);
        }

        public override double VisitMultiplyExpr(SheetsParser.MultiplyExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            return left * right;
        }

        public override double VisitDivideExpr(SheetsParser.DivideExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            return left / right;
        }

        public override double VisitAddExpr(SheetsParser.AddExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            return left + right;
        }

        public override double VisitSubtractExpr(SheetsParser.SubtractExprContext context)
        {
            var left = Visit(context.expression(0));
            var right = Visit(context.expression(1));
            return left - right;
        }

        public override double VisitDecrementExpr(SheetsParser.DecrementExprContext context)
        {
            var value = Visit(context.expression());
            return value - 1;
        }

        public override double VisitIncrementExpr(SheetsParser.IncrementExprContext context)
        {
            var value = Visit(context.expression());
            return value + 1;
        }

        public override double VisitNegateExpr(SheetsParser.NegateExprContext context)
        {
            var value = Visit(context.expression());
            return -value;
        }

        public override double VisitParenExpr(SheetsParser.ParenExprContext context)
        {
            return Visit(context.expression());
        }

        private Dictionary<string, double> evaluatedValues = new Dictionary<string, double>();

        private HashSet<string> visiting = new HashSet<string>();

        public override double VisitCellExpr(SheetsParser.CellExprContext context)
        {
            string cellName = context.GetText();

            if (evaluatedValues.ContainsKey(cellName))
            {
                return evaluatedValues[cellName];
            }

            if (visiting.Contains(cellName))
            {
                throw new Exception($"Циклічне посилання на {cellName}");
            }

            visiting.Add(cellName);

            var cell = cells.Find(c => c.Name == cellName);
            if (cell is not null)
            {
                double result = Eval(cell.Expression);

                evaluatedValues[cellName] = result;

                visiting.Remove(cellName);

                return result;
            }
            else
            {
                visiting.Remove(cellName);
                throw new Exception($"Клітина {cellName} не знайдена");
            }
        }
        public override double VisitNumberExpr(SheetsParser.NumberExprContext context)
        {
            return double.Parse(context.GetText());
        }
    }
}
