using HtmlAgilityPack;
using RealCommerce.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RealCommerce.Services
{
    public class newsService
    {
        private readonly HttpClient _httpClient;

        public newsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetPostBodyAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string htmlContent = await response.Content.ReadAsStringAsync();

                // Use HtmlAgilityPack to parse the HTML content
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                // Extract only the text content from the HTML content
                string body = htmlDocument.DocumentNode.InnerText.Trim();
                Console.WriteLine(body);
                return body;
            }
            catch (Exception ex)
            {
                return "Error fetching post body: " + ex.Message;
            }
        }

        public List<Post> GetNewsItemsFromRSS()
        {
            List<Post> newsItems = new List<Post>();

            string rssUrl = "http://news.google.com/news?pz=1&cf=all&ned=en_il&hl=en&output=rss";

            // Logic to fetch news items from RSS feed
            using (XmlReader reader = XmlReader.Create(rssUrl))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader);

                foreach (SyndicationItem item in feed.Items)
                {
                    Post newsItem = new Post
                    {
                        Title = item.Title.Text,
                        //Body = item.Summary.Text,
                        LinkPost = item.Links[0].Uri.AbsoluteUri
                    };
                    newsItems.Add(newsItem);
                }
            }

            //Mock data for demonstration
            //for (int i = 1; i <= 5; i++)
            //{
            //    Post newsItem = new Post
            //    {
            //        Title = $"Mock Title {i}",
            //        Body = $"Mock Body {i}",
            //        LinkPost = $"http://example.com/post/{i}"
            //    };
            //    newsItems.Add(newsItem);
            //}
            return newsItems;
        }
    }
}
