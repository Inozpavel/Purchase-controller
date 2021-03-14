using AutoMapper;

namespace Stores.Tests
{
    public static class Helper
    {
        public static IMapper CreateMapperWithProfile<T>() where T : Profile
        {
            var conf = new MapperConfiguration(options =>
                options.AddProfile(typeof(T)));
            var mapper = new Mapper(conf);
            return mapper;
        }
    }
}