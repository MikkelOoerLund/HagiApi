using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using HagiDomain;
using HagiApi.Configuration;

namespace HagiApi
{

    public class UserContext : DbContext
    {
        private readonly ConnectionStringContainer _connectionStringContainer;

        public DbSet<User> Users { get; set; }


        public UserContext(ConnectionStringContainer connectionStringContainer)
        {
            _connectionStringContainer = connectionStringContainer;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _connectionStringContainer.UserContextConnectionString;
            var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 23));

            optionsBuilder.UseMySql(connectionString, mySqlServerVersion);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserName).HasMaxLength(255);
            });
        }
    }

}
