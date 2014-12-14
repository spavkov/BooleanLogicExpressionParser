using System;
using System.Collections.Generic;

namespace BooleanLogicParser
{
    // Expression         := [ "!" ] <Boolean> { <BooleanOperator> <Boolean> } ...
    // Boolean            := <BooleanConstant> | <Expression> | "(" <Expression> ")"
    // BooleanOperator    := "And" | "Or" 
    // BooleanConstant    := "True" | "False"
    public class Parser
    {
        private readonly IEnumerator<Token> _tokens;

        public Parser(IEnumerable<Token> tokens)
        {
            _tokens = tokens.GetEnumerator();
            _tokens.MoveNext();
        }

        public bool Parse()
        {
            while (_tokens.Current != null)
            {
                var isNegated = _tokens.Current is NegationToken;
                if (isNegated)
                    _tokens.MoveNext();

                var boolean = ParseBoolean();
                if (isNegated)
                    boolean = !boolean;

                while (_tokens.Current is OperandToken)
                {
                    var operand = _tokens.Current;
                    if (!_tokens.MoveNext())
                    {
                        throw new Exception("Missing expression after operand");
                    }
                    var nextBoolean = ParseBoolean();

                    if (operand is AndToken)
                        boolean = boolean && nextBoolean;
                    else
                        boolean = boolean || nextBoolean;

                }

                return boolean;
            }

            throw new Exception("Empty expression");
        }

        private bool ParseBoolean()
        {
            if (_tokens.Current is BooleanValueToken)
            {
                var current = _tokens.Current;
                _tokens.MoveNext();

                if (current is TrueToken)
                    return true;

                return false;
            }
            if (_tokens.Current is OpenParenthesisToken)
            {
                _tokens.MoveNext();

                var expInPars = Parse();

                if (!(_tokens.Current is ClosedParenthesisToken))
                    throw new Exception("Expecting Closing Parenthesis");
                    
                _tokens.MoveNext(); 

                return expInPars;
            }
            if (_tokens.Current is ClosedParenthesisToken)
                throw new Exception("Unexpected Closed Parenthesis");

            // since its not a BooleanConstant or Expression in parenthesis, it must be a expression again
            var val = Parse();
            return val;
        }
    }
}