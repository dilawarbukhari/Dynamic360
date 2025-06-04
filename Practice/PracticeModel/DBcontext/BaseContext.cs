using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PracticeModel.BaseEntity;
using PracticeModel.DataModel;
using System.Security.Claims;


namespace PracticeModel.DBcontext
{
    public class BaseContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseContext(DbContextOptions<BaseContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<ApplicationUser> Users { get; set; } = null!;
        public DbSet<Categories> Categories { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<Medicines> Medicines { get; set; }
        public DbSet <Permission>Permissions { get; set; }



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? _httpContextAccessor.HttpContext?.User.FindFirst("sub")?.Value ?? "Default";

            foreach (var entry in ChangeTracker.Entries<IBaseEntity>())
            {
                if (entry.State == EntityState.Added)  // New record
                {
                    entry.Entity.CreatedBy = userId;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                    entry.Entity.Deleted = false; // Ensure new records are not marked as deleted
                }
                else if (entry.State == EntityState.Modified)  // Updating record
                {
                    entry.Entity.ModifiedBy = userId;
                    entry.Entity.ModifiedDate = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }

}
