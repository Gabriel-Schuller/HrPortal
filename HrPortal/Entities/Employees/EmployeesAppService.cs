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
using HrPortal.Employees;

namespace HrPortal.Employees
{

    [Authorize(HrPortalPermissions.Employees.Default)]
    public class EmployeesAppService : ApplicationService, IEmployeesAppService
    {

        private readonly IEmployeeRepository _employeeRepository;
        private readonly EmployeeManager _employeeManager;

        public EmployeesAppService(IEmployeeRepository employeeRepository, EmployeeManager employeeManager)
        {

            _employeeRepository = employeeRepository;
            _employeeManager = employeeManager;
        }

        public virtual async Task<PagedResultDto<EmployeeDto>> GetListAsync(GetEmployeesInput input)
        {
            var totalCount = await _employeeRepository.GetCountAsync(input.FilterText, input.TotalNumberOfDaysThisYearMin, input.TotalNumberOfDaysThisYearMax, input.Name, input.CNP, input.InformationsCI, input.Rezidence, input.SendingAddress, input.RelevancePhoneNumber, input.PersonalPhoneNumber, input.HiringDateMin, input.HiringDateMax, input.BirthDayMin, input.BirthDayMax, input.StartingSalaryMin, input.StartingSalaryMax, input.PaysProgrammerTaxes);
            var items = await _employeeRepository.GetListAsync(input.FilterText, input.TotalNumberOfDaysThisYearMin, input.TotalNumberOfDaysThisYearMax, input.Name, input.CNP, input.InformationsCI, input.Rezidence, input.SendingAddress, input.RelevancePhoneNumber, input.PersonalPhoneNumber, input.HiringDateMin, input.HiringDateMax, input.BirthDayMin, input.BirthDayMax, input.StartingSalaryMin, input.StartingSalaryMax, input.PaysProgrammerTaxes, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<EmployeeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Employee>, List<EmployeeDto>>(items)
            };
        }

        public virtual async Task<EmployeeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Employee, EmployeeDto>(await _employeeRepository.GetAsync(id));
        }

        [Authorize(HrPortalPermissions.Employees.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _employeeRepository.DeleteAsync(id);
        }

        [Authorize(HrPortalPermissions.Employees.Create)]
        public virtual async Task<EmployeeDto> CreateAsync(EmployeeCreateDto input)
        {

            var employee = await _employeeManager.CreateAsync(
            input.TotalNumberOfDaysThisYear, input.Name, input.CNP, input.InformationsCI, input.Rezidence, input.SendingAddress, input.RelevancePhoneNumber, input.PersonalPhoneNumber, input.HiringDate, input.BirthDay, input.StartingSalary, input.PaysProgrammerTaxes
            );

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }

        [Authorize(HrPortalPermissions.Employees.Edit)]
        public virtual async Task<EmployeeDto> UpdateAsync(Guid id, EmployeeUpdateDto input)
        {

            var employee = await _employeeManager.UpdateAsync(
            id,
            input.TotalNumberOfDaysThisYear, input.Name, input.CNP, input.InformationsCI, input.Rezidence, input.SendingAddress, input.RelevancePhoneNumber, input.PersonalPhoneNumber, input.HiringDate, input.BirthDay, input.StartingSalary, input.PaysProgrammerTaxes
            );

            return ObjectMapper.Map<Employee, EmployeeDto>(employee);
        }
    }
}