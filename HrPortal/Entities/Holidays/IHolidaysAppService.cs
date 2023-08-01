using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HrPortal.Holidays
{
    public interface IHolidaysAppService : IApplicationService
    {
        Task<PagedResultDto<HolidayDto>> GetListAsync(GetHolidaysInput input);

        Task<HolidayDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<HolidayDto> CreateAsync(HolidayCreateDto input);

        Task<HolidayDto> UpdateAsync(Guid id, HolidayUpdateDto input);
    }
}