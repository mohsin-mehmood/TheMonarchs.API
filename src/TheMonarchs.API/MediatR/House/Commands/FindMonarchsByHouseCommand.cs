using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TheMonarchs.API.Common.Exceptions;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Interfaces;

namespace TheMonarchs.API.MediatR.House.Commands
{
    public class FindMonarchsByHouseCommand : IRequest<IEnumerable<HouseMonarchsDto>>
    {

        public string HouseName { get; set; }
    }


    public class FindMonarchsByHouseCommandHandler : IRequestHandler<FindMonarchsByHouseCommand, IEnumerable<HouseMonarchsDto>>
    {

        private readonly IMonarchDataRepository _dataRepository;
        private readonly IMapper _mapper;

        public FindMonarchsByHouseCommandHandler(IMonarchDataRepository dataRepository, IMapper mapper)
        {
            _dataRepository = dataRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HouseMonarchsDto>> Handle(FindMonarchsByHouseCommand request, CancellationToken cancellationToken)
        {
            var houseMonarchs = await _dataRepository.GetMonarchsByHouseAsync(request.HouseName);

            if(!houseMonarchs.Any())
            {
                throw new NotFoundException(nameof(HouseMonarchsDto), request.HouseName);
            }


            return _mapper.Map<IEnumerable<HouseMonarchsDto>>(houseMonarchs);

        }
    }
}
