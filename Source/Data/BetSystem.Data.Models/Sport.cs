namespace BetSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    using Common.Models;

    public class Sport : BaseModel<int>
    {
        public ICollection<Event> events;

        public Sport()
        {
            this.events = new HashSet<Event>();
        }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Index(IsUnique = true)]
        public string Key { get; set; }

        public virtual ICollection<Event> Events
        {
            get { return this.events; }
            set { this.events = value; }
        }
    }
}
