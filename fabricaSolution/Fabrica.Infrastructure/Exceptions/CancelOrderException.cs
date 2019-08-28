namespace Fabrica.Infrastructure.Exceptions
{
    public class CancelOrderException : FabricaBaseException
    {
        private const string CancelOrderExceptionMessage = GlobalConstants.CancelOrderExceptionMessage;
        private readonly string errorMessage;

        public CancelOrderException(string errorMessage = CancelOrderExceptionMessage)
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
