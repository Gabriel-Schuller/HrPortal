using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HrPortal.Holidays
{
    public class HolidayManager : DomainService
    {
        private readonly IHolidayRepository _holidayRepository;

        public HolidayManager(IHolidayRepository holidayRepository)
        {
            _holidayRepository = holidayRepository;
        }

        public async Task<Holiday> CreateAsync(
        int daysRemainedLastYear, int daysUsedThisYear, int daysRemained)
        {

            var holiday = new Holiday(
             GuidGenerator.Create(),
             daysRemainedLastYear, daysUsedThisYear, daysRemained
             );

            return await _holidayRepository.InsertAsync(holiday);
        }

        public async Task<Holiday> UpdateAsync(
            Guid id,
            int daysRemainedLastYear, int daysUsedThisYear, int daysRemained
        )
        {

            var holiday = await _holidayRepository.GetAsync(id);

            holiday.DaysRemainedLastYear = daysRemainedLastYear;
            holiday.DaysUsedThisYear = daysUsedThisYear;
            holiday.DaysRemained = daysRemained;

            return await _holidayRepository.UpdateAsync(holiday);
        }

    }
}