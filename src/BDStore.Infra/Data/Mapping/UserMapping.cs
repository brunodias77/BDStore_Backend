using BDStore.Domain.Users;
using BDStore.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                // Converte o Id para uma string antes de ser armazenado no banco de dados
                id => id.ToString(),
                // Converte a string do banco de dados de volta para um UserId
                // usando o construtor de UserId que aceita um Guid
                id => new UserId(Guid.Parse(id))
            )
            // Define o nome da coluna no banco de dados como "UserId"
            .HasColumnName("UserId");

        builder.Property(u => u.FirstName)
            .HasConversion(
                firstName => firstName.ToString(),
                firstName => new FirstName(firstName)
            )
            .HasColumnName("FirstName");

        builder.Property(u => u.LastName)
            .HasConversion(
                lastName => lastName.ToString(),
                lastName => new LastName(lastName)
            )
            .HasColumnName("LastName");

        builder.Property(u => u.Email)
            .HasConversion(
                email => email.ToString(),
                email => new Email(email)
            )
            .HasColumnName("Email");

        builder.Property(u => u.Password)
            .HasColumnName("Password");
    }
}