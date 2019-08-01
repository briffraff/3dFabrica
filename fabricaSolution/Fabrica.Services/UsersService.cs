namespace Fabrica.Services
{
    using Fabrica.Data;
    using Fabrica.Services.Contracts;

    public class UsersService : DataService, IUsersService
    {
        public UsersService(FabricaDBContext context) : base(context)
        {

        }


    }
}
