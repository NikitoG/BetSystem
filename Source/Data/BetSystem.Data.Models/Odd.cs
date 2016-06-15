namespace BetSystem.Data.Models
{

    using System.ComponentModel.DataAnnotations;

    using BetSystem.Data.Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Odd : BaseModel<int>
    {

        [Required]
        public string Name { get; set; }

        public decimal? SpecialBetValue { get; set; }

        public decimal Value { get; set; }
        
        public int BetId { get; set; }

        public virtual Bet Bet { get; set; }
    }
}
