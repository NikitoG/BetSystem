using BetSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssCrawler
{
    public class GamesComparer : IEqualityComparer<Match>
    {
        public int GetHashCode(Match co)
        {
            if (co == null)
            {
                return 0;
            }
            return co.Key.GetHashCode();
        }

        public bool Equals(Match x1, Match x2)
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
            return x1.Key == x2.Key && x1.Name == x2.Name && x1.MatchType == x2.MatchType && x1.StartDate == x2.StartDate;
        }
    }
}
