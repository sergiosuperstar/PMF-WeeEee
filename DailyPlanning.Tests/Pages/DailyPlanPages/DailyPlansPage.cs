﻿using DailyPlanning.Tests.Constants;
using DailyPlanning.Tests.Pages.WorkItemPages;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanning.Tests.Pages.DailyPlanPages
{
    class DailyPlansPage
    {
        private BrowserWindow browser;

        public DailyPlansPage(BrowserWindow browser)
        {
            this.browser = browser;
        }

        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);
            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();
            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.INDEX_DAILY_PLAN_TITLE);
        }

        public void NavigateDailyPlans()
        {
            var uiLinkDailyPlans = new HtmlHyperlink(browser);
            uiLinkDailyPlans.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexDailyPlanPageConst.MENU_DAILY_PLAN_LINK_ID);
            uiLinkDailyPlans.Find();
            Mouse.Click(uiLinkDailyPlans);
        }

        public AddDailyPlanPage NavigateToAddDailyPlan()
        {
            var uiLinkDailyPlans = new HtmlHyperlink(browser);

            uiLinkDailyPlans.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.INDEX_DAILY_PLAN_ADD_LINK_ID);
            uiLinkDailyPlans.Find();
            Mouse.Click(uiLinkDailyPlans);

            return new AddDailyPlanPage(browser);
        }

        public EditDailyPlanPage NavigateToEditDailyPlan()
        {
            var uiLinkAdd = new HtmlHyperlink(browser);
            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditDailyPlanPageConst.INDEX_DAILY_PLAN_EDIT_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            return new EditDailyPlanPage(browser);
        }

        public DetailsDailyPlanPage NavigateToDetailsDailyPlan()
        {
            var uiDetails = new HtmlHyperlink(browser);
            uiDetails.SearchProperties.Add(HtmlControl.PropertyNames.Id, DailyPlanPageConst.INDEX_DAILYPLAN_DETAILS_LINK_ID);
            uiDetails.Find();
            Mouse.Click(uiDetails);

            return new DetailsDailyPlanPage(browser);
        }

        public WorkItemDetailsPage NavigateToDetailsTodayWorkItem()
        {
            var uiWITodayDetails = new HtmlHyperlink(browser);
            uiWITodayDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, DailyPlanPageConst.INDEX_DAILY_PLAN_TODAY_DETAILS_LINK_CLASS);
            uiWITodayDetails.Find();
            Mouse.Click(uiWITodayDetails);

            return new WorkItemDetailsPage(browser);
        }

        public WorkItemDetailsPage NavigateToDetailsDayBeforeWorkItem()
        {
            var uiWITodayDetails = new HtmlHyperlink(browser);
            uiWITodayDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, DailyPlanPageConst.INDEX_DAILY_PLAN_DAY_BEFORE_DETAILS_LINK_CLASS);
            uiWITodayDetails.Find();
            Mouse.Click(uiWITodayDetails);

            return new WorkItemDetailsPage(browser);
        }

        public int TableRowCount()
        {
            var uiTable = new HtmlTable(browser);
            uiTable.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexDailyPlanPageConst.INDEX_TABLE_ID);
            uiTable.Find();

            return uiTable.RowCount;

        }






    }
}
