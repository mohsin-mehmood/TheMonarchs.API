using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Interfaces;

namespace TheMonarchs.API.MediatR.Monarchs.Queries
{
    public class GetLongestRulingMonarchQuery : IRequest<MonarchDto>
    {
    }

    public class GetLongestRulingMonarchQueryHandler : IRequestHandler<GetLongestRulingMonarchQuery, MonarchDto>
    {

        private readonly IMonarchDataRepository _dataRepository;
        private readonly IMapper _mapper;

        public GetLongestRulingMonarchQueryHandler(IMonarchDataRepository dataRepository, IMapper mapper)
        {
            _dataRepository = dataRepository;
            _mapper = mapper;
        }


        public async Task<MonarchDto> Handle(GetLongestRulingMonarchQuery request, CancellationToken cancellationToken)
        {
            var monarch = await _dataRepository.GetLongestRulingMonarchAsync();
            return _mapper.Map<MonarchDto>(monarch);
        }
    }
}
