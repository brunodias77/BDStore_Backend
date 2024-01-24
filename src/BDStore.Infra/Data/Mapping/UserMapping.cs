using BDStore.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BDStore.Infra.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users"); // Substitua "Users" pelo nome da tabela no seu banco de dados, se necessário

            builder.HasKey(x => x.Id.Value);

            builder.Property(x => x.Id)
                .HasColumnName("UserId");
        }
    }
}