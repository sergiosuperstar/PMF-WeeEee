using DailyPlanning.Infrastructure.Database;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace DailyPlanning.Tests.TestScenarios
{
    [CodedUITest]
    public class BaseTest
    {
        protected BrowserWindow browser;

        [TestInitialize()]
        public void Initialize()
        {
            browser = BrowserWindow.Launch("http://localhost:54813");
        }

        [TestCleanup()]
        public void Cleanup()
        {
            browser.Close();
        }
    }
}
