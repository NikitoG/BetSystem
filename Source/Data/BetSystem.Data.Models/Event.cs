﻿namespace BetSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using BetSystem.Data.Common.Models;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Event : BaseModel<int>
    {
        public ICollection<Match> games;

        public Event()
        {
            this.games = new HashSet<Match>();
        }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsLive { get; set; }
        
        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }

        public virtual ICollection<Match> Games
        {
            get { return this.games; }
            set { this.games = value; }
        }
    }
}
