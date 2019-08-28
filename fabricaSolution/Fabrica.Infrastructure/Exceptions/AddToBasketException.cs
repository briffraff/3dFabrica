using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrica.Infrastructure.Exceptions
{
    public class AddToBasketException : FabricaBaseException
    {
        private const string AddToBasketExceptionMessage = GlobalConstants.AddToBasketExceptionMessage;
        private readonly string errorMessage;

        public AddToBasketException(string errorMessage = AddToBasketExceptionMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
