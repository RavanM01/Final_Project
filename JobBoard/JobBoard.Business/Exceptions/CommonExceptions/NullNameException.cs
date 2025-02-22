using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Exceptions.CommonExceptions
{
    public class NullNameException : Exception
    {
        public NullNameException():base("Ad bos ola bilmez")
        {
        }

        public NullNameException(string? message) : base(message)
        {
        }

        public NullNameException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
