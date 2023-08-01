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
using HrPortal.Holidays;
using HrPortal.Permissions;
using HrPortal.Shared;

namespace HrPortal.Pages
{
    public partial class Holidays
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar { get; } = new PageToolbar();
        private IReadOnlyList<HolidayDto> HolidayList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateHoliday { get; set; }
        private bool CanEditHoliday { get; set; }
        private bool CanDeleteHoliday { get; set; }
        private HolidayCreateDto NewHoliday { get; set; }
        private Validations NewHolidayValidations { get; set; } = new();
        private HolidayUpdateDto EditingHoliday { get; set; }
        private Validations EditingHolidayValidations { get; set; } = new();
        private Guid EditingHolidayId { get; set; }
        private Modal CreateHolidayModal { get; set; } = new();
        private Modal EditHolidayModal { get; set; } = new();
        private GetHolidaysInput Filter { get; set; }
        private DataGridEntityActionsColumn<HolidayDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "holiday-create-tab";
        protected string SelectedEditTab = "holiday-edit-tab";

        public Holidays()
        {
            NewHoliday = new HolidayCreateDto();
            EditingHoliday = new HolidayUpdateDto();
            Filter = new GetHolidaysInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            HolidayList = new List<HolidayDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }



        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:Holidays"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {


            Toolbar.AddButton(L["NewHoliday"], async () =>
            {
                await OpenCreateHolidayModalAsync();
            }, IconName.Add, requiredPolicyName: HrPortalPermissions.Holidays.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateHoliday = await AuthorizationService
                .IsGrantedAsync(HrPortalPermissions.Holidays.Create);
            CanEditHoliday = await AuthorizationService
                            .IsGrantedAsync(HrPortalPermissions.Holidays.Edit);
            CanDeleteHoliday = await AuthorizationService
                            .IsGrantedAsync(HrPortalPermissions.Holidays.Delete);
        }

        private async Task GetHolidaysAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await HolidaysAppService.GetListAsync(Filter);
            HolidayList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetHolidaysAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<HolidayDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetHolidaysAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateHolidayModalAsync()
        {
            NewHoliday = new HolidayCreateDto
            {


            };
            await NewHolidayValidations.ClearAll();
            await CreateHolidayModal.Show();
        }

        private async Task CloseCreateHolidayModalAsync()
        {
            NewHoliday = new HolidayCreateDto
            {


            };
            await CreateHolidayModal.Hide();
        }

        private async Task OpenEditHolidayModalAsync(HolidayDto input)
        {
            var holiday = await HolidaysAppService.GetAsync(input.Id);

            EditingHolidayId = holiday.Id;
            EditingHoliday = ObjectMapper.Map<HolidayDto, HolidayUpdateDto>(holiday);
            await EditingHolidayValidations.ClearAll();
            await EditHolidayModal.Show();
        }

        private async Task DeleteHolidayAsync(HolidayDto input)
        {
            await HolidaysAppService.DeleteAsync(input.Id);
            await GetHolidaysAsync();
        }

        private async Task CreateHolidayAsync()
        {
            try
            {
                if (await NewHolidayValidations.ValidateAll() == false)
                {
                    return;
                }

                await HolidaysAppService.CreateAsync(NewHoliday);
                await GetHolidaysAsync();
                await CloseCreateHolidayModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditHolidayModalAsync()
        {
            await EditHolidayModal.Hide();
        }

        private async Task UpdateHolidayAsync()
        {
            try
            {
                if (await EditingHolidayValidations.ValidateAll() == false)
                {
                    return;
                }

                await HolidaysAppService.UpdateAsync(EditingHolidayId, EditingHoliday);
                await GetHolidaysAsync();
                await EditHolidayModal.Hide();
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
