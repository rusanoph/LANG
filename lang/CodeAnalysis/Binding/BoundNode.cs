namespace Lang.CodeAnalysis
{
    internal abstract class BoundNode
    {
        public abstract BoundNodeKind Kind {get; }
    }
}