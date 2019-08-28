namespace Fabrica.Infrastructure.Exceptions
{
    public class TransactionException : FabricaBaseException
    {
        private const string TransactionExceptionMessage = GlobalConstants.TransactionExceptionMessage;
        private readonly string errorMessage;

        public TransactionException(string errorMessage = TransactionExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
