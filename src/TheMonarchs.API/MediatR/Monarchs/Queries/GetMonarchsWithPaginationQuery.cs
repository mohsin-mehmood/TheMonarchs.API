using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TheMonarchs.Core.Dto;
using TheMonarchs.Core.Interfaces;

namespace TheMonarchs.API.MediatR.Monarchs.Queries
{
    public class GetMonarchsWithPaginationQuery : IRequest<(PagingMetadata pageMetadata, IEnumerable<MonarchDto> monarches)>
    {


        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }

    public class GetMonarchsWithPaginationQueryHandler : IRequestHandler<GetMonarchsWithPaginationQuery, (PagingMetadata pageMetadata, IEnumerable<MonarchDto> monarches)>
    {
        private readonly IMonarchDataRepository _dataRepository;
        private readonly IMapper _mapper;

        public GetMonarchsWithPaginationQueryHandler(IMonarchDataRepository dataRepository, IMapper mapper)
        {
            _dataRepository = dataRepository;
            _mapper = mapper;
        }


        public async Task<(PagingMetadata pageMetadata, IEnumerable<MonarchDto> monarches)> Handle(GetMonarchsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var pagedMonarchs = await _dataRepository.GetAllMonarchsAsync(new PagingParams
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            });

            var pagingMetadata = new PagingMetadata
            {
                TotalCount = pagedMonarchs.TotalCount,
                PageSize = pagedMonarchs.PageSize,
                CurrentPage = pagedMonarchs.CurrentPage,
                TotalPages = pagedMonarchs.TotalPages,
                HasNext = pagedMonarchs.HasNext,
                HasPrevious = pagedMonarchs.HasPrevious
            };


            return (pagingMetadata, _mapper.Map<IEnumerable<MonarchDto>>(pagedMonarchs));
        }
    }

}
