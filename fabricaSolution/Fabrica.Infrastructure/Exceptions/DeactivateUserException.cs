namespace Fabrica.Infrastructure.Exceptions
{
    public class DeactivateUserException : FabricaBaseException
    {
        private const string DeactivateUserExceptionMessage = GlobalConstants.DeactivateUserExceptionMessage;
        private readonly string errorMessage;

        public DeactivateUserException(string errorMessage = DeactivateUserExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
