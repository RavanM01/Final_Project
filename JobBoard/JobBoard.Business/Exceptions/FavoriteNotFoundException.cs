using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Business.Exceptions
{
    public class FavoriteNotFoundException : Exception
    {
        public FavoriteNotFoundException() : base("Favori iş bulunamadı.") { }

        public FavoriteNotFoundException(string message) : base(message) { }

        public FavoriteNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }
    }

}
