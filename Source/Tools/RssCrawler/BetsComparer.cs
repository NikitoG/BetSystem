namespace RssCrawler
{
    using BetSystem.Data.Models;
    using System.Collections.Generic;

    public class BetsComparer : IEqualityComparer<Bet>
    {
        public int GetHashCode(Bet co)
        {
            if (co == null)
            {
                return 0;
            }
            return co.Key.GetHashCode();
        }

        public bool Equals(Bet x1, Bet x2)
        {
            if (object.ReferenceEquals(x1, x2))
            {
                return true;
            }
            if (object.ReferenceEquals(x1, null) ||
                object.ReferenceEquals(x2, null))
            {
                return false;
            }
            return x1.Key == x2.Key && x1.Name == x2.Name && x1.IsLive == x2.IsLive;
        }
    }
}
