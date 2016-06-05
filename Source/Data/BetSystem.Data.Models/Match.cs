namespace BetSystem.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using BetSystem.Data.Common.Models;
    using System.Collections.Generic;
    public class Match : BaseModel<int>
    {
        public ICollection<Bet> bets;

        public Match()
        {
            this.bets = new HashSet<Bet>();
        }

        public int Key { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public MatchType MatchType { get; set; }

        public DateTime StartDate { get; set; }
        
        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        public virtual ICollection<Bet> Bets
        {
            get { return this.bets; }
            set { this.bets = value; }
        }
    }
}
