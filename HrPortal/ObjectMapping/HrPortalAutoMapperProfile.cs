using HrPortal.Employees;
using HrPortal.Holidays;
using System;
using HrPortal.Shared;
using Volo.Abp.AutoMapper;
using HrPortal.BonusSalaries;
using AutoMapper;

namespace HrPortal.ObjectMapping;

public class HrPortalAutoMapperProfile : Profile
{
    public HrPortalAutoMapperProfile()
    {
        /* Create your AutoMapper object mappings here */

        CreateMap<BonusSalary, BonusSalaryDto>();

        CreateMap<BonusSalaryDto, BonusSalaryUpdateDto>();

        CreateMap<Holiday, HolidayDto>();

        CreateMap<HolidayDto, HolidayUpdateDto>();

        CreateMap<Employee, EmployeeDto>();

        CreateMap<EmployeeDto, EmployeeUpdateDto>();
    }
}