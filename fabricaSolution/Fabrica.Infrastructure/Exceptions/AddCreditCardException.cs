namespace Fabrica.Infrastructure.Exceptions
{
    public class AddCreditCardException : FabricaBaseException
    {
        private const string AddCreditCardExceptionMessage = GlobalConstants.AddCreditCardExceptionMessage;
        private readonly string errorMessage;

        public AddCreditCardException(string errorMessage = AddCreditCardExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
