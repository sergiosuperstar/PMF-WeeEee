using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace DailyPlanning.Tests.Pages.DailyPlanPages
{
    class AddDailyPlanPage
    {

        BrowserWindow browser;

        public AddDailyPlanPage(BrowserWindow browserWindow)
        {
            this.browser = browserWindow;
        }

        public AddDailyPlanPage SelectWorkItemsToday(string[] items)
        {
            var boxDayBefore = new HtmlList(browser);
            boxDayBefore.SearchProperties.Add(HtmlControl.PropertyNames.Class, AddDailyPlanPageConst.SELECT_DAILY_PLAN_TODAY_LISTBOX_CLASS);
            boxDayBefore.Find();
            boxDayBefore.SelectedItems = items;

            return this;
        }

        public AddDailyPlanPage SelectWorkItemsDayBefore(string[] items)
        {
            var boxToday = new HtmlList(browser);
            boxToday.SearchProperties.Add(HtmlControl.PropertyNames.Class, AddDailyPlanPageConst.SELECT_DAILY_PLAN_DAY_BEFORE_LISTBOX_CLASS);
            boxToday.Find();
            boxToday.SelectedItems = items;

            return this;
        }

        public AddDailyPlanPage InsertNote(string note)
        {
            var uiNote = new HtmlTextArea(browser);
            uiNote.SearchProperties.Add(HtmlControl.PropertyNames.Class, AddDailyPlanPageConst.ADD_NOTE_TEXT_CLASS);
            uiNote.Find();
            Keyboard.SendKeys(uiNote, note);

            return this;
        }

        public DailyPlansPage SaveDailyPlan()
        {
            var uiCreate = new HtmlInputButton(browser);
            uiCreate.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.CREATE_DAILY_PLAN_LINK_ID);
            uiCreate.Find();
            Mouse.Click(uiCreate);
           

            return new DailyPlansPage(browser); ;
        }
        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);
            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();
            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.ADD_DAILY_PLAN_TITLE);
        }

        public bool IsValidationDisplayedForSelectTodayWorkItems()
        {
            var uiValidationError = new HtmlControl(browser);
            uiValidationError.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.SELECT_DAILY_PLAN_TODAY_TEXT_ERROR_ID);

            return uiValidationError.TryFind();
        }

        public bool IsValidationDisplayedForSelectDayBeforeWorkItems()
        {
            var uiValidationError = new HtmlControl(browser);
            uiValidationError.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.SELECT_DAILY_PLAN_DAY_BEFORE_TEXT_ERROR_ID);

            return uiValidationError.TryFind();
        }

        public bool IsValidationDisplayedForNote()
        {
            var uiValidationError = new HtmlControl(browser);
            uiValidationError.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.ADD_NOTE_TEXT_ERROR_ID);

            return uiValidationError.TryFind();
        }

       

    }
}
