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
using DailyPlanning.Infrastructure.Database;

namespace DailyPlanning.Tests.TestScenarios
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class BaseTest
    {
        protected BrowserWindow browser;

        [TestInitialize()]
        public void Initialize()
        {
        //    DailyPlanningInitializationHandler.Initialize();
            browser = BrowserWindow.Launch("http://localhost:54813");
        }

        [TestCleanup()]
        public void Cleanup()
        {
            browser.Close();
        }
    }
}
