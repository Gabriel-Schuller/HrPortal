using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HrPortal.BonusSalaries
{
    public interface IBonusSalariesAppService : IApplicationService
    {
        Task<PagedResultDto<BonusSalaryDto>> GetListAsync(GetBonusSalariesInput input);

        Task<BonusSalaryDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<BonusSalaryDto> CreateAsync(BonusSalaryCreateDto input);

        Task<BonusSalaryDto> UpdateAsync(Guid id, BonusSalaryUpdateDto input);
    }
}