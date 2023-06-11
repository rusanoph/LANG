namespace Lang.CodeAnalysis
{
    internal abstract class BoundExpression : BoundNode
    {
        public abstract Type Type { get; }
    }
}