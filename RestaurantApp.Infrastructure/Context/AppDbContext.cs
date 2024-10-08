using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RestaurantApp.Domain.Core.BaseEntities;
using RestaurantApp.Domain.Entities.Orders;
using RestaurantApp.Domain.Entities.Products;
using RestaurantApp.Domain.Entities.Users;
using RestaurantApp.Domain.Entities.Users.AppUser;
using RestaurantApp.Domain.Entities.Websites;
using RestaurantApp.Domain.Enums;
using RestaurantApp.Infrastructure.Configurations;


namespace RestaurantApp.Infrastructure.Context
{
    public class AppDbContext:IdentityDbContext<IdentityUser,IdentityRole,string>
	{

		#region İlk migration'da yoruma al
		private readonly IHttpContextAccessor _contextAccessor;
		public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor) : base(options)
		{
			_contextAccessor = contextAccessor;
		}
		#endregion
		public const string DevConnectionString = "MelodyAppDev";

		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
			//connction string i jsondan çekeceğim için bu şekilde bir constructor oluşturuyorum.
		}
		//User
		public virtual DbSet<Admin> Admins { get; set; }
		public virtual DbSet<AppUser> AppUsers { get; set; }
		public virtual DbSet<UserAddress> UserAddresses { get; set; }
		//Product
		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Ingredient> Ingredients { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<Campaign> Campaigns { get; set; }
		public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }
		public virtual DbSet<ProductCampaign> ProductCampaigns { get; set; }
		//Order
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<CartProduct> CartProducts { get; set; }
		public virtual DbSet<Comment> Comments { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		//Website
		//public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<AboutUs> AboutUs { get; set; }
		public virtual DbSet<Feature> Features { get; set; }
		public virtual DbSet<Slider> Sliders { get; set; }
		public virtual DbSet<SocialMedia> SocialMedias { get; set; }
		
		//public virtual DbSet<Menu> Menus { get; set; }
		//public virtual DbSet<MenuCampaign> MenuCampaigns { get; set; }
		//public virtual DbSet<MenuProduct> MenuProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(typeof(IEntityConfiguration).Assembly);
			base.OnModelCreating(builder);
		}
		public override int SaveChanges()
		{
			return base.SaveChanges();
		}
		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			SetBaseProperties();
			return base.SaveChangesAsync(cancellationToken);
		}

		private void SetBaseProperties()
		{
			var entries = ChangeTracker.Entries<BaseEntity>();
			var userId = "UserBulunamadı";

			foreach (var entry in entries)
			{
				SetIfAdded(entry, userId);
				SetIfModified(entry, userId);
				SetIfDeleted(entry, userId);
			}
		}

		private void SetIfDeleted(EntityEntry<BaseEntity> entry, string userId)
		{
			if (entry.State != EntityState.Deleted)
			{
				return;
			}
			if (entry.Entity is not AuditableEntity entity)
			{
				return;
			}
			entry.State = EntityState.Modified;
			entry.Entity.Status = Status.Deleted;
			entity.DeletedDate = DateTime.Now;
			entity.DeletedBy = userId;
		}

		private void SetIfModified(EntityEntry<BaseEntity> entry, string userId)
		{
			if (entry.State == EntityState.Modified)
			{
				entry.Entity.Status =Status.Updated;
				entry.Entity.UpdatedDate = DateTime.Now;
				entry.Entity.UpdatedBy = userId;
			}
		}

		private void SetIfAdded(EntityEntry<BaseEntity> entry, string userId)
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.Status =Status.Created;
				entry.Entity.CreatedDate = DateTime.Now;
				entry.Entity.CreatedBy = userId;
			}
		}
	}
}
