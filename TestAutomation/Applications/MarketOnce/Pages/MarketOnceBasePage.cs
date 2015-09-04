﻿using System;
using System.Linq;
using AutomationCore;
using AutomationCore.Shared;
using AutomationCore.Shared.Exceptions;

namespace TestAutomation.Applications.MarketOnce.Pages
{
    public class MarketOnceBasePage : WebPage
    {
        public MarketOnceBasePage(ITestableWebPage baseObject) : base(baseObject)
        {
            //since the base object is re-used through many pages only add these once
            if (!SubElements.ContainsKey("Logout"))
            {
                RegisterSubElement("Logout", new { Id = "ctl00_ucHeader_lnkLogout" });
                RegisterSubElement("Menu Link", new { css = "[id^=ctl00_marketOnceSiteMenu_ssmSiteMenu__mnu][id$=_lnk]" });
                RegisterSubElement("Menu Link SubMenu Expand", new { css = "[id^=ctl00_marketOnceSiteMenu_ssmSiteMenu__mnu][id$=_openmenu]" });
                RegisterSubElement("Menu Link SubMenu Link", new { css = "[id^=ctl00_marketOnceSiteMenu_ssmSiteMenu__mnu][id$=_lnkChild]" });
            }
        }

        /// <summary>
        /// Navigates to the marketonce page requesetd
        /// </summary>
        /// <typeparam name="T">The page that you are expecting to navigate to</typeparam>
        /// <param name="navigationPath"></param>
        /// <returns></returns>
        public T NavigateMenu<T>(params string[] navigationPath) where T : WebPage
        {
            if (navigationPath != null && navigationPath.Length >= 1)
            {
                var mainNavigationElements = FindSubElements("Menu Link", 120);
                var targetLink = mainNavigationElements.FirstOrDefault(e => e.InnerHtml().Contains(navigationPath[0]));

                if (targetLink != null && navigationPath.Length == 1)
                {
                    targetLink.Click();
                }
                
                if (targetLink != null && navigationPath.Length == 2)
                {
                    var container = targetLink.Parent(2);

                    var expandMenu = container.FindSubElement(GetElementProperties("Menu Link SubMenu Expand"), 120);
                    var navigationTargets = container.FindSubElements(GetElementProperties("Menu Link SubMenu Link"), 120);

                    expandMenu.Click();

                    targetLink = navigationTargets.FirstOrDefault(e => e.InnerHtml().Contains(navigationPath[1]));

                    if (targetLink != null)
                    {
                        targetLink.Click();
                    }
                }

                return New<T>();
            }

            throw new Exception(string.Format("Invalid Navigation Path {0}", string.Join(", ", navigationPath ?? new string[0])));
        }

        public void CompleteTextInput(string field, string value)
        {
            var element = FindSubElement(field);

            Type(element, value);

            if (!element.GetAttribute("value").Equals(value))
            {
                throw new ActionFailedException(string.Format("Field {0} should have the value {1} but it does not!", field, value));
            }
        }

    }
}