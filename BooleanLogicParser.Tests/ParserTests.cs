using System;
using NUnit.Framework;

namespace BooleanLogicParser.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [TestCase("true", ExpectedResult = true)]
        [TestCase(")", ExpectedException = (typeof(Exception)))]
        [TestCase("az", ExpectedException = (typeof(Exception)))]
        [TestCase("", ExpectedException = (typeof(Exception)))]
        [TestCase("()", ExpectedException = typeof(Exception))]
        [TestCase("true and", ExpectedException = typeof(Exception))]
        [TestCase("false", ExpectedResult = false)]
        [TestCase("true ", ExpectedResult = true)]
        [TestCase("false ", ExpectedResult = false)]
        [TestCase(" true", ExpectedResult = true)]
        [TestCase(" false", ExpectedResult = false)]
        [TestCase(" true ", ExpectedResult = true)]
        [TestCase(" false ", ExpectedResult = false)]
        [TestCase("(false)", ExpectedResult = false)]
        [TestCase("(true)", ExpectedResult = true)]
        [TestCase("true and false", ExpectedResult = false)]
        [TestCase("false and true", ExpectedResult = false)]
        [TestCase("false and false", ExpectedResult = false)]
        [TestCase("true and true", ExpectedResult = true)]
        [TestCase("!true", ExpectedResult = false)]
        [TestCase("!(true)", ExpectedResult = false)]
        [TestCase("!(true", ExpectedException = typeof(Exception))]
        [TestCase("!(!(true))", ExpectedResult = true)]
        [TestCase("!false", ExpectedResult = true)]
        [TestCase("!(false)", ExpectedResult = true)]
        [TestCase("(!(false)) and (!(true))", ExpectedResult = false)]
        [TestCase("!((!(false)) and (!(true)))", ExpectedResult = true)]
        [TestCase("!false and !true", ExpectedResult = false)]
        [TestCase("false and true and true", ExpectedResult = false)]
        [TestCase("false or true or false", ExpectedResult = true)]
        public bool CanParseSingleToken(string expression)
        {
            var tokens = new Tokenizer(expression).Tokenize();
            var parser = new Parser(tokens);
            return parser.Parse();
        }
    }
}
