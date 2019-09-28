using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SideAssistant.Content
{
    class RSSParser
    {
        public ObservableCollection<ListViewData.RSSList> list = new ObservableCollection<ListViewData.RSSList>();

        public async void loadRSS()
        {
            if (Properties.Settings.Default.rss_link != null)
            {
                var url = Properties.Settings.Default.rss_link;
                // 指定したサイトのHTMLをストリームで取得する
                var client = new HttpClient();
                using (var stream = await client.GetAsync(new Uri(url)))
                {
                    var response = await stream.Content.ReadAsStringAsync();
                    var parser = new HtmlParser();
                    var document = await parser.ParseDocumentAsync(response);

                    //取り出す
                    var titleDoc = document.GetElementsByTagName("title");
                    var linkDoc = document.GetElementsByTagName("guid");
                    for (var i = 0; i < linkDoc.Length; i++)
                    {

                        Match smIDRegex = Regex.Match(linkDoc[i].InnerHtml, "(sm|nw)([0-9]+)");
                        var smID = "https://www.nicovideo.jp/watch/" + smIDRegex.Value;
                        var item = new ListViewData.RSSList { title = titleDoc[i + 1].TextContent, url = smID };
                        list.Add(item);
                    }
                }
            }
        }
    }
}
