﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAutomation.Shared;

namespace TestAutomation.Applications.MarketOnce.Controls.Smart
{
    public class SmartGrid
    {
        private ITestableWebPage _baseObject;

        private string BaseName { get; set; }
        private Dictionary<string, int> ColumnIndexs = new Dictionary<string, int>();

        public SmartGrid(ITestableWebPage baseObject, string gridId)
        {
            _baseObject = baseObject;

            BaseName = string.Format("SmartGrid - {0}", gridId);

            if (!_baseObject.SubElements.ContainsKey(BaseName))
            {
                _baseObject.RegisterSubElement(BaseName, new { id = "contains=" + gridId });
                _baseObject.RegisterSubElement(BaseName + " Rows", new { parentelement = BaseName, css = "tr.SmartView_Row, tr.SmartView_AltRow" });

                _baseObject.RegisterSubElement(BaseName + " Records Count", new { parentelement = BaseName, css = ".SmartView_Pager tr[name=PagerRow] td:first-child" });

                _baseObject.RegisterSubElement(BaseName + " Headers", new { parentelement = BaseName, css = "th.SmartView_HeaderRow_Center, th a" });

                var columns = _baseObject.FindSubElements(BaseName + " Headers");

                for (int i = 1; i <= columns.Count; i++)
                {
                    var columnHeader = columns.ElementAt(i - 1);
                    var columnName = columnHeader.InnerHtml();
                    var isLinkColumn = !columnHeader.GetTagName().Equals("a"); //columns are links when the headers are not clickable
                    ColumnIndexs.Add(columnName, i);

                    _baseObject.RegisterSubElement(BaseName + columnName + " Column Data", new { parentelement = BaseName, css = string.Format("tr td:nth-child({0}){1}", i, isLinkColumn ? " a" : string.Empty) });
                }

            }
        }

        public List<ITestableWebElement> FindRows(IDictionary<string, string> columnValuePairs)
        {
            if (columnValuePairs == null)
            {
                return new List<ITestableWebElement>();
            }

            var matches = new List<ITestableWebElement>();
            foreach (var key in columnValuePairs.Keys)
            {
                if (!ColumnIndexs.ContainsKey(key))
                {
                    return new List<ITestableWebElement>();
                }

                if (matches.Any())
                {
                    var thisIterationMatches = new List<ITestableWebElement>();
                    foreach (var match in matches)
                    {
                        var columnMatch = match.FindSubElement(new Dictionary<string, string>()
                        {
                            {"css", string.Format("td:nth-child({0})", ColumnIndexs[key])}
                        });

                        if (columnMatch.InnerHtml().Equals(columnValuePairs[key]))
                        {
                            thisIterationMatches.Add(columnMatch);
                        }
                    }
                    if (!thisIterationMatches.Any())
                    {
                        return new List<ITestableWebElement>();
                    }
                    else
                    {
                        matches = thisIterationMatches;
                    }
                }
                else
                {
                    var expectedValue = columnValuePairs[key];
                    var matchingColumns = _baseObject.FindSubElements(BaseName + key + " Column Data").Where(e => e.InnerHtml().Equals(expectedValue)).ToList();

                    if (matchingColumns.Any())
                    {
                        matches.AddRange(matchingColumns.Select(match => match.Parent()));
                    }

                }
            }
            return matches;
        }

        public List<string> GetColumnContents(string columnName)
        {
            return _baseObject.FindSubElements(BaseName + columnName + " Column Data").Select(e => e.InnerHtml()).ToList();
        } 
    }
}
