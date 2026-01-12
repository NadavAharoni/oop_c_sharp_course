namespace Classes
{
    enum Operand
    {
        ADD,
        SUB,
        MUL,
        DIV
    }
    public abstract class BaseExpression
    {
        public abstract double Evaluate();
    }

    class CompositeExpression : BaseExpression
    {
        public BaseExpression e1, e2;
        public Operand op;

        public override double Evaluate()
        {
            switch (op)
            {
                case Operand.ADD:
                    return e1.Evaluate() + e2.Evaluate();
                case Operand.SUB:
                    return e1.Evaluate() - e2.Evaluate();
                case Operand.MUL:
                    return e1.Evaluate() * e2.Evaluate();
                case Operand.DIV:
                    return e1.Evaluate() / e2.Evaluate();
                default:
                    throw new System.Exception("Unknown operand");
            }
        }
    }
    class SimpleExpression : BaseExpression
    {
        double number;
        public SimpleExpression(double number)
        {
            this.number = number;
        }
        public override double Evaluate()
        {
            return number;
        }
    }
}
