namespace Fabrica.Infrastructure.Exceptions
{
    using System;

    public abstract class FabricaBaseException : Exception
    {
        private const string FabricaBaseExceptionMessage = GlobalConstants.FabricaBaseExceptionMessage;
        private readonly string errorMessage;

        protected FabricaBaseException(string errorMessage = FabricaBaseExceptionMessage) : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
