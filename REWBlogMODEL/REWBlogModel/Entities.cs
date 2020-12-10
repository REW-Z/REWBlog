namespace REW的空间Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<ARTICLES> ARTICLES { get; set; }
        public virtual DbSet<TYPES> TYPES { get; set; }
        public virtual DbSet<USERS> USERS { get; set; }
        public virtual DbSet<RELATIONS> RELATIONS { get; set; }
        public virtual DbSet<CHECKIN> CHECKIN { get; set; }
        public virtual DbSet<NOTIFICATIONS> NOTIFICATIONS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TYPES>()
                .HasMany(e => e.ARTICLES)
                .WithRequired(e => e.TYPES)
                .HasForeignKey(e => e.A_TID)
                .WillCascadeOnDelete(false);
        }
    }
}
