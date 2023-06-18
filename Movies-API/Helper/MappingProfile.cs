using AutoMapper;

namespace Movies_API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieCreate, Movie>()
                .ForMember(x => x.Poster, y => y.Ignore());
            CreateMap<Movie, MovieUpdate>()
                .ForMember(x => x.Poster, y => y.Ignore());

        }
    }
}
