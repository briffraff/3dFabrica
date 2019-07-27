namespace Fabrica.Services
{
    using Data;

    public abstract class DataService
    {
        protected readonly FabricaDBContext context;

        protected DataService(FabricaDBContext context)
        {
            this.context = context;
        }
    }
}
