using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Rainfall.Services.Class
{
    public static class Ensure
    {
        //raw check?
        public static void NotNullOrWhiteSpace(string? value, string? msg = null, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(msg ?? "The value cannot be null or white space", paramName);
        }
        public static void NotNull(string? value, string? msg = null, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value is null)
                throw new ArgumentNullException(msg ?? "The value cannot be null", paramName);
        }
        public static void NotZero(int? value, string? msg = null, [CallerArgumentExpression("value")] string? paramName = null)
        {
            if (value == 0)
                throw new ArgumentException(msg ?? "The value cannot be 0", paramName);
        }
    }
}
