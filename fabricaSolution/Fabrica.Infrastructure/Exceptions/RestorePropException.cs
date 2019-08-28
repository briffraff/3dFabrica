namespace Fabrica.Infrastructure.Exceptions
{
    public class RestorePropException : FabricaBaseException
    {
        private const string RestorePropExceptionMessage = GlobalConstants.RestorePropExceptionMessage;
        private readonly string errorMessage;

        public RestorePropException(string errorMessage = RestorePropExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
