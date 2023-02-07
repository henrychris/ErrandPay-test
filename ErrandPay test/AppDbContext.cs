using ErrandPay_test.Models;
using Microsoft.EntityFrameworkCore;
using User.Models;

namespace ErrandPay_test
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //public AppDbContext(DbContextOptions options) : base(options)
        //{
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseSqlite(_configuration.GetConnectionString("SqlConnection"));
            // builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("C:/Users/hidde/source/repos/ErrandPay test/ErrandPay test/Application.db"));
        }

        public DbSet<UserObj> Users { get; set; }
        public DbSet<Event> Events { get; set; }

    }
}
