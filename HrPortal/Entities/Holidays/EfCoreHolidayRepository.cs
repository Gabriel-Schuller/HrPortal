using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using HrPortal.Data;

namespace HrPortal.Holidays
{
    public class EfCoreHolidayRepository : EfCoreRepository<HrPortalDbContext, Holiday, Guid>, IHolidayRepository
    {
        public EfCoreHolidayRepository(IDbContextProvider<HrPortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Holiday>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, daysRemainedLastYearMin, daysRemainedLastYearMax, daysUsedThisYearMin, daysUsedThisYearMax, daysRemainedMin, daysRemainedMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? HolidayConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? daysRemainedLastYearMin = null,
            int? daysRemainedLastYearMax = null,
            int? daysUsedThisYearMin = null,
            int? daysUsedThisYearMax = null,
            int? daysRemainedMin = null,
            int? daysRemainedMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, daysRemainedLastYearMin, daysRemainedLastYearMax, daysUsedThisYearMin, daysUsedThisYearMax, daysRemainedMin, daysRemainedMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Holiday> ApplyFilter(
            IQueryable<Holiday> query,
            string filterText,
            int? daysRemainedLastYearMin = null,
            int? daysRemainedLastYearMax = null,
            int? daysUsedThisYearMin = null,
            int? daysUsedThisYearMax = null,
            int? daysRemainedMin = null,
            int? daysRemainedMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(daysRemainedLastYearMin.HasValue, e => e.DaysRemainedLastYear >= daysRemainedLastYearMin.Value)
                    .WhereIf(daysRemainedLastYearMax.HasValue, e => e.DaysRemainedLastYear <= daysRemainedLastYearMax.Value)
                    .WhereIf(daysUsedThisYearMin.HasValue, e => e.DaysUsedThisYear >= daysUsedThisYearMin.Value)
                    .WhereIf(daysUsedThisYearMax.HasValue, e => e.DaysUsedThisYear <= daysUsedThisYearMax.Value)
                    .WhereIf(daysRemainedMin.HasValue, e => e.DaysRemained >= daysRemainedMin.Value)
                    .WhereIf(daysRemainedMax.HasValue, e => e.DaysRemained <= daysRemainedMax.Value);
        }
    }
}