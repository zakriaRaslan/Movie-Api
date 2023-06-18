
namespace Movies_API.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string StoryLine { get; set; }

        public byte[] Poster { get; set; }
        public byte GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
