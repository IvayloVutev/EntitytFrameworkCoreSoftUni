
using P02_FootballBetting.Data.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class Game
    {
        public Game()
        {
              this.Bets = new HashSet<Bet>();
              this.PlayersStatistics = new HashSet<PlayerStatistic>();
        }

        [Key]
        public int GameId { get; set; }

        public int HomeTeamId { get; set; }

        public virtual Team HomeTeam { get; set; }

        public int AwayTeamId { get; set; }

        public virtual Team AwayTeam { get; set; }


        public int HomeTeamGoals { get; set; }

        public int AwayTeamGoals { get; set; }

        public DateTime DateTime { get; set; }

        public double HomeTeamBetRate { get; set; }

        public double AwayTeamBetRate { get; set; }

        public double DrawBetRate { get; set; }

        public Prediction Result { get; set; }

        public ICollection<Bet> Bets { get; set; }
        public virtual ICollection<PlayerStatistic> PlayersStatistics { get; set; }

    }
}
