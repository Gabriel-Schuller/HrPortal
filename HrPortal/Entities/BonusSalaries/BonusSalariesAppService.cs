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
using HrPortal.BonusSalaries;

namespace HrPortal.BonusSalaries
{

    [Authorize(HrPortalPermissions.BonusSalaries.Default)]
    public class BonusSalariesAppService : ApplicationService, IBonusSalariesAppService
    {

        private readonly IBonusSalaryRepository _bonusSalaryRepository;
        private readonly BonusSalaryManager _bonusSalaryManager;

        public BonusSalariesAppService(IBonusSalaryRepository bonusSalaryRepository, BonusSalaryManager bonusSalaryManager)
        {

            _bonusSalaryRepository = bonusSalaryRepository;
            _bonusSalaryManager = bonusSalaryManager;
        }

        public virtual async Task<PagedResultDto<BonusSalaryDto>> GetListAsync(GetBonusSalariesInput input)
        {
            var totalCount = await _bonusSalaryRepository.GetCountAsync(input.FilterText, input.AmmountMin, input.AmmountMax, input.AppliedDateMin, input.AppliedDateMax);
            var items = await _bonusSalaryRepository.GetListAsync(input.FilterText, input.AmmountMin, input.AmmountMax, input.AppliedDateMin, input.AppliedDateMax, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<BonusSalaryDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<BonusSalary>, List<BonusSalaryDto>>(items)
            };
        }

        public virtual async Task<BonusSalaryDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<BonusSalary, BonusSalaryDto>(await _bonusSalaryRepository.GetAsync(id));
        }

        [Authorize(HrPortalPermissions.BonusSalaries.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _bonusSalaryRepository.DeleteAsync(id);
        }

        [Authorize(HrPortalPermissions.BonusSalaries.Create)]
        public virtual async Task<BonusSalaryDto> CreateAsync(BonusSalaryCreateDto input)
        {

            var bonusSalary = await _bonusSalaryManager.CreateAsync(
            input.Ammount, input.AppliedDate
            );

            return ObjectMapper.Map<BonusSalary, BonusSalaryDto>(bonusSalary);
        }

        [Authorize(HrPortalPermissions.BonusSalaries.Edit)]
        public virtual async Task<BonusSalaryDto> UpdateAsync(Guid id, BonusSalaryUpdateDto input)
        {

            var bonusSalary = await _bonusSalaryManager.UpdateAsync(
            id,
            input.Ammount, input.AppliedDate
            );

            return ObjectMapper.Map<BonusSalary, BonusSalaryDto>(bonusSalary);
        }
    }
}