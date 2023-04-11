namespace Lang.CodeAnalysis
{
    sealed class NumberExpressionSyntax : ExpressionSyntax
    {
        private readonly SyntaxToken numberToken;

        public NumberExpressionSyntax(SyntaxToken numberToken)
        {
            NumberToken = numberToken;
        }

        public override SyntaxKind Kind => SyntaxKind.NumberExpression;
        public SyntaxToken NumberToken { get; }

        public override IEnumerable<SyntaxNode> GetChildren()
        {
            yield return NumberToken;
        }
    }

}