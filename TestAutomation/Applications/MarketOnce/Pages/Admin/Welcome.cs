﻿using AutomationCore.Initialization;
using AutomationCore.Shared;
using TestAutomation.Applications.MarketOnce.Pages.Home;

namespace TestAutomation.Applications.MarketOnce.Pages.Admin
{
    public class Welcome : BasePage
    {
        public Welcome(ITestableWebPage baseObject) : base(baseObject)
        {
            RegisterSubElement("Email Dashboard", new { Text = "Email", Id = "contains=ctl00_cphMain_dlModules" });
            RegisterSubElement("Users Link", new { Text = "Users", Id = "contains=marketOnceSiteMenu_ssmAdmin_childItems" });
            
            FindSubElement("Logout", 120);
            Global.TestOutput.WriteLineWithScreenshot("Opened Welcome Page", GetScreenshot());
        }

        public EmailDashboard NavigateToEmailModule()
        {
            Click("Email Dashboard");

            return New<EmailDashboard>();
        }

        public Users NavigateToUserAdmin()
        {
            Click("Users Link");

            return New<Users>();
        }
    }
}
