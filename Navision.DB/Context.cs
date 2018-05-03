namespace Navision.DB
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        { 
            this.OnModelCreating(new DbModelBuilder()); 
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserMobile> UsersMobile { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.timestamp)
                .IsFixedLength();

            modelBuilder.Entity<UserMobile>()
               .Property(e => e.timestamp)
               .IsFixedLength();
        }
    }
}
