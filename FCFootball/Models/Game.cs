using System;
using System.Collections.Generic;

namespace FCFootball.Models
{
    public partial class Game
    {
        public Game()
        {
            GameStats = new HashSet<GameStat>();
            PlayerStats = new HashSet<PlayerStat>();
        }

        public int GameID { get; set; }
        public string? Opponent { get; set; }
        public DateTime? Date { get; set; }
        public bool? Home { get; set; }
        public string? Result { get; set; }
        public byte? TeamScore { get; set; }
        public byte? OpponentScore { get; set; }

        public virtual ICollection<GameStat> GameStats { get; set; }
        public virtual ICollection<PlayerStat> PlayerStats { get; set; }
    }
}
