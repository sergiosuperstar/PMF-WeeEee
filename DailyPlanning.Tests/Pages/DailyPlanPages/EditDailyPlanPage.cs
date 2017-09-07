using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPlanning.Tests.Pages.DailyPlanPages
{
    class EditDailyPlanPage
    {
        BrowserWindow browser;

        public EditDailyPlanPage(BrowserWindow broserWindow)
        {
            this.browser = broserWindow;
        }

        public bool CheckPageTitle()
        {
            var uiTitle = new HtmlControl(browser);
            uiTitle.SearchProperties.Add(HtmlControl.PropertyNames.Id, PageTitlesConst.PAGE_TITLE_ID);
            uiTitle.Find();
            uiTitle.WaitForControlReady();
            return uiTitle.InnerText.ToString().Equals(PageTitlesConst.EDIT_DAILY_PLAN_TITLE);
        }

        public EditDailyPlanPage SelectWorkItemsDayBefore(string[] items)
        {
            var boxDayBefore = new HtmlList(browser);
            boxDayBefore.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.SELECT_DAILY_PLAN_DAY_BEFORE_LISTBOX_ID);
            boxDayBefore.Find();
            boxDayBefore.SelectedItems = items;

            return this;
        }

        public EditDailyPlanPage SelectWorkItemsToday(string[] items)
        {
            var boxToday = new HtmlList(browser);
            boxToday.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.SELECT_DAILY_PLAN_TODAY_LISTBOX_ID);
            boxToday.Find();
            boxToday.SelectedItems =items;

            return this;
        }

        public EditDailyPlanPage InsertNote(string note)
        {

            var uiNote = new HtmlTextArea(browser);
            uiNote.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.ADD_NOTE_TEXT_ID);
            uiNote.Find();
            uiNote.SetProperty(HtmlEdit.PropertyNames.Text, note);

            return this;
        }

        public DailyPlansPage SaveDailyPlan()
        {

            var uiSave = new HtmlInputButton(browser);
            uiSave.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditDailyPlanPageConst.SAVE_DAILY_PLAN_EDIT_BUTTON_ID);
            uiSave.Find();
            Mouse.Click(uiSave);
            return new DailyPlansPage(browser);
        }
    }
}
