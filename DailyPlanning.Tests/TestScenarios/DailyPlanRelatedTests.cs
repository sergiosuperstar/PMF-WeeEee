using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using DailyPlanning.Tests.Constants;

namespace DailyPlanning.Tests.TestScenarios
{

    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class DailyPlanRelatedTests
    {
        BrowserWindow browser;

        [TestInitialize()]
        public void Initialize()
        {
            browser = BrowserWindow.Launch("http://localhost:54813");
        }

        [TestMethod]
        public void GoToDailyPlansTest()
        {
            var uiLinkDailyPlans = new HtmlHyperlink(browser);
            uiLinkDailyPlans.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexDailyPlanPageConst.MENU_DAILY_PLAN_LINK_ID);
            uiLinkDailyPlans.Find();
            Mouse.Click(uiLinkDailyPlans);
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.

        }


        [TestMethod]
        public void AddDailyPlanTest()
        {


            GoToDailyPlansTest();

            var uiLinkAdd = new HtmlHyperlink(browser);
            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.INDEX_DAILY_PLAN_ADD_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            var box = new HtmlList(browser);
            box.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.SELECT_DAILY_PLAN_DAY_BEFORE_LISTBOX_ID);
            box.Find();
            box.SelectedItems = new string[]{"WorkItem 2"};

            var boxToday = new HtmlList(browser);
            box.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.SELECT_DAILY_PLAN_TODAY_LISTBOX_ID);
            box.Find();
            box.SelectedItems = new string[] { "WorkItem 2" };

            var uiNote = new HtmlTextArea(browser);
            uiNote.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.ADD_NOTE_TEXT_ID);
            uiNote.Find();
            Keyboard.SendKeys(uiNote, "test note");

            var uiCreate = new HtmlInputButton(browser);
            uiCreate.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.CREATE_DAILY_PLAN_LINK_ID);
            uiCreate.Find();
            Mouse.Click(uiCreate);

        }


        [TestMethod]
        public void EditDailyPlanTest()
        {
       
            AddDailyPlanTest();


            var uiLinkAdd = new HtmlHyperlink(browser);
            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditDailyPlanPageConst.INDEX_DAILY_PLAN_EDIT_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            var box = new HtmlList(browser);
            box.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.SELECT_DAILY_PLAN_DAY_BEFORE_LISTBOX_ID);
            box.Find();
            box.SelectedItems = new string[] { "WorkItem 3" };

            var boxToday = new HtmlList(browser);
            box.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.SELECT_DAILY_PLAN_TODAY_LISTBOX_ID);
            box.Find();
            box.SelectedItems = new string[] { "WorkItem 1" };

            var uiNote = new HtmlTextArea(browser);
            uiNote.SearchProperties.Add(HtmlControl.PropertyNames.Id, AddDailyPlanPageConst.ADD_NOTE_TEXT_ID);
            uiNote.Find();
            uiNote.SetProperty(HtmlEdit.PropertyNames.Text, "test edit note");

            var uiCreate = new HtmlInputButton(browser);
            uiCreate.SearchProperties.Add(HtmlControl.PropertyNames.Id, EditDailyPlanPageConst.SAVE_DAILY_PLAN_EDIT_BUTTON_ID);
            uiCreate.Find();
            Mouse.Click(uiCreate);

        }

        [TestCleanup()]
        public void Cleanup()
        {
            browser.Close();
        }

    }
}
