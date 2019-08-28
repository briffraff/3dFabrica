namespace Fabrica.Infrastructure.Exceptions
{
    public class ActivateUserException : FabricaBaseException
    {
        private const string ActivatePropExceptionMessage = GlobalConstants.ActivatePropExceptionMessage;
        private readonly string errorMessage;

        public ActivateUserException(string errorMessage = ActivatePropExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
