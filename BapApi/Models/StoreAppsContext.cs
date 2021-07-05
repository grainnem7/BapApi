using Microsoft.EntityFrameworkCore;

namespace BapApi.Models
{
    public class StoreAppsContext : DbContext
    {
        public DbSet<StoreApp> StoreApps { get; set; }


        //public StoreAppsContext(DbContextOptions<StoreAppsContext> options) : base(options)
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=storeapps.db");
    }

}
