namespace EntityFrameworkAuditingSample.Models
{
    public class ApplicationDbContext : DbContextBase<ApplicationUser>
    {
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<EntityFrameworkAuditingSample.Models.Contact> Contacts { get; set; }
    }
}