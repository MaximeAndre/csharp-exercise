
using CSharpExercise.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSharpExercise.src.Infrastructure.Persistence.Configuration
{
    /// <summary>
    /// Used to create the Db
    /// </summary>
    internal class UserInfoConfiguration : IEntityTypeConfiguration<UserInfo>
    {
        private const string TableName = "user_info";
        public void Configure(EntityTypeBuilder<UserInfo> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("integer")
                .IsRequired();

            builder.HasIndex(x => x.Id)
                .HasDatabaseName("id")               
                .IsUnique();

            builder.Property(x => x.Login)
                .HasColumnName("login")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Password)
                .HasColumnName("password")
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")               
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .HasColumnName("email")
                .HasColumnType("varchar(320)")
                .HasMaxLength(320);

        }
    }
}
