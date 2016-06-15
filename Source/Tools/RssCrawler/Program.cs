namespace RssCrawler
{
    using System;

    using BetSystem.Data;
    using BetSystem.Data.Common;
    using BetSystem.Data.Models;
    using BetSystem.Services.Data;
    using System.Xml.Linq;
    using System.Linq;
    using System.Threading;
    using System.Timers;

    public class Program
    {
        public static void Main(string[] args)
        {
            RssFeed.Start();
            Console.WriteLine("Ready");
        }
    }
}