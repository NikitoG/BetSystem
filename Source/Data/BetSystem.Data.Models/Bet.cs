namespace BetSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    using BetSystem.Data.Common.Models;

    public class Bet : BaseModel<int>
    {
        public ICollection<Odd> odds;

        public Bet()
        {
            this.odds = new HashSet<Odd>();
        }

        public int Key { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public bool IsLive { get; set; }

        [Required]
        public int MatchId { get; set; }

        public virtual Match Match { get; set; }

        public virtual ICollection<Odd> Odds
        {
            get { return this.odds; }
            set { this.odds = value; }
        }
    }
}
