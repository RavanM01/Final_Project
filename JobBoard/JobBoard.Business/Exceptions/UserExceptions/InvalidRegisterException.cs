using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Exceptions.UserExceptions
{
    public class InvalidRegisterException : Exception
    {
        public InvalidRegisterException() :base("Geçersiz e-posta veya şifre!")
        {
        }

        public InvalidRegisterException(string? message) : base(message)
        {
        }

        public InvalidRegisterException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
