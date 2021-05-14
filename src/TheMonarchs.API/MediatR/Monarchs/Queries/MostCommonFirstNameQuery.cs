using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Interfaces;

namespace TheMonarchs.API.MediatR.Monarchs.Queries
{
    public class MostCommonFirstNameQuery : IRequest<FirstNameOccurencesDto>
    {
    }


    public class MostCommonFirstNameQueryHandler : IRequestHandler<MostCommonFirstNameQuery, FirstNameOccurencesDto>
    {

        private readonly IMonarchDataRepository _dataRepository;
        private readonly IMapper _mapper;

        public MostCommonFirstNameQueryHandler(IMonarchDataRepository dataRepository, IMapper mapper)
        {
            _dataRepository = dataRepository;
            _mapper = mapper;
        }


        public async Task<FirstNameOccurencesDto> Handle(MostCommonFirstNameQuery request, CancellationToken cancellationToken)
        {

            var commonFirstName = await _dataRepository.GetMostCommonFirstNameAsync();
            return _mapper.Map<FirstNameOccurencesDto>(commonFirstName);
        }
    }
}
