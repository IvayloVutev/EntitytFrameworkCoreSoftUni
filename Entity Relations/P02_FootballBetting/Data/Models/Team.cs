
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models
{
    public class Team
    {
        public Team()
        {
            this.HomeGames = new HashSet<Game>();
            this.AwayGames = new HashSet<Game>();
            this.Players = new HashSet<Player>();
        }

        [Key]
        public int TeamId { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;

        [Required]
        public string LogoUrl { get; set; } = null!;

        public string Initials { get; set; } = null!;

        public decimal Budget { get; set; }

        public int PrimaryKitColorId { get; set; }

        public virtual Color PrimaryKitColor { get; set; }

        public int SecondaryKitColorId { get; set; }

        public virtual Color SecondaryKitColor { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }


        [InverseProperty(nameof(Game.HomeTeam))]
        public virtual ICollection<Game>HomeGames { get; set; }

        [InverseProperty(nameof(Game.AwayTeam))]
        public virtual ICollection<Game>AwayGames { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
