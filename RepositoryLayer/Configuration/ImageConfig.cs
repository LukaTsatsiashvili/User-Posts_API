using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration
{
    public class ImageConfig : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.File);

            builder.Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.FileExtension)
                .IsRequired();

            builder.Property(x => x.FileSizeInBytes)
                .IsRequired();

            builder.Property(x => x.FilePath)
                .IsRequired();
        }
    }
}
