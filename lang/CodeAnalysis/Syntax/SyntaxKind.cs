namespace Lang.CodeAnalysis.Syntax
{
    public enum SyntaxKind
    {
        // Tokens
        BadToken,
        EndOfFileToken,
        WhiteSpaceToken,
        NumberToken,
        PlusToken,
        MinusToken,
        StarToken,
        SlashToken,
        BangToken,
        AmpersandAmpersandToken,
        PipePipeToken,
        OpenParenthesisToken,
        CloseParenthesisToken,
        IdentifierToken,

        // Keywords
        TrueKeyword,
        FalseKeyword,

        // Expressions tokens
        LiteralExpression,
        UnaryExpression,
        BinaryExpression,
        ParenthesizedExpression,
        
    }
}