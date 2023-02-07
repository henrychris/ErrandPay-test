using ErrandPay_test.Models;
using User.Models;

namespace UserAPI.Repository
{
    public interface IUserRepository
    {
        bool Add(UserObj user);

        Tuple<bool, decimal> FundWallet(UserObj user, decimal amount);

        UserObj FindByEmail(string email);

        bool PayForEvent(Event eventAttr, UserObj user);

        // need to setup sessions
        // need user to be able to logout.
    }
}
