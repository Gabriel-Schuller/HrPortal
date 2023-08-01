using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HrPortal.Employees
{
    public interface IEmployeeRepository : IRepository<Employee, Guid>
    {
        Task<List<Employee>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}