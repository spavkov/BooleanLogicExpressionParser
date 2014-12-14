using System;
using System.Linq;
using NUnit.Framework;

namespace BooleanLogicParser.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        [TestCase("And", ExpectedResult = typeof(AndToken))]
        [TestCase("and", ExpectedResult = typeof(AndToken))]
        [TestCase("Or", ExpectedResult = typeof(OrToken))]
        [TestCase("or", ExpectedResult = typeof(OrToken))]
        [TestCase("True", ExpectedResult = typeof(TrueToken))]
        [TestCase("False", ExpectedResult = typeof(FalseToken))]
        [TestCase("!", ExpectedResult = typeof(NegationToken))]
        [TestCase("(", ExpectedResult = typeof(OpenParenthesisToken))]
        [TestCase(")", ExpectedResult = typeof(ClosedParenthesisToken))]
        [TestCase("a", ExpectedException = typeof(Exception))]
        [TestCase("(trae)", ExpectedException = typeof(Exception))]
        public Type CanParseSingleToken(string expression)
        {
            var tokens = new Tokenizer(expression).Tokenize();
            return (tokens.First().GetType());
        }

        public void CanParseComplexTokenStructure()
        {
            var tokens = new Tokenizer("!(True And False)").Tokenize();
            var list = tokens.ToList();
            Assert.IsTrue(list[0] is NegationToken);
            Assert.IsTrue(list[1] is OpenParenthesisToken);
            Assert.IsTrue(list[2] is TrueToken);
            Assert.IsTrue(list[3] is AndToken);
            Assert.IsTrue(list[4] is FalseToken);
            Assert.IsTrue(list[5] is ClosedParenthesisToken);
        }
    }
}
