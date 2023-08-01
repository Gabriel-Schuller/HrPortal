using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HrPortal.Holidays
{
    public interface IHolidayRepository : IRepository<Holiday, Guid>
    {
        Task<List<Holiday>> GetListAsync(
            string filterText = null,
            int? daysRemainedLastYearMin = null,
            int? daysRemainedLastYearMax = null,
            int? daysUsedThisYearMin = null,
            int? daysUsedThisYearMax = null,
            int? daysRemainedMin = null,
            int? daysRemainedMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? daysRemainedLastYearMin = null,
            int? daysRemainedLastYearMax = null,
            int? daysUsedThisYearMin = null,
            int? daysUsedThisYearMax = null,
            int? daysRemainedMin = null,
            int? daysRemainedMax = null,
            CancellationToken cancellationToken = default);
    }
}