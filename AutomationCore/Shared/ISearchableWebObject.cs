﻿using System.Collections.Generic;

namespace AutomationCore.Shared
{
    public interface ISearchableWebObject
    {
        int DefaultActionTimeout { get; set; }
        IDictionary<string, IDictionary<string, string>> SubElements { get; set; }

        IDictionary<string, string> GetElementProperties(string targetElement);
        ITestableWebElement FindSubElement(string targetSubElement);
        ITestableWebElement FindSubElement(string targetSubElement, int timeout);
        ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties);
        ITestableWebElement FindSubElement(IDictionary<string, string> subElementProperties, int timeout);
        IList<ITestableWebElement> FindSubElements(string targetSubElement);
        IList<ITestableWebElement> FindSubElements(string targetSubElement, int timeout);
        IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties);
        IList<ITestableWebElement> FindSubElements(IDictionary<string, string> subElementProperties, int timeout);
        
        void RegisterSubElement(string name, dynamic elementProperties);
        void RegisterSubElement(string name, params string[] elementProperties);
        void RegisterSubElement(string name, IDictionary<string, string> elementProperties);
    }
}
