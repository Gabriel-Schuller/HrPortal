using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace HrPortal.Employees
{
    public class Employee : AuditedEntity<Guid>
    {
        public virtual int TotalNumberOfDaysThisYear { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string CNP { get; set; }

        [CanBeNull]
        public virtual string? InformationsCI { get; set; }

        [CanBeNull]
        public virtual string? Rezidence { get; set; }

        [CanBeNull]
        public virtual string? SendingAddress { get; set; }

        [CanBeNull]
        public virtual string? RelevancePhoneNumber { get; set; }

        [CanBeNull]
        public virtual string? PersonalPhoneNumber { get; set; }

        public virtual DateTime HiringDate { get; set; }

        public virtual DateTime BirthDay { get; set; }

        public virtual int StartingSalary { get; set; }

        public virtual bool PaysProgrammerTaxes { get; set; }

        public Employee()
        {

        }

        public Employee(Guid id, int totalNumberOfDaysThisYear, string name, string cNP, string informationsCI, string rezidence, string sendingAddress, string relevancePhoneNumber, string personalPhoneNumber, DateTime hiringDate, DateTime birthDay, int startingSalary, bool paysProgrammerTaxes)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(cNP, nameof(cNP));
            Check.Length(cNP, nameof(cNP), EmployeeConsts.CNPMaxLength, 0);
            Check.Length(relevancePhoneNumber, nameof(relevancePhoneNumber), EmployeeConsts.RelevancePhoneNumberMaxLength, 0);
            Check.Length(personalPhoneNumber, nameof(personalPhoneNumber), EmployeeConsts.PersonalPhoneNumberMaxLength, 0);
            TotalNumberOfDaysThisYear = totalNumberOfDaysThisYear;
            Name = name;
            CNP = cNP;
            InformationsCI = informationsCI;
            Rezidence = rezidence;
            SendingAddress = sendingAddress;
            RelevancePhoneNumber = relevancePhoneNumber;
            PersonalPhoneNumber = personalPhoneNumber;
            HiringDate = hiringDate;
            BirthDay = birthDay;
            StartingSalary = startingSalary;
            PaysProgrammerTaxes = paysProgrammerTaxes;
        }

    }
}