namespace Fabrica.Infrastructure.Exceptions
{
    public class DeletePropException : FabricaBaseException
    {
        private const string DeletePropExceptionMessage = GlobalConstants.DeletePropExceptionMessage;
        private readonly string errorMessage;

        public DeletePropException(string errorMessage = DeletePropExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
