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
using HrPortal.BonusSalaries;
using HrPortal.Permissions;
using HrPortal.Shared;

namespace HrPortal.Pages
{
    public partial class BonusSalaries
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private IReadOnlyList<BonusSalaryDto> BonusSalaryList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateBonusSalary { get; set; }
        private bool CanEditBonusSalary { get; set; }
        private bool CanDeleteBonusSalary { get; set; }
        private BonusSalaryCreateDto NewBonusSalary { get; set; }
        private Validations NewBonusSalaryValidations { get; set; } = new();
        private BonusSalaryUpdateDto EditingBonusSalary { get; set; }
        private Validations EditingBonusSalaryValidations { get; set; } = new();
        private Guid EditingBonusSalaryId { get; set; }
        private Modal CreateBonusSalaryModal { get; set; } = new();
        private Modal EditBonusSalaryModal { get; set; } = new();
        private GetBonusSalariesInput Filter { get; set; }
        private DataGridEntityActionsColumn<BonusSalaryDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "bonusSalary-create-tab";
        protected string SelectedEditTab = "bonusSalary-edit-tab";

        public BonusSalaries()
        {
            NewBonusSalary = new BonusSalaryCreateDto();
            EditingBonusSalary = new BonusSalaryUpdateDto();
            Filter = new GetBonusSalariesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            BonusSalaryList = new List<BonusSalaryDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:BonusSalaries"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {


            Toolbar.AddButton(L["NewBonusSalary"], async () =>
            {
                await OpenCreateBonusSalaryModalAsync();
            }, IconName.Add, requiredPolicyName: HrPortalPermissions.BonusSalaries.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateBonusSalary = await AuthorizationService
                .IsGrantedAsync(HrPortalPermissions.BonusSalaries.Create);
            CanEditBonusSalary = await AuthorizationService
                            .IsGrantedAsync(HrPortalPermissions.BonusSalaries.Edit);
            CanDeleteBonusSalary = await AuthorizationService
                            .IsGrantedAsync(HrPortalPermissions.BonusSalaries.Delete);
        }

        private async Task GetBonusSalariesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await BonusSalariesAppService.GetListAsync(Filter);
            BonusSalaryList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetBonusSalariesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<BonusSalaryDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetBonusSalariesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateBonusSalaryModalAsync()
        {
            NewBonusSalary = new BonusSalaryCreateDto
            {
                AppliedDate = DateTime.Now,


            };
            await NewBonusSalaryValidations.ClearAll();
            await CreateBonusSalaryModal.Show();
        }

        private async Task CloseCreateBonusSalaryModalAsync()
        {
            NewBonusSalary = new BonusSalaryCreateDto
            {
                AppliedDate = DateTime.Now,


            };
            await CreateBonusSalaryModal.Hide();
        }

        private async Task OpenEditBonusSalaryModalAsync(BonusSalaryDto input)
        {
            var bonusSalary = await BonusSalariesAppService.GetAsync(input.Id);

            EditingBonusSalaryId = bonusSalary.Id;
            EditingBonusSalary = ObjectMapper.Map<BonusSalaryDto, BonusSalaryUpdateDto>(bonusSalary);
            await EditingBonusSalaryValidations.ClearAll();
            await EditBonusSalaryModal.Show();
        }

        private async Task DeleteBonusSalaryAsync(BonusSalaryDto input)
        {
            await BonusSalariesAppService.DeleteAsync(input.Id);
            await GetBonusSalariesAsync();
        }

        private async Task CreateBonusSalaryAsync()
        {
            try
            {
                if (await NewBonusSalaryValidations.ValidateAll() == false)
                {
                    return;
                }

                await BonusSalariesAppService.CreateAsync(NewBonusSalary);
                await GetBonusSalariesAsync();
                await CloseCreateBonusSalaryModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditBonusSalaryModalAsync()
        {
            await EditBonusSalaryModal.Hide();
        }

        private async Task UpdateBonusSalaryAsync()
        {
            try
            {
                if (await EditingBonusSalaryValidations.ValidateAll() == false)
                {
                    return;
                }

                await BonusSalariesAppService.UpdateAsync(EditingBonusSalaryId, EditingBonusSalary);
                await GetBonusSalariesAsync();
                await EditBonusSalaryModal.Hide();
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
