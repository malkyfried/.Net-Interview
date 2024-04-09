# News Feed
 
News Feed is a web application that fetches news articles from a specified RSS feed and displays them on the webpage.
Users can view the list of news topics and click on individual articles to see more details.

**Features:**

**News Feed:** Displays a list of news topics fetched from an RSS feed.
**Post Details:** Allows users to click on a news topic to view more details including the post title, body, and link.
**Caching:** Utilizes caching to improve performance by storing news items in memory for a specified duration.

**Technologies Used:**

**ASP.NET Core MVC:** The project is built using ASP.NET Core MVC framework.
**C#:** Backend logic and business logic are implemented in C#.
**HTML/CSS/JavaScript:** Frontend development is done using HTML, CSS, and JavaScript.
**SyndicationFeed:** The SyndicationFeed class from System.ServiceModel.Syndication is used to parse RSS feeds.
**MemoryCache:** The MemoryCache class from Microsoft.Extensions.Caching.Memory is used to store news items in memory cache.
**Visual Studio:** The project is developed using Visual Studio IDE.

**Getting Started:**

Clone the repository: git clone <repository-url>
Open the solution file in Visual Studio.
Build the solution to restore dependencies.
Run the application and navigate to the specified URL.

**Usage:**

Upon running the application, users will be presented with a list of news topics.
Clicking on a news topic will display more details including the post title, body, and link.
The application utilizes caching to improve performance by storing news items in memory for a specified duration
