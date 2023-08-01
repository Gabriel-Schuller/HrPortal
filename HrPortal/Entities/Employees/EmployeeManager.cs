using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace HrPortal.Employees
{
    public class EmployeeManager : DomainService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> CreateAsync(
        int totalNumberOfDaysThisYear, string name, string cNP, string informationsCI, string rezidence, string sendingAddress, string relevancePhoneNumber, string personalPhoneNumber, DateTime hiringDate, DateTime birthDay, int startingSalary, bool paysProgrammerTaxes)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(cNP, nameof(cNP));
            Check.Length(cNP, nameof(cNP), EmployeeConsts.CNPMaxLength);
            Check.Length(relevancePhoneNumber, nameof(relevancePhoneNumber), EmployeeConsts.RelevancePhoneNumberMaxLength);
            Check.Length(personalPhoneNumber, nameof(personalPhoneNumber), EmployeeConsts.PersonalPhoneNumberMaxLength);
            Check.NotNull(hiringDate, nameof(hiringDate));
            Check.NotNull(birthDay, nameof(birthDay));

            var employee = new Employee(
             GuidGenerator.Create(),
             totalNumberOfDaysThisYear, name, cNP, informationsCI, rezidence, sendingAddress, relevancePhoneNumber, personalPhoneNumber, hiringDate, birthDay, startingSalary, paysProgrammerTaxes
             );

            return await _employeeRepository.InsertAsync(employee);
        }

        public async Task<Employee> UpdateAsync(
            Guid id,
            int totalNumberOfDaysThisYear, string name, string cNP, string informationsCI, string rezidence, string sendingAddress, string relevancePhoneNumber, string personalPhoneNumber, DateTime hiringDate, DateTime birthDay, int startingSalary, bool paysProgrammerTaxes
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(cNP, nameof(cNP));
            Check.Length(cNP, nameof(cNP), EmployeeConsts.CNPMaxLength);
            Check.Length(relevancePhoneNumber, nameof(relevancePhoneNumber), EmployeeConsts.RelevancePhoneNumberMaxLength);
            Check.Length(personalPhoneNumber, nameof(personalPhoneNumber), EmployeeConsts.PersonalPhoneNumberMaxLength);
            Check.NotNull(hiringDate, nameof(hiringDate));
            Check.NotNull(birthDay, nameof(birthDay));

            var employee = await _employeeRepository.GetAsync(id);

            employee.TotalNumberOfDaysThisYear = totalNumberOfDaysThisYear;
            employee.Name = name;
            employee.CNP = cNP;
            employee.InformationsCI = informationsCI;
            employee.Rezidence = rezidence;
            employee.SendingAddress = sendingAddress;
            employee.RelevancePhoneNumber = relevancePhoneNumber;
            employee.PersonalPhoneNumber = personalPhoneNumber;
            employee.HiringDate = hiringDate;
            employee.BirthDay = birthDay;
            employee.StartingSalary = startingSalary;
            employee.PaysProgrammerTaxes = paysProgrammerTaxes;

            return await _employeeRepository.UpdateAsync(employee);
        }

    }
}