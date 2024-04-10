using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.CreatedTime)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.PostId)
                .IsRequired();

            builder
                .HasOne(x => x.Post)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Data seeding
            builder.HasData(new Comment
            {
                Id = Guid.Parse("eb948015-a9c8-4b73-a43d-c415736e502b"),
                Content = "Some random content just for demonstration purposes",
                CreatedTime = "10/04/2024",
                UserId = Guid.Parse("63b57b77-d71d-413e-bc45-d8858fd41550"),
                PostId = Guid.Parse("480c6dd5-85f3-443a-ae0e-a890753139ef")
            });
        }
    }
}
