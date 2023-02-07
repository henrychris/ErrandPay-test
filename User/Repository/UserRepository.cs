using ErrandPay_test;
using ErrandPay_test.Models;
using User.Models;

namespace UserAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        


        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public bool Add(UserObj user)
        {
            if (user == null)
            {
                return false;
            }
            if (_appDbContext.Users.Any(c => c.Email == user.Email))
            {
                return false;
            }
            _appDbContext.Users.Add(user);
            _appDbContext.SaveChanges();
            return true;
        }

        public UserObj FindByEmail(string email)
        {
            var x =  _appDbContext.Users.FirstOrDefault(c => c.Email == email);
            return x == null ? null : x;
        }

        public Tuple<bool, decimal> FundWallet(UserObj user, decimal amount)
        {
            // check 
            if (user != null)
            {
                user.Wallet += amount;
                _appDbContext.SaveChanges();
                // return tuple of bool and balance?
                return Tuple.Create(true, user.Wallet);
            }

            return Tuple.Create(false, amount);
        }

        public bool PayForEvent(Event eventAttr, UserObj user)
        {
                        if ((user.Wallet - eventAttr.Price) < 0)
            {
                return false;
            }
            user.Wallet -= eventAttr.Price;
            _appDbContext.SaveChanges();
            // return a tuple and new balance?
            return true;
        }
    }
}
