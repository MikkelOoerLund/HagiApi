using HagiDomain;
using Microsoft.EntityFrameworkCore;

namespace HagiApi
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(UserContext userContext) : base(userContext)
        {
        }


        public async Task<User?> GetUserWithUsernameAsync(string userName)
        {
            return await DbSet.FirstOrDefaultAsync<User>(x => x.UserName == userName);
        }


        public async Task<bool> HasUserWithUsernameAsync(string userName)
        {
            return await DbSet.AnyAsync(x => x.UserName == userName);
        }

    }

}
