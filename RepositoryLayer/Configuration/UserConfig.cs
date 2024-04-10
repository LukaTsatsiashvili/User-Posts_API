using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasMany(x => x.Posts)
                .WithOne(x => x.Author)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Cascade);

            // Data seeding
            builder.HasData(new User
            {
                Id = Guid.Parse("63b57b77-d71d-413e-bc45-d8858fd41550"),
                Name = "John",
                Email = "John@gmail.com"
            });
        }
    }
}
