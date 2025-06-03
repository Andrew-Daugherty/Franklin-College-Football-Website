using System;
using System.Collections.Generic;

namespace FCFootball.Models
{
    public partial class Player
    {
        public Player()
        {
            PlayerStats = new HashSet<PlayerStat>();
        }

        public int PlayerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Height { get; set; }
        public short? Weight { get; set; }
        public string? Position { get; set; }
        public byte? Number { get; set; }
        public string? Image { get; set; }

		public virtual ICollection<PlayerStat> PlayerStats { get; set; }
    }
}
