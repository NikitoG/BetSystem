namespace RssCrawler
{
    using BetSystem.Data.Models;
    using System.Collections.Generic;

    public class SportsComparer : IEqualityComparer<Sport>
    {
        public int GetHashCode(Sport co)
        {
            if (co == null)
            {
                return 0;
            }
            return co.Key.GetHashCode();
        }

        public bool Equals(Sport x1, Sport x2)
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
            return x1.Key == x2.Key && x1.Name == x2.Name;
        }
    }
}
