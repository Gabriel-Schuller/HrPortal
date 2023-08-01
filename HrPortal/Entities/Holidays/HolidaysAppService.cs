using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using HrPortal.Permissions;
using HrPortal.Holidays;

namespace HrPortal.Holidays
{

    [Authorize(HrPortalPermissions.Holidays.Default)]
    public class HolidaysAppService : ApplicationService, IHolidaysAppService
    {

        private readonly IHolidayRepository _holidayRepository;
        private readonly HolidayManager _holidayManager;

        public HolidaysAppService(IHolidayRepository holidayRepository, HolidayManager holidayManager)
        {

            _holidayRepository = holidayRepository;
            _holidayManager = holidayManager;
        }

        public virtual async Task<PagedResultDto<HolidayDto>> GetListAsync(GetHolidaysInput input)
        {
            var totalCount = await _holidayRepository.GetCountAsync(input.FilterText, input.DaysRemainedLastYearMin, input.DaysRemainedLastYearMax, input.DaysUsedThisYearMin, input.DaysUsedThisYearMax, input.DaysRemainedMin, input.DaysRemainedMax);
            var items = await _holidayRepository.GetListAsync(input.FilterText, input.DaysRemainedLastYearMin, input.DaysRemainedLastYearMax, input.DaysUsedThisYearMin, input.DaysUsedThisYearMax, input.DaysRemainedMin, input.DaysRemainedMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<HolidayDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Holiday>, List<HolidayDto>>(items)
            };
        }

        public virtual async Task<HolidayDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Holiday, HolidayDto>(await _holidayRepository.GetAsync(id));
        }

        [Authorize(HrPortalPermissions.Holidays.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _holidayRepository.DeleteAsync(id);
        }

        [Authorize(HrPortalPermissions.Holidays.Create)]
        public virtual async Task<HolidayDto> CreateAsync(HolidayCreateDto input)
        {

            var holiday = await _holidayManager.CreateAsync(
            input.DaysRemainedLastYear, input.DaysUsedThisYear, input.DaysRemained
            );

            return ObjectMapper.Map<Holiday, HolidayDto>(holiday);
        }

        [Authorize(HrPortalPermissions.Holidays.Edit)]
        public virtual async Task<HolidayDto> UpdateAsync(Guid id, HolidayUpdateDto input)
        {

            var holiday = await _holidayManager.UpdateAsync(
            id,
            input.DaysRemainedLastYear, input.DaysUsedThisYear, input.DaysRemained
            );

            return ObjectMapper.Map<Holiday, HolidayDto>(holiday);
        }
    }
}