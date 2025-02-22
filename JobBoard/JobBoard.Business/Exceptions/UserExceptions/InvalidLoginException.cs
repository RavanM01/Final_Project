using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Exceptions.UserExceptions
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException() : base("Geçersiz e-posta veya şifre!") { }

        public InvalidLoginException(string message) : base(message) { }

        public InvalidLoginException(string message, Exception innerException) : base(message, innerException) { }
    }

}
