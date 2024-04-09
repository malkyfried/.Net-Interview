using RealCommerce.Models;
using System.ServiceModel.Syndication;
using System.Xml;

namespace RealCommerce.Services
{
    public class newsService
    {
        public List<Post> newsItems { get; set; } // Property to hold news items

        // Constructor to initialize news items from RSS
        public newsService()
        {
            GetNewsItemsFromRSS();
        }

        // Method to get post body by id
        public string GetPostBodyById(string id)
        {
            try
            {
                if (newsItems != null) // Check if news items are initialized
                {
                    var post = newsItems.FirstOrDefault(p => p.Id == id); // Find post by id
                    if (post != null)
                    {
                        return post.Body; // Return body if post found
                    }
                    else
                    {
                        return "Post not found."; // Return message if post not found
                    }
                }
                else
                {
                    return "News items not initialized."; // Return message if news items not initialized
                }
            }
            catch (Exception ex)
            {
                return "Error fetching post body: " + ex.Message; // Return error message if exception occurs
            }
        }

        // Method to fetch news items from RSS feed
        public List<Post> GetNewsItemsFromRSS()
        {
            newsItems = new List<Post>(); // Initialize news items list

            string rssUrl = "http://news.google.com/news?pz=1&cf=all&ned=en_il&hl=en&output=rss";

            using (XmlReader reader = XmlReader.Create(rssUrl))
            {
                SyndicationFeed feed = SyndicationFeed.Load(reader); // Load RSS feed

                // Iterate through feed items and populate news items list
                foreach (SyndicationItem item in feed.Items)
                {
                    Post newsItem = new Post
                    {
                        Id = item.Id,
                        Title = item.Title.Text,
                        Body = item.Summary.Text,
                        LinkPost = item.Links[0].Uri.AbsoluteUri
                    };
                    newsItems.Add(newsItem); // Add news item to list
                }
            }
            return newsItems; // Return list of news items
        }
    }
}
