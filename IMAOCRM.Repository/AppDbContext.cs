using IMAOCMS.Core.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IMAOCRM.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public AppDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("SqlConnection"));
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=185.50.69.43;Initial Catalog=IMAOCMS;User Id=sa;Password=1qaz2wsx_?=; TrustServerCertificate=True; MultipleActiveResultSets=true;");
        //}
        //public virtual DbSet<Com48> Com48s { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EpcReadData> EpcReadDatas { get; set; }
        public DbSet<EPCReadTemp> EPCReadTemps { get; set; }
        public DbSet<RFIDDevice> RFIDDevices { get; set; }
        public DbSet<RFIDDeviceAntenna> RFIDDeviceAntennas { get; set; }
        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityreference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {

                                entityreference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                if (entityreference.IsDelete == true)
                                {
                                    Entry(entityreference).Property(x => x.UpdatedDate).IsModified = false;
                                    Entry(entityreference).Property(x => x.UpdaterId).IsModified = false;
                                    entityreference.DeletedDate = DateTime.Now;
                                }
                                else
                                {
                                    Entry(entityreference).Property(x => x.CreatedDate).IsModified = false;
                                    Entry(entityreference).Property(x => x.CreatorId).IsModified = false;
                                    entityreference.UpdatedDate = DateTime.Now;
                                }

                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
