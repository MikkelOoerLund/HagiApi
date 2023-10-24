using HagiDomain;

namespace HagiApi
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(UserContext userContext) : base(userContext)
        {
        }


    }

}
