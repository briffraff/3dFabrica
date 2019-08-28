namespace Fabrica.Infrastructure.Exceptions
{
    public class EditPropException : FabricaBaseException
    {
        private const string EditPropExceptionMessage = GlobalConstants.EditPropExceptionMessage;
        private readonly string errorMessage;

        public EditPropException(string errorMessage = EditPropExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
