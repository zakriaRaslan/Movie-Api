namespace Movies_API.Dtos
{
    public class MovieBase
    {

        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoryLine { get; set; }
        public byte GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
