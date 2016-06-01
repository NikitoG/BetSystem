namespace BetSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BetSystem.Data.Common.Models;
    using System.Collections.Generic;
    public class Event : BaseModel<int>
    {
        public ICollection<Match> games;

        public Event()
        {
            this.games = new HashSet<Match>();
        }

        public int Key { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsLive { get; set; }

        [Required]
        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }

        public virtual ICollection<Match> Games
        {
            get { return this.games; }
            set { this.games = value; }
        }
    }
}
