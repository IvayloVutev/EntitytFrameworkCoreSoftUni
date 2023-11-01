
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class Town
    {
        public Town()
        {
             this.Teams = new HashSet<Team>();   
        }

        [Key]
        public int TownId { get; set; }

        public string Name { get; set; } = null!;

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Team> Teams { get;}
    }
}
