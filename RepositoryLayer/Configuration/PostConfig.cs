using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Content) 
                .IsRequired()
                .HasMaxLength(5000);

            builder.Property(x => x.PublishedAt)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.AuthorId)
                .IsRequired();

            builder
                .HasOne(x => x.Author)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(x => x.Comments)
                .WithOne(x => x.Post);

            // Data seeding
            builder.HasData(new Post
            {
                Id = Guid.Parse("480c6dd5-85f3-443a-ae0e-a890753139ef"),
                Title = "Random Title",
                Content = "Random content for random title.",
                PublishedAt = "10/04/2024",
                AuthorId = Guid.Parse("63b57b77-d71d-413e-bc45-d8858fd41550")
            });
        }
    }
}
