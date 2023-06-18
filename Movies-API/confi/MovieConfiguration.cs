using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Movies_API.confi
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(250);

            builder.Property(x => x.StoryLine).HasMaxLength(2500);

        }
    }
}
