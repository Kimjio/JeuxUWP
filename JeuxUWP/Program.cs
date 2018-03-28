using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Jeux
{
    public class Program
    {
        string food ="";
        private bool isExitDay;

        public Program()
        {
        }
        public async Task settingAsync()
        {
            string url = "https://stu.dge.go.kr/sts_sci_md00_001.do?schulCode=D100000282&schulCrseScCode=4&schulKndScCode=04&schYm=" + DateTime.Now.ToString("yyyy")+ DateTime.Now.ToString("MM");
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync(url);

            HtmlNode docNodes = doc.DocumentNode;

            var query = from table in docNodes.Descendants("tbody")
                        from row in table.Descendants("tr")
                        from cell in row.Descendants("td")
                        from content in cell.Descendants("div")
                        select new { Table = table.Id, CellText = cell.InnerText, CellHtml = cell.InnerHtml, ContentHtml = content.InnerHtml};
            Debug.WriteLine(DateTime.Now.Day.ToString()+"AA");
            foreach (var content in query)
            {
                string text = content.ContentHtml;
                string[] chunk = text.Split("<br/>");

                for (int i=0; i < chunk.Length; i++)
                {
                    if (chunk[i].Contains(DateTime.Now.Day.ToString()) && chunk[i].IndexOf(DateTime.Now.Day.ToString())==0)
                    {
                        chunk[i]=chunk[i].Replace(DateTime.Now.Day.ToString(), "");
                        parshing(chunk);
                    }
                }
            }
        }

        public async Task settingAsync(int month, int day)
        {
            DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateTime date = now;
            if (month - date.Month != 0 || day - date.Day != 0)
                date = date.AddMonths(month-date.Month).AddDays(day-date.Day);
            string url = "https://stu.dge.go.kr/sts_sci_md00_001.do?schulCode=D100000282&schulCrseScCode=4&schulKndScCode=04&schYm=" + date.ToString("yyyy") + date.ToString("MM");
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync(url);

            HtmlNode docNodes = doc.DocumentNode;

            var query = from table in docNodes.Descendants("tbody")
                        from row in table.Descendants("tr")
                        from cell in row.Descendants("td")
                        from content in cell.Descendants("div")
                        select new { Table = table.Id, CellText = cell.InnerText, CellHtml = cell.InnerHtml, ContentHtml = content.InnerHtml };

            foreach (var content in query)
            {
                string text = content.ContentHtml;
                string[] chunk = text.Split("<br/>");

                for (int i = 0; i < chunk.Length; i++)
                {
                    if (chunk[i].Contains(day.ToString()) && !chunk[i].Contains("[석식]") &&chunk[i].IndexOf(day.ToString()) == 0)
                    {
                        if (DateTime.Compare(now, date) == -1 || DateTime.Compare(now, date) == 0)
                            isExitDay = true;
                    }
                }
            }
        }

        public void parshing(string[] texts)
        {
            string data="";
            foreach (string tmp in texts) 
            {
                data += tmp;
            }
            texts = data.Split("<br>");
            foreach (string text in texts)
            {
                if (text.Trim().Length < 1)
                    continue;
                 
                if (food.Length > 1)
                    food += "\n" + text;
                else
                    food += text;
            }
        }
        public bool getExitDay()
        {
            return isExitDay;
        }
        public string getFood()
        {
            return food;
        }
    }
}