using BetSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RssCrawler
{
    public class EventsComparer : IEqualityComparer<Event>
    {
        public int GetHashCode(Event co)
        {
            if (co == null)
            {
                return 0;
            }
            return co.Key.GetHashCode();
        }

        public bool Equals(Event x1, Event x2)
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
            return x1.Key == x2.Key && x1.Name == x2.Name && x1.CategoryId == x2.CategoryId && x1.IsLive == x2.IsLive;
        }
    }
}
