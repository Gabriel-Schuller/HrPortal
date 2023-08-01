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

namespace HrPortal.Employees
{
    public class EfCoreEmployeeRepository : EfCoreRepository<HrPortalDbContext, Employee, Guid>, IEmployeeRepository
    {
        public EfCoreEmployeeRepository(IDbContextProvider<HrPortalDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Employee>> GetListAsync(
            string filterText = null,
            int? totalNumberOfDaysThisYearMin = null,
            int? totalNumberOfDaysThisYearMax = null,
            string name = null,
            string cNP = null,
            string informationsCI = null,
            string rezidence = null,
            string sendingAddress = null,
            string relevancePhoneNumber = null,
            string personalPhoneNumber = null,
            DateTime? hiringDateMin = null,
            DateTime? hiringDateMax = null,
            DateTime? birthDayMin = null,
            DateTime? birthDayMax = null,
            int? startingSalaryMin = null,
            int? startingSalaryMax = null,
            bool? paysProgrammerTaxes = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, totalNumberOfDaysThisYearMin, totalNumberOfDaysThisYearMax, name, cNP, informationsCI, rezidence, sendingAddress, relevancePhoneNumber, personalPhoneNumber, hiringDateMin, hiringDateMax, birthDayMin, birthDayMax, startingSalaryMin, startingSalaryMax, paysProgrammerTaxes);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EmployeeConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            int? totalNumberOfDaysThisYearMin = null,
            int? totalNumberOfDaysThisYearMax = null,
            string name = null,
            string cNP = null,
            string informationsCI = null,
            string rezidence = null,
            string sendingAddress = null,
            string relevancePhoneNumber = null,
            string personalPhoneNumber = null,
            DateTime? hiringDateMin = null,
            DateTime? hiringDateMax = null,
            DateTime? birthDayMin = null,
            DateTime? birthDayMax = null,
            int? startingSalaryMin = null,
            int? startingSalaryMax = null,
            bool? paysProgrammerTaxes = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, totalNumberOfDaysThisYearMin, totalNumberOfDaysThisYearMax, name, cNP, informationsCI, rezidence, sendingAddress, relevancePhoneNumber, personalPhoneNumber, hiringDateMin, hiringDateMax, birthDayMin, birthDayMax, startingSalaryMin, startingSalaryMax, paysProgrammerTaxes);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<Employee> ApplyFilter(
            IQueryable<Employee> query,
            string filterText,
            int? totalNumberOfDaysThisYearMin = null,
            int? totalNumberOfDaysThisYearMax = null,
            string name = null,
            string cNP = null,
            string informationsCI = null,
            string rezidence = null,
            string sendingAddress = null,
            string relevancePhoneNumber = null,
            string personalPhoneNumber = null,
            DateTime? hiringDateMin = null,
            DateTime? hiringDateMax = null,
            DateTime? birthDayMin = null,
            DateTime? birthDayMax = null,
            int? startingSalaryMin = null,
            int? startingSalaryMax = null,
            bool? paysProgrammerTaxes = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.CNP.Contains(filterText) || e.InformationsCI.Contains(filterText) || e.Rezidence.Contains(filterText) || e.SendingAddress.Contains(filterText) || e.RelevancePhoneNumber.Contains(filterText) || e.PersonalPhoneNumber.Contains(filterText))
                    .WhereIf(totalNumberOfDaysThisYearMin.HasValue, e => e.TotalNumberOfDaysThisYear >= totalNumberOfDaysThisYearMin.Value)
                    .WhereIf(totalNumberOfDaysThisYearMax.HasValue, e => e.TotalNumberOfDaysThisYear <= totalNumberOfDaysThisYearMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(cNP), e => e.CNP.Contains(cNP))
                    .WhereIf(!string.IsNullOrWhiteSpace(informationsCI), e => e.InformationsCI.Contains(informationsCI))
                    .WhereIf(!string.IsNullOrWhiteSpace(rezidence), e => e.Rezidence.Contains(rezidence))
                    .WhereIf(!string.IsNullOrWhiteSpace(sendingAddress), e => e.SendingAddress.Contains(sendingAddress))
                    .WhereIf(!string.IsNullOrWhiteSpace(relevancePhoneNumber), e => e.RelevancePhoneNumber.Contains(relevancePhoneNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(personalPhoneNumber), e => e.PersonalPhoneNumber.Contains(personalPhoneNumber))
                    .WhereIf(hiringDateMin.HasValue, e => e.HiringDate >= hiringDateMin.Value)
                    .WhereIf(hiringDateMax.HasValue, e => e.HiringDate <= hiringDateMax.Value)
                    .WhereIf(birthDayMin.HasValue, e => e.BirthDay >= birthDayMin.Value)
                    .WhereIf(birthDayMax.HasValue, e => e.BirthDay <= birthDayMax.Value)
                    .WhereIf(startingSalaryMin.HasValue, e => e.StartingSalary >= startingSalaryMin.Value)
                    .WhereIf(startingSalaryMax.HasValue, e => e.StartingSalary <= startingSalaryMax.Value)
                    .WhereIf(paysProgrammerTaxes.HasValue, e => e.PaysProgrammerTaxes == paysProgrammerTaxes);
        }
    }
}