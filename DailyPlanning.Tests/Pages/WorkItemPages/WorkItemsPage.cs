﻿using DailyPlanning.Tests.Constants;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DailyPlanning.Tests.Pages.WorkItemPages
{
    class WorkItemsPage
    {
        private BrowserWindow browser;

        public WorkItemsPage(BrowserWindow browser)
        {
            this.browser = browser;
        }
        
        public void Navigate()
        {
            var uiLinkProjects = new HtmlHyperlink(browser);

            uiLinkProjects.SearchProperties.Add(HtmlControl.PropertyNames.Id, IndexPageConst.MENU_WORKITEM_LINK_ID);
            uiLinkProjects.Find();
            Mouse.Click(uiLinkProjects);
        }

        public AddWorkItemPage goToAddWorkItem()
        {
            var uiLinkAdd = new HtmlHyperlink(browser);

            uiLinkAdd.SearchProperties.Add(HtmlControl.PropertyNames.Id, WorkItemsPageConst.INDEX_ADD_LINK_ID);
            uiLinkAdd.Find();
            Mouse.Click(uiLinkAdd);

            return new AddWorkItemPage(browser);
        }

        public EditWorkItemPage goToEditWorkItem()
        {
            var uiLinkEdit = new HtmlHyperlink(browser);

            uiLinkEdit.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_EDIT_LINK_CLASS);
            uiLinkEdit.TryFind();
            Mouse.Click(uiLinkEdit);

            return new EditWorkItemPage(browser);
        }

        public WorkItemDetailsPage goToDetails()
        {
            var uiLinkDetails = new HtmlHyperlink(browser);

            uiLinkDetails.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_DETAILS_LINK_CLASS);
            uiLinkDetails.Find();
            Mouse.Click(uiLinkDetails);

            return new WorkItemDetailsPage(browser);
        }

        public DeleteWorkItemPage DeleteWorkItem()
        {
            var uiLinkDelete = new HtmlHyperlink(browser);
            uiLinkDelete.SearchProperties.Add(HtmlControl.PropertyNames.Class, WorkItemsPageConst.INDEX_DELETE_LINK_CLASS);
            uiLinkDelete.Find();
            Mouse.Click(uiLinkDelete);

            return new DeleteWorkItemPage(browser);
        }
    }
}
