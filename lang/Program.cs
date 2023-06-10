using System;
using Lang.CodeAnalysis;

namespace Lang 
{
    internal static class Program 
    {
        private static void Main(string[] args) 
        {
            var showTree = false;
            var showTokens = false;

            while (true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();

                // End if input is empty or whitespaced
                if (string.IsNullOrWhiteSpace(line))
                    return;

                // REPL API
                if (line == "#showTree")
                {
                    // Enable showing parse trees
                    showTree = !showTree;
                    Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees");
                    continue;
                }
                else if (line == "#showTokens")
                {
                    // Enable lexical analysis. Showing tokens
                    showTokens = !showTokens;
                    Console.WriteLine(showTokens ? "Showing tokens." : "Not showing tokens.");
                    continue;
                }
                else if (line == "#cls")
                {
                    Console.Clear();
                    continue;
                }
                else if (line.StartsWith("#"))
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ERROR: Unknown command.");
                    Console.ForegroundColor = color;
                }

                // PARSER part
                var syntaxTree = SyntaxTree.Parse(line);

                // Print parse tree if enabled
                if (showTree)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    PrettyPrint(syntaxTree.Root);
                    Console.ResetColor();
                }

                if (showTokens)
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    TokensPrint(line);
                    Console.ResetColor();
                }
                
                // Evalution of expression
                if (!syntaxTree.Diagnostics.Any())
                {
                    var e = new Evaluator(syntaxTree.Root);
                    var result = e.Evaluate();
                    Console.WriteLine(result);
                }
                // Error processing
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;

                    foreach (var diagnostic in syntaxTree.Diagnostics)
                        Console.WriteLine(diagnostic);
                    
                    Console.ResetColor();
                }
            }
        }

        static void PrettyPrint(SyntaxNode node, string indent="", bool isLast=true)
        {
            // └──
            // ├──
            // │

            var marker = isLast ? "└──" : "├──";

            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);
            
            if (node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }
            Console.WriteLine();

            indent += isLast ? "    " : "│   ";
            
            var lastChild = node.GetChildren().LastOrDefault();

            foreach (var child in node.GetChildren())
                PrettyPrint(child, indent, child == lastChild);
        }
        
        static void TokensPrint(string line)
        {
            var lexer = new Lexer(line);
            while (true)
            {
                var token = lexer.NextToken();
                if (token.Kind == SyntaxKind.EndOfFileToken)
                    break;

                Console.Write($"{token.Kind}: '{token.Text}'");
                if (token.Value != null)
                    Console.Write($" {token.Value}");
                
                Console.WriteLine();
            }
        }
    }

}