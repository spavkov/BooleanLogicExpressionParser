namespace BooleanLogicParser
{
    public class OperandToken : Token
    {
        
    }
    public class OrToken : OperandToken
    {
    }

    public class AndToken : OperandToken
    {
    }

    public class BooleanValueToken : Token
    {
        
    }

    public class FalseToken : BooleanValueToken
    {
    }

    public class TrueToken : BooleanValueToken
    {
    }

    public class ParenthesisToken : Token
    {

    }

    public class ClosedParenthesisToken : ParenthesisToken
    {
    }


    public class OpenParenthesisToken : ParenthesisToken
    {
    }

    public class NegationToken : Token
    {
    }

    public abstract class Token
    {

    }
}
