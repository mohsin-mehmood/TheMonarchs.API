using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Interfaces;

namespace TheMonarchs.API.MediatR.House.Queries
{
    public class GetLongestRulingHouseQuery : IRequest<HouseMonarchsDto>
    {
    }


    public class GetLongestRulingHouseQueryHandler : IRequestHandler<GetLongestRulingHouseQuery, HouseMonarchsDto>
    {
        private readonly IMonarchDataRepository _dataRepository;
        private readonly IMapper _mapper;

        public GetLongestRulingHouseQueryHandler(IMonarchDataRepository dataRepository, IMapper mapper)
        {
            _dataRepository = dataRepository;
            _mapper = mapper;
        }

        public async Task<HouseMonarchsDto> Handle(GetLongestRulingHouseQuery request, CancellationToken cancellationToken)
        {
            var longestRulingHouse = await _dataRepository.GetLongestRulingHouseAsync();

            return _mapper.Map<HouseMonarchsDto>(longestRulingHouse);

        }
    }
}
