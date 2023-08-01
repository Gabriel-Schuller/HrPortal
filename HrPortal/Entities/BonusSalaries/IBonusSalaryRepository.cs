using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HrPortal.BonusSalaries
{
    public interface IBonusSalaryRepository : IRepository<BonusSalary, Guid>
    {
        Task<List<BonusSalary>> GetListAsync(
            string filterText = null,
            int? ammountMin = null,
            int? ammountMax = null,
            DateTime? appliedDateMin = null,
            DateTime? appliedDateMax = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            int? ammountMin = null,
            int? ammountMax = null,
            DateTime? appliedDateMin = null,
            DateTime? appliedDateMax = null,
            CancellationToken cancellationToken = default);
    }
}