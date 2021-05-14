using AutoMapper;
using System.Collections.Generic;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Entities;

namespace TheMonarchs.API.Automapper
{
    /// <summary>
    /// Automapper Profile
    /// </summary>
    public class MappingProfile : Profile
    {
        private string GetRulingPeriod(Monarch src)
        {
            if (src.YearEnd.HasValue)
            {
                return $"{src.YearsRuled} Years({src.YearStart} - {src.YearEnd})";
            }


            return $"Start Year {src.YearStart}";
        }

        public MappingProfile()
        {
            CreateMap<Monarch, MonarchDto>()
                    .ForMember(dest => dest.RulingPeriod,
                            opt => opt.MapFrom(src => GetRulingPeriod(src)));


            CreateMap<(string house, IEnumerable<Monarch> Monarchs), HouseMonarchsDto>()
                .ForMember(dest=>dest.House, opts=>opts.MapFrom(src=>src.house))
                .ForMember(dest=>dest.Monarchs, opts=>opts.MapFrom(src=>src.Monarchs));


            CreateMap<(string firstName, IEnumerable<string> occurences), FirstNameOccurencesDto>()
                .ForMember(dest=>dest.Name, opts=>opts.MapFrom(src=>src.firstName))
                .ForMember(dest => dest.Occurences, opts => opts.MapFrom(src => src.occurences));
        }
    }
}
