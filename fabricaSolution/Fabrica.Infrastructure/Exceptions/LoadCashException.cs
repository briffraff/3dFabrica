namespace Fabrica.Infrastructure.Exceptions
{
    public class LoadCashException : FabricaBaseException
    {
        private const string LoadCashExceptionMessage = GlobalConstants.LoadCashExceptionMessage;
        private readonly string errorMessage;

        public LoadCashException(string errorMessage = LoadCashExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
