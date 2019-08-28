namespace Fabrica.Infrastructure.Exceptions
{
    public class BuyLicenseException : FabricaBaseException
    {
        private const string BuyLicenseExceptionMessage = GlobalConstants.BuyLicenseExceptionMessage;
        private readonly string errorMessage;

        public BuyLicenseException(string errorMessage = BuyLicenseExceptionMessage) 
            : base(errorMessage)
        {
            this.errorMessage = errorMessage;
        }

    }
}
