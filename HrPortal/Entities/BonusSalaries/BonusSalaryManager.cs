using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HrPortal.BonusSalaries
{
    public class BonusSalaryManager : DomainService
    {
        private readonly IBonusSalaryRepository _bonusSalaryRepository;

        public BonusSalaryManager(IBonusSalaryRepository bonusSalaryRepository)
        {
            _bonusSalaryRepository = bonusSalaryRepository;
        }

        public async Task<BonusSalary> CreateAsync(
        int ammount, DateTime appliedDate)
        {
            Check.NotNull(appliedDate, nameof(appliedDate));

            var bonusSalary = new BonusSalary(
             GuidGenerator.Create(),
             ammount, appliedDate
             );

            return await _bonusSalaryRepository.InsertAsync(bonusSalary);
        }

        public async Task<BonusSalary> UpdateAsync(
            Guid id,
            int ammount, DateTime appliedDate
        )
        {
            Check.NotNull(appliedDate, nameof(appliedDate));

            var bonusSalary = await _bonusSalaryRepository.GetAsync(id);

            bonusSalary.Ammount = ammount;
            bonusSalary.AppliedDate = appliedDate;

            return await _bonusSalaryRepository.UpdateAsync(bonusSalary);
        }

    }
}