using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace purchase_api.Services.Exceptions
{
    public class DbConcurrencyException : Exception
    {
        public DbConcurrencyException(String msg) : base(msg)
        {
        }
    }
}
