using BDStore.Application.Abstractions.Mediator;
using BDStore.Domain.Abstraction;
using BDStore.Domain.Clients;
using BDStore.Domain.Products;
using BDStore.Domain.Users;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BDStore.Infra.Data
{
    public class ApplicationDbContext : IdentityDbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost,1433;Database=BDStore;User ID=sa;Password=1q2w3e4r@#$;Trusted_Connection=False; TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                         e => e.GetProperties().Where(p => p.ClrType == typeof(string))))

                modelBuilder.Ignore<IDomainEvent>();
            modelBuilder.Ignore<Event>();
            modelBuilder.Ignore<ValidationResult>();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                         .SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            var sucess = await base.SaveChangesAsync() > 0;
            if (sucess) await _mediatorHandler.PublishEvents(this);
            return sucess;
        }
    }

    public static class MediatorExtension
    {
        // Método de extensão para publicar eventos a partir de um DbContext
        public static async Task PublishEvents<T>(this IMediatorHandler mediator, T ctx) where T : DbContext
        {
            // Obtém todas as entidades do contexto que possuem notificações pendentes
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notifications != null && x.Entity.Notifications.Any());

            // Obtém todas as notificações pendentes das entidades
            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notifications)
                .ToList();

            // Limpa as notificações pendentes das entidades
            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            // Publica cada evento de domínio usando o mediador
            var tasks = domainEvents
                .Select(async (domainEvent) => { await mediator.PublishEvent(domainEvent); });
            await Task.WhenAll(tasks);

            // Alternativamente, poderia ser feito um loop direto sobre os eventos e publicados um a um
            // foreach (var task in domainEvents)
            // {
            //     await mediator.Publish(task);
            // }
        }
    }
}