namespace Fabrica.Infrastructure.Exceptions
{
    public class CreatePropException : FabricaBaseException
    {
        private const string CreatePropExceptionMessage = GlobalConstants.CreatePropExceptionMessage;
        private readonly string errorMessage;

        public CreatePropException(string errorMessage = CreatePropExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
