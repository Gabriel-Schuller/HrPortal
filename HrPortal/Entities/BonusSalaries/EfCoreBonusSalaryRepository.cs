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

namespace HrPortal.BonusSalaries
{
    public class EfCoreBonusSalaryRepository : EfCoreRepository<HrPortalDbContext, BonusSalary, Guid>, IBonusSalaryRepository
    {
        public EfCoreBonusSalaryRepository(IDbContextProvider<HrPortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<BonusSalary>> GetListAsync(
            string filterText = null,
            int? ammountMin = null,
            int? ammountMax = null,
            DateTime? appliedDateMin = null,
            DateTime? appliedDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, ammountMin, ammountMax, appliedDateMin, appliedDateMax);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? BonusSalaryConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? ammountMin = null,
            int? ammountMax = null,
            DateTime? appliedDateMin = null,
            DateTime? appliedDateMax = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, ammountMin, ammountMax, appliedDateMin, appliedDateMax);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<BonusSalary> ApplyFilter(
            IQueryable<BonusSalary> query,
            string filterText,
            int? ammountMin = null,
            int? ammountMax = null,
            DateTime? appliedDateMin = null,
            DateTime? appliedDateMax = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true)
                    .WhereIf(ammountMin.HasValue, e => e.Ammount >= ammountMin.Value)
                    .WhereIf(ammountMax.HasValue, e => e.Ammount <= ammountMax.Value)
                    .WhereIf(appliedDateMin.HasValue, e => e.AppliedDate >= appliedDateMin.Value)
                    .WhereIf(appliedDateMax.HasValue, e => e.AppliedDate <= appliedDateMax.Value);
        }
    }
}