namespace BetSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.ServiceModel.Syndication;
    using System.Xml;
    using System.Xml.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<BetSystemDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BetSystemDbContext context)
        {
            var feedUrl = @"http://vitalbet.net/sportxml";
            try
            {
                var feedContent = XDocument.Load(feedUrl);
                var feed = SyndicationFeed.Load(feedContent.CreateReader());
                //var webClient = new WebClient();
                //// hide ;-)
                //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                //// fetch feed as string
                //var content = webClient.OpenRead(feedUrl);
                //var contentReader = new StreamReader(content);
                //var rssFeedAsString = contentReader.ReadToEnd();
                //// convert feed to XML using LINQ to XML and finally create new XmlReader object
                //var feed = SyndicationFeed.Load(XDocument.Parse(rssFeedAsString).CreateReader());
                //var firstFeedItem = feed.Items.FirstOrDefault();
                //Console.WriteLine(firstFeedItem.Title.Text);
                //Console.WriteLine(firstFeedItem.Links.FirstOrDefault().Uri.AbsoluteUri);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
