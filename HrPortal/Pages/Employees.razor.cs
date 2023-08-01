using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using HrPortal.Employees;
using HrPortal.Permissions;
using HrPortal.Shared;

namespace HrPortal.Pages
{
    public partial class Employees
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<EmployeeDto> EmployeeList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateEmployee { get; set; }
        private bool CanEditEmployee { get; set; }
        private bool CanDeleteEmployee { get; set; }
        private EmployeeCreateDto NewEmployee { get; set; }
        private Validations NewEmployeeValidations { get; set; } = new();
        private EmployeeUpdateDto EditingEmployee { get; set; }
        private Validations EditingEmployeeValidations { get; set; } = new();
        private Guid EditingEmployeeId { get; set; }
        private Modal CreateEmployeeModal { get; set; } = new();
        private Modal EditEmployeeModal { get; set; } = new();
        private GetEmployeesInput Filter { get; set; }
        private DataGridEntityActionsColumn<EmployeeDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "employee-create-tab";
        protected string SelectedEditTab = "employee-edit-tab";
        
        public Employees()
        {
            NewEmployee = new EmployeeCreateDto();
            EditingEmployee = new EmployeeUpdateDto();
            Filter = new GetEmployeesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            EmployeeList = new List<EmployeeDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }


        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Employees"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            
            
            Toolbar.AddButton(L["NewEmployee"], async () =>
            {
                await OpenCreateEmployeeModalAsync();
            }, IconName.Add, requiredPolicyName: HrPortalPermissions.Employees.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateEmployee = await AuthorizationService
                .IsGrantedAsync(HrPortalPermissions.Employees.Create);
            CanEditEmployee = await AuthorizationService
                            .IsGrantedAsync(HrPortalPermissions.Employees.Edit);
            CanDeleteEmployee = await AuthorizationService
                            .IsGrantedAsync(HrPortalPermissions.Employees.Delete);
        }

        private async Task GetEmployeesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await EmployeesAppService.GetListAsync(Filter);
            EmployeeList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetEmployeesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<EmployeeDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetEmployeesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateEmployeeModalAsync()
        {
            NewEmployee = new EmployeeCreateDto{
                HiringDate = DateTime.Now,
BirthDay = DateTime.Now,

                
            };
            await NewEmployeeValidations.ClearAll();
            await CreateEmployeeModal.Show();
        }

        private async Task CloseCreateEmployeeModalAsync()
        {
            NewEmployee = new EmployeeCreateDto{
                HiringDate = DateTime.Now,
BirthDay = DateTime.Now,

                
            };
            await CreateEmployeeModal.Hide();
        }

        private async Task OpenEditEmployeeModalAsync(EmployeeDto input)
        {
            var employee = await EmployeesAppService.GetAsync(input.Id);
            
            EditingEmployeeId = employee.Id;
            EditingEmployee = ObjectMapper.Map<EmployeeDto, EmployeeUpdateDto>(employee);
            await EditingEmployeeValidations.ClearAll();
            await EditEmployeeModal.Show();
        }

        private async Task DeleteEmployeeAsync(EmployeeDto input)
        {
            await EmployeesAppService.DeleteAsync(input.Id);
            await GetEmployeesAsync();
        }

        private async Task CreateEmployeeAsync()
        {
            try
            {
                if (await NewEmployeeValidations.ValidateAll() == false)
                {
                    return;
                }

                await EmployeesAppService.CreateAsync(NewEmployee);
                await GetEmployeesAsync();
                await CloseCreateEmployeeModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditEmployeeModalAsync()
        {
            await EditEmployeeModal.Hide();
        }

        private async Task UpdateEmployeeAsync()
        {
            try
            {
                if (await EditingEmployeeValidations.ValidateAll() == false)
                {
                    return;
                }

                await EmployeesAppService.UpdateAsync(EditingEmployeeId, EditingEmployee);
                await GetEmployeesAsync();
                await EditEmployeeModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }
        

    }
}
