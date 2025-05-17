using TicTacToe.Board;

namespace TicTacToe.Validation;

public class Validator
{
    public enum InputReslut
    {
        IncorrectInput,
        Success
    }
    
    
    public static InputReslut InputValidityCheck(string input, out Coord coord)
    {
        coord = new Coord { y = default, x = default };
        
        if (input.Length < 2)
        {
            return InputReslut.IncorrectInput;
        }

        if (int.TryParse(input[0].ToString(), out int y) && int.TryParse(input[1].ToString(), out int x))
        {
            coord.y = y;
            coord.x = x;
            
            return InputReslut.Success;
        }

        return InputReslut.IncorrectInput;
    }
}