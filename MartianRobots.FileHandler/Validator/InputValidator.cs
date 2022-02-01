using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MartianRobots.FileHandler.Validator
{
    public class InputValidator : IInputValidator
    {
        public bool Validate(List<string> content)
        {
            var gridRgx = new Regex(@"^([1-9]|[1-4]\d|[5][0])( )([1-9]|[1-4]\d|[5][0])$");

            if (!gridRgx.IsMatch(content[0])) throw new ValidationException("Input grid data is in incorrect format");

            return true;
        }
    }
}
